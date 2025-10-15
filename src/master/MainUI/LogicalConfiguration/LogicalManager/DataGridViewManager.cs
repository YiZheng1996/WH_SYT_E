using MainUI.LogicalConfiguration.Services;

namespace MainUI.LogicalConfiguration.LogicalManager
{

    public class DataGridViewManager(
        DataGridView grid,
        IWorkflowStateService workflowState)
    {
        private readonly DataGridView _grid = grid ?? throw new ArgumentNullException(nameof(grid));

        private readonly IWorkflowStateService _workflowState = workflowState ??
            throw new ArgumentNullException(nameof(workflowState));

        /// <summary>
        /// 获取选中下标
        /// </summary>
        /// <returns></returns>
        public int SelectedRows()
        {
            if (_grid.SelectedRows.Count > 0)
            {
                return grid.SelectedRows[0].Index;
            }
            return -1;
        }

        /// <summary>
        /// 清空表格
        /// </summary>
        public void Clears()
        {
            grid.Rows.Clear();
        }

        /// <summary>
        /// 添加行数据到DataGridView
        /// </summary>
        public void AddRow(string stepName)
        {
            var steps = _workflowState.GetSteps();
            _grid.Rows.Add(steps.Count, stepName);
        }

        /// <summary>
        /// 删除选中的行数据
        /// </summary>
        public void DeleteSelectedRow()
        {
            if (_grid.SelectedRows.Count > 0)
            {
                var selectedRow = _grid.SelectedRows[0];
                //int index = selectedRow.Index;

                //// 从工作流状态中删除
                //if (_workflowState.RemoveStepAt(index))
                //{
                // 从网格中删除
                _grid.Rows.Remove(selectedRow);

                // 重新排序
                ReorderRows();
                //}
            }
        }

        /// <summary>
        /// 重新排序行
        /// </summary>
        private void ReorderRows()
        {
            // 更新网格中的步骤号
            for (int i = 0; i < _grid.Rows.Count; i++)
            {
                if (_grid.Rows[i].Cells["ColStepNum"] != null)
                {
                    _grid.Rows[i].Cells["ColStepNum"].Value = i + 1;
                }
            }

            // 更新工作流状态中的步骤号
            var steps = _workflowState.GetSteps();
            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].StepNum = i + 1;
            }
        }

        /// <summary>
        /// ataGridView中指定列是否有重复值
        /// </summary>
        /// <param name="dataGridView">控件名</param>
        /// <param name="columnName">列名</param>
        /// <returns></returns>
        public bool HasDuplicateValuesInColumn(string columnName)
        {
            // 使用LINQ查询找到重复的项
            var duplicateValues = _grid.Rows
                .Cast<DataGridViewRow>() // 将Rows集合转换为IEnumerable<DataGridViewRow>
                .Select(row => row.Cells[columnName].Value) // 选择指定列的值
                .GroupBy(value => value) // 对值进行分组
                .Where(group => group.Count() > 1) // 找到有多个的组（即重复的值）
                .Any(); // 如果有任何重复，返回true

            return duplicateValues;
        }
    }
}
