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
        byte[] textboxContent ;
        long basicValue;


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
            Dictionary<int, string> ValueSizeAndName = new Dictionary<int, string> { { 1, "Byte" }, { 2, "Short" }, { 4, "Int" }, { 8, "Long" } ,{42,"String" } };                
            //ValueSizeAndName.Add(1, "Byte");
            //ValueSizeAndName.Add(2, "Short");
            //ValueSizeAndName.Add(4, "Int");
            //ValueSizeAndName.Add(8, "Long");

            InputTypeComboBox.DataSource = new BindingSource(ValueSizeAndName,null);
            InputTypeComboBox.DisplayMember = "Value";
            InputTypeComboBox.ValueMember = "Key";

        }

        private void FirstScan(object sender, EventArgs e)
        {
            firstTry.ScanSystem();
            firstTry.CreateEntryPoints();

            firstTry.SearchForValues(size, textboxContent);
            firstTry.CreateDataGridSource1(42/*textboxContent*/);

            source.DataSource = firstTry.dataGridSource;
            //source.DataSource = firstTry.CreateDataGridSource1(textboxContent);
            dataGridView1.DataSource = source;
            dataGridView1.Columns[2].Visible = false;
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
            NextScanButton.Enabled = true;
            ResetButton.Enabled = true;
            AddressFoundLabel.Visible = true;
            basicValue = BitConverter.ToInt64(textboxContent,0); //ToDo: fehler bei string
            AddressFoundLabel.Text = firstTry.memoryMemory.Count.ToString();
            FirstScanButton.Enabled = false;
        }
       
        private void NextScan(object sender, EventArgs e)
        {
            dataGridView1.Columns[2].Visible = true;
            firstTry.CompareLists(size, BitConverter.ToInt64( textboxContent,0));
            firstTry.CompareDataGridSource(BitConverter.ToInt64( textboxContent,0), basicValue);
            source.DataSource = firstTry.dataGridSource;
            basicValue = BitConverter.ToInt64( textboxContent,0); //ToDo: fehler

            //dataGridView1.DataSource = source;
            //listBox1.DataSource = firstTry.memoryMemory.ConvertAll(delegate (IntPtr i) { return i.ToString("X8"); });
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            size = ((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key;
            if (size==42)
            {
                size = ValueToFindTextBox.Text.Length;
            }
        }

        private void ValueToFindTextBox_TextChanged(object sender, EventArgs e)
        {

            switch (((KeyValuePair<int, string>)InputTypeComboBox.SelectedItem).Key)
            {
                case 1:
                    if (byte.TryParse(ValueToFindTextBox.Text, out byte result1))
                    {
                        textboxContent = new byte[1] { result1 };
                    }
                    break;
                case 2:
                    if (short.TryParse(ValueToFindTextBox.Text, out short result2))
                    {
                        textboxContent = new byte[2] { (byte)(result2 & 255), (byte)((result2 >> 8) & 255) };
                    }
                    break;
                case 4:
                    if (int.TryParse(ValueToFindTextBox.Text, out int result4))
                    {
                        textboxContent = new byte[4] { (byte)(result4 & 255)
                            , (byte)((result4 >> 8) & 255)
                            , (byte)((result4 >> 16) & 255)
                            , (byte)((result4 >> 24) & 255) };
                    }
                    break;
                case 8:
                    if (long.TryParse(ValueToFindTextBox.Text, out long result8))
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
                    textboxContent = new byte[ValueToFindTextBox.Text.Length];
                    char[] arsch = ValueToFindTextBox.Text.ToCharArray();

                    for (int i = 0; i < arsch.Length; i++)
                    {
                        textboxContent[i] = (byte)arsch[i];
                    }

                    break;
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
            AttachProcess attach_Process = new AttachProcess();

            if (attach_Process.ShowDialog() == DialogResult.OK)
            {
                firstTry.NewProcess( attach_Process.GetProcessId() );
                FirstScanButton.Enabled = true;
            }

            attach_Process.Close();
        }
    }
}
