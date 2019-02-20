namespace dummyHacker
{
    partial class Attach_Process
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
            this.ProcessListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ProcessesFoundLabel
            // 
            this.ProcessesFoundLabel.AutoSize = true;
            this.ProcessesFoundLabel.Location = new System.Drawing.Point(6, 23);
            this.ProcessesFoundLabel.Name = "ProcessesFoundLabel";
            this.ProcessesFoundLabel.Size = new System.Drawing.Size(56, 13);
            this.ProcessesFoundLabel.TabIndex = 1;
            this.ProcessesFoundLabel.Text = "Processes";
            // 
            // AcceptProcessButton
            // 
            this.AcceptProcessButton.Location = new System.Drawing.Point(84, 286);
            this.AcceptProcessButton.Name = "AcceptProcessButton";
            this.AcceptProcessButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptProcessButton.TabIndex = 2;
            this.AcceptProcessButton.Text = "OK";
            this.AcceptProcessButton.UseVisualStyleBackColor = true;
            // 
            // ProcessListBox
            // 
            this.ProcessListBox.FormattingEnabled = true;
            this.ProcessListBox.Location = new System.Drawing.Point(9, 39);
            this.ProcessListBox.MultiColumn = true;
            this.ProcessListBox.Name = "ProcessListBox";
            this.ProcessListBox.Size = new System.Drawing.Size(215, 238);
            this.ProcessListBox.TabIndex = 3;
            // 
            // Attach_Process
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 337);
            this.Controls.Add(this.ProcessListBox);
            this.Controls.Add(this.AcceptProcessButton);
            this.Controls.Add(this.ProcessesFoundLabel);
            this.Name = "Attach_Process";
            this.Text = "Attach_Process";
            this.Load += new System.EventHandler(this.Attach_Process_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ProcessesFoundLabel;
        private System.Windows.Forms.Button AcceptProcessButton;
        private System.Windows.Forms.ListBox ProcessListBox;
    }
}