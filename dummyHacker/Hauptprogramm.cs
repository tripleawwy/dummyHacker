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

        MemoryEditor firstTry = new MemoryEditor();
        BindingSource source = new BindingSource();
        int size;
        byte[] textboxContent;
        string penner;
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
            Dictionary<int, string> ValueSizeAndName = new Dictionary<int, string> { { 1, "Byte" }, { 2, "Short" }, { 4, "Int" }, { 8, "Long" }, { 42, "String" } };
            InputTypeComboBox.DataSource = new BindingSource(ValueSizeAndName, null);
            InputTypeComboBox.DisplayMember = "Value";
            InputTypeComboBox.ValueMember = "Key";

        }

        private void FirstScan(object sender, EventArgs e)
        {
            firstTry.ScanSystem();
            firstTry.CreateEntryPoints();
            textboxContent = TextBoxContentAsByteArray(ValueToFindTextBox.Text);
            firstTry.SearchForValuesInMultipleThreads(size, textboxContent);
            firstTry.CreateDataGridSource1(penner);

            source.DataSource = firstTry.dataGridSource;
            //source.DataSource = firstTry.CreateDataGridSource1(textboxContent);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[2].Visible = false;
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
            NextScanButton.Enabled = true;
            ResetButton.Enabled = true;
            AddressFoundLabel.Visible = true;
            basicValue = penner;
            AddressFoundLabel.Text = firstTry.memoryMemory.Count.ToString();
            FirstScanButton.Enabled = false;
            InputTypeComboBox.Enabled = false;
        }

        private void NextScan(object sender, EventArgs e)
        {
            TextBoxContentAsByteArray(ValueToFindTextBox.Text);
            firstTry.CompareLists(size, TextBoxContentAsByteArray(ValueToFindTextBox.Text));
            firstTry.CompareDataGridSource(penner, basicValue);
            source.DataSource = firstTry.dataGridSource;
            basicValue = penner;
            AutoRefreshcheckBox.Enabled = true;
            dataGridView1.Columns[2].Visible = true;
            AddressFoundLabel.Text = firstTry.memoryMemory.Count.ToString();
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

        private byte[] TextBoxContentAsByteArray(string textboxtext)
        {
            byte[] textboxContent = new byte[0];
            penner = textboxtext;
            switch (((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key)
            {
                case 1:
                    if (byte.TryParse(textboxtext, out byte result1))
                    {
                        textboxContent = new byte[1] { result1 };
                    }
                    break;
                case 2:
                    if (short.TryParse(textboxtext, out short result2))
                    {
                        textboxContent = new byte[2] { (byte)(result2 & 255), (byte)((result2 >> 8) & 255) };
                    }
                    break;
                case 4:
                    if (int.TryParse(textboxtext, out int result4))
                    {
                        textboxContent = new byte[4] { (byte)(result4 & 255)
                            , (byte)((result4 >> 8) & 255)
                            , (byte)((result4 >> 16) & 255)
                            , (byte)((result4 >> 24) & 255) };
                    }
                    break;
                case 8:
                    if (long.TryParse(textboxtext, out long result8))
                    {
                        textboxContent = new byte[8] { (byte)(result8 & 255)
                            , (byte)((result8 >> 8) & 255)
                            , (byte)((result8 >> 16) & 255)
                            , (byte)((result8 >> 24) & 255)
                            , (byte)((result8 >> 32) & 255)
                            , (byte)((result8 >> 40) & 255)
                            , (byte)((result8 >> 48) & 255)
                            , (byte)((result8 >> 56) & 255)};
                    }
                    break;
                default:
                    size = textboxtext.Length;
                    textboxContent = new byte[textboxtext.Length];
                    byte[] arsch = System.Text.Encoding.Default.GetBytes(textboxtext);

                    for (int i = 0; i < arsch.Length; i++)
                    {
                        textboxContent[i] = arsch[i];
                    }
                    break;
            }
            return textboxContent;
        }

        private void Schreiben_Click(object sender, EventArgs e)
        {
            {
                IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
                firstTry.TestWrite(test, TextBoxContentAsByteArray(WriteValueTextBox.Text),size);
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
            IntPtr test = new IntPtr(int.Parse(WriteAddressTextBox.Text, System.Globalization.NumberStyles.HexNumber));
            int test2 = int.Parse(WriteValueTextBox.Text);
            while (Freeze.Checked == true)
            {
                schreiben.Invoke((Action)(() => schreiben.Enabled = false));
                firstTry.TestWrite(test, TextBoxContentAsByteArray(WriteValueTextBox.Text), size);
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
                firstTry.NewProcess(attach_Process.GetProcessId());
                FirstScanButton.Enabled = true;
                ValueToFindTextBox.Enabled = true;
            }
            attach_Process.Close();
        }
    }
}
