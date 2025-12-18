namespace MainUI.LogicalConfiguration.LogicalManager
{
    /// <summary>
    /// DataGridView 管理器 - 只负责UI操作
    /// </summary>
    /// <remarks>
    /// 构造函数
    /// </remarks>
    public class DataGridViewManager(DataGridView grid, ILogger logger = null)
    {
        private readonly DataGridView _grid = grid ?? throw new ArgumentNullException(nameof(grid));
        private readonly StepDetailsProvider _detailsProvider = new((Microsoft.Extensions.Logging.ILogger)logger);

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
        /// 获取所有选中行的索引数组(按索引从小到大排序)
        /// </summary>
        /// <returns>选中行的索引数组,如果没有选中则返回空数组</returns>
        public int[] GetSelectedRowIndices()
        {
            if (_grid.SelectedRows.Count == 0)
                return [];

            return [.. _grid.SelectedRows
                .Cast<DataGridViewRow>()
                .Select(row => row.Index)
                .OrderBy(i => i)];
        }

        /// <summary>
        /// 获取选中行数量
        /// </summary>
        /// <returns>选中行的数量</returns>
        public int GetSelectedRowCount()
        {
            return _grid.SelectedRows.Count;
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
            // 保存当前选中行索引
            int selectedIndex = GetSelectedRowIndex();

            // 保存滚动位置
            int firstDisplayedRowIndex = -1;
            if (_grid.FirstDisplayedScrollingRowIndex >= 0)
            {
                firstDisplayedRowIndex = _grid.FirstDisplayedScrollingRowIndex;
            }

            // 清空并重新填充
            _grid.Rows.Clear();

            foreach (var step in steps)
            {
                _grid.Rows.Add(
                    step.StepNum,                                  // 步骤号
                    step.StepName,                                 // 步骤名称
                    GetStepTypeName(step),                         // 步骤类型
                    _detailsProvider.GetStepDetailsPreview(step),  // 步骤详情
                    step.Remark ?? string.Empty,                   // 备注
                    step.Status                                    // 状态
                );
            }

            // 恢复选中状态
            if (selectedIndex >= 0 && selectedIndex < _grid.Rows.Count)
            {
                _grid.ClearSelection();
                _grid.Rows[selectedIndex].Selected = true;

                // 设置当前单元格，确保键盘导航正常
                if (_grid.Rows[selectedIndex].Cells.Count > 0)
                {
                    _grid.CurrentCell = _grid.Rows[selectedIndex].Cells[0];
                }
            }

            // 恢复滚动位置
            if (firstDisplayedRowIndex >= 0 && firstDisplayedRowIndex < _grid.Rows.Count)
            {
                try
                {
                    _grid.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
                }
                catch
                {
                    // 某些情况下设置滚动位置可能失败，忽略即可
                }
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
            return step.StepName switch
            {
                "延时等待" => "逻辑控制",
                "条件判断" => "逻辑控制",
                "循环工具" => "逻辑控制",
                "等待稳定" => "逻辑控制",
                "变量赋值" => "数据操作",
                "数据读取" => "数据操作",
                "消息通知" => "数据操作",
                "数据计算" => "数据操作",
                "读取PLC" => "通信操作",
                "写入PLC" => "通信操作",
                "读取单元格" => "报表工具",
                "写入单元格" => "报表工具",
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
                0 => Color.White,
                1 => Color.LightYellow,
                2 => Color.LightGreen,
                3 => Color.LightCoral,
                _ => Color.White
            };
        }

        #endregion
    }
}