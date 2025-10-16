namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// DataGridView 管理器 - 只负责UI操作
    /// </summary>
    public class DataGridViewManager(DataGridView grid)
    {
        private readonly DataGridView _grid = grid ?? throw new ArgumentNullException(nameof(grid));

        /// <summary>
        /// 获取选中行的索引
        /// </summary>
        public int GetSelectedRowIndex()
        {
            if (_grid.SelectedRows.Count > 0)
            {
                return _grid.SelectedRows[0].Index;
            }
            return -1;
        }

        /// <summary>
        /// 清空表格
        /// </summary>
        public void Clear()
        {
            _grid.Rows.Clear();
        }

        /// <summary>
        /// 从数据源刷新整个表格
        /// </summary>
        /// <param name="steps">步骤列表</param>
        public void RefreshFromDataSource(List<ChildModel> steps)
        {
            _grid.Rows.Clear();

            foreach (var step in steps)
            {
                _grid.Rows.Add(
                    step.StepNum,           // 步骤号
                    step.StepName,          // 步骤名称
                    GetStepTypeName(step),  // 步骤类型
                    GetStatusText(step.Status), // 状态
                    "-"                      // 执行时间
                );
            }
        }

        /// <summary>
        /// 更新指定行的状态
        /// </summary>
        public void UpdateRowStatus(int rowIndex, int status, string executionTime = "")
        {
            if (rowIndex >= 0 && rowIndex < _grid.Rows.Count)
            {
                var row = _grid.Rows[rowIndex];
                row.Cells["ColStatus"].Value = GetStatusText(status);

                if (!string.IsNullOrEmpty(executionTime))
                {
                    row.Cells["ColExecutionTime"].Value = executionTime;
                }

                // 设置行的背景色
                row.DefaultCellStyle.BackColor = GetStatusColor(status);
            }
        }

        /// <summary>
        /// 检查指定列是否有重复值
        /// </summary>
        public bool HasDuplicateValuesInColumn(string columnName)
        {
            var duplicateValues = _grid.Rows
                .Cast<DataGridViewRow>()
                .Select(row => row.Cells[columnName].Value)
                .GroupBy(value => value)
                .Where(group => group.Count() > 1)
                .Any();

            return duplicateValues;
        }

        #region 私有辅助方法

        /// <summary>
        /// 获取步骤类型名称
        /// </summary>
        private string GetStepTypeName(ChildModel step)
        {
            // 根据步骤名称返回类型
            return step.StepName switch
            {
                "延时等待" => "逻辑控制",
                "条件判断" => "逻辑控制",
                "循环开始" => "逻辑控制",
                "循环结束" => "逻辑控制",
                "变量赋值" => "数据操作",
                "数据读取" => "数据操作",
                "数据计算" => "数据操作",
                "读取PLC" => "通信操作",
                "写入PLC" => "通信操作",
                _ => "其他"
            };
        }

        /// <summary>
        /// 获取状态文本
        /// </summary>
        private string GetStatusText(int status)
        {
            return status switch
            {
                0 => "⏳ 未执行",
                1 => "▶ 执行中",
                2 => "✓ 成功",
                3 => "❌ 失败",
                _ => "未知"
            };
        }

        /// <summary>
        /// 获取状态颜色
        /// </summary>
        private Color GetStatusColor(int status)
        {
            return status switch
            {
                0 => Color.White,                      // 未执行
                1 => Color.LightYellow,                // 执行中
                2 => Color.LightGreen,                 // 成功
                3 => Color.LightCoral,                 // 失败
                _ => Color.White
            };
        }

        #endregion
    }
}