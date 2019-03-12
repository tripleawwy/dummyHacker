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
