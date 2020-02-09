using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace ImageMeasureSystem
{
    public partial class frmRecipe : Form
    {
        string mode;
        string recipeDir;
        string outString;
        public frmRecipe()
        {
            InitializeComponent();
        }

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
                    ctl.Text = "One Defect Type";
                    ctl.BackColor = Color.Red;
                }
                else if (vaule == "1")
                {
                    ctl.Text = "Many Defect Type";
                    ctl.BackColor = Color.Green;
                }

            }
        }

        //載入dll讀取INI
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnvalue, int buffersize, string filepath);

        //宣告StringBuilder使用GetPrivateProfileString取得ini設定
        private void GetValue(string section, string key, out string value, string filePath)
        {
            StringBuilder stringBuilder = new StringBuilder();
            GetPrivateProfileString(section, key, "", stringBuilder, 1024, filePath);
            value = stringBuilder.ToString();
        }
        // 寫入ini設定
        public void WriteIniFile(string section, string key, Object value, string filePath)
        {
            WritePrivateProfileString(section, key, value.ToString(), filePath);
        }
        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            mode = frmMain.mode;
            
            if (mode == "0")
            {
                recipeDir = Path.Combine(frmMain.recipeDir, "PIIN");
            }
            else if (mode == "1")
            {
                recipeDir = Path.Combine(frmMain.recipeDir, "LCIN");
                for(int i = 1; i < 6; i++)
                {
                    if (Directory.Exists(Path.Combine(recipeDir, "TPAB0" + i.ToString())))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(recipeDir, "TPAB0" + i.ToString()));
                        FileInfo[] recipeList = dirInfo.GetFiles("*.RCP");
                        if (recipeList.Count() > 0)
                        {
                            foreach (FileInfo file in recipeList)
                            {
                                string fileName = file.Name.Split('.')[0];


                                GetValue("Prod Data", "ProdName", out outString, file.FullName);
                                string prodName = outString;
                                GetValue("Prod Data", "PID", out outString, file.FullName);
                                string pid = outString;
                                GetValue("Prod Data", "LineID", out outString, file.FullName);
                                string lineID = outString;

                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = pid;
                                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = lineID;
                                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = prodName;
                            }
                        }
                    }
                    
                }
                
            }
            
        }
        //將字典寫入Table
        private void SaveTable()
        {
            //檢查資料是否有空白並清除空格
            foreach(Control ctrl in this.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (ctrl.Text == null || ctrl.Text.Replace(" ", "") == "")
                    {
                        MessageBox.Show("存在空白資料，請確認");
                        return;
                    }
                    else
                    {
                        ctrl.Text = ctrl.Text.Replace(" ", "");
                    }
                }
            }

            //寫入資料
            string filePath = Path.Combine(recipeDir, textBoxLineID.Text, textBoxPID.Text + ".RCP");
            if (File.Exists(filePath))
            {
                DialogResult dr = MessageBox.Show("Recipe已存在，是否要覆蓋?", "確認視窗", MessageBoxButtons.OKCancel);
                if (dr != DialogResult.OK)
                    return;
            }
            WriteIniFile("Prod Data", "ProdName", textBoxProdName.Text, filePath);
            WriteIniFile("Prod Data", "PID", textBoxPID.Text, filePath);
            WriteIniFile("Prod Data", "LineID", textBoxLineID.Text, filePath);

            
            foreach(Control ctrl in groupBoxHeadSetting.Controls)
            {
                if (ctrl is TextBox)
                    if (ctrl.Text.Replace(" ", "") != "" && ctrl.Text.Replace(" ", "").Length < 2)
                        ctrl.Text = "0" + ctrl.Text;
                        
            }
            foreach(Control ctrl in groupBoxSPECSetting.Controls)
            {
                if (ctrl is TextBox)
                {
                    if (!CheckDouble(ctrl.Text))
                    {
                        MessageBox.Show("There is not Double Exist in SPEC Setting ");
                        return;
                    }
                }
                    
            }
            WriteIniFile("Head Setting", "Head1", textBoxHead1.Text, filePath);
            WriteIniFile("Head Setting", "Head2", textBoxHead2.Text, filePath);
            WriteIniFile("Head Setting", "Head3", textBoxHead3.Text, filePath);
            WriteIniFile("Head Setting", "Head4", textBoxHead4.Text, filePath);
            WriteIniFile("Head Setting", "Head5", textBoxHead5.Text, filePath);
            WriteIniFile("Head Setting", "Head6", textBoxHead6.Text, filePath);
            WriteIniFile("Head Setting", "Head7", textBoxHead7.Text, filePath);
            WriteIniFile("Head Setting", "Head8", textBoxHead8.Text, filePath);
            WriteIniFile("Head Setting", "Head9", textBoxHead9.Text, filePath);
            WriteIniFile("Head Setting", "Head10", textBoxHead10.Text, filePath);

            WriteIniFile("SPEC Setting", "Normal_OOS_Max", textBoxNormalOOSMax.Text, filePath);
            WriteIniFile("SPEC Setting", "Normal_OOS_Min", textBoxNormalOOSMin.Text, filePath);
            WriteIniFile("SPEC Setting", "Normal_OOC_Max", textBoxNormalOOCMax.Text, filePath);
            WriteIniFile("SPEC Setting", "Normal_OOC_Min", textBoxNormalOOCMin.Text, filePath);

            WriteIniFile("SPEC Setting", "Overlap_OOS_Max", textBoxOverlapOOSMax.Text, filePath);
            WriteIniFile("SPEC Setting", "Overlap_OOS_Min", textBoxOverlapOOSMin.Text, filePath);
            WriteIniFile("SPEC Setting", "Overlap_OOC_Max", textBoxOverlapOOCMax.Text, filePath);
            WriteIniFile("SPEC Setting", "Overlap_OOC_Min", textBoxOverlapOOCMin.Text, filePath);

            WriteIniFile("SPEC Setting", "Corner_OOS_Max", textBoxCornerOOSMax.Text, filePath);
            WriteIniFile("SPEC Setting", "Corner_OOS_Min", textBoxCornerOOSMin.Text, filePath);
            WriteIniFile("SPEC Setting", "Corner_OOC_Max", textBoxCornerOOCMax.Text, filePath);
            WriteIniFile("SPEC Setting", "Corner_OOC_Min", textBoxCornerOOCMin.Text, filePath);

            WriteIniFile("AI Model Setting", "Corner_AI_Model", textBoxModelCorner.Text, filePath);
            WriteIniFile("AI Model Setting", "Normal_AI_Model", textBoxModelNormal.Text, filePath);
            WriteIniFile("AI Model Setting", "Overlap_AI_Model", textBoxModelOverlap.Text, filePath);

            WriteIniFile("Sampling Rate Setting", "AI_Sampling_Rate", textBoxAISamplingRate.Text, filePath);
            
            if (buttonAIDefectCodeType.Text == "One Defect Type")
            {
                WriteIniFile("AI DefectCode Type", "DefectCode_Type", "0", filePath);
            }
            else
            {
                WriteIniFile("AI DefectCode Type", "DefectCode_Type", "1", filePath);
            }
            MessageBox.Show("儲存完成");
        }
        

        //檢查是否為合法的Double
        private bool CheckDouble(string str)
        {
            try
            {
                Convert.ToDouble(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

     

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定要儲存Recipe?", "確認視窗", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                SaveTable();
                ReadTable();
                
            }
        }



        //dataGridView滑鼠點擊事件
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((DataGridView)sender == dataGridView1 && e.RowIndex >= 0)
            {
                string filePath = Path.Combine(recipeDir, dataGridView1.Rows[e.RowIndex].Cells["lineID"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["pid"].Value.ToString() + ".RCP");
                ReadRecipeData(filePath);
            }

        }
        private void ReadRecipeData(string filePath)
        {
            if (File.Exists(filePath))
            {
                

                GetValue("Prod Data", "ProdName", out outString, filePath);
                textBoxProdName.Text = outString;
                GetValue("Prod Data", "PID", out outString, filePath);
                textBoxPID.Text = outString;
                GetValue("Prod Data", "LineID", out outString, filePath);
                textBoxLineID.Text = outString;

                GetValue("Head Setting", "Head1", out outString, filePath);
                textBoxHead1.Text = outString;
                GetValue("Head Setting", "Head2", out outString, filePath);
                textBoxHead2.Text = outString;
                GetValue("Head Setting", "Head3", out outString, filePath);
                textBoxHead3.Text = outString;
                GetValue("Head Setting", "Head4", out outString, filePath);
                textBoxHead4.Text = outString;
                GetValue("Head Setting", "Head5", out outString, filePath);
                textBoxHead5.Text = outString;
                GetValue("Head Setting", "Head6", out outString, filePath);
                textBoxHead6.Text = outString;
                GetValue("Head Setting", "Head7", out outString, filePath);
                textBoxHead7.Text = outString;
                GetValue("Head Setting", "Head8", out outString, filePath);
                textBoxHead8.Text = outString;
                GetValue("Head Setting", "Head9", out outString, filePath);
                textBoxHead9.Text = outString;
                GetValue("Head Setting", "Head10", out outString, filePath);
                textBoxHead10.Text = outString;

                GetValue("SPEC Setting", "Normal_OOS_Max", out outString, filePath);
                textBoxNormalOOSMax.Text = outString;
                GetValue("SPEC Setting", "Normal_OOS_Min", out outString, filePath);
                textBoxNormalOOSMin.Text = outString;
                GetValue("SPEC Setting", "Normal_OOC_Max", out outString, filePath);
                textBoxNormalOOCMax.Text = outString;
                GetValue("SPEC Setting", "Normal_OOC_Min", out outString, filePath);
                textBoxNormalOOCMin.Text = outString;

                GetValue("SPEC Setting", "Overlap_OOS_Max", out outString, filePath);
                textBoxOverlapOOSMax.Text = outString;
                GetValue("SPEC Setting", "Overlap_OOS_Min", out outString, filePath);
                textBoxOverlapOOSMin.Text = outString;
                GetValue("SPEC Setting", "Overlap_OOC_Max", out outString, filePath);
                textBoxOverlapOOCMax.Text = outString;
                GetValue("SPEC Setting", "Overlap_OOC_Min", out outString, filePath);
                textBoxOverlapOOCMin.Text = outString;

                GetValue("SPEC Setting", "Corner_OOS_Max", out outString, filePath);
                textBoxCornerOOSMax.Text = outString;
                GetValue("SPEC Setting", "Corner_OOS_Min", out outString, filePath);
                textBoxCornerOOSMin.Text = outString;
                GetValue("SPEC Setting", "Corner_OOC_Max", out outString, filePath);
                textBoxCornerOOCMax.Text = outString;
                GetValue("SPEC Setting", "Corner_OOC_Min", out outString, filePath);
                textBoxCornerOOCMin.Text = outString;

                GetValue("AI Model Setting", "Corner_AI_Model", out outString, filePath);
                textBoxModelCorner.Text = outString;
                GetValue("AI Model Setting", "Normal_AI_Model", out outString, filePath);
                textBoxModelNormal.Text = outString;
                GetValue("AI Model Setting", "Overlap_AI_Model", out outString, filePath);
                textBoxModelOverlap.Text = outString;

                GetValue("Sampling Rate Setting", "AI_Sampling_Rate", out outString, filePath);
                textBoxAISamplingRate.Text = outString;
                
                GetValue("AI DefectCode Type", "DefectCode_Type", out outString, filePath);
                string strTemp = outString;
                if (strTemp == "1")
                {
                    UpdateButton("1", buttonAIDefectCodeType);
                }
                else
                {
                    UpdateButton("0", buttonAIDefectCodeType);
                }
            }
            else
            {
                MessageBox.Show("Recipe File Not Found in : " + filePath);
            }
        }
        private void frmRecipe_Load(object sender, EventArgs e)
        {

        }

        private void buttonAIDefectCodeType_Click(object sender, EventArgs e)
        {
            if (buttonAIDefectCodeType.Text == "One Defect Type")
            {
                UpdateButton("1", buttonAIDefectCodeType);
            }
            else
            {
                UpdateButton("0", buttonAIDefectCodeType);
            }
        }


    }
    public class RecpieLCIN
    {
        public string prodName { get; set; }
        public string pid { get; set; }
        public string lineID { get; set; }
        public string head1 { get; set; }
        public string head2 { get; set; }
        public string head3 { get; set; }
        public string head4 { get; set; }
        public string head5 { get; set; }
        public string head6 { get; set; }
        public string head7 { get; set; }
        public string head8 { get; set; }
        public string head9 { get; set; }
        public string head10 { get; set; }
        public List<string> headList { get; set; }
        public string normalOOSMax { get; set; }
        public string normalOOSMin { get; set; }
        public string normalOOCMax { get; set; }
        public string normalOOCMin { get; set; }
        public string overlapOOSMax { get; set; }
        public string overlapOOSMin { get; set; }
        public string overlapOOCMax { get; set; }
        public string overlapOOCMin { get; set; }
        public string cornerOOSMax { get; set; }
        public string cornerOOSMin { get; set; }
        public string cornerOOCMax { get; set; }
        public string cornerOOCMin { get; set; }
        public string aiModelCorner { get; set; }
        public string aiModelOverlap { get; set; }
        public string aiModelNormal { get; set; }
        public string aiSamplingRate { get; set; }
        public string aiDefectCodeType { get; set; }    //標示此ID的AI DefectCode是單一種還是多種
    }
}
