﻿namespace dummyHacker
{
    partial class Form1
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
            this.ScanNumberOne = new System.Windows.Forms.Button();
            this.FurtherScans = new System.Windows.Forms.Button();
            this.InputTypeComboBox = new System.Windows.Forms.ComboBox();
            this.ValueToFindTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.schreiben = new System.Windows.Forms.Button();
            this.WriteAddressTextBox = new System.Windows.Forms.TextBox();
            this.WriteValueTextBox = new System.Windows.Forms.TextBox();
            this.WriteAddressLabel = new System.Windows.Forms.Label();
            this.WriteValueLabel = new System.Windows.Forms.Label();
            this.Open = new System.Windows.Forms.Button();
            this.dataGridRefresher = new System.ComponentModel.BackgroundWorker();
            this.Freeze = new System.Windows.Forms.CheckBox();
            this.Freezer = new System.ComponentModel.BackgroundWorker();
            this.ResetButton = new System.Windows.Forms.Button();
            this.AutoRefreshcheckBox = new System.Windows.Forms.CheckBox();
            this.AddressFoundLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ScanNumberOne
            // 
            this.ScanNumberOne.Enabled = false;
            this.ScanNumberOne.Location = new System.Drawing.Point(616, 45);
            this.ScanNumberOne.Name = "ScanNumberOne";
            this.ScanNumberOne.Size = new System.Drawing.Size(75, 23);
            this.ScanNumberOne.TabIndex = 1;
            this.ScanNumberOne.Text = "First Scan";
            this.ScanNumberOne.UseVisualStyleBackColor = true;
            this.ScanNumberOne.Click += new System.EventHandler(this.FirstScan);
            // 
            // FurtherScans
            // 
            this.FurtherScans.Enabled = false;
            this.FurtherScans.Location = new System.Drawing.Point(697, 45);
            this.FurtherScans.Name = "FurtherScans";
            this.FurtherScans.Size = new System.Drawing.Size(75, 23);
            this.FurtherScans.TabIndex = 5;
            this.FurtherScans.Text = "Next Scan";
            this.FurtherScans.UseVisualStyleBackColor = true;
            this.FurtherScans.Click += new System.EventHandler(this.NextScan);
            // 
            // InputTypeComboBox
            // 
            this.InputTypeComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.InputTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputTypeComboBox.FormattingEnabled = true;
            this.InputTypeComboBox.Items.AddRange(new object[] {
            "asd"});
            this.InputTypeComboBox.Location = new System.Drawing.Point(339, 47);
            this.InputTypeComboBox.Name = "InputTypeComboBox";
            this.InputTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.InputTypeComboBox.TabIndex = 6;
            this.InputTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // ValueToFindTextBox
            // 
            this.ValueToFindTextBox.Location = new System.Drawing.Point(494, 47);
            this.ValueToFindTextBox.Name = "ValueToFindTextBox";
            this.ValueToFindTextBox.Size = new System.Drawing.Size(100, 20);
            this.ValueToFindTextBox.TabIndex = 7;
            this.ValueToFindTextBox.Click += new System.EventHandler(this.ValueToFindtextBox_Click);
            this.ValueToFindTextBox.TextChanged += new System.EventHandler(this.ValueToFindTextBox_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(405, 292);
            this.dataGridView1.TabIndex = 9;
            // 
            // schreiben
            // 
            this.schreiben.Location = new System.Drawing.Point(697, 337);
            this.schreiben.Name = "schreiben";
            this.schreiben.Size = new System.Drawing.Size(75, 23);
            this.schreiben.TabIndex = 11;
            this.schreiben.Text = "schreiben";
            this.schreiben.UseVisualStyleBackColor = true;
            this.schreiben.Click += new System.EventHandler(this.Schreiben_Click);
            // 
            // WriteAddressTextBox
            // 
            this.WriteAddressTextBox.Location = new System.Drawing.Point(591, 339);
            this.WriteAddressTextBox.Name = "WriteAddressTextBox";
            this.WriteAddressTextBox.Size = new System.Drawing.Size(100, 20);
            this.WriteAddressTextBox.TabIndex = 12;
            this.WriteAddressTextBox.Click += new System.EventHandler(this.AddresstextBox_Click);
            // 
            // WriteValueTextBox
            // 
            this.WriteValueTextBox.Location = new System.Drawing.Point(591, 366);
            this.WriteValueTextBox.Name = "WriteValueTextBox";
            this.WriteValueTextBox.Size = new System.Drawing.Size(100, 20);
            this.WriteValueTextBox.TabIndex = 13;
            // 
            // WriteAddressLabel
            // 
            this.WriteAddressLabel.AutoSize = true;
            this.WriteAddressLabel.Location = new System.Drawing.Point(522, 342);
            this.WriteAddressLabel.Name = "WriteAddressLabel";
            this.WriteAddressLabel.Size = new System.Drawing.Size(45, 13);
            this.WriteAddressLabel.TabIndex = 14;
            this.WriteAddressLabel.Text = "Address";
            // 
            // WriteValueLabel
            // 
            this.WriteValueLabel.AutoSize = true;
            this.WriteValueLabel.Location = new System.Drawing.Point(532, 369);
            this.WriteValueLabel.Name = "WriteValueLabel";
            this.WriteValueLabel.Size = new System.Drawing.Size(34, 13);
            this.WriteValueLabel.TabIndex = 15;
            this.WriteValueLabel.Text = "Value";
            // 
            // Open
            // 
            this.Open.Location = new System.Drawing.Point(12, 12);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(75, 23);
            this.Open.TabIndex = 16;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            // 
            // dataGridRefresher
            // 
            this.dataGridRefresher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Refresher_DoWork);
            // 
            // Freeze
            // 
            this.Freeze.AutoSize = true;
            this.Freeze.Location = new System.Drawing.Point(697, 369);
            this.Freeze.Name = "Freeze";
            this.Freeze.Size = new System.Drawing.Size(58, 17);
            this.Freeze.TabIndex = 17;
            this.Freeze.Text = "Freeze";
            this.Freeze.UseVisualStyleBackColor = true;
            this.Freeze.CheckedChanged += new System.EventHandler(this.Freeze_CheckedChanged);
            // 
            // Freezer
            // 
            this.Freezer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Freezer_DoWork);
            // 
            // ResetButton
            // 
            this.ResetButton.Enabled = false;
            this.ResetButton.Location = new System.Drawing.Point(616, 75);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 18;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // AutoRefreshcheckBox
            // 
            this.AutoRefreshcheckBox.AutoSize = true;
            this.AutoRefreshcheckBox.Location = new System.Drawing.Point(12, 83);
            this.AutoRefreshcheckBox.Name = "AutoRefreshcheckBox";
            this.AutoRefreshcheckBox.Size = new System.Drawing.Size(85, 17);
            this.AutoRefreshcheckBox.TabIndex = 19;
            this.AutoRefreshcheckBox.Text = "AutoRefresh";
            this.AutoRefreshcheckBox.UseVisualStyleBackColor = true;
            this.AutoRefreshcheckBox.CheckedChanged += new System.EventHandler(this.AutoRefreshcheckBox_CheckedChanged);
            // 
            // AddressFoundLabel
            // 
            this.AddressFoundLabel.AutoSize = true;
            this.AddressFoundLabel.Location = new System.Drawing.Point(302, 83);
            this.AddressFoundLabel.Name = "AddressFoundLabel";
            this.AddressFoundLabel.Size = new System.Drawing.Size(73, 13);
            this.AddressFoundLabel.TabIndex = 20;
            this.AddressFoundLabel.Text = "AddressCount";
            this.AddressFoundLabel.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.AddressFoundLabel);
            this.Controls.Add(this.AutoRefreshcheckBox);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.Freeze);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.WriteValueLabel);
            this.Controls.Add(this.WriteAddressLabel);
            this.Controls.Add(this.WriteValueTextBox);
            this.Controls.Add(this.WriteAddressTextBox);
            this.Controls.Add(this.schreiben);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ValueToFindTextBox);
            this.Controls.Add(this.InputTypeComboBox);
            this.Controls.Add(this.FurtherScans);
            this.Controls.Add(this.ScanNumberOne);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ScanNumberOne;
        private System.Windows.Forms.Button FurtherScans;
        private System.Windows.Forms.ComboBox InputTypeComboBox;
        private System.Windows.Forms.TextBox ValueToFindTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button schreiben;
        private System.Windows.Forms.TextBox WriteAddressTextBox;
        private System.Windows.Forms.TextBox WriteValueTextBox;
        private System.Windows.Forms.Label WriteAddressLabel;
        private System.Windows.Forms.Label WriteValueLabel;
        private System.Windows.Forms.Button Open;
        private System.ComponentModel.BackgroundWorker dataGridRefresher;
        private System.Windows.Forms.CheckBox Freeze;
        private System.ComponentModel.BackgroundWorker Freezer;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.CheckBox AutoRefreshcheckBox;
        private System.Windows.Forms.Label AddressFoundLabel;
    }
}