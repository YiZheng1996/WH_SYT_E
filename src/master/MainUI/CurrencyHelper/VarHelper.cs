using MainUI.Procedure.Mask;
using System.Security.Cryptography;
using System.Text;

namespace MainUI.CurrencyHelper
{
    public partial class VarHelper
    {
        public static IFreeSql fsql;
        public static string SoftName = "";
        public static string ModelTypeName => $"{TestViewModel.ModelTypeName}_{TestViewModel.ModelName}";
        public static NewModels TestViewModel = new();

        static VarHelper() { }

        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="salt">头</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static string SHA512Encoding(string salt, string password)
        {
            byte[] saltPasswordValue = Encoding.UTF8.GetBytes(salt + password);
            // 计算哈希值
            saltPasswordValue = MD5.HashData(saltPasswordValue);

            for (int i = 0; i < 1023; i++)//因为上面计算了一次hash,所以只需要迭代1023次
            {
                saltPasswordValue = MD5.HashData(saltPasswordValue);
            }
            return Convert.ToBase64String(saltPasswordValue);
        }

        /// <summary>
        /// 遮罩层
        /// </summary>
        /// <param name="dialog">Form</param>
        public static void ShowDialogWithOverlay(Form dialog1, Form dialog2)
        {
            LayerForm layerForm = new(dialog1, dialog2);
            layerForm.ShowDialog();
        }

        /// <summary>
        /// 遮罩层显示对话框并返回结果
        /// </summary>
        /// <param name="parentForm">父窗体（背景遮罩的目标）</param>
        /// <param name="dialogForm">要显示的对话框</param>
        /// <returns>对话框的 DialogResult（OK、Cancel 等）</returns>
        public static DialogResult ShowDialogWithOverlayEx(Form parentForm, Form dialogForm)
        {
            LayerForm layerForm = new(parentForm, dialogForm);
            layerForm.ShowDialog();

            // 返回实际对话框的 DialogResult
            return dialogForm.DialogResult;
        }

        /// <summary>
        /// 量程转换
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inputL">4</param>
        /// <param name="inputH">20</param>
        /// <param name="outL">0</param>
        /// <param name="outH">1000</param>
        /// <returns></returns>
        public static double AIAO_Convert(double input, double inputL, double inputH, double outL, double outH)
        {
            double rst = (outH - outL) * (input - inputL) / (inputH - inputL) + outL;
            rst = Math.Round(rst, 3);
            return rst;
        }

    }
}
