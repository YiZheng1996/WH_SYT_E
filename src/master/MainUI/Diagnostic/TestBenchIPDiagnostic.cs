using MainUI.Service;
using System.Text;

namespace MainUI.Diagnostic
{
    /// <summary>
    /// 试验台IP诊断工具
    /// </summary>
    public static class TestBenchIPDiagnostic
    {
        /// <summary>
        /// 执行完整诊断并返回报告
        /// </summary>
        /// <returns>诊断报告</returns>
        public static string RunFullDiagnostic()
        {
            var report = new StringBuilder();
            report.AppendLine("========== 试验台IP诊断报告 ==========");
            report.AppendLine($"诊断时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine();

            try
            {
                // 1. 检查本机IP
                report.AppendLine("【1. 本机IP地址检测】");
                var localIPs = TestBenchService.GetAllLocalIPAddresses();

                if (localIPs.Count == 0)
                {
                    report.AppendLine("  ❌ 未检测到有效的IP地址");
                }
                else
                {
                    report.AppendLine($"  ✓ 检测到 {localIPs.Count} 个IP地址:");
                    foreach (var ip in localIPs)
                    {
                        report.AppendLine($"    - {ip}");
                    }
                }
                report.AppendLine();

                // 2. 当前使用的IP
                report.AppendLine("【2. 当前使用的IP地址】");
                var currentIP = TestBenchService.CurrentMachineIP;
                if (string.IsNullOrEmpty(currentIP))
                {
                    report.AppendLine("  ❌ 未获取到当前IP");
                }
                else
                {
                    report.AppendLine($"  ✓ 当前IP: {currentIP}");
                }
                report.AppendLine();

                // 3. 试验台配置状态
                report.AppendLine("【3. 试验台配置状态】");
                var statusInfo = TestBenchService.GetStatusInfo();
                foreach (var item in statusInfo)
                {
                    report.AppendLine($"  {item.Key}: {item.Value}");
                }
                report.AppendLine();

                // 4. IP验证结果
                report.AppendLine("【4. IP访问验证】");
                if (!string.IsNullOrEmpty(currentIP))
                {
                    var (isValid, message) = TestBenchService.ValidateIPAccess(currentIP);
                    report.AppendLine(isValid
                        ? $"  ✓ {message}"
                        : $"  ❌ {message}");
                }
                else
                {
                    report.AppendLine("  ⚠ 无法验证,当前IP未获取");
                }
                report.AppendLine();

                // 5. 数据库中的试验台配置
                report.AppendLine("【5. 数据库中的试验台配置】");
                var testBenchBLL = new TestBenchBLL();
                var allBenches = testBenchBLL.GetActiveTestBenches();

                if (allBenches == null || allBenches.Count == 0)
                {
                    report.AppendLine("  ⚠ 数据库中没有配置任何试验台");
                }
                else
                {
                    report.AppendLine($"  配置数量: {allBenches.Count} 个");
                    foreach (var bench in allBenches)
                    {
                        report.AppendLine($"  - [{bench.BenchCode}] {bench.BenchName}");
                        report.AppendLine($"    IP: {bench.BenchIP ?? "未配置"}");
                        report.AppendLine($"    位置: {bench.Location}");
                        report.AppendLine($"    状态: {(bench.Status ? "✓启用" : "❌禁用")}");
                        report.AppendLine($"    数据权限: {(bench.DataScope == 1 ? "所有试验台" : "仅本试验台")}");
                    }
                }
                report.AppendLine();

                // 6. 建议
                report.AppendLine("【6. 配置建议】");
                if (localIPs.Count > 0 && !string.IsNullOrEmpty(currentIP))
                {
                    var (isValid, _) = TestBenchService.ValidateIPAccess(currentIP);
                    if (!isValid)
                    {
                        report.AppendLine("  ⚠ 当前IP未在数据库中注册,建议:");
                        report.AppendLine($"    1. 在数据库 testbench 表中添加记录");
                        report.AppendLine($"    2. 设置 BenchIP = '{currentIP}'");
                        report.AppendLine($"    3. 设置 Status = 1 (启用)");
                        report.AppendLine($"    4. 重启程序");
                    }
                    else
                    {
                        report.AppendLine("  ✓ 配置正常,可以正常使用");
                    }
                }
                else
                {
                    report.AppendLine("  ❌ 网络配置异常,请检查:");
                    report.AppendLine("    1. 网络连接是否正常");
                    report.AppendLine("    2. 网卡是否启用");
                    report.AppendLine("    3. IP地址是否正确配置");
                }

            }
            catch (Exception ex)
            {
                report.AppendLine();
                report.AppendLine("【诊断过程发生错误】");
                report.AppendLine($"错误信息: {ex.Message}");
                report.AppendLine($"堆栈跟踪: {ex.StackTrace}");
            }

            report.AppendLine();
            report.AppendLine("========== 诊断报告结束 ==========");

            return report.ToString();
        }

        /// <summary>
        /// 保存诊断报告到文件
        /// </summary>
        /// <param name="filePath">文件路径,null则自动生成</param>
        /// <returns>保存的文件路径</returns>
        public static string SaveDiagnosticReport(string filePath = null)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    var fileName = $"TestBench_Diagnostic_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                    filePath = Path.Combine(Application.StartupPath, "Logs", fileName);
                }

                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var report = RunFullDiagnostic();
                File.WriteAllText(filePath, report, Encoding.UTF8);

                NlogHelper.Default.Info($"诊断报告已保存到: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("保存诊断报告失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 快速检查当前配置是否正常
        /// </summary>
        /// <returns>(是否正常, 错误信息)</returns>
        public static (bool IsHealthy, string Message) QuickHealthCheck()
        {
            try
            {
                // 1. 检查IP
                var currentIP = TestBenchService.CurrentMachineIP;
                if (string.IsNullOrEmpty(currentIP))
                {
                    return (false, "无法获取本机IP地址");
                }

                // 2. 验证IP
                var (isValid, message) = TestBenchService.ValidateIPAccess(currentIP);
                if (!isValid)
                {
                    return (false, message);
                }

                // 3. 检查试验台状态
                var currentBench = TestBenchService.CurrentTestBench;
                if (currentBench == null)
                {
                    return (false, "试验台未初始化");
                }

                if (!currentBench.Status)
                {
                    return (false, $"试验台 [{currentBench.BenchName}] 已被禁用");
                }

                return (true, $"配置正常,当前试验台: {currentBench.BenchName}");
            }
            catch (Exception ex)
            {
                return (false, $"健康检查失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 生成SQL配置语句(帮助管理员快速配置)
        /// </summary>
        /// <param name="benchName">试验台名称</param>
        /// <param name="benchCode">试验台编号</param>
        /// <param name="location">位置</param>
        /// <returns>SQL语句</returns>
        public static string GenerateConfigSQL(string benchName = "默认试验台",
                                              string benchCode = "BENCH-001",
                                              string location = "实验室")
        {
            var currentIP = TestBenchService.CurrentMachineIP;
            if (string.IsNullOrEmpty(currentIP))
            {
                currentIP = "请手动填写IP";
            }

            var sql = new StringBuilder();
            sql.AppendLine("-- 试验台配置SQL语句");
            sql.AppendLine("-- 请根据实际情况修改后执行");
            sql.AppendLine();
            sql.AppendLine("INSERT INTO testbench (");
            sql.AppendLine("  BenchCode,");
            sql.AppendLine("  BenchName,");
            sql.AppendLine("  Location,");
            sql.AppendLine("  Status,");
            sql.AppendLine("  BenchIP,");
            sql.AppendLine("  DataScope,");
            sql.AppendLine("  CreateTime");
            sql.AppendLine(") VALUES (");
            sql.AppendLine($"  '{benchCode}',");
            sql.AppendLine($"  '{benchName}',");
            sql.AppendLine($"  '{location}',");
            sql.AppendLine("  1,  -- 状态: 1=启用, 0=禁用");
            sql.AppendLine($"  '{currentIP}',  -- 当前检测到的IP");
            sql.AppendLine("  0,  -- 数据权限: 0=仅本试验台, 1=所有试验台");
            sql.AppendLine($"  '{DateTime.Now:yyyy-MM-dd HH:mm:ss}'");
            sql.AppendLine(");");

            return sql.ToString();
        }
    }
}
