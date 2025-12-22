namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 函数注册表
    /// 管理所有支持的表达式函数,消除重复的函数定义代码
    /// </summary>
    internal class FunctionRegistry
    {
        private readonly Dictionary<string, Func<List<object>, object>> _functions;

        public FunctionRegistry()
        {
            _functions = new Dictionary<string, Func<List<object>, object>>(StringComparer.OrdinalIgnoreCase);
            InitializeFunctions();
        }

        #region 公共方法

        /// <summary>
        /// 获取函数实现
        /// </summary>
        public Func<List<object>, object> GetFunction(string name)
        {
            // 尝试规范化函数名(移除常见前缀)
            var normalizedNames = new[]
            {
                name,
                name.Replace("MATH.", ""),
                name.Replace("STRING.", ""),
                name.Replace("DATETIME.", ""),
                name.Replace("ELAPSED.", ""),
                name.Replace("DATEDIFF.", "")
            };

            foreach (var normalizedName in normalizedNames)
            {
                if (_functions.TryGetValue(normalizedName, out var func))
                    return func;
            }

            return null;
        }

        /// <summary>
        /// 检查函数是否支持
        /// </summary>
        public bool IsSupported(string name)
        {
            return GetFunction(name) != null;
        }

        /// <summary>
        /// 获取所有支持的函数名称
        /// </summary>
        public IEnumerable<string> GetAllFunctionNames()
        {
            return _functions.Keys;
        }

        #endregion

        #region 函数初始化

        /// <summary>
        /// 初始化所有支持的函数
        /// </summary>
        private void InitializeFunctions()
        {
            RegisterMathFunctions();
            RegisterStringFunctions();
            RegisterDateTimeFunctions();
            RegisterLogicFunctions();
        }

        #endregion

        #region 数学函数注册

        private void RegisterMathFunctions()
        {
            // 基础数学函数 - 使用统一的转换工具
            RegisterFunction("ABS", args => Math.Abs(ExpressionUtils.ConvertToDouble(args[0])));
            RegisterFunction("SQRT", args => Math.Sqrt(ExpressionUtils.ConvertToDouble(args[0])));
            RegisterFunction("POW", args => Math.Pow(
                ExpressionUtils.ConvertToDouble(args[0]),
                ExpressionUtils.ConvertToDouble(args[1])));

            RegisterFunction("ROUND", args => Math.Round(
                ExpressionUtils.ConvertToDouble(args[0]),
                args.Count > 1 ? Convert.ToInt32(args[1]) : 0));

            RegisterFunction("FLOOR", args => Math.Floor(ExpressionUtils.ConvertToDouble(args[0])));
            RegisterFunction("CEILING", args => Math.Ceiling(ExpressionUtils.ConvertToDouble(args[0])));

            // 三角函数
            RegisterFunction("SIN", args => Math.Sin(ExpressionUtils.ConvertToDouble(args[0])));
            RegisterFunction("COS", args => Math.Cos(ExpressionUtils.ConvertToDouble(args[0])));
            RegisterFunction("TAN", args => Math.Tan(ExpressionUtils.ConvertToDouble(args[0])));

            // 统计函数
            RegisterFunction("MAX", args => args.Max(ExpressionUtils.ConvertToDouble));
            RegisterFunction("MIN", args => args.Min(ExpressionUtils.ConvertToDouble));
            RegisterFunction("AVG", args => args.Average(ExpressionUtils.ConvertToDouble));
            RegisterFunction("SUM", args => args.Sum(ExpressionUtils.ConvertToDouble));

            // 使用别名注册(同时支持MATH.前缀)
            RegisterAlias("MATH.ABS", "ABS");
            RegisterAlias("MATH.SQRT", "SQRT");
            RegisterAlias("MATH.POW", "POW");
            RegisterAlias("MATH.ROUND", "ROUND");
            RegisterAlias("MATH.FLOOR", "FLOOR");
            RegisterAlias("MATH.CEILING", "CEILING");
            RegisterAlias("MATH.SIN", "SIN");
            RegisterAlias("MATH.COS", "COS");
            RegisterAlias("MATH.TAN", "TAN");
            RegisterAlias("MATH.MAX", "MAX");
            RegisterAlias("MATH.MIN", "MIN");
        }

        #endregion

        #region 字符串函数注册

        private void RegisterStringFunctions()
        {
            RegisterFunction("LEN", args => args[0]?.ToString()?.Length ?? 0);
            RegisterFunction("UPPER", args => args[0]?.ToString()?.ToUpper() ?? "");
            RegisterFunction("LOWER", args => args[0]?.ToString()?.ToLower() ?? "");
            RegisterFunction("TRIM", args => args[0]?.ToString()?.Trim() ?? "");
            RegisterFunction("LEFT", args =>
            {
                var str = args[0]?.ToString() ?? "";
                var length = Convert.ToInt32(args[1]);
                return str.Length <= length ? str : str.Substring(0, length);
            });
            RegisterFunction("RIGHT", args =>
            {
                var str = args[0]?.ToString() ?? "";
                var length = Convert.ToInt32(args[1]);
                return str.Length <= length ? str : str.Substring(str.Length - length);
            });
            RegisterFunction("SUBSTRING", args =>
            {
                var str = args[0]?.ToString() ?? "";
                var start = Convert.ToInt32(args[1]);
                var length = args.Count > 2 ? Convert.ToInt32(args[2]) : str.Length - start;
                return str.Substring(start, Math.Min(length, str.Length - start));
            });
            RegisterFunction("REPLACE", args =>
            {
                var str = args[0]?.ToString() ?? "";
                var oldValue = args[1]?.ToString() ?? "";
                var newValue = args[2]?.ToString() ?? "";
                return str.Replace(oldValue, newValue);
            });
            RegisterFunction("CONCAT", args => string.Concat(args.Select(a => a?.ToString() ?? "")));
            RegisterFunction("JOIN", args =>
            {
                var separator = args[0]?.ToString() ?? "";
                var items = args.Skip(1).Select(a => a?.ToString() ?? "");
                return string.Join(separator, items);
            });

            // 使用别名注册
            RegisterAlias("STRING.LEN", "LEN");
            RegisterAlias("STRING.UPPER", "UPPER");
            RegisterAlias("STRING.LOWER", "LOWER");
            RegisterAlias("STRING.TRIM", "TRIM");
            RegisterAlias("STRING.LEFT", "LEFT");
            RegisterAlias("STRING.RIGHT", "RIGHT");
            RegisterAlias("STRING.SUBSTRING", "SUBSTRING");
            RegisterAlias("STRING.REPLACE", "REPLACE");
            RegisterAlias("STRING.CONCAT", "CONCAT");
            RegisterAlias("STRING.JOIN", "JOIN");
        }

        #endregion

        #region 日期时间函数注册

        private void RegisterDateTimeFunctions()
        {
            // NOW函数
            RegisterFunction("NOW", args => DateTime.Now);
            RegisterFunction("DATETIME.NOW", args => DateTime.Now);

            // FORMAT函数
            RegisterFunction("FORMAT", args =>
            {
                var dt = ExpressionUtils.ConvertToDateTime(args[0]);
                var format = args.Count > 1 ? args[1]?.ToString() : "yyyy-MM-dd HH:mm:ss";
                return dt.ToString(format);
            });
            RegisterAlias("DATETIME.FORMAT", "FORMAT");

            // 日期部分提取
            RegisterFunction("YEAR", args => ExpressionUtils.ConvertToDateTime(args[0]).Year);
            RegisterFunction("MONTH", args => ExpressionUtils.ConvertToDateTime(args[0]).Month);
            RegisterFunction("DAY", args => ExpressionUtils.ConvertToDateTime(args[0]).Day);
            RegisterFunction("HOUR", args => ExpressionUtils.ConvertToDateTime(args[0]).Hour);
            RegisterFunction("MINUTE", args => ExpressionUtils.ConvertToDateTime(args[0]).Minute);
            RegisterFunction("SECOND", args => ExpressionUtils.ConvertToDateTime(args[0]).Second);

            // 使用配置化方式注册时间差函数
            RegisterDateDiffFunctions();

            // 使用配置化方式注册经过时间函数
            RegisterElapsedTimeFunctions();
        }

        /// <summary>
        /// 注册时间差计算函数 - 配置化注册,避免重复代码
        /// </summary>
        private void RegisterDateDiffFunctions()
        {
            var configs = new[]
            {
                ("SECONDS", "TotalSeconds"),
                ("MILLISECONDS", "TotalMilliseconds"),
                ("MINUTES", "TotalMinutes"),
                ("HOURS", "TotalHours"),
                ("DAYS", "TotalDays")
            };

            foreach (var (suffix, property) in configs)
            {
                var func = CreateDateDiffFunction(property);
                RegisterFunction($"DATEDIFF.{suffix}", func);
                RegisterFunction($"DateDiff.{suffix}", func);
            }
        }

        /// <summary>
        /// 创建时间差计算函数
        /// </summary>
        private Func<List<object>, object> CreateDateDiffFunction(string propertyName)
        {
            return args =>
            {
                try
                {
                    var endTime = ExpressionUtils.ConvertToDateTime(args[0]);
                    var startTime = ExpressionUtils.ConvertToDateTime(args[1]);
                    var diff = endTime - startTime;

                    // 使用反射获取对应的属性值
                    var property = typeof(TimeSpan).GetProperty(propertyName);
                    return (double)property.GetValue(diff);
                }
                catch
                {
                    return 0.0;
                }
            };
        }

        /// <summary>
        /// 注册经过时间函数 - 配置化注册,避免重复代码
        /// </summary>
        private void RegisterElapsedTimeFunctions()
        {
            var configs = new[]
            {
                ("SECONDS", "TotalSeconds"),
                ("MILLISECONDS", "TotalMilliseconds"),
                ("MINUTES", "TotalMinutes")
            };

            foreach (var (suffix, property) in configs)
            {
                RegisterFunction($"ELAPSED.{suffix}", CreateElapsedTimeFunction(property));
            }
        }

        /// <summary>
        /// 创建经过时间函数
        /// </summary>
        private Func<List<object>, object> CreateElapsedTimeFunction(string propertyName)
        {
            return args =>
            {
                try
                {
                    var startTime = ExpressionUtils.ConvertToDateTime(args[0]);
                    var elapsed = DateTime.Now - startTime;

                    var property = typeof(TimeSpan).GetProperty(propertyName);
                    return (double)property.GetValue(elapsed);
                }
                catch
                {
                    return 0.0;
                }
            };
        }

        #endregion

        #region 逻辑函数注册

        private void RegisterLogicFunctions()
        {
            RegisterFunction("IF", args =>
            {
                var condition = ExpressionUtils.ConvertToBool(args[0]);
                return condition ? args[1] : args[2];
            });

            RegisterFunction("ISNULL", args => args[0] == null);
            RegisterFunction("ISEMPTY", args => string.IsNullOrEmpty(args[0]?.ToString()));
        }

        #endregion

        #region 辅助方法 - 简化注册流程

        /// <summary>
        /// 注册函数 - 简化注册代码
        /// </summary>
        private void RegisterFunction(string name, Func<List<object>, object> func)
        {
            _functions[name] = func;
        }

        /// <summary>
        /// 注册别名 - 多个函数名指向同一个实现
        /// </summary>
        private void RegisterAlias(string alias, string targetName)
        {
            if (_functions.TryGetValue(targetName, out var func))
            {
                _functions[alias] = func;
            }
        }

        #endregion
    }
}
