namespace ImageMeasureSystem
{
    partial class frmSpecSetting
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxNormalOOSMax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxProd = new System.Windows.Forms.TextBox();
            this.labelProd = new System.Windows.Forms.Label();
            this.buttonSaveData = new System.Windows.Forms.Button();
            this.textBoxNormalOOSMin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.prodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.normalOOSMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.normalOOSMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.normalOOCMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.normalOOCMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overlapOOSMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overlapOOSMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overlapOOCMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.overlapOOCMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cornerOOSMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cornerOOSMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cornerOOCMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cornerOOCMin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxNormalOOCMin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxNormalOOCMax = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxOverlapOOCMin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOverlapOOCMax = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxOverlapOOSMin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxOverlapOOSMax = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCornerOOCMin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxCornerOOCMax = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxCornerOOSMin = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxCornerOOSMax = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.normalOOSMax,
            this.normalOOSMin,
            this.normalOOCMax,
            this.normalOOCMin,
            this.overlapOOSMax,
            this.overlapOOSMin,
            this.overlapOOCMax,
            this.overlapOOCMin,
            this.cornerOOSMax,
            this.cornerOOSMin,
            this.cornerOOCMax,
            this.cornerOOCMin});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1288, 574);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseClick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(123, 691);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 51);
            this.button3.TabIndex = 58;
            this.button3.Text = "刪除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Khaki;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(13, 691);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 51);
            this.button1.TabIndex = 57;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxNormalOOSMax
            // 
            this.textBoxNormalOOSMax.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxNormalOOSMax.Location = new System.Drawing.Point(231, 637);
            this.textBoxNormalOOSMax.Name = "textBoxNormalOOSMax";
            this.textBoxNormalOOSMax.Size = new System.Drawing.Size(198, 34);
            this.textBoxNormalOOSMax.TabIndex = 56;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(231, 609);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 25);
            this.label2.TabIndex = 55;
            this.label2.Text = "Normal_OOS_Max";
            // 
            // textBoxProd
            // 
            this.textBoxProd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxProd.Location = new System.Drawing.Point(13, 637);
            this.textBoxProd.Name = "textBoxProd";
            this.textBoxProd.Size = new System.Drawing.Size(186, 34);
            this.textBoxProd.TabIndex = 54;
            // 
            // labelProd
            // 
            this.labelProd.AutoSize = true;
            this.labelProd.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelProd.Location = new System.Drawing.Point(77, 608);
            this.labelProd.Name = "labelProd";
            this.labelProd.Size = new System.Drawing.Size(52, 25);
            this.labelProd.TabIndex = 53;
            this.labelProd.Text = "產品";
            // 
            // buttonSaveData
            // 
            this.buttonSaveData.BackColor = System.Drawing.Color.Lime;
            this.buttonSaveData.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonSaveData.Location = new System.Drawing.Point(1153, 756);
            this.buttonSaveData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonSaveData.Name = "buttonSaveData";
            this.buttonSaveData.Size = new System.Drawing.Size(147, 51);
            this.buttonSaveData.TabIndex = 52;
            this.buttonSaveData.Text = "儲存資料";
            this.buttonSaveData.UseVisualStyleBackColor = false;
            this.buttonSaveData.Click += new System.EventHandler(this.buttonSaveData_Click);
            // 
            // textBoxNormalOOSMin
            // 
            this.textBoxNormalOOSMin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxNormalOOSMin.Location = new System.Drawing.Point(435, 637);
            this.textBoxNormalOOSMin.Name = "textBoxNormalOOSMin";
            this.textBoxNormalOOSMin.Size = new System.Drawing.Size(186, 34);
            this.textBoxNormalOOSMin.TabIndex = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(435, 609);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 25);
            this.label1.TabIndex = 59;
            this.label1.Text = "Normal_OOS_Min";
            // 
            // prodName
            // 
            this.prodName.HeaderText = "產品名稱";
            this.prodName.Name = "prodName";
            this.prodName.ReadOnly = true;
            // 
            // normalOOSMax
            // 
            this.normalOOSMax.HeaderText = "Normal OOS Max";
            this.normalOOSMax.Name = "normalOOSMax";
            this.normalOOSMax.ReadOnly = true;
            // 
            // normalOOSMin
            // 
            this.normalOOSMin.HeaderText = "Normal OOS Min";
            this.normalOOSMin.Name = "normalOOSMin";
            this.normalOOSMin.ReadOnly = true;
            // 
            // normalOOCMax
            // 
            this.normalOOCMax.HeaderText = "Normal OOC Max";
            this.normalOOCMax.Name = "normalOOCMax";
            this.normalOOCMax.ReadOnly = true;
            // 
            // normalOOCMin
            // 
            this.normalOOCMin.HeaderText = "Normal OOC Min";
            this.normalOOCMin.Name = "normalOOCMin";
            this.normalOOCMin.ReadOnly = true;
            // 
            // overlapOOSMax
            // 
            this.overlapOOSMax.HeaderText = "Overlap OOS Max";
            this.overlapOOSMax.Name = "overlapOOSMax";
            this.overlapOOSMax.ReadOnly = true;
            // 
            // overlapOOSMin
            // 
            this.overlapOOSMin.HeaderText = "Overlap OOS Min";
            this.overlapOOSMin.Name = "overlapOOSMin";
            this.overlapOOSMin.ReadOnly = true;
            // 
            // overlapOOCMax
            // 
            this.overlapOOCMax.HeaderText = "Overlap OOC Max";
            this.overlapOOCMax.Name = "overlapOOCMax";
            this.overlapOOCMax.ReadOnly = true;
            // 
            // overlapOOCMin
            // 
            this.overlapOOCMin.HeaderText = "Overlap OOC Min";
            this.overlapOOCMin.Name = "overlapOOCMin";
            this.overlapOOCMin.ReadOnly = true;
            // 
            // cornerOOSMax
            // 
            this.cornerOOSMax.HeaderText = "Corner OOS Max";
            this.cornerOOSMax.Name = "cornerOOSMax";
            this.cornerOOSMax.ReadOnly = true;
            // 
            // cornerOOSMin
            // 
            this.cornerOOSMin.HeaderText = "Corner OOS Min";
            this.cornerOOSMin.Name = "cornerOOSMin";
            this.cornerOOSMin.ReadOnly = true;
            // 
            // cornerOOCMax
            // 
            this.cornerOOCMax.HeaderText = "Corner OOC Max";
            this.cornerOOCMax.Name = "cornerOOCMax";
            this.cornerOOCMax.ReadOnly = true;
            // 
            // cornerOOCMin
            // 
            this.cornerOOCMin.HeaderText = "Corner OOC Min";
            this.cornerOOCMin.Name = "cornerOOCMin";
            this.cornerOOCMin.ReadOnly = true;
            // 
            // textBoxNormalOOCMin
            // 
            this.textBoxNormalOOCMin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxNormalOOCMin.Location = new System.Drawing.Point(886, 637);
            this.textBoxNormalOOCMin.Name = "textBoxNormalOOCMin";
            this.textBoxNormalOOCMin.Size = new System.Drawing.Size(186, 34);
            this.textBoxNormalOOCMin.TabIndex = 72;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(886, 609);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 25);
            this.label7.TabIndex = 71;
            this.label7.Text = "Normal_OOC_Min";
            // 
            // textBoxNormalOOCMax
            // 
            this.textBoxNormalOOCMax.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxNormalOOCMax.Location = new System.Drawing.Point(682, 637);
            this.textBoxNormalOOCMax.Name = "textBoxNormalOOCMax";
            this.textBoxNormalOOCMax.Size = new System.Drawing.Size(198, 34);
            this.textBoxNormalOOCMax.TabIndex = 70;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(682, 609);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 25);
            this.label8.TabIndex = 69;
            this.label8.Text = "Normal_OOC_Max";
            // 
            // textBoxOverlapOOCMin
            // 
            this.textBoxOverlapOOCMin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxOverlapOOCMin.Location = new System.Drawing.Point(886, 708);
            this.textBoxOverlapOOCMin.Name = "textBoxOverlapOOCMin";
            this.textBoxOverlapOOCMin.Size = new System.Drawing.Size(186, 34);
            this.textBoxOverlapOOCMin.TabIndex = 80;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(886, 680);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 25);
            this.label3.TabIndex = 79;
            this.label3.Text = "Overlap_OOC_Min";
            // 
            // textBoxOverlapOOCMax
            // 
            this.textBoxOverlapOOCMax.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxOverlapOOCMax.Location = new System.Drawing.Point(682, 708);
            this.textBoxOverlapOOCMax.Name = "textBoxOverlapOOCMax";
            this.textBoxOverlapOOCMax.Size = new System.Drawing.Size(198, 34);
            this.textBoxOverlapOOCMax.TabIndex = 78;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(682, 680);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(193, 25);
            this.label4.TabIndex = 77;
            this.label4.Text = "Overlap_OOC_Max";
            // 
            // textBoxOverlapOOSMin
            // 
            this.textBoxOverlapOOSMin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxOverlapOOSMin.Location = new System.Drawing.Point(435, 708);
            this.textBoxOverlapOOSMin.Name = "textBoxOverlapOOSMin";
            this.textBoxOverlapOOSMin.Size = new System.Drawing.Size(186, 34);
            this.textBoxOverlapOOSMin.TabIndex = 76;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(435, 680);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(189, 25);
            this.label5.TabIndex = 75;
            this.label5.Text = "Overlap_OOS_Min";
            // 
            // textBoxOverlapOOSMax
            // 
            this.textBoxOverlapOOSMax.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxOverlapOOSMax.Location = new System.Drawing.Point(231, 708);
            this.textBoxOverlapOOSMax.Name = "textBoxOverlapOOSMax";
            this.textBoxOverlapOOSMax.Size = new System.Drawing.Size(198, 34);
            this.textBoxOverlapOOSMax.TabIndex = 74;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(231, 680);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(192, 25);
            this.label6.TabIndex = 73;
            this.label6.Text = "Overlap_OOS_Max";
            // 
            // textBoxCornerOOCMin
            // 
            this.textBoxCornerOOCMin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxCornerOOCMin.Location = new System.Drawing.Point(886, 773);
            this.textBoxCornerOOCMin.Name = "textBoxCornerOOCMin";
            this.textBoxCornerOOCMin.Size = new System.Drawing.Size(186, 34);
            this.textBoxCornerOOCMin.TabIndex = 88;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(886, 745);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 25);
            this.label9.TabIndex = 87;
            this.label9.Text = "Corner_OOC_Min";
            // 
            // textBoxCornerOOCMax
            // 
            this.textBoxCornerOOCMax.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxCornerOOCMax.Location = new System.Drawing.Point(682, 773);
            this.textBoxCornerOOCMax.Name = "textBoxCornerOOCMax";
            this.textBoxCornerOOCMax.Size = new System.Drawing.Size(198, 34);
            this.textBoxCornerOOCMax.TabIndex = 86;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(682, 745);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(183, 25);
            this.label10.TabIndex = 85;
            this.label10.Text = "Corner_OOC_Max";
            // 
            // textBoxCornerOOSMin
            // 
            this.textBoxCornerOOSMin.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxCornerOOSMin.Location = new System.Drawing.Point(435, 773);
            this.textBoxCornerOOSMin.Name = "textBoxCornerOOSMin";
            this.textBoxCornerOOSMin.Size = new System.Drawing.Size(186, 34);
            this.textBoxCornerOOSMin.TabIndex = 84;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(435, 745);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(179, 25);
            this.label11.TabIndex = 83;
            this.label11.Text = "Corner_OOS_Min";
            // 
            // textBoxCornerOOSMax
            // 
            this.textBoxCornerOOSMax.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxCornerOOSMax.Location = new System.Drawing.Point(231, 773);
            this.textBoxCornerOOSMax.Name = "textBoxCornerOOSMax";
            this.textBoxCornerOOSMax.Size = new System.Drawing.Size(198, 34);
            this.textBoxCornerOOSMax.TabIndex = 82;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(231, 745);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(182, 25);
            this.label12.TabIndex = 81;
            this.label12.Text = "Corner_OOS_Max";
            // 
            // frmSpecSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 854);
            this.Controls.Add(this.textBoxCornerOOCMin);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxCornerOOCMax);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxCornerOOSMin);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxCornerOOSMax);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxOverlapOOCMin);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxOverlapOOCMax);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxOverlapOOSMin);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxOverlapOOSMax);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxNormalOOCMin);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxNormalOOCMax);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxNormalOOSMin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxNormalOOSMax);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxProd);
            this.Controls.Add(this.labelProd);
            this.Controls.Add(this.buttonSaveData);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmSpecSetting";
            this.Text = "frmSpecSetting";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxNormalOOSMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxProd;
        private System.Windows.Forms.Label labelProd;
        private System.Windows.Forms.Button buttonSaveData;
        private System.Windows.Forms.TextBox textBoxNormalOOSMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn normalOOSMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn normalOOSMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn normalOOCMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn normalOOCMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn overlapOOSMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn overlapOOSMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn overlapOOCMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn overlapOOCMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cornerOOSMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn cornerOOSMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cornerOOCMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn cornerOOCMin;
        private System.Windows.Forms.TextBox textBoxNormalOOCMin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxNormalOOCMax;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxOverlapOOCMin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOverlapOOCMax;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxOverlapOOSMin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxOverlapOOSMax;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCornerOOCMin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxCornerOOCMax;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxCornerOOSMin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxCornerOOSMax;
        private System.Windows.Forms.Label label12;
    }
}