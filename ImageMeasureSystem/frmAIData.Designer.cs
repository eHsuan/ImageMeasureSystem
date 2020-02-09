namespace ImageMeasureSystem
{
    partial class frmAIData
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
            this.dataGridViewLCIN1 = new System.Windows.Forms.DataGridView();
            this.HeadNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.類型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLCIN1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewLCIN1
            // 
            this.dataGridViewLCIN1.AllowUserToAddRows = false;
            this.dataGridViewLCIN1.AllowUserToDeleteRows = false;
            this.dataGridViewLCIN1.AllowUserToResizeColumns = false;
            this.dataGridViewLCIN1.AllowUserToResizeRows = false;
            this.dataGridViewLCIN1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dataGridViewLCIN1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLCIN1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HeadNumber,
            this.類型,
            this.Value1,
            this.Value2,
            this.Value3});
            this.dataGridViewLCIN1.Location = new System.Drawing.Point(11, 11);
            this.dataGridViewLCIN1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewLCIN1.Name = "dataGridViewLCIN1";
            this.dataGridViewLCIN1.ReadOnly = true;
            this.dataGridViewLCIN1.RowHeadersWidth = 51;
            this.dataGridViewLCIN1.RowTemplate.Height = 27;
            this.dataGridViewLCIN1.Size = new System.Drawing.Size(724, 768);
            this.dataGridViewLCIN1.TabIndex = 40;
            // 
            // HeadNumber
            // 
            this.HeadNumber.HeaderText = "Head編號";
            this.HeadNumber.MinimumWidth = 6;
            this.HeadNumber.Name = "HeadNumber";
            this.HeadNumber.Width = 125;
            // 
            // 類型
            // 
            this.類型.HeaderText = "Type";
            this.類型.MinimumWidth = 6;
            this.類型.Name = "類型";
            this.類型.Width = 125;
            // 
            // Value1
            // 
            this.Value1.HeaderText = "資料1";
            this.Value1.MinimumWidth = 6;
            this.Value1.Name = "Value1";
            this.Value1.Width = 125;
            // 
            // Value2
            // 
            this.Value2.HeaderText = "資料2";
            this.Value2.MinimumWidth = 6;
            this.Value2.Name = "Value2";
            this.Value2.Width = 125;
            // 
            // Value3
            // 
            this.Value3.HeaderText = "資料3";
            this.Value3.MinimumWidth = 6;
            this.Value3.Name = "Value3";
            this.Value3.Width = 125;
            // 
            // frmAIData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 790);
            this.Controls.Add(this.dataGridViewLCIN1);
            this.Name = "frmAIData";
            this.Text = "frmAIData";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLCIN1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridViewLCIN1;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeadNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn 類型;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value3;
    }
}