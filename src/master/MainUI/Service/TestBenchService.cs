using MainUI.Diagnostic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace MainUI.Service
{
    /// <summary>
    /// 试验台服务类 - 提供试验台相关的业务逻辑
    /// </summary>
    public static class TestBenchService
    {
        private static TestBenchModel _currentTestBench;
        private static readonly TestBenchBLL _testBenchBLL = new();
        private static string _currentMachineIP;
        private static TestBenchIPConfig _ipConfig = new();

        /// <summary>
        /// 当前试验台ID
        /// </summary>
        public static int CurrentTestBenchID => _currentTestBench?.ID ?? 0;

        /// <summary>
        /// 当前试验台信息
        /// </summary>
        public static TestBenchModel CurrentTestBench => _currentTestBench;

        /// <summary>
        /// 当前机器IP地址
        /// </summary>
        public static string CurrentMachineIP => _currentMachineIP;

        /// <summary>
        /// 初始化试验台信息 - 基于IP地址自动识别
        /// </summary>
        public static void Initialize()
        {
            try
            {
                // 1. 尝试读取已保存的IP
                _ipConfig = new TestBenchIPConfig();
                var savedIP = _ipConfig.SelectedIP;

                if (!string.IsNullOrEmpty(savedIP))
                {
                    // 验证已保存的IP是否在数据库中存在
                    var testBench = FindTestBenchByIP(savedIP);

                    if (testBench != null && testBench.Status)
                    {
                        // IP有效,直接使用
                        _currentMachineIP = savedIP;
                        _currentTestBench = testBench;
                        NlogHelper.Default.Info($"使用已保存的IP: {_currentMachineIP}, 试验台: {testBench.BenchName}");
                        return;
                    }
                    else
                    {
                        NlogHelper.Default.Warn($"已保存的IP [{savedIP}] 在数据库中不存在或已禁用,需要重新选择");
                        // 清除无效的配置
                        ClearSavedIP();
                    }
                }

                // 2. 获取本机所有可用IP
                var availableIPs = GetAllLocalIPAddresses();

                if (availableIPs.Count == 0)
                {
                    throw new Exception("无法检测到有效的IP地址!\n\n请检查:\n1. 网络连接是否正常\n2. 网卡是否启用");
                }

                // 3. 获取数据库中所有已注册的试验台IP映射
                var ipTestBenchMap = GetIPTestBenchMapping();

                // 4. 显示IP选择窗体(不管是否有已注册的IP,都让用户选择)
                string selectedIP = ShowIPSelectionDialog(availableIPs, ipTestBenchMap);

                if (string.IsNullOrEmpty(selectedIP))
                {
                    // 用户取消选择,抛出异常退出
                    NlogHelper.Default.Info("用户取消选择IP,程序退出");
                    throw new Exception("用户取消选择IP地址,程序已退出!");
                }

                // 5. 验证选择的IP
                if (!ipTestBenchMap.ContainsKey(selectedIP))
                {
                    throw new Exception($"选择的IP地址 [{selectedIP}] 未在数据库中注册!");
                }

                var matchedTestBench = ipTestBenchMap[selectedIP];

                if (!matchedTestBench.Status)
                {
                    throw new Exception($"试验台 [{matchedTestBench.BenchName}] 已被禁用!");
                }

                // 6. 立即保存选择的IP
                _currentMachineIP = selectedIP;
                _currentTestBench = matchedTestBench;
                SaveSelectedIP(selectedIP);

                NlogHelper.Default.Info($"成功初始化试验台: {_currentTestBench.BenchName} (ID: {_currentTestBench.ID}, IP: {_currentMachineIP})");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"初始化试验台失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 根据IP地址查找试验台
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>匹配的试验台,未找到返回null</returns>
        private static TestBenchModel FindTestBenchByIP(string ipAddress)
        {
            try
            {
                var allTestBenches = _testBenchBLL.GetActiveTestBenches();

                if (allTestBenches == null || allTestBenches.Count == 0)
                {
                    NlogHelper.Default.Warn("系统中没有配置任何试验台");
                    return null;
                }

                // 精确匹配IP地址
                var matchedBench = allTestBenches.FirstOrDefault(tb =>
                    !string.IsNullOrEmpty(tb.BenchIP) &&
                    tb.BenchIP.Trim().Equals(ipAddress.Trim(), StringComparison.OrdinalIgnoreCase));

                if (matchedBench == null)
                {
                    NlogHelper.Default.Warn($"未找到IP地址 [{ipAddress}] 对应的试验台");
                    LogAvailableTestBenches(allTestBenches);
                }

                return matchedBench;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("查找试验台失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 记录所有可用的试验台信息(用于调试)
        /// </summary>
        private static void LogAvailableTestBenches(List<TestBenchModel> testBenches)
        {
            NlogHelper.Default.Info("系统中配置的试验台:");
            foreach (var bench in testBenches)
            {
                NlogHelper.Default.Info($"  - {bench.BenchName} (ID: {bench.ID}, IP: {bench.BenchIP ?? "未配置"}, 状态: {(bench.Status ? "启用" : "禁用")})");
            }
        }

        /// <summary>
        /// 判断当前用户是否可以查看所有试验台的数据
        /// </summary>
        /// <returns>true-可以查看所有数据, false-只能查看当前试验台数据</returns>
        public static bool CanViewAllBenchData()
        {
            try
            {
                if (_currentTestBench == null)
                {
                    NlogHelper.Default.Warn("试验台未初始化,默认不允许查看所有数据");
                    return false;
                }

                // DataScope: 0=仅本试验台, 1=所有试验台
                return _currentTestBench.DataScope == 1;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("检查数据查看权限失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 验证指定IP是否可以访问试验台
        /// </summary>
        /// <param name="ipAddress">要验证的IP地址</param>
        /// <returns>验证结果</returns>
        public static (bool IsValid, string Message) ValidateIPAccess(string ipAddress)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ipAddress))
                {
                    return (false, "IP地址不能为空");
                }

                var testBench = FindTestBenchByIP(ipAddress);

                if (testBench == null)
                {
                    return (false, $"IP地址 [{ipAddress}] 未在系统中注册");
                }

                if (!testBench.Status)
                {
                    return (false, $"试验台 [{testBench.BenchName}] 已被禁用");
                }

                return (true, $"验证成功,试验台: {testBench.BenchName}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("验证IP访问权限失败", ex);
                return (false, $"验证失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 手动切换试验台(需要管理员权限)
        /// </summary>
        /// <param name="testBenchId">试验台ID</param>
        /// <returns>切换是否成功</returns>
        public static bool SwitchTestBench(int testBenchId)
        {
            try
            {
                var testBench = _testBenchBLL.GetTestBench(testBenchId);

                if (testBench == null)
                {
                    NlogHelper.Default.Warn($"试验台ID {testBenchId} 不存在");
                    return false;
                }

                if (!testBench.Status)
                {
                    NlogHelper.Default.Warn($"试验台 {testBench.BenchName} 已被禁用");
                    return false;
                }

                _currentTestBench = testBench;
                NlogHelper.Default.Info($"手动切换到试验台: {testBench.BenchName} (ID: {testBench.ID})");
                return true;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("切换试验台失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取所有可用的网络IP地址(仅物理网卡)
        /// </summary>
        /// <returns>IP地址列表</returns>
        public static List<string> GetAllLocalIPAddresses()
        {
            var ipList = new List<string>();

            try
            {
                // 只获取物理网卡(有线或无线),排除虚拟网卡
                var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni => ni.OperationalStatus == OperationalStatus.Up
                              && (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||     // 有线网卡
                                  ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)  // 无线网卡
                              && !ni.Description.Contains("Virtual", StringComparison.OrdinalIgnoreCase)
                              && !ni.Description.Contains("VMware", StringComparison.OrdinalIgnoreCase)
                              && !ni.Description.Contains("VirtualBox", StringComparison.OrdinalIgnoreCase)
                              && !ni.Description.Contains("Hyper-V", StringComparison.OrdinalIgnoreCase));

                foreach (var networkInterface in networkInterfaces)
                {
                    var ipProperties = networkInterface.GetIPProperties();
                    var ipv4Addresses = ipProperties.UnicastAddresses
                        .Where(ip => ip.Address.AddressFamily == AddressFamily.InterNetwork
                                  && !IPAddress.IsLoopback(ip.Address))
                        .Select(ip => ip.Address.ToString());

                    ipList.AddRange(ipv4Addresses);
                }

                // 去重
                ipList = [.. ipList.Distinct()];
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取本机所有IP地址失败", ex);
            }

            return ipList;
        }

        /// <summary>
        /// 重新初始化试验台(用于配置更改后刷新)
        /// </summary>
        public static void Reinitialize()
        {
            try
            {
                NlogHelper.Default.Info("重新初始化试验台...");
                _currentTestBench = null;
                _currentMachineIP = null;
                Initialize();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("重新初始化试验台失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取试验台状态信息
        /// </summary>
        /// <returns>状态信息字典</returns>
        public static Dictionary<string, string> GetStatusInfo()
        {
            var info = new Dictionary<string, string>();

            try
            {
                info["当前IP"] = _currentMachineIP ?? "未获取";
                info["试验台名称"] = _currentTestBench?.BenchName ?? "未初始化";
                info["试验台编号"] = _currentTestBench?.BenchCode ?? "N/A";
                info["试验台位置"] = _currentTestBench?.Location ?? "N/A";
                info["数据权限"] = CanViewAllBenchData() ? "所有试验台" : "仅本试验台";
                info["配置IP"] = _currentTestBench?.BenchIP ?? "N/A";
                info["状态"] = _currentTestBench?.Status == true ? "启用" : "禁用";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取状态信息失败", ex);
                info["错误"] = ex.Message;
            }

            return info;
        }

        /// <summary>
        /// 保存选择的IP地址到配置文件
        /// </summary>
        private static void SaveSelectedIP(string ipAddress)
        {
            try
            {
                _ipConfig.SelectedIP = ipAddress;
                _ipConfig.Save();
                NlogHelper.Default.Info($"已保存选择的IP到配置文件: {ipAddress}");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存选择的IP失败", ex);
                throw; // 如果保存失败,应该抛出异常
            }
        }

        /// 获取IP与试验台的映射关系
        /// </summary>
        /// <returns>IP与试验台的字典映射</returns>
        public static Dictionary<string, TestBenchModel> GetIPTestBenchMapping()
        {
            var mapping = new Dictionary<string, TestBenchModel>();

            try
            {
                var allTestBenches = _testBenchBLL.GetActiveTestBenches();

                if (allTestBenches != null && allTestBenches.Count > 0)
                {
                    foreach (var bench in allTestBenches)
                    {
                        if (!string.IsNullOrEmpty(bench.BenchIP) && bench.Status)
                        {
                            mapping[bench.BenchIP.Trim()] = bench;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("获取IP试验台映射失败", ex);
            }

            return mapping;
        }

        /// <summary>
        /// 显示IP选择对话框
        /// </summary>
        /// <param name="availableIPs">可用的IP列表</param>
        /// <param name="ipTestBenchMap">IP与试验台的映射</param>
        /// <returns>用户选择的IP,取消返回null</returns>
        private static string ShowIPSelectionDialog(List<string> availableIPs, Dictionary<string, TestBenchModel> ipTestBenchMap)
        {
            try
            {
                using var selectForm = new frmSelectIP(availableIPs, ipTestBenchMap);
                if (selectForm.ShowDialog() == DialogResult.OK)
                {
                    return selectForm.SelectedIP;
                }
                return null;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("显示IP选择对话框失败", ex);
                return null;
            }
        }

        /// <summary>
        /// 清除已保存的IP配置(用于重新选择)
        /// </summary>
        public static void ClearSavedIP()
        {
            try
            {
                _ipConfig.SelectedIP = string.Empty;
                _ipConfig.Save();
                NlogHelper.Default.Info("已清除保存的IP配置");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("清除保存的IP配置失败", ex);
            }
        }
    }
}