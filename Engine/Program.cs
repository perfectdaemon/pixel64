using System;
using System.Windows.Forms;

namespace SharpPixel.Engine
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                Log.Instance.Write(" ---- Exit");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                if (!ex.Message.Contains("Не удалось что-то записать в лог"))
                    Log.Instance.Write("UNHANDLED ERROR: " + ex.ToString());
            }
        }
    }
}
