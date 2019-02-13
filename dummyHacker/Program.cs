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
            int processId = 8560;
            bool inherit = false;
            IntPtr targetHandle = new IntPtr();
            Process targetProcess = new Process();
            targetProcess = Process.GetProcessById(processId);

            IntPtr targetAddress = new IntPtr(0x00509B74);
            //int intRead = 0;
            //IntPtr buffer = new IntPtr(&intRead);
            int size = sizeof(int);
            byte[] buffer = new byte[size];
            IntPtr arsch = IntPtr.Zero;

            targetHandle = OpenProcess(Flags.PROCESS_VM_READ, inherit, processId);
            ReadProcessMemory(targetHandle, targetAddress, buffer, size, out arsch);
            //ReadProcessMemory(targetProcess.Handle, targetAddress, buffer, size, out arsch);

            Console.WriteLine(targetProcess.MainModule.BaseAddress.ToString("X8"));
            Console.WriteLine(targetProcess.MainModule.EntryPointAddress.ToString("X8"));
            Console.WriteLine(targetProcess.MainModule.ModuleMemorySize);
            Console.WriteLine(BitConverter.ToInt32(buffer, 0).ToString("X8"));

            //int test = BitConverter.ToInt32(buffer, 0);
            //IntPtr testptr = new IntPtr(test);
            IntPtr testptr = new IntPtr(0x00509B74);
            //ShowIntValue(targetHandle, testptr, 7);
            PointerToIntValue(targetHandle, targetProcess.MainModule.BaseAddress);

            //int i = 0;
            //while (ReadProcessMemory(targetHandle, testptr + (4 * i), buffer, size, out arsch) == true)
            //{
            //    Console.WriteLine((testptr + 4 * i).ToString("X8") + " = " + BitConverter.ToInt32(buffer, 0).ToString("X8") + " ||| as int :" + BitConverter.ToInt32(buffer, 0));

            //    if (BitConverter.ToInt32(buffer, 0) == 19)
            //    {
            //        Console.WriteLine((testptr + 4 * i).ToString("X8") + " Offset : + " + (i * 4).ToString("X") + " = " + BitConverter.ToInt32(buffer, 0).ToString("X8") + " ||| as int :" + BitConverter.ToInt32(buffer, 0));
            //    }
            //    i++;
            //}
            //Console.WriteLine(i);



            //IntPtr egal = targetAddress + (int)12;
            //Console.WriteLine(targetAddress + " | " + egal);
            //Console.WriteLine(targetAddress.ToString("X8") + " | " + egal.ToString("X8"));

            Console.WriteLine("\n**********************************************\n");
            //for (int i = 0; i < 10; i++)
            //{
            //    ReadProcessMemory(targetHandle, targetAddress + (4 * i), buffer, size, out arsch);
            //    Console.WriteLine(BitConverter.ToInt32(buffer, 0).ToString("X8"));
            //}

            //int test =  BitConverter.ToInt32(buffer,0);
            //int* ptr = (int*)test;
            //IntPtr zeig = new IntPtr(ptr);

            //ReadProcessMemory(targetHandle, zeig, buffer, size, out arsch);
            //Console.WriteLine(BitConverter.ToInt32(buffer, 0).ToString("X8"));

            //Console.WriteLine("{0}", *ptr);

            //IntPtr ptr2test = new IntPtr(&test);            
            //ReadProcessMemory(targetHandle, targetAddress, buffer, size, out arsch);
            //Console.WriteLine(BitConverter.ToInt32(buffer, 0));
            Console.ReadLine();
        }
        public static void ShowIntValue(IntPtr targetHandle, IntPtr targetAddress, int valueToFind)
        {
            int i = 0;
            int size = sizeof(int);
            byte[] buffer = new byte[size];
            IntPtr notNecessary = IntPtr.Zero;

            while (ReadProcessMemory(targetHandle, targetAddress + (4 * i), buffer, size, out notNecessary) == true)
            {
                if (BitConverter.ToInt32(buffer, 0) == valueToFind)
                {
                    Console.WriteLine((targetAddress + 4 * i).ToString("X8") + " Offset : + " + (i * 4).ToString("X") + " = " + BitConverter.ToInt32(buffer, 0).ToString("X8") + " ||| as int :" + BitConverter.ToInt32(buffer, 0));
                }
                i++;
            }
        }
        public static bool IsCopy(IntPtr ptr, List<IntPtr[]> list)
        {
            foreach (IntPtr[] item in list)
            {
                if ((uint)ptr >= (uint)item[0] && (uint)ptr <= (uint)item[1])
                {
                    return true;
                }
            }
            return false;
        }
        public static void PointerToIntValue(IntPtr targetHandle, IntPtr BaseAdress)
        {
            int i = 0;
            int j = 0;
            int total = 0;
            int size = sizeof(int);
            int ptrAsInt;
            int preRead = 1024;
            byte[] buffer = new byte[size];
            byte[] buffer2 = new byte[size * preRead];
            IntPtr notNecessary = IntPtr.Zero;
            IntPtr subBaseAddress = new IntPtr();
            IntPtr endOfSubBaseAddress = new IntPtr();
            //IntPtr[][] habIchSchonGelesen = new IntPtr[][];
            List<IntPtr[]> habIchSchonGelesen = new List<IntPtr[]>();


            while (ReadProcessMemory(targetHandle, BaseAdress + (4 * i), buffer, size, out notNecessary))
            {
                subBaseAddress = (IntPtr)(ptrAsInt = BitConverter.ToInt32(buffer, 0));

                if (!IsCopy(subBaseAddress, habIchSchonGelesen))
                {
                    while (subBaseAddress != IntPtr.Zero
                                 && !IsCopy(subBaseAddress + (4 * j * preRead), habIchSchonGelesen)
                                && ReadProcessMemory(targetHandle, subBaseAddress + (4 * j * preRead), buffer2, size * preRead, out notNecessary))
                    {
                        j++;
                    }

                    endOfSubBaseAddress = subBaseAddress + (4 * j * preRead);
                    habIchSchonGelesen.Add(new IntPtr[] { subBaseAddress, endOfSubBaseAddress });
                }

                if (j > 0)
                {
                    total = total + j;
                    Console.WriteLine(i.ToString("#,##0") + " || " + total.ToString("#,##0"));

                    j = 0;
                }
                i++;
            }
            Console.WriteLine(i);
        }
    }
}
