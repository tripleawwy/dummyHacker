namespace dummyHacker
{
    partial class AttachProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProcessesFoundLabel = new System.Windows.Forms.Label();
            this.AcceptProcessButton = new System.Windows.Forms.Button();
            this.ProcessListView = new System.Windows.Forms.ListView();
            this.processname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.processid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ProcessesFoundLabel
            // 
            this.ProcessesFoundLabel.AutoSize = true;
            this.ProcessesFoundLabel.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessesFoundLabel.Location = new System.Drawing.Point(6, 19);
            this.ProcessesFoundLabel.Name = "ProcessesFoundLabel";
            this.ProcessesFoundLabel.Size = new System.Drawing.Size(62, 17);
            this.ProcessesFoundLabel.TabIndex = 1;
            this.ProcessesFoundLabel.Text = "Processes";
            // 
            // AcceptProcessButton
            // 
            this.AcceptProcessButton.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AcceptProcessButton.Location = new System.Drawing.Point(89, 311);
            this.AcceptProcessButton.Name = "AcceptProcessButton";
            this.AcceptProcessButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptProcessButton.TabIndex = 2;
            this.AcceptProcessButton.Text = "OK";
            this.AcceptProcessButton.UseVisualStyleBackColor = true;
            // 
            // ProcessListView
            // 
            this.ProcessListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.processname,
            this.processid});
            this.ProcessListView.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessListView.FullRowSelect = true;
            this.ProcessListView.Location = new System.Drawing.Point(9, 39);
            this.ProcessListView.MultiSelect = false;
            this.ProcessListView.Name = "ProcessListView";
            this.ProcessListView.Size = new System.Drawing.Size(235, 251);
            this.ProcessListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ProcessListView.TabIndex = 3;
            this.ProcessListView.UseCompatibleStateImageBehavior = false;
            this.ProcessListView.View = System.Windows.Forms.View.Details;
            // 
            // processname
            // 
            this.processname.Text = "processname";
            this.processname.Width = 120;
            // 
            // processid
            // 
            this.processid.Text = "processid";
            this.processid.Width = 85;
            // 
            // AttachProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 346);
            this.Controls.Add(this.ProcessListView);
            this.Controls.Add(this.AcceptProcessButton);
            this.Controls.Add(this.ProcessesFoundLabel);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Name = "AttachProcess";
            this.Text = "AttachProcess";
            this.Load += new System.EventHandler(this.Attach_Process_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ProcessesFoundLabel;
        private System.Windows.Forms.Button AcceptProcessButton;
        private System.Windows.Forms.ListView ProcessListView;
        private System.Windows.Forms.ColumnHeader processname;
        private System.Windows.Forms.ColumnHeader processid;
    }
}