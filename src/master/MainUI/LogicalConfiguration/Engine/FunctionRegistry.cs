namespace MainUI.LogicalConfiguration.Engine
{
    /// <summary>
    /// 函数注册表 - 管理所有支持的表达式函数
    /// </summary>
    internal class FunctionRegistry
    {
        private readonly Dictionary<string, Func<List<object>, object>> _functions;

        public FunctionRegistry()
        {
            _functions = new Dictionary<string, Func<List<object>, object>>(StringComparer.OrdinalIgnoreCase);
            InitializeFunctions();
        }

        /// <summary>
        /// 获取函数实现
        /// </summary>
        public Func<List<object>, object> GetFunction(string name)
        {
            return _functions.TryGetValue(name, out var func) ? func : null;
        }

        /// <summary>
        /// 检查函数是否支持
        /// </summary>
        public bool IsSupported(string name)
        {
            return _functions.ContainsKey(name);
        }

        /// <summary>
        /// 获取所有支持的函数名称
        /// </summary>
        public IEnumerable<string> GetAllFunctionNames()
        {
            return _functions.Keys;
        }

        /// <summary>
        /// 初始化所有支持的函数
        /// </summary>
        private void InitializeFunctions()
        {
            // === 数学函数 ===
            RegisterMathFunctions();

            // === 字符串函数 ===
            RegisterStringFunctions();

            // === 日期时间函数 ===
            RegisterDateTimeFunctions();

            // === 条件逻辑函数 ===
            RegisterLogicFunctions();
        }

        #region 数学函数

        private void RegisterMathFunctions()
        {
            // 基础数学函数
            _functions["ABS"] = args => Math.Abs(Convert.ToDouble(args[0]));
            _functions["SQRT"] = args => Math.Sqrt(Convert.ToDouble(args[0]));
            _functions["POW"] = args => Math.Pow(Convert.ToDouble(args[0]), Convert.ToDouble(args[1]));
            _functions["ROUND"] = args => Math.Round(Convert.ToDouble(args[0]), args.Count > 1 ? Convert.ToInt32(args[1]) : 0);
            _functions["FLOOR"] = args => Math.Floor(Convert.ToDouble(args[0]));
            _functions["CEILING"] = args => Math.Ceiling(Convert.ToDouble(args[0]));

            // 三角函数
            _functions["SIN"] = args => Math.Sin(Convert.ToDouble(args[0]));
            _functions["COS"] = args => Math.Cos(Convert.ToDouble(args[0]));
            _functions["TAN"] = args => Math.Tan(Convert.ToDouble(args[0]));

            // 统计函数
            _functions["MAX"] = args => args.Max(x => Convert.ToDouble(x));
            _functions["MIN"] = args => args.Min(x => Convert.ToDouble(x));
            _functions["AVG"] = args => args.Average(x => Convert.ToDouble(x));
            _functions["SUM"] = args => args.Sum(x => Convert.ToDouble(x));
        }

        #endregion

        #region 字符串函数

        private void RegisterStringFunctions()
        {
            _functions["LEN"] = args => args[0]?.ToString()?.Length ?? 0;
            _functions["UPPER"] = args => args[0]?.ToString()?.ToUpper() ?? string.Empty;
            _functions["LOWER"] = args => args[0]?.ToString()?.ToLower() ?? string.Empty;
            _functions["TRIM"] = args => args[0]?.ToString()?.Trim() ?? string.Empty;

            _functions["SUBSTRING"] = args =>
            {
                var str = args[0]?.ToString() ?? string.Empty;
                var start = Convert.ToInt32(args[1]);
                var length = args.Count > 2 ? Convert.ToInt32(args[2]) : str.Length - start;
                return str.Substring(start, length);
            };

            _functions["CONCAT"] = args => string.Join("", args.Select(a => a?.ToString() ?? ""));

            _functions["REPLACE"] = args =>
            {
                var str = args[0]?.ToString() ?? string.Empty;
                var oldValue = args[1]?.ToString() ?? string.Empty;
                var newValue = args[2]?.ToString() ?? string.Empty;
                return str.Replace(oldValue, newValue);
            };

            _functions["CONTAINS"] = args =>
            {
                var str = args[0]?.ToString() ?? string.Empty;
                var value = args[1]?.ToString() ?? string.Empty;
                return str.Contains(value);
            };
        }

        #endregion

        #region 日期时间函数

        private void RegisterDateTimeFunctions()
        {
            // 当前时间函数
            _functions["NOW"] = args => DateTime.Now;
            _functions["TODAY"] = args => DateTime.Today;
            _functions["UTCNOW"] = args => DateTime.UtcNow;

            // 时间组件提取
            _functions["YEAR"] = args => ConvertToDateTime(args[0]).Year;
            _functions["MONTH"] = args => ConvertToDateTime(args[0]).Month;
            _functions["DAY"] = args => ConvertToDateTime(args[0]).Day;
            _functions["HOUR"] = args => ConvertToDateTime(args[0]).Hour;
            _functions["MINUTE"] = args => ConvertToDateTime(args[0]).Minute;
            _functions["SECOND"] = args => ConvertToDateTime(args[0]).Second;
            _functions["DAYOFWEEK"] = args => (int)ConvertToDateTime(args[0]).DayOfWeek;
            _functions["DAYOFYEAR"] = args => ConvertToDateTime(args[0]).DayOfYear;

            // 时间加减
            _functions["ADDDAYS"] = args => ConvertToDateTime(args[0]).AddDays(Convert.ToDouble(args[1]));
            _functions["ADDHOURS"] = args => ConvertToDateTime(args[0]).AddHours(Convert.ToDouble(args[1]));
            _functions["ADDMINUTES"] = args => ConvertToDateTime(args[0]).AddMinutes(Convert.ToDouble(args[1]));
            _functions["ADDSECONDS"] = args => ConvertToDateTime(args[0]).AddSeconds(Convert.ToDouble(args[1]));

            // 时间差计算 - 使用工厂方法生成
            RegisterDateDiffFunctions();

            // 经过时间计算
            RegisterElapsedTimeFunctions();

            // 时间格式化
            _functions["FORMATDATE"] = args =>
            {
                var dt = ConvertToDateTime(args[0]);
                var format = args.Count > 1 ? args[1]?.ToString() : "yyyy-MM-dd HH:mm:ss";
                return dt.ToString(format);
            };
        }

        /// <summary>
        /// 注册时间差计算函数（避免重复代码）
        /// </summary>
        private void RegisterDateDiffFunctions()
        {
            // 定义时间差计算的配置
            var dateDiffConfigs = new[]
            {
                new { Names = new[] { "DATEDIFF_SECONDS", "DateDiff.Seconds" }, Unit = "TotalSeconds" },
                new { Names = new[] { "DATEDIFF_MILLISECONDS", "DateDiff.Milliseconds" }, Unit = "TotalMilliseconds" },
                new { Names = new[] { "DATEDIFF_MINUTES", "DateDiff.Minutes" }, Unit = "TotalMinutes" },
                new { Names = new[] { "DATEDIFF_HOURS", "DateDiff.Hours" }, Unit = "TotalHours" },
                new { Names = new[] { "DATEDIFF_DAYS", "DateDiff.Days" }, Unit = "TotalDays" }
            };

            foreach (var config in dateDiffConfigs)
            {
                Func<List<object>, object> func = args =>
                {
                    try
                    {
                        var endTime = ConvertToDateTime(args[0]);
                        var startTime = ConvertToDateTime(args[1]);
                        var diff = endTime - startTime;

                        // 使用反射获取对应的属性值
                        return (double)typeof(TimeSpan).GetProperty(config.Unit).GetValue(diff);
                    }
                    catch
                    {
                        return 0;
                    }
                };

                // 注册所有别名
                foreach (var name in config.Names)
                {
                    _functions[name] = func;
                }
            }
        }

        /// <summary>
        /// 注册经过时间函数（避免重复代码）
        /// </summary>
        private void RegisterElapsedTimeFunctions()
        {
            var elapsedConfigs = new[]
            {
                new { Name = "ELAPSED_SECONDS", Unit = "TotalSeconds" },
                new { Name = "ELAPSED_MILLISECONDS", Unit = "TotalMilliseconds" },
                new { Name = "ELAPSED_MINUTES", Unit = "TotalMinutes" }
            };

            foreach (var config in elapsedConfigs)
            {
                _functions[config.Name] = args =>
                {
                    try
                    {
                        var startTime = ConvertToDateTime(args[0]);
                        var elapsed = DateTime.Now - startTime;
                        return (double)typeof(TimeSpan).GetProperty(config.Unit).GetValue(elapsed);
                    }
                    catch
                    {
                        return 0;
                    }
                };
            }
        }

        /// <summary>
        /// 将对象转换为 DateTime
        /// </summary>
        private DateTime ConvertToDateTime(object value)
        {
            if (value is DateTime dt)
                return dt;

            if (value is string str && DateTime.TryParse(str, out DateTime result))
                return result;

            try
            {
                return Convert.ToDateTime(value);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region 条件逻辑函数

        private void RegisterLogicFunctions()
        {
            _functions["IF"] = args =>
            {
                var condition = Convert.ToBoolean(args[0]);
                return condition ? args[1] : args[2];
            };

            _functions["ISNULL"] = args => args[0] ?? args[1];

            _functions["ISEMPTY"] = args =>
            {
                var str = args[0]?.ToString();
                return string.IsNullOrEmpty(str);
            };
        }

        #endregion
    }
}