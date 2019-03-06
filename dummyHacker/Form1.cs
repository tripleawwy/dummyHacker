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

namespace dummyHacker
{
    public partial class Form1 : Form
    {
        int helper = 0;
        
        public Form1()
        {
            InitializeComponent();
            HelpInitialize();
        }

        private void HelpInitialize()
        {
            AddRows(10);
            vScrollBar1.Maximum = dataGridView2.RowCount;


        }

        public void AddRows(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows.Insert(i, Penner());
                dataGridView2.Rows[i].HeaderCell.Value = (i * dataGridView2.ColumnCount).ToString("X8");
            }
            helper += rowCount;
        }



        public void ChangeContentPositive(int rowCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                dataGridView2.Rows[i].SetValues(Penner());
                dataGridView2.Rows[i].HeaderCell.Value = ((helper +i) * dataGridView2.ColumnCount).ToString("X8");
            }
            helper += rowCount;
        }

        public void ChangeContentNegative(int rowCount)
        {
            helper -= rowCount;
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
            ChangeContentPositive(dataGridView2.RowCount);
            //dataGridView2.FirstDisplayedScrollingRowIndex = e.NewValue;

            //else
            //{
            //    dataGridView2.FirstDisplayedScrollingRowIndex = e.NewValue;
            //    ChangeContentNegative(dataGridView2.RowCount);
            //}

        }


        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            //vScrollBar1.Value = e.NewValue;
        }


        private void dataGridView2_Click(object sender, EventArgs e)
        {
            vScrollBar1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangeContentPositive(dataGridView2.RowCount);
        }
    }
}
