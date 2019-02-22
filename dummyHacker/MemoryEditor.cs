using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static DLLImports.Kernel32DLL;
using static DLLImports.Kernel32DLL.ProcessAccessFlags;
using static DLLImports.Kernel32DLL.StateEnum;

namespace dummyHacker
{
    public class MyStruct
    {
        public MyStruct(string _ptr, string _value)
        {
            Address = _ptr;
            Value = _value;
        }
        public MyStruct(string _ptr, string _value, string _previousValue)
        {
            Address = _ptr;
            Value = _value;
            PreviousValue = _previousValue;
        }

        public string Address { get; set; }
        public string Value { get; set; }
        public string PreviousValue { get; set; }

    }

    public class MemoryEditor
    {

        private IntPtr notNecessary = IntPtr.Zero;
        private IntPtr targetHandle = new IntPtr();
        int _processId = -1;
        readonly long maximum32BitAddress = 0x7fff0000;
        private IntPtr minimumAddress;
        public Dictionary<IntPtr, uint> regionBeginning;
        public List<IntPtr> memoryMemory;
        public List<MyStruct> dataGridSource;

        public List<MyStruct> CreateDataGridSource(string valueToFind)
        {
            List<MyStruct> dataGridSource = new List<MyStruct>();
            foreach (IntPtr address in memoryMemory)
            {
                MyStruct record = new MyStruct(address.ToString("X8"), valueToFind);
                dataGridSource.Add(record);
            }
            return dataGridSource;
        }

        public void CreateDataGridSource1(string valueToFind)
        {
            dataGridSource = new List<MyStruct>();
            foreach (IntPtr address in memoryMemory)
            {
                MyStruct record = new MyStruct(address.ToString("X8"), valueToFind);
                dataGridSource.Add(record);
            }
        }
        public void CreateDataGridSource1(string valueToFind, string previousValue)
        {
            dataGridSource = new List<MyStruct>();
            foreach (IntPtr address in memoryMemory)
            {
                MyStruct record = new MyStruct(address.ToString("X8"), valueToFind, previousValue);
                dataGridSource.Add(record);
            }
        }

        public void CompareDataGridSource(string valueToFind, string previousValue)
        {
            dataGridSource = new List<MyStruct>();
            foreach (IntPtr item in memoryMemory)
            {
                MyStruct compare = new MyStruct(item.ToString("X8"), valueToFind, previousValue);
                dataGridSource.Add(compare);
            }
        }

        public void RefreshSource(string previousValue, int size, bool isString)
        {
            string currentValue;
            dataGridSource = new List<MyStruct>();
            foreach (IntPtr item in memoryMemory)
            {
                byte[] refresh = new byte[size];
                ReadProcessMemory(targetHandle, item, refresh, (uint)size, out notNecessary);
                if (isString)
                {
                    currentValue = System.Text.Encoding.Default.GetString(refresh);
                }
                else
                {
                    currentValue = (BitConverter.ToInt32(refresh, 0)).ToString();
                }
                MyStruct compare = new MyStruct(item.ToString("X8"), currentValue, previousValue);
                dataGridSource.Add(compare);
            }
        }

        public void NewProcess(int processId)
        {
            regionBeginning = new Dictionary<IntPtr, uint>();
            targetHandle = OpenProcess(QueryInformation | VirtualMemoryRead | VirtualMemoryWrite | VirtualMemoryOperation, false, processId);
            _processId = processId;
        }

        public void ScanSystem()
        {
            SystemInfo currentSystem = new SystemInfo();
            GetSystemInfo(out currentSystem);
            minimumAddress = currentSystem.MinimumApplicationAddress;
        }




        public void CreateEntryPoints()
        {
            long helpMinimumAddress = (long)minimumAddress;
            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION();

            while (helpMinimumAddress < maximum32BitAddress)
            {
                
                minimumAddress = new IntPtr(helpMinimumAddress);
                VirtualQueryEx(targetHandle, minimumAddress, out memoryInfo, (uint)Marshal.SizeOf(memoryInfo));
                if (memoryInfo.RegionSize < 0) //TODO: prüfen regionsize int oder uint  |  prüfen cast auf long oder int
                {
                    break;
                }

                if (
                    //writable = not (not writeable)
                    memoryInfo.Protect != AllocationProtectEnum.PAGE_EXECUTE_READ //not writable
                    && memoryInfo.Protect != AllocationProtectEnum.PAGE_READONLY //not writable

                    //readonly
                    //(memoryInfo.Protect == AllocationProtectEnum.PAGE_EXECUTE_READ //not writable
                    //|| memoryInfo.Protect == AllocationProtectEnum.PAGE_READONLY) //not writable

                    //others
                     //memoryInfo.Protect == AllocationProtectEnum.PAGE_READWRITE //liefert nicht alle writables

                    && memoryInfo.Protect != AllocationProtectEnum.PAGE_WRITECOPY //CopyOnWrite 
                    && (memoryInfo.Type == TypeEnum.MEM_IMAGE || memoryInfo.Type == TypeEnum.MEM_PRIVATE))
                {
                        regionBeginning.Add(memoryInfo.BaseAddress, memoryInfo.RegionSize);
                }


                helpMinimumAddress = (uint)memoryInfo.BaseAddress + memoryInfo.RegionSize;
            }

        }




        public void AnalyzeRegions()
        {
            foreach (KeyValuePair<IntPtr, uint> pair in regionBeginning)
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



        public void SearchForValues(int typeSize, byte[] valueToFind)
        {
            //ToDo: das hier ist arsch langsam...
            memoryMemory = new List<IntPtr>();

            foreach (KeyValuePair<IntPtr, uint> pair in regionBeginning)
            {
                byte[] memoryBuffer = new byte[pair.Value];
                if (ReadProcessMemory(targetHandle, pair.Key, memoryBuffer, (uint)pair.Value, out notNecessary))
                {
                    for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    {
                        bool found = true;
                        for (int j = 0; j < typeSize; j++)
                        {
                            if (memoryBuffer[i + j] != valueToFind[j])
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            memoryMemory.Add(pair.Key + i);
                        }
                    }

                    //switch (typeSize)
                    //{
                    //    case 1:

                    //        break;
                    //    case 2:
                    //        for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    //        {
                    //            if ((memoryBuffer[i + 1] << 8 
                    //                | memoryBuffer[i]) == valueToFind)
                    //            {
                    //                memoryMemory.Add(pair.Key + i);
                    //            }
                    //        }
                    //        break;
                    //    case 4:
                    //        for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    //        {
                    //            if ((memoryBuffer[i + 3] << 8 
                    //                | memoryBuffer[i + 2] << 8 
                    //                | memoryBuffer[i + 1] << 8 
                    //                | memoryBuffer[i]) == valueToFind)
                    //            {
                    //                memoryMemory.Add(pair.Key + i);
                    //            }
                    //        }
                    //        break;
                    //    case 8:
                    //        for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    //        {
                    //            if ((long)(memoryBuffer[i + 7] << 8
                    //                | memoryBuffer[i + 6] << 8
                    //                | memoryBuffer[i + 5] << 8
                    //                | memoryBuffer[i + 4] << 8
                    //                | memoryBuffer[i + 3] << 8
                    //                | memoryBuffer[i + 2] << 8
                    //                | memoryBuffer[i + 1] << 8
                    //                | memoryBuffer[i]) == valueToFind)
                    //            {
                    //                memoryMemory.Add(pair.Key + i);
                    //            }
                    //        }
                    //        break;
                    //    default:
                    //        for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    //        {
                    //            if ((memoryBuffer[i + 7] << 8
                    //                | memoryBuffer[i + 6] << 8
                    //                | memoryBuffer[i + 5] << 8
                    //                | memoryBuffer[i + 4] << 8
                    //                | memoryBuffer[i + 3] << 8
                    //                | memoryBuffer[i + 2] << 8
                    //                | memoryBuffer[i + 1] << 8
                    //                | memoryBuffer[i]) == valueToFind)
                    //            {
                    //                memoryMemory.Add(pair.Key + i);
                    //            }
                    //        }
                    //        break;

                    //}
                }
            }
        }

        public void CompareLists(int typeSize, byte[] valueToFind)
        {
            byte[] compareBuffer = new byte[typeSize];

            for (int i = memoryMemory.Count - 1; i >= 0; i--)
            {
                if (ReadProcessMemory(targetHandle, memoryMemory[i], compareBuffer, (uint)typeSize, out notNecessary))
                {
                    for (int k = 0; k < compareBuffer.Length - (typeSize - 1); k++)
                    {
                        bool found = true;
                        for (int j = 0; j < typeSize; j++)
                        {
                            if (compareBuffer[k + j] != valueToFind[j])
                            {
                                found = false;
                            }
                        }
                        if (!found)
                        {
                            memoryMemory.Remove(memoryMemory[i]);
                        }
                    }
                    //switch (_typeSize)
                    //{
                    //    case 1:
                    //        if (compareBuffer[0] != _valueToFind)
                    //        {
                    //            memoryMemory.Remove(memoryMemory[i]);
                    //        }
                    //        break;
                    //    case 2:

                    //        if ((compareBuffer[1] << 8 | compareBuffer[0]) != _valueToFind)
                    //        {
                    //            memoryMemory.Remove(memoryMemory[i]);
                    //        }

                    //        break;
                    //    case 4:
                    //        if ((compareBuffer[3] << 8 | compareBuffer[2] << 8 | compareBuffer[1] << 8 | compareBuffer[0]) != _valueToFind)
                    //        {
                    //            memoryMemory.Remove(memoryMemory[i]);
                    //        }
                    //        break;
                    //    case 8:
                    //        if ((long)(compareBuffer[7] << 8
                    //            | compareBuffer[6] << 8
                    //            | compareBuffer[5] << 8
                    //            | compareBuffer[4] << 8
                    //            | compareBuffer[3] << 8
                    //            | compareBuffer[2] << 8
                    //            | compareBuffer[1] << 8
                    //            | compareBuffer[0]) != _valueToFind)
                    //        {
                    //            memoryMemory.Remove(memoryMemory[i]);
                    //        }
                    //        break;
                    //}
                }
            }
        }

        public void TestWrite(IntPtr address, byte[] wantedValue)
        {
            WriteProcessMemory(targetHandle, address, wantedValue, 4, out notNecessary);
        }

        public void Reset()
        {
            minimumAddress = IntPtr.Zero;
            dataGridSource = new List<MyStruct>();
            memoryMemory = new List<IntPtr>();
            regionBeginning = new Dictionary<IntPtr, uint>();
        }


        public void ViewMemory()
        {
            ScanSystem();
            CreateEntryPoints();
            AnalyzeRegions();
        }


    }
}
