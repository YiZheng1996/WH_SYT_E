using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 报表工具方法集合 - 灵活版本
    /// 支持固定值、变量、表达式、系统属性等多种数据源
    /// </summary>
    public class ReportMethods(GlobalVariableManager globalVariableManager) : DSLMethodBase
    {
        public override string Category => "报表工具";
        public override string Description => "提供Excel报表读写等功能";

        // 变量管理器
        private readonly GlobalVariableManager _globalVariableManager = 
            globalVariableManager ?? throw new ArgumentNullException(nameof(globalVariableManager));

        #region 公共方法

        /// <summary>
        /// 保存报表方法
        /// </summary>
        public async Task<bool> SaveReport(Parameter_SaveReport param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                await Task.CompletedTask;

                if (BaseTest.Report == null)
                    throw new InvalidOperationException("报表控件未初始化");

                if (string.IsNullOrWhiteSpace(param.ReportPath))
                    throw new ArgumentException("报表保存路径不能为空");

                BaseTest.Report.SaveAS(param.ReportPath);
                return true;
            }, false);
        }

        /// <summary>
        /// 读取单元格方法
        /// </summary>
        public async Task<object> ReadCells(Parameter_ReadCells param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                await Task.CompletedTask;

                if (BaseTest.Report == null)
                    throw new InvalidOperationException("报表控件未初始化");

                if (string.IsNullOrWhiteSpace(param.CellAddress))
                    throw new ArgumentException("单元格地址不能为空");

                var value = BaseTest.Report.Read(param.CellAddress);
                return value;
            }, (object)null);
        }

        /// <summary>
        /// 写入单元格方法 - 灵活版本
        /// 支持多种数据源类型
        /// </summary>
        public async Task<bool> WriteCells(Parameter_WriteCells param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                await Task.CompletedTask;

                if (BaseTest.Report == null)
                    throw new InvalidOperationException("报表控件未初始化,请确保已加载报表");

                if (param.Items == null || param.Items.Count == 0)
                    throw new ArgumentException("写入项列表不能为空");

                // 遍历写入每个单元格
                foreach (var item in param.Items)
                {
                    if (string.IsNullOrWhiteSpace(item.CellAddress))
                    {
                        NlogHelper.Default.Warn($"跳过空单元格地址");
                        continue;
                    }

                    try
                    {
                        // 根据数据源类型获取值
                        var value = await GetValueBySourceType(item);

                        // 应用格式化(如果有)
                        var formattedValue = ApplyFormatting(value, item.FormatString);

                        // 使用RWReport控件写入单元格
                        BaseTest.Report.Write(item.CellAddress, formattedValue);

                        NlogHelper.Default.Info($"成功写入单元格 {item.CellAddress}: {formattedValue}");
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"写入单元格 {item.CellAddress} 失败: {ex.Message}", ex);
                        throw new InvalidOperationException($"写入单元格 {item.CellAddress} 失败: {ex.Message}", ex);
                    }
                }

                return true;
            }, false);
        }

        #endregion

        #region 数据源处理方法

        /// <summary>
        /// 根据数据源类型获取值
        /// </summary>
        private async Task<object> GetValueBySourceType(WriteCellItem item)
        {
            try
            {
                return item.SourceType switch
                {
                    CellsDataSourceType.FixedValue => GetFixedValue(item),
                    CellsDataSourceType.Variable => GetVariableValue(item),
                    CellsDataSourceType.Expression => await EvaluateExpression(item),
                    CellsDataSourceType.SystemProperty => GetSystemPropertyValue(item),
                    _ => item.FixedValue ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取数据源值失败: {ex.Message}", ex);
                return $"[错误:{ex.Message}]";
            }
        }

        /// <summary>
        /// 获取固定值
        /// </summary>
        private object GetFixedValue(WriteCellItem item)
        {
            return item.FixedValue ?? string.Empty;
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        private object GetVariableValue(WriteCellItem item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.VariableName))
                {
                    NlogHelper.Default.Warn("变量名为空");
                    return string.Empty;
                }

                // 从全局变量管理器获取变量值
                var variable = _globalVariableManager?.FindVariableByName(item.VariableName);

                if (variable != null)
                {
                    return variable.VarValue ?? string.Empty;
                }
                else
                {
                    NlogHelper.Default.Warn($"变量 {item.VariableName} 不存在");
                    return $"[变量{item.VariableName}不存在]";
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取变量 {item.VariableName} 失败: {ex.Message}", ex);
                return $"[获取失败:{ex.Message}]";
            }
        }

        /// <summary>
        /// 计算表达式
        /// 支持变量替换、运算符、函数调用等
        /// </summary>
        private async Task<object> EvaluateExpression(WriteCellItem item)
        {
            try
            {
                await Task.CompletedTask;

                if (string.IsNullOrWhiteSpace(item.Expression))
                    return string.Empty;

                var expression = item.Expression;

                // 1. 替换变量 {VarName} 为实际值
                expression = ReplaceVariables(expression);

                // 2. 处理DateTime等系统函数
                expression = ProcessSystemFunctions(expression);

                // 3. 计算表达式
                var result = EvaluateSimpleExpression(expression);

                return result;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"计算表达式失败: {item.Expression}, 错误: {ex.Message}", ex);
                return $"[表达式错误:{ex.Message}]";
            }
        }

        /// <summary>
        /// 替换表达式中的变量
        /// 例如: {Var1} + {Var2} → 10 + 20
        /// </summary>
        private string ReplaceVariables(string expression)
        {
            // 匹配 {变量名} 格式
            var pattern = @"\{([^}]+)\}";
            var matches = Regex.Matches(expression, pattern);

            foreach (Match match in matches)
            {
                var varName = match.Groups[1].Value;
                var varValue = GetVariableValueByName(varName);

                // 替换变量为值
                expression = expression.Replace(match.Value, varValue?.ToString() ?? "0");
            }

            return expression;
        }

        /// <summary>
        /// 处理系统函数
        /// 例如: DateTime.Now, DateTime.Now.AddDays(-1)
        /// </summary>
        private string ProcessSystemFunctions(string expression)
        {
            try
            {
                // 处理 DateTime.Now
                if (expression.Contains("DateTime.Now"))
                {
                    // 简单替换 DateTime.Now 为实际值
                    var now = DateTime.Now;

                    // 检查是否有方法调用
                    var dateTimePattern = @"DateTime\.Now(\.[A-Za-z]+\([^\)]*\))*";
                    var match = Regex.Match(expression, dateTimePattern);

                    if (match.Success)
                    {
                        var dateTimeExpr = match.Value;
                        var result = EvaluateDateTimeExpression(dateTimeExpr);
                        expression = expression.Replace(dateTimeExpr, $"\"{result}\"");
                    }
                }

                return expression;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Warn($"处理系统函数失败: {ex.Message}");
                return expression;
            }
        }

        /// <summary>
        /// 计算DateTime表达式
        /// </summary>
        private string EvaluateDateTimeExpression(string expression)
        {
            try
            {
                // 示例: DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")
                var now = DateTime.Now;

                // 简单的字符串匹配处理
                if (expression.Contains("AddDays"))
                {
                    var daysMatch = Regex.Match(expression, @"AddDays\((-?\d+)\)");
                    if (daysMatch.Success)
                    {
                        var days = int.Parse(daysMatch.Groups[1].Value);
                        now = now.AddDays(days);
                    }
                }

                if (expression.Contains("AddHours"))
                {
                    var hoursMatch = Regex.Match(expression, @"AddHours\((-?\d+)\)");
                    if (hoursMatch.Success)
                    {
                        var hours = int.Parse(hoursMatch.Groups[1].Value);
                        now = now.AddHours(hours);
                    }
                }

                // 提取ToString格式
                var formatMatch = Regex.Match(expression, @"ToString\(""([^""]+)""\)");
                if (formatMatch.Success)
                {
                    var format = formatMatch.Groups[1].Value;
                    return now.ToString(format);
                }

                return now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Warn($"计算DateTime表达式失败: {ex.Message}");
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// 计算简单表达式 (四则运算)
        /// </summary>
        private object EvaluateSimpleExpression(string expression)
        {
            try
            {
                // 如果表达式是纯字符串(带引号),直接返回
                if (expression.StartsWith("\"") && expression.EndsWith("\""))
                {
                    return expression.Trim('"');
                }

                // 如果包含运算符,尝试计算
                if (expression.Contains("+") || expression.Contains("-") ||
                    expression.Contains("*") || expression.Contains("/"))
                {
                    // 使用 DataTable.Compute 计算表达式
                    var dataTable = new System.Data.DataTable();
                    var result = dataTable.Compute(expression, string.Empty);
                    return result;
                }

                // 否则直接返回
                return expression;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Warn($"计算表达式失败: {expression}, {ex.Message}");
                return expression;
            }
        }

        /// <summary>
        /// 获取系统属性值 (通过反射)
        /// 例如: NewUsers.NewUserInfo.Username
        /// </summary>
        private object GetSystemPropertyValue(WriteCellItem item)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(item.PropertyPath))
                    return string.Empty;

                var propertyPath = item.PropertyPath.Trim();

                // 分割属性路径
                var parts = propertyPath.Split('.');

                object currentObject = null;
                Type currentType = null;

                // 获取根对象
                switch (parts[0])
                {
                    case "NewUsers":
                        currentType = typeof(NewUsers);
                        break;
                    case "VarHelper":
                        currentType = typeof(VarHelper);
                        break;
                    case "DateTime":
                        currentType = typeof(DateTime);
                        break;
                    case "BaseTest":
                        currentType = typeof(BaseTest);
                        break;
                    default:
                        NlogHelper.Default.Warn($"未知的根对象: {parts[0]}");
                        return $"[未知对象:{parts[0]}]";
                }

                // 逐级获取属性值
                for (int i = 1; i < parts.Length; i++)
                {
                    var propertyName = parts[i];

                    // 检查是否是方法调用 (例如: ToString("yyyy-MM-dd"))
                    if (propertyName.Contains("("))
                    {
                        return EvaluateMethodCall(currentObject ?? currentType, propertyName);
                    }

                    // 获取属性或字段
                    if (currentObject == null)
                    {
                        // 静态属性/字段
                        var staticMember = currentType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Static)
                            ?? currentType.GetField(propertyName, BindingFlags.Public | BindingFlags.Static) as MemberInfo;

                        if (staticMember is PropertyInfo staticProp)
                            currentObject = staticProp.GetValue(null);
                        else if (staticMember is FieldInfo staticField)
                            currentObject = staticField.GetValue(null);
                        else
                        {
                            NlogHelper.Default.Warn($"找不到静态成员: {currentType.Name}.{propertyName}");
                            return $"[找不到:{propertyName}]";
                        }
                    }
                    else
                    {
                        // 实例属性/字段
                        currentType = currentObject.GetType();
                        var member = currentType.GetProperty(propertyName)
                            ?? currentType.GetField(propertyName) as MemberInfo;

                        if (member is PropertyInfo prop)
                            currentObject = prop.GetValue(currentObject);
                        else if (member is FieldInfo field)
                            currentObject = field.GetValue(currentObject);
                        else
                        {
                            NlogHelper.Default.Warn($"找不到成员: {currentType.Name}.{propertyName}");
                            return $"[找不到:{propertyName}]";
                        }
                    }

                    if (currentObject == null)
                    {
                        NlogHelper.Default.Warn($"属性值为null: {propertyPath}");
                        return string.Empty;
                    }
                }

                return currentObject ?? string.Empty;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取系统属性 {item.PropertyPath} 失败: {ex.Message}", ex);
                return $"[获取失败:{ex.Message}]";
            }
        }

        /// <summary>
        /// 执行方法调用
        /// 例如: ToString("yyyy-MM-dd"), AddDays(-1)
        /// </summary>
        private object EvaluateMethodCall(object obj, string methodCall)
        {
            try
            {
                // 解析方法名和参数
                var methodName = methodCall.Substring(0, methodCall.IndexOf('('));
                var argsString = methodCall.Substring(methodCall.IndexOf('(') + 1);
                argsString = argsString.TrimEnd(')');

                // 解析参数
                var args = new List<object>();
                if (!string.IsNullOrWhiteSpace(argsString))
                {
                    // 简单处理: 去除引号,分割逗号
                    var argParts = argsString.Split(',');
                    foreach (var arg in argParts)
                    {
                        var trimmedArg = arg.Trim().Trim('"');

                        // 尝试转换为数字
                        if (int.TryParse(trimmedArg, out int intValue))
                            args.Add(intValue);
                        else if (double.TryParse(trimmedArg, out double doubleValue))
                            args.Add(doubleValue);
                        else
                            args.Add(trimmedArg);
                    }
                }

                // 获取方法
                Type type = obj is Type t ? t : obj.GetType();
                var method = type.GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                if (method != null)
                {
                    var result = method.Invoke(obj is Type ? null : obj, args.ToArray());
                    return result;
                }

                NlogHelper.Default.Warn($"找不到方法: {methodName}");
                return $"[方法不存在:{methodName}]";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"执行方法调用失败: {methodCall}, {ex.Message}", ex);
                return $"[方法调用失败:{ex.Message}]";
            }
        }

        /// <summary>
        /// 根据变量名获取变量值
        /// </summary>
        private object GetVariableValueByName(string varName)
        {
            try
            {
                var variable = _globalVariableManager?.FindVariableByName(varName);
                return variable?.VarValue;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 应用格式化
        /// </summary>
        private object ApplyFormatting(object value, string formatString)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(formatString) || value == null)
                    return value;

                // 数值格式化
                if (value is double doubleVal)
                    return doubleVal.ToString(formatString);

                if (value is int intVal)
                    return intVal.ToString(formatString);

                if (value is decimal decimalVal)
                    return decimalVal.ToString(formatString);

                // 日期格式化
                if (value is DateTime dateVal)
                    return dateVal.ToString(formatString);

                return value;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Warn($"格式化失败: {ex.Message}");
                return value;
            }
        }

        #endregion
    }
}