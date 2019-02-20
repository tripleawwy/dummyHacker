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
    public partial class Hauptprogramm : Form
    {

        public MemoryEditor firstTry = new MemoryEditor();
        BindingSource source = new BindingSource();
        int size;
        int textboxContent;
        int basicValue;


        public Hauptprogramm()
        {
            InitializeComponent();
        }


        private void HauptProgramm_Load(object sender, EventArgs e)
        {
            ComboSource();
        }
        private void ComboSource()
        {
            Dictionary<int, string> ValueSizeAndName = new Dictionary<int, string>();
            ValueSizeAndName.Add(1, "Byte");
            ValueSizeAndName.Add(2, "Short");
            ValueSizeAndName.Add(4, "Int");
            ValueSizeAndName.Add(8, "Long");
            InputTypeComboBox.DataSource = new BindingSource(ValueSizeAndName,null);
            InputTypeComboBox.DisplayMember = "Value";
            InputTypeComboBox.ValueMember = "Key";

        }

        private void FirstScan(object sender, EventArgs e)
        {
            firstTry.NewProcess(4696);
            firstTry.ScanSystem();
            firstTry.CreateEntryPoints();
            firstTry.SearchForValues(size, textboxContent);
            firstTry.CreateDataGridSource1(textboxContent);

            source.DataSource = firstTry.dataGridSource;
            //source.DataSource = firstTry.CreateDataGridSource1(textboxContent);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[2].Visible = false;
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
            NextScanButton.Enabled = true;
            ResetButton.Enabled = true;
            AddressFoundLabel.Visible = true;
            basicValue = textboxContent;
            AddressFoundLabel.Text = firstTry.memoryMemory.Count.ToString();
            FirstScanButton.Enabled = false;
        }
       
        private void NextScan(object sender, EventArgs e)
        {
            dataGridView1.Columns[2].Visible = true;
            firstTry.CompareLists(size, textboxContent);
            firstTry.CompareDataGridSource(textboxContent, basicValue);
            source.DataSource = firstTry.dataGridSource;
            basicValue = textboxContent;

            //dataGridView1.DataSource = source;
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key;
            if (size!=0)
            {
                FirstScanButton.Enabled = true;
            }
        }

        private void ValueToFindTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(ValueToFindTextBox.Text, out textboxContent) && ValueToFindTextBox.TextLength != 0)
            {
                ValueToFindTextBox.Text = ValueToFindTextBox.Text.Remove(ValueToFindTextBox.Text.Length - 1);
                ValueToFindTextBox.SelectionStart = ValueToFindTextBox.Text.Length;
            }                     
        }

        private void Schreiben_Click(object sender, EventArgs e)
        {            
            {
                IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
                int test2 = int.Parse(WriteValueTextBox.Text);
                firstTry.TestWrite(test, test2);
            }
        }

        private void AddresstextBox_Click(object sender, EventArgs e)
        {
            WriteAddressTextBox.SelectAll();
        }

        private void Refresher_DoWork(object sender, DoWorkEventArgs e)
        {
            while (AutoRefreshcheckBox.Checked==true)
            {
                firstTry.RefreshSource(basicValue);
                dataGridView1.Invoke((Action)(() => source.DataSource = firstTry.dataGridSource));
                Thread.Sleep(500);
            }
        }

        private void ValueToFindtextBox_Click(object sender, EventArgs e)
        {
            ValueToFindTextBox.SelectAll();
        }

        private void Freezer_DoWork(object sender, DoWorkEventArgs e)
        {
            IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
            int test2 = int.Parse(WriteValueTextBox.Text);
            while (Freeze.Checked == true)
            {
                schreiben.Invoke((Action)(() => schreiben.Enabled = false));
                firstTry.TestWrite(test, test2);
                Thread.Sleep(100);
            }
            schreiben.Invoke((Action)(() => schreiben.Enabled = true));
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            firstTry.Reset();
        }

        private void AutoRefreshcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoRefreshcheckBox.Checked==true)
            {
                dataGridRefresher.RunWorkerAsync();
            }
        }

        private void Freeze_CheckedChanged(object sender, EventArgs e)
        {
            if (Freeze.Checked == true)
            {
                Freezer.RunWorkerAsync();
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            Attach_Process attach_Process = new Attach_Process();
            attach_Process.Show();            
        }
    }
}
