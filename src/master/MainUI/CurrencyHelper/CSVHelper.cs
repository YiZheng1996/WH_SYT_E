using System.Text;

namespace MainUI.CurrencyHelper
{
    public class CSVHelper
    {
        /// <summary>
        /// 创建文件。目录不存在则创建目录。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string CreateFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                //目录文件夹不存在，则创建文件夹
                string root = Path.GetDirectoryName(fileName);
                if (Directory.Exists(root) == false)
                    Directory.CreateDirectory(root);

                if (File.Exists(fileName) == false)
                    fs = File.Create(fileName);
            }
            catch (Exception ex)
            {
                NlogHelper.Default.Error("CSV帮助类创建文件失败:", ex);
            }
            finally
            {
                fs?.Dispose();
            }
            return fileName;
        }

        /// <summary>
        ///  保存数据到csv文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Save(string path, object[] data)
        {
            try
            {
                // 注册自定义编码提供程序
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                bool isNewOrEmpty = !File.Exists(path) || new FileInfo(path).Length == 0;
                using StreamWriter sw = new(path, true, Encoding.GetEncoding("GB2312"));
                if (isNewOrEmpty)
                {
                    sw.WriteLine(string.Join(',', DataGatherHelper.strings));
                }
                sw.WriteLine(string.Join(",", data));
                return true;
            }
            catch (Exception)
            {
                // 写文件时发生异常（例如文件被占用）则返回false
                return false;
            }
        }



        public static List<string> ReadAsList(string path)
        {
            // 方法来注册自定义编码提供程序
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (!File.Exists(path))
                return new List<string>();
            List<string> lstObj = new List<string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path, Encoding.GetEncoding("GB2312"));
                string str = string.Empty;
                while ((str = sr.ReadLine()) != null)
                {
                    lstObj.Add(str);
                }
            }
            catch (Exception)
            {
                return lstObj;
            }
            finally
            {
                sr?.Dispose();
            }
            return lstObj;
        }

        public static DataTable ReadAsDatatable(string path)
        {
            DataTable dt = new();
            if (!File.Exists(path))
                return null;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(path, Encoding.GetEncoding("GB2312"));
                string firstRow = sr.ReadLine();
                string[] colAry = firstRow.Split(',');
                for (int i = 0; i < colAry.Length; i++)
                {
                    dt.Columns.Add(new DataColumn(colAry[i]));
                }

                string str = string.Empty;
                while ((str = sr.ReadLine()) != null)
                {
                    DataRow row = dt.NewRow();
                    string[] contentAry = str.Split(',');
                    for (int i = 0; i < contentAry.Length; i++)
                    {
                        row[i] = contentAry[i];
                    }
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                throw new Exception(err);
            }
            finally
            {
                sr?.Dispose();
            }
            return dt;
        }
    }

    internal class DataGatherHelper
    {
        public static CancellationTokenSource DataCts = new();
        private static Task _task;
        public static List<string> strings = ["时间", "压力", "流量"];
        readonly static Random Random = new();
        public static string filePath { get; set; }

        /// <summary>
        /// 记录实时值
        /// </summary>
        /// <param name="speed">秒</param>
        public static void DataGather(int speed)
        {
            string DirectryPath = Application.StartupPath + "Autosave\\";
            string DirectryDatePath = DirectryPath + DateTime.Now.ToString("yyyy-MM-dd");
            if (!Directory.Exists(DirectryDatePath))
                Directory.CreateDirectory(DirectryDatePath);
            string fileName = DateTime.Now.ToString("HH：mm：ss") + ".csv";
            string filePath = DirectryDatePath + "\\" + fileName;
            CSVHelper.CreateFile(filePath);
            DataCts = new CancellationTokenSource();
            CancellationToken Token = DataCts.Token;
            _task = Task.Factory.StartNew(() =>
            {
                while (!Token.IsCancellationRequested)
                {
                    var aa = Random.NextDouble() * 1000;
                    var BB = Random.NextDouble() * 100;
                    //object[] data = [DateTime.Now.ToString("HH:mm:ss"), OPCHelper.AIgrp[0], OPCHelper.AIgrp[1]];
                    object[] data = [DateTime.Now.ToString("HH:mm:ss"), aa, BB];
                    var keyValue = CSVHelper.Save(filePath, data);
                    Task.Delay(speed * 1000).Wait();
                }
            }, Token);
        }
    }

}
