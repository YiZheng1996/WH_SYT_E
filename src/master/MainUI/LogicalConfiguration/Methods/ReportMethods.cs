using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services;
using MainUI.Service;
using System.Reflection;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 报表工具方法集合
    /// 使用封装后的 ReportExpressionHelper
    /// </summary>
    public class ReportMethods : DSLMethodBase
    {
        public override string Category => "报表工具";
        public override string Description => "提供Excel报表读写等功能";

        // 变量管理器
        private readonly GlobalVariableManager _globalVariableManager;

        // 报表表达式助手
        private readonly Lazy<ReportExpressionHelper> _expressionHelper;

        #region 构造函数

        public ReportMethods(GlobalVariableManager globalVariableManager)
        {
            _globalVariableManager = globalVariableManager ?? throw new ArgumentNullException(nameof(globalVariableManager));

            // 延迟初始化表达式助手
            _expressionHelper = new Lazy<ReportExpressionHelper>(() =>
                new ReportExpressionHelper(_globalVariableManager));
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 保存报表方法
        /// </summary>
        public async Task<bool> SaveReport(Parameter_SaveReport param)
        {
            return await ExecuteWithLogging(param, async () =>
            {
                await Task.CompletedTask;

                if (string.IsNullOrWhiteSpace(param.ReportPath))
                    throw new ArgumentException("报表保存路径不能为空");

                // 使用 ReportService 获取报表控件
                ReportService.InvokeOnReportControl(report =>
                {
                    report.SaveAS(param.ReportPath);
                });

                NlogHelper.Default.Info($"报表已保存到: {param.ReportPath}");
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

                // 如果没有读取项,返回 null
                if (param.ReadItems == null || param.ReadItems.Count == 0)
                {
                    NlogHelper.Default.Warn("读取项列表为空");
                    return null;
                }

                // 如果只有一个项,返回单个值(用于预览)
                if (param.ReadItems.Count == 1)
                {
                    var item = param.ReadItems[0];

                    if (string.IsNullOrWhiteSpace(item.CellAddress))
                    {
                        NlogHelper.Default.Warn("单元格地址为空");
                        return null;
                    }

                    var value = ReportService.InvokeOnReportControl(report =>
                    {
                        return report.Read(item.CellAddress);
                    });

                    NlogHelper.Default.Info($"成功读取单元格 {item.CellAddress}: {value}");
                    return value;
                }

                //  多个项的情况,返回字典(单元格地址 -> 值)
                var results = new Dictionary<string, object>();

                foreach (var item in param.ReadItems)
                {
                    if (string.IsNullOrWhiteSpace(item.CellAddress))
                    {
                        NlogHelper.Default.Warn("跳过空单元格地址");
                        continue;
                    }

                    try
                    {
                        var value = ReportService.InvokeOnReportControl(report =>
                        {
                            return report.Read(item.CellAddress);
                        });

                        results[item.CellAddress] = value;
                        NlogHelper.Default.Info($"成功读取单元格 {item.CellAddress}: {value}");
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"读取单元格 {item.CellAddress} 失败: {ex.Message}", ex);
                        results[item.CellAddress] = null;
                    }
                }

                return results;

            }, null);
        }

        /// <summary>
        /// 写入单元格方法
        /// 使用 ReportExpressionHelper 计算表达式
        /// </summary>
        public async Task<DetailedResult> WriteCells(Parameter_WriteCells param)
        {
            return await ExecuteWithDetailedResult(param, async () =>
            {
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

                        // 使用 ReportService 在UI线程写入单元格
                        ReportService.InvokeOnReportControl(report =>
                        {
                            report.Write(item.CellAddress, formattedValue);
                        });

                        NlogHelper.Default.Info($"成功写入单元格 {item.CellAddress}: {formattedValue} (类型:{item.SourceType})");
                    }
                    catch (Exception ex)
                    {
                        NlogHelper.Default.Error($"写入单元格 {item.CellAddress} 失败: {ex.Message}", ex);
                        throw new InvalidOperationException($"写入单元格 {item.CellAddress} 失败: {ex.Message}", ex);
                    }
                }
            });
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
                NlogHelper.Default.Error($"获取数据源值失败 (类型:{item.SourceType}): {ex.Message}", ex);
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
            if (string.IsNullOrWhiteSpace(item.VariableName))
            {
                NlogHelper.Default.Warn("变量名为空");
                return string.Empty;
            }

            try
            {
                // 清理变量名：移除可能的花括号
                var cleanVarName = item.VariableName.Trim();
                if (cleanVarName.StartsWith('{') && cleanVarName.EndsWith('}'))
                {
                    cleanVarName = cleanVarName[1..^1].Trim();
                    NlogHelper.Default.Debug($"清理变量名: {item.VariableName} -> {cleanVarName}");
                }

                // 输出当前所有变量（用于调试）
                var allVariables = _globalVariableManager.GetAllVariables();
                if (allVariables != null && allVariables.Count > 0)
                {
                    var varNames = string.Join(", ", allVariables.Select(v => $"'{v.VarName}'"));
                    NlogHelper.Default.Debug($"变量列表: {varNames}");
                }

                var variable = _globalVariableManager.FindVariableByName(cleanVarName);
                if (variable == null)
                {
                    NlogHelper.Default.Warn($"变量未找到: {cleanVarName}");
                    NlogHelper.Default.Warn($"原始变量名: {item.VariableName}");

                    // 尝试模糊匹配（容错处理）
                    var similarVar = allVariables?
                        .FirstOrDefault(v => v.VarName.Equals(cleanVarName, StringComparison.OrdinalIgnoreCase));

                    if (similarVar != null)
                    {
                        NlogHelper.Default.Info($"找到大小写不匹配的变量: {similarVar.VarName}，使用该变量");
                        return similarVar.VarValue ?? string.Empty;
                    }

                    return $"[变量未找到:{cleanVarName}]";
                }

                var value = variable.VarValue;

                // 如果值为 null，返回空字符串
                return value ?? string.Empty;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取变量值失败: {item.VariableName}", ex);
                return $"[错误:{ex.Message}]";
            }
        }

        /// <summary>
        /// 计算表达式（支持 PLC 引用）
        /// </summary>
        private async Task<object> EvaluateExpression(WriteCellItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Expression))
            {
                NlogHelper.Default.Warn("表达式为空");
                return string.Empty;
            }

            try
            {
                // 使用异步方法处理表达式（支持 PLC 引用）
                var result = await _expressionHelper.Value.EvaluateForReportAsync(item.Expression);

                NlogHelper.Default.Debug($"表达式计算成功: {item.Expression} = {result}");
                return result;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"计算表达式失败: {item.Expression}", ex);
                return $"[表达式错误:{ex.Message}]";
            }
        }

        /// <summary>
        /// 获取系统属性值
        /// </summary>
        private object GetSystemPropertyValue(WriteCellItem item)
        {
            if (string.IsNullOrWhiteSpace(item.PropertyPath))
            {
                NlogHelper.Default.Warn("属性路径为空");
                return string.Empty;
            }

            try
            {
                // 分割属性路径 (例如: NewUsers.NewUserInfo.Username)
                var parts = item.PropertyPath.Split('.');

                if (parts.Length < 2)
                {
                    NlogHelper.Default.Warn($"属性路径格式无效: {item.PropertyPath}");
                    return $"[路径格式错误]";
                }

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
                    if (propertyName.Contains('('))
                    {
                        var result = EvaluateMethodCall(currentObject ?? currentType, propertyName);
                        NlogHelper.Default.Debug($"方法调用成功: {item.PropertyPath} = {result}");
                        return result;
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

                    currentType = currentObject?.GetType();
                }

                NlogHelper.Default.Debug($"系统属性获取成功: {item.PropertyPath} = {currentObject}");
                return currentObject ?? string.Empty;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"获取系统属性失败: {item.PropertyPath}", ex);
                return $"[错误:{ex.Message}]";
            }
        }

        /// <summary>
        /// 执行方法调用
        /// </summary>
        private object EvaluateMethodCall(object target, string methodCall)
        {
            try
            {
                // 解析方法名和参数 (例如: ToString("yyyy-MM-dd"))
                var methodName = methodCall.Substring(0, methodCall.IndexOf('('));
                var paramsStr = methodCall.Substring(methodCall.IndexOf('(') + 1,
                    methodCall.LastIndexOf(')') - methodCall.IndexOf('(') - 1);

                // 解析参数
                var parameters = string.IsNullOrWhiteSpace(paramsStr)
                    ? Array.Empty<object>()
                    : paramsStr.Split(',').Select(p => p.Trim(' ', '"', '\'')).ToArray();

                // 获取方法
                Type type = target is Type t ? t : target.GetType();
                var method = type.GetMethod(methodName,
                    (target is Type ? BindingFlags.Public | BindingFlags.Static : BindingFlags.Public | BindingFlags.Instance));

                if (method == null)
                {
                    NlogHelper.Default.Warn($"找不到方法: {type.Name}.{methodName}");
                    return $"[方法未找到:{methodName}]";
                }

                // 调用方法
                var result = method.Invoke(target is Type ? null : target, parameters);
                NlogHelper.Default.Debug($"方法调用成功: {methodCall} = {result}");
                return result;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"方法调用失败: {methodCall}", ex);
                return $"[方法错误:{ex.Message}]";
            }
        }

        /// <summary>
        /// 应用格式化
        /// </summary>
        private object ApplyFormatting(object value, string formatString)
        {
            if (value == null)
                return value;

            try
            {
                // 记录原始类型用于调试
                var originalType = value.GetType().Name;
                NlogHelper.Default.Debug($"ApplyFormatting - 原始类型: {originalType}, 值: {value}, 格式: {formatString ?? "无"}");

                // 统一处理：先判断是否为数值类型的字符串
                string valueStr = value.ToString();

                // 尝试解析为数值
                if (double.TryParse(valueStr, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out double numValue))
                {
                    // 如果没有指定格式，默认保留一位小数
                    if (string.IsNullOrWhiteSpace(formatString))
                    {
                        formatString = "F1";
                    }

                    var formatted = numValue.ToString(formatString, System.Globalization.CultureInfo.InvariantCulture);
                    NlogHelper.Default.Debug($"数值格式化: {value} ({originalType}) -> {formatted} (格式:{formatString})");
                    return formatted;
                }

                // 日期时间处理
                if (value is DateTime dt || DateTime.TryParse(valueStr, out dt))
                {
                    if (!string.IsNullOrWhiteSpace(formatString))
                    {
                        var formatted = dt.ToString(formatString);
                        NlogHelper.Default.Debug($"日期格式化: {dt} -> {formatted} (格式:{formatString})");
                        return formatted;
                    }
                }

                // 如果都不是，返回原值
                NlogHelper.Default.Debug($"未格式化，返回原值: {value} ({originalType})");
                return value;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Warn($"格式化失败 (值:{value}, 类型:{value.GetType().Name}, 格式:{formatString}): {ex.Message}");
                return value;
            }
        }

        #endregion
    }
}