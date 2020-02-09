namespace ImageMeasureSystem
{
    partial class frmABCD
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelNowX = new System.Windows.Forms.Label();
            this.labelNowY = new System.Windows.Forms.Label();
            this.buttonSample = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelImageName = new System.Windows.Forms.Label();
            this.labelImageNum = new System.Windows.Forms.Label();
            this.pictureBoxRight = new System.Windows.Forms.PictureBox();
            this.pictureBoxLeft = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelMeasureType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxOK = new System.Windows.Forms.CheckBox();
            this.checkBoxNG = new System.Windows.Forms.CheckBox();
            this.labelCheckMark = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeasureData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).BeginInit();
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
            this.dataGridViewMeasureData.Location = new System.Drawing.Point(12, 641);
            this.dataGridViewMeasureData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewMeasureData.Name = "dataGridViewMeasureData";
            this.dataGridViewMeasureData.RowTemplate.Height = 27;
            this.dataGridViewMeasureData.Size = new System.Drawing.Size(299, 102);
            this.dataGridViewMeasureData.TabIndex = 1;
            // 
            // pointNum
            // 
            this.pointNum.HeaderText = "NO.";
            this.pointNum.Name = "pointNum";
            this.pointNum.Width = 50;
            // 
            // locationX
            // 
            this.locationX.HeaderText = "X";
            this.locationX.Name = "locationX";
            this.locationX.Width = 60;
            // 
            // locationY
            // 
            this.locationY.HeaderText = "Y";
            this.locationY.Name = "locationY";
            this.locationY.Width = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(609, 621);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "X : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(716, 621);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y : ";
            // 
            // labelNowX
            // 
            this.labelNowX.AutoSize = true;
            this.labelNowX.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelNowX.Location = new System.Drawing.Point(663, 621);
            this.labelNowX.Name = "labelNowX";
            this.labelNowX.Size = new System.Drawing.Size(30, 31);
            this.labelNowX.TabIndex = 4;
            this.labelNowX.Text = "X";
            // 
            // labelNowY
            // 
            this.labelNowY.AutoSize = true;
            this.labelNowY.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelNowY.Location = new System.Drawing.Point(769, 621);
            this.labelNowY.Name = "labelNowY";
            this.labelNowY.Size = new System.Drawing.Size(30, 31);
            this.labelNowY.TabIndex = 5;
            this.labelNowY.Text = "X";
            // 
            // buttonSample
            // 
            this.buttonSample.BackColor = System.Drawing.Color.Red;
            this.buttonSample.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonSample.Location = new System.Drawing.Point(669, 698);
            this.buttonSample.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSample.Name = "buttonSample";
            this.buttonSample.Size = new System.Drawing.Size(80, 42);
            this.buttonSample.TabIndex = 32;
            this.buttonSample.Text = "清除";
            this.buttonSample.UseVisualStyleBackColor = false;
            this.buttonSample.Click += new System.EventHandler(this.buttonSample_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Gold;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold);
            this.button1.Location = new System.Drawing.Point(669, 653);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 42);
            this.button1.TabIndex = 33;
            this.button1.Text = "上一步";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SkyBlue;
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(512, 653);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(125, 80);
            this.button2.TabIndex = 34;
            this.button2.Text = "計算";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(337, 641);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 25);
            this.label3.TabIndex = 37;
            this.label3.Text = "圖檔名稱 : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(337, 668);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 25);
            this.label4.TabIndex = 38;
            this.label4.Text = "模品編號 : ";
            // 
            // labelImageName
            // 
            this.labelImageName.AutoSize = true;
            this.labelImageName.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelImageName.Location = new System.Drawing.Point(436, 641);
            this.labelImageName.Name = "labelImageName";
            this.labelImageName.Size = new System.Drawing.Size(38, 25);
            this.labelImageName.TabIndex = 39;
            this.labelImageName.Text = "XX";
            this.labelImageName.TextChanged += new System.EventHandler(this.labelImageName_TextChanged);
            // 
            // labelImageNum
            // 
            this.labelImageNum.AutoSize = true;
            this.labelImageNum.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelImageNum.Location = new System.Drawing.Point(436, 668);
            this.labelImageNum.Name = "labelImageNum";
            this.labelImageNum.Size = new System.Drawing.Size(38, 25);
            this.labelImageNum.TabIndex = 40;
            this.labelImageNum.Text = "XX";
            // 
            // pictureBoxRight
            // 
            this.pictureBoxRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxRight.Image = global::ImageMeasureSystem.Properties.Resources.common_PageNexts_32x32;
            this.pictureBoxRight.Location = new System.Drawing.Point(860, 653);
            this.pictureBoxRight.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxRight.Name = "pictureBoxRight";
            this.pictureBoxRight.Size = new System.Drawing.Size(79, 80);
            this.pictureBoxRight.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxRight.TabIndex = 36;
            this.pictureBoxRight.TabStop = false;
            this.pictureBoxRight.Click += new System.EventHandler(this.pictureBoxRight_Click);
            // 
            // pictureBoxLeft
            // 
            this.pictureBoxLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxLeft.Image = global::ImageMeasureSystem.Properties.Resources.common_PagePrevious_32x32;
            this.pictureBoxLeft.Location = new System.Drawing.Point(774, 653);
            this.pictureBoxLeft.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxLeft.Name = "pictureBoxLeft";
            this.pictureBoxLeft.Size = new System.Drawing.Size(79, 80);
            this.pictureBoxLeft.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLeft.TabIndex = 35;
            this.pictureBoxLeft.TabStop = false;
            this.pictureBoxLeft.Click += new System.EventHandler(this.pictureBoxLeft_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 600);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_Con);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            // 
            // labelMeasureType
            // 
            this.labelMeasureType.AutoSize = true;
            this.labelMeasureType.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelMeasureType.Location = new System.Drawing.Point(436, 698);
            this.labelMeasureType.Name = "labelMeasureType";
            this.labelMeasureType.Size = new System.Drawing.Size(38, 25);
            this.labelMeasureType.TabIndex = 42;
            this.labelMeasureType.Text = "XX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(337, 698);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 25);
            this.label6.TabIndex = 41;
            this.label6.Text = "量測種類 : ";
            // 
            // checkBoxOK
            // 
            this.checkBoxOK.AutoSize = true;
            this.checkBoxOK.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxOK.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.checkBoxOK.Location = new System.Drawing.Point(55, 681);
            this.checkBoxOK.Name = "checkBoxOK";
            this.checkBoxOK.Size = new System.Drawing.Size(83, 42);
            this.checkBoxOK.TabIndex = 43;
            this.checkBoxOK.Text = "OK";
            this.checkBoxOK.UseVisualStyleBackColor = true;
            this.checkBoxOK.Visible = false;
            this.checkBoxOK.CheckedChanged += new System.EventHandler(this.checkBoxOK_CheckedChanged);
            // 
            // checkBoxNG
            // 
            this.checkBoxNG.AutoSize = true;
            this.checkBoxNG.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkBoxNG.ForeColor = System.Drawing.Color.Red;
            this.checkBoxNG.Location = new System.Drawing.Point(177, 681);
            this.checkBoxNG.Name = "checkBoxNG";
            this.checkBoxNG.Size = new System.Drawing.Size(87, 42);
            this.checkBoxNG.TabIndex = 44;
            this.checkBoxNG.Text = "NG";
            this.checkBoxNG.UseVisualStyleBackColor = true;
            this.checkBoxNG.Visible = false;
            this.checkBoxNG.CheckedChanged += new System.EventHandler(this.checkBoxNG_CheckedChanged);
            // 
            // labelCheckMark
            // 
            this.labelCheckMark.AutoSize = true;
            this.labelCheckMark.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelCheckMark.ForeColor = System.Drawing.Color.Red;
            this.labelCheckMark.Location = new System.Drawing.Point(29, 641);
            this.labelCheckMark.Name = "labelCheckMark";
            this.labelCheckMark.Size = new System.Drawing.Size(254, 36);
            this.labelCheckMark.TabIndex = 45;
            this.labelCheckMark.Text = "確認Mark位置結果";
            // 
            // frmABCD
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1287, 753);
            this.ControlBox = false;
            this.Controls.Add(this.labelCheckMark);
            this.Controls.Add(this.checkBoxNG);
            this.Controls.Add(this.checkBoxOK);
            this.Controls.Add(this.labelMeasureType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelImageNum);
            this.Controls.Add(this.labelImageName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBoxRight);
            this.Controls.Add(this.pictureBoxLeft);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSample);
            this.Controls.Add(this.labelNowY);
            this.Controls.Add(this.labelNowX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dataGridViewMeasureData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmABCD";
            this.Text = "ABCD Measure";
            this.Load += new System.EventHandler(this.frmABCD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMeasureData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dataGridViewMeasureData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelNowX;
        private System.Windows.Forms.Label labelNowY;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationX;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationY;
        private System.Windows.Forms.Button buttonSample;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBoxLeft;
        private System.Windows.Forms.PictureBox pictureBoxRight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelImageName;
        private System.Windows.Forms.Label labelImageNum;
        private System.Windows.Forms.Label labelMeasureType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxOK;
        private System.Windows.Forms.CheckBox checkBoxNG;
        private System.Windows.Forms.Label labelCheckMark;
    }
}