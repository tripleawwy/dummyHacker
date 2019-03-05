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
        BindingSource source = new BindingSource();
        byte[] textboxContent;



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
            textboxContent = MemoryConverter.TextBoxContentToByteArray(ValueToFindTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
            meow.Value = textboxContent;
            CheckForString();
            meow.Start();


            source.DataSource = meow.Output;
            dataGridView1.DataSource = source;


            AddressFoundLabel.Text = meow.Output.Count().ToString();
            NextScanButton.Enabled = true;
            ResetButton.Enabled = true;
            AddressFoundLabel.Visible = true;
            FirstScanButton.Enabled = false;
            InputTypeComboBox.Enabled = false;


            Collector.RunWorkerAsync();
        }

        private void CheckForString()
        {
            if (((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key == 42)
            {
                meow.IsString = true;
            }
            else
            {
                meow.IsString = false;
            }
        }

        private void NextScan_Click(object sender, EventArgs e)
        {
            textboxContent = MemoryConverter.TextBoxContentToByteArray(ValueToFindTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
            meow.Value = textboxContent;
            meow.CompareLists();
            
            

            source.DataSource = meow.Output;
            dataGridView1.DataSource = source;


            AddressFoundLabel.Text = meow.Output.Count().ToString();
            AutoRefreshcheckBox.Enabled = true;


            //foolin around
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
        }

        private void Schreiben_Click(object sender, EventArgs e)
        {
            {
                byte[] writeboxContent = MemoryConverter.TextBoxContentToByteArray(WriteValueTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key);
                IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
                meow.TestWrite(test, writeboxContent);
            }
        }

        private void AddresstextBox_Click(object sender, EventArgs e)
        {
            WriteAddressTextBox.SelectAll();
        }

        private void Refresher_DoWork(object sender, DoWorkEventArgs e)
        {
            int backup;
            while (AutoRefreshcheckBox.Checked == true)
            {
                backup = backup = dataGridView1.FirstDisplayedCell.RowIndex;
                meow.RefreshList();
                dataGridView1.Invoke((Action)(() => source.DataSource = meow.Output));
                dataGridView1.Invoke((Action)(() => dataGridView1.FirstDisplayedScrollingRowIndex = backup));
                Thread.Sleep(500);
            }
        }

        private void ValueToFindtextBox_Click(object sender, EventArgs e)
        {
            ValueToFindTextBox.SelectAll();
        }


        //TODO richtig machen(is noch irgendwie unsauber)
        private void Freezer_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] writeboxContent = new byte[0];
            WriteValueTextBox.Invoke((Action)(() => writeboxContent = MemoryConverter.TextBoxContentToByteArray(WriteValueTextBox.Text, ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key)));
            IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
            while (Freeze.Checked == true)
            {
                schreiben.Invoke((Action)(() => schreiben.Enabled = false));
                meow.TestWrite(test, writeboxContent);
                Thread.Sleep(100);
            }
            schreiben.Invoke((Action)(() => schreiben.Enabled = true));
        }


        private void ResetButton_Click(object sender, EventArgs e)
        {
            meow.Reset();
            source.DataSource = meow.Output;
            dataGridView1.DataSource = source;
            AddressFoundLabel.Visible = false;
            FirstScanButton.Enabled = true;
            InputTypeComboBox.Enabled = true;
        }

        private void AutoRefreshcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoRefreshcheckBox.Checked == true)
            {
                ResetButton.Enabled = false;
                dataGridRefresher.RunWorkerAsync();
            }
            if (AutoRefreshcheckBox.Checked==false)
            {
                ResetButton.Enabled = true;
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
                meow = new MemoryEditorV2(attach_Process.GetProcessId());
                FirstScanButton.Enabled = true;
                ValueToFindTextBox.Enabled = true;
                InputTypeComboBox.Enabled = true;
            }
            attach_Process.Close();
        }

        private void Collector_DoWork(object sender, DoWorkEventArgs e)
        {
            GC.Collect();
        }
    }
}
