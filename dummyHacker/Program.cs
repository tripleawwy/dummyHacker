using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace dummyHacker
{
    unsafe class Program
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        public struct Flags
        {
            public static uint PROCESS_VM_READ = 0x0010;
            public static uint PROCESS_VM_WRITE = 0x0020;
        }

        static void Main(string[] args)
        {
            int processId = 9560;
            bool inherit = false;
            IntPtr targetHandle = new IntPtr();
            Process targetProcess = new Process();
            targetProcess = Process.GetProcessById(processId);

            IntPtr targetAddress = new IntPtr(0x00509B74);
            //int intRead = 0;
            //IntPtr buffer = new IntPtr(&intRead);
            int size = sizeof(int);
            byte[] buffer = new byte[size];
            IntPtr arsch =  IntPtr.Zero;

            targetHandle = OpenProcess(Flags.PROCESS_VM_READ, inherit, processId);
            ReadProcessMemory(targetHandle, targetAddress, buffer, size, out arsch);
            //ReadProcessMemory(targetProcess.Handle, targetAddress, buffer, size, out arsch);

            Console.WriteLine(targetProcess.MainModule.BaseAddress.ToString("X8"));
            Console.WriteLine(targetProcess.MainModule.EntryPointAddress.ToString("X8"));
            Console.WriteLine(targetProcess.MainModule.ModuleMemorySize);
            Console.WriteLine(BitConverter.ToInt32(buffer,0).ToString("X8"));

            //IntPtr egal = targetAddress + (int)12;
            //Console.WriteLine(targetAddress + " | " + egal);
            //Console.WriteLine(targetAddress.ToString("X8") + " | " + egal.ToString("X8"));

            Console.WriteLine("\n**********************************************\n");
            //for (int i = 0; i < 10; i++)
            //{
            //    ReadProcessMemory(targetHandle, targetAddress + (4 * i), buffer, size, out arsch);
            //    Console.WriteLine(BitConverter.ToInt32(buffer, 0).ToString("X8"));
            //}

            int test =  BitConverter.ToInt32(buffer,0);
            int* ptr = (int*)test;
            IntPtr zeig = new IntPtr(ptr);

            ReadProcessMemory(targetHandle, zeig, buffer, size, out arsch);
            Console.WriteLine(BitConverter.ToInt32(buffer, 0).ToString("X8"));

            //Console.WriteLine("{0}", *ptr);

            //IntPtr ptr2test = new IntPtr(&test);            
            //ReadProcessMemory(targetHandle, targetAddress, buffer, size, out arsch);
            //Console.WriteLine(BitConverter.ToInt32(buffer, 0));
            Console.ReadLine();
        }
    }
}
