using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using static DLLImports.Kernel32DLL;
using static DLLImports.Kernel32DLL.ProcessAccessFlags;
using static DLLImports.Kernel32DLL.TypeEnum;
using static DLLImports.Kernel32DLL.StateEnum;

namespace dummyHacker
{
    //public class MyStruct
    //{
    //    public MyStruct(IntPtr _ptr, byte[] _value)
    //    {
    //        Pointer = _ptr;
    //        Value = _value;
    //    }

    //    public IntPtr Pointer { get; set; }
    //    public byte[] Value { get; set; }
    //}

    public class MemoryEditor
    {

        private IntPtr notNecessary = IntPtr.Zero;
        private IntPtr targetHandle = new IntPtr();
        int _processId;
        readonly long maximum32BitAddress = 0x7fffffff;
        private IntPtr minimumAddress;
        private Dictionary<IntPtr, int> regionBeginning;
        private List<IntPtr> memoryMemory { get; set; }



        public void NewProcess(int processId)
        {
            regionBeginning = new Dictionary<IntPtr, int>();

            targetHandle = OpenProcess(QueryInformation | VirtualMemoryRead | VirtualMemoryWrite | VirtualMemoryOperation, false, processId);
            _processId = processId;
        }
        public void ScanSystem()
        {
            SystemInfo currentSystem = new SystemInfo();
            GetSystemInfo(out currentSystem);

            IntPtr minimumAddress = currentSystem.MinimumApplicationAddress;
        }
        public void CreateEntryPoints()
        {
            long helpMinimumAddress = (long)minimumAddress;
            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION();

            while (helpMinimumAddress <= maximum32BitAddress)
            {
                minimumAddress = new IntPtr(helpMinimumAddress);
                VirtualQueryEx(targetHandle, minimumAddress, out memoryInfo, (uint)Marshal.SizeOf(memoryInfo));
                if ((long)memoryInfo.RegionSize < 0) //TODO: prüfen regionsize int oder uint  |  prüfen cast auf long oder int
                {
                    break;
                }
                if (memoryInfo.Protect == AllocationProtectEnum.PAGE_READWRITE
                    && memoryInfo.State == MEM_COMMIT)
                {
                    regionBeginning.Add(memoryInfo.BaseAddress, (int)memoryInfo.RegionSize);
                }
                helpMinimumAddress = helpMinimumAddress + memoryInfo.RegionSize;
            }
        }

        public void AnalyzeRegions()
        {
            foreach (KeyValuePair<IntPtr, int> pair in regionBeginning)
            {
                byte[] memoryBuffer = new byte[pair.Value];
                if (ReadProcessMemory(targetHandle, pair.Key, memoryBuffer, (uint)pair.Value, out notNecessary))
                {
                    foreach (byte item in memoryBuffer)
                    {
                        //Console.Write(item.ToString("X2") + " ");
                    }
                }
            }            
        }
        

        public List <IntPtr> SearchForValues(int typeSize, int valueToFind)
        {
                memoryMemory = new List<IntPtr>();

            foreach (KeyValuePair<IntPtr, int> pair in regionBeginning)
            {
                byte[] memoryBuffer = new byte[pair.Value];
                if (ReadProcessMemory(targetHandle, pair.Key, memoryBuffer, (uint)pair.Value, out notNecessary))
                {
                    switch (typeSize)
                    {
                        case 1:
                            for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                            {
                                if (memoryBuffer[i] == valueToFind)
                                {
                                    memoryMemory.Add(pair.Key + i);
                                }
                            }
                            break;
                        case 2:
                            for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                            {
                                if ((memoryBuffer[i + 1] << 8 | memoryBuffer[i]) == valueToFind)
                                {
                                    memoryMemory.Add(pair.Key + i);
                                }
                            }
                            break;
                        case 4:
                            for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                            {
                                if ((memoryBuffer[i + 3] << 8 | memoryBuffer[i + 2] << 8 | memoryBuffer[i + 1] << 8 | memoryBuffer[i]) == valueToFind)
                                {
                                    memoryMemory.Add(pair.Key + i);
                                }
                            }
                            break;
                        case 8:
                            for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                            {
                                if ((long)(memoryBuffer[i + 7] << 8
                                    | memoryBuffer[i + 6] << 8
                                    | memoryBuffer[i + 5] << 8
                                    | memoryBuffer[i + 4] << 8
                                    | memoryBuffer[i + 3] << 8
                                    | memoryBuffer[i + 2] << 8
                                    | memoryBuffer[i + 1] << 8
                                    | memoryBuffer[i]) == valueToFind)
                                {
                                    memoryMemory.Add(pair.Key + i);
                                }
                            }
                            break;
                    }
                }
            }
            return memoryMemory;
        }

        public void ShowMatchingAddresses()
        {
            ScanSystem();
            CreateEntryPoints();
            int _typeSize = 4;
            int _valueToFind = 20;
            SearchForValues(_typeSize, _valueToFind);
            for (int i = 0; i < memoryMemory.Count; i++)
            {
                Console.Write(memoryMemory.ElementAt(i).ToString("X8") + " ");
            }
            Console.WriteLine(memoryMemory.Count);
        }

        public void CompareLists()
        {
            uint _typeSize = 4;
            int _valueToFind = 19;
            byte[] compareBuffer = new byte[_typeSize];

            for(int i = memoryMemory.Count-1; i >=0; i--)
            {
                if (ReadProcessMemory(targetHandle, memoryMemory[i], compareBuffer, _typeSize, out notNecessary))
                {
                    switch (_typeSize)
                    {
                        case 1:
                                if (compareBuffer[0] != _valueToFind)
                                {
                                    memoryMemory.Remove(memoryMemory[i]);
                                }
                            break;
                        case 2:

                                if ((compareBuffer[1] << 8 | compareBuffer[0]) != _valueToFind)
                                {
                                    memoryMemory.Remove(memoryMemory[i]);
                                }

                            break;
                        case 4:
                                if ((compareBuffer[3] << 8 | compareBuffer[2] << 8 | compareBuffer[1] << 8 | compareBuffer[0]) != _valueToFind)
                                {
                                    memoryMemory.Remove(memoryMemory[i]);
                                }
                            break;
                        case 8:
                                if ((long)(compareBuffer[7] << 8
                                    | compareBuffer[6] << 8
                                    | compareBuffer[5] << 8
                                    | compareBuffer[4] << 8
                                    | compareBuffer[3] << 8
                                    | compareBuffer[2] << 8
                                    | compareBuffer[1] << 8
                                    | compareBuffer[0]) != _valueToFind)
                                {
                                    memoryMemory.Remove(memoryMemory[i]);
                                }
                            break;
                    }
                }
            }
        }

        public void ShowComparisonResults()
        {
            CompareLists();
            for (int i = 0; i < memoryMemory.Count; i++)
            {
                Console.Write(memoryMemory.ElementAt(i).ToString("X8") + " ");
            }
            Console.WriteLine(memoryMemory.Count);
        }



        public void ViewMemory()
        {
            ScanSystem();
            CreateEntryPoints();
            AnalyzeRegions();
        }




    }
}
