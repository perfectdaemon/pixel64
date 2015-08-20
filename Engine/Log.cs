using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SharpPixel.Engine
{
    public class Log
    {
        private static Log instance = new Log();
        public static Log Instance { get { return instance; } }

        private string logFileName = "log_" + DateTime.Now.ToString("ddMMyyyy") + ".log";

        private void WriteSystemInfo()
        {
            var os = Environment.OSVersion.ToString();
            var net = Environment.Version.ToString();
            Write("\t ---- OS: " + os);
            Write("\t ---- NET: " + net);            
        }

        public Log()
        {
            Write(" ---- Log started");
            WriteSystemInfo();
        }

        public void Write(string message)
        {
            try
            {
                var path = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
                using (var fs = new FileStream(path + logFileName, FileMode.Append))
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine("{0:yyyy-MM-dd HH:mm:ss}\t\t{1}", DateTime.Now, message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Не удалось что-то записать в лог: " + ex.ToString(), ex);
            }
        }        
    }
}
