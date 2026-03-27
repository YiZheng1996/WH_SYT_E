using MainUI.CurrencyHelper;
using MainUI.LogicalConfiguration.Forms;
using MainUI.LogicalConfiguration.Helpers;
using MainUI.LogicalConfiguration.Infrastructure;
using MainUI.LogicalConfiguration.LogicalManager;
using MainUI.LogicalConfiguration.Methods.Core;
using MainUI.LogicalConfiguration.Parameter;
using Microsoft.Extensions.Logging;

namespace MainUI.LogicalConfiguration.Methods
{
    /// <summary>
    /// 用户输入步骤执行方法
    /// 运行时在 UI 线程弹出 FrmRuntimeUserInput，
    /// 等待操作员填值后将结果写入目标变量，再继续执行流程
    /// </summary>
    public class UserInputMethods(
        GlobalVariableManager variableManager,
        ILogger<UserInputMethods> logger) : DSLMethodBase
    {
        private readonly GlobalVariableManager _variableManager =
            variableManager ?? throw new ArgumentNullException(nameof(variableManager));
        private readonly ILogger<UserInputMethods> _logger =
            logger ?? throw new ArgumentNullException(nameof(logger));

        public override string Category    => "系统工具";
        public override string Description => "运行时弹窗让操作员填值，结果写入目标变量";

        /// <summary>
        /// 执行用户输入步骤
        /// </summary>
        /// <param name="param">配置参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>
        ///   DetailedResult.Successful()  — 用户填值成功<br/>
        ///   DetailedResult.Failed(msg)   — 用户取消 / 超时停止流程 / 变量不存在
        /// </returns>
        public Task<DetailedResult> ExecuteAsync(
            Parameter_UserInput param,
            CancellationToken cancellationToken = default)
        {
            return ExecuteWithDetailedResult(param, async () =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                // ── 1. 校验目标变量是否存在 ──────────────────
                if (string.IsNullOrWhiteSpace(param.TargetVariableName))
                    throw new ArgumentException("目标变量名不能为空");

                var targetVar = _variableManager.TryFindVariableByName(param.TargetVariableName);
                if (targetVar == null)
                    throw new InvalidOperationException(
                        $"目标变量 '{param.TargetVariableName}' 不存在，请先在「变量定义」步骤中创建");

                _logger.LogInformation(
                    "用户输入步骤开始: Title={Title}, InputType={Type}, TargetVar={Var}",
                    param.Title, param.InputType, param.TargetVariableName);

                // ── 2. 在 UI 线程弹出运行时输入窗口 ──────────
                DialogResult dlgResult = DialogResult.Cancel;
                string inputValue = null;

                await ShowInputDialogOnUIThread(param, out dlgResult, out inputValue);

                // ── 3. 处理结果 ───────────────────────────────
                switch (dlgResult)
                {
                    case DialogResult.OK:
                        // 用户确认（含超时使用默认值）
                        WriteValueToVariable(targetVar, inputValue, param);
                        _logger.LogInformation(
                            "用户输入完成: Var={Var}, Value={Value}",
                            param.TargetVariableName, inputValue);
                        break;

                    case DialogResult.Ignore:
                        // 超时跳过 — 不写变量，当作成功跳过
                        _logger.LogInformation(
                            "用户输入步骤超时，已跳过: Var={Var}", param.TargetVariableName);
                        break;

                    case DialogResult.Cancel:
                    default:
                        // 用户取消 或 超时停止流程
                        throw new OperationCanceledException(
                            $"用户取消了输入或超时停止流程（步骤：{param.Title}）");
                }
            });
        }

        #region 私有辅助

        /// <summary>
        /// 在主 UI 线程上同步弹出输入窗口
        /// </summary>
        private static Task ShowInputDialogOnUIThread(
            Parameter_UserInput param,
            out DialogResult dlgResult,
            out string inputValue)
        {
            // 必须用同步方式拿结果，因为 out 参数无法跨 async 边界
            // 使用 TaskCompletionSource 让调用方 await
            var tcs = new TaskCompletionSource<(DialogResult, string)>();

            DialogResult capturedResult = DialogResult.Cancel;
            string       capturedValue  = null;

            // 找到当前打开的主窗口（通常是第一个）
            var mainForm = Application.OpenForms.Count > 0
                ? Application.OpenForms[0]
                : null;

            void ShowDialog()
            {
                try
                {
                    using var dlg = new FrmRuntimeUserInput(param);

                    DialogResult result;
                    if (mainForm != null)
                    {
                        // 使用遮罩层显示，和其他运行时弹窗一致
                        result = VarHelper.ShowDialogWithOverlayEx(mainForm, dlg);
                    }
                    else
                    {
                        result = dlg.ShowDialog();
                    }

                    capturedResult = result;
                    capturedValue  = result == DialogResult.OK ? dlg.InputValue : null;
                    tcs.SetResult((result, capturedValue));
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            }

            if (mainForm != null && mainForm.InvokeRequired)
                mainForm.Invoke((Action)ShowDialog);
            else
                ShowDialog();

            // 同步获取结果赋值给 out 参数
            var (r, v) = tcs.Task.GetAwaiter().GetResult();
            dlgResult  = r;
            inputValue = v;

            return Task.CompletedTask;
        }

        /// <summary>
        /// 将用户输入值写入目标变量（尝试按变量类型转换）
        /// </summary>
        private void WriteValueToVariable(
            VarItem_Enhanced targetVar,
            string value,
            Parameter_UserInput param)
        {
            try
            {
                var varType = targetVar.VarType?.ToLower() ?? "string";

                // 使用 GlobalVariableManager 的统一写入方法
                _variableManager.UpdateVariableValue(
                    param.TargetVariableName, value, varType);

                _logger.LogDebug(
                    "变量写入成功: {VarName}({VarType}) = {Value}",
                    param.TargetVariableName, varType, value);
            }
            catch (Exception ex)
            {
                // 类型转换失败时退回写字符串
                _logger.LogWarning(ex,
                    "变量类型转换失败，以字符串写入: {VarName} = {Value}",
                    param.TargetVariableName, value);

                _variableManager.UpdateVariableValue(
                    param.TargetVariableName, value, "string");
            }
        }

        #endregion
    }
}
