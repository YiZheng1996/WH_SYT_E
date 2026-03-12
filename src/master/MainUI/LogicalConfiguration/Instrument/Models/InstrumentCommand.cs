using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainUI.LogicalConfiguration.Instrument.Models
{
    /// <summary>
    /// 命令模板
    /// </summary>
    public class InstrumentCommand
    {
        /// <summary>
        /// 命令唯一标识
        /// </summary>
        public string CommandId { get; set; } = Guid.NewGuid().ToString("N");

        /// <summary>
        /// 命令名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 命令显示名称
        /// </summary>
        public string DisplayName { get; set; } = "";

        /// <summary>
        /// 命令类型
        /// </summary>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// 命令描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 请求报文模板
        /// 支持变量占位符: {参数名}
        /// 支持变量引用: {$变量名}
        /// </summary>
        public string RequestTemplate { get; set; } = "";

        /// <summary>
        /// 请求数据类型
        /// </summary>
        public DataType RequestDataType { get; set; } = DataType.String;

        /// <summary>
        /// 响应解析规则列表
        /// </summary>
        public List<ResponseParseRule> ParseRules { get; set; } = new();

        /// <summary>
        /// 期望响应格式(用于验证)
        /// </summary>
        public string ExpectedResponsePattern { get; set; } = "";

        /// <summary>
        /// 成功响应标识
        /// </summary>
        public string SuccessIndicator { get; set; } = "";

        /// <summary>
        /// 失败响应标识
        /// </summary>
        public string FailureIndicator { get; set; } = "";

        /// <summary>
        /// 命令专用超时(毫秒，0表示使用默认)
        /// </summary>
        public int Timeout { get; set; } = 0;

        /// <summary>
        /// 发送后延时(毫秒)
        /// </summary>
        public int DelayAfterSend { get; set; } = 0;

        /// <summary>
        /// 是否等待响应
        /// </summary>
        public bool WaitForResponse { get; set; } = true;

        /// <summary>
        /// 排序顺序(用于界面显示)
        /// </summary>
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// 克隆命令对象(深拷贝)
        /// </summary>
        public InstrumentCommand Clone()
        {
            var clone = new InstrumentCommand
            {
                CommandId = Guid.NewGuid().ToString("N"), // 生成新的ID
                Name = this.Name,
                DisplayName = this.DisplayName,
                CommandType = this.CommandType,
                Description = this.Description,
                RequestTemplate = this.RequestTemplate,
                RequestDataType = this.RequestDataType,
                ExpectedResponsePattern = this.ExpectedResponsePattern,
                SuccessIndicator = this.SuccessIndicator,
                FailureIndicator = this.FailureIndicator,
                Timeout = this.Timeout,
                DelayAfterSend = this.DelayAfterSend,
                WaitForResponse = this.WaitForResponse,
                SortOrder = this.SortOrder
            };

            // 深拷贝解析规则列表
            if (this.ParseRules != null)
            {
                clone.ParseRules = new List<ResponseParseRule>();
                foreach (var rule in this.ParseRules)
                {
                    clone.ParseRules.Add(new ResponseParseRule
                    {
                        Name = rule.Name,
                        ParseType = rule.ParseType,
                        StartPosition = rule.StartPosition,
                        Length = rule.Length,
                        Delimiter = rule.Delimiter,
                        SegmentIndex = rule.SegmentIndex,
                        RegexPattern = rule.RegexPattern,
                        RegexGroupIndex = rule.RegexGroupIndex,
                        JsonPath = rule.JsonPath,
                        TargetDataType = rule.TargetDataType,
                        ScaleFactor = rule.ScaleFactor,
                        Offset = rule.Offset
                    });
                }
            }

            return clone;
        }
    }
}
