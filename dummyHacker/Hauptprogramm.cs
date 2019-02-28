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
        MemoryEditorV2 meow;
        MemoryEditor firstTry = new MemoryEditor();
        BindingSource source = new BindingSource();
        int size;
        byte[] textboxContent;
        string basicValue;
        bool isString = false;



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
            // TODO : InputTypeComboBox.Items.AddRange(new object[] {"das hier"});
            Dictionary<int, string> ValueSizeAndName = new Dictionary<int, string> { { 1, "Byte" }, { 2, "Short" }, { 4, "Int" }, { 8, "Long" }, { 42, "String" } };
            InputTypeComboBox.DataSource = new BindingSource(ValueSizeAndName, null);
            InputTypeComboBox.DisplayMember = "Value";
            InputTypeComboBox.ValueMember = "Key";
            InputTypeComboBox.SelectedIndex = 2;
        }

        private void FirstScan_Click(object sender, EventArgs e)
        {
            //meow.Value = MemoryConverter.TextBoxContentAsByteArray(ValueToFindTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
            //meow.Start();
            //BindingList<ScanStructure> test = new BindingList<ScanStructure>(meow.ScanResult);
            //dataGridView1.DataSource = test;
            //AddressFoundLabel.Text = meow.ScanResult.Count().ToString();


            firstTry.ScanSystem();
            firstTry.CreateEntryPoints();


            textboxContent = MemoryConverter.TextBoxContentAsByteArray(ValueToFindTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
            size = textboxContent.Length;
            basicValue = ValueToFindTextBox.Text;


            firstTry.SearchForValuesInMultipleThreads(size, textboxContent);
            firstTry.CreateDataGridSource1(ValueToFindTextBox.Text);
            source.DataSource = firstTry.dataGridSource;
            dataGridView1.DataSource = source;


            dataGridView1.Columns[2].Visible = false;
            NextScanButton.Enabled = true;
            ResetButton.Enabled = true;
            AddressFoundLabel.Visible = true;
            AddressFoundLabel.Text = firstTry.dataGridSource.Count.ToString();
            FirstScanButton.Enabled = false;
            InputTypeComboBox.Enabled = false;


            Collector.RunWorkerAsync();
        }

        private void NextScan_Click(object sender, EventArgs e)
        {
            textboxContent = MemoryConverter.TextBoxContentAsByteArray(ValueToFindTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
            size = textboxContent.Length;


            firstTry.CompareLists(size, textboxContent);
            firstTry.CompareDataGridSource(ValueToFindTextBox.Text, basicValue);
            source.DataSource = firstTry.dataGridSource;
            basicValue = ValueToFindTextBox.Text;
            AutoRefreshcheckBox.Enabled = true;
            dataGridView1.Columns[2].Visible = true;
            AddressFoundLabel.Text = firstTry.memoryMemory.Count.ToString();

            //foolin around
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key;
            if (size == 42)
            {
                isString = true;
            }
        }


        private void Schreiben_Click(object sender, EventArgs e)
        {
            {
                byte[] writeboxContent = MemoryConverter.TextBoxContentAsByteArray(WriteValueTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
                IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
                firstTry.TestWrite(test, writeboxContent, writeboxContent.Length);
            }
        }

        private void AddresstextBox_Click(object sender, EventArgs e)
        {
            WriteAddressTextBox.SelectAll();
        }

        private void Refresher_DoWork(object sender, DoWorkEventArgs e)
        {
            while (AutoRefreshcheckBox.Checked == true)
            {
                firstTry.RefreshSource(basicValue, size, isString);
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
            byte[] writeboxContent = MemoryConverter.TextBoxContentAsByteArray(WriteValueTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
            IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
            while (Freeze.Checked == true)
            {
                schreiben.Invoke((Action)(() => schreiben.Enabled = false));
                firstTry.TestWrite(test, writeboxContent, writeboxContent.Length);
                Thread.Sleep(100);
            }
            schreiben.Invoke((Action)(() => schreiben.Enabled = true));
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            firstTry.Reset();
            FirstScanButton.Enabled = true;
            InputTypeComboBox.Enabled = true;
        }

        private void AutoRefreshcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoRefreshcheckBox.Checked == true)
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
            AttachProcess attach_Process = new AttachProcess();

            if (attach_Process.ShowDialog() == DialogResult.OK)
            {
                //meow = new MemoryEditorV2(attach_Process.GetProcessId());
                firstTry.NewProcess(attach_Process.GetProcessId());
                FirstScanButton.Enabled = true;
                ValueToFindTextBox.Enabled = true;
            }
            attach_Process.Close();
        }

        private void Collector_DoWork(object sender, DoWorkEventArgs e)
        {
            GC.Collect();
        }
    }
}
