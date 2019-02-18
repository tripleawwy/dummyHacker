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
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void firstScan(object sender, EventArgs e)
        {
            MemoryEditor firstTry = new MemoryEditor();
            firstTry.NewProcess(15240);
            firstTry.ScanSystem();
            firstTry.CreateEntryPoints();
            BindingList<string> arsch = new BindingList<string>(firstTry.SearchForValues(4, 20).ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); }));
            dataGridView1.DataSource = firstTry.SearchForValues(4, 20);

            listBox1.DataSource = firstTry.SearchForValues(4, 20).ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
            listView1.GridLines = true;
            
            //= firstTry.SearchForValues(4, 20).ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
        }

    }
}
