using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DLLImports.Kernel32DLL;

namespace dummyHacker
{
    class Garbage
    {


        public string[] Penner(int columnCount)
        {
            Random random = new Random();

            string[] penner = new string[columnCount];


            for (int i = 0; i < columnCount; i++)
            {
                penner[i] = random.Next(256).ToString("X2");
            }
            return penner;
        }



        public string[,] Penner2(int rowCount, int columnCount)
        {
            Random random = new Random();
            string[,] rectangularArray = new string[rowCount, columnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    rectangularArray[i, j] = random.Next(256).ToString("X2");
                }
            }
            return rectangularArray;
        }


        public string[][] Penner3(int rowCount, int columnCount)
        {
            Random random = new Random();
            string[][] jaggedArray = new string[rowCount][];


            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new string[columnCount];

                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    jaggedArray[i][j] = random.Next(256).ToString("X2");
                }
            }
            return jaggedArray;
        }

        //private void SplitList(List<PointerStructure> list)
        //{
        //    int threadCount = Environment.ProcessorCount;

        //    for (int i = 0; i < regionLists.Count(); i++)
        //    {
        //        splittedDump[i] = new List<PointerStructure>();
        //    }

        //    for (int i = 0; i < list.Count(); i++)
        //    {
        //        splittedDump[i % threadCount].Add(list.ElementAt(i));
        //    }
        //}

        //private void SearchForPointerSynchronously()
        //{
        //    List<Task> pointer = new List<Task>();

        //    foreach (List<PointerStructure> structures in splittedDump)
        //    {
        //        Task task = Task.Run(() => SearchForPointer(structures));
        //        pointer.Add(task);
        //    }

        //    foreach (Task task in pointer)
        //    {
        //        task.Wait();
        //    }
        //}

        //private void SearchForPointer(IntPtr referencedAddress)
        //{
        //    bool found;
        //    int _typesize = TypeSize;
        //    uint _offset;
        //    uint _currentValue;


        //    IntPtr _referencedAddress = referencedAddress;

        //    MEMORY_BASIC_INFORMATION memInfo = new MEMORY_BASIC_INFORMATION();
        //    VirtualQueryEx(targetHandle, _referencedAddress, out memInfo, (uint)Marshal.SizeOf(memInfo));

        //    foreach (PointerStructure structure in MemoryDump)
        //    {
        //        for (int i = 0; i < structure.memory.Length - (_typesize - 1); i++)
        //        {
        //            if (structure.regionBeginning + i == new IntPtr(0x509b74))
        //            {
        //                int egal = 0;
        //            }
        //            found = true;
        //            _currentValue = (uint)(structure.memory[i + 3] << 24 | structure.memory[i + 2] << 16 | structure.memory[i + 1] << 8 | structure.memory[i]);
        //            if (_currentValue < (uint)memInfo.BaseAddress || _currentValue > (uint)_referencedAddress)
        //            {
        //                found = false;
        //            }

        //            if (found && PointerHistory.Count() == 1)
        //            {
        //                List<uint> scan = new List<uint>();
        //                _offset = (uint)_referencedAddress - _currentValue;


        //                if (!PointingFromModule(_offset, structure, i, scan))
        //                {
        //                    scan.Add((uint)(structure.regionBeginning + i));
        //                    scan.Add((uint)CurrentProcess.Modules.Count);
        //                    scan.Add(_offset);
        //                    WriteToList(scan);
        //                }
        //            }
        //            else if (found && PointerHistory.Count() > 1)
        //            {
        //                _offset = (uint)_referencedAddress - _currentValue;

        //                foreach (List<uint> item in PointerHistory.ElementAt(PointerHistory.Count() - 1))
        //                {
        //                    if (_currentValue + _offset == item.ElementAt(0))
        //                    {
        //                        if (!PointingFromModule(_offset, structure, i, item))
        //                        {
        //                            item.Add(_offset);
        //                            WriteToList(item);
        //                        }
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }


        //}

        //private bool PointingFromModule(uint _offset, PointerStructure structure, int i, List<uint> scan)
        //{
        //    for (int j = 0; j < CurrentProcess.Modules.Count; j++)
        //    {
        //        if ((uint)structure.regionBeginning + i >= (uint)CurrentProcess.Modules[j].BaseAddress && (uint)structure.regionBeginning <= (uint)CurrentProcess.Modules[j].BaseAddress + CurrentProcess.Modules[j].ModuleMemorySize)
        //        {
        //            if (scan.Count() == 0)
        //            {
        //                scan.Add((uint)(structure.regionBeginning + i));
        //                scan.Add((uint)j);
        //            }
        //            else
        //            {
        //                scan[0] = (uint)(structure.regionBeginning + i);
        //                scan[1] = (uint)j;
        //            }
        //            scan.Add(_offset);
        //            WriteToList(scan);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        //private void DumpMemorySynchronously()
        //{
        //    MemoryDump = new List<PointerStructure>();
        //    List<Thread> memoryThreads = new List<Thread>();

        //    foreach (List<RegionStructure> item in regionLists)
        //    {
        //        Thread region = new Thread(() => DumpMemory(item));
        //        region.Start();
        //        memoryThreads.Add(region);
        //    }

        //    foreach (Thread thread in memoryThreads)
        //    {
        //        thread.Join();
        //    }

        //    SplitList(MemoryDump);

        //    int egal = 0;
        //}

        //public struct PointerStructure
        //{
        //    public PointerStructure(IntPtr regionBeginning, int regionSize, byte[] memory)
        //    {
        //        this.regionBeginning = regionBeginning;
        //        this.regionSize = regionSize;
        //        this.memory = memory;
        //    }

        //    public IntPtr regionBeginning;
        //    public int regionSize;
        //    public byte[] memory;
        //}

        //private void DumpMemory(List<RegionStructure> list)
        //{
        //    foreach (RegionStructure pair in list)
        //    {
        //        byte[] memoryBuffer = new byte[pair.RegionSize];
        //        if (ReadProcessMemory(targetHandle, pair.RegionBeginning, memoryBuffer, (uint)pair.RegionSize, out notNecessary))
        //        {
        //            WriteToList(memoryBuffer, pair);
        //        }
        //    }
        //}


        //private void WriteToList(List<uint> scan)
        //{
        //    bool done = false;
        //    while (!done)
        //    {
        //        Monitor.TryEnter(PointerHistory.Last(), ref done); //waits until no other thread is accessing the list
        //        if (done)
        //        {

        //            PointerHistory.Last().Add(scan);
        //            Monitor.Exit(PointerHistory.Last());
        //        }
        //    }
        //}


        //private void WriteToList(byte[] memoryBuffer, RegionStructure regionStructure)
        //{
        //    bool done = false;
        //    while (!done)
        //    {
        //        Monitor.TryEnter(MemoryDump, ref done); //waits until no other thread is accessing the list
        //        if (done)
        //        {
        //            MemoryDump.Add(new PointerStructure(regionStructure.RegionBeginning, regionStructure.RegionSize, memoryBuffer));
        //            Monitor.Exit(MemoryDump);
        //        }
        //    }
        //}

        //private void SearchForPointerInMultipleThreads()
        //{
        //    List<Thread> threadList = new List<Thread>();
        //    PointerHistory.Add(new List<uint[]>());


        //    foreach (List<RegionStructure> list in regionLists)
        //    {
        //        Thread arsch = new Thread(() => PointerScan(list));
        //        arsch.Start();
        //        threadList.Add(arsch);
        //    }

        //    foreach (Thread thread in threadList)
        //    {
        //        thread.Join();
        //    }

        //    PointerOutput = MemoryConverter.CreateDataGridForPointer(PointerHistory);
        //}

        ////reads memory for specific regions, compares them to a value and saves them into a history
        //private void PointerScan(List<RegionStructure> list)
        //{

        //    int _typesize = TypeSize;
        //    uint _offset;
        //    uint _currentValue;
        //    uint _processID = (uint)ProcessId;

        //    IntPtr _referencedAddress = new IntPtr(BitConverter.ToInt32(CurrentAddress, 0));

        //    MEMORY_BASIC_INFORMATION memInfo = new MEMORY_BASIC_INFORMATION();
        //    VirtualQueryEx(targetHandle, _referencedAddress, out memInfo, (uint)Marshal.SizeOf(memInfo));


        //    foreach (RegionStructure pair in list)
        //    {
        //        bool found;
        //        bool done;
        //        byte[] memoryBuffer = new byte[pair.RegionSize];
        //        if (ReadProcessMemory(targetHandle, pair.RegionBeginning, memoryBuffer, (uint)pair.RegionSize, out notNecessary))            //ToDo: das hier ist arsch langsam...
        //        {
        //            for (int i = 0; i < memoryBuffer.Length - (_typesize - 1); i++)
        //            {
        //                found = true;
        //                _currentValue = (uint)(memoryBuffer[i + 3] << 24 | memoryBuffer[i + 2] << 16 | memoryBuffer[i + 1] << 8 | memoryBuffer[i]);
        //                if (_currentValue < (uint)memInfo.BaseAddress || _currentValue > (uint)_referencedAddress)
        //                {
        //                    found = false;
        //                }
        //                if (found)
        //                {
        //                    _offset = (uint)_referencedAddress - _currentValue;
        //                    done = false;
        //                    uint[] scan = new uint[] { (uint)(pair.RegionBeginning + i), _processID, _offset };

        //                    while (!done)
        //                    {
        //                        Monitor.TryEnter(PointerHistory.Last(), ref done); //waits until no other thread is accessing the list
        //                        if (done)
        //                        {
        //                            PointerHistory.Last().Add(scan);
        //                            Monitor.Exit(PointerHistory.Last());
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}


        public string[][] RPMtoString(int ColumnCount, int RowCount, MemoryEditorV2 meow, int helper)
        {

            string[][] jaggedRPMstringArray = new string[RowCount][];

            int size = ColumnCount * RowCount;

            byte[] buffer = new byte[size];

            if (ReadProcessMemory(meow.targetHandle, new IntPtr(helper), buffer, (uint)size, out meow.notNecessary))
            {
                for (int i = 0; i < RowCount; i++)
                {
                    jaggedRPMstringArray[i] = new string[ColumnCount];

                    for (int j = 0; j < ColumnCount; j++)
                    {
                        jaggedRPMstringArray[i][j] = buffer[i * ColumnCount + j].ToString("X2");
                    }

                }
            }
            else
            {
                bool found = false;

                foreach (List<RegionStructure> list in meow.regionLists)
                {
                    if (found)
                    {
                        break;
                    }


                    foreach (RegionStructure item in list)
                    {
                        if (
                            helper > (int)item.RegionBeginning
                            && helper < ((int)item.RegionBeginning + item.RegionSize)
                            && helper + size > ((int)item.RegionBeginning + item.RegionSize)
                            )
                        {
                            uint sizeHelper = (uint)((int)item.RegionBeginning + item.RegionSize - helper);
                            found = true;

                            ReadProcessMemory(meow.targetHandle, new IntPtr(helper), buffer, sizeHelper, out meow.notNecessary);
                            for (int i = 0; i < RowCount; i++)
                            {
                                jaggedRPMstringArray[i] = new string[ColumnCount];

                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    if (sizeHelper > i * ColumnCount + j)
                                    {
                                        jaggedRPMstringArray[i][j] = buffer[i * ColumnCount + j].ToString("X2");
                                    }
                                    else
                                    {
                                        jaggedRPMstringArray[i][j] = "JJ";
                                    }
                                }
                            }
                            if (found)
                            {
                                break;
                            }
                        }



                        else if (
                            !found
                            && helper < (int)item.RegionBeginning
                            && helper + size > (int)item.RegionBeginning
                            )
                        {
                            int helperHelper = 0;
                            int sizeHelper = (int)item.RegionBeginning - helper;
                            helperHelper = helper + sizeHelper;

                            found = true;

                            ReadProcessMemory(meow.targetHandle, new IntPtr(helperHelper), buffer, (uint)size, out meow.notNecessary);
                            for (int i = 0; i < RowCount; i++)
                            {
                                jaggedRPMstringArray[i] = new string[ColumnCount];

                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    if (sizeHelper > i * ColumnCount + j)
                                    {
                                        jaggedRPMstringArray[i][j] = "jj";
                                    }
                                    else
                                    {
                                        jaggedRPMstringArray[i][j] = buffer[i * ColumnCount + j].ToString("X2");
                                    }
                                }
                            }
                            if (found)
                            {
                                break;
                            }
                        }



                        else
                        {
                            for (int i = 0; i < RowCount; i++)
                            {
                                jaggedRPMstringArray[i] = new string[ColumnCount];

                                for (int j = 0; j < ColumnCount; j++)
                                {
                                    jaggedRPMstringArray[i][j] = "??";
                                }
                            }
                        }

                    }
                }
            }
            return jaggedRPMstringArray;
        }
    }
}
