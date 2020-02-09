namespace ADMImageJudgeSystem
{
    partial class frmAlarm
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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxTop = new System.Windows.Forms.PictureBox();
            this.pictureBoxButton = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.timerpictureBox = new System.Windows.Forms.Timer(this.components);
            this.timerTopMost = new System.Windows.Forms.Timer(this.components);
            this.textBoxAlarmMessage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxTop
            // 
            this.pictureBoxTop.BackColor = System.Drawing.Color.Silver;
            this.pictureBoxTop.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxTop.Name = "pictureBoxTop";
            this.pictureBoxTop.Size = new System.Drawing.Size(1332, 121);
            this.pictureBoxTop.TabIndex = 0;
            this.pictureBoxTop.TabStop = false;
            // 
            // pictureBoxButton
            // 
            this.pictureBoxButton.BackColor = System.Drawing.Color.Silver;
            this.pictureBoxButton.Location = new System.Drawing.Point(12, 542);
            this.pictureBoxButton.Name = "pictureBoxButton";
            this.pictureBoxButton.Size = new System.Drawing.Size(1332, 121);
            this.pictureBoxButton.TabIndex = 1;
            this.pictureBoxButton.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(549, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 80);
            this.label1.TabIndex = 2;
            this.label1.Text = "Alarm!!!";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.Lime;
            this.buttonClose.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonClose.Location = new System.Drawing.Point(563, 443);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(262, 71);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "關閉視窗";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // textBoxAlarmMessage
            // 
            this.textBoxAlarmMessage.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.textBoxAlarmMessage.Location = new System.Drawing.Point(12, 236);
            this.textBoxAlarmMessage.Multiline = true;
            this.textBoxAlarmMessage.Name = "textBoxAlarmMessage";
            this.textBoxAlarmMessage.Size = new System.Drawing.Size(1332, 201);
            this.textBoxAlarmMessage.TabIndex = 5;
            // 
            // frmAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 675);
            this.Controls.Add(this.textBoxAlarmMessage);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBoxButton);
            this.Controls.Add(this.pictureBoxTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAlarm";
            this.Text = "frmAlarm";
            this.Load += new System.EventHandler(this.frmAlarm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxTop;
        private System.Windows.Forms.PictureBox pictureBoxButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Timer timerpictureBox;
        private System.Windows.Forms.Timer timerTopMost;
        private System.Windows.Forms.TextBox textBoxAlarmMessage;
    }
}