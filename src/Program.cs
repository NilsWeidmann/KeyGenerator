using System;
using System.Windows.Forms;

namespace KeyGenerator
{
    static class Program
    {
        //public static Schluesselzahlen s;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        /// 
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //s = new Schluesselzahlen();
            Application.Run(new KeyGenerator());
        }
    }
}
