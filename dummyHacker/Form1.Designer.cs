namespace dummyHacker
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
            this.button1 = new System.Windows.Forms.Button();
            this.nextScan = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.erneuern = new System.Windows.Forms.Button();
            this.schreiben = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Open = new System.Windows.Forms.Button();
            this.dataGridRefresher = new System.ComponentModel.BackgroundWorker();
            this.Freeze = new System.Windows.Forms.CheckBox();
            this.Freezer = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(616, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "First Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.firstScan);
            // 
            // nextScan
            // 
            this.nextScan.Enabled = false;
            this.nextScan.Location = new System.Drawing.Point(697, 45);
            this.nextScan.Name = "nextScan";
            this.nextScan.Size = new System.Drawing.Size(75, 23);
            this.nextScan.TabIndex = 5;
            this.nextScan.Text = "Next Scan";
            this.nextScan.UseVisualStyleBackColor = true;
            this.nextScan.Click += new System.EventHandler(this.nextScan_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Byte",
            "Short",
            "Int",
            "Long"});
            this.comboBox1.Location = new System.Drawing.Point(339, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(494, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Click += new System.EventHandler(this.textBox1_Click);
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(405, 292);
            this.dataGridView1.TabIndex = 9;
            // 
            // erneuern
            // 
            this.erneuern.Location = new System.Drawing.Point(697, 199);
            this.erneuern.Name = "erneuern";
            this.erneuern.Size = new System.Drawing.Size(75, 23);
            this.erneuern.TabIndex = 10;
            this.erneuern.Text = "Refresh";
            this.erneuern.UseVisualStyleBackColor = true;
            this.erneuern.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // schreiben
            // 
            this.schreiben.Location = new System.Drawing.Point(697, 337);
            this.schreiben.Name = "schreiben";
            this.schreiben.Size = new System.Drawing.Size(75, 23);
            this.schreiben.TabIndex = 11;
            this.schreiben.Text = "schreiben";
            this.schreiben.UseVisualStyleBackColor = true;
            this.schreiben.Click += new System.EventHandler(this.schreiben_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(591, 339);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 12;
            this.textBox2.Click += new System.EventHandler(this.textBox2_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(591, 366);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(522, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(532, 369);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Value";
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
            this.dataGridRefresher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
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
            // 
            // Freezer
            // 
            this.Freezer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Freezer_DoWork);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Freeze);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.schreiben);
            this.Controls.Add(this.erneuern);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.nextScan);
            this.Controls.Add(this.button1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button nextScan;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button erneuern;
        private System.Windows.Forms.Button schreiben;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Open;
        private System.ComponentModel.BackgroundWorker dataGridRefresher;
        private System.Windows.Forms.CheckBox Freeze;
        private System.ComponentModel.BackgroundWorker Freezer;
    }
}