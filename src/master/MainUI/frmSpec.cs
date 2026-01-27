using AntdUI;
using MainUI.Service;

namespace MainUI
{
    public partial class FrmSpec : UIForm
    {
        public FrmSpec() => InitializeComponent();

        // 加载被试品型号
        private void frmSpec_Load(object sender, EventArgs e)
        {
            BindModels();
        }

        ModelTypeBLL pbll = new();
        /// <summary>
        /// 获取被试品类别列表
        /// </summary>
        private void BindModels()
        {
            try
            {
                ModelTypeBLL bModelType = new();
                cboType.DisplayMember = "ModelTypeName";
                cboType.ValueMember = "ID";
                cboType.DataSource = bModelType.GetModelsByTestBench(TestBenchService.CurrentTestBenchID);
                LoadData();
            }
            catch (Exception ex)
            {
                MessageHelper.MessageOK($"数据加载错误：{ex.Message}");
            }
        }

        private void LoadData()
        {
            Tables.Columns = [
                new Column("ID","型号"){ Align = ColumnAlign.Center , Visible = false },
                new Column("ModelTypeID","类型ID"){ Align = ColumnAlign.Center ,  Visible = false},
                new Column("ModelTypeName","产品类型名称"){ Align = ColumnAlign.Center , Visible = false},
                new Column("ModelName","产品型号名称"){ Align = ColumnAlign.Center },
                new Column("DrawingNo","产品图号"){ Align = ColumnAlign.Center },
                new Column("Mark","名称型号备注"){ Align = ColumnAlign.Center },
            ];

            // 管理员可以看到所有型号(包括未发布的),其他用户只能看已发布的
            bool isAdmin = NewUsers.NewUserInfo.ID == 1;
            Tables.DataSource = ModelBLL.GetNewModels(
                cboType.SelectedValue.ToInt32(),
                IsRelease: isAdmin  // true=显示所有, false=只显示已发布
            );
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 管理员可以看到所有型号(包括未发布的),其他用户只能看已发布的
            bool isAdmin = NewUsers.NewUserInfo.ID == 1;

            cboModel.ValueMember = "ID";
            cboModel.DisplayMember = "ModelName";
            cboModel.DataSource = ModelBLL.GetNewModels(
                cboType.SelectedValue.ToInt32(),
                IsRelease: isAdmin  // true=显示所有, false=只显示已发布
            );
            LoadData();
        }

        private void Tables_CellClick(object sender, AntdUI.TableClickEventArgs e)
        {
            if (e.Record is NewModels model)
            {
                VarHelper.TestViewModel = model;
            }
        }

        private void Tables_CellDoubleClick(object sender, AntdUI.TableClickEventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error(ex.Message);
            }
        }

        private void btnSelectRow_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
