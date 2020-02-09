using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ImageMeasureSystem
{
    public partial class frmSystemSetting : Form
    {

        string outString;
        string imageServer;
        string constr;
        string constrSample;
        string ip;
        string port;
        string userId;
        string password;
        string dpiX;
        string dpiY;
        string dtNow;
        string testMode;
        string judgeResult;
        string judgeTimeCount = "0";
        string resultPath;
        string judgeTime;
        string testServer;
        string resultImagePath;
        string cycleTime;
        //System.Windows.Forms.Timer timerJudgeCount = new System.Windows.Forms.Timer();
        string lastTime;
        string nextTime;
        string nextDeleyTime;
        string deleyTime;
        double cycleTimeSpan;
        string checking;    //是否檢查中的flag
        string deleyCheckFlag;      //是否是延遲檢查的flag
        string uploadSpcData;
        string maxImage;
        string fileServer;
        string aiServer;
        string reMeasurePath;
        string loginTablePath;
        string lineIDNow;
        string admImageSortString;
        string prod;
        string mode;    //目前設備種類，0:PIIN ABCD , 1 : LCIN
        string ibwMode;     //紀錄是否啟用IBW Mode
        string aiMode;     //紀錄是否啟用AI Mode
        string localLineID;     //紀錄LocalMode的線別ID
        string xmlSavePath;
        string waitAITime;
        string aiSamplingRate;
        string recipeDir;
      

        public frmSystemSetting()
        {
            InitializeComponent();
            
            this.Text = "系統設定";
        }
        //載入dll讀取INI
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnvalue, int buffersize, string filepath);
        private string IniFilePath;

        //呼叫委派Main UI修改控制項
        private delegate void UpdateButtonCallBack(string vaule, Button ctl);
        private void UpdateButton(string vaule, Button ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateButtonCallBack uu = new UpdateButtonCallBack(UpdateButton);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {
                if (vaule == "0")
                {
                    ctl.Text = "OFF";
                    ctl.BackColor = Color.Red;
                }
                else if (vaule == "1")
                {
                    ctl.Text = "ON";
                    ctl.BackColor = Color.Green;
                }
                
            }
        }



        //宣告StringBuilder使用GetPrivateProfileString取得ini設定
        private void GetValue(string section, string key, out string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            GetPrivateProfileString(section, key, "", stringBuilder, 1024, IniFilePath);
            value = stringBuilder.ToString();
        }

        // 寫入ini設定
        public void WriteIniFile(string section, string key, Object value)
        {
            WritePrivateProfileString(section, key, value.ToString(), IniFilePath);
        }

        public void ReadINI()
        {
            //宣告ini路徑
            IniFilePath = frmMain.IniFilePath;
            mode = frmMain.mode;
            GetValue("PATH", "ImageServer", out outString);
            imageServer = outString;
            GetValue("PATH", "FileServer", out outString);
            fileServer = outString;
            GetValue("PATH", "AIServer", out outString);
            aiServer = outString;
            GetValue("PATH", "ReMeasurePath", out outString);
            reMeasurePath = outString;
            GetValue("PATH", "RecipeDir", out outString);
            recipeDir = outString;
            GetValue("SQL Server", "Constr", out outString);
            constrSample = outString;
            GetValue("SQL Server", "IP", out outString);
            ip = outString;
            GetValue("SQL Server", "Port", out outString);
            port = outString;
            GetValue("SQL Server", "UserID", out outString);
            userId = outString;
            GetValue("SQL Server", "Password", out outString);
            password = outString;
            GetValue("PATH", "ResultPath", out outString);
            resultPath = outString;
            GetValue("PATH", "ResultImagePath", out outString);
            resultImagePath = outString;
            GetValue("PATH", "XMLSavePath", out outString);
            xmlSavePath = outString;
            GetValue("FEATURES", "ADMImageSort", out outString);
            admImageSortString = outString;
            GetValue("FEATURES", "UploadSPCData", out outString);
            uploadSpcData = outString;
            GetValue("FEATURES", "DpiX", out outString);
            dpiX = outString;
            GetValue("FEATURES", "DpiY", out outString);
            dpiY = outString;
            GetValue("FEATURES", "IBWMode", out outString);
            ibwMode = outString;
            if (ibwMode == "1")
            {
                label1IbwLineID.Visible = false;
                comboBoxIbwLineID.Visible = false;
            }
            GetValue("FEATURES", "AIMode", out outString);
            aiMode = outString;

            GetValue("FEATURES", "LocalLineID", out outString);
            localLineID = outString;
            GetValue("FEATURES", "WaitAITime", out outString);
            waitAITime = outString;
            GetValue("FEATURES", "AISamplingRate", out outString);
            aiSamplingRate = outString;
            UpdateButton(ibwMode, buttonIbwMode);
            UpdateButton(aiMode, buttonAIMode);
            textBoxImageServer.Text = imageServer;
            textBoxFileServer.Text = fileServer;
            textBoxAIServer.Text = aiServer;
            textBoxResultPath.Text = resultPath;
            textBoxResultImagePath.Text = resultImagePath;
            textBoxReMeasurePath.Text = reMeasurePath;
            textBoxADMImageSort.Text = admImageSortString;
            textBoxSQLIP.Text = ip;
            textBoxSQLPort.Text = port;
            textBoxSQLUserID.Text = userId;
            textBoxSQLPassword.Text = password;
            textBoxDpiX.Text = dpiX;
            textBoxDpiY.Text = dpiY;
            UpdateButton(uploadSpcData, buttonUploadSpcData);
            comboBoxIbwLineID.SelectedItem = localLineID;
            textBoxXmlSavePath.Text = xmlSavePath;
            textBoxWaitAITime.Text = waitAITime;
            textBoxAISamplingRate.Text = aiSamplingRate;
            textBoxRecipeDir.Text = recipeDir;

            if (mode == "0")
            {

            }
            else if (mode == "1")
            {
                //依照機台種類調整各項設定權限
                textBoxImageServer.Enabled = false;
                textBoxFileServer.Enabled = false;
                textBoxAIServer.Enabled = true;
                textBoxSQLIP.Enabled = false;
                textBoxSQLPort.Enabled = false;
                textBoxSQLUserID.Enabled = false;
                textBoxSQLPassword.Enabled = false;
            }
            
        }

        private void buttonJudgeOK_Click(object sender, EventArgs e)
        {
            //show Messageox詢問是否確定要儲存
            DialogResult result = MessageBox.Show("是否確定要儲存變更設定?", "確認視窗", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                WriteINI();
            }
        }
        private void WriteINI()
        {
            //宣告ini路徑
            IniFilePath = frmMain.IniFilePath;
            //先檢查是否有設定是空白
            var textBoxs1 = groupBox1.Controls.OfType<TextBox>();
            var textBoxs2 = groupBox2.Controls.OfType<TextBox>();
            var textBoxs3 = groupBox3.Controls.OfType<TextBox>();
            foreach (TextBox conTemp in textBoxs1)
            {
                if (conTemp is TextBox)
                {
                    if (conTemp.Text.Replace(" ", "") == "" || conTemp.Text.Replace(" ", "") == null)
                    {
                        MessageBox.Show("設定不能為空白，請確認");
                        return;
                    }
                }
            }
            foreach (TextBox conTemp in textBoxs2)
            {
                if (conTemp is TextBox)
                {
                    if (conTemp.Text.Replace(" ", "") == "" || conTemp.Text.Replace(" ", "") == null)
                    {
                        MessageBox.Show("設定不能為空白，請確認");
                        return;
                    }
                }
            }
            foreach (TextBox conTemp in textBoxs3)
            {
                if (conTemp is TextBox)
                {
                    if (conTemp.Text.Replace(" ", "") == "" || conTemp.Text.Replace(" ", "") == null)
                    {
                        MessageBox.Show("設定不能為空白，請確認");
                        return;
                    }
                }
            }
            imageServer = textBoxImageServer.Text;
            fileServer = textBoxFileServer.Text;
            aiServer = textBoxAIServer.Text;
            resultPath = textBoxResultPath.Text;
            resultImagePath = textBoxResultImagePath.Text;
            reMeasurePath = textBoxReMeasurePath.Text;
            admImageSortString = textBoxADMImageSort.Text;
            ip = textBoxSQLIP.Text;
            port = textBoxSQLPort.Text;
            userId = textBoxSQLUserID.Text;
            password = textBoxSQLPassword.Text;
            dpiX = textBoxDpiX.Text;
            dpiY = textBoxDpiY.Text;
            localLineID = comboBoxIbwLineID.SelectedItem.ToString();
            xmlSavePath = textBoxXmlSavePath.Text;
            waitAITime = textBoxWaitAITime.Text;
            aiSamplingRate = textBoxAISamplingRate.Text;
            recipeDir = textBoxRecipeDir.Text;
            if (buttonUploadSpcData.Text == "OFF")
            {
                uploadSpcData = "0";
            }
            else if (buttonUploadSpcData.Text == "ON")
            {
                uploadSpcData = "1";
            }
            if (buttonIbwMode.Text == "OFF")
            {
                ibwMode = "0";
            }
            else if (buttonIbwMode.Text == "ON")
            {
                ibwMode = "1";
            }
            if (buttonAIMode.Text == "OFF")
            {
                aiMode = "0";
            }
            else if (buttonAIMode.Text == "ON")
            {
                aiMode = "1";
            }

            WriteIniFile("PATH", "ImageServer", imageServer);
            WriteIniFile("PATH", "FileServer", fileServer);
            WriteIniFile("PATH", "AIServer", aiServer);
            WriteIniFile("PATH", "ReMeasurePath", reMeasurePath);
            WriteIniFile("SQL Server", "Constr", constrSample);
            WriteIniFile("SQL Server", "IP", ip);
            WriteIniFile("SQL Server", "Port", port);
            WriteIniFile("SQL Server", "UserID", userId);
            WriteIniFile("SQL Server", "Password", password);
            WriteIniFile("PATH", "ResultPath", resultPath);
            WriteIniFile("PATH", "ResultImagePath", resultImagePath);
            WriteIniFile("PATH", "XMLSavePath", xmlSavePath);
            WriteIniFile("PATH", "RecipeDir", recipeDir);
            WriteIniFile("FEATURES", "ADMImageSort", admImageSortString);
            WriteIniFile("FEATURES", "UploadSPCData", uploadSpcData);
            WriteIniFile("FEATURES", "DpiX", dpiX);
            WriteIniFile("FEATURES", "DpiY", dpiY);
            WriteIniFile("FEATURES", "IBWMode", ibwMode);
            WriteIniFile("FEATURES", "AIMode", aiMode);
            WriteIniFile("FEATURES", "LocalLineID", localLineID);
            WriteIniFile("FEATURES", "WaitAITime", waitAITime);
            WriteIniFile("FEATURES", "AISamplingRate", aiSamplingRate);
            MessageBox.Show("儲存完成");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonUploadSpcData.Text == "OFF")
            {
                UpdateButton("1", buttonUploadSpcData);
            }
            else
            {
                UpdateButton("0", buttonUploadSpcData);
            }
        }

        private void buttonIbwMode_Click(object sender, EventArgs e)
        {
            if (buttonIbwMode.Text == "OFF")
            {
                UpdateButton("1", buttonIbwMode);
                comboBoxIbwLineID.Visible = false;
                label1IbwLineID.Visible = false;
            }
            else
            {
                UpdateButton("0", buttonIbwMode);
                comboBoxIbwLineID.Visible = true;
                label1IbwLineID.Visible = true;
            }
        }

        private void buttonAIMode_Click(object sender, EventArgs e)
        {
            if (buttonAIMode.Text == "OFF")
            {
                UpdateButton("1", buttonAIMode);
            }
            else
            {
                UpdateButton("0", buttonAIMode);
            }
        }
    }
}
