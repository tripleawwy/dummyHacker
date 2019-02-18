using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;


namespace dummyHacker
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());            
        }
    }

    //public class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        MemoryEditor firstTry = new MemoryEditor();
    //        firstTry.NewProcess(7600);
    //        firstTry.ShowMatchingAddresses();
    //        firstTry.ShowComparisonResults();
    //        Console.ReadLine();
    //    }      
        
    //}
}
