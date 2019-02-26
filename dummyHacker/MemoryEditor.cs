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
        public Dictionary<IntPtr, int> regionBeginning;
        public List<Thread> threadList;
        public List<IntPtr> memoryMemory;
        public List<MyStruct> dataGridSource;
        public IOrderedEnumerable <KeyValuePair <IntPtr,int> >sortedRegions;

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
            regionBeginning = new Dictionary<IntPtr, int>();
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
            //int _regionSize;
            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION();
            int _regionSize;

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
                    //memoryInfo.Protect != AllocationProtectEnum.PAGE_EXECUTE_READ //not writable
                    //&& memoryInfo.Protect != AllocationProtectEnum.PAGE_READONLY //not writable
                    //&& memoryInfo.Protect != 0 //not writable
                    //&& memoryInfo.Protect != AllocationProtectEnum.PAGE_GUARDPLUSREADWRITE //not writable

                    //readonly
                    //(memoryInfo.Protect == AllocationProtectEnum.PAGE_EXECUTE_READ //not writable
                    //|| memoryInfo.Protect == AllocationProtectEnum.PAGE_READONLY) //not writable

                    //necessary
                    memoryInfo.Protect == AllocationProtectEnum.PAGE_READWRITE //
                    || memoryInfo.Protect == AllocationProtectEnum.PAGE_WRITECOMBINEPLUSREADWRITE

                    //&& memoryInfo.Protect != AllocationProtectEnum.PAGE_WRITECOPY //CopyOnWrite 
                    && (memoryInfo.Type == TypeEnum.MEM_IMAGE || memoryInfo.Type == TypeEnum.MEM_PRIVATE))
                {
                    if (regionBeginning.Count != 0 && (regionBeginning.Last().Key + regionBeginning.Last().Value == memoryInfo.BaseAddress))
                    {
                        helpMinimumAddress = (uint)memoryInfo.BaseAddress - regionBeginning.Last().Value;
                        _regionSize = regionBeginning.Last().Value + (int)memoryInfo.RegionSize;
                        regionBeginning.Remove(regionBeginning.Last().Key);
                        regionBeginning.Add(new IntPtr(helpMinimumAddress), _regionSize);
                    }
                    else
                    {
                        regionBeginning.Add(memoryInfo.BaseAddress, (int)memoryInfo.RegionSize);
                    }
                }
                helpMinimumAddress = (uint)memoryInfo.BaseAddress + memoryInfo.RegionSize;
            }
            //sortedRegions = from entry in regionBeginning orderby entry.Value descending select entry;// sort region descending by region regionsize
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



        public void SearchForValues(int typeSize, byte[] valueToFind)
        {
            int count = 0;
            memoryMemory = new List<IntPtr>();
            bool found;

            foreach (KeyValuePair<IntPtr, int> pair in regionBeginning)
            {
                byte[] memoryBuffer = new byte[pair.Value];
                if (ReadProcessMemory(targetHandle, pair.Key, memoryBuffer, (uint)pair.Value, out notNecessary))            //ToDo: das hier ist arsch langsam...
                {
                    for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    {
                        found = true;
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
                }
                else
                {
                    count++;
                }
            }
            int egal = count;
        }

        public void SearchForValuesInMultipleThreads(int typeSize, byte[] valueToFind)
        {
            threadList = new List<Thread>();
            memoryMemory = new List<IntPtr>();

            int threadCount = Environment.ProcessorCount;
            //int threadCount = 1;
            Dictionary<IntPtr, int>[]  splittedRegionBeginning = new Dictionary<IntPtr, int>[threadCount];

            for(int i=0;i< splittedRegionBeginning.Count();i++)
            {
                splittedRegionBeginning[i] = new Dictionary<IntPtr, int>();
            }

            for(int i = 0; i < regionBeginning.Count(); i++)
            {
                splittedRegionBeginning[i % threadCount].Add(regionBeginning.ElementAt(i).Key, regionBeginning.ElementAt(i).Value);
            }

            foreach (Dictionary<IntPtr, int> dict in splittedRegionBeginning)
            {
                Thread arsch = new Thread(() => TestMethod2(typeSize, valueToFind, dict));
                arsch.Start();
                threadList.Add(arsch);
            }

            foreach (Thread thread in threadList)
            {
                thread.Join();
            }
        }

        private void TestMethod(int typeSize, byte[] valueToFind, KeyValuePair<IntPtr, int> pair)
        {
            bool found;
            bool done;
            byte[] memoryBuffer = new byte[pair.Value];
            if (ReadProcessMemory(targetHandle, pair.Key, memoryBuffer, (uint)pair.Value, out notNecessary))            //ToDo: das hier ist arsch langsam...
            {
                for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                {
                    found = true;
                    for (int j = 0; j < typeSize; j++)
                    {
                        if (memoryBuffer[i + j] != valueToFind[j])
                        {
                            found = false;
                        }
                    }
                    if (found)
                    {
                        done = false;

                        while (!done)
                        {
                            Monitor.TryEnter(memoryMemory, ref done);
                            if (done)
                            {
                                memoryMemory.Add(pair.Key + i);
                                Monitor.Exit(memoryMemory);
                            }
                        }
                    }
                }
            }
        }

        private void TestMethod2(int typeSize, byte[] valueToFind, Dictionary<IntPtr, int> dict)
        {
            foreach (KeyValuePair<IntPtr, int> pair in dict)
            {
                bool found;
                bool done;
                byte[] memoryBuffer = new byte[pair.Value];
                if (ReadProcessMemory(targetHandle, pair.Key, memoryBuffer, (uint)pair.Value, out notNecessary))            //ToDo: das hier ist arsch langsam...
                {
                    for (int i = 0; i < memoryBuffer.Length - (typeSize - 1); i++)
                    {
                        found = true;
                        for (int j = 0; j < typeSize; j++)
                        {
                            if (memoryBuffer[i + j] != valueToFind[j])
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            done = false;

                            while (!done)
                            {
                                Monitor.TryEnter(memoryMemory, ref done);
                                if (done)
                                {
                                    memoryMemory.Add(pair.Key + i);
                                    Monitor.Exit(memoryMemory);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CompareLists(int typeSize, byte[] valueToFind)
        {
            byte[] compareBuffer = new byte[typeSize];
            bool found;

            for (int i = memoryMemory.Count - 1; i >= 0; i--)
            {
                if (ReadProcessMemory(targetHandle, memoryMemory[i], compareBuffer, (uint)typeSize, out notNecessary))
                {
                    for (int k = 0; k < compareBuffer.Length - (typeSize - 1); k++)
                    {
                        found = true;
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

        public void TestWrite(IntPtr address, byte[] wantedValue, int size)
        {
            WriteProcessMemory(targetHandle, address, wantedValue, size, out notNecessary);
        }

        public void Reset()
        {
            minimumAddress = IntPtr.Zero;
            dataGridSource = new List<MyStruct>();
            memoryMemory = new List<IntPtr>();
            regionBeginning = new Dictionary<IntPtr, int>();
        }


        public void ViewMemory()
        {
            ScanSystem();
            CreateEntryPoints();
            AnalyzeRegions();
        }


    }
}
