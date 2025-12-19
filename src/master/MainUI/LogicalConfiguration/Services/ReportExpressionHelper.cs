using MainUI.LogicalConfiguration.Engine;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.LogicalConfiguration.Services
{
    /// <summary>
    /// 报表专用表达式计算助手
    /// 封装现有的 ExpressionEngine,为报表工具提供简化的API
    /// </summary>
    public class ReportExpressionHelper
    {
        private readonly ExpressionEngine _engine;
        private readonly GlobalVariableManager _variableManager;
        private readonly ILogger _logger;

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="variableManager">全局变量管理器</param>
        /// <param name="logger">日志记录器(可选)</param>
        public ReportExpressionHelper(GlobalVariableManager variableManager, ILogger logger = null)
        {
            var _plcManager = Program.ServiceProvider?.GetService<IPLCManager>();
            _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
            _logger = logger;

            // 初始化底层的表达式计算引擎
            _engine = new ExpressionEngine(_variableManager, _plcManager);
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 为报表场景评估表达式
        /// 自动处理错误,返回友好的错误提示
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>计算结果或错误提示</returns>
        public object EvaluateForReport(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    _logger?.LogWarning("报表表达式为空");
                    return string.Empty;
                }

                _logger?.LogDebug($"开始计算报表表达式: {expression}");

                // 使用底层引擎计算
                var result = _engine.EvaluateExpression(expression);

                if (result.Success)
                {
                    _logger?.LogDebug($"表达式计算成功: {expression} = {result.Result}");
                    return result.Result ?? string.Empty;
                }
                else
                {
                    // 针对报表优化错误提示
                    var errorMsg = FormatErrorMessage(expression, result.ErrorMessage);
                    _logger?.LogWarning($"表达式计算失败: {expression}, 错误: {result.ErrorMessage}");
                    return errorMsg;
                }
            }
            catch (Exception ex)
            {
                var errorMsg = $"[计算错误:{ex.Message}]";
                _logger?.LogError(ex, $"报表表达式计算异常: {expression}");
                return errorMsg;
            }
        }

        /// <summary>
        /// 异步评估表达式
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>计算结果或错误提示</returns>
        public async Task<object> EvaluateForReportAsync(string expression)
        {
            //return await Task.Run(() => EvaluateForReport(expression));
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    _logger?.LogWarning("报表表达式为空");
                    return string.Empty;
                }

                _logger?.LogDebug($"开始异步计算报表表达式: {expression}");

                // 使用底层引擎的真正异步方法计算（支持 PLC 读取）
                var result = await _engine.EvaluateExpressionAsync(expression);

                if (result.Success)
                {
                    _logger?.LogDebug($"表达式计算成功: {expression} = {result.Result}");
                    return result.Result ?? string.Empty;
                }
                else
                {
                    var errorMsg = FormatErrorMessage(expression, result.ErrorMessage);
                    _logger?.LogWarning($"表达式计算失败: {expression}, 错误: {result.ErrorMessage}");
                    return errorMsg;
                }
            }
            catch (Exception ex)
            {
                var errorMsg = $"[计算错误:{ex.Message}]";
                _logger?.LogError(ex, $"报表表达式计算异常: {expression}");
                return errorMsg;
            }
        }

        /// <summary>
        /// 验证表达式格式是否正确
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>是否有效</returns>
        public bool ValidateExpression(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                    return false;

                // 尝试计算表达式
                var result = _engine.EvaluateExpression(expression);
                return result.Success;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证表达式并获取详细信息
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>验证结果(是否有效, 错误消息)</returns>
        public (bool IsValid, string ErrorMessage) ValidateExpressionWithDetails(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                    return (false, "表达式不能为空");

                var result = _engine.EvaluateExpression(expression);

                if (result.Success)
                {
                    return (true, string.Empty);
                }
                else
                {
                    return (false, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// 获取表达式中引用的所有变量
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>变量名列表</returns>
        public List<string> GetReferencedVariables(string expression)
        {
            var variables = new List<string>();

            if (string.IsNullOrWhiteSpace(expression))
                return variables;

            try
            {
                // 匹配 {变量名} 格式
                var matches = System.Text.RegularExpressions.Regex.Matches(expression, @"\{(\w+)\}");

                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    var varName = match.Groups[1].Value;
                    if (!variables.Contains(varName))
                    {
                        variables.Add(varName);
                    }
                }

                _logger?.LogTrace($"表达式 '{expression}' 引用的变量: {string.Join(", ", variables)}");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, $"解析表达式变量失败: {expression}");
            }

            return variables;
        }

        /// <summary>
        /// 检查表达式中引用的变量是否都存在
        /// </summary>
        /// <param name="expression">表达式字符串</param>
        /// <returns>验证结果(是否有效, 缺失的变量列表)</returns>
        public (bool IsValid, List<string> MissingVariables) CheckVariableExistence(string expression)
        {
            var referencedVars = GetReferencedVariables(expression);
            var missingVars = new List<string>();

            foreach (var varName in referencedVars)
            {
                var variable = _variableManager.FindVariableByName(varName);
                if (variable == null)
                {
                    missingVars.Add(varName);
                }
            }

            var isValid = missingVars.Count == 0;

            if (!isValid)
            {
                _logger?.LogWarning($"表达式 '{expression}' 引用的变量不存在: {string.Join(", ", missingVars)}");
            }

            return (isValid, missingVars);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 格式化错误消息,使其更适合在报表中显示
        /// </summary>
        /// <param name="expression">原始表达式</param>
        /// <param name="errorMessage">原始错误消息</param>
        /// <returns>格式化后的错误消息</returns>
        private string FormatErrorMessage(string expression, string errorMessage)
        {
            // 简化错误消息
            if (errorMessage.Contains("变量未找到") || errorMessage.Contains("null"))
            {
                return "[变量不存在]";
            }

            if (errorMessage.Contains("除") && errorMessage.Contains("零"))
            {
                return "[除数为零]";
            }

            if (errorMessage.Contains("运算") || errorMessage.Contains("操作"))
            {
                return "[运算错误]";
            }

            if (errorMessage.Contains("语法") || errorMessage.Contains("解析"))
            {
                return "[表达式格式错误]";
            }

            // 返回简化的通用错误
            return $"[计算错误]";
        }

        #endregion

        #region 静态帮助方法

        /// <summary>
        /// 获取支持的函数列表
        /// </summary>
        /// <returns>函数名称和说明的字典</returns>
        public static Dictionary<string, string> GetSupportedFunctions()
        {
            return new Dictionary<string, string>
            {
                // 字符串函数
                { "LEN", "获取字符串长度\n示例: LEN({Text})" },
                { "UPPER", "转换为大写\n示例: UPPER({Name})" },
                { "LOWER", "转换为小写\n示例: LOWER({Name})" },
                { "TRIM", "去除首尾空格\n示例: TRIM({Text})" },
                { "SUBSTRING", "截取子字符串\n示例: SUBSTRING({Text}, 0, 5)" },
                { "REPLACE", "替换字符串\n示例: REPLACE({Text}, \"旧\", \"新\")" },

                // 日期函数
                { "NOW", "获取当前日期时间\n示例: NOW()" },
                { "TODAY", "获取当前日期\n示例: TODAY()" },
                { "FORMAT", "格式化日期或数值\n示例: FORMAT(NOW(), \"yyyy-MM-dd\")" },

                // 数学函数
                { "ABS", "取绝对值\n示例: ABS({Value})" },
                { "MAX", "获取最大值\n示例: MAX({Val1}, {Val2}, {Val3})" },
                { "MIN", "获取最小值\n示例: MIN({Val1}, {Val2}, {Val3})" }
            };
        }

        /// <summary>
        /// 获取支持的运算符列表
        /// </summary>
        /// <returns>运算符和说明的字典</returns>
        public static Dictionary<string, string> GetSupportedOperators()
        {
            return new Dictionary<string, string>
            {
                // 算术运算符
                { "+", "加法或字符串连接" },
                { "-", "减法" },
                { "*", "乘法" },
                { "/", "除法" },
                { "%", "取余" },

                // 比较运算符
                { "==", "等于" },
                { "!=", "不等于" },
                { "<", "小于" },
                { "<=", "小于等于" },
                { ">", "大于" },
                { ">=", "大于等于" },

                // 逻辑运算符
                { "&&", "逻辑与" },
                { "||", "逻辑或" },
                { "!", "逻辑非" }
            };
        }

        /// <summary>
        /// 获取表达式示例
        /// </summary>
        /// <returns>表达式示例列表</returns>
        public static List<(string Category, string Example, string Description)> GetExpressionExamples()
        {
            return new List<(string, string, string)>
            {
                // 基础运算
                ("基础运算", "{Var1} + {Var2}", "两个变量相加"),
                ("基础运算", "{Price} * 1.13", "计算含税价(税率13%)"),
                ("基础运算", "({Max} + {Min}) / 2", "计算平均值"),
                
                // 字符串操作
                ("字符串", "测试结果:{Result}", "字符串拼接"),
                ("字符串", "UPPER({Name})", "转换为大写"),
                ("字符串", "SUBSTRING({Text}, 0, 10)", "截取前10个字符"),
                
                // 日期格式化
                ("日期", "FORMAT(NOW(), \"yyyy-MM-dd\")", "格式化当前日期"),
                ("日期", "FORMAT(NOW(), \"HH:mm:ss\")", "格式化当前时间"),
                
                // 逻辑判断
                ("逻辑", "{Score} >= 60", "判断是否及格"),
                ("逻辑", "{Temp} > 20 && {Temp} < 30", "判断温度范围"),
                
                // 复杂示例
                ("复杂", "{Name}-FORMAT(NOW(), \"yyyyMMdd\")", "生成带日期的名称"),
                ("复杂", "MAX({Val1}, {Val2}, {Val3})", "获取三个值的最大值")
            };
        }

        #endregion
    }
}