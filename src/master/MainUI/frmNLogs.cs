using System.ComponentModel;

namespace MainUI
{
    public partial class frmNLogs : UIForm
    {
        NlogsBLL nlogsBLL = new();

        public frmNLogs()
        {
            InitializeComponent();
            InitializeLogTypeComboBox();
            AddColumns();
            dtpStartBig.Value = DateTime.Now.ToDateTime();
        }

        private void AddColumns()
        {
            lstViewNlg.Columns.Add("日志等级", -2, HorizontalAlignment.Left);
            lstViewNlg.Columns.Add("记录时间", -2, HorizontalAlignment.Left);
            lstViewNlg.Columns.Add("用户名", -2, HorizontalAlignment.Left);
            lstViewNlg.Columns.Add("操作信息", -2, HorizontalAlignment.Left);
            lstViewNlg.Columns.Add("信息来源", -2, HorizontalAlignment.Left);
        }

        /// <summary>
        /// 初始化日志类型下拉框
        /// </summary>
        private void InitializeLogTypeComboBox()
        {
            try
            {
                // 获取枚举数据
                var logTypes = EnumExtensions.GetEnumItems<LogType>();

                // 绑定到下拉框
                cboType.DataSource = logTypes;
                cboType.DisplayMember = "DisplayName";  // 显示中文描述
                cboType.ValueMember = "Value";           // 值为枚举名称(英文)

                // 默认选中"全部"
                cboType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("初始化日志类型下拉框失败", ex);
                MessageHelper.MessageOK(this, $"初始化失败: {ex.Message}", AntdUI.TType.Error);
            }
        }

        public void LoadData()
        {
            try
            {
                // 获取选中的日志类型名称(All, Trace, Debug, Info, Warn, Error, Fatal)
                string selectedType = cboType.SelectedValue?.ToString();

                // 如果选择的是"All",传递 null 查询所有类型
                if (selectedType == "All")
                {
                    selectedType = null;
                }

                var StartTime = dtpStartBig.Value.Date + new TimeSpan(0, 0, 0);
                var StopTime = dtpStartBig.Value.Date + new TimeSpan(23, 59, 59);

                List<NlogsModel> nlogs = nlogsBLL.FindList(selectedType, StartTime, StopTime);

                lstViewNlg.Items.Clear();
                for (int i = 0; i < nlogs.Count; i++)
                {
                    ListViewItem listView = new(nlogs[i].Level);
                    listView.SubItems.Add(nlogs[i].MessTime.ToString("HH:mm:ss"));
                    listView.SubItems.Add(nlogs[i].UserName);
                    listView.SubItems.Add(nlogs[i].MessageName);
                    listView.SubItems.Add(nlogs[i].Message);
                    lstViewNlg.Items.Add(listView);
                }

                AutoResizeColumnWidth(nlogs);

                // 显示查询结果统计
                Text = $"日志查询 - 共 {nlogs.Count} 条记录";
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("日志查询失败", ex);
                MessageHelper.MessageOK(this, $"日志查询失败: {ex.Message}", AntdUI.TType.Error);
            }
        }

        private void frmNLogs_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 自动调整ListView的列宽的方法
        /// </summary>
        /// <param name="nlogs"></param>
        public void AutoResizeColumnWidth(List<NlogsModel> nlogs)
        {
            if (nlogs.Count > 0)
            {
                //先调整内容自适应
                lstViewNlg.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //再调整标题宽度自适应
                for (int i = 0; i < lstViewNlg.Columns.Count; i++)
                    lstViewNlg.Columns[i].Width = -2;
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogType
    {
        [Description("全部等级")]
        All = 0,

        [Description("跟踪")]
        Trace = 1,

        [Description("调试")]
        Debug = 2,

        [Description("信息")]
        Info = 3,

        [Description("警告")]
        Warn = 4,

        [Description("错误")]
        Error = 5,

        [Description("致命")]
        Fatal = 6
    }
}