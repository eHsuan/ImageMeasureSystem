namespace ImageMeasureSystem
{
    partial class frmPanelTA
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
            this.dataGridViewIdList = new System.Windows.Forms.DataGridView();
            this.lineID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.glassID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewPIIN1 = new System.Windows.Forms.DataGridView();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lt_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lt_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diff1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rb_X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rb_Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diff2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewPIIN2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diff3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diff4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewLCIN1 = new System.Windows.Forms.DataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxGlassIdNow = new System.Windows.Forms.TextBox();
            this.textBoxUnit = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.HeadNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIdList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPIIN1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPIIN2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLCIN1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewIdList
            // 
            this.dataGridViewIdList.AllowUserToAddRows = false;
            this.dataGridViewIdList.AllowUserToDeleteRows = false;
            this.dataGridViewIdList.AllowUserToResizeColumns = false;
            this.dataGridViewIdList.AllowUserToResizeRows = false;
            this.dataGridViewIdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewIdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.lineID,
            this.unitID,
            this.prodName,
            this.glassID,
            this.pid});
            this.dataGridViewIdList.Location = new System.Drawing.Point(11, 462);
            this.dataGridViewIdList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewIdList.Name = "dataGridViewIdList";
            this.dataGridViewIdList.ReadOnly = true;
            this.dataGridViewIdList.RowHeadersWidth = 51;
            this.dataGridViewIdList.RowTemplate.Height = 27;
            this.dataGridViewIdList.Size = new System.Drawing.Size(520, 142);
            this.dataGridViewIdList.TabIndex = 56;
            this.dataGridViewIdList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseClick);
            // 
            // lineID
            // 
            this.lineID.HeaderText = "線別";
            this.lineID.MinimumWidth = 6;
            this.lineID.Name = "lineID";
            this.lineID.ReadOnly = true;
            this.lineID.Width = 75;
            // 
            // unitID
            // 
            this.unitID.HeaderText = "子機";
            this.unitID.MinimumWidth = 6;
            this.unitID.Name = "unitID";
            this.unitID.ReadOnly = true;
            this.unitID.Width = 60;
            // 
            // prodName
            // 
            this.prodName.HeaderText = "產品";
            this.prodName.MinimumWidth = 6;
            this.prodName.Name = "prodName";
            this.prodName.ReadOnly = true;
            this.prodName.Width = 70;
            // 
            // glassID
            // 
            this.glassID.HeaderText = "基板ID";
            this.glassID.MinimumWidth = 6;
            this.glassID.Name = "glassID";
            this.glassID.ReadOnly = true;
            this.glassID.Width = 140;
            // 
            // pid
            // 
            this.pid.HeaderText = "PID";
            this.pid.MinimumWidth = 6;
            this.pid.Name = "pid";
            this.pid.ReadOnly = true;
            this.pid.Width = 125;
            // 
            // dataGridViewPIIN1
            // 
            this.dataGridViewPIIN1.AllowUserToAddRows = false;
            this.dataGridViewPIIN1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPIIN1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.type,
            this.lt_X,
            this.lt_Y,
            this.diff1,
            this.rb_X,
            this.rb_Y,
            this.diff2});
            this.dataGridViewPIIN1.Location = new System.Drawing.Point(11, 76);
            this.dataGridViewPIIN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewPIIN1.Name = "dataGridViewPIIN1";
            this.dataGridViewPIIN1.RowHeadersWidth = 51;
            this.dataGridViewPIIN1.RowTemplate.Height = 27;
            this.dataGridViewPIIN1.Size = new System.Drawing.Size(519, 190);
            this.dataGridViewPIIN1.TabIndex = 54;
            this.dataGridViewPIIN1.Visible = false;
            // 
            // type
            // 
            this.type.HeaderText = "Type";
            this.type.MinimumWidth = 6;
            this.type.Name = "type";
            this.type.Width = 50;
            // 
            // lt_X
            // 
            this.lt_X.HeaderText = "左上X";
            this.lt_X.MinimumWidth = 6;
            this.lt_X.Name = "lt_X";
            this.lt_X.Width = 90;
            // 
            // lt_Y
            // 
            this.lt_Y.HeaderText = "左上Y";
            this.lt_Y.MinimumWidth = 6;
            this.lt_Y.Name = "lt_Y";
            this.lt_Y.Width = 90;
            // 
            // diff1
            // 
            this.diff1.HeaderText = "距離";
            this.diff1.MinimumWidth = 6;
            this.diff1.Name = "diff1";
            this.diff1.Width = 90;
            // 
            // rb_X
            // 
            this.rb_X.HeaderText = "右下X";
            this.rb_X.MinimumWidth = 6;
            this.rb_X.Name = "rb_X";
            this.rb_X.Width = 90;
            // 
            // rb_Y
            // 
            this.rb_Y.HeaderText = "右下Y";
            this.rb_Y.MinimumWidth = 6;
            this.rb_Y.Name = "rb_Y";
            this.rb_Y.Width = 90;
            // 
            // diff2
            // 
            this.diff2.HeaderText = "距離";
            this.diff2.MinimumWidth = 6;
            this.diff2.Name = "diff2";
            this.diff2.Width = 90;
            // 
            // dataGridViewPIIN2
            // 
            this.dataGridViewPIIN2.AllowUserToAddRows = false;
            this.dataGridViewPIIN2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPIIN2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.diff3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.diff4});
            this.dataGridViewPIIN2.Location = new System.Drawing.Point(11, 270);
            this.dataGridViewPIIN2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewPIIN2.Name = "dataGridViewPIIN2";
            this.dataGridViewPIIN2.RowHeadersWidth = 51;
            this.dataGridViewPIIN2.RowTemplate.Height = 27;
            this.dataGridViewPIIN2.Size = new System.Drawing.Size(519, 186);
            this.dataGridViewPIIN2.TabIndex = 55;
            this.dataGridViewPIIN2.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Type";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "左上X";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 90;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "左上Y";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 90;
            // 
            // diff3
            // 
            this.diff3.HeaderText = "距離";
            this.diff3.MinimumWidth = 6;
            this.diff3.Name = "diff3";
            this.diff3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "右下X";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "右下Y";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 90;
            // 
            // diff4
            // 
            this.diff4.HeaderText = "距離";
            this.diff4.MinimumWidth = 6;
            this.diff4.Name = "diff4";
            this.diff4.Width = 125;
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
            this.dataType,
            this.Value1,
            this.Value2,
            this.Value3});
            this.dataGridViewLCIN1.Location = new System.Drawing.Point(11, 76);
            this.dataGridViewLCIN1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewLCIN1.Name = "dataGridViewLCIN1";
            this.dataGridViewLCIN1.RowHeadersVisible = false;
            this.dataGridViewLCIN1.RowHeadersWidth = 51;
            this.dataGridViewLCIN1.RowTemplate.Height = 27;
            this.dataGridViewLCIN1.Size = new System.Drawing.Size(520, 382);
            this.dataGridViewLCIN1.TabIndex = 57;
            this.dataGridViewLCIN1.Visible = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxGlassIdNow);
            this.groupBox5.Location = new System.Drawing.Point(11, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox5.Size = new System.Drawing.Size(176, 50);
            this.groupBox5.TabIndex = 58;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "目前檢查基板";
            // 
            // textBoxGlassIdNow
            // 
            this.textBoxGlassIdNow.Location = new System.Drawing.Point(16, 19);
            this.textBoxGlassIdNow.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxGlassIdNow.Name = "textBoxGlassIdNow";
            this.textBoxGlassIdNow.Size = new System.Drawing.Size(140, 25);
            this.textBoxGlassIdNow.TabIndex = 0;
            // 
            // textBoxUnit
            // 
            this.textBoxUnit.Location = new System.Drawing.Point(16, 19);
            this.textBoxUnit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxUnit.Name = "textBoxUnit";
            this.textBoxUnit.Size = new System.Drawing.Size(52, 25);
            this.textBoxUnit.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxUnit);
            this.groupBox3.Location = new System.Drawing.Point(191, 12);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(80, 50);
            this.groupBox3.TabIndex = 59;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "子機";
            // 
            // HeadNumber
            // 
            this.HeadNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.HeadNumber.HeaderText = "Head編號";
            this.HeadNumber.MinimumWidth = 6;
            this.HeadNumber.Name = "HeadNumber";
            this.HeadNumber.Width = 95;
            // 
            // dataType
            // 
            this.dataType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataType.HeaderText = "Type";
            this.dataType.MinimumWidth = 6;
            this.dataType.Name = "dataType";
            this.dataType.Width = 65;
            // 
            // Value1
            // 
            this.Value1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value1.HeaderText = "資料1";
            this.Value1.MinimumWidth = 6;
            this.Value1.Name = "Value1";
            this.Value1.Width = 73;
            // 
            // Value2
            // 
            this.Value2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value2.HeaderText = "資料2";
            this.Value2.MinimumWidth = 6;
            this.Value2.Name = "Value2";
            this.Value2.Width = 73;
            // 
            // Value3
            // 
            this.Value3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Value3.HeaderText = "資料3";
            this.Value3.MinimumWidth = 6;
            this.Value3.Name = "Value3";
            this.Value3.Width = 73;
            // 
            // frmPanelTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 616);
            this.Controls.Add(this.dataGridViewIdList);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dataGridViewLCIN1);
            this.Controls.Add(this.dataGridViewPIIN1);
            this.Controls.Add(this.dataGridViewPIIN2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmPanelTA";
            this.Text = "frmPanelTA";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewIdList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPIIN1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPIIN2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLCIN1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.DataGridView dataGridViewIdList;
        public System.Windows.Forms.DataGridView dataGridViewPIIN1;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn lt_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn lt_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn diff1;
        private System.Windows.Forms.DataGridViewTextBoxColumn rb_X;
        private System.Windows.Forms.DataGridViewTextBoxColumn rb_Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn diff2;
        public System.Windows.Forms.DataGridView dataGridViewPIIN2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn diff3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn diff4;
        public System.Windows.Forms.DataGridView dataGridViewLCIN1;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.TextBox textBoxGlassIdNow;
        public System.Windows.Forms.TextBox textBoxUnit;
        public System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineID;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitID;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn glassID;
        private System.Windows.Forms.DataGridViewTextBoxColumn pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn HeadNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value3;
    }
}