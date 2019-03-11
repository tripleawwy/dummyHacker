using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace dummyHacker
{
    public partial class AttachProcess : Form
    {
        public AttachProcess()
        {
            InitializeComponent();
        }

        private void Attach_Process_Load(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            foreach (Process activeProcess in Process.GetProcesses())
            {
                if (!String.IsNullOrEmpty(activeProcess.MainWindowTitle))
                {
                    ListViewItem item = new ListViewItem(activeProcess.MainWindowTitle);
                    item.SubItems.Add(activeProcess.Id.ToString());
                    ProcessListView.Items.Add(item);
                }
            }
        }

        public int GetProcessId()
        {           
            return int.Parse(ProcessListView.SelectedItems[0].SubItems[1].Text);
        }

        private void AcceptProcessButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
