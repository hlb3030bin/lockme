using System;
using System.IO;
using System.Text;

namespace LockMe.Base.OxBase
{
    public class LogSimpleMan
    {
        public static void WriteLogFile(string strLog)
        {

            RunLog(p =>
            {
                p.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " : " + strLog);
            });
        }

        public static void WriteBegin()
        {

            RunLog(p =>
            {
                p.Append("-------------------------begin-------------------------");
            });
        }
        public static void WriteEnd()
        {

            RunLog(p =>
            {
                p.Append("-------------------------end-------------------------");
            });
        }

        private static void RunLog(Action<StringBuilder> action)
        {
            string sFileName = CreateDic();
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
                //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            StringBuilder bu = new StringBuilder();
            action.Invoke(bu);
            sw.WriteLine(bu.ToString());
            sw.Close();
            fs.Close();
        }
        private static string CreateDic()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\logsimpleman\\log";
            //检查上传的物理路径是否存在，不存在则创建
            string sFilePath = path + DateTime.Now.ToString("yyyyMM");
            string sFileName = "" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            return sFileName;
        }
    }
}