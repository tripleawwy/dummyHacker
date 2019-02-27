namespace dummyHacker
{
    partial class Hauptprogramm
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
            this.FirstScanButton = new System.Windows.Forms.Button();
            this.NextScanButton = new System.Windows.Forms.Button();
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
            this.Collector = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // FirstScanButton
            // 
            this.FirstScanButton.Enabled = false;
            this.FirstScanButton.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.FirstScanButton.Location = new System.Drawing.Point(616, 45);
            this.FirstScanButton.Name = "FirstScanButton";
            this.FirstScanButton.Size = new System.Drawing.Size(75, 23);
            this.FirstScanButton.TabIndex = 1;
            this.FirstScanButton.Text = "First Scan";
            this.FirstScanButton.UseVisualStyleBackColor = true;
            this.FirstScanButton.Click += new System.EventHandler(this.FirstScan_Click);
            // 
            // NextScanButton
            // 
            this.NextScanButton.Enabled = false;
            this.NextScanButton.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.NextScanButton.Location = new System.Drawing.Point(697, 45);
            this.NextScanButton.Name = "NextScanButton";
            this.NextScanButton.Size = new System.Drawing.Size(75, 23);
            this.NextScanButton.TabIndex = 5;
            this.NextScanButton.Text = "Next Scan";
            this.NextScanButton.UseVisualStyleBackColor = true;
            this.NextScanButton.Click += new System.EventHandler(this.NextScan_Click);
            // 
            // InputTypeComboBox
            // 
            this.InputTypeComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.InputTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.InputTypeComboBox.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.InputTypeComboBox.FormattingEnabled = true;
           
            this.InputTypeComboBox.Location = new System.Drawing.Point(339, 47);
            this.InputTypeComboBox.Name = "InputTypeComboBox";
            this.InputTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.InputTypeComboBox.TabIndex = 6;
            this.InputTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // ValueToFindTextBox
            // 
            this.ValueToFindTextBox.Enabled = false;
            this.ValueToFindTextBox.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.ValueToFindTextBox.Location = new System.Drawing.Point(494, 47);
            this.ValueToFindTextBox.Name = "ValueToFindTextBox";
            this.ValueToFindTextBox.Size = new System.Drawing.Size(100, 21);
            this.ValueToFindTextBox.TabIndex = 7;
            this.ValueToFindTextBox.Click += new System.EventHandler(this.ValueToFindtextBox_Click);
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
            this.schreiben.Font = new System.Drawing.Font("Calibri", 8.25F);
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
            this.WriteAddressTextBox.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.WriteAddressTextBox.Location = new System.Drawing.Point(591, 339);
            this.WriteAddressTextBox.Name = "WriteAddressTextBox";
            this.WriteAddressTextBox.Size = new System.Drawing.Size(100, 21);
            this.WriteAddressTextBox.TabIndex = 12;
            this.WriteAddressTextBox.Click += new System.EventHandler(this.AddresstextBox_Click);
            // 
            // WriteValueTextBox
            // 
            this.WriteValueTextBox.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.WriteValueTextBox.Location = new System.Drawing.Point(591, 366);
            this.WriteValueTextBox.Name = "WriteValueTextBox";
            this.WriteValueTextBox.Size = new System.Drawing.Size(100, 21);
            this.WriteValueTextBox.TabIndex = 13;
            // 
            // WriteAddressLabel
            // 
            this.WriteAddressLabel.AutoSize = true;
            this.WriteAddressLabel.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.WriteAddressLabel.Location = new System.Drawing.Point(522, 342);
            this.WriteAddressLabel.Name = "WriteAddressLabel";
            this.WriteAddressLabel.Size = new System.Drawing.Size(45, 13);
            this.WriteAddressLabel.TabIndex = 14;
            this.WriteAddressLabel.Text = "Address";
            // 
            // WriteValueLabel
            // 
            this.WriteValueLabel.AutoSize = true;
            this.WriteValueLabel.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.WriteValueLabel.Location = new System.Drawing.Point(532, 369);
            this.WriteValueLabel.Name = "WriteValueLabel";
            this.WriteValueLabel.Size = new System.Drawing.Size(33, 13);
            this.WriteValueLabel.TabIndex = 15;
            this.WriteValueLabel.Text = "Value";
            // 
            // Open
            // 
            this.Open.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Open.Location = new System.Drawing.Point(12, 12);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(75, 23);
            this.Open.TabIndex = 16;
            this.Open.Text = "Open";
            this.Open.UseVisualStyleBackColor = true;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // dataGridRefresher
            // 
            this.dataGridRefresher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Refresher_DoWork);
            // 
            // Freeze
            // 
            this.Freeze.AutoSize = true;
            this.Freeze.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Freeze.Location = new System.Drawing.Point(697, 369);
            this.Freeze.Name = "Freeze";
            this.Freeze.Size = new System.Drawing.Size(57, 17);
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
            this.ResetButton.Font = new System.Drawing.Font("Calibri", 8.25F);
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
            this.AutoRefreshcheckBox.Enabled = false;
            this.AutoRefreshcheckBox.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.AutoRefreshcheckBox.Location = new System.Drawing.Point(12, 83);
            this.AutoRefreshcheckBox.Name = "AutoRefreshcheckBox";
            this.AutoRefreshcheckBox.Size = new System.Drawing.Size(84, 17);
            this.AutoRefreshcheckBox.TabIndex = 19;
            this.AutoRefreshcheckBox.Text = "AutoRefresh";
            this.AutoRefreshcheckBox.UseVisualStyleBackColor = true;
            this.AutoRefreshcheckBox.CheckedChanged += new System.EventHandler(this.AutoRefreshcheckBox_CheckedChanged);
            // 
            // AddressFoundLabel
            // 
            this.AddressFoundLabel.AutoSize = true;
            this.AddressFoundLabel.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.AddressFoundLabel.Location = new System.Drawing.Point(344, 84);
            this.AddressFoundLabel.Name = "AddressFoundLabel";
            this.AddressFoundLabel.Size = new System.Drawing.Size(73, 13);
            this.AddressFoundLabel.TabIndex = 20;
            this.AddressFoundLabel.Text = "AddressCount";
            this.AddressFoundLabel.Visible = false;
            // 
            // Collector
            // 
            this.Collector.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Collector_DoWork);
            // 
            // Hauptprogramm
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
            this.Controls.Add(this.NextScanButton);
            this.Controls.Add(this.FirstScanButton);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.Name = "Hauptprogramm";
            this.Text = "Hauptprogramm";
            this.Load += new System.EventHandler(this.HauptProgramm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button FirstScanButton;
        private System.Windows.Forms.Button NextScanButton;
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
        private System.ComponentModel.BackgroundWorker Collector;
    }
}