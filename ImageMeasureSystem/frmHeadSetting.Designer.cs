namespace ImageMeasureSystem
{
    partial class frmHeadSetting
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
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxHead1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxProd = new System.Windows.Forms.TextBox();
            this.labelProd = new System.Windows.Forms.Label();
            this.textBoxHead2 = new System.Windows.Forms.TextBox();
            this.labelhead2 = new System.Windows.Forms.Label();
            this.textBoxHead3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxHead4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxHead5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxHead6 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHead7 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxHead8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.prodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.head10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxHead10 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxHead9 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.BackColor = System.Drawing.Color.Lime;
            this.buttonSaveData.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSaveData.Location = new System.Drawing.Point(1113, 610);
            this.buttonSaveData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(147, 51);
            this.buttonSaveData.TabIndex = 22;
            this.buttonSaveData.Text = "儲存資料";
            this.buttonSaveData.UseVisualStyleBackColor = false;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.prodName,
            this.head1,
            this.head2,
            this.head3,
            this.head4,
            this.head5,
            this.head6,
            this.head7,
            this.head8,
            this.head9,
            this.head10});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1248, 560);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(122, 676);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 51);
            this.button3.TabIndex = 37;
            this.button3.Text = "刪除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Khaki;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(12, 676);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 51);
            this.button1.TabIndex = 36;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxHead1
            // 
            this.textBoxHead1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead1.Location = new System.Drawing.Point(214, 622);
            this.textBoxHead1.Name = "textBoxHead1";
            this.textBoxHead1.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead1.TabIndex = 35;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(214, 594);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 25);
            this.label2.TabIndex = 34;
            this.label2.Text = "Head1";
            // 
            // textBoxProd
            // 
            this.textBoxProd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxProd.Location = new System.Drawing.Point(12, 622);
            this.textBoxProd.Name = "textBoxProd";
            this.textBoxProd.Size = new System.Drawing.Size(186, 34);
            this.textBoxProd.TabIndex = 33;
            // 
            // labelProd
            // 
            this.labelProd.AutoSize = true;
            this.labelProd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelProd.Location = new System.Drawing.Point(76, 593);
            this.labelProd.Name = "labelProd";
            this.labelProd.Size = new System.Drawing.Size(52, 25);
            this.labelProd.TabIndex = 32;
            this.labelProd.Text = "產品";
            // 
            // textBoxHead2
            // 
            this.textBoxHead2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead2.Location = new System.Drawing.Point(297, 622);
            this.textBoxHead2.Name = "textBoxHead2";
            this.textBoxHead2.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead2.TabIndex = 39;
            // 
            // labelhead2
            // 
            this.labelhead2.AutoSize = true;
            this.labelhead2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelhead2.Location = new System.Drawing.Point(297, 594);
            this.labelhead2.Name = "labelhead2";
            this.labelhead2.Size = new System.Drawing.Size(75, 25);
            this.labelhead2.TabIndex = 38;
            this.labelhead2.Text = "Head2";
            // 
            // textBoxHead3
            // 
            this.textBoxHead3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead3.Location = new System.Drawing.Point(380, 622);
            this.textBoxHead3.Name = "textBoxHead3";
            this.textBoxHead3.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead3.TabIndex = 41;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(380, 594);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 25);
            this.label3.TabIndex = 40;
            this.label3.Text = "Head3";
            // 
            // textBoxHead4
            // 
            this.textBoxHead4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead4.Location = new System.Drawing.Point(463, 622);
            this.textBoxHead4.Name = "textBoxHead4";
            this.textBoxHead4.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead4.TabIndex = 43;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(463, 594);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 25);
            this.label4.TabIndex = 42;
            this.label4.Text = "Head4";
            // 
            // textBoxHead5
            // 
            this.textBoxHead5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead5.Location = new System.Drawing.Point(546, 622);
            this.textBoxHead5.Name = "textBoxHead5";
            this.textBoxHead5.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead5.TabIndex = 45;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(546, 594);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 25);
            this.label5.TabIndex = 44;
            this.label5.Text = "Head5";
            // 
            // textBoxHead6
            // 
            this.textBoxHead6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead6.Location = new System.Drawing.Point(629, 622);
            this.textBoxHead6.Name = "textBoxHead6";
            this.textBoxHead6.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead6.TabIndex = 47;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(629, 594);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 25);
            this.label6.TabIndex = 46;
            this.label6.Text = "Head6";
            // 
            // textBoxHead7
            // 
            this.textBoxHead7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead7.Location = new System.Drawing.Point(712, 622);
            this.textBoxHead7.Name = "textBoxHead7";
            this.textBoxHead7.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead7.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(712, 594);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 25);
            this.label7.TabIndex = 48;
            this.label7.Text = "Head7";
            // 
            // textBoxHead8
            // 
            this.textBoxHead8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead8.Location = new System.Drawing.Point(795, 622);
            this.textBoxHead8.Name = "textBoxHead8";
            this.textBoxHead8.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead8.TabIndex = 51;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(795, 594);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 25);
            this.label8.TabIndex = 50;
            this.label8.Text = "Head8";
            // 
            // prodName
            // 
            this.prodName.HeaderText = "產品";
            this.prodName.Name = "prodName";
            this.prodName.ReadOnly = true;
            // 
            // head1
            // 
            this.head1.HeaderText = "Head1";
            this.head1.Name = "head1";
            this.head1.ReadOnly = true;
            // 
            // head2
            // 
            this.head2.HeaderText = "Head2";
            this.head2.Name = "head2";
            this.head2.ReadOnly = true;
            // 
            // head3
            // 
            this.head3.HeaderText = "Head3";
            this.head3.Name = "head3";
            this.head3.ReadOnly = true;
            // 
            // head4
            // 
            this.head4.HeaderText = "Head4";
            this.head4.Name = "head4";
            this.head4.ReadOnly = true;
            // 
            // head5
            // 
            this.head5.HeaderText = "Head5";
            this.head5.Name = "head5";
            this.head5.ReadOnly = true;
            // 
            // head6
            // 
            this.head6.HeaderText = "Head6";
            this.head6.Name = "head6";
            this.head6.ReadOnly = true;
            // 
            // head7
            // 
            this.head7.HeaderText = "Head7";
            this.head7.Name = "head7";
            this.head7.ReadOnly = true;
            // 
            // head8
            // 
            this.head8.HeaderText = "Head8";
            this.head8.Name = "head8";
            this.head8.ReadOnly = true;
            // 
            // head9
            // 
            this.head9.HeaderText = "Head9";
            this.head9.Name = "head9";
            this.head9.ReadOnly = true;
            // 
            // head10
            // 
            this.head10.HeaderText = "Head10";
            this.head10.Name = "head10";
            this.head10.ReadOnly = true;
            // 
            // textBoxHead10
            // 
            this.textBoxHead10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead10.Location = new System.Drawing.Point(960, 622);
            this.textBoxHead10.Name = "textBoxHead10";
            this.textBoxHead10.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead10.TabIndex = 55;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(960, 594);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 54;
            this.label1.Text = "Head10";
            // 
            // textBoxHead9
            // 
            this.textBoxHead9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxHead9.Location = new System.Drawing.Point(877, 622);
            this.textBoxHead9.Name = "textBoxHead9";
            this.textBoxHead9.Size = new System.Drawing.Size(77, 34);
            this.textBoxHead9.TabIndex = 53;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(877, 594);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 25);
            this.label9.TabIndex = 52;
            this.label9.Text = "Head9";
            // 
            // frmHeadSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 750);
            this.Controls.Add(this.textBoxHead10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxHead9);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxHead8);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxHead7);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxHead6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxHead5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxHead4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxHead3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxHead2);
            this.Controls.Add(this.labelhead2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxHead1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxProd);
            this.Controls.Add(this.labelProd);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.buttonSaveData);
            this.Name = "frmHeadSetting";
            this.Text = "frmHeadSetting";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxHead1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxProd;
        private System.Windows.Forms.Label labelProd;
        private System.Windows.Forms.TextBox textBoxHead2;
        private System.Windows.Forms.Label labelhead2;
        private System.Windows.Forms.TextBox textBoxHead3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxHead4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxHead5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxHead6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHead7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxHead8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn head1;
        private System.Windows.Forms.DataGridViewTextBoxColumn head2;
        private System.Windows.Forms.DataGridViewTextBoxColumn head3;
        private System.Windows.Forms.DataGridViewTextBoxColumn head4;
        private System.Windows.Forms.DataGridViewTextBoxColumn head5;
        private System.Windows.Forms.DataGridViewTextBoxColumn head6;
        private System.Windows.Forms.DataGridViewTextBoxColumn head7;
        private System.Windows.Forms.DataGridViewTextBoxColumn head8;
        private System.Windows.Forms.DataGridViewTextBoxColumn head9;
        private System.Windows.Forms.DataGridViewTextBoxColumn head10;
        private System.Windows.Forms.TextBox textBoxHead10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxHead9;
        private System.Windows.Forms.Label label9;
    }
}