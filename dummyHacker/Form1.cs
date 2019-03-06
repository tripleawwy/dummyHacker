using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using static DLLImports.Kernel32DLL;

namespace dummyHacker
{
    public partial class Form1 : Form
    {
        int processID = 0x67c;
        int helper = 0x00DBA208;
        int baseAddress = 0x00DBA208;


        public Form1()
        {
            InitializeComponent();
            HelpInitialize();
        }

        private void HelpInitialize()
        {
            AddRows(10);
            vScrollBar1.Maximum = 500;
            vScrollBar1.Value = vScrollBar1.Maximum / 2 - vScrollBar1.LargeChange / 2;

        }

        public string[] RPMtoString(int ColumnCount)
        {
            MemoryEditorV2 versuch = new MemoryEditorV2(processID);
            versuch.InitializeBasicInformation();


            string[] RPMstring = new string[ColumnCount];

            int size = dataGridView2.ColumnCount * sizeof(byte);
            byte[] buffer = new byte[size];
            ReadProcessMemory(versuch.targetHandle, new IntPtr(baseAddress), buffer, (uint)size, out versuch.notNecessary);
            for (int i = 0; i < buffer.Length; i++)
            {
                RPMstring[i] = buffer[i].ToString("X2");
            }



            return RPMstring;


        }

        public void AddRows(int rowCount)
        {
            helper += 0;
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows.Insert(i, Penner());
                dataGridView2.Rows[i].HeaderCell.Value = ((helper + i) * dataGridView2.ColumnCount).ToString("X8");
            }
        }



        public void ChangeContentPositive(int rowCount)
        {
            helper += 1;
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows[i].SetValues(Penner());
                dataGridView2.Rows[i].HeaderCell.Value = ((helper + i) * dataGridView2.ColumnCount).ToString("X8");
            }
        }

        public void RefreshContent(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows[i].SetValues(RPMtoString(dataGridView2.ColumnCount));
                dataGridView2.Rows[i].HeaderCell.Value = ((baseAddress + i) * dataGridView2.ColumnCount).ToString("X8");
            }
        }

        public void ChangeContentNegative(int rowCount)
        {
            helper -= 1;
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows[i].SetValues(Penner());
                dataGridView2.Rows[i].HeaderCell.Value = ((helper + i) * dataGridView2.ColumnCount).ToString("X8");
            }
        }

        public void DeleteRows(int rowCount)
        {

        }

        public string[] Penner()
        {
            string[] penner = new string[dataGridView2.ColumnCount];

            for (int i = 0; i < dataGridView2.ColumnCount; i++)
            {
                penner[i] = i.ToString("X2");
            }
            return penner;
        }


        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
            {
                ChangeContentPositive(dataGridView2.RowCount);
            }
            else if (e.NewValue < e.OldValue)
            {
                ChangeContentNegative(dataGridView2.RowCount);
            }
        }


        private void dataGridView2_Click(object sender, EventArgs e)
        {
            //vScrollBar1.Focus();
            //vScrollBar1.Value = vScrollBar1.Maximum / 2;
            //vScrollBar1.Update();
            //vScrollBar1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshContent(dataGridView2.RowCount);
        }

        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            while (checkBox1.Checked == true)
            {
                vScrollBar1.Invoke((Action)(() => vScrollBar1.Value = vScrollBar1.Maximum / 2));
                Thread.Sleep(50);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }


    }
}
