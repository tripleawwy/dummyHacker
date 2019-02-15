using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;
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
        private IntPtr arsch = IntPtr.Zero;
        private IntPtr targetHandle = new IntPtr();
        int _processId;
        readonly long maximum32BitAddress = 0x7fffffff;
        private IntPtr minimumAddress;
        private Dictionary<IntPtr, int> AddressAndValue;
        private Dictionary<IntPtr, int> regionBeginning;



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
                    regionBeginning.Add(memoryInfo.BaseAddress,(int)memoryInfo.RegionSize);
                }
                helpMinimumAddress = helpMinimumAddress + memoryInfo.RegionSize;
            }
        }

        public void AnalyzeRegions(byte[] buffer)
        {
            byte[] memoryBuffer = buffer;

            for (int i = 0; i < regionBeginning.Count; i++)
            {
                if (ReadProcessMemory(targetHandle, regionBeginning, memoryBuffer, regionBeginning, out arsch))
                {

                }
            }
        }

        //for (int i = 0; i < buffer.Length - 3; i++)
        //{

        //    if ((buffer[i + 3] << 8 | buffer[i + 2] << 8 | buffer[i + 1] << 8 | buffer[i]) == 20)
        //    {
        //        j++;
        //        Console.Write((buffer.BaseAddress + i).ToString("X8") + " = " + 20 + "\t");
        //    }
        //}


    }
}
