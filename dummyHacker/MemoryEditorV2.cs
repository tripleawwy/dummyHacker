using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static DLLImports.Kernel32DLL;
using static DLLImports.Kernel32DLL.ProcessAccessFlags;

namespace dummyHacker
{
    public struct ScanStructure : IComparable<ScanStructure>
    {
        public ScanStructure(IntPtr ptr, byte[] value)
        {
            Address = ptr;
            Value = value;
        }
        public IntPtr Address { get; set; }
        public byte[] Value { get; set; }

        public int CompareTo(ScanStructure other)
        {
            return ((uint)this.Address).CompareTo((uint)other.Address);
        }
    }

    public struct RegionStructure
    {
        public RegionStructure(IntPtr regionBeginning, int regionSize)
        {
            RegionBeginning = regionBeginning;
            RegionSize = regionSize;
        }
        public IntPtr RegionBeginning { get; set; }
        public int RegionSize { get; set; }
    }


    public class MemoryEditorV2
    {
        public MemoryEditorV2(int processId)
        {
            ProcessId = processId;
            InitializeBasicInformation();
        }

        public byte[] Value { get; set; }
        public int TypeSize => Value.Length;
        public int ProcessId { get; set; }
        public bool IsString { get; set; }
        public byte[] CurrentAddress { get; set; }


        public Process CurrentProcess;



        public List<DatagridSource> Output;
        public List<string[]> PointerOutput;



        public IntPtr targetHandle;
        public IntPtr notNecessary;
        public long maximum32BitAddress;
        public IntPtr minimumAddress;



        public List<RegionStructure>[] regionLists;
        public List<List<ScanStructure>> ScanHistory;
        public List<List<PointerStructure>> PointerHistory;
        public List<MemoryStructure> MemoryDump;




        //fills MemoryEditorV2 with basic information such as the TargetHandle, MinimumApplicationAddress and asks for rights to read memoryRegions
        public void InitializeBasicInformation()
        {
            ScanSystem();

            notNecessary = IntPtr.Zero;
            regionLists = new List<RegionStructure>[Environment.ProcessorCount];
            ScanHistory = new List<List<ScanStructure>>();
            PointerHistory = new List<List<PointerStructure>>();
            MemoryDump = new List<MemoryStructure>();
            CurrentProcess = Process.GetProcessById(ProcessId);

            targetHandle = OpenProcess(QueryInformation | VirtualMemoryRead | VirtualMemoryWrite | VirtualMemoryOperation, false, ProcessId);
        }

        //scans the system for the MinimumApplicationAddress and some other useful information(no implementation for these)
        private void ScanSystem()
        {
            SystemInfo currentSystem = new SystemInfo();
            GetSystemInfo(out currentSystem);
            minimumAddress = currentSystem.MinimumApplicationAddress;
            maximum32BitAddress = 0x7fff0000;
        }

        public void Start()
        {
            CreateEntryPoints();
            SearchForValuesInMultipleThreads();
        }

        //creates a list of accessible memory regions in VRAM (readable&&writable)
        private void CreateEntryPoints()
        {
            long helpMinimumAddress = (long)minimumAddress;
            RegionStructure region;
            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION();
            List<RegionStructure> originalRegionList = new List<RegionStructure>();


            while (helpMinimumAddress < maximum32BitAddress)
            {
                minimumAddress = new IntPtr(helpMinimumAddress);

                //receives basic memory information for a handle specified by OpenProcess (more info : MEMORY_BASIC_INFORMATION struct)
                VirtualQueryEx(targetHandle, minimumAddress, out memoryInfo, (uint)Marshal.SizeOf(memoryInfo));
                if (memoryInfo.RegionSize < 0) //TODO: prüfen regionsize int oder uint  |  prüfen cast auf long oder int
                {
                    break;
                }

                //checks regions for necessary ProtectionStatus to ReadAndWrite and for the needed MemoryType 
                if (memoryInfo.Protect == AllocationProtectEnum.PAGE_READWRITE
                    || memoryInfo.Protect == AllocationProtectEnum.PAGE_WRITECOMBINEPLUSREADWRITE
                    && (memoryInfo.Type == TypeEnum.MEM_IMAGE || memoryInfo.Type == TypeEnum.MEM_PRIVATE))
                {
                    //adds regions to a List
                    region = new RegionStructure(memoryInfo.BaseAddress, (int)memoryInfo.RegionSize);
                    originalRegionList.Add(region);
                }
                helpMinimumAddress = (uint)memoryInfo.BaseAddress + memoryInfo.RegionSize;
            }
            SplitList(originalRegionList);

        }



        //splits a list into as many lists as the count of processors of the system
        private void SplitList(List<RegionStructure> list)
        {
            int threadCount = Environment.ProcessorCount;

            for (int i = 0; i < regionLists.Count(); i++)
            {
                regionLists[i] = new List<RegionStructure>();
            }

            for (int i = 0; i < list.Count(); i++)
            {
                regionLists[i % threadCount].Add(list.ElementAt(i));
            }
        }




        //creates threads and assigns them to work a list
        private void SearchForValuesInMultipleThreads()
        {
            List<Thread> threadList = new List<Thread>();
            ScanHistory.Add(new List<ScanStructure>());


            foreach (List<RegionStructure> list in regionLists)
            {
                Thread arsch = new Thread(() => ReadMemory(list));
                arsch.Start();
                threadList.Add(arsch);
            }

            foreach (Thread thread in threadList)
            {
                thread.Join();
            }

            if (ScanHistory.Last().Count <= 10_000)
            {
                ScanHistory.Last().Sort();
            }

            Output = MemoryConverter.CreateDataGrid(ScanHistory, IsString);
        }

        //reads memory for specific regions, compares them to a value and saves them into a history
        private void ReadMemory(List<RegionStructure> list)
        {
            byte[] _value = Value;
            //byte[] _allocAdr = new byte[4];
            //byte[] _valueAdr = new byte[4];
            int _typesize = TypeSize;

            foreach (RegionStructure pair in list)
            {
                bool found;
                bool done;
                byte[] memoryBuffer = new byte[pair.RegionSize];
                if (ReadProcessMemory(targetHandle, pair.RegionBeginning, memoryBuffer, (uint)pair.RegionSize, out notNecessary))            //ToDo: das hier ist arsch langsam...
                {
                    for (int i = 0; i < memoryBuffer.Length - (_typesize - 1); i++)
                    {
                        found = true;
                        for (int j = 0; j < _typesize; j++)
                        {
                            //version ptr suche
                            //if (memoryBuffer[i + j] < _allocAdr[j] || memoryBuffer[i + j] > _valueAdr[j])
                            //{
                            //    found = false;
                            //}


                            if (memoryBuffer[i + j] != _value[j])
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            done = false;
                            ScanStructure scan = new ScanStructure(pair.RegionBeginning + i, _value);

                            while (!done)
                            {
                                Monitor.TryEnter(ScanHistory.Last(), ref done); //waits until no other thread is accessing the list
                                if (done)
                                {
                                    ScanHistory.Last().Add(scan);
                                    Monitor.Exit(ScanHistory.Last());
                                }
                            }
                        }
                    }
                }
            }
        }

        //Compares Lists and removes no longer valid entries and adds the result to the history
        public void CompareLists()
        {
            //just compares the values in the previous list by reading small areas(size of the new value) in VRAM
            List<ScanStructure> ScanResult = new List<ScanStructure>();
            byte[] _value = Value;
            int _typesize = TypeSize;
            bool found;

            byte[] compareBuffer = new byte[_typesize];

            for (int i = ScanHistory.Last().Count() - 1; i >= 0; i--)
            {
                if (ReadProcessMemory(targetHandle, ScanHistory.Last().ElementAt(i).Address, compareBuffer, (uint)_typesize, out notNecessary))
                {
                    for (int k = 0; k < compareBuffer.Length - (_typesize - 1); k++)
                    {
                        found = true;
                        for (int j = 0; j < _typesize; j++)
                        {
                            if (compareBuffer[k + j] != _value[j])
                            {
                                found = false;
                            }
                            if (found)
                            {
                                ScanResult.Add(new ScanStructure(ScanHistory.Last().ElementAt(i).Address, _value));
                                found = false;
                            }
                        }
                    }
                }
            }
            if (ScanResult.Count() <= 20_000)
            {
                ScanResult.Sort();
            }
            ScanHistory.Add(ScanResult);
            Output = MemoryConverter.CreateDataGrid(ScanHistory, IsString);
        }


        //reads changes in memory for addresses in the list and updates these values
        public void RefreshList()
        {
            List<ScanStructure> arsch = new List<ScanStructure>();


            foreach (ScanStructure item in ScanHistory.Last())
            {
                byte[] refresh = new byte[item.Value.Length];
                ReadProcessMemory(targetHandle, item.Address, refresh, (uint)item.Value.Length, out notNecessary);
                arsch.Add(new ScanStructure(item.Address, refresh));
            }
            ScanHistory.Add(arsch);
            Output = MemoryConverter.RefreshDatagrid(ScanHistory, IsString);
            ScanHistory.Remove(ScanHistory.Last());
        }

        //writes values to a specific address in memory
        public void TestWrite(IntPtr address, byte[] wantedValue)
        {
            WriteProcessMemory(targetHandle, address, wantedValue, wantedValue.Length, out notNecessary);
        }

        //sets all necessary variables to status quo ante in order to do a new scan
        public void Reset()
        {
            minimumAddress = IntPtr.Zero;
            ScanHistory = new List<List<ScanStructure>>();
            Output = new List<DatagridSource>();
            regionLists = new List<RegionStructure>[Environment.ProcessorCount];
            PointerHistory = new List<List<PointerStructure>>();
        }

        public void PointerStart()
        {
            DumpMemorySynchronously();
            PointerHistory.Add(new List<PointerStructure>());


            IntPtr _currentAddress = new IntPtr(BitConverter.ToInt32(CurrentAddress, 0));
            MEMORY_BASIC_INFORMATION memInfo = new MEMORY_BASIC_INFORMATION();
            VirtualQueryEx(targetHandle, _currentAddress, out memInfo, (uint)Marshal.SizeOf(memInfo));
            PointerStructure pointer = new PointerStructure((uint)_currentAddress, new RegionStructure(memInfo.BaseAddress, (int)memInfo.RegionSize));
            PointerSearch(pointer);

            int egal = 0;

            PointerOutput = MemoryConverter.CreateDataGridForPointer(PointerHistory);
        }

        public struct PointerStructure
        {
            public PointerStructure(uint address, RegionStructure regionStructure)
            {
                this.regionStructure = regionStructure;
                offsets = new List<uint>();
                addresses = new List<uint> { address };
                moduleName = "stack";
            }

            public List<uint> addresses;
            public List<uint> offsets;
            public RegionStructure regionStructure;
            public string moduleName;
        }


        public struct MemoryStructure
        {
            public MemoryStructure(IntPtr regionBeginning, int regionSize, byte[] memory)
            {
                this.regionBeginning = regionBeginning;
                this.regionSize = regionSize;
                this.memory = memory;
            }

            public IntPtr regionBeginning;
            public int regionSize;
            public byte[] memory;
        }


        private bool PointingFromModule(PointerStructure pointer)
        {
            for (int i = 0; i < CurrentProcess.Modules.Count; i++)
            {
                if (pointer.addresses.First() >= (uint)CurrentProcess.Modules[i].BaseAddress && pointer.addresses.First() <= (uint)CurrentProcess.Modules[i].BaseAddress + CurrentProcess.Modules[i].ModuleMemorySize)
                {
                    return true;
                }
            }
            return false;
        }

        private string ModuleName(PointerStructure pointer)
        {
            for (int i = 0; i < CurrentProcess.Modules.Count; i++)
            {
                if (pointer.addresses.First() >= (uint)CurrentProcess.Modules[i].BaseAddress && pointer.addresses.First() <= (uint)CurrentProcess.Modules[i].BaseAddress + CurrentProcess.Modules[i].ModuleMemorySize)
                {
                    return CurrentProcess.Modules[i].ModuleName;
                }
            }
            return "arsch";
        }


        /// <summary>
        /// searches for pointer
        /// </summary>
        /// <param name="pointer"></param>
        private void PointerSearch(PointerStructure pointer)
        {
            foreach (MemoryStructure structure in MemoryDump)
            {
                for (int i = 0; i < structure.memory.Length - (TypeSize - 1); i++)
                {
                    uint _currentValue = (uint)(structure.memory[i + 3] << 24 | structure.memory[i + 2] << 16 | structure.memory[i + 1] << 8 | structure.memory[i]);
                }
            }
        }

        private bool PointingToRegion(uint currentValue, PointerStructure pointer)
        {
            if (currentValue < (uint)pointer.regionStructure.RegionBeginning || currentValue > pointer.addresses.First())
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void DumpMemorySynchronously()
        {
            List<Task> memoryTasks = new List<Task>();

            foreach (List<RegionStructure> item in regionLists)
            {
                Task region = Task.Run(() => DumpMemory(item));
                memoryTasks.Add(region);
            }

            foreach (Task task in memoryTasks)
            {
                task.Wait();
            }
        }


        private void DumpMemory(List<RegionStructure> list)
        {
            foreach (RegionStructure pair in list)
            {
                byte[] memoryBuffer = new byte[pair.RegionSize];
                if (ReadProcessMemory(targetHandle, pair.RegionBeginning, memoryBuffer, (uint)pair.RegionSize, out notNecessary))
                {
                    WriteToList(memoryBuffer, pair);
                }
            }
        }

        private void WriteToList(byte[] memoryBuffer, RegionStructure regionStructure)
        {
            bool done = false;
            while (!done)
            {
                Monitor.TryEnter(MemoryDump, ref done); //waits until no other thread is accessing the list
                if (done)
                {
                    MemoryDump.Add(new MemoryStructure(regionStructure.RegionBeginning, regionStructure.RegionSize, memoryBuffer));
                    Monitor.Exit(MemoryDump);
                }
            }
        }

        private MemoryStructure MainModuleDump()
        {
            byte[] memoryBuffer = new byte[CurrentProcess.MainModule.ModuleMemorySize];
            if (ReadProcessMemory(targetHandle, CurrentProcess.MainModule.BaseAddress, memoryBuffer, (uint)CurrentProcess.MainModule.ModuleMemorySize, out notNecessary))
            {
                return new MemoryStructure(CurrentProcess.MainModule.BaseAddress, CurrentProcess.MainModule.ModuleMemorySize, memoryBuffer);
            }
            else
            {
                throw new Exception("unreadable stack");
            }
        }



    }


}
