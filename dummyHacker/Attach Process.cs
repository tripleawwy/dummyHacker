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
    public partial class Attach_Process : Form
    {
        public Attach_Process()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void Attach_Process_Load(object sender, EventArgs e)
        {

        }

        private void LoadProcesses()
        {
            Dictionary<int, string> ProcessIdAndName = new Dictionary<int, string>();

            foreach (Process activeProcess in Process.GetProcesses())
            {
                ProcessIdAndName.Add(activeProcess.Id, activeProcess.ProcessName);
            }
            ProcessListBox.DataSource = new BindingSource(ProcessIdAndName, null);
            ProcessListBox.DisplayMember = "Value";
            ProcessListBox.ValueMember = "Key";
        }
    }
}
