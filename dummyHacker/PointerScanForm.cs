using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dummyHacker
{
    public partial class PointerScanForm : Form
    {

        MemoryEditorV2 pointer;
        public PointerScanForm(MemoryEditorV2 editorV2)
        {
            pointer = editorV2;
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


        private void Scanner_DoWork(object sender, DoWorkEventArgs e)
        {
            //for (int i = 0; i < pointer.PointerOutput.Count(); i++)
            //{
            //    PointerScanView.Invoke((Action)(() => PointerScanView.Rows.Insert(i, pointer.PointerOutput.ElementAt(i))));
            //}
        }


        private void PointerScanForm_Shown(object sender, EventArgs e)
        {
            pointer.Start2();
            GC.Collect();
            for (int i = 0; i < pointer.PointerOutput.Count(); i++)
            {
                PointerScanView.Rows.Insert(i, pointer.PointerOutput.ElementAt(i));
            }
        }

        private void PointerScanView_DoubleClick(object sender, EventArgs e)
        {
            pointer.CurrentAddress = BitConverter.GetBytes(int.Parse(PointerScanView.CurrentRow.Cells[0].Value.ToString(), System.Globalization.NumberStyles.HexNumber));
            pointer.Start2();
            GC.Collect();
            for (int i = 0; i < pointer.PointerOutput.Count(); i++)
            {
                PointerScanView.Rows.Insert(i, pointer.PointerOutput.ElementAt(i));
            }
        }
    }
}
