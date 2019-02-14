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
    unsafe class Program
    {
        public struct MyStruct
        {
            public IntPtr ptr;
            public int value;

            public MyStruct(IntPtr _ptr, int _value)
            {
                ptr = _ptr;value = _value;
            }
        }

        static void Main(string[] args)
        {

            int processId = 8732;
            bool inherit = false;
            IntPtr targetHandle = new IntPtr();
            Process targetProcess = new Process();
            targetProcess = Process.GetProcessById(processId);

            IntPtr targetAddress = new IntPtr(0x00509B74);
            //int intRead = 0;
            //IntPtr buffer = new IntPtr(&intRead);
            uint size = sizeof(int);
            byte[] buffer = new byte[size];
            IntPtr arsch = IntPtr.Zero;

            SystemInfo currentSystem = new SystemInfo();
            GetSystemInfo(out currentSystem);


            long maximum32BitAddress = 0x7fffffff;
            IntPtr minimumAddress = currentSystem.MinimumApplicationAddress;
            long helpMinimumAddress = (long)minimumAddress;

            uint totalMemory = 0;
            targetHandle = OpenProcess(QueryInformation | VirtualMemoryRead | VirtualMemoryWrite, inherit, processId);

            int memsize = sizeof(MEMORY_BASIC_INFORMATION);
            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION();
            byte[] memoryBuffer;
            int j = 0;

            //spaß am rande
            List<MyStruct> pointerToAnInt = new List<MyStruct>();
            //pointerToAnInt.Add(new MyStruct(IntPtr.Zero, 42));
            //Console.WriteLine(pointerToAnInt.ElementAt(0).wert == 42 ? "geht" : "geht nich");

            uint[] bjoern = new uint[10000];
            //List<uint> bjoern = new List<uint>();
            //IntPtr[] bjoern = new IntPtr[???];

            List<IntPtr[]> geleseneSpeicherBereiche = new List<IntPtr[]>();

            while (helpMinimumAddress <= maximum32BitAddress)
            {
                minimumAddress = new IntPtr(helpMinimumAddress);

                VirtualQueryEx(targetHandle, minimumAddress, out memoryInfo, (uint)memsize);
                if ((long)memoryInfo.RegionSize < 0) //TODO: prüfen regionsize int oder uint  |  prüfen cast auf long oder int
                {
                    break;
                }
                memoryBuffer = new byte[memoryInfo.RegionSize];

                if ((memoryInfo.State == MEM_COMMIT || memoryInfo.State == MEM_RESERVE)
                    && (memoryInfo.Type == MEM_PRIVATE /*|| memoryInfo.Type == MEM_MAPPED || memoryInfo.Type == MEM_IMAGE*/)
                    && ReadProcessMemory(targetHandle, memoryInfo.BaseAddress, memoryBuffer, memoryInfo.RegionSize, out arsch))
                {
                    geleseneSpeicherBereiche.Add(new IntPtr[] { memoryInfo.BaseAddress, (memoryInfo.BaseAddress + (int)memoryInfo.RegionSize) }); //regionsize wird eh noch int siehe todo
                    

                    for (int i = 0; i < memoryBuffer.Length - 3; i++)
                    {

                        if ((memoryBuffer[i + 3] << 8 | memoryBuffer[i + 2] << 8 | memoryBuffer[i + 1] << 8 | memoryBuffer[i]) == 20)
                        {
                            j++;
                            //bjoern[j] = (uint)(memoryInfo.BaseAddress + i);
                            //bjoern.Add((uint)(memoryInfo.BaseAddress + i));
                            Console.Write((memoryInfo.BaseAddress + i).ToString("X8") + " = " + 20 + "\t");
                        }
                    }
                    totalMemory = totalMemory + memoryInfo.RegionSize;
                }

                helpMinimumAddress = helpMinimumAddress + memoryInfo.RegionSize;
                //if (helpMinimumAddress >= int.MaxValue)
                //{
                //    break;
                //}
                //minimumAddress = new IntPtr(helpMinimumAddress);

            }
         
            Console.WriteLine("accessible data in memory : " + totalMemory.ToString("#,##0") + " Bytes");
            Console.WriteLine(j+"\n**********************************************\n");







            //ReadProcessMemory(targetHandle, targetAddress, buffer, size, out arsch);
            //ReadProcessMemory(targetProcess.Handle, targetAddress, buffer, size, out arsch);

            Console.WriteLine(targetProcess.MainModule.BaseAddress.ToString("X8"));
            Console.WriteLine(targetProcess.MainModule.EntryPointAddress.ToString("X8"));
            Console.WriteLine(targetProcess.MainModule.ModuleMemorySize);
            Console.WriteLine(BitConverter.ToInt32(buffer, 0).ToString("X8"));

            //int test = BitConverter.ToInt32(buffer, 0);
            //IntPtr testptr = new IntPtr(test);
            IntPtr testptr = new IntPtr(0x00509B74);
            //ShowIntValue(targetHandle, testptr, 7);
            //PointerToIntValue(targetHandle, targetProcess.MainModule.BaseAddress);




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
            uint size = sizeof(int);
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
            uint size = sizeof(int);
            int ptrAsInt;
            int preRead = 1024;
            byte[] buffer = new byte[size];
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
                                && ReadProcessMemory(targetHandle, subBaseAddress + (4 * j), buffer, size, out notNecessary))
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
