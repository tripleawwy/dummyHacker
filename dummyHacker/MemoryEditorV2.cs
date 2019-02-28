using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static DLLImports.Kernel32DLL;
using static DLLImports.Kernel32DLL.ProcessAccessFlags;

namespace dummyHacker
{
    public struct ScanStructure
    {
        public ScanStructure(IntPtr ptr, byte[] value)
        {
            Address = ptr;
            Value = value;
        }
        public IntPtr Address { get; set; }
        public byte[] Value { get; set; }
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
        public MemoryEditorV2( int processId)
        {
            ProcessId = processId;
            InitializeBasicInformation();
        }

        public byte[] Value { get; set; }
        public int TypeSize => Value.Length;
        public int ProcessId { get; set; }



        private IntPtr targetHandle;
        private IntPtr notNecessary;
        private long maximum32BitAddress;
        private IntPtr minimumAddress;



        private List<Thread> threadList;
        private List<RegionStructure>[] regionLists;
        private List<ScanStructure> ScanResult;




        //fills MemoryEditorV2 with basic information such as the TargetHandle, MinimumApplicationAddress and asks for rights to read memoryRegions
        private void InitializeBasicInformation()
        {
            ScanSystem();

            notNecessary = IntPtr.Zero;
            ScanResult = new List<ScanStructure>();
            regionLists = new List<RegionStructure>[Environment.ProcessorCount];

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
            MEMORY_BASIC_INFORMATION memoryInfo = new MEMORY_BASIC_INFORMATION();
            List<RegionStructure> originalRegionList = new List<RegionStructure>();
            RegionStructure region;


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

                    //combines regions which are arranged consecutively in VRAM and adds them to a List
                    if (originalRegionList.Count != 0 && (originalRegionList.Last().RegionBeginning + originalRegionList.Last().RegionSize == memoryInfo.BaseAddress))
                    {
                        region = new RegionStructure(originalRegionList.Last().RegionBeginning, originalRegionList.Last().RegionSize + (int)memoryInfo.RegionSize);
                        originalRegionList.Add(region);
                        originalRegionList.Remove(originalRegionList.ElementAt(originalRegionList.Count - 2));
                    }
                    //adds regions to a List
                    else
                    {
                        region = new RegionStructure(memoryInfo.BaseAddress, (int)memoryInfo.RegionSize);
                        originalRegionList.Add(region);
                    }
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



        private void SearchForValuesInMultipleThreads()
        {
            threadList = new List<Thread>();

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
        }

        private void ReadMemory(List<RegionStructure> list)
        {
            byte[] _value = Value;
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
                            if (memoryBuffer[i + j] != _value[j])
                            {
                                found = false;
                            }
                        }
                        if (found)
                        {
                            done = false;

                            while (!done)
                            {
                                Monitor.TryEnter(ScanResult, ref done);
                                if (done)
                                {
                                    ScanStructure scan = new ScanStructure(pair.RegionBeginning + i, _value);
                                    ScanResult.Add(scan);
                                    Monitor.Exit(ScanResult);
                                }
                            }
                        }
                    }
                }
            }
        }
    }


}
