using AntdUI;
namespace MainUI.Service
{
    /// <summary>
    /// 表格服务类，用于管理和交互表格，包括初始化列、加载测试项、设置行样式和处理选中状态变化。
    /// </summary>
    /// <param name="table">表格控件</param>
    /// <param name="itemPoints">设置行样式和处理选择状态的更改</param>
    public class TableService(Table table, List<ItemPointModel> itemPoints)
    {
        /// <summary>
        /// 初始化表格列
        /// </summary>
        public void InittializeColumns()
        {
            table.Columns =
            [
               new ColumnCheck("Check"){ Checked = true },
               new Column("ItemName", "项点名称"){ Align = ColumnAlign.Left, Width = "210" },
               new Column("ItemKey", "Key"){ Visible = false ,Align = ColumnAlign.Left },
            ];
            table.SetRowStyle += TableItemPoint_SetRowStyle;
        }

        /// <summary>
        /// 设置行样式事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Table.CellStyleInfo TableItemPoint_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            return SetRowStyle(e.Record);
        }

        /// <summary>
        /// 加载测试项
        /// </summary> 
        public void LoadTestItems()
        {
            itemPoints.Clear();
            TestStepBLL stepBLL = new();
            var testSteps = stepBLL.GetTestItems(VarHelper.TestViewModel.ID).OrderBy(x => x.Step) // 按Step字段排序
                    .ToList(); ;
            itemPoints.AddRange(testSteps.Select(ts => new ItemPointModel
            {
                Check = true,
                ItemName = ts.TestProcessName
            }));
            table.DataSource = itemPoints;
        }

        /// <summary>
        /// 设置表格行样式 
        /// </summary>
        public Table.CellStyleInfo SetRowStyle(object record)
        {
            try
            {
                if (record is ItemPointModel data)
                {
                    return data.ColorState switch
                    {
                        0 => new Table.CellStyleInfo { BackColor = Color.Transparent },
                        1 => new Table.CellStyleInfo { BackColor = Color.FromArgb(255, 255, 128) },
                        2 => new Table.CellStyleInfo { BackColor = Color.FromArgb(50, 205, 50) },
                        3 => new Table.CellStyleInfo { BackColor = Color.FromArgb(231, 54, 36) },
                        _ => null
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("任务查看界面，颜色改变错误：", ex);
                return null;
            }
        }

        /// <summary>
        /// 处理选中状态变化
        /// </summary>
        public void HandleCheckedChanged(TableCheckEventArgs e)
        {
            if (e.Record is ItemPointModel item)
            {
                int index = itemPoints.FindIndex(p => p.ItemName == item.ItemName);
                if (index != -1)
                {
                    itemPoints[index].Check = item.Check;
                }
            }
        }
    }
}