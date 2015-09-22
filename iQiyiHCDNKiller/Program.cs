using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iQiyiHCDNKiller
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            runclient();
            //Application.Run(new Form1());
            Application.Run();
        }

        static void runclient()
        {
            ConfigLoader cl = ConfigLoader.instance;
            System.Diagnostics.Process exep = new System.Diagnostics.Process();
            exep.StartInfo.FileName = Path.Combine(cl.clientpath, "QyClient.exe");
            exep.Start();
            exep.WaitForExit();
            System.Threading.Thread.Sleep(cl.waitstart);
            System.Diagnostics.Process[] procarray = System.Diagnostics.Process.GetProcessesByName("QyClient");
            if (procarray.Length == 0)
            {
                exit_kernal();
                Application.Exit();
            }
            foreach (System.Diagnostics.Process proc in procarray)
            {
                proc.EnableRaisingEvents = true;
                proc.Exited += new EventHandler(exep_Exited);
            }
        }

        //exep_Exited事件处理代码，这里外部程序退出后激活，可以执行你要的操作
        static void exep_Exited(object sender, EventArgs e)
        {
            exit_kernal();
        }

        static void exit_kernal()
        {
            ConfigLoader cl = ConfigLoader.instance;
            System.Threading.Thread.Sleep(cl.waitexit);
            System.Diagnostics.Process[] procarray = System.Diagnostics.Process.GetProcessesByName("QyKernel");
            foreach (System.Diagnostics.Process proc in procarray)
            {
                proc.Kill();
            }
            Application.Exit();
        }
    }
}
