using MainUI.LogicalConfiguration.Instrument.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CommandType = MainUI.LogicalConfiguration.Instrument.Models.CommandType;

namespace MainUI.LogicalConfiguration.Instrument.Services
{

    /// <summary>
    /// 仪器驱动管理服务实现
    /// </summary>
    public class InstrumentDriverService : IInstrumentDriverService
    {
        #region 字段

        private readonly ILogger<InstrumentDriverService> _logger;
        private readonly string _configFilePath;
        private readonly object _lockObject = new();
        private List<InstrumentDriver> _drivers = new();
        private bool _isLoaded = false;

        #endregion

        #region 事件

        public event Action DriversChanged;

        #endregion

        #region 构造函数

        public InstrumentDriverService(ILogger<InstrumentDriverService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // 配置文件路径
            var configDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "Instruments");
            if (!Directory.Exists(configDir))
            {
                Directory.CreateDirectory(configDir);
            }
            _configFilePath = Path.Combine(configDir, "InstrumentDrivers.json");

            // 初始加载
            _ = LoadDriversAsync();
        }

        #endregion

        #region 查询方法

        /// <summary>
        /// 获取所有启用的仪器驱动（用于下拉选择等场景）
        /// </summary>
        public async Task<List<InstrumentDriver>> GetAllDriversAsync()
        {
            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                // 只返回启用的驱动，用于下拉选择等场景
                return _drivers.Where(d => d.Enabled).ToList();
            }
        }

        /// <summary>
        /// 获取所有仪器驱动（包括禁用的，用于管理界面）
        /// </summary>
        /// <returns>所有驱动列表，包括已禁用的驱动</returns>
        public async Task<List<InstrumentDriver>> GetAllDriversIncludingDisabledAsync()
        {
            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                // 返回所有驱动，不过滤 Enabled 状态
                // 用于驱动管理界面，让用户可以看到和管理所有驱动（包括禁用的）
                return _drivers.ToList();
            }
        }

        public async Task<InstrumentDriver> GetDriverByIdAsync(string driverId)
        {
            if (string.IsNullOrEmpty(driverId))
                return null;

            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                return _drivers.FirstOrDefault(d => d.DriverId == driverId);
            }
        }

        public async Task<InstrumentDriver> GetDriverByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                return _drivers.FirstOrDefault(d =>
                    d.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                    d.DisplayName.Equals(name, StringComparison.OrdinalIgnoreCase));
            }
        }

        public async Task<List<InstrumentDriver>> GetDriversByCategoryAsync(InstrumentCategory category)
        {
            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                return _drivers.Where(d => d.Category == category && d.Enabled).ToList();
            }
        }

        #endregion

        #region 增删改方法

        public async Task<bool> AddDriverAsync(InstrumentDriver driver)
        {
            if (driver == null)
                return false;

            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                // 检查是否已存在同名驱动
                if (_drivers.Any(d => d.Name.Equals(driver.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    _logger.LogWarning("仪器驱动名称已存在: {Name}", driver.Name);
                    return false;
                }

                // 确保有唯一ID
                if (string.IsNullOrEmpty(driver.DriverId))
                {
                    driver.DriverId = Guid.NewGuid().ToString("N");
                }

                driver.CreatedTime = DateTime.Now;
                driver.ModifiedTime = DateTime.Now;

                _drivers.Add(driver);
            }

            var result = await SaveAsync();
            if (result)
            {
                _logger.LogInformation("添加仪器驱动成功: {Name}", driver.Name);
                DriversChanged?.Invoke();
            }

            return result;
        }

        public async Task<bool> UpdateDriverAsync(InstrumentDriver driver)
        {
            if (driver == null || string.IsNullOrEmpty(driver.DriverId))
                return false;

            await EnsureLoadedAsync();

            lock (_lockObject)
            {
                var index = _drivers.FindIndex(d => d.DriverId == driver.DriverId);
                if (index < 0)
                {
                    // 临时诊断日志
                    _logger.LogWarning("未找到要更新的仪器驱动: {DriverId}", driver.DriverId);
                    _logger.LogWarning("当前内存中所有驱动ID: {Ids}",
                        string.Join(", ", _drivers.Select(d => d.DriverId)));
                    return false;
                }

                driver.ModifiedTime = DateTime.Now;
                _drivers[index] = driver;
            }

            var result = await SaveAsync();
            if (result)
            {
                _logger.LogInformation("更新仪器驱动成功: {Name}", driver.Name);
                DriversChanged?.Invoke();
            }

            return result;
        }

        public async Task<bool> DeleteDriverAsync(string driverId)
        {
            if (string.IsNullOrEmpty(driverId))
                return false;

            await EnsureLoadedAsync();

            string driverName;
            lock (_lockObject)
            {
                var driver = _drivers.FirstOrDefault(d => d.DriverId == driverId);
                if (driver == null)
                {
                    _logger.LogWarning("未找到要删除的仪器驱动: {DriverId}", driverId);
                    return false;
                }

                driverName = driver.Name;
                _drivers.Remove(driver);
            }

            var result = await SaveAsync();
            if (result)
            {
                _logger.LogInformation("删除仪器驱动成功: {Name}", driverName);
                DriversChanged?.Invoke();
            }

            return result;
        }

        public async Task<InstrumentDriver> CloneDriverAsync(string driverId)
        {
            var sourceDriver = await GetDriverByIdAsync(driverId);
            if (sourceDriver == null)
                return null;

            var clonedDriver = sourceDriver.Clone();

            if (await AddDriverAsync(clonedDriver))
            {
                return clonedDriver;
            }

            return null;
        }

        #endregion

        #region 导入导出

        public async Task<bool> ExportDriverAsync(string driverId, string filePath)
        {
            try
            {
                var driver = await GetDriverByIdAsync(driverId);
                if (driver == null)
                {
                    _logger.LogWarning("导出失败，未找到仪器驱动: {DriverId}", driverId);
                    return false;
                }

                var json = JsonConvert.SerializeObject(driver, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                await File.WriteAllTextAsync(filePath, json);
                _logger.LogInformation("导出仪器驱动成功: {Name} -> {FilePath}", driver.Name, filePath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导出仪器驱动失败: {DriverId}", driverId);
                return false;
            }
        }

        public async Task<bool> ExportAllDriversAsync(string filePath)
        {
            try
            {
                await EnsureLoadedAsync();

                List<InstrumentDriver> driversToExport;
                lock (_lockObject)
                {
                    driversToExport = _drivers.ToList();
                }

                var json = JsonConvert.SerializeObject(driversToExport, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                await File.WriteAllTextAsync(filePath, json);
                _logger.LogInformation("导出所有仪器驱动成功: {Count} 个 -> {FilePath}", driversToExport.Count, filePath);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导出所有仪器驱动失败");
                return false;
            }
        }

        public async Task<InstrumentDriver> ImportDriverAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _logger.LogWarning("导入文件不存在: {FilePath}", filePath);
                    return null;
                }

                var json = await File.ReadAllTextAsync(filePath);

                // 尝试解析为单个驱动
                InstrumentDriver driver = null;
                try
                {
                    driver = JsonConvert.DeserializeObject<InstrumentDriver>(json);
                }
                catch
                {
                    // 如果失败，尝试解析为数组并取第一个
                    var drivers = JsonConvert.DeserializeObject<List<InstrumentDriver>>(json);
                    driver = drivers?.FirstOrDefault();
                }

                if (driver == null)
                {
                    _logger.LogWarning("导入文件格式无效: {FilePath}", filePath);
                    return null;
                }

                // 生成新ID避免冲突
                driver.DriverId = Guid.NewGuid().ToString("N");

                // 检查名称冲突，如有则重命名
                var existingDriver = await GetDriverByNameAsync(driver.Name);
                if (existingDriver != null)
                {
                    driver.Name = $"{driver.Name}_{DateTime.Now:yyyyMMddHHmmss}";
                    driver.DisplayName = $"{driver.DisplayName} (导入)";
                }

                if (await AddDriverAsync(driver))
                {
                    _logger.LogInformation("导入仪器驱动成功: {Name}", driver.Name);
                    return driver;
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "导入仪器驱动失败: {FilePath}", filePath);
                return null;
            }
        }

        #endregion

        #region 持久化

        public async Task<bool> SaveAsync()
        {
            try
            {
                List<InstrumentDriver> driversToSave;
                lock (_lockObject)
                {
                    driversToSave = _drivers.ToList();
                }

                var json = JsonConvert.SerializeObject(driversToSave, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                // 确保目录存在
                var dir = Path.GetDirectoryName(_configFilePath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                await File.WriteAllTextAsync(_configFilePath, json);
                _logger.LogDebug("保存仪器驱动配置成功: {Count} 个", driversToSave.Count);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存仪器驱动配置失败");
                return false;
            }
        }

        public async Task<bool> ReloadAsync()
        {
            _isLoaded = false;
            return await LoadDriversAsync();
        }

        private async Task<bool> LoadDriversAsync()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    _logger.LogInformation("仪器驱动配置文件不存在，创建默认配置");
                    lock (_lockObject)
                    {
                        _drivers = CreateDefaultDrivers();
                        _isLoaded = true;
                    }
                    await SaveAsync();
                    return true;
                }

                var json = await File.ReadAllTextAsync(_configFilePath);
                var loadedDrivers = JsonConvert.DeserializeObject<List<InstrumentDriver>>(json);

                lock (_lockObject)
                {
                    _drivers = loadedDrivers ?? new List<InstrumentDriver>();
                    _isLoaded = true;
                }

                _logger.LogInformation("加载仪器驱动配置成功: {Count} 个", _drivers.Count);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "加载仪器驱动配置失败");
                lock (_lockObject)
                {
                    _drivers = new List<InstrumentDriver>();
                    _isLoaded = true;
                }
                return false;
            }
        }

        private async Task EnsureLoadedAsync()
        {
            if (!_isLoaded)
            {
                await LoadDriversAsync();
            }
        }

        #endregion

        #region 默认驱动配置

        /// <summary>
        /// 创建示例默认驱动
        /// </summary>
        private List<InstrumentDriver> CreateDefaultDrivers()
        {
            var drivers = new List<InstrumentDriver>();

            // 示例1: TCP仪器(SCPI协议万用表)
            var multimeter = new InstrumentDriver
            {
                Name = "Keysight_34461A",
                DisplayName = "Keysight 34461A 万用表",
                Category = InstrumentCategory.Multimeter,
                Manufacturer = "Keysight",
                Model = "34461A",
                Description = "6½位数字万用表，支持SCPI命令",
                ProtocolType = ProtocolType.TcpIp,
                FrameConfig = new FrameConfig
                {
                    Enabled = true,
                    ResponseTerminator = "\n"
                },
                Commands = new List<InstrumentCommand>
                {
                    new InstrumentCommand
                    {
                        Name = "ReadDCVoltage",
                        DisplayName = "读取直流电压",
                        CommandType = CommandType.Query,
                        Description = "测量并返回直流电压值",
                        RequestTemplate = "MEAS:VOLT:DC?\n",
                        WaitForResponse = true,
                        ParseRules = new List<ResponseParseRule>
                        {
                            new ResponseParseRule
                            {
                                Name = "Voltage",
                                TargetVariable = "MeasuredVoltage",
                                ParseType = "Position",
                                StartPosition = 0,
                                Length = -1,
                                TargetDataType = DataType.Double
                            }
                        }
                    },
                    new InstrumentCommand
                    {
                        Name = "ReadACVoltage",
                        DisplayName = "读取交流电压",
                        CommandType = CommandType.Query,
                        Description = "测量并返回交流电压值",
                        RequestTemplate = "MEAS:VOLT:AC?\n",
                        WaitForResponse = true,
                        ParseRules = new List<ResponseParseRule>
                        {
                            new ResponseParseRule
                            {
                                Name = "ACVoltage",
                                TargetVariable = "MeasuredACVoltage",
                                ParseType = "Position",
                                StartPosition = 0,
                                Length = -1,
                                TargetDataType = DataType.Double
                            }
                        }
                    },
                    new InstrumentCommand
                    {
                        Name = "ReadResistance",
                        DisplayName = "读取电阻",
                        CommandType = CommandType.Query,
                        Description = "测量并返回电阻值",
                        RequestTemplate = "MEAS:RES?\n",
                        WaitForResponse = true,
                        ParseRules = new List<ResponseParseRule>
                        {
                            new ResponseParseRule
                            {
                                Name = "Resistance",
                                TargetVariable = "MeasuredResistance",
                                ParseType = "Position",
                                StartPosition = 0,
                                Length = -1,
                                TargetDataType = DataType.Double
                            }
                        }
                    },
                    new InstrumentCommand
                    {
                        Name = "GetIdentity",
                        DisplayName = "获取设备信息",
                        CommandType = CommandType.Query,
                        Description = "查询设备标识信息",
                        RequestTemplate = "*IDN?\n",
                        WaitForResponse = true
                    }
                }
            };

            var tcpConfig = new TcpProtocolConfig
            {
                IpAddress = "192.168.1.100",
                Port = 5025,
                ConnectionTimeout = 5000,
                ReadTimeout = 3000
            };
            multimeter.SetProtocolConfig(tcpConfig);
            drivers.Add(multimeter);

            // 示例2: 串口仪器(温控器)
            var tempController = new InstrumentDriver
            {
                Name = "Generic_TempController",
                DisplayName = "通用温控器",
                Category = InstrumentCategory.TemperatureController,
                Manufacturer = "通用",
                Model = "TC-100",
                Description = "串口通讯温度控制器",
                ProtocolType = ProtocolType.Serial,
                FrameConfig = new FrameConfig
                {
                    Enabled = true,
                    FrameHeader = "02",
                    FrameFooter = "03",
                    ChecksumType = ChecksumType.XOR
                },
                Commands = new List<InstrumentCommand>
                {
                    new InstrumentCommand
                    {
                        Name = "ReadTemperature",
                        DisplayName = "读取当前温度",
                        CommandType = CommandType.Read,
                        Description = "读取当前温度值",
                        RequestTemplate = "RT",
                        WaitForResponse = true,
                        ParseRules = new List<ResponseParseRule>
                        {
                            new ResponseParseRule
                            {
                                Name = "Temperature",
                                TargetVariable = "CurrentTemperature",
                                ParseType = "Position",
                                StartPosition = 2,
                                Length = 4,
                                TargetDataType = DataType.Double,
                                ScaleFactor = 0.1
                            }
                        }
                    },
                    new InstrumentCommand
                    {
                        Name = "SetTemperature",
                        DisplayName = "设置目标温度",
                        CommandType = CommandType.Write,
                        Description = "设置目标温度值",
                        RequestTemplate = "ST{Temperature}",
                        WaitForResponse = true,
                        Parameters = new List<CommandParameter>
                        {
                            new CommandParameter
                            {
                                Name = "Temperature",
                                DisplayName = "目标温度",
                                DataType = DataType.Double,
                                Required = true,
                                Description = "要设置的目标温度值",
                                MinValue = 0,
                                MaxValue = 300
                            }
                        },
                        SuccessIndicator = "OK"
                    }
                }
            };

            var serialConfig = new SerialProtocolConfig
            {
                PortName = "COM1",
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBitsType.One,
                Parity = ParityType.None
            };
            tempController.SetProtocolConfig(serialConfig);
            drivers.Add(tempController);

            // 示例3: Modbus设备(传感器)
            var modbusSensor = new InstrumentDriver
            {
                Name = "Modbus_Sensor",
                DisplayName = "Modbus传感器",
                Category = InstrumentCategory.Sensor,
                Manufacturer = "通用",
                Model = "MS-200",
                Description = "Modbus RTU协议传感器",
                ProtocolType = ProtocolType.ModbusRtu,
                Commands = new List<InstrumentCommand>
                {
                    new InstrumentCommand
                    {
                        Name = "ReadHoldingRegisters",
                        DisplayName = "读取保持寄存器",
                        CommandType = CommandType.Read,
                        Description = "读取保持寄存器(功能码03)",
                        RequestTemplate = "03,{StartAddress},{Count}",
                        WaitForResponse = true,
                        Parameters = new List<CommandParameter>
                        {
                            new CommandParameter
                            {
                                Name = "StartAddress",
                                DisplayName = "起始地址",
                                DataType = DataType.Integer,
                                DefaultValue = "0",
                                Required = true
                            },
                            new CommandParameter
                            {
                                Name = "Count",
                                DisplayName = "寄存器数量",
                                DataType = DataType.Integer,
                                DefaultValue = "1",
                                Required = true
                            }
                        }
                    },
                    new InstrumentCommand
                    {
                        Name = "WriteSingleRegister",
                        DisplayName = "写入单个寄存器",
                        CommandType = CommandType.Write,
                        Description = "写入单个保持寄存器(功能码06)",
                        RequestTemplate = "06,{Address},{Value}",
                        WaitForResponse = true,
                        Parameters = new List<CommandParameter>
                        {
                            new CommandParameter
                            {
                                Name = "Address",
                                DisplayName = "寄存器地址",
                                DataType = DataType.Integer,
                                DefaultValue = "0",
                                Required = true
                            },
                            new CommandParameter
                            {
                                Name = "Value",
                                DisplayName = "写入值",
                                DataType = DataType.Integer,
                                Required = true
                            }
                        }
                    }
                }
            };

            var modbusConfig = new ModbusProtocolConfig
            {
                SlaveAddress = 1,
                PortName = "COM2",
                BaudRate = 9600,
                DataBits = 8,
                StopBits = StopBitsType.One,
                Parity = ParityType.None
            };
            modbusConfig.SetModbusType(false); // RTU模式
            modbusSensor.SetProtocolConfig(modbusConfig);
            drivers.Add(modbusSensor);

            return drivers;
        }

        #endregion
    }
}
