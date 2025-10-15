namespace MainUI
{
    public partial class frmHardWare : UIForm
    {
        Dictionary<int, Procedure.UCCalibration> dicAI = [];
        Dictionary<int, Procedure.UCCalibration> dicAO = [];
        AIGrp AIgrp = OPCHelper.AIgrp;
        AOGrp AOgrp = OPCHelper.AOgrp;
        PLCCalibration Plcc = OPCHelper.plcc;
        public frmHardWare()
        {
            InitializeComponent();
            SetDic();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        private void frmHardWare_Load(object sender, EventArgs e)
        {
            AIgrp.AIvalueGrpChanged += AIgrp_AIvalueGrpChanged;
            InitZeroGain();
            timer1.Enabled = true;
        }

        private void AIgrp_AIvalueGrpChanged(object sender, int index, double value)
        {
            try
            {
                if (dicAI.TryGetValue(index, out Procedure.UCCalibration Ucvalue))
                {
                    Ucvalue.Value = Math.Round(value, 1);
                }
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare AIgrp_ValueGroupChanged;" + ex.Message;
                Debug.WriteLine(err);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var item in dicAO)
            {
                switch (item.Key)
                {
                    //case 0:
                    //    dicAO[item.Key].Value = AOgrp.CAO00;
                    //    break;
                    //case 1:
                    //    dicAO[item.Key].Value = AOgrp.CAO01;
                    //    break;
                    //case 2:
                    //    dicAO[item.Key].Value = AOgrp.CAO02;
                    //    break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// 初始化零点增益
        /// </summary>
        public void InitZeroGain()
        {
            try
            {
                InitZeroGainAI();
                InitZeroGainAO();
            }
            catch (Exception)
            {
                MessageBox.Show("硬件校准界面，初始化零点增益有误。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// AI初始化零点增益
        /// </summary>
        private void InitZeroGainAI()
        {
            foreach (var item in dicAI)
            {
                item.Value.InitData();
                item.Value.Submit();
            }
        }

        /// <summary>
        /// AO初始化零点增益
        /// </summary>
        private void InitZeroGainAO()
        {
            foreach (var item in dicAO)
            {
                item.Value.InitData();
                item.Value.Submit();
            }
        }
        /// <summary>
        /// 绑定键值对
        /// </summary>
        private void SetDic()
        {
            //AI
            dicAI.Clear();
            foreach (Control item in panel3.Controls)
            {
                if (item is Procedure.UCCalibration)
                {
                    Procedure.UCCalibration tmp = item as Procedure.UCCalibration;
                    dicAI.Add(tmp.Index, tmp);
                }
            }
            //AO 
            dicAO.Clear();
            foreach (Control item in grpAO.Controls)
            {
                if (item is Procedure.UCCalibration)
                {
                    Procedure.UCCalibration tmp = item as Procedure.UCCalibration;
                    dicAO.Add(tmp.Index, tmp);
                }
            }
        }
        //AI 通用的
        private void ucCalibration_AI_Submited(object sender, Procedure.SubmitArgs e)
        {
            try
            {
                Procedure.UCCalibration cuCur = sender as Procedure.UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;
                Plcc.SetAIZero(e.Index, e.Zero);
                Plcc.SetAIGain(e.Index, e.Gain);
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_AI_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }
        //AO 通用的
        private void ucCalibration_AO_Submited(object sender, Procedure.SubmitArgs e)
        {
            try
            {
                Procedure.UCCalibration cuCur = sender as Procedure.UCCalibration;
                cuCur.GainValue = e.Gain;
                cuCur.ZeroValue = e.Zero;
                Plcc.SetAOZero(e.Index, e.Zero);
                Plcc.SetAOGain(e.Index, e.Gain);
            }
            catch (Exception ex)
            {
                string err = "Error at frmHardWare ucCalibration_AO_Submited;" + ex.Message;
                Debug.WriteLine(err);
            }
        }




    }
}
