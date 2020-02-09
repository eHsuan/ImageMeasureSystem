﻿namespace ImageMeasureSystem
{
    partial class frmAIModelSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxModelCorner = new System.Windows.Forms.TextBox();
            this.textBoxProdName = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonJudgeOK = new System.Windows.Forms.Button();
            this.prodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelCorner = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelNormal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelOverlap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxModelNormal = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxModelOverlap = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
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
            this.modelCorner,
            this.modelNormal,
            this.modelOverlap});
            this.dataGridView1.Location = new System.Drawing.Point(17, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(745, 456);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView1_CellMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(12, 515);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Corner模型編號";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(12, 484);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "產品名稱";
            // 
            // textBoxModelCorner
            // 
            this.textBoxModelCorner.Location = new System.Drawing.Point(193, 515);
            this.textBoxModelCorner.Name = "textBoxModelCorner";
            this.textBoxModelCorner.Size = new System.Drawing.Size(100, 25);
            this.textBoxModelCorner.TabIndex = 3;
            // 
            // textBoxProdName
            // 
            this.textBoxProdName.Location = new System.Drawing.Point(193, 484);
            this.textBoxProdName.Name = "textBoxProdName";
            this.textBoxProdName.Size = new System.Drawing.Size(100, 25);
            this.textBoxProdName.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(672, 570);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 51);
            this.button3.TabIndex = 33;
            this.button3.Text = "刪除";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Khaki;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(573, 570);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 51);
            this.button1.TabIndex = 32;
            this.button1.Text = "修改";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonJudgeOK
            // 
            this.buttonJudgeOK.BackColor = System.Drawing.Color.Lime;
            this.buttonJudgeOK.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonJudgeOK.Location = new System.Drawing.Point(578, 625);
            this.buttonJudgeOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonJudgeOK.Name = "buttonJudgeOK";
            this.buttonJudgeOK.Size = new System.Drawing.Size(184, 51);
            this.buttonJudgeOK.TabIndex = 31;
            this.buttonJudgeOK.Text = "儲存資料";
            this.buttonJudgeOK.UseVisualStyleBackColor = false;
            this.buttonJudgeOK.Click += new System.EventHandler(this.buttonJudgeOK_Click);
            // 
            // prodName
            // 
            this.prodName.HeaderText = "產品名稱";
            this.prodName.MinimumWidth = 6;
            this.prodName.Name = "prodName";
            this.prodName.ReadOnly = true;
            this.prodName.Width = 125;
            // 
            // modelCorner
            // 
            this.modelCorner.HeaderText = "Corner模型編號";
            this.modelCorner.MinimumWidth = 6;
            this.modelCorner.Name = "modelCorner";
            this.modelCorner.ReadOnly = true;
            this.modelCorner.Width = 125;
            // 
            // modelNormal
            // 
            this.modelNormal.HeaderText = "Normal模型編號";
            this.modelNormal.MinimumWidth = 6;
            this.modelNormal.Name = "modelNormal";
            this.modelNormal.ReadOnly = true;
            this.modelNormal.Width = 125;
            // 
            // modelOverlap
            // 
            this.modelOverlap.HeaderText = "Overlap模型編號";
            this.modelOverlap.MinimumWidth = 6;
            this.modelOverlap.Name = "modelOverlap";
            this.modelOverlap.ReadOnly = true;
            this.modelOverlap.Width = 125;
            // 
            // textBoxModelNormal
            // 
            this.textBoxModelNormal.Location = new System.Drawing.Point(193, 546);
            this.textBoxModelNormal.Name = "textBoxModelNormal";
            this.textBoxModelNormal.Size = new System.Drawing.Size(100, 25);
            this.textBoxModelNormal.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(12, 546);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 25);
            this.label3.TabIndex = 34;
            this.label3.Text = "Normal模型編號";
            // 
            // textBoxModelOverlap
            // 
            this.textBoxModelOverlap.Location = new System.Drawing.Point(193, 577);
            this.textBoxModelOverlap.Name = "textBoxModelOverlap";
            this.textBoxModelOverlap.Size = new System.Drawing.Size(100, 25);
            this.textBoxModelOverlap.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(12, 577);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 25);
            this.label4.TabIndex = 36;
            this.label4.Text = "Overlap模型編號";
            // 
            // frmAIModelSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 686);
            this.Controls.Add(this.textBoxModelOverlap);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxModelNormal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonJudgeOK);
            this.Controls.Add(this.textBoxProdName);
            this.Controls.Add(this.textBoxModelCorner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "frmAIModelSetting";
            this.Text = "frmAIModelSetting";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxModelCorner;
        private System.Windows.Forms.TextBox textBoxProdName;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonJudgeOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn prodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelCorner;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelNormal;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelOverlap;
        private System.Windows.Forms.TextBox textBoxModelNormal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxModelOverlap;
        private System.Windows.Forms.Label label4;
    }
}