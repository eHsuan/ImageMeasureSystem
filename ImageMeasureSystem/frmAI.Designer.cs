namespace ImageMeasureSystem
{
    partial class frmAI
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
            this.dataGridViewMeasureData = new System.Windows.Forms.DataGridView();
            this.pointNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.locationY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.labelImageNum = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelMeasureType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelImageCount = new System.Windows.Forms.Label();
            this.labelDontMeasure = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeasureData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewMeasureData
            // 
            this.dataGridViewMeasureData.AllowUserToAddRows = false;
            this.dataGridViewMeasureData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMeasureData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pointNum,
            this.locationX,
            this.locationY});
            this.dataGridViewMeasureData.Location = new System.Drawing.Point(691, 574);
            this.dataGridViewMeasureData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewMeasureData.Name = "dataGridViewMeasureData";
            this.dataGridViewMeasureData.RowHeadersWidth = 51;
            this.dataGridViewMeasureData.RowTemplate.Height = 27;
            this.dataGridViewMeasureData.Size = new System.Drawing.Size(249, 102);
            this.dataGridViewMeasureData.TabIndex = 1;
            // 
            // pointNum
            // 
            this.pointNum.HeaderText = "NO.";
            this.pointNum.MinimumWidth = 6;
            this.pointNum.Name = "pointNum";
            this.pointNum.Width = 50;
            // 
            // locationX
            // 
            this.locationX.HeaderText = "X";
            this.locationX.MinimumWidth = 6;
            this.locationX.Name = "locationX";
            this.locationX.Width = 60;
            // 
            // locationY
            // 
            this.locationY.HeaderText = "Y";
            this.locationY.MinimumWidth = 6;
            this.locationY.Name = "locationY";
            this.locationY.Width = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(658, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 21);
            this.label4.TabIndex = 38;
            this.label4.Text = "模品編號 : ";
            // 
            // labelImageNum
            // 
            this.labelImageNum.AutoSize = true;
            this.labelImageNum.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelImageNum.Location = new System.Drawing.Point(757, 57);
            this.labelImageNum.Name = "labelImageNum";
            this.labelImageNum.Size = new System.Drawing.Size(32, 21);
            this.labelImageNum.TabIndex = 40;
            this.labelImageNum.Text = "XX";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(640, 480);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_Con);
            // 
            // labelMeasureType
            // 
            this.labelMeasureType.AutoSize = true;
            this.labelMeasureType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelMeasureType.Location = new System.Drawing.Point(757, 87);
            this.labelMeasureType.Name = "labelMeasureType";
            this.labelMeasureType.Size = new System.Drawing.Size(32, 21);
            this.labelMeasureType.TabIndex = 42;
            this.labelMeasureType.Text = "XX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(658, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 21);
            this.label6.TabIndex = 41;
            this.label6.Text = "量測種類 : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(658, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 21);
            this.label3.TabIndex = 43;
            this.label3.Text = "圖片數量 : ";
            // 
            // labelImageCount
            // 
            this.labelImageCount.AutoSize = true;
            this.labelImageCount.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelImageCount.Location = new System.Drawing.Point(757, 26);
            this.labelImageCount.Name = "labelImageCount";
            this.labelImageCount.Size = new System.Drawing.Size(32, 21);
            this.labelImageCount.TabIndex = 44;
            this.labelImageCount.Text = "XX";
            // 
            // labelDontMeasure
            // 
            this.labelDontMeasure.AutoSize = true;
            this.labelDontMeasure.Font = new System.Drawing.Font("微軟正黑體", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelDontMeasure.ForeColor = System.Drawing.Color.Red;
            this.labelDontMeasure.Location = new System.Drawing.Point(658, 161);
            this.labelDontMeasure.Name = "labelDontMeasure";
            this.labelDontMeasure.Size = new System.Drawing.Size(257, 38);
            this.labelDontMeasure.TabIndex = 45;
            this.labelDontMeasure.Text = "確認影像無需量測";
            this.labelDontMeasure.Visible = false;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxMessage.Location = new System.Drawing.Point(12, 542);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.Size = new System.Drawing.Size(673, 169);
            this.textBoxMessage.TabIndex = 46;
            // 
            // frmAI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(955, 780);
            this.ControlBox = false;
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.labelDontMeasure);
            this.Controls.Add(this.labelImageCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelMeasureType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelImageNum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridViewMeasureData);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmAI";
            this.Text = "ABCD Measure";
            this.Load += new System.EventHandler(this.frmABCD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeasureData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.DataGridView dataGridViewMeasureData;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationX;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelImageNum;
        private System.Windows.Forms.Label labelMeasureType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelImageCount;
        private System.Windows.Forms.Label labelDontMeasure;
        public System.Windows.Forms.TextBox textBoxMessage;
    }
}