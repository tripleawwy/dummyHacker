using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static DLLImports.Kernel32DLL;

namespace dummyHacker
{
    public partial class MemoryViewForm : Form
    {


        private static Random random = new Random();

        MemoryEditorV2 meow;
        int helper;
        int baseAddress;
        bool shallCounterWork;
        bool counterDirection;
        MEMORY_BASIC_INFORMATION memInfo;



        public MemoryViewForm(MemoryEditorV2 editorV2, int selectedAddress)
        {
            meow = editorV2;
            baseAddress = selectedAddress;
            helper = AddressMinusLastPosition(baseAddress);


            InitializeComponent();
            HelpInitialize();

        }

        private void HelpInitialize()
        {
            AddRows(10);
            vScrollBar1.Maximum = 50_000;
            vScrollBar1.Value = vScrollBar1.Maximum / 2 - vScrollBar1.LargeChange / 2;
            memInfo = new MEMORY_BASIC_INFORMATION();

        }

        public int AddressMinusLastPosition(int CurrentAddress)
        {
            helper = CurrentAddress - CurrentAddress % 0x10;

            return helper;
        }

        public void GetMemoryInfo()
        {
            VirtualQueryEx(meow.targetHandle, new IntPtr(helper), out memInfo, (uint)Marshal.SizeOf(memInfo));
        }


        public string[][] Test(int ColumnCount, int RowCount)
        {

            string[][] testString = new string[RowCount][];
            for (int i = 0; i < RowCount; i++)
            {
                testString[i] = new string[ColumnCount];
            }
            int size = ColumnCount * RowCount;
            byte[] buffer = new byte[size];

            //check whether memory chunk is readable at all
            if (!ReadProcessMemory(meow.targetHandle, new IntPtr(helper), buffer, (uint)size, out meow.notNecessary))
            {
                GetMemoryInfo();

                //if Address is inside of an accessible chunk but buffer exceeds the regionsize
                // if (/*helper >= (int)memInfo.BaseAddress &&*/ helper + size > ((int)memInfo.BaseAddress + memInfo.RegionSize)  )
                if (((int)memInfo.BaseAddress + memInfo.RegionSize) < (helper + size))
                {
                    uint sizeHelper = (uint)((int)memInfo.BaseAddress + memInfo.RegionSize - helper);

                    //1. Anfang lesen
                    if (ReadProcessMemory(meow.targetHandle, new IntPtr(helper), buffer, sizeHelper, out meow.notNecessary))
                    {
                        for (int i = 0; i < sizeHelper; i++)
                        {
                            testString[i / ColumnCount][i % ColumnCount] = buffer[i].ToString("X2");

                        }
                    }
                    else
                    {
                        for (int i = 0; i < sizeHelper; i++)
                        {
                            testString[i / ColumnCount][i % ColumnCount] = "JJ";

                        }
                    }


                    //for (int i = 0; i < RowCount; i++)
                    //{
                    //    testString[i] = new string[ColumnCount];

                    //    for (int j = 0; j < ColumnCount; j++)
                    //    {
                    //        if (i * ColumnCount + j < sizeHelper )
                    //        {
                    //            testString[i][j] = buffer[i * ColumnCount + j].ToString("X2");
                    //        }
                    //        else
                    //        {
                    //            testString[i][j] = "JJ";
                    //        }
                    //    }
                    //}
                    //}
                    //if Address is outside of an accessible chunk but inside with corresponding buffer
                    //else if (/*helper < (int)memInfo.BaseAddress + memInfo.RegionSize &&*/ helper + size > (int)memInfo.BaseAddress + memInfo.RegionSize)
                    //{

                    //sizeHelper = (int)memInfo.BaseAddress - helper; //hier ist der fehler
                    // int helperHelper = helper + sizeHelper;


                    //2. Ende lesen

                    if (ReadProcessMemory(meow.targetHandle, new IntPtr(helper + sizeHelper), buffer, (uint)size - sizeHelper, out meow.notNecessary))
                    //                        if (ReadProcessMemory(meow.targetHandle, new IntPtr(helperHelper), buffer, (uint)size, out meow.notNecessary))
                    {


                        for (int i = (int)sizeHelper; i < size; i++)
                        {
                            testString[i / ColumnCount][i % ColumnCount] = buffer[i - sizeHelper].ToString("X2");

                        }
                    }
                    else
                    {
                        for (int i = (int)sizeHelper; i < size; i++)
                        {
                            testString[i / ColumnCount][i % ColumnCount] = "jj";

                        }
                    }


                    //for (int i = 0; i < RowCount; i++)
                    //{
                    //    testString[i] = new string[ColumnCount];

                    //    for (int j = 0; j < ColumnCount; j++)
                    //    {
                    //        if (sizeHelper > i * ColumnCount + j)
                    //        {
                    //            testString[i][j] = "jj";
                    //        }
                    //        else
                    //        {
                    //            testString[i][j] = buffer[i * ColumnCount + j].ToString("X2");
                    //        }
                    //    }
                    //}

                }
                //most likely protected area
                else
                {
                    for (int i = 0; i < RowCount; i++)
                    {
                        testString[i] = new string[ColumnCount];

                        for (int j = 0; j < ColumnCount; j++)
                        {
                            testString[i][j] = "??";
                        }
                    }
                }

            }
            //if memory chunk is readable convert the buffer
            else
            {
                for (int i = 0; i < RowCount; i++)
                {
                    testString[i] = new string[ColumnCount];

                    for (int j = 0; j < ColumnCount; j++)
                    {
                        testString[i][j] = buffer[i * ColumnCount + j].ToString("X2");
                    }

                }
            }
            return testString;
        }


        public string[][] RPMtoString(int ColumnCount, int RowCount)
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

        public void AddRows(int rowCount)
        {
            //string[][] arsch = RPMtoString(dataGridView2.ColumnCount, rowCount);
            string[][] arsch = Test(dataGridView2.ColumnCount, rowCount);
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows.Insert(i, arsch[i]);
                dataGridView2.Rows[i].HeaderCell.Value = (helper + i * dataGridView2.ColumnCount).ToString("X8");
                dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[baseAddress % 0x10];
            }
        }



        public void ChangeContent(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                //string[][] arsch = RPMtoString(dataGridView2.ColumnCount, rowCount);
                string[][] arsch = Test(dataGridView2.ColumnCount, rowCount);
                dataGridView2.Rows[i].SetValues(arsch[i]);
            }
        }


        public void ChangeCellHeader(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows[i].HeaderCell.Value = (helper + i * dataGridView2.ColumnCount).ToString("X8");
            }
        }


        public string[] Penner()
        {
            string[] penner = new string[dataGridView2.ColumnCount];


            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                penner[i] = random.Next(256).ToString("X2");
            }
            return penner;
        }



        public string[,] Penner2(int rowCount)
        {
            string[,] rectangularArray = new string[rowCount, dataGridView2.ColumnCount];


            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    rectangularArray[i, j] = random.Next(256).ToString("X2");
                }
            }
            return rectangularArray;
        }


        public string[][] Penner3(int rowCount)
        {
            string[][] jaggedArray = new string[rowCount][];


            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new string[dataGridView2.ColumnCount];

                for (int j = 0; j < jaggedArray[i].Length; j++)
                {
                    jaggedArray[i][j] = random.Next(256).ToString("X2");
                }
            }
            return jaggedArray;
        }



        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (
                vScrollBar1.Value + 500 <= e.NewValue
                && !Counter.IsBusy
                )
            {
                counterDirection = true;
                shallCounterWork = true;
                Counter.RunWorkerAsync();


            }
            else if (vScrollBar1.Value - 500 >= e.NewValue && !Counter.IsBusy)
            {
                counterDirection = false;
                shallCounterWork = true;
                Counter.RunWorkerAsync();


            }
            else if (
                     e.Type == ScrollEventType.LargeIncrement
                     || e.Type == ScrollEventType.SmallIncrement
                     //|| e.Type == ScrollEventType.ThumbTrack
                     //|| e.Type == ScrollEventType.ThumbPosition
                     //|| e.Type == ScrollEventType.Last
                     //|| e.Type == ScrollEventType.First
                     //|| e.Type == ScrollEventType.EndScroll
                     || e.Type == ScrollEventType.SmallDecrement
                     || e.Type == ScrollEventType.LargeDecrement
                     )
            {
                helper += (e.NewValue - e.OldValue) * 0x010;
                ChangeCellHeader(dataGridView2.RowCount);
                e.NewValue = vScrollBar1.Maximum / 2;
            }

            else
            {
                e.NewValue = vScrollBar1.Maximum / 2;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            if (backgroundWorker1.IsBusy)
            {
                backgroundWorker1.CancelAsync();
            }
            DialogResult = DialogResult.OK;
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.WorkerSupportsCancellation = true;
            while (checkBox1.Checked == true)
            {
                dataGridView2.Invoke((Action)(() => ChangeContent(dataGridView2.RowCount)));
                Thread.Sleep(200);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
        }

        private void dataGridView2_MouseHover(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
        }

        private void Counter_DoWork(object sender, DoWorkEventArgs e)
        {
            while (
                shallCounterWork
                || vScrollBar1.Value == vScrollBar1.Maximum
                || vScrollBar1.Value == vScrollBar1.Minimum
                )
            {
                if (vScrollBar1.Value == vScrollBar1.Maximum / 2)
                {
                    shallCounterWork = false;
                }
                if (counterDirection)
                {
                    helper += 0x1000;
                    ChangeCellHeader(dataGridView2.RowCount);
                    Thread.Sleep(5);
                }
                else
                {
                    helper -= 0x1000;
                    ChangeCellHeader(dataGridView2.RowCount);
                    Thread.Sleep(5);
                }
            }
        }

        private void MemoryViewForm_Load(object sender, EventArgs e)
        {

        }
    }
}
