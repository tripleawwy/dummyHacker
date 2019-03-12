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


        //converts a memory chunk to an array of string arrays (10*16 : memoryView case)
        public string[][] RPMasString(int columnCount, int rowCount)
        {

            string[][] jaggedArray = new string[rowCount][];
            for (int i = 0; i < rowCount; i++)
            {
                jaggedArray[i] = new string[columnCount];
            }
            int size = columnCount * rowCount;
            byte[] buffer = new byte[size];

            //check whether memory chunk is readable at all
            if (!ReadProcessMemory(meow.targetHandle, new IntPtr(helper), buffer, (uint)size, out meow.notNecessary))
            {
                GetMemoryInfo();

                //if regionborder
                if (((int)memInfo.BaseAddress + memInfo.RegionSize) < (helper + size))
                {
                    uint sizeHelper = (uint)((int)memInfo.BaseAddress + memInfo.RegionSize - helper);

                    //in the beginning fill not readable with "JJ"
                    if (ReadProcessMemory(meow.targetHandle, new IntPtr(helper), buffer, sizeHelper, out meow.notNecessary))
                    {
                        for (int i = 0; i < sizeHelper; i++)
                        {
                            jaggedArray[i / columnCount][i % columnCount] = buffer[i].ToString("X2");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < sizeHelper; i++)
                        {
                            jaggedArray[i / columnCount][i % columnCount] = "JJ";

                        }
                    }

                    //in the end fill not readable with "jj"
                    if (ReadProcessMemory(meow.targetHandle, new IntPtr(helper + sizeHelper), buffer, (uint)size - sizeHelper, out meow.notNecessary))
                    {
                        for (int i = (int)sizeHelper; i < size; i++)
                        {
                            jaggedArray[i / columnCount][i % columnCount] = buffer[i - sizeHelper].ToString("X2");

                        }
                    }
                    else
                    {
                        for (int i = (int)sizeHelper; i < size; i++)
                        {
                            jaggedArray[i / columnCount][i % columnCount] = "jj";

                        }
                    }
                }
                //most likely protected areas are filled with "??"
                else
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        jaggedArray[i] = new string[columnCount];

                        for (int j = 0; j < columnCount; j++)
                        {
                            jaggedArray[i][j] = "??";
                        }
                    }
                }

            }
            //if memory chunk is readable convert the buffer
            else
            {
                for (int i = 0; i < rowCount; i++)
                {
                    jaggedArray[i] = new string[columnCount];

                    for (int j = 0; j < columnCount; j++)
                    {
                        jaggedArray[i][j] = buffer[i * columnCount + j].ToString("X2");
                    }

                }
            }
            return jaggedArray;
        }
        

        public void AddRows(int rowCount)
        {
            string[][] arsch = RPMasString(dataGridView2.ColumnCount, rowCount);
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
                string[][] arsch = RPMasString(dataGridView2.ColumnCount, rowCount);
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
