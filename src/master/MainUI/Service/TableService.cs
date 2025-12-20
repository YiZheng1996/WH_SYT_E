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
               new ColumnCheck("Check"){ Checked = true }/*.SetAutoCheck(false)*/,
               new Column("ItemName", "项点名称"){ Align = ColumnAlign.Left, Width = "210" },
               new Column("ItemKey", "Key"){ Visible = false ,Align = ColumnAlign.Left },
            ];
            table.SetRowStyle += table_SetRowStyle;
        }

        /// <summary>
        /// 设置行样式事件处理程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Table.CellStyleInfo table_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
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

        #region 表格Check状态获取

        /// <summary>
        /// 设置Check列的AutoCheck属性
        /// </summary>
        /// <param name="enabled">是否启用自动勾选</param>
        public void SetCheckColumnAutoCheck(bool enabled)
        {
            try
            {
                if (table?.Columns == null)
                {
                    NlogHelper.Default.Warn("table 或其列集合为空，无法设置 AutoCheck");
                    return;
                }

                // 查找 Check 列
                var checkColumn = table.Columns
                    .OfType<ColumnCheck>()
                    .FirstOrDefault();

                if (checkColumn != null)
                {
                    checkColumn.SetAutoCheck(enabled);
                    
                    // 强制刷新表格显示
                    table.Invalidate();
                }
                else
                {
                    NlogHelper.Default.Warn("未找到 ColumnCheck 类型的列");
                }
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error($"设置 ColumnCheck AutoCheck 失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 刷新表格显示
        /// </summary>
        public void RefreshTable()
        {
            try
            {
                if (table.InvokeRequired)
                {
                    table.Invoke(new Action(RefreshTable));
                    return;
                }

                table.Invalidate();
                table.Update();
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("刷新表格失败", ex);
            }
        }

        #endregion

        #region 刷新表格颜色使用
        /// <summary>
        /// 刷新 table 显示（重新绑定数据源）
        /// </summary>
        public void Refreshtable()
        {
            try
            {
                if (table.InvokeRequired)
                {
                    table.Invoke(new Action(Refreshtable));
                    return;
                }

                // 重新绑定数据源以刷新显示
                table.DataSource = null;
                table.DataSource = itemPoints;

                NlogHelper.Default.Debug("table 已刷新");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("刷新 table 失败", ex);
            }
        }

        /// <summary>
        /// 重置所有项目的颜色状态为默认
        /// </summary>
        public void ResetAllItemPointColors()
        {
            try
            {
                foreach (var item in itemPoints)
                {
                    item.ColorState = 0; // 0 = 默认颜色
                }

                Refreshtable();

                NlogHelper.Default.Info("已重置所有项目颜色为默认状态");
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("重置项目颜色失败", ex);
            }
        }
        #endregion
    }
}