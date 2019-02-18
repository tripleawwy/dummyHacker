using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dummyHacker
{
    public partial class Form1 : Form
    {

        MemoryEditor firstTry = new MemoryEditor();
        BindingSource source = new BindingSource();
        int size;
        int textboxContent;
        int basicValue;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void firstScan(object sender, EventArgs e)
        {
            firstTry.NewProcess(11836);
            firstTry.ScanSystem();
            firstTry.CreateEntryPoints();
            firstTry.SearchForValues(size, textboxContent);
            firstTry.CreateDataGridSource1(textboxContent);

            source.DataSource = firstTry.dataGridSource;
            //source.DataSource = firstTry.CreateDataGridSource1(textboxContent);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[2].Visible = false;


            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
            nextScan.Enabled = true;
            basicValue = textboxContent;



        }
       
        private void nextScan_Click(object sender, EventArgs e)
        {
            firstTry.CompareLists(size, textboxContent);
            firstTry.CompareDataGridSource(textboxContent, basicValue);
            source.DataSource = firstTry.dataGridSource;
            //dataGridView1.DataSource = source;
            dataGridView1.Columns[2].Visible = true;
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Items.Contains("Byte"))
            {
                size = 1;
                button1.Enabled = true;
            }
            if (comboBox1.Items.Contains("Short"))
            {
                size = 2;
                button1.Enabled = true;
            }
            if (comboBox1.Items.Contains("Int"))
            {
                size = 4;
                button1.Enabled = true;
            }
            if (comboBox1.Items.Contains("Long"))
            {
                size = 8;
                button1.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out textboxContent) && textBox1.TextLength != 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                textBox1.SelectionStart = textBox1.Text.Length;
            }
                     
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
