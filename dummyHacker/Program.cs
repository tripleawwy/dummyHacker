using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static DLLImports.Kernel32DLL;
using static DLLImports.Kernel32DLL.ProcessAccessFlags;
using static DLLImports.Kernel32DLL.TypeEnum;
using static DLLImports.Kernel32DLL.StateEnum;


namespace dummyHacker
{


    public class Program
    {
        static void Main(string[] args)
        {
            MemoryEditor firstTry = new MemoryEditor();
            firstTry.NewProcess(3452);
            firstTry.CreateEntryPoints();
            

            Console.ReadLine();
        }      
        
    }
}
