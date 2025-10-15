using AntdUI;
namespace MainUI.Service
{
    /// <summary>
    /// �������࣬���ڹ���ͽ�����񣬰�����ʼ���С����ز������������ʽ�ʹ���ѡ��״̬�仯��
    /// </summary>
    /// <param name="table">���ؼ�</param>
    /// <param name="itemPoints">��������ʽ�ʹ���ѡ��״̬�ĸ���</param>
    public class TableService(Table table, List<ItemPointModel> itemPoints)
    {
        /// <summary>
        /// ��ʼ�������
        /// </summary>
        public void InittializeColumns()
        {
            table.Columns =
            [
               new ColumnCheck("Check"){ Checked = true },
               new Column("ItemName", "�������"){ Align = ColumnAlign.Left, Width = "210" },
               new Column("ItemKey", "Key"){ Visible = false ,Align = ColumnAlign.Left },
            ];
            table.SetRowStyle += TableItemPoint_SetRowStyle;
        }

        /// <summary>
        /// ��������ʽ�¼��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private Table.CellStyleInfo TableItemPoint_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            return SetRowStyle(e.Record);
        }

        /// <summary>
        /// ���ز�����
        /// </summary> 
        public void LoadTestItems()
        {
            itemPoints.Clear();
            TestStepBLL stepBLL = new();
            var testSteps = stepBLL.GetTestItems(VarHelper.TestViewModel.ID).OrderBy(x => x.Step) // ��Step�ֶ�����
                    .ToList(); ;
            itemPoints.AddRange(testSteps.Select(ts => new ItemPointModel
            {
                Check = true,
                ItemName = ts.TestProcessName
            }));
            table.DataSource = itemPoints;
        }

        /// <summary>
        /// ���ñ������ʽ 
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
                NlogHelper.Default.Error("����鿴���棬��ɫ�ı����", ex);
                return null;
            }
        }

        /// <summary>
        /// ����ѡ��״̬�仯
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