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
            LoadProcesses();
        }

        private void Attach_Process_Load(object sender, EventArgs e)
        {

        }

        private void LoadProcesses()
        {
            foreach (Process activeProcess in Process.GetProcesses())
            {
                ListViewItem item = new ListViewItem(activeProcess.ProcessName);
                item.SubItems.Add(activeProcess.Id.ToString());
                ProcessListView.Items.Add(item);
            }
        }
    }
}
