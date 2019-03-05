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

        BindingSource source = new BindingSource();
        int backup;
        int helperscrollindex = 15;
        int baseaddress= -15;

        int datacolumnCount = 16;
        int datarowCount = 50;


        public Form1()
        {
            InitializeComponent();
        }

        public DataTable Bla()
        {


            DataTable meineTabelle = new DataTable();

            for (int i = 0; i < datacolumnCount; i++)
            {
                meineTabelle.Columns.Add(new DataColumn(i.ToString("X"), typeof(string)));
            }

            for (int i = baseaddress; i < baseaddress + datarowCount; i++)
            {

                DataRow row = meineTabelle.NewRow();
                row[0] = (i * datacolumnCount).ToString();
                //row[0] = 255;
                meineTabelle.Rows.Add(row);
            }

            return meineTabelle;
        }




        private void button1_Click(object sender, EventArgs e)
        {
            DataTable arsch = Bla();
            source.DataSource = arsch;
            dataGridView1.DataSource = source;
            RowTestWriter();
            dataGridView1.FirstDisplayedScrollingRowIndex = helperscrollindex;
        }

        private void RowTestWriter()
        {
            int i = baseaddress;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                item.HeaderCell.Value = (i * datacolumnCount).ToString("X8");
                i++;

            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (checkBox1.Checked == true)
            {
                //backup = dataGridView1.FirstDisplayedCell.RowIndex;


                //dataGridView1.Invoke((Action)(() => dataGridView1.DataSource = Bla()));


                //dataGridView1.Invoke((Action)(() => dataGridView1.FirstDisplayedScrollingRowIndex = backup));



                Bla();
                Thread.Sleep(500);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            source.DataSource = Bla();
            dataGridView1.DataSource = source;


            //backup = dataGridView1.FirstDisplayedCell.RowIndex;

            //DataTable arsch = Bla();
            //source.DataSource = arsch;
            //dataGridView1.DataSource = source;


            ////dataGridView1.CurrentCell = dataGridView1.Rows[backup].Cells[0];
            //dataGridView1.FirstDisplayedScrollingRowIndex = backup;
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            if (dataGridView1.FirstDisplayedScrollingRowIndex < helperscrollindex)
            {
                baseaddress -= 10;

            }
            if (dataGridView1.FirstDisplayedScrollingRowIndex > helperscrollindex)
            {
                baseaddress += 10;
            }
            //dataGridView1.FirstDisplayedScrollingRowIndex = helperscrollindex;


            //dataGridView1.DataSource = Bla();


            RowTestWriter();
        }
    }
}
