namespace dummyHacker
{
    partial class PointerScanForm
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
            this.PointerScanView = new System.Windows.Forms.DataGridView();
            this.BaseAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModuleName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offset1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offset2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offset3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Offset4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CloseButton = new System.Windows.Forms.Button();
            this.ScannerBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.PointerScanView)).BeginInit();
            this.SuspendLayout();
            // 
            // PointerScanView
            // 
            this.PointerScanView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PointerScanView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BaseAddress,
            this.ModuleName,
            this.Offset1,
            this.Offset2,
            this.Offset3,
            this.Offset4});
            this.PointerScanView.Location = new System.Drawing.Point(26, 45);
            this.PointerScanView.Name = "PointerScanView";
            this.PointerScanView.Size = new System.Drawing.Size(643, 475);
            this.PointerScanView.TabIndex = 0;
            // 
            // BaseAddress
            // 
            this.BaseAddress.HeaderText = "BaseAddress";
            this.BaseAddress.Name = "BaseAddress";
            // 
            // ModuleName
            // 
            this.ModuleName.HeaderText = "ModuleName";
            this.ModuleName.Name = "ModuleName";
            // 
            // Offset1
            // 
            this.Offset1.HeaderText = "Offset1";
            this.Offset1.Name = "Offset1";
            // 
            // Offset2
            // 
            this.Offset2.HeaderText = "Offset2";
            this.Offset2.Name = "Offset2";
            // 
            // Offset3
            // 
            this.Offset3.HeaderText = "Offset3";
            this.Offset3.Name = "Offset3";
            // 
            // Offset4
            // 
            this.Offset4.HeaderText = "Offset4";
            this.Offset4.Name = "Offset4";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(306, 560);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ScannerBackgroundWorker
            // 
            this.ScannerBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Scanner_DoWork);
            // 
            // PointerScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 606);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.PointerScanView);
            this.Font = new System.Drawing.Font("Calibri", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "PointerScanForm";
            this.Text = "PointerScanForm";
            this.Shown += new System.EventHandler(this.PointerScanForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.PointerScanView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView PointerScanView;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaseAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModuleName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset4;
        private System.ComponentModel.BackgroundWorker ScannerBackgroundWorker;
    }
}