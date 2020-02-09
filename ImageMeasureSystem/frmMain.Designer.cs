namespace ImageMeasureSystem
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerNow = new System.Windows.Forms.Timer(this.components);
            this.timerJudgeCount = new System.Windows.Forms.Timer(this.components);
            this.timerCycle = new System.Windows.Forms.Timer(this.components);
            this.timerCheckServer = new System.Windows.Forms.Timer(this.components);
            this.timerDeleyCheck = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.系統設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.比例尺設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.使用者帳戶設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBCD量測數量設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recipe設定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查詢ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.基板歷史紀錄ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.校正ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.比例尺校正ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.buttonAIProcessStart = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCreateExcelData = new System.Windows.Forms.Button();
            this.buttonJudgeCancel = new System.Windows.Forms.Button();
            this.buttonJudgeOK = new System.Windows.Forms.Button();
            this.buttonPanelChange = new System.Windows.Forms.Button();
            this.checkBoxPause = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelUserID = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelTimeNow = new System.Windows.Forms.Label();
            this.timerAIProcess = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 21);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(1675, 989);
            this.splitContainer1.SplitterDistance = 494;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.設定ToolStripMenuItem,
            this.查詢ToolStripMenuItem,
            this.校正ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1360, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 設定ToolStripMenuItem
            // 
            this.設定ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系統設定ToolStripMenuItem,
            this.比例尺設定ToolStripMenuItem,
            this.使用者帳戶設定ToolStripMenuItem,
            this.aBCD量測數量設定ToolStripMenuItem,
            this.recipe設定ToolStripMenuItem});
            this.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem";
            this.設定ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.設定ToolStripMenuItem.Text = "設定";
            // 
            // 系統設定ToolStripMenuItem
            // 
            this.系統設定ToolStripMenuItem.Name = "系統設定ToolStripMenuItem";
            this.系統設定ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.系統設定ToolStripMenuItem.Text = "系統設定";
            this.系統設定ToolStripMenuItem.Click += new System.EventHandler(this.系統設定ToolStripMenuItem_Click);
            // 
            // 比例尺設定ToolStripMenuItem
            // 
            this.比例尺設定ToolStripMenuItem.Name = "比例尺設定ToolStripMenuItem";
            this.比例尺設定ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.比例尺設定ToolStripMenuItem.Text = "比例尺設定";
            this.比例尺設定ToolStripMenuItem.Click += new System.EventHandler(this.比例尺設定ToolStripMenuItem_Click);
            // 
            // 使用者帳戶設定ToolStripMenuItem
            // 
            this.使用者帳戶設定ToolStripMenuItem.Name = "使用者帳戶設定ToolStripMenuItem";
            this.使用者帳戶設定ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.使用者帳戶設定ToolStripMenuItem.Text = "使用者帳戶設定";
            this.使用者帳戶設定ToolStripMenuItem.Click += new System.EventHandler(this.使用者帳戶設定ToolStripMenuItem_Click);
            // 
            // aBCD量測數量設定ToolStripMenuItem
            // 
            this.aBCD量測數量設定ToolStripMenuItem.Name = "aBCD量測數量設定ToolStripMenuItem";
            this.aBCD量測數量設定ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.aBCD量測數量設定ToolStripMenuItem.Text = "ABCD量測數量設定";
            this.aBCD量測數量設定ToolStripMenuItem.Click += new System.EventHandler(this.aBCD量測數量設定ToolStripMenuItem_Click);
            // 
            // recipe設定ToolStripMenuItem
            // 
            this.recipe設定ToolStripMenuItem.Name = "recipe設定ToolStripMenuItem";
            this.recipe設定ToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.recipe設定ToolStripMenuItem.Text = "Recipe設定";
            this.recipe設定ToolStripMenuItem.Click += new System.EventHandler(this.recipe設定ToolStripMenuItem_Click);
            // 
            // 查詢ToolStripMenuItem
            // 
            this.查詢ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.基板歷史紀錄ToolStripMenuItem});
            this.查詢ToolStripMenuItem.Name = "查詢ToolStripMenuItem";
            this.查詢ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.查詢ToolStripMenuItem.Text = "查詢";
            // 
            // 基板歷史紀錄ToolStripMenuItem
            // 
            this.基板歷史紀錄ToolStripMenuItem.Name = "基板歷史紀錄ToolStripMenuItem";
            this.基板歷史紀錄ToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.基板歷史紀錄ToolStripMenuItem.Text = "基板歷史紀錄";
            this.基板歷史紀錄ToolStripMenuItem.Click += new System.EventHandler(this.基板歷史紀錄ToolStripMenuItem_Click);
            // 
            // 校正ToolStripMenuItem
            // 
            this.校正ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.比例尺校正ToolStripMenuItem});
            this.校正ToolStripMenuItem.Name = "校正ToolStripMenuItem";
            this.校正ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.校正ToolStripMenuItem.Text = "校正";
            // 
            // 比例尺校正ToolStripMenuItem
            // 
            this.比例尺校正ToolStripMenuItem.Name = "比例尺校正ToolStripMenuItem";
            this.比例尺校正ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.比例尺校正ToolStripMenuItem.Text = "比例尺校正";
            this.比例尺校正ToolStripMenuItem.Click += new System.EventHandler(this.比例尺校正ToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(3, 21);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.button4);
            this.splitContainer2.Panel1.Controls.Add(this.button3);
            this.splitContainer2.Panel1.Controls.Add(this.button2);
            this.splitContainer2.Panel1.Controls.Add(this.buttonAIProcessStart);
            this.splitContainer2.Panel1.Controls.Add(this.button1);
            this.splitContainer2.Panel1.Controls.Add(this.buttonCreateExcelData);
            this.splitContainer2.Panel1.Controls.Add(this.buttonJudgeCancel);
            this.splitContainer2.Panel1.Controls.Add(this.buttonJudgeOK);
            this.splitContainer2.Panel1.Controls.Add(this.buttonPanelChange);
            this.splitContainer2.Panel1.Controls.Add(this.checkBoxPause);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer2.Panel1.Controls.Add(this.buttonLogin);
            this.splitContainer2.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer2.Size = new System.Drawing.Size(493, 794);
            this.splitContainer2.SplitterDistance = 199;
            this.splitContainer2.TabIndex = 42;
            // 
            // buttonAIProcessStart
            // 
            this.buttonAIProcessStart.BackColor = System.Drawing.Color.Aqua;
            this.buttonAIProcessStart.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonAIProcessStart.Location = new System.Drawing.Point(2, 131);
            this.buttonAIProcessStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAIProcessStart.Name = "buttonAIProcessStart";
            this.buttonAIProcessStart.Size = new System.Drawing.Size(126, 48);
            this.buttonAIProcessStart.TabIndex = 58;
            this.buttonAIProcessStart.Text = "AI Process Start";
            this.buttonAIProcessStart.UseVisualStyleBackColor = false;
            this.buttonAIProcessStart.Visible = false;
            this.buttonAIProcessStart.Click += new System.EventHandler(this.buttonAIProcessStart_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Aquamarine;
            this.button1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button1.Location = new System.Drawing.Point(177, 74);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 40);
            this.button1.TabIndex = 57;
            this.button1.Text = "AI Data";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // buttonCreateExcelData
            // 
            this.buttonCreateExcelData.BackColor = System.Drawing.Color.SkyBlue;
            this.buttonCreateExcelData.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonCreateExcelData.Location = new System.Drawing.Point(2, 132);
            this.buttonCreateExcelData.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCreateExcelData.Name = "buttonCreateExcelData";
            this.buttonCreateExcelData.Size = new System.Drawing.Size(126, 48);
            this.buttonCreateExcelData.TabIndex = 56;
            this.buttonCreateExcelData.Text = "生成列表資料";
            this.buttonCreateExcelData.UseVisualStyleBackColor = false;
            this.buttonCreateExcelData.Click += new System.EventHandler(this.buttonCreateExcelData_Click);
            // 
            // buttonJudgeCancel
            // 
            this.buttonJudgeCancel.BackColor = System.Drawing.Color.Red;
            this.buttonJudgeCancel.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonJudgeCancel.Location = new System.Drawing.Point(378, 123);
            this.buttonJudgeCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonJudgeCancel.Name = "buttonJudgeCancel";
            this.buttonJudgeCancel.Size = new System.Drawing.Size(111, 59);
            this.buttonJudgeCancel.TabIndex = 55;
            this.buttonJudgeCancel.Text = "Cancel";
            this.buttonJudgeCancel.UseVisualStyleBackColor = false;
            this.buttonJudgeCancel.Click += new System.EventHandler(this.buttonJudgeCancel_Click_1);
            // 
            // buttonJudgeOK
            // 
            this.buttonJudgeOK.BackColor = System.Drawing.Color.Lime;
            this.buttonJudgeOK.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonJudgeOK.Location = new System.Drawing.Point(279, 123);
            this.buttonJudgeOK.Margin = new System.Windows.Forms.Padding(2);
            this.buttonJudgeOK.Name = "buttonJudgeOK";
            this.buttonJudgeOK.Size = new System.Drawing.Size(95, 59);
            this.buttonJudgeOK.TabIndex = 54;
            this.buttonJudgeOK.Text = "OK";
            this.buttonJudgeOK.UseVisualStyleBackColor = false;
            this.buttonJudgeOK.Click += new System.EventHandler(this.buttonJudgeOK_Click_1);
            // 
            // buttonPanelChange
            // 
            this.buttonPanelChange.BackColor = System.Drawing.Color.Lime;
            this.buttonPanelChange.Font = new System.Drawing.Font("微軟正黑體", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonPanelChange.Location = new System.Drawing.Point(8, 74);
            this.buttonPanelChange.Margin = new System.Windows.Forms.Padding(2);
            this.buttonPanelChange.Name = "buttonPanelChange";
            this.buttonPanelChange.Size = new System.Drawing.Size(90, 40);
            this.buttonPanelChange.TabIndex = 49;
            this.buttonPanelChange.Text = "AI";
            this.buttonPanelChange.UseVisualStyleBackColor = false;
            this.buttonPanelChange.Click += new System.EventHandler(this.buttonPanelAI_Click);
            // 
            // checkBoxPause
            // 
            this.checkBoxPause.AutoSize = true;
            this.checkBoxPause.Location = new System.Drawing.Point(338, 87);
            this.checkBoxPause.Name = "checkBoxPause";
            this.checkBoxPause.Size = new System.Drawing.Size(50, 16);
            this.checkBoxPause.TabIndex = 47;
            this.checkBoxPause.Text = "Pause";
            this.checkBoxPause.UseVisualStyleBackColor = true;
            this.checkBoxPause.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelUserID);
            this.groupBox2.Controls.Add(this.labelUserName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(279, 12);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(160, 70);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "使用者資訊";
            // 
            // labelUserID
            // 
            this.labelUserID.AutoSize = true;
            this.labelUserID.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.labelUserID.Location = new System.Drawing.Point(55, 38);
            this.labelUserID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUserID.Name = "labelUserID";
            this.labelUserID.Size = new System.Drawing.Size(72, 18);
            this.labelUserID.TabIndex = 6;
            this.labelUserID.Text = "00000000";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.labelUserName.Location = new System.Drawing.Point(64, 21);
            this.labelUserName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(35, 18);
            this.labelUserName.TabIndex = 5;
            this.labelUserName.Text = "XXX";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.label7.Location = new System.Drawing.Point(8, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "工號 : ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微軟正黑體", 10F);
            this.label8.Location = new System.Drawing.Point(7, 22);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 18);
            this.label8.TabIndex = 3;
            this.label8.Text = "使用者 : ";
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.SkyBlue;
            this.buttonLogin.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Bold);
            this.buttonLogin.Location = new System.Drawing.Point(279, 83);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(53, 31);
            this.buttonLogin.TabIndex = 44;
            this.buttonLogin.Text = "登入";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelTimeNow);
            this.groupBox1.Location = new System.Drawing.Point(7, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(260, 51);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "現在時間";
            // 
            // labelTimeNow
            // 
            this.labelTimeNow.AutoSize = true;
            this.labelTimeNow.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelTimeNow.Location = new System.Drawing.Point(8, 17);
            this.labelTimeNow.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelTimeNow.Name = "labelTimeNow";
            this.labelTimeNow.Size = new System.Drawing.Size(175, 20);
            this.labelTimeNow.TabIndex = 1;
            this.labelTimeNow.Text = "yyyy/HH/dd hh:mm:ss";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Aquamarine;
            this.button2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button2.Location = new System.Drawing.Point(177, 118);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 25);
            this.button2.TabIndex = 59;
            this.button2.Text = "TEST1";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Aquamarine;
            this.button3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button3.Location = new System.Drawing.Point(177, 141);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(90, 25);
            this.button3.TabIndex = 60;
            this.button3.Text = "TEST2";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Aquamarine;
            this.button4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button4.Location = new System.Drawing.Point(177, 161);
            this.button4.Margin = new System.Windows.Forms.Padding(2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 25);
            this.button4.TabIndex = 61;
            this.button4.Text = "TEST3";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1360, 742);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.splitContainer1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerNow;
        private System.Windows.Forms.Timer timerJudgeCount;
        private System.Windows.Forms.Timer timerCycle;
        private System.Windows.Forms.Timer timerCheckServer;
        private System.Windows.Forms.Timer timerDeleyCheck;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 系統設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 比例尺設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 使用者帳戶設定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查詢ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 基板歷史紀錄ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 校正ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 比例尺校正ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBCD量測數量設定ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.CheckBox checkBoxPause;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelUserID;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelTimeNow;
        private System.Windows.Forms.Button buttonPanelChange;
        private System.Windows.Forms.Timer timerAIProcess;
        public System.Windows.Forms.Button buttonCreateExcelData;
        public System.Windows.Forms.Button buttonJudgeCancel;
        public System.Windows.Forms.Button buttonJudgeOK;
        private System.Windows.Forms.ToolStripMenuItem recipe設定ToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button buttonAIProcessStart;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
    }
}

