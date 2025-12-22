using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using MainUI.LogicalConfiguration.Services.ServicesPLC;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 实时监控提示方法类
    /// 提供带实时数值监控的交互提示功能
    /// </summary>
    public class RealtimeMonitorPromptMethods(
        GlobalVariableManager variableManager,
        IPLCManager plcManager,
        ILogger<RealtimeMonitorPromptMethods> logger) : DSLMethodBase
    {
        private readonly GlobalVariableManager _variableManager = variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly IPLCManager _plcManager = plcManager ?? throw new ArgumentNullException(nameof(plcManager));
        private readonly ILogger<RealtimeMonitorPromptMethods> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        public override string Category => "系统工具";
        public override string Description => "显示实时监控提示对话框";

        /// <summary>
        /// 显示实时监控提示对话框
        /// </summary>
        /// <param name="param">参数配置</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>用户是否点击确定</returns>
        public async Task<bool> ShowRealtimeMonitorPrompt(
            Parameter_RealtimeMonitorPrompt param,
            CancellationToken cancellationToken = default)
        {
            return await ExecuteWithLogging(param, () =>
            {
                _logger.LogInformation("显示实时监控提示: {Title}", param.Title);

                // 检查取消
                cancellationToken.ThrowIfCancellationRequested();

                // 验证参数
                ValidateParameter(param);

                // 在UI线程上显示对话框
                DialogResult result = DialogResult.Cancel;

                if (System.Windows.Forms.Application.OpenForms.Count > 0)
                {
                    var mainForm = System.Windows.Forms.Application.OpenForms[0];
                    mainForm.Invoke(new Action(() =>
                    {
                        using var dialog = new Form_RealtimeMonitorPrompt(
                            param,
                            _variableManager,
                            _plcManager);

                        result = VarHelper.ShowDialogWithOverlayEx(mainForm, dialog);
                        //result = dialog.ShowDialog(mainForm);
                    }));
                }
                else
                {
                    using var dialog = new Form_RealtimeMonitorPrompt(
                        param,
                        _variableManager,
                        _plcManager);

                    result = dialog.ShowDialog();
                }

                bool success = result == DialogResult.OK;
                _logger.LogInformation("实时监控提示关闭，结果: {Result}", success ? "确定" : "取消");

                return Task.FromResult(success);
            }, false);
        }

        /// <summary>
        /// 验证参数配置
        /// </summary>
        private void ValidateParameter(Parameter_RealtimeMonitorPrompt param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param), "参数不能为空");
            }

            if (param.MonitorSourceType == MonitorSourceType.Variable)
            {
                if (string.IsNullOrWhiteSpace(param.MonitorVariable))
                {
                    throw new ArgumentException("监测变量名不能为空", nameof(param.MonitorVariable));
                }

                // 检查变量是否存在
                var variable = _variableManager.GetAllVariables()
                    .FirstOrDefault(v => v.VarName == param.MonitorVariable) ?? throw new InvalidOperationException($"监测变量不存在: {param.MonitorVariable}");
            }
            else // PLC
            {
                if (string.IsNullOrWhiteSpace(param.PlcModuleName))
                {
                    throw new ArgumentException("PLC模块名不能为空", nameof(param.PlcModuleName));
                }

                if (string.IsNullOrWhiteSpace(param.PlcAddress))
                {
                    throw new ArgumentException("PLC地址不能为空", nameof(param.PlcAddress));
                }
            }

            if (param.RefreshInterval < 100)
            {
                _logger.LogWarning("刷新间隔过短({Interval}ms)，建议设置为至少100ms", param.RefreshInterval);
            }
        }
    }
}