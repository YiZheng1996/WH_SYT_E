using MainUI.LogicalConfiguration.Services;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace MainUI.Procedure.DSL.LogicalConfiguration.Forms
{
    public partial class Form_LoopConfiguration : UIForm
    {
        private readonly ILogger _logger;
        private readonly IWorkflowStateService _workflowStateService;
        public Form_LoopConfiguration(IWorkflowStateService workflowStateService, ILogger<Form_LoopConfiguration> logger)
        {
            _logger = logger;
            _workflowStateService = workflowStateService;
            InitializeComponent();
            InitializeUI();
        }

        public Form_LoopConfiguration()
        {
            InitializeComponent();
            InitializeUI();
        }

        #region 事件处理方法
        private void InitializeUI()
        {
            // 初始化变量下拉框（从全局变量列表加载）
            LoadVariables();

            // 设置默认显示For循环面板
            ShowLoopConfigPanel(LoopType.For);
        }

        private void RbLoopType_CheckedChanged(object sender, EventArgs e)
        {
            UIRadioButton rb = sender as UIRadioButton;
            if (!rb.Checked) return;

            if (rb == rbForLoop)
                ShowLoopConfigPanel(LoopType.For);
            else if (rb == rbWhileLoop)
                ShowLoopConfigPanel(LoopType.While);
            else if (rb == rbForeachLoop)
                ShowLoopConfigPanel(LoopType.Foreach);
        }

        private void ShowLoopConfigPanel(LoopType loopType)
        {
            // 隐藏所有面板
            pnlForLoop.Visible = false;
            pnlWhileLoop.Visible = false;
            pnlForeachLoop.Visible = false;

            // 显示对应面板
            switch (loopType)
            {
                case LoopType.For:
                    pnlForLoop.Visible = true;
                    break;
                case LoopType.While:
                    pnlWhileLoop.Visible = true;
                    break;
                case LoopType.Foreach:
                    pnlForeachLoop.Visible = true;
                    break;
            }
        }

        private void LoadVariables()
        {
            try
            {
                var variables = _workflowStateService.GetAllVariables().ToArray();
                cmbWhileVar.Items.Clear();
                cmbWhileVar.Items.AddRange(variables);

                cmbForeachArray.Items.Clear();
                cmbForeachArray.Items.AddRange(variables);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载变量列表失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddStep_Click(object sender, EventArgs e)
        {
            // 打开步骤选择对话框
            // 这里应该调用现有的步骤配置界面
            MessageBox.Show("添加步骤功能需要集成现有的步骤配置界面", "提示");
        }

        private void BtnRemoveStep_Click(object sender, EventArgs e)
        {
            if (dgvLoopSteps.SelectedRows.Count > 0)
            {
                dgvLoopSteps.Rows.RemoveAt(dgvLoopSteps.SelectedRows[0].Index);
                UpdateStepIndices();
            }
        }

        private void BtnEditStep_Click(object sender, EventArgs e)
        {
            if (dgvLoopSteps.SelectedRows.Count > 0)
            {
                // 编辑选中的步骤
                MessageBox.Show("编辑步骤功能需要集成现有的步骤配置界面", "提示");
            }
        }

        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (dgvLoopSteps.SelectedRows.Count > 0 && dgvLoopSteps.SelectedRows[0].Index > 0)
            {
                int index = dgvLoopSteps.SelectedRows[0].Index;
                DataGridViewRow row = dgvLoopSteps.Rows[index];
                dgvLoopSteps.Rows.RemoveAt(index);
                dgvLoopSteps.Rows.Insert(index - 1, row);
                dgvLoopSteps.ClearSelection();
                dgvLoopSteps.Rows[index - 1].Selected = true;
                UpdateStepIndices();
            }
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (dgvLoopSteps.SelectedRows.Count > 0 &&
                dgvLoopSteps.SelectedRows[0].Index < dgvLoopSteps.Rows.Count - 1)
            {
                int index = dgvLoopSteps.SelectedRows[0].Index;
                DataGridViewRow row = dgvLoopSteps.Rows[index];
                dgvLoopSteps.Rows.RemoveAt(index);
                dgvLoopSteps.Rows.Insert(index + 1, row);
                dgvLoopSteps.ClearSelection();
                dgvLoopSteps.Rows[index + 1].Selected = true;
                UpdateStepIndices();
            }
        }

        private void UpdateStepIndices()
        {
            for (int i = 0; i < dgvLoopSteps.Rows.Count; i++)
            {
                dgvLoopSteps.Rows[i].Cells["StepIndex"].Value = i + 1;
            }
        }

        private void BtnTest_Click(object sender, EventArgs e)
        {
            // 测试运行循环配置
            if (ValidateConfiguration())
            {
                MessageBox.Show("配置验证通过，可以执行测试", "测试结果",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if (ValidateConfiguration())
            {
                // 生成循环配置参数并返回
                GenerateLoopConfiguration();
            }
        }

        private bool ValidateConfiguration()
        {
            try
            {
                if (rbForLoop.Checked)
                {
                    if (string.IsNullOrEmpty(txtForVar.Text))
                    {
                        MessageBox.Show("请输入循环变量名", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!int.TryParse(txtForStart.Text, out _))
                    {
                        MessageBox.Show("起始值必须是整数", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!int.TryParse(txtForEnd.Text, out _))
                    {
                        MessageBox.Show("结束值必须是整数", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (!int.TryParse(txtForStep.Text, out int step) || step <= 0)
                    {
                        MessageBox.Show("步长必须是正整数", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (rbWhileLoop.Checked)
                {
                    if (string.IsNullOrEmpty(cmbWhileVar.Text))
                    {
                        MessageBox.Show("请选择或输入变量名", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (string.IsNullOrEmpty(txtWhileValue.Text))
                    {
                        MessageBox.Show("请输入比较值", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                else if (rbForeachLoop.Checked)
                {
                    if (string.IsNullOrEmpty(cmbForeachArray.Text))
                    {
                        MessageBox.Show("请选择或输入数组变量", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    if (string.IsNullOrEmpty(txtForeachItem.Text))
                    {
                        MessageBox.Show("请输入项目变量名", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

                if (dgvLoopSteps.Rows.Count == 0)
                {
                    MessageBox.Show("循环体至少需要包含一个步骤", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"配置验证异常: {ex.Message}", "验证失败",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void GenerateLoopConfiguration()
        {
            // 根据选择的循环类型生成对应的参数对象
            // 这里应该返回相应的Parameter对象供DSL执行引擎使用
        }
        #endregion
    }

    public enum LoopType
    {
        For,
        While,
        Foreach
    }
}