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
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Xml;
using LCIN_AI;
using System.Reflection;


namespace ImageMeasureSystem
{
    public partial class frmMain : Form
    {
        frmImage frmImage = new frmImage();
        frmSample frmSample = new frmSample();
        frmABCD frmABCD = new frmABCD();
        frmLCIN frmLCIN = new frmLCIN();
        frmAI frmAI = new frmAI();
        frmPanelTA frmPanelTA = new frmPanelTA();
        frmPanelAI frmPanelAI = new frmPanelAI();
        frmAIData frmAIData = new frmAIData();
        frmTypeChose frmTypeChose = new frmTypeChose();
        frmSystemSetting frmSystemSetting = new frmSystemSetting();
        frmRateSetting frmRateSetting = new frmRateSetting();
        funGenerateXml funGenerateXml = new funGenerateXml();
        frmHeadSetting frmHeadSetting = new frmHeadSetting();
        frmPidSetting frmPidSetting = new frmPidSetting();
        frmExcelCopyData frmExcelCopyData = new frmExcelCopyData();
        frmSpecSetting frmSpecSetting = new frmSpecSetting();
        frmSpecSettingABCD frmSpecSettingABCD = new frmSpecSettingABCD();
        frmUserID frmUserID = new frmUserID();
        frmABCDNumSetting frmABCDNumSetting = new frmABCDNumSetting();
        frmAIModelSetting frmAIModelSetting = new frmAIModelSetting();
        frmRecipe frmRecipe = new frmRecipe();
        AI _ai = new AI();
        Thread threadWaitForAIOutput;
        Thread threadMain;  //手動量測主執行緒
        Thread threadAIProcess; //AI流程主執行緒
        Thread threadAICheck;   //確認是否有AI資料需要執行的執行緒
        Thread threadAIProcessListen;   //監聽確認AI流程是否完成
        public static bool threadAIProcessCompleteFlag = false;
        string outString;
        string imageServer;
        string constr;
        string constrSample;
        string ip;

        int aiSamplingCount = 0;    //Ai Sampling計數器
        Stopwatch _timerAI = new Stopwatch();

        string port;
        string userId;
        string password;
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
        bool checking = false;    //TA是否檢查中的flag
        bool checkingAI = false;    // //標記是否正在執行AIProcess
        string deleyCheckFlag;      //是否是延遲檢查的flag
        public static string dpiX;    //紀錄量測圖片的Picturebox寬
        public static string dpiY;    //紀錄量測圖片的Picturebox高
        public static string ibwMode;   //紀錄是否啟用IBW模式
        public static string aiMode;   //紀錄是否啟用AI模式
        public static string localLineID;   //紀錄是Local模式的線別編號
        public static string IniFilePath;
        public static string fixMode = "0";
        public static string completeFlag = "0";  //紀錄是否已看完全部圖片的flag
        public static string recipeDir;
        string uploadSpcData;
        string maxImage;
        string fileServer;
        public static string aiServer;
        string waitAITime;
        string reMeasurePath;
        string aiSamplingRate;
        string loginTablePath;
        public static string lineIDNow;   //紀錄TA主線的全域變數
        public static string lineIDNowAI;   //紀錄AI主線的全域變數
        public static string recipeIDNow;   //紀錄目前recipeID的全域變數
        public static string unitIDNow; //紀錄TA子機的全域變數
        public static string unitIDNowAI; //紀錄AI子機的全域變數
        public static string glassDirNow;     //紀錄TA讀取的image Dir
        public static string glassDirNowAI;     //紀錄AI讀取的image Dir
        public static string prodNow;
        public static string prodNowAI;
        public static string pidNow;
        public static string pidNowAI;
        string operationNum;
        public static string mode;    //目前設備種類，0:PIIN ABCD , 1 : LCIN
        string overSpecFlag;    //目前TA量測資料是否OOS OOC Flag 0:無, 1:OOC, 2:OOS
        public static string overSpecAIFlag;    //目前AI量測資料是否OOS OOC Flag 0:無, 1:OOC, 2:OOS
        string admImageSortString;
        string xmlSavePath; //XML輸出路徑
        public static string userID = null;
        public static string passWord = null;
        public static string glassIDNow = null;     //紀錄目前TA處理的GLASSID
        public static string glassIDNowAI = null;   //紀錄目前AI處理的GLASSID
        public static string recipeIniFile = null;
        List<string> releaseTable = new List<string> { };
        List<string> deleyList = new List<string> { };
        List<List<string>> idList = new List<List<string>> { }; 
        List<string> admImageSort = new List<string> { };   //ADM圖檔排序順序設定
        public static List<string> imagePathList = new List<string> { };
        public static List<string> imagePathListAI = new List<string> { };
        public static List<List<string>> measureData = new List<List<string>> { };
        public static List<List<string>> measureDataSummary = new List<List<string>> { };
        public static List<MeasureDataFromLCIN> measureDataFromLCIN = new List<MeasureDataFromLCIN>();
        //存放副程式frmPanelTA的讀取GLASS DATA請求
        public static ReadRequest readRequest = null;


        //Login Table用字典
        public static Dictionary<string, List<string>> loginTable = new Dictionary<string, List<string>>();

        //ADMRate用字典
        public static Dictionary<string, string> admRate = new Dictionary<string, string>();
     
        //GlassID與圖片路徑對應關係的字典
        public static Dictionary<string, string> imageDirDict = new Dictionary<string, string>();
        //GlassID與圖片路徑對應關係的字典
        public static Dictionary<string, string> sealUnitDict = new Dictionary<string, string>();

        //各產品使用的SPEC字典forPIIN
        public static Dictionary<string, PIINSpec> specSettingABCD = new Dictionary<string, PIINSpec>();
        //ABCD各產品使用的量測模品數量的字典
        public static Dictionary<string, string> aBCDCount = new Dictionary<string, string>();
        //待量測id的字典
        public static Dictionary<string, WaitMeasureID> waitMeasureDict = new Dictionary<string, WaitMeasureID>();
        //LCIN Recipe資料的字典 key = pid + lineid
        public static Dictionary<string, RecpieLCIN> recipeDict = new Dictionary<string, RecpieLCIN>();

        //紀錄Sampling Rate目前Count的值的數值的字典
        public static Dictionary<string, int> samplingCount = new Dictionary<string, int>();

        

        public frmMain()        {
            InitializeComponent();
            //宣告ini路徑
            IniFilePath = Application.StartupPath + "\\ImageMeasureSystem.ini";
            
            //註冊timerJudgeCount;等待人員判定的計時器的Tick事件
            this.timerJudgeCount.Tick += new EventHandler(TimerJudgeCount_Tick);
            //註冊timerNow;現在時間的Tick事件
            this.timerNow.Tick += new EventHandler(TimerNow_Tick);
            //註冊timerCycle;啟動時間的Tick事件
            this.timerCycle.Tick += new EventHandler(TimerCycle_Tick);
            ////註冊timerCheckServer;定時檢查Server連線的Tick事件
            //this.timerCheckServer.Tick += new EventHandler(TimerCheckServer_Tick);
            //註冊timerDelayCheck;延遲提醒人員量測觸發計時器的Tick事件
            this.timerDeleyCheck.Tick += new EventHandler(TimerDeleyCheck_Tick);
            //TimerRefresh;更新datagridview計時器的Tick事件
            this.timerRefresh.Tick += new EventHandler(TimerRefresh_Tick);
            //TimerAIProcess;觸發AI自動量測流程計時器的Tick事件
            this.timerAIProcess.Tick += new EventHandler(TimerAIProcess_Tick);

            //宣告主視窗關閉事件
            this.FormClosing += new FormClosingEventHandler(mainForm_Closing);
            //宣告frmSystemSetting視窗關閉事件
            frmSystemSetting.FormClosed += new FormClosedEventHandler(frmSystemSetting_Closed);
            //宣告frmSystemSetting視窗關閉事件
            frmRateSetting.FormClosed += new FormClosedEventHandler(frmRateSetting_Closed);
            //宣告frmHeadSetting視窗關閉事件
            frmHeadSetting.FormClosed += new FormClosedEventHandler(frmHeadSetting_Closed);
            //宣告frmPidSetting視窗關閉事件
            frmRecipe.FormClosed += new FormClosedEventHandler(frmRecipe_Closed);
            //宣告frmSpecSetting視窗關閉事件
            frmSpecSetting.FormClosed += new FormClosedEventHandler(frmSpecSetting_Closed);
            //宣告frmSpecSettingABCD視窗關閉事件
            frmSpecSettingABCD.FormClosed += new FormClosedEventHandler(frmSpecSettingABCD_Closed);
            //宣告frmUserID視窗關閉事件
            frmUserID.FormClosed += new FormClosedEventHandler(frmUserID_Closed);
            //宣告frmABCDNumSetting視窗關閉事件
            frmABCDNumSetting.FormClosed += new FormClosedEventHandler(frmABCDNumSetting_Closed);
            //宣告frmABCDNumSetting視窗關閉事件
            frmAIModelSetting.FormClosed += new FormClosedEventHandler(frmAIModelSetting_Closed);
            //版本號
            this.Text = "LCD4 Test Image Measure System" + "    " + "[v2.0.9 Beta build 20200207]";

            //// 找出字體大小,並算出比例
            //float dpiX, dpiY;
            //Graphics graphics = this.CreateGraphics();
            //dpiX = graphics.DpiX;
            //dpiY = graphics.DpiY;
            //int intPercent = (dpiX == 96) ? 100 : (dpiX == 120) ? 125 : 150;
            //if (intPercent == 100)
            //{
            //    this.Width = 1693;
            //    this.Height = 1050;
            //    this.splitContainer1.Height = 1010;
            //    this.splitContainer1.Width = 1675;
            //    this.splitContainer1.SplitterDistance = 523;
            //}
            this.KeyPreview = true; //在此視窗為Focus時才偵測
        }
        //等待人員判定的計時器的Tick事件
        private void TimerJudgeCount_Tick(object sender, EventArgs e)
        {
            if (frmPanelTA.dataGridViewIdList.RowCount > 0)
            {
                //CallWarningForm();
            }

        }
        //等待人員判定延遲的計時器的Tick事件
        private void TimerDeleyCheck_Tick(object sender, EventArgs e)
        {
            if (!checking && timerJudgeCount.Enabled == false)
            {
                TimerJudgeCount("ON");
            }
            else if (checking && timerJudgeCount.Enabled == true)
            {
                TimerJudgeCount("OFF");
            }

        }
        //計時器觸發事件:更新dataGridView
        private void TimerRefresh_Tick(object sender, EventArgs e)
        {

            if (measureData.Count > 0)
            {
                if (mode == "0")
                {
                    if (measureData[0][0] == "A" || measureData[0][0] == "B" || measureData[0][0] == "C" || measureData[0][0] == "D")
                    {
                        ReWriteDataGridView(measureData, "0");
                    }
                    else if (measureData[0][0] != "A" && measureData[0][0] != "B" && measureData[0][0] != "C" && measureData[0][0] != "D")
                    {
                        ReWriteDataGridView(measureData, "1");
                    }
                    //檢查規格並標記顏色
                    overSpecFlag = CheckUploadDataSpec(DataSourceType.TA);
                }
                //結束後清除list
                measureData.Clear();
            }
            if (measureDataFromLCIN.Count() > 0)
            {
                if (mode == "1")
                {
                    foreach (MeasureDataFromLCIN measureData in measureDataFromLCIN.ToArray())
                    {
                        if (measureData.dataSourceFromAIorTA == DataSourceType.TA)   //此資料為TA量測
                        {
                            string panelCount = measureData.panelCount;
                            string measureType = measureData.measureType;

                            for (int x = 0; x < frmPanelTA.dataGridViewLCIN1.RowCount; x++) //寫入panelTa
                            {
                                if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() == panelCount)
                                {
                                    if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString().Split(' ')[0] == measureType)
                                    {
                                        if (measureType != "Corner")
                                        {
                                            frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value = Math.Round(Convert.ToDouble(measureData.measureData[0]) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                                        }
                                        else if (measureType == "Corner")
                                        {
                                            frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value = Math.Round(Convert.ToDouble(measureData.measureData[0]) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                                            frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value = Math.Round(Convert.ToDouble(measureData.measureData[1]) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                                        }
                                    }
                                }
                            }
                            if (!measureData.beChangedFlag && aiMode == "1")    //寫入AI原始資料
                            {
                                for (int x = 0; x < frmAIData.dataGridViewLCIN1.RowCount; x++)
                                {
                                    if (frmAIData.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() == panelCount)
                                    {
                                        if (frmAIData.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString().Split(' ')[0] == measureType)
                                        {
                                            if (measureType != "Corner")
                                            {
                                                frmAIData.dataGridViewLCIN1.Rows[x].Cells[2].Value = Math.Round(Convert.ToDouble(measureData.measureData[0]) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                                            }
                                            else if (measureType == "Corner")
                                            {
                                                frmAIData.dataGridViewLCIN1.Rows[x].Cells[2].Value = Math.Round(Convert.ToDouble(measureData.measureData[0]) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                                                frmAIData.dataGridViewLCIN1.Rows[x].Cells[3].Value = Math.Round(Convert.ToDouble(measureData.measureData[1]) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                                            }
                                        }
                                    }
                                }
                            }

                            //檢查規格並標記顏色
                            overSpecFlag = CheckUploadDataSpec(DataSourceType.TA);

                        }
                        else if (measureData.dataSourceFromAIorTA == DataSourceType.AI)  //此資料為AI自動量測
                        {
                            string panelCount = measureData.panelCount;
                            string measureType = measureData.measureType;

                            for (int x = 0; x < frmPanelAI.dataGridViewLCIN1.RowCount; x++) //寫入panelAI
                            {
                                if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() == panelCount)
                                {
                                    if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString().Split(' ')[0] == measureType)
                                    {
                                        if (measureType != "Corner")
                                        {
                                            frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value = Math.Round(Convert.ToDouble(measureData.measureData[0]) * Convert.ToDouble(admRate[lineIDNowAI]), 3).ToString();
                                        }
                                        else if (measureType == "Corner")
                                        {
                                            frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value = Math.Round(Convert.ToDouble(measureData.measureData[0]) * Convert.ToDouble(admRate[lineIDNowAI]), 3).ToString();
                                            frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value = Math.Round(Convert.ToDouble(measureData.measureData[1]) * Convert.ToDouble(admRate[lineIDNowAI]), 3).ToString();
                                        }
                                    }
                                }
                            }
                            //檢查規格並標記顏色
                            overSpecAIFlag = CheckUploadDataSpec(DataSourceType.AI);
                        }
                        
                        
                        measureDataFromLCIN.Remove(measureData);    //清除資料
                    }

                }
            }
            
                
            
        }
  
        private void TimerNow_Tick(object sender, EventArgs e)
        {
            labelTimeNow.Text = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");
        }
        //計時器觸發事件:監控是否有待確認ID以及是否有讀取需求
        private void TimerCycle_Tick(object sender, EventArgs e)
        {
            timerCycle.Stop();  //暫停計時器避免重複觸發

            //先確認是否有讀取glass資料需求
            if (readRequest != null)
            {
                DialogResult result = MessageBox.Show("是否要讀取GlassID : " + readRequest.glassID + " 的影像", "確認視窗", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    ReadGlassData(readRequest);
                    readRequest = null;
                }
                else
                {
                    readRequest = null;
                }
                
            }
            string lineID = "";
            string glassID = "";
            string prod = "";
            string pid = "";
            List<WaitMeasureID> waitMeasureIDList = new List<WaitMeasureID> { };
            idList.Clear();
            
            if (checkBoxPause.Checked == false)
            {
                if (mode == "0")
                {
                    //輪巡ABCD資料夾底下各線的資料夾
                    foreach (string dirTemp in Directory.GetDirectories(reMeasurePath))
                    {
                        lineID = Path.GetFileNameWithoutExtension(dirTemp);
                        if (Directory.GetDirectories(dirTemp).Count() > 0)
                        {
                            foreach (string glassDirTemp in Directory.GetDirectories(dirTemp))
                            {
                                glassID = Path.GetFileNameWithoutExtension(glassDirTemp);
                                prod = GetProd(glassID, lineID)[0];
                                _ImageList images = LoadImageList(glassID, lineID);
                                if (!waitMeasureDict.ContainsKey(glassID))
                                    waitMeasureDict.Add(glassID, new WaitMeasureID
                                    {
                                        glassID = glassID,
                                        lineID = lineID,
                                        prod = prod,
                                        aiDataExistFlag = true, 
                                        imageNameListABCD = images.imageNameListABCD
                                    });
                                //List<string> id = new List<string> { lineID, prod, glassID };
                                //idList.Add(id);
                            }
                        }
                    }
                    if (waitMeasureDict.Count > 0)
                    {
                        frmPanelTA.dataGridViewIdList.Rows.Clear();
                        foreach (KeyValuePair<string, WaitMeasureID> listTemp in waitMeasureDict.ToArray())
                        {
                            frmPanelTA.dataGridViewIdList.Rows.Add();
                            frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[0].Value = listTemp.Value.lineID;
                            frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[1].Value = listTemp.Value.prod;
                            frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[2].Value = listTemp.Value.glassID;
                        }
                    }
                }
                else if (mode == "1")
                {
                    
                    string reMeasurePath2 = Path.Combine(reMeasurePath, DateTime.Now.ToString("yyyy_M_d"));
                    string reMeasurePath3 = Path.Combine(reMeasurePath, DateTime.Now.AddDays(-1).ToString("yyyy_M_d"));
                    if (ibwMode == "0")
                    {
                        reMeasurePath2 = Path.Combine(reMeasurePath, DateTime.Now.ToString("yyyy_M_d"));
                        reMeasurePath3 = Path.Combine(reMeasurePath, DateTime.Now.AddDays(-1).ToString("yyyy_M_d"));

                        //輪巡LCIN資料夾底下各線的資料夾
                        if (Directory.Exists(reMeasurePath2))
                        {
                            int imageCountCheck = 0;
                            foreach (string dirTemp in Directory.GetDirectories(reMeasurePath2))
                            {
                                if (Path.GetFileNameWithoutExtension(dirTemp).Count() >= 12)
                                {
                                    lineID = "TPAB0" + localLineID;
                                    if (Path.GetFileNameWithoutExtension(dirTemp).Count() == 12)
                                    {

                                        glassID = Path.GetFileNameWithoutExtension(dirTemp).Substring(0, 12);  //LCIN GlassID只取資料夾名稱前12碼
                                        if (imageDirDict.ContainsKey(glassID))
                                        {
                                            imageDirDict[glassID] = dirTemp;
                                        }
                                        else
                                        {
                                            imageDirDict.Add(glassID, dirTemp);
                                        }
                                        string[] _prodData = GetProd(glassID, lineID, dirTemp);
                                        prod = _prodData[0];
                                        pid = _prodData[1];

                                        if (prod == "None") //代表此PID未設定Recipe
                                        {
                                            CallWarningForm("PID : " + recipeIDNow + " Recipe未設定", "PID : " + recipeIDNow + " Recipe未設定");
                                            //MessageBox.Show("PID : " + recipeIDNow + " 產品名稱未設定");

                                        }
                                        else if (prod == "Reject")
                                        {

                                        }
                                        else
                                        {
                                            DirectoryInfo dirInfo = new DirectoryInfo(dirTemp);
                                            //檢查圖檔數量是否有缺少
                                            if (dirInfo.GetFiles("*.bmp").Count() > 0)
                                            {
                                                List<string> headList = recipeDict[pid + lineID].headList;
                                                imageCountCheck = headList.Count() * 3;
                                                
                                                if (dirInfo.GetFiles("*.bmp").Count() >= imageCountCheck)
                                                {
                                                    bool checkingFlag = false;
                                                    if (dirInfo.GetFiles("*.tmp").Count() > 0)
                                                    {
                                                        checkingFlag = true;
                                                    }
                                                    if (!waitMeasureDict.ContainsKey(glassID))
                                                    {
                                                        _ImageList images = LoadImageList(glassID, lineID);
                                                        WaitMeasureID waitID = new WaitMeasureID
                                                        {
                                                            glassID = glassID,
                                                            lineID = lineID,
                                                            prod = prod,
                                                            aiDataExistFlag = false,
                                                            aiJudgedCornerFlag = false,
                                                            aiJudgedNormalFlag = false,
                                                            aiJudgedOverlapFlag = false,
                                                            imageNameListCorner = images.imageNameListCorner,
                                                            imageNameListNormal = images.imageNameListNormal,
                                                            imageNameListOverlap = images.imageNameListOverlap,
                                                            dateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                            aiInputSendedFlag = false,
                                                            measuringFlag = checkingFlag,
                                                            pid = pid,
                                                            aiDefectCodeType = recipeDict[pid + lineID].aiDefectCodeType

                                                        };

                                                        waitMeasureDict.Add(glassID, waitID);
                                                        if (recipeDict[pid + lineID].aiModelCorner != "0")   //模型編號為0代表無設定
                                                        {
                                                            if (!waitID.aiInputSendedFlag)
                                                            {
                                                                waitID.aiInputSendedFlag = true;
                                                                if (aiMode == "1")
                                                                {
                                                                    _ai.GenerateAIFile(waitID, images, aiServer, new ModelNum { Corner = recipeDict[pid + lineID].aiModelCorner, Normal = recipeDict[pid + lineID].aiModelNormal, Overlap = recipeDict[pid + lineID].aiModelOverlap });
                                                                    _timerAI.Start();
                                                                    WriteToAILog("Sended AI Information : " + waitID.glassID, "Debug", true);
                                                                }
                                                                else
                                                                    waitMeasureDict[glassID].aiDataExistFlag = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            waitMeasureDict[glassID].aiDataExistFlag = true;
                                                            waitMeasureDict[glassID].measureByAIorTA = "TA";    //model編號中無此產品，量測權責轉給TA
                                                        }
                                                    }
                                                    else
                                                    {
                                                        waitMeasureDict[glassID].measuringFlag = checkingFlag;
                                                    }
                                                }
                                            }

                                        }


                                    }
                                }
                            }
                        }
                        //輪巡LCIN資料夾底下各線的資料夾
                        if (Directory.Exists(reMeasurePath3))
                        {
                            int imageCountCheck = 0;
                            foreach (string dirTemp in Directory.GetDirectories(reMeasurePath3))
                            {

                                if (Path.GetFileNameWithoutExtension(dirTemp).Count() >= 12)
                                {
                                    lineID = "TPAB0" + localLineID;
                                    if (Path.GetFileNameWithoutExtension(dirTemp).Count() == 12)
                                    {

                                        glassID = Path.GetFileNameWithoutExtension(dirTemp).Substring(0, 12);  //LCIN GlassID只取資料夾名稱前12碼
                                        if (imageDirDict.ContainsKey(glassID))
                                        {
                                            imageDirDict[glassID] = dirTemp;
                                        }
                                        else
                                        {
                                            imageDirDict.Add(glassID, dirTemp);
                                        }

                                        string[] _prodData = GetProd(glassID, lineID, dirTemp);
                                        prod = _prodData[0];
                                        pid = _prodData[1];

                                        if (prod == "None") //代表此PID未設定Recipe
                                        {
                                            CallWarningForm("PID : " + recipeIDNow + " Recipe未設定", "PID : " + recipeIDNow + " Recipe未設定");
                                            //MessageBox.Show("PID : " + recipeIDNow + " 產品名稱未設定");

                                        }
                                        else if (prod == "Reject")
                                        {

                                        }
                                        else
                                        {
                                            //檢查圖檔數量是否有缺少
                                            DirectoryInfo dirInfo = new DirectoryInfo(dirTemp);
                                            //檢查圖檔數量是否有缺少
                                            if (dirInfo.GetFiles("*.bmp").Count() > 0)
                                            {
                                                List<string> headList = recipeDict[pid + lineID].headList;
                                                imageCountCheck = headList.Count() * 3;

                                                if (dirInfo.GetFiles("*.bmp").Count() >= imageCountCheck)
                                                {
                                                    bool checkingFlag = false;
                                                    if (dirInfo.GetFiles("*.tmp").Count() > 0)
                                                    {
                                                        checkingFlag = true;
                                                    }
                                                    if (!waitMeasureDict.ContainsKey(glassID))
                                                    {
                                                        _ImageList images = LoadImageList(glassID, lineID);
                                                        WaitMeasureID waitID = new WaitMeasureID
                                                        {
                                                            glassID = glassID,
                                                            lineID = lineID,
                                                            prod = prod,
                                                            aiDataExistFlag = false,
                                                            aiJudgedCornerFlag = false,
                                                            aiJudgedNormalFlag = false,
                                                            aiJudgedOverlapFlag = false,
                                                            imageNameListCorner = images.imageNameListCorner,
                                                            imageNameListNormal = images.imageNameListNormal,
                                                            imageNameListOverlap = images.imageNameListOverlap,
                                                            dateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                            aiInputSendedFlag = false,
                                                            measuringFlag = checkingFlag,
                                                            pid = pid,
                                                            aiDefectCodeType = recipeDict[pid + lineID].aiDefectCodeType

                                                        };
                                                        waitMeasureDict.Add(glassID, waitID);
                                                        if (recipeDict[pid + lineID].aiModelCorner != "0")   //模型編號為0代表無設定
                                                        {
                                                            if (!waitID.aiInputSendedFlag)
                                                            {
                                                                waitID.aiInputSendedFlag = true;
                                                                if (aiMode == "1")
                                                                {
                                                                    _ai.GenerateAIFile(waitID, images, aiServer, new ModelNum { Corner = recipeDict[pid + lineID].aiModelCorner, Normal = recipeDict[pid + lineID].aiModelNormal, Overlap = recipeDict[pid + lineID].aiModelOverlap });
                                                                    _timerAI.Start();
                                                                    WriteToAILog("Sended AI Information : " + waitID.glassID, "Debug", true);
                                                                }
                                                                else
                                                                    waitMeasureDict[glassID].aiDataExistFlag = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            waitMeasureDict[glassID].aiDataExistFlag = true;
                                                            waitMeasureDict[glassID].measureByAIorTA = "TA";    //model編號中無此產品，量測權責轉給TA
                                                        }
                                                    }
                                                    else

                                                    {
                                                        waitMeasureDict[glassID].measuringFlag = checkingFlag;
                                                    }
                                                }
                                            }

                                        }


                                    }
                                }
                            }
                        }

                    }
                    else if (ibwMode == "1")
                    {

                        //輪巡LCIN資料夾底下各線的資料夾
                        if (Directory.Exists(reMeasurePath))
                        {
                            int imageCountCheck = 0;
                            foreach (string dirTemp in Directory.GetDirectories(reMeasurePath))
                            {

                                if (Path.GetFileNameWithoutExtension(dirTemp).Count() <= 8 && Path.GetFileNameWithoutExtension(dirTemp).Count() >= 5 && Path.GetFileNameWithoutExtension(dirTemp).Substring(0, 4) == "LCIN")   //IBW Mode
                                {

                                    lineID = "TPAB" + Path.GetFileNameWithoutExtension(dirTemp).Substring(4, 2);

                                    string reMeasurePath4 = Path.Combine(dirTemp, "Image", DateTime.Now.ToString("yyyy_M_d"));
                                    string reMeasurePath5 = Path.Combine(dirTemp, "Image", DateTime.Now.AddDays(-1).ToString("yyyy_M_d"));
                                    //搜尋當天
                                    if (Directory.Exists(reMeasurePath4))
                                    {
                                        foreach (string glassDirTemp in Directory.GetDirectories(reMeasurePath4))
                                        {
                                            if (Path.GetFileNameWithoutExtension(glassDirTemp).Count() >= 12)
                                            {

                                                glassID = Path.GetFileNameWithoutExtension(glassDirTemp).Substring(0, 12);  //LCIN GlassID只取資料夾名稱前12碼
                                                if (imageDirDict.ContainsKey(glassID))
                                                {
                                                    imageDirDict[glassID] = glassDirTemp;
                                                }
                                                else
                                                {
                                                    imageDirDict.Add(glassID, glassDirTemp);
                                                }

                                                string[] _prodData = GetProd(glassID, lineID, glassDirTemp);
                                                prod = _prodData[0];
                                                pid = _prodData[1];

                                                if (prod == "None") //代表此PID未設定Recipe
                                                {
                                                    CallWarningForm("PID : " + recipeIDNow + " Recipe未設定", "PID : " + recipeIDNow + " Recipe未設定");
                                                    //MessageBox.Show("PID : " + recipeIDNow + " 產品名稱未設定");

                                                }
                                                else if (prod == "Reject")
                                                {
                                                    continue;
                                                }
                                                else if (prod == "empty")
                                                {
                                                    //空資料夾
                                                    continue;
                                                }
                                                else
                                                {
                                                    DirectoryInfo dirInfo = new DirectoryInfo(glassDirTemp);
                                                    //檢查圖檔數量是否有缺少
                                                    if (dirInfo.GetFiles("*.bmp").Count() > 0)
                                                    {
                                                        //檢查圖檔數量是否有缺少
                                                        List<string> headList = recipeDict[pid + lineID].headList;
                                                        imageCountCheck = headList.Count() * 3;

                                                        if (dirInfo.GetFiles("*.bmp").Count() >= imageCountCheck)
                                                        {
                                                            bool checkingFlag = false;
                                                            if (dirInfo.GetFiles("*.tmp").Count() > 0)
                                                            {
                                                                checkingFlag = true;
                                                            }
                                                            if (!waitMeasureDict.ContainsKey(glassID))
                                                            {
                                                                _ImageList images = LoadImageList(glassID, lineID);
                                                                WaitMeasureID waitID = new WaitMeasureID
                                                                {
                                                                    glassID = glassID,
                                                                    lineID = lineID,
                                                                    prod = prod,
                                                                    aiDataExistFlag = false,
                                                                    aiJudgedCornerFlag = false,
                                                                    aiJudgedNormalFlag = false,
                                                                    aiJudgedOverlapFlag = false,
                                                                    imageNameListCorner = images.imageNameListCorner,
                                                                    imageNameListNormal = images.imageNameListNormal,
                                                                    imageNameListOverlap = images.imageNameListOverlap,
                                                                    dateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                                    aiInputSendedFlag = false,
                                                                    measuringFlag = checkingFlag,
                                                                    pid = pid,
                                                                    aiDefectCodeType = recipeDict[pid + lineID].aiDefectCodeType
                                                                };
                                                                waitMeasureDict.Add(glassID, waitID);
                                                                if (recipeDict[pid + lineID].aiModelCorner != "0")   //模型編號為0代表無設定
                                                                {
                                                                    if (!waitID.aiInputSendedFlag)
                                                                    {
                                                                        waitID.aiInputSendedFlag = true;
                                                                        if (aiMode == "1")
                                                                        {
                                                                            _ai.GenerateAIFile(waitID, images, aiServer, new ModelNum { Corner = recipeDict[pid + lineID].aiModelCorner, Normal = recipeDict[pid + lineID].aiModelNormal, Overlap = recipeDict[pid + lineID].aiModelOverlap });
                                                                            _timerAI.Start();
                                                                            WriteToAILog("Sended AI Information : " + waitID.glassID, "Debug", true);

                                                                        }
                                                                        else
                                                                            waitMeasureDict[glassID].aiDataExistFlag = true;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    waitMeasureDict[glassID].aiDataExistFlag = true;
                                                                    waitMeasureDict[glassID].measureByAIorTA = "TA";    //model編號中無此產品，量測權責轉給TA
                                                                }
                                                            }
                                                            else
                                                            {
                                                                waitMeasureDict[glassID].measuringFlag = checkingFlag;
                                                            }
                                                        }
                                                    }





                                                }

                                            }

                                        }

                                    }
                                    //搜尋前一天
                                    if (Directory.Exists(reMeasurePath5))
                                    {
                                        foreach (string glassDirTemp in Directory.GetDirectories(reMeasurePath5))
                                        {
                                            if (Path.GetFileNameWithoutExtension(glassDirTemp).Count() >= 12)
                                            {

                                                glassID = Path.GetFileNameWithoutExtension(glassDirTemp).Substring(0, 12);  //LCIN GlassID只取資料夾名稱前12碼
                                                if (imageDirDict.ContainsKey(glassID))
                                                {
                                                    imageDirDict[glassID] = glassDirTemp;
                                                }
                                                else
                                                {
                                                    imageDirDict.Add(glassID, glassDirTemp);
                                                }

                                                string[] _prodData = GetProd(glassID, lineID, glassDirTemp);
                                                prod = _prodData[0];
                                                pid = _prodData[1];

                                                if (prod == "None") //代表此PID未設定Recipe
                                                {
                                                    CallWarningForm("PID : " + recipeIDNow + " Recipe未設定", "PID : " + recipeIDNow + " Recipe未設定");
                                                    //MessageBox.Show("PID : " + recipeIDNow + " 產品名稱未設定");

                                                }
                                                else if (prod == "Reject")
                                                {

                                                }
                                                else
                                                {
                                                    DirectoryInfo dirInfo = new DirectoryInfo(glassDirTemp);
                                                    //檢查圖檔數量是否有缺少
                                                    if (dirInfo.GetFiles("*.bmp").Count() > 0)
                                                    {
                                                        List<string> headList = recipeDict[pid + lineID].headList;
                                                        imageCountCheck = headList.Count() * 3;

                                                        if (dirInfo.GetFiles("*.bmp").Count() >= imageCountCheck)
                                                        {
                                                            bool checkingFlag = false;
                                                            if (dirInfo.GetFiles("*.tmp").Count() > 0)
                                                                checkingFlag = true;
                                                            
                                                            if (!waitMeasureDict.ContainsKey(glassID))
                                                            {
                                                                _ImageList images = LoadImageList(glassID, lineID);
                                                                WaitMeasureID waitID = new WaitMeasureID
                                                                {
                                                                    glassID = glassID,
                                                                    lineID = lineID,
                                                                    prod = prod,
                                                                    aiDataExistFlag = false,
                                                                    aiJudgedCornerFlag = false,
                                                                    aiJudgedNormalFlag = false,
                                                                    aiJudgedOverlapFlag = false,
                                                                    imageNameListCorner = images.imageNameListCorner,
                                                                    imageNameListNormal = images.imageNameListNormal,
                                                                    imageNameListOverlap = images.imageNameListOverlap,
                                                                    dateTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                                                                    aiInputSendedFlag = false,
                                                                    pid = pid,
                                                                    aiDefectCodeType = recipeDict[pid + lineID].aiDefectCodeType
                                                                };
                                                                waitMeasureDict.Add(glassID, waitID);
                                                                if (recipeDict[pid + lineID].aiModelCorner != "0")   //模型編號為0代表無設定
                                                                {
                                                                    if (!waitID.aiInputSendedFlag)
                                                                    {
                                                                        waitID.aiInputSendedFlag = true;
                                                                        if (aiMode == "1")
                                                                        {
                                                                            _ai.GenerateAIFile(waitID, images, aiServer, new ModelNum { Corner = recipeDict[pid + lineID].aiModelCorner, Normal = recipeDict[pid + lineID].aiModelNormal, Overlap = recipeDict[pid + lineID].aiModelOverlap });
                                                                            _timerAI.Start();
                                                                            WriteToAILog("Sended AI Information : " + waitID.glassID, "Debug", true);
                                                                        }

                                                                        else
                                                                            waitMeasureDict[glassID].aiDataExistFlag = true;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    waitMeasureDict[glassID].aiDataExistFlag = true;
                                                                    waitMeasureDict[glassID].measureByAIorTA = "TA";    //model編號中無此產品，量測權責轉給TA
                                                                }
                                                            }
                                                            else
                                                            {
                                                                waitMeasureDict[glassID].measuringFlag = checkingFlag;
                                                            }
                                                        }
                                                    }
                                                }

                                            }

                                        }

                                    }


                                }

                            }
                        }
                    }
                    frmPanelTA.dataGridViewIdList.Rows.Clear();
                    if (waitMeasureDict.Count > 0)
                    {
                        frmPanelTA.dataGridViewIdList.Rows.Clear();
                        foreach (KeyValuePair<string, WaitMeasureID> listTemp in waitMeasureDict)
                        {
                            if (sealUnitDict.ContainsKey(listTemp.Value.glassID) && listTemp.Value.aiDataExistFlag)
                            {
                                if (listTemp.Value.measureByAIorTA == "TA")
                                {
                                    frmPanelTA.dataGridViewIdList.Rows.Add();
                                    frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells["lineID"].Value = listTemp.Value.lineID;
                                    frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells["unitID"].Value = sealUnitDict[listTemp.Value.glassID];
                                    frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells["prodName"].Value = listTemp.Value.prod;
                                    frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells["glassID"].Value = listTemp.Value.glassID;
                                    frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells["pid"].Value = listTemp.Value.pid;
                                    if (listTemp.Value.measuringFlag)
                                    {
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[0].Style.BackColor = Color.Red;
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[1].Style.BackColor = Color.Red;
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[2].Style.BackColor = Color.Red;
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[3].Style.BackColor = Color.Red;
                                    }
                                    else
                                    {
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[0].Style.BackColor = Color.White;
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[1].Style.BackColor = Color.White;
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[2].Style.BackColor = Color.White;
                                        frmPanelTA.dataGridViewIdList.Rows[frmPanelTA.dataGridViewIdList.RowCount - 1].Cells[3].Style.BackColor = Color.White;
                                    }
                                }
                            }
                            
                        }
                    }
                    
                }
            }
            timerCycle.Start();  //重新啟動計時器

        }
        //監控是否有待AI確認ID
        private void TimerAIProcess_Tick(object sender, EventArgs e)
        {
            timerAIProcess.Stop();
            if (waitMeasureDict.Count > 0)
            {
                foreach (KeyValuePair<string, WaitMeasureID> listTemp in waitMeasureDict.ToArray())
                {
                    if (!checkingAI)
                    {
                        if (listTemp.Value.measureByAIorTA == "AI" && listTemp.Value.aiDataExistFlag)
                        {
                            //檢查Sampling Count
                            //先取得Sampling Rate設定
                            string samplingRate = recipeDict[listTemp.Value.pid + listTemp.Value.lineID].aiSamplingRate;
                            if (!samplingCount.ContainsKey(listTemp.Value.pid + listTemp.Value.lineID)) //若不存在samplingCount中則代表未統計過
                                samplingCount.Add(listTemp.Value.pid + listTemp.Value.lineID, 1);

                            if (samplingCount[listTemp.Value.pid + listTemp.Value.lineID] >= int.Parse(samplingRate))
                            {
                                listTemp.Value.measureByAIorTA = "TA";  //達到Sampling標準，權限轉回TA並賦歸COUNT
                                samplingCount[listTemp.Value.pid + listTemp.Value.lineID] = 1;
                                WriteToAILog("AI Sampling Count Was Achieved : " + listTemp.Value.glassID, "Debug", true);
                            }
                            else
                            {
                                //未達Sampling標準，Count++

                                WriteToAILog("Recived AI Response Information : " + listTemp.Value.glassID, "Debug", true);
                                WriteToAILog("Now AI Sampling Count is : " + samplingCount[listTemp.Value.pid + listTemp.Value.lineID], "Debug", true);
                                samplingCount[listTemp.Value.pid + listTemp.Value.lineID]++;
                                object arg = (object)new object[]
                                {
                                    listTemp.Value.lineID,
                                    listTemp.Value.glassID,
                                    listTemp.Value.prod,
                                    waitMeasureDict[listTemp.Value.glassID],
                                    listTemp.Value.pid

                                };
                                checkingAI = true;
                                AIProcess(arg);
                            }
                        }
                    }
                    

                }
            }
            timerAIProcess.Start();


        }
        //20191128新增焦點變更時強制將焦點設定在此Label避免誤觸
        private void ControlLossFoucs()
        {
            label8.Focus();
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

        ///////////////////////////////委派宣告/////////////////////////////////////
        //呼叫委派Main UI修改控制項
        private delegate void ClosefrmImageCallBack(string vaule, frmImage ctl);
        private void ClosefrmImage(string vaule, frmImage ctl)
        {
            if (this.InvokeRequired)
            {
                ClosefrmImageCallBack uu = new ClosefrmImageCallBack(ClosefrmImage);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {
                ctl.Close();
                ctl.Dispose();

            }
        }

        //呼叫委派Main UI修改控制項
        private delegate void UpdateLabelCallBack(string vaule, Label ctl);
        private void UpdateLabel(string vaule, Label ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateLabelCallBack uu = new UpdateLabelCallBack(UpdateLabel);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {
                
                ctl.Text = vaule;
                
            }
        }
        //呼叫委派Main UI修改控制項
        private delegate void UpdateDateTimeLabelCallBack(string vaule, Label ctl);
        private void UpdateDateTimeLabel(string vaule, Label ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateDateTimeLabelCallBack uu = new UpdateDateTimeLabelCallBack(UpdateDateTimeLabel);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {

                ctl.Text = vaule.Substring(0,4) + "/" + vaule.Substring(4,2) + "/" + vaule.Substring(6, 2) + " " + vaule.Substring(8,2) + ":" + vaule.Substring(10, 2) + ":" + vaule.Substring(12, 2);

            }
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

                ctl.Text = vaule;
                if (vaule == "登出")
                {
                    ctl.BackColor = Color.Red;
                }
                else if (vaule == "登入")
                {
                    ctl.BackColor = Color.SkyBlue;
                }

            }
        }
        //呼叫委派Main UI呼叫主程序
        private delegate void MainMonitorCallBack(object vaule);
        private void MainMonitor(object vaule)
        {
            if (this.InvokeRequired)
            {
                MainMonitorCallBack uu = new MainMonitorCallBack(MainMonitor);
                this.Invoke(uu, vaule);
            }

            else
            {
                ThreadMain(vaule);
            }
        }

        //呼叫委派Main UI修改控制項
        public delegate void UpdateTextBoxCallBack(string vaule, TextBox ctl);
        public void UpdateTextBox(string vaule, TextBox ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateTextBoxCallBack uu = new UpdateTextBoxCallBack(UpdateTextBox);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {

                ctl.Text = vaule;
            }
        }

        //呼叫委派Main UI Append textBox
        private delegate void AppendTextBoxCallBack(string vaule, TextBox ctl);
        private void AppendTextBox(string vaule, TextBox ctl)
        {
            if (this.InvokeRequired)
            {
                AppendTextBoxCallBack uu = new AppendTextBoxCallBack(AppendTextBox);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {
                ctl.AppendText(vaule + "\r\n");
            }
        }

        //呼叫委派Main UI Append RichTextBox
        private delegate void AppendRichTextBoxCallBack(string vaule, RichTextBox ctl, Color color);
        private void AppendRichTextBox(string vaule, RichTextBox ctl, Color color)
        {
            if (this.InvokeRequired)
            {
                AppendRichTextBoxCallBack uu = new AppendRichTextBoxCallBack(AppendRichTextBox);
                this.Invoke(uu, vaule, ctl, color);
            }

            else
            {
                ctl.SelectionColor = color;
                ctl.AppendText(vaule + "\r\n");
            }
        }

        //呼叫委派Main UI修改控制項
        private delegate void DataGridViewClearCallBack(DataGridView ctl);
        private void DataGridViewClear(DataGridView ctl)
        {
            if (this.InvokeRequired)
            {
                DataGridViewClearCallBack uu = new DataGridViewClearCallBack(DataGridViewClear);
                this.Invoke(uu, ctl);
            }

            else
            {
                ctl.Rows.Clear();
            }
        }
        //呼叫委派Main UI修改控制項
        private delegate void TopMostCallBack(frmMain ctl);

        //呼叫委派Main UI更新AIData的DataGridView
        private delegate void DataGridViewRefreshCallBack(DataGridView ctl);
        private void DataGridViewRefresh(DataGridView ctl)
        {
            if (this.InvokeRequired)
            {
                DataGridViewClearCallBack uu = new DataGridViewClearCallBack(DataGridViewRefresh);
                this.Invoke(uu, ctl);
            }

            else
            {
                ctl.Rows.Clear();
            }
        }

        private void FormTopMost(frmMain ctl)
        {
            if (this.InvokeRequired)
            {
                TopMostCallBack uu = new TopMostCallBack(FormTopMost);
                this.Invoke(uu, ctl);
            }

            else
            {
                ctl.TopMost = true;
                ctl.TopMost = false;
            }
        }

        //呼叫委派Main UI來呼叫副程式主程序
        private delegate void MainThreadCallBack();
        private void _MainThread()
        {
            if (this.InvokeRequired)
            {
                MainThreadCallBack uu = new MainThreadCallBack(_MainThread);
                this.Invoke(uu);
            }

            else
            {
                if (mode == "0")
                {
                    frmABCD.PictureBoxDelect();
                    frmABCD.PictureBoxBuild();
                }
                else if (mode == "1")
                {
                    frmLCIN.PictureBoxDelect();
                    frmLCIN.MainThread();
                }
            }
        }

        //呼叫委派Main UI來呼叫副程式主程序
        private delegate void AIMainThreadCallBack();
        private void _AIMainThread()
        {
            if (this.InvokeRequired)
            {
                AIMainThreadCallBack uu = new AIMainThreadCallBack(_AIMainThread);
                this.Invoke(uu);
            }

            else
            {
                frmAI.AIProcess();
            }
        }

        //呼叫委派Main UI呼叫副程式PictureBoxDelect()
        private delegate void PictureBoxDelectCallBack();
        private void PictureBoxDelect()
        {
            if (this.InvokeRequired)
            {
                PictureBoxDelectCallBack uu = new PictureBoxDelectCallBack(PictureBoxDelect);
                this.Invoke(uu);
            }

            else
            {
                if (mode == "0")
                    frmABCD.PictureBoxDelect();
                else
                    frmLCIN.PictureBoxDelect();
            }
        }

        //呼叫委派Main UI呼叫副程式PictureBoxDelect()
        private delegate void AIPictureBoxDelectCallBack();
        private void AIPictureBoxDelect()
        {
            if (this.InvokeRequired)
            {
                AIPictureBoxDelectCallBack uu = new AIPictureBoxDelectCallBack(AIPictureBoxDelect);
                this.Invoke(uu);
            }

            else
            {
                frmAI.PictureBoxDelect();
            }
        }

        //呼叫委派Main UI啟動等待人員判定timer
        private delegate void TimerJudgeCountCallBack(string command);
        private void TimerJudgeCount(string command)
        {
            if (this.InvokeRequired)
            {
                TimerJudgeCountCallBack uu = new TimerJudgeCountCallBack(TimerJudgeCount);
                this.Invoke(uu);
            }

            else
            {
                if (command == "ON")
                {
                    judgeTimeCount = "1";
                    timerJudgeCount.Interval = 60000;
                    timerJudgeCount.Enabled = true;
                    timerJudgeCount.Start();
                }
                else if (command == "OFF")
                {
                    judgeTimeCount = "0";
                    timerJudgeCount.Enabled = false;
                    timerJudgeCount.Stop();
                }
            }
        }
        //呼叫委派Main UI啟動DeleyCheck觸發延遲判定流程執行緒
        private delegate void TimerDeleyCheckCallBack();

        ///////////////////////////////委派宣告/////////////////////////////////////

        //private void TimerDeleyCheck()
        //{
        //    if (this.InvokeRequired)
        //    {
        //        TimerDeleyCheckCallBack uu = new TimerDeleyCheckCallBack(TimerDeleyCheck);
        //        this.Invoke(uu);
        //    }

        //    else
        //    {
        //        deleyCheckFlag = "1";   //若一般流程結束後有deleylist則進入待deleycheck計時器
        //        timerDeleyCheck.Interval = int.Parse(deleyTime);
        //        timerDeleyCheck.Enabled = true;
        //        timerDeleyCheck.Start();
        //    }
        //}
        private void frmMain_Load(object sender, EventArgs e)
        {
            checking = false;
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width;    //取得現在螢幕解析度寬
            //int screenHeight = Screen.PrimaryScreen.Bounds.Height;      //取得現在螢幕解析度高
            ////依照解析度調整視窗大小
            //if (screenWidth < 1920)
            //{
            //    this.Width = 865;
            //}
            //if (screenHeight < 1080)
            //{
            //    this.Height = 725;
            //}
            // 找出字體大小,並算出比例
            //float dpiX, dpiY;
            //Graphics graphics = this.CreateGraphics();
            //dpiX = graphics.DpiX;
            //dpiY = graphics.DpiY;
            //int intPercent = (dpiX == 96) ? 100 : (dpiX == 120) ? 125 : 150;
            //int rate = intPercent / 100;
            //// 針對字體變更Form的大小
            //this.Size = new Size(this.Size.Width * rate, this.Size.Height * rate);


            if (!File.Exists(Path.Combine(Application.StartupPath, "ImageMeasureSystem.ini")))
            {
                MessageBox.Show("ImageMeasureSystem.ini不存在，請確認");
                this.Close();
            }

            //選擇機台種類
            ChoseType();

            if (mode == "0")
            {
                frmPanelTA.dataGridViewIdList.Columns.Remove("unitID");
                aBCD量測數量設定ToolStripMenuItem.Visible = true;
                frmPanelTA.groupBox3.Text = "線別";  //設定線別顯示group文字
            }
            else if (mode == "1")
            {
                aBCD量測數量設定ToolStripMenuItem.Visible = false;
                frmPanelTA.groupBox3.Text = "子機";  //設定線別顯示group文字
            }

            GetValue("PATH", "ImageServer", out outString, IniFilePath);
            imageServer = outString;
            GetValue("PATH", "FileServer", out outString, IniFilePath);
            fileServer = outString;
            GetValue("PATH", "AIServer", out outString, IniFilePath);
            aiServer = outString;
            GetValue("PATH", "ReMeasurePath", out outString, IniFilePath);
            reMeasurePath = outString;

            GetValue("PATH", "LoginTablePath", out outString, IniFilePath);
            loginTablePath = outString;

            GetValue("PATH", "Operation", out outString, IniFilePath);
            operationNum = outString;

            GetValue("PATH", "XMLSavePath", out outString, IniFilePath);
            xmlSavePath = outString;
            GetValue("PATH", "RecipeDir", out outString, IniFilePath);
            recipeDir = outString;

            GetValue("SQL Server", "Constr", out outString, IniFilePath);
            constrSample = outString;
            
            
            GetValue("SQL Server", "IP", out outString, IniFilePath);
            ip = outString;
            GetValue("SQL Server", "Port", out outString, IniFilePath);
            port = outString;
            GetValue("SQL Server", "UserID", out outString, IniFilePath);
            userId = outString;
            GetValue("SQL Server", "Password", out outString, IniFilePath);
            password = outString;
            GetValue("FEATURES", "TestMode", out outString, IniFilePath);
            testMode = outString;
            GetValue("FEATURES", "DpiX", out outString, IniFilePath);
            dpiX = outString;
            GetValue("FEATURES", "DpiY", out outString, IniFilePath);
            dpiY = outString;
            GetValue("PATH", "ResultPath", out outString, IniFilePath);
            resultPath = outString;
            GetValue("FEATURES", "JudgeTime", out outString, IniFilePath);
            judgeTime = outString;
            GetValue("FEATURES", "UploadSPCData", out outString, IniFilePath);
            uploadSpcData = outString;
            GetValue("PATH", "TestServer", out outString, IniFilePath);
            testServer = outString;
            GetValue("PATH", "ResultImagePath", out outString, IniFilePath);
            resultImagePath = outString;
            GetValue("FEATURES", "CycleTime", out outString, IniFilePath);
            cycleTime = outString;
            cycleTimeSpan = double.Parse(cycleTime);
            GetValue("FEATURES", "DeleyTime", out outString, IniFilePath);
            deleyTime = outString;
            GetValue("FEATURES", "MaxImage", out outString, IniFilePath);
            maxImage = outString;
            GetValue("FEATURES", "IBWMode", out outString, IniFilePath);
            ibwMode = outString;
            GetValue("FEATURES", "AIMode", out outString, IniFilePath);
            aiMode = outString;
            GetValue("FEATURES", "LocalLineID", out outString, IniFilePath);
            localLineID = outString;
            GetValue("FEATURES", "AISampling", out outString, IniFilePath);
            aiSamplingRate = outString;
            GetValue("FEATURES", "ADMImageSort", out outString, IniFilePath);
            admImageSortString = outString;
            admImageSort.Clear();
            foreach (string strTemp in admImageSortString.Split(','))
            {
                admImageSort.Add(strTemp.Replace(" ", ""));
            }
            GetValue("FEATURES", "WaitAITime", out outString, IniFilePath);
            waitAITime = outString;
            constr = constrSample + "User ID=" + userId + ";" + "Password=" + password + ";";

            if (testMode == "1")
            {
                imageServer = @"D:\";
                resultPath = @"D:\";
                UpdateLabel("99999999", labelUserID);
                UpdateLabel("TEST", labelUserName);
                UpdateButton("登出", buttonLogin);
            }
            //檢查暫存資料夾
            if (Directory.Exists(Path.Combine(Application.StartupPath, "temp")))
            {
                try
                {
                    //清空此id所有format
                    DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Application.StartupPath, "temp"));
                    int tempFileCount = dirInfo.GetFiles().Length;
                    string[] tempFiles = null;
                    if (tempFileCount > 0)
                    {
                        tempFiles = System.IO.Directory.GetFiles(Path.Combine(Application.StartupPath, "temp"), "*.*", System.IO.SearchOption.AllDirectories);    //取得資料夾下所有檔案路徑
                        if (tempFiles != null)
                        {
                            foreach (string tempPath in tempFiles)
                            {
                                try
                                {
                                    File.Delete(tempPath);
                                }
                                catch
                                {

                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(Application.StartupPath, "temp"));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            //系統啟動先檢查一次Server連線狀態
            //CheckServer();

            //讀取release table儲存到變數中
            ReadReleaseTable();

            ////啟動ServerCheck計時器;每600000ms檢查一次
            //this.timerCheckServer.Interval = 600000;
            //this.timerCheckServer.Enabled = true;
            //this.timerCheckServer.Start();

            //啟動時鐘;設定每100ms更新一次
            this.timerNow.Interval = 100;
            this.timerNow.Enabled = true;
            this.timerNow.Start();

            //啟動datagridview更新計時器
            this.timerRefresh.Interval = 100;
            this.timerRefresh.Enabled = true;
            this.timerRefresh.Start();

            //啟動檢查待判片延遲時間計時器
            //this.timerDeleyCheck.Interval = 10000;
            //this.timerDeleyCheck.Enabled = true;
            //this.timerDeleyCheck.Start();
            //讀取最後一次檢查時間
            nextTime = ReadLastTime();
            //UpdateDateTimeLabel(nextTime, labelNextTime);

            //啟動待判ID檢查循環計時器
            this.timerCycle.Interval = 1000;
            this.timerCycle.Enabled = true;
            this.timerCycle.Start();

            //檢查是否為測試模式
            //if (testMode != "1")
            //{
            //    buttonTest.Visible = false;
            //}
            //else
            //{
                
            //}
            //讀取loginTable
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "PIUser.txt"), Encoding.Default))
            {
                ReadLoginTable(sr);
            }

            //讀取UserName
            //using (StreamReader sr2 = new StreamReader(Path.Combine(Application.StartupPath, "PIUser.txt"), Encoding.Default))
            //{
            //    ReadUserName(sr2);
            //}

            //讀取ADMRate
            using (StreamReader sr3 = new StreamReader(Path.Combine(Application.StartupPath, "ADMRateTable.txt"), Encoding.Default))
            {
                ReadADMRate(sr3);
            }

            //讀取SPEC
            using (StreamReader sr4 = new StreamReader(Path.Combine(Application.StartupPath, "SPEC.txt"), Encoding.Default))
            {
                ReadSpec(sr4);
            }
            if (mode == "0")
            {
                //讀取ADMCount
                using (StreamReader sr5 = new StreamReader(Path.Combine(Application.StartupPath, "ABCDCount.txt"), Encoding.Default))
                {
                    ReadABCDCount(sr5);
                }
                //讀取SPEC_ABCD
                using (StreamReader sr6 = new StreamReader(Path.Combine(Application.StartupPath, "SPEC_ABCD.txt"), Encoding.Default))
                {
                    ReadSpecABCD(sr6);
                }
            }
            //讀取Recipe
            ReadRecipe();

            if (mode == "0")
            {
                frmPanelTA.dataGridViewPIIN1.Visible = true;
                frmPanelTA.dataGridViewPIIN2.Visible = true;
                frmPanelTA.dataGridViewLCIN1.Visible = false;

                frmABCD.MdiParent = this;
                frmABCD.Parent = splitContainer1.Panel2;
                frmABCD.WindowState = FormWindowState.Maximized;
                frmABCD.Show();
            }
            else if (mode == "1")
            {

                frmPanelTA.dataGridViewPIIN1.Visible = false;
                frmPanelTA.dataGridViewPIIN2.Visible = false;
                frmPanelTA.dataGridViewLCIN1.Visible = true;
                frmLCIN.MdiParent = this;
                frmLCIN.Parent = splitContainer1.Panel2;
                frmLCIN.WindowState = FormWindowState.Maximized;
                frmLCIN.Show();

                frmPanelTA.MdiParent = this;
                frmPanelTA.Parent = splitContainer2.Panel2;
                frmPanelTA.WindowState = FormWindowState.Maximized;
                frmPanelTA.Show();
                

                if (aiMode == "1")
                {
                    ////啟動WaitForAIOutput 執行緒
                    //threadWaitForAIOutput = new Thread(WaitForAIOutput);
                    //threadWaitForAIOutput.Name = "threadWaitForAIOutput";
                    //threadWaitForAIOutput.IsBackground = true;
                    //threadWaitForAIOutput.Start();

                    ////啟動觸發AI自動流程循環計時器
                    //buttonAIProcessStart.Text = "AI Process Stop";
                    //buttonAIProcessStart.BackColor = Color.Red;
                    //this.timerAIProcess.Interval = 1000;
                    //this.timerAIProcess.Enabled = true;
                    //this.timerAIProcess.Start();
                }
            }
        }

        //計算時間
        private string TimeCalculation(double time)
        {

            DateTime nextTime = DateTime.Now.AddMilliseconds(time);
            return nextTime.ToString("yyyyMMddHHmmss");
        }
        //private string[] CheckGlassId(string lineId, string time)
        //{
            
        //    //先清空id清單
        //    idList.Clear();
        //    //歸零取用id編號
        //    idNum = 0;
        //    //宣告變數
        //    string[] rtn = null;
        //    string dataBase = "GlassData" + lineId;

            
        //    SqlCommand mySqlCommand = null;
        //    DateTime checkTime = DateTime.ParseExact(time, "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime checkTime2 = checkTime.AddHours(-2);   //以觸發時間往前兩小時搜尋此區間內最後一片基板ID
        //    string checkTime3 = checkTime2.ToString("yyyyMMddHHmmss");
        //    WriteToLog("Check Time is : " + checkTime3, lineId, "Debug");
        //    //建立sql連線
        //    using (SqlConnection conn = new SqlConnection(constr))
        //    {
        //        //宣告sql字串
        //        string sqlStrSearchGlassId = string.Format("SELECT GlassID, Prod FROM {0} WHERE Time >= '{1}' ORDER BY Time DESC;", dataBase, checkTime3);
        //        //傳送sql字串
        //        try
        //        {
        //            mySqlCommand = new SqlCommand(sqlStrSearchGlassId, conn);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.ToString());
        //            WriteToLog("Search GlassID From SQLServer Fail", lineId, "Warning");
        //            rtn = new string[] { "000000000000", "000000000000" };
        //            return rtn;
        //        }
        //        //用datatable接收sql server傳回的資料
        //        SqlDataAdapter da = new SqlDataAdapter(mySqlCommand);
        //        DataSet ds = new DataSet();
        //        ds.Clear();
        //        da.Fill(ds);
        //        DataTable myDataTble = ds.Tables[0];
        //        int myDataTbleRowsCount = myDataTble.Rows.Count;
        //        WriteToLog("Search GlassID Complete", lineId, "Debug");
        //        //若有data
        //        if (myDataTbleRowsCount != 0 && testMode == "0")
        //        {
        //            WriteToLog("Update Glass ID List", lineId, "Debug");
        //            for (int x = 0; x < myDataTbleRowsCount; x++)
        //            {
        //                if (x >= 20)
        //                {
        //                    break;
        //                }
        //                if (myDataTble.Rows[x][0].ToString() != null && myDataTble.Rows[x][0].ToString() != "")
        //                {
                            
        //                    idList.Add(myDataTble.Rows[x][0].ToString());
        //                }
        //            }

        //            rtn = new string[] { myDataTble.Rows[0][0].ToString(), myDataTble.Rows[0][1].ToString() };
        //            WriteToLog("GlassID is : " + rtn[0] + "，Prod is : " + rtn[1], lineId, "Debug");
        //        }
        //        else
        //        {
        //            //無資料
        //            rtn = new string[] { "X", "X" };
        //            WriteToLog("Search GlassID is No Data", lineId, "Debug");
        //        }
        //    }
            
        //    return rtn;
        //}
        private void WriteToLog(string command, string lineId, string type = "None")
        {
            dtNow = DateTime.Now.ToString("yyyMMdd");
            string logFileDir = @"log\" + dtNow + @"\" + dtNow + ".txt";
            try
            {
                //if (File.Exists(@"log") == false)
                //{
                //    File.Create("log");
                //}
                //if (File.Exists(@"log\" + dtNow) == false)
                //{
                //    File.Create(@"log\" + dtNow);
                //}
                //if (File.Exists(logFileDir) == false)
                //{
                //    File.Create(logFileDir);
                //}
                if (File.Exists(@"log\" + dtNow) == false)
                {
                    Directory.CreateDirectory(@"log\" + dtNow);
                }
                if (File.Exists(logFileDir) == false)
                {
                    File.Create(logFileDir).Close();
                }

                FileStream logFs = new FileStream(logFileDir, FileMode.Append);
                StreamWriter logWriter = new StreamWriter(logFs);
                string dtNow2 = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");
                if (command == null || command == "")
                {
                    command = "No Command";
                }
                logWriter.WriteLine(dtNow2 + "[" + type + "]" + "[" + lineId + "]" + " : " + command);
                logWriter.Close();
                logFs.Close();
            }
            catch
            {

            }
            
        }

        private void WriteToAILog(string command, string type, bool append)
        {
            //type 分為Error, Debug
            dtNow = DateTime.Now.ToString("yyyMMdd");
            string logFileDir = Path.Combine(Application.StartupPath, "log", "AI", dtNow);
            string dtNow2 = DateTime.Now.ToString("yyy/MM/dd HH:mm:ss");
            try
            {

                if (!Directory.Exists(logFileDir))
                {
                    Directory.CreateDirectory(logFileDir);
                }
                string logFilePath;
                switch (type.ToUpper())
                {
                    case "ERROR":
                        logFilePath = Path.Combine(logFileDir, "ErrorLog.txt");
                        break;
                    case "DEBUG":
                        logFilePath = Path.Combine(logFileDir, "DebugLog.txt");
                        break;
                    default:
                        logFilePath = Path.Combine(logFileDir, "DebugLog.txt");
                        break;
                }
                FileStream logFs = new FileStream(logFilePath, FileMode.Append);
                StreamWriter logWriter = new StreamWriter(logFs);
                
                if (command == null || command == "")
                {
                    command = "Unknow Command";
                }
                logWriter.WriteLine(dtNow2 + " : " + command);
                logWriter.Close();
                logFs.Close();
            }
            catch
            {

            }

            if (append)
                AppendTextBox(dtNow2 + " : " + command, frmAI.textBoxMessage);
        }

        private _ImageList LoadImageList(string glassId, string lineId)
        {
            string imageDir = null;
            
            if (mode == "0")
            {
                List<string> rtn = new List<string> { };
                if (testMode == "1")
                {
                    imageDir = Path.Combine(reMeasurePath, lineId, glassId);
                }
                else
                {
                    //imageDir = Path.Combine(imageServer, "LCD", "2100", "FixedImages", "PI" + lineId.Substring(4,2 ), time.Substring(0,8), glassId);
                    imageDir = Path.Combine(reMeasurePath, lineId, glassId);
                    WriteToLog("Image Folder is : " + imageDir, lineId, "Debug");
                }
                if (Directory.Exists(imageDir))
                {
                    if (Directory.GetFiles(imageDir).Length > 0)    //確認資料夾下有圖片檔案
                    {
                        foreach (string temp in Directory.GetFiles(imageDir))
                        {
                            if (String.Equals(Path.GetExtension(temp), ".JPG", StringComparison.CurrentCultureIgnoreCase))
                            {
                                rtn.Add(temp);
                            }

                        }
                        WriteToLog("Search Image Path List Complete : ", lineId, "Debug");
                    }
                }
                return new _ImageList { imageNameListABCD = rtn };
            }
            else
            {
                List<string> rtnCorner = new List<string> { };
                List<string> rtnNormal = new List<string> { };
                List<string> rtnOverlap = new List<string> { };
                if (testMode == "1")
                {
                    imageDir = Path.Combine(reMeasurePath, lineId, glassId);
                }
                else
                {
                    //imageDir = Path.Combine(imageServer, "LCD", "2100", "FixedImages", "PI" + lineId.Substring(4,2 ), time.Substring(0,8), glassId);

                    for (int _c = 0; _c < 3; _c++)
                    {
                       
                        if (imageDirDict.ContainsKey(glassId))
                        {
                            imageDir = imageDirDict[glassId];
                            break;
                        }
                        else
                        {
                            Thread.Sleep(500);
                        }
                        
                    }
                    
                   
                    WriteToLog("Image Folder is : " + imageDir, lineId, "Debug");
                }
                if (Directory.Exists(imageDir))
                {
                    if (Directory.GetFiles(imageDir).Length > 0)    //確認資料夾下有圖片檔案
                    {
                        foreach (string temp in Directory.GetFiles(imageDir))
                        {
                            if (String.Equals(Path.GetExtension(temp), ".bmp", StringComparison.CurrentCultureIgnoreCase))
                            {
                                string[] splitTemp = temp.Split('\\');
                                string imageName = splitTemp[splitTemp.Count() - 1].Split('.')[0];
                                string imageTypeSpilt = imageName.Split('_')[3].Split('-')[1];
                                switch (imageTypeSpilt.Substring(0, 3).ToUpper())
                                {
                                    case "COR":
                                        rtnCorner.Add(temp);
                                        break;
                                    case "NOR":
                                        rtnNormal.Add(temp);
                                        break;
                                    case "OVE":
                                        rtnOverlap.Add(temp);
                                        break;
                                }

                            }

                        }
                        WriteToLog("Search Image Path List Complete : ", lineId, "Debug");
                    }
                }
                return new _ImageList { imageNameListCorner = rtnCorner, imageNameListNormal = rtnNormal, imageNameListOverlap = rtnOverlap };
            }
            
        }

        private void PictureBoxDelete()
        {
            try
            {
                frmImage form = this.Controls.Find("frmImage", true)[0] as frmImage;
                form.PictureBoxDelect();
                form.Dispose();
            }
            catch
            {

            }
        }

        private void ShowImages(List<string> imagePathList, string glassId, string lineId, WaitMeasureID measureID)
        {
            //Label labelName = (Label)this.Controls.Find("labelStatusNow" + lineId.Substring(4, 2), true)[0];
            if (glassId != "X" && glassId != "000000000000" && glassId != "Pass")
            {

                //UpdateLabel("Loading...", labelName);
                Thread.Sleep(50);

                glassIDNow = glassId;
                if (imagePathList[0] != "NoImage" && imagePathList.Count > 0)
                {
                    WriteToLog("Creat PictureBox Thread is Start", lineId, "Debug");

                    _MainThread(); //呼叫委派主UI動態建立物件
                }
                else
                {
                    WriteToLog("No Image Can Creat PictureBox", lineId, "Debug");
                    judgeResult = "Pass";
                }
                
                
                
                WriteToLog("judgeResult is NA", lineId, "Debug");
                judgeResult = "NA";
                //呼叫等待人員判定計時器
                judgeTimeCount = "1";
                WriteToLog("TimeJudgeCount Thread is Start", lineId, "Debug");


           
            }
            else if (glassId == "X")
            {
                WriteToLog("No Glass Data", lineId, "Debug");
             
            }
            else
            {
                WriteToLog("Line and Prod is Unrelease", lineId, "Debug");
                judgeResult = "Pass";
            }

            //置頂視窗
            WriteToLog("Set Form Top", lineId, "Debug");
            FormTopMost(this);
            
  
        }

        private void ShowAIImages(List<string> imagePathList, string glassId, string lineId, WaitMeasureID measureID)
        {
            //Label labelName = (Label)this.Controls.Find("labelStatusNow" + lineId.Substring(4, 2), true)[0];
            if (glassId != "X" && glassId != "000000000000" && glassId != "Pass")
            {
                
           
                Thread.Sleep(50);
                //先呼叫PictureBoxDelect清除上一個frmImage的所有picturebox
                AIPictureBoxDelect();
                glassIDNowAI = glassId;
                lineIDNowAI = lineId;
                if (imagePathList[0] != "NoImage" && imagePathList.Count > 0)
                {
                    threadAIProcess = new Thread(frmAI.AIProcess);
                    threadAIProcess.IsBackground = true;
                    threadAIProcess.Name = "threadAIProcess";
                    threadAIProcess.Start();
                    checkingAI = true;
                    threadAIProcessCompleteFlag = false;

                    
                    //啟動監聽threadAIProcess完成迴圈
                    threadAIProcessListen = new Thread(AIProcessListen);
                    threadAIProcessListen.IsBackground = true;
                    threadAIProcessListen.Name = "threadAIProcessListen";
                    threadAIProcessListen.Start();

                }
                else
                {

                }
            }
            else if (glassId == "X")
            {
                WriteToLog("No Glass Data", lineId, "Debug");
            }
            else
            {
                WriteToLog("Line and Prod is Unrelease", lineId, "Debug");
            }
        }

        //監控AI Process Thread是否已量測完畢
        private void AIProcessListen()
        {
            //啟動計時器
            Stopwatch _timer = new Stopwatch();
            _timer.Start();
            while (true)
            {
                if (Convert.ToDouble(_timer.ElapsedMilliseconds) < 60000)
                {
                    if (threadAIProcessCompleteFlag)
                    {
                        //檢查是否有OOC/OOS
                        if (overSpecAIFlag != "0")
                        {
                            switch(overSpecAIFlag)
                            {
                                case "1":
                                    AppendRichTextBox(DateTime.Now.ToString("[HH:mm:ss]") + " " + glassIDNowAI + " " + lineIDNowAI + " " + prodNowAI + " " + "OOC", frmPanelAI.richTextBoxAIResult, Color.DarkOrange);
                                    break;
                                case "2":
                                    AppendRichTextBox(DateTime.Now.ToString("[HH:mm:ss]") + " " + glassIDNowAI + " " + lineIDNowAI + " " + prodNowAI + " " + "OOS", frmPanelAI.richTextBoxAIResult, Color.Red);
                                    break;
                            }

                            waitMeasureDict[glassIDNowAI].measureByAIorTA = "TA";  //AI量測有OOS OOC 權限轉給TA
                            imageDirDict.Remove(glassIDNowAI);
                            WriteToAILog("OOS or OOC Exist in AI Measure Result", "Debug", true);
                            WriteToAILog("Transfer Glass Data to TA for Measure", "Debug", true);
                            ClearMeasureData(DataSourceType.AI);
                           

                            break;
                        }
                        else
                        {
                            Thread.Sleep(1000); //緩衝1秒再開始finishi function避免出現最後一個測項上傳資料不完整
                            MeasureFinishied(DataSourceType.AI);
                            _timer.Stop();
                            checkingAI = false;
                            break;
                        }
                        
                    }
                }
                else
                {
                    WriteToAILog("超過時間未收到threadAIProcess完成指令，強制結束", "Error", true);
                    _timer.Stop();
                    threadAIProcess.Abort();
                    checkingAI = false;
                    break;
                }
            }
            checkingAI = false;
            threadAIProcessListen.Abort();
            
        }
        private void JudgeTimeCount(string command)
        {
            if (command == "1")
            {

            }
        }

        //儲存OKNG的資訊到SQL Server
        private void WriteToSQL(string lineId, string sqlCommand)
        {
            
            //建立sql連線
            SqlCommand mySqlCommand = null;
            using (SqlConnection conn = new SqlConnection(constr))
            {
                //宣告sql字串
                string sqlStrSearchGlassId = sqlCommand;
                //傳送sql字串
                try
                {
                    mySqlCommand = new SqlCommand(sqlStrSearchGlassId, conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    WriteToLog("Write Data To SQLServer Fail，Constr : " + sqlCommand, lineId, "Warning");
                }
            }
        }

        private void OperatorJudge()
        {

        }

        private void Monitor(object arg)
        {
            checking = true; //turnon檢查中flag
            imagePathList.Clear();
            object[] argList = (object[])arg;
            string lineId = argList[0].ToString();
            string glassId = argList[1].ToString();
            string prod = argList[2].ToString();
          

            if (glassId != "X" && glassId != "000000000000" && glassId != "Pass")
            {
                WriteToLog("Search Image Path List", lineId, "Debug");
                
                //將LoadImageList回傳的分割的圖片路徑list合併
                if (mode == "0")
                {
                    imagePathList = ((WaitMeasureID)argList[3]).imageNameListABCD;
                }
                else
                {
                    imagePathList = ((WaitMeasureID)argList[3]).imageNameListCorner.Concat(((WaitMeasureID)argList[3]).imageNameListNormal).Concat(((WaitMeasureID)argList[3]).imageNameListOverlap).ToList();
                }
                
                //呼叫備份影像function用來給AI學習
                if (imageDirDict.ContainsKey(glassId))
                {
                    object argCopy = (object)new string[] { imageDirDict[glassId], prod, lineId, glassId };
                    Thread td = new Thread(new ParameterizedThreadStart(CopyImageFolderThread));
                    td.Start(argCopy);
                }
                else
                {
                    WriteToLog("imageDirDict中找不到 : " + glassId, lineId);
                }
                imagePathList = SortList(imagePathList, prod, pidNow, lineId);
                
                if (imagePathList[0] == "None") //若是排序失敗
                {
                    MessageBox.Show("圖檔重新排序時發現與Head編號設定不同或是圖檔顯示順序設定有誤導致無法比對成功，請確認");
                    frmLCIN.CancelMeasure();
                    DataGridViewClear(frmPanelTA.dataGridViewLCIN1);
                    UpdateTextBox("", frmPanelTA.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
                    UpdateTextBox("", frmPanelTA.textBoxUnit);  //更新目前檢查基板ID資訊
                    checking = false;
                    return;
                }
            }
            UpdateTextBox(glassId, frmPanelTA.textBoxGlassIdNow);  //更新目前檢查基板ID資訊

            if (mode == "0")
            {
                UpdateTextBox(lineId.Substring(5,1), frmPanelTA.textBoxUnit);  //更新目前檢查基板ID資訊
                unitIDNow = lineId;
            }
            else if (mode == "1")
            {
                if (sealUnitDict.ContainsKey(glassId))
                {
                    UpdateTextBox(sealUnitDict[glassId], frmPanelTA.textBoxUnit);  //更新目前檢查基板ID資訊
                    unitIDNow = sealUnitDict[glassId];
                }
                else
                {
                    MessageBox.Show("找不到此ID的子機資訊");
                    return;
                }
            }
            

            WriteToLog("ShowImages Thread is Start", lineId, "Debug");
            ShowImages(imagePathList, glassId, lineId, (WaitMeasureID)argList[3]);
        }

        private void AIProcess(object arg)
        {
            imagePathListAI.Clear();
            object[] argList = (object[])arg;
            string lineId = argList[0].ToString();
            string glassId = argList[1].ToString();
            string prod = argList[2].ToString();
            string pid = argList[4].ToString();
            pidNowAI = pid;
            if (glassId != "X" && glassId != "000000000000" && glassId != "Pass")
            {
                WriteToAILog("Search Image Path List", "Debug", true);

                //將LoadImageList回傳的分割的圖片路徑list合併
                imagePathListAI = ((WaitMeasureID)argList[3]).imageNameListCorner.Concat(((WaitMeasureID)argList[3]).imageNameListNormal).Concat(((WaitMeasureID)argList[3]).imageNameListOverlap).ToList();

                imagePathListAI = SortList(imagePathListAI, prod, pid, lineId);
                prodNowAI = prod;

                if (imagePathListAI.Count == 0 || imagePathListAI[0] == "None") //若是排序失敗
                {
                    frmAI.CancelMeasure();
                    DataGridViewClear(frmPanelAI.dataGridViewLCIN1);
                    UpdateTextBox("", frmPanelAI.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
                    UpdateTextBox("", frmPanelAI.textBoxUnit);  //更新目前檢查基板ID資訊
                    
                    return;
                }
            }
            UpdateTextBox(glassId, frmPanelAI.textBoxGlassIdNow);  //更新目前檢查基板ID資訊

            if (mode == "0")
            {
                UpdateTextBox(lineId.Substring(5, 1), frmPanelAI.textBoxUnit);  //更新目前檢查基板ID資訊
                unitIDNowAI = lineId;
            }
            else if (mode == "1")
            {
                if (sealUnitDict.ContainsKey(glassId))
                {
                    UpdateTextBox(sealUnitDict[glassId], frmPanelAI.textBoxUnit);  //更新目前檢查基板ID資訊
                    unitIDNowAI = sealUnitDict[glassId];
                }
                else
                {
                    WriteToAILog("找不到此ID : " + glassId + " 的子機資訊", "Error", true);
                    return;
                }
            }

            glassDirNowAI = imageDirDict[glassId];

            //寫入量測資料DataGridView Head
            frmPanelAI.dataGridViewLCIN1.Rows.Clear();
            //先填好Panel編號
            List<string> panelNumSplit = null;
            string[] typeList = new string[] { "Normal Seal", "Overlap", "Corner" };

            panelNumSplit = recipeDict[pid + lineId].headList;
            for (int x = 0; x < panelNumSplit.Count() * 3; x++)
            {
                frmPanelAI.dataGridViewLCIN1.Rows.Add();
            }
            int rowCount = 0;
            for (int x = 0; x < panelNumSplit.Count(); x++)
            {
                foreach (string strTemp in typeList)
                {
                    frmPanelAI.dataGridViewLCIN1.Rows[rowCount].Cells[0].Value = "P" + panelNumSplit[x];
                    frmPanelAI.dataGridViewLCIN1.Rows[rowCount].Cells[1].Value = strTemp;
                    rowCount++;
                }

            }


            WriteToAILog("Call ShowAIImages Thread", "Debug", false);
            ShowAIImages(imagePathListAI, glassId, lineId, (WaitMeasureID)argList[3]);
        }

        private void CopyImageFolderThread(object arg)
        {
            string[] argList = (string[])arg;
            CopyImageFolder(argList[0], argList[1], argList[2], argList[3]);
        }


        private void CopyImageFolder(string imageDir, string prod, string lineID, string glassID)
        {
            string targetDir = Path.Combine(@"X:\", "LCIN", lineID, prod, glassID);
            if (!Directory.Exists(targetDir))
            {
                try
                {
                    Directory.CreateDirectory(targetDir);
                }
                catch
                {

                }
            }
            
            try
            {
                CopyDirectory(imageDir, targetDir);
            }
            catch(Exception ex)
            {
                WriteToLog("備份教導影像失敗，Message : " + ex.ToString(), lineID);
            }
        }

        private void FinishMeasure(DataSourceType dataSource)
        {
            if (frmPanelTA.textBoxGlassIdNow.Text.ToString() != "")
            {
                if (labelUserName.Text == "XXX" || labelUserID.Text == "00000000")
                {
                    MessageBox.Show("請先登入");
                    return;
                }
                else if (completeFlag != "1")
                {
                    MessageBox.Show("圖片未全部看過無法完成流程");
                    return;
                }
                //詢問視窗確認是否上傳
                string strMessage = "";
                if (uploadSpcData == "1")
                {
                    strMessage = "請確認是否要上報量測資料 ?";
                }
                else
                {
                    strMessage = "確定要結束量測 ?";
                }
                DialogResult dr = MessageBox.Show(strMessage, "確認視窗", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    if (overSpecFlag == "1")
                    {
                        DialogResult dr2 = MessageBox.Show("量測結果有OOS / OOC存在，是否還要上報?", "確認視窗", MessageBoxButtons.OKCancel);
                        {
                            if (dr2 == DialogResult.OK)
                            {
                                MeasureFinishied(DataSourceType.TA);
                            }
                            else
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        MeasureFinishied(DataSourceType.TA);
                    }
                }
                else
                {
                    return;
                }
            }
        }
        public void CancelMeasure(DataSourceType dataSource)
        {
            if (dataSource == DataSourceType.TA)
            {
                DialogResult dr = MessageBox.Show("是否確定要取消量測?", "確認視窗", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    if (mode == "0")
                    {
                        frmABCD.CancelMeasure();
                        frmPanelTA.dataGridViewPIIN1.Rows.Clear();
                        frmPanelTA.dataGridViewPIIN2.Rows.Clear();
                        UpdateTextBox("", frmPanelTA.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
                        UpdateTextBox("", frmPanelTA.textBoxUnit);  //更新目前檢查基板子機資訊
                        checking = false;
                    }
                    else if (mode == "1")
                    {
                        frmLCIN.CancelMeasure();
                        frmPanelTA.dataGridViewLCIN1.Rows.Clear();
                        DeleteTemp();
                        UpdateTextBox("", frmPanelTA.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
                        UpdateTextBox("", frmPanelTA.textBoxUnit);  //更新目前檢查基板子機資訊
                        checking = false;
                    }
                }
            }
            else
            {
                if (mode == "0")
                {
                    frmAI.CancelMeasure();
                    frmPanelAI.dataGridViewPIIN1.Rows.Clear();
                    frmPanelAI.dataGridViewPIIN2.Rows.Clear();
                    UpdateTextBox("", frmPanelAI.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
                    UpdateTextBox("", frmPanelAI.textBoxUnit);  //更新目前檢查基板子機資訊
                }
                else if (mode == "1")
                {
                    frmAI.CancelMeasure();
                    frmPanelAI.dataGridViewLCIN1.Rows.Clear();
                    DeleteTemp();
                    UpdateTextBox("", frmPanelAI.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
                    UpdateTextBox("", frmPanelAI.textBoxUnit);  //更新目前檢查基板子機資訊
                }
            }
        }

        public void StopJudgeTimeCount()
        {
            timerJudgeCount.Stop();
            
            timerJudgeCount.Enabled = false;
            timerJudgeCount.Dispose();
        }
        //測試用主程序
        private void testMonitor(object arg)
        {
            //string[] argList = (string[])arg;
            //string lineId = argList[0];
            //string glassId = argList[1];
            //string prod = argList[2];
            ////呼叫等待人員判定計時器
            ////judgeTimeCount = "1";
            ////timerJudgeCount.Interval = int.Parse(judgeTime);
            ////timerJudgeCount.Enabled = true;
            ////timerJudgeCount.Start();
            ////imagePathList.Add("LT01.jpg");
            ////imagePathList.Add("RB01.jpg");
            ////imagePathList.Add("LT02.jpg");
            ////imagePathList.Add("RB02.jpg");
            ////imagePathList.Add("MT01.jpg");
            ////imagePathList.Add("MB01.jpg");
            ////imagePathList.Add("MT02.jpg");
            ////imagePathList.Add("MB02.jpg");
            //ImageList images = LoadImageList(glassId, lineId);
            ////將LoadImageList回傳的分割的圖片路徑list合併
            //if (mode == "0")
            //{
            //    imagePathList = images.imageNameListABCD;
            //}
            //else
            //{
            //    imagePathList = images.imageNameListCorner.Concat(images.imageNameListNormal).Concat(images.imageNameListOverlap).ToList();
            //}
            ////整理圖檔list順序
            //imagePathList = SortList(imagePathList);
            //ShowImages(imagePathList, glassId, lineId, prod);
            
        }
        private void ThreadMain(object arg)
        {
            threadMain = new Thread(new ParameterizedThreadStart(Monitor));
            threadMain.IsBackground = true;
            threadMain.Start(arg);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        //使用user32.dll來搜尋特定窗體
        [DllImport("USER32.DLL")] //引用User32.dll
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern int SendMessage(
           IntPtr hWnd,　　　// handle to destination window
           int Msg,　　　 // message
           int wParam,　// first message parameter
           int lParam // second message parameter
        );

        const int SC_CLOSE = 0x10;

        private string ReadLastTime()
        {
            string rtn = null;
            string recordPath = Path.Combine("Record.Conf");
            if (!File.Exists(recordPath))
            {
                rtn = DateTime.Now.AddMinutes(2).ToString("yyyyMMddHHmmss");
            }
            else
            {
                StreamReader recordRead = new StreamReader(recordPath);
                string[] lines = File.ReadAllLines(recordPath, Encoding.ASCII);
                rtn = lines[0];
                if (lines[0] == null || lines[0] == "")
                {
                    rtn = DateTime.Now.AddMinutes(2).ToString("yyyyMMddHHmmss");
                }
                else
                {
                    DateTime time1 = DateTime.ParseExact(rtn, "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);

                    if (time1 > DateTime.Now)
                    {
                        rtn = DateTime.Now.AddMinutes(2).ToString("yyyyMMddHHmmss");
                    }
                    else
                    {
                        DateTime time2 = time1.AddMilliseconds(cycleTimeSpan);
                        rtn = time2.ToString("yyyyMMddHHmmss");
                        if (time2 < DateTime.Now)
                        {
                            rtn = DateTime.Now.AddMinutes(2).ToString("yyyyMMddHHmmss");
                        }
                    }
                }
                
            }
            return rtn;
        }
        private void WriteLastTime(string time)
        {
            string recordPath = Path.Combine("Record.Conf");
            string[] lines = { time };
            File.WriteAllLines(recordPath, lines, Encoding.ASCII);
        }
        //檢查SQL Server連線
        public bool TestSqlConn()
        {
            //測試模式一律輸出 true
            if (testMode == "1")
            {
                return true;
            }
            else
            {
                
                SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
                scsb.DataSource = ip + "," + port;
                scsb.UserID = userId;
                scsb.Password = password;
                scsb.ConnectTimeout = 3; //預設連接逾時為3秒
                using (SqlConnection con = new SqlConnection(scsb.ToString()))
                {
                    try
                    {
                        con.Open();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        return false;
                    }
                }
            }

        }
        private void CallWarningForm(string comment, string headText = "None")
        {
            //先關閉舊的
            try
            {
                IntPtr player;
                //檢查是否已存在放大的灰階圖視窗
                if (headText == "None") //若已存在則關閉舊的彈出新的
                {
                    player = FindWindow(null, "frmWarningForm");

                    if (player != IntPtr.Zero)
                    {
                        /* 傳送關閉的指令 */
                        SendMessage(player, SC_CLOSE, 0, 0);
                    }
                    
                    frmWarningForm frmWarningForm = new frmWarningForm();
                    frmWarningForm.Text = "frmWarningForm";
                    frmWarningForm.SetMessage(comment);
                    frmWarningForm.Show();
                }
                else
                {
                    //若已存在則不彈出新的
                    player = FindWindow(null, headText);
                    if (player == IntPtr.Zero)
                    {
                        frmWarningForm frmWarningForm = new frmWarningForm();
                        frmWarningForm.Text = headText;
                        frmWarningForm.SetMessage(comment);
                        frmWarningForm.Show();
                    }

                    
                }
                
            }
            catch
            {

            }
            
            
        }
        private void ReadReleaseTable()
        {
            //讀取Releasetable並寫入字典中
            string releaseDataReadData = "";

            StreamReader releaseDataRead = new StreamReader(@"Release.txt", System.Text.Encoding.Default);
            while (true)
            {
                releaseDataReadData = releaseDataRead.ReadLine();
                if (releaseDataReadData == "" || releaseDataReadData == null)
                {
                    break;
                }
                else if (releaseDataReadData.Substring(0, 1) == "@")
                {
                    continue;
                }
                else
                {
                    string[] releaseDataList = releaseDataReadData.Split(',');
                    releaseTable.Add(releaseDataList[0] + releaseDataList[1]);
                }
            }
        }
        private string ReleaseCheck(string data)
        {
            string rtn = null;
            if (releaseTable.Contains(data))
            {
                rtn = "1";
            }
            else
            {
                rtn = "0";
            }
            return rtn;
        }
        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void buttonCallCheck_Click(object sender, EventArgs e)
        {
            nextTime = DateTime.Now.AddSeconds(5).ToString("yyyyMMddHHmmss");
            //UpdateDateTimeLabel(nextTime, labelNextTime);
            
        }


        //登入成功變更資訊
        private void LoginSuccess()
        {
            if (userID != null)
            {
                UpdateLabel(userID, labelUserID);
                UpdateLabel(loginTable[userID][0], labelUserName);
                UpdateButton("登出", buttonLogin);
                if (userID == "99999999")
                {
                    fixMode = "1";
                }
            }
        }
        //登出成功變更資訊
        private void LogoutSuccess()
        {
            if (userID != null)
            {
                UpdateLabel("00000000", labelUserID);
                UpdateLabel("XXX", labelUserName);
                UpdateButton("登入", buttonLogin);
                fixMode = "0";
            }
        }

        private void buttonDeley_Click(object sender, EventArgs e)
        {
            judgeResult = "Deley";
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            judgeResult = "Change";
        }


        private void ADMRejudgeProcess()
        {
            while (true)
            {
                //宣告路徑
                
            }
        }
        public void ReWriteDataGridView(List<List<string>> data, string type)
        {
            //num = 0 : 左上, num = 1 : 右下
            //type = 0 : 角落, type = 1 : 非角落
            string num = data[0][3];
            if (type == "0")
            {
                if (frmPanelTA.dataGridViewPIIN1.RowCount < 4)
                {
                    frmPanelTA.dataGridViewPIIN1.Rows.Clear();
                    {
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                    }
                }
                //左上的ABCD
                if (num == "01")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (data[0][0] == "A")
                        {
                            if (i == 0)
                            {
                                for (int x = 0; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[x].Value = data[i][x];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 0; x < data[1].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[x].Value = data[i][x];
                                }
                            }
                        }
                       else if (data[0][0] == "B")
                        {
                            if (i == 0)
                            {
                                for (int x = 0; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[x].Value = data[i][x];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 0; x < data[1].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[x].Value = data[i][x];
                                }
                            }
                        }

                    }
                }
                //右下的A & B
                else if ( num == "02")
                {
                    for (int i = 0; i < 2; i++)
                    {
                        if (data[0][0] == "A")
                        {
                            if (i == 0)
                            {
                                for (int x = 1; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[x + 3].Value = data[i][x];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 1; x < data[1].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[x + 3].Value = data[i][x];
                                }
                            }
                        }
                        else if (data[0][0] == "B")
                        {
                            if (i == 0)
                            {
                                for (int x = 1; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[x + 3].Value = data[i][x];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 1; x < data[1].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[x + 3].Value = data[i][x];
                                }
                            }
                        }

                    }
                }
                
            }
            else if (type == "1")
            {
                if (frmPanelTA.dataGridViewPIIN2.RowCount < 6)
                {
                    frmPanelTA.dataGridViewPIIN2.Rows.Clear();
                    {
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                    }
                }
                //左上的ABCD
                if (num == "01")
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[0][0] == "MA")
                        {
                            if (i == 0)
                            {
                                for (int x = 0; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[x].Value = data[i][x];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 0; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[3].Cells[x].Value = data[i][x];
                                }
                            }
                        }
                        else if (data[0][0] == "MB")
                        {
                            if (i == 0)
                            {
                                for (int x = 0; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[x].Value = data[i][x];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 0; x < data[0].Count - 1; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[4].Cells[x].Value = data[i][x];
                                }
                            }
                        }
                        else if (data[0][0] == "MK")
                        {
                            frmPanelTA.dataGridViewPIIN2.Rows[0].Cells[0].Value = data[0][0];
                            frmPanelTA.dataGridViewPIIN2.Rows[0].Cells[3].Value = data[0][1];
                            
                        }
                    }
                }
                //右下的ABCD
                else if (num == "02")
                {
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (data[0][0] == "MA")
                        {
                            if (i == 0)
                            {
                                for (int x = 0; x < data[0].Count - 2; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[x + 4].Value = data[i][x + 1];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 0; x < data[0].Count - 2; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[3].Cells[x + 4].Value = data[i][x + 1];
                                }
                            }
                        }
                        else if (data[0][0] == "MB")
                        {
                            if (i == 0)
                            {
                                for (int x = 0; x < data[0].Count - 2; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[x + 4].Value = data[i][x + 1];
                                }
                            }
                            else if (i == 1)
                            {
                                for (int x = 0; x < data[0].Count - 2; x++)
                                {
                                    frmPanelTA.dataGridViewPIIN2.Rows[4].Cells[x + 4].Value = data[i][x + 1];
                                }
                            }
                        }
                        else if (data[0][0] == "MK")
                        {
                      
                            frmPanelTA.dataGridViewPIIN2.Rows[0].Cells[6].Value = data[0][1];
                        }
                    }
                }
                
            }

            //讀取轉換比例table
            double transRate = 1;
            if (admRate.ContainsKey(lineIDNow))
            {
                transRate = Convert.ToDouble(admRate[lineIDNow]);
            }
            
            //更新距離的計算
            //檢查01左上A
            if (frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[2].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[2].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[3].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[2].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[2].Value.ToString())) * transRate).ToString();
            }
            //檢查01左上B
            if (frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[2].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[2].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[3].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[2].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[2].Value.ToString())) * transRate).ToString();
            }
            //檢查01右下C
            if (frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[1].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[1].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[3].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[1].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[1].Value.ToString())) * transRate).ToString();
            }
            //檢查01右下d
            if (frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[1].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[1].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[3].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[1].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[1].Value.ToString())) * transRate).ToString();
            }


            //檢查02左上A
            if (frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[5].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[5].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[6].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[5].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[5].Value.ToString())) * transRate).ToString();
            }
            //檢查02左上B
            if (frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[5].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[5].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[6].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[5].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[5].Value.ToString())) * transRate).ToString();
            }
            //檢查02右下C
            if (frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[4].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[4].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[6].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[0].Cells[4].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[2].Cells[4].Value.ToString())) * transRate).ToString();
            }
            //檢查02右下D
            if (frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[4].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[4].Value != null)
            {
                frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[6].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[1].Cells[4].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN1.Rows[3].Cells[4].Value.ToString())) * transRate).ToString();
            }

            //檢查01中心A
            if (frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[2].Value != null && frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[2].Value != null)
            {
                frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[3].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[2].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[2].Value.ToString())) * transRate).ToString();
            }
            //檢查01中心B
            if (frmPanelTA.dataGridViewPIIN2.Rows[3].Cells[2].Value != null && frmPanelTA.dataGridViewPIIN2.Rows[4].Cells[2].Value != null)
            {
                frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[3].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[3].Cells[2].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[4].Cells[2].Value.ToString())) * transRate).ToString();
            }

            //檢查02中心A
            if (frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[5].Value != null && frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[5].Value != null)
            {
                frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[6].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[5].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[5].Value.ToString())) * transRate).ToString();
            }
            //檢查02中心B
            if (frmPanelTA.dataGridViewPIIN2.Rows[3].Cells[5].Value != null && frmPanelTA.dataGridViewPIIN2.Rows[4].Cells[5].Value != null)
            {
                frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[6].Value = (Math.Abs(Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[3].Cells[5].Value.ToString()) - Convert.ToInt16(frmPanelTA.dataGridViewPIIN2.Rows[4].Cells[5].Value.ToString())) * transRate).ToString();
            }

            

        }
        //收到讀取基板資料需求後觸發
        private void ReadGlassData(ReadRequest request)
        {
            if (request != null)
            {
                if (labelUserName.Text != "XXX" || labelUserID.Text != "00000000")
                {
                    string glassID = request.glassID;
                    if (waitMeasureDict.ContainsKey(glassID))
                    {
                        lineIDNow = waitMeasureDict[glassID].lineID;
                        prodNow = waitMeasureDict[glassID].prod;
                    }
                    else
                    {
                        MessageBox.Show("在內部資料中找不到此ID : " + glassID + "資料，建議重啟程式");
                        return;
                    }


                   
                    if (mode == "0")
                    {


                        if (!specSettingABCD.ContainsKey(prodNow))
                        {
                            MessageBox.Show("此產品OOS OOC SPEC尚未設定，請確認");
                            return;
                        }

                        //清空現有資料
                        frmPanelTA.dataGridViewPIIN1.Rows.Clear();
                        frmPanelTA.dataGridViewPIIN2.Rows.Clear();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN1.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        frmPanelTA.dataGridViewPIIN2.Rows.Add();
                        object arg = (object)new object[]
                        {
                            lineIDNow,
                            glassID,
                            prodNow,
                            waitMeasureDict[glassID],
                        };
                        ThreadMain(arg);
                        checking = true;
                    }
                    else if (mode == "1")
                    {

                        lineIDNow = request.lineID;
                        prodNow = request.prodName;
                        string pid = request.pid;
                        glassDirNow = imageDirDict[glassID];
                        pidNow = pid;


                        //檢查所有字典是否已設定
                        if (!admRate.ContainsKey(lineIDNow))
                        {
                            MessageBox.Show("此線別比例尺尚未設定，請確認");
                            return;
                        }

                        
                        //清空現有資料
                        frmPanelTA.dataGridViewLCIN1.Rows.Clear();
                        frmAIData.dataGridViewLCIN1.Rows.Clear();
                        //先填好Panel編號
                        List<string> panelNumSplit = null;
                        string[] typeList = new string[] { "Normal Seal", "Overlap", "Corner" };

                        panelNumSplit = recipeDict[pidNow + lineIDNow].headList;
                        for (int x = 0; x < panelNumSplit.Count() * 3; x++)
                        {
                            frmPanelTA.dataGridViewLCIN1.Rows.Add();
                            frmAIData.dataGridViewLCIN1.Rows.Add();
                        }
                        int rowCount = 0;
                        for (int x = 0; x < panelNumSplit.Count(); x++)
                        {
                            foreach (string strTemp in typeList)
                            {
                                frmPanelTA.dataGridViewLCIN1.Rows[rowCount].Cells[0].Value = "P" + panelNumSplit[x];
                                frmPanelTA.dataGridViewLCIN1.Rows[rowCount].Cells[1].Value = strTemp;
                                frmAIData.dataGridViewLCIN1.Rows[rowCount].Cells[0].Value = "P" + panelNumSplit[x];
                                frmAIData.dataGridViewLCIN1.Rows[rowCount].Cells[1].Value = strTemp;
                                rowCount++;
                            }

                        }


                        if (prodNow == "None")
                        {
                            MessageBox.Show("GlassID : " + glassID + " Recipe未設定，請通知PM確認");
                            return;
                        }

                        //檢查是否為校正模式
                        if (fixMode == "1")
                        {
                            CallMessageForm("目前為校正模式，請注意");
                        }

                        File.Create(Path.Combine(imageDirDict[glassID], "checking.tmp"));   //創建識別量測中用的旗標暫存檔

                        object arg = (object)new object[]
                        {
                            lineIDNow,
                            glassID,
                            prodNow,
                            waitMeasureDict[glassID]
                        };
                        ThreadMain(arg);
                        checking = true;
                    }

                    
                }
                else
                {
                    MessageBox.Show("請先登入");
                }
            }
            
        }



        //取得產品名稱
        private string[] GetProd(string glassID, string lineID, string imageDirPath = "None")
        {
            string prod = "";
            string pid = "";
            string[] rtn = null;
            if (mode == "0")
            {
                string formatPath = "";
                string formatPathLocal = "";
                string formatDir = Path.Combine(fileServer, "LCD", "2100", glassID.Substring(0, 5), glassID.Substring(0, 10), "FORMAT");
                string[] formatPathList = Directory.GetFiles(formatDir);
                string[] formatDirListLocal = Directory.GetFiles(Path.Combine(Application.StartupPath, "temp"));
                //檢查暫存區是否有此id的format
                foreach (string fileTempLocal in formatDirListLocal)
                {
                    if (Path.GetFileNameWithoutExtension(fileTempLocal).Substring(0, 12) == glassID)
                    {
                        formatPathLocal = fileTempLocal;
                    }
                }
                //暫存區未找到
                if (formatPath == "")
                {
                    foreach (string fileTemp in formatPathList)
                    {
                        if (Path.GetFileNameWithoutExtension(fileTemp).Substring(0, 12) == glassID)
                        {
                            formatPathLocal = Path.Combine(Path.Combine(Application.StartupPath, "temp"), glassID + ".txt");
                            try
                            {
                                File.Copy(fileTemp, formatPathLocal, true);
                                formatPath = formatPathLocal;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("下載Format失敗，Message : " + ex.ToString());
                            }

                        }
                    }
                }

                if (formatPath != "")
                {
                    using (StreamReader sr = new StreamReader(formatPathLocal))
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            string readData = sr.ReadLine();
                            if (i == 1)
                            {
                                prod = readData.Substring(15, 4);
                                rtn = new string[] { prod };
                            }
                        }
                    }
                }
            }
            else if (mode == "1")
            {
                
                if (Directory.GetFiles(imageDirPath).Length > 0)    //確認資料夾下有圖片檔案
                {
                    foreach (string temp in Directory.GetFiles(imageDirPath).ToArray())
                    {
                        if (String.Equals(Path.GetExtension(temp), ".bmp", StringComparison.CurrentCultureIgnoreCase))
                        {
                            string[] imageNameSplit = Path.GetFileNameWithoutExtension(temp).Split('_');
                            string sealUnit = imageNameSplit[1].Substring(1, 1);  //取得子機編號
                            if(!sealUnitDict.ContainsKey(glassID))
                            {
                                sealUnitDict.Add(glassID, sealUnit);
                            }
                            
                            recipeIDNow = imageNameSplit[2];    //取得PID
                            if (recipeDict.ContainsKey(recipeIDNow + lineID))
                            {
                                prod = recipeDict[recipeIDNow + lineID].prodName;
                                rtn = new string[] { prod, recipeIDNow };
                                break;
                            }
                            else
                            {
                                rtn = new string[] { "None", "None" };
                                break;
                            }
                            
                        }
                        else
                        {
                            rtn = new string[] { "Reject", "Reject" };
                            break;
                        }

                    }
                  
                }
                else
                {
                    rtn = new string[] { "empty", "empty" };
                }
            }
            
            
            return rtn;
        }
        //重新排序List
        private List<string> SortList(List<string> list, string prod, string pid, string lineID)
        {
            List<string> rtn = new List<string> { };
            int flag = 1;   //標記目前排序的編號
            int flag2 = 1;  //標記迴圈是否須中斷
            int checkTypeFlag = 0;  //標記LCIN排序時排到哪種影像類型
            int checkFlag = 0;
            string imageName = "";
            string imageNum = "";
            string imageType = "";
            while (list.Count > 0)
            {
                if (mode == "0")
                {
                    foreach (string checkTemp in admImageSort)  //依序比較ini設定的順序
                    {
                        flag2 = 1;
                        checkFlag = 0;
                        while (flag2 == 1)
                        {
                            if (list.Count == 2)
                            {

                            }
                            string[] splitTemp = list[checkFlag].Split('\\');
                            imageName = splitTemp[splitTemp.Count() - 1].Split('.')[0].Substring(0, 2);
                            imageNum = splitTemp[splitTemp.Count() - 1].Split('.')[0].Substring(2, 2);
                            //檢查list中的檔名是否與ini設定的順序相同
                            if (imageName == checkTemp && Convert.ToInt16(imageNum) == flag)
                            {
                                if (imageName == "MK" && flag > 2)
                                {
                                    continue;   //MK只有1 & 2
                                }
                                flag2 = 0;
                                rtn.Add(list[checkFlag]);
                                list.Remove(list[checkFlag]); //已比較成功的元素由list中刪除避免重複比較
                            }
                            else
                            {
                                if (checkFlag < list.Count - 1)
                                {
                                    checkFlag++;
                                }
                                else
                                {
                                    checkFlag = 0;
                                }
                            }
                        }
                        if (imageName == "MB")  //比較到MB代表這一模已比較完畢
                        {
                            flag++;
                            break;  //跳出迴圈讓admImageSort重頭跑一次
                        }
                    }
                }
                else if (mode == "1")
                {
                    //解析此產品用到多少Head
                    int headCount = 0;
                    List<string> headList = null;
                    
                    headList = recipeDict[pid + lineID].headList;
                    headCount = headList.Count();
                    
                    
                    foreach (string checkTemp in headList)  //依序比較ini設定的順序
                    {
                        string headCheckStr = "P" + checkTemp;

                        List<string> checkTypeList = admImageSort;
                        foreach (string checkTypeTemp in checkTypeList)
                        {
                            flag2 = 1;
                            checkFlag = 0;

                            while (flag2 == 1)
                            {

                                string[] splitTemp = list[checkFlag].Split('\\');
                                imageName = splitTemp[splitTemp.Count() - 1].Split('.')[0];
                                string imageTypeSpilt = imageName.Split('_')[3].Split('-')[1];
                                if (imageTypeSpilt.Substring(0, 1).Equals("n", StringComparison.OrdinalIgnoreCase))
                                {
                                    imageType = "normal";
                                }
                                else if (imageTypeSpilt.Substring(0, 1).Equals("c", StringComparison.OrdinalIgnoreCase))
                                {
                                    imageType = "corner";
                                }
                                else if (imageTypeSpilt.Substring(0, 1).Equals("o", StringComparison.OrdinalIgnoreCase))
                                {
                                    imageType = "overlap";
                                }
                                
                                //檢查list中的檔名是否與ini設定的順序相同
                                if (imageName.Split('_')[3].Split('-')[0] == headCheckStr && imageType.Equals(checkTypeTemp, StringComparison.OrdinalIgnoreCase))
                                {
                                    flag2 = 0;
                                    checkTypeFlag++;
                                    rtn.Add(list[checkFlag]);
                                    list.Remove(list[checkFlag]); //已比較成功的元素由list中刪除避免重複比較

                                }
                                else
                                {
                                    if (checkFlag < list.Count - 1)
                                    {
                                        checkFlag++;
                                    }
                                    else
                                    {
                                        flag2 = 0;
                                    }
                                    //else
                                    //{
                                    //    MessageBox.Show("圖檔重新排序時發現與Head編號設定不同或是圖檔顯示順序設定有誤導致無法比對成功，請確認");
                                    //    rtn.Add("None");
                                    //    return rtn;
                                    //}
                                }
                            }
                            if (imageName.Split('_')[3] == headList[headCount - 1] && imageType == checkTypeList[2])  //這一模已比較完畢
                            {
                                flag++;
                                break;  //跳出迴圈讓admImageSort重頭跑一次
                            }
                        }
                        
                    }
                    //如果清單還有代表有other，依序加入圖檔清單但不排序
                    if (list.Count != 0)
                    {
                        checkFlag = 0;
                        while(checkFlag < list.Count)
                        {
                            string[] splitTemp = list[checkFlag].Split('\\');
                            imageName = splitTemp[splitTemp.Count() - 1].Split('.')[0];
                            string imageTypeSpilt = imageName.Split('_')[3].Split('-')[1];
                            if (imageTypeSpilt.Substring(0, 3).Equals("oth", StringComparison.CurrentCulture))
                            {
                                rtn.Add(list[checkFlag]);
                                list.Remove(list[checkFlag]);
                            }
                            else
                            {
                                checkFlag++;
                            }
                        }
                    }
                    //全部完成後正常list應該count == 0 檢查一次
                    if (list.Count != 0)
                    {
                        
                        rtn.Clear();
                        rtn.Add("None");
                        return rtn;
                    }
                }
            }
            return rtn;
        }

        private void ReadLoginTable(StreamReader loginTableDataRead)
        {
            string loginTableDataReadData = "";
            
            while (true)
            {
                loginTableDataReadData = loginTableDataRead.ReadLine();
                if (loginTableDataReadData == "" || loginTableDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] loginTableDtatList = loginTableDataReadData.Split(',');
                    List<string> userData = new List<string> { };
                    userData.Add(loginTableDtatList[1].Replace(" ", ""));
                    userData.Add(loginTableDtatList[2].Replace(" ", ""));
                    userData.Add(loginTableDtatList[3].Replace(" ", ""));
                    loginTable.Add(loginTableDtatList[0].Replace("\"", "").Replace(" ", ""), userData);
                }
            }
        }

        //private void ReadUserName(StreamReader userNameDataRead)
        //{
        //    string userNameDataReadData = "";
        //    int x = 0;
        //    while (true)
        //    {
        //        userNameDataReadData = userNameDataRead.ReadLine();
        //        if (userNameDataReadData == "" || userNameDataReadData == null)
        //        {
        //            break;
        //        }
        //        else if (x == 0)
        //        {
        //            x++;
        //            continue;
        //        }
        //        else
        //        {
        //            string[] userNameDtatList = userNameDataReadData.Split(',');
        //            userName.Add(userNameDtatList[0].Replace("\"", "").Replace(" ", ""), userNameDtatList[1].Replace("\"", "").Replace(" ", ""));
        //        }
        //    }
        //}
        private void ReadADMRate(StreamReader admRateDataRead)
        {
            string admRateDataReadData = "";
           
            while (true)
            {
                admRateDataReadData = admRateDataRead.ReadLine();
                if (admRateDataReadData == "" || admRateDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] admRateDtatList = admRateDataReadData.Split(',');
                    admRate.Add(admRateDtatList[0], admRateDtatList[1]);
                }
            }
        }
        private void ReadSpec(StreamReader specDataRead)
        {
            string specDataReadData = "";
          
            while (true)
            {
                specDataReadData = specDataRead.ReadLine();
                if (specDataReadData == "" || specDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] specDtatList = specDataReadData.Split(',');
                    string[] specList = specDtatList[1].Split('&');
                    //specSetting.Add(specDtatList[0], specList);
                }
            }
        }
        private void ReadSpecABCD(StreamReader specDataRead)
        {
            string specDataReadData = "";
           
            while (true)
            {
                specDataReadData = specDataRead.ReadLine();
                if (specDataReadData == "" || specDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] specDtatList = specDataReadData.Split(',');
                    string[] specList = specDtatList[1].Split('&');

                    specSettingABCD.Add(specDtatList[0], new PIINSpec
                    {
                        aOOSMax = specList[0],
                        aOOSMin = specList[1],
                        aOOCMax = specList[2],
                        aOOCMin = specList[3],
                        bOOSMax = specList[4],
                        bOOSMin = specList[5],
                        bOOCMax = specList[6],
                        bOOCMin = specList[7],
                        cOOSMax = specList[8],
                        cOOSMin = specList[9],
                        cOOCMax = specList[10],
                        cOOCMin = specList[11],
                        dOOSMax = specList[12],
                        dOOSMin = specList[13],
                        dOOCMax = specList[14],
                        dOOCMin = specList[15]
                    });
                }
            }
        }
        private void ReadABCDCount(StreamReader abcdCountDataRead)
        {
            string abcdCountDataReadData = "";
          
            while (true)
            {
                abcdCountDataReadData = abcdCountDataRead.ReadLine();
                if (abcdCountDataReadData == "" || abcdCountDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] abcdCountDtatList = abcdCountDataReadData.Split(',');
                    string abcdCountList = abcdCountDtatList[1];
                    aBCDCount.Add(abcdCountDtatList[0], abcdCountList);
                }
            }
        }

        private void MeasureFinishied(DataSourceType dataSource)
        {
            if (dataSource == DataSourceType.TA)
            {
                if (mode == "0")
                {
                    measureDataSummary.Clear();
                    //檢查資料完整性
                    for (int x = 0; x < frmPanelTA.dataGridViewPIIN1.RowCount; x++)
                    {
                        string type = "";
                        switch (x)
                        {
                            case 0:
                                type = "A";
                                break;
                            case 1:
                                type = "B";
                                break;
                            case 2:
                                type = "C";
                                break;
                            case 3:
                                type = "D";
                                break;
                        }
                        if (frmPanelTA.dataGridViewPIIN1.Rows[x].Cells[3].Value == null)
                        {
                            MessageBox.Show("左上膜" + type + "值 " + "，" + "上傳資料不完整，請確認");
                            return;
                        }
                        else if (frmPanelTA.dataGridViewPIIN1.Rows[x].Cells[6].Value == null)
                        {
                            MessageBox.Show("右下膜" + type + "值 " + "，" + "上傳資料不完整，請確認");
                            return;
                        }

                    }
                    //檢查MT MB量測資訊完整性
                    if (frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[3].Value == null)
                    {
                        MessageBox.Show("左上膜MT值 " + "，" + "上傳資料不完整，請確認");
                        return;
                    }
                    else if (frmPanelTA.dataGridViewPIIN2.Rows[1].Cells[6].Value == null)
                    {
                        MessageBox.Show("右下膜MT值 " + "，" + "上傳資料不完整，請確認");
                        return;
                    }
                    else if (frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[3].Value == null)
                    {
                        MessageBox.Show("左上膜MB值 " + "，" + "上傳資料不完整，請確認");
                        return;
                    }
                    else if (frmPanelTA.dataGridViewPIIN2.Rows[2].Cells[6].Value == null)
                    {
                        MessageBox.Show("右下膜MB值 " + "，" + "上傳資料不完整，請確認");
                        return;
                    }
                }
                else if (mode == "1")
                {
                  
                    measureDataSummary.Clear();
                    //檢查資料完整性
                    for (int x = 0; x < frmPanelTA.dataGridViewLCIN1.RowCount; x++)
                    {
                        if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() != "Corner")
                        {
                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value == null)
                            {
                                MessageBox.Show(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "上傳資料不完整，請確認");
                                return;
                            }
                        }
                        else if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Corner")
                        {
                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value == null)
                            {
                                MessageBox.Show(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "上傳資料不完整，請確認");
                                return;
                            }
                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value == null)
                            {
                                MessageBox.Show(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "上傳資料不完整，請確認");
                                return;
                            }
                        }
                    }
                    
                  
                }

                //上傳量測後影像
                string imageDirLocal = Path.Combine(Application.StartupPath, "Temp", glassIDNow);
                string imageDir = "";
                if (mode == "0")
                {
                    imageDir = Path.Combine(resultImagePath, "PI" + lineIDNow.Substring(4, 2), glassIDNow.Substring(0, 12));
                }
                else if (mode == "1")
                {
                    imageDir = Path.Combine(resultImagePath, "TPRB" + lineIDNow.Substring(4, 2) + "00", DateTime.Now.ToString("yyyyMMdd"), glassIDNow.Substring(0, 12));
                }
                try
                {
                    DirectoryCopy(imageDirLocal, imageDir, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("上傳量測後影像發生錯誤，Message : " + ex.ToString());
                    return;
                }

                //清空picturebox圖像
                frmABCD.PictureBoxDelect();
                frmLCIN.PictureBoxDelect();

                //刪除量測中識別用旗標暫存檔
                DeleteTemp();

                Thread.Sleep(1000);

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        DeleteSrcFolder(glassDirNow);   //刪除量測圖檔
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(100);
                        //if (i == 2)
                        //{
                        //    MessageBox.Show("量測後刪除來源影像時發生錯誤，Message : " + ex.ToString());

                        //}

                    }
                }

                if (mode == "0")
                {
                    ReturnDataTable rdt = ConvertDt("ABCD", frmPanelTA.dataGridViewPIIN1, frmPanelTA.dataGridViewPIIN2);
                    ExportDataToExcel(Path.Combine(Application.StartupPath, "ABCD.xlsx"), "ABCD", rdt.dataTable, rdt.dataTable2);
                }
                else if (mode == "1")
                {
                    if (uploadSpcData == "1")
                    {
                        try
                        {
                            //開始將DATAGRIDVIEW轉成ARRAY
                            measureDataSummary = DataGridViewToArray(dataSource);
                            GenerateXMLFile(measureDataSummary, dataSource);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("上報失敗，Message : " + ex.ToString());
                            return;
                        }
                    }
                    //紀錄量測記錄
                    WriteToMeasureLog(glassIDNow, lineIDNow, userID, loginTable[userID][0]);
                    //寫入Excel
                    GenerateMeasureDataToXlsx(DataSourceType.TA);
                    if (uploadSpcData == "0")
                    {
                        DialogResult dr2 = MessageBox.Show("是否生成複製到Excel用的列表資料?", "確認視窗", MessageBoxButtons.OKCancel);
                        if (dr2 == DialogResult.OK)
                        {
                            if (measureDataSummary.Count == 4)
                            {
                                frmExcelCopyData.UpdataDataGridView(measureDataSummary);
                                frmExcelCopyData.ShowDialog();
                            }
                        }
                    }
                    if (aiMode == "1")
                    {
                        frmLCIN.GenerateAIXlsx(frmPanelTA.dataGridViewLCIN1, frmAIData.dataGridViewLCIN1);
                    }

                }


                //輸出完成清空資料
                imageDirDict.Remove(glassIDNow);
                waitMeasureDict.Remove(glassIDNow);
                ClearMeasureData(DataSourceType.TA);
                checking = false;
            }
            else if (dataSource == DataSourceType.AI)
            {
                //檢查資料完整性
                for (int x = 0; x < frmPanelAI.dataGridViewLCIN1.RowCount; x++)
                {
                    if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() != "Corner")
                    {
                        if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value == null)
                        {
                            WriteToAILog(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "上傳資料不完整", "Error", true);
                            return;
                        }
                    }
                    else if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Corner")
                    {
                        if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value == null)
                        {
                            WriteToAILog(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "上傳資料不完整", "Error", true);
                            return;
                        }
                        if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value == null)
                        {
                            WriteToAILog(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "上傳資料不完整", "Error", true);
                            return;
                        }
                    }
                }


                //上傳量測後影像
                string imageDirLocal = Path.Combine(Application.StartupPath, "Temp", glassIDNowAI);
                string imageDir = "";
                if (mode == "0")
                {
                    imageDir = Path.Combine(resultImagePath, "PI" + lineIDNowAI.Substring(4, 2), glassIDNowAI.Substring(0, 12));
                }
                else if (mode == "1")
                {
                    imageDir = Path.Combine(resultImagePath, "TPRB" + lineIDNowAI.Substring(4, 2) + "00", DateTime.Now.ToString("yyyyMMdd"), glassIDNowAI.Substring(0, 12));
                }
                try
                {
                    DirectoryCopy(imageDirLocal, imageDir, true);
                }
                catch (Exception ex)
                {
                    WriteToAILog("上傳量測後影像發生錯誤，Message : " + ex.ToString(), "Error", true);
   
                    return;
                }

                //清空picturebox圖像
                frmAI.PictureBoxDelect();

                Thread.Sleep(1000);

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        DeleteSrcFolder(glassDirNowAI);   //刪除量測圖檔
                        break;
                    }
                    catch
                    {
                        Thread.Sleep(100);
                        //if (i == 2)
                        //{
                        //    MessageBox.Show("量測後刪除來源影像時發生錯誤，Message : " + ex.ToString());

                        //}

                    }
                }

                if (mode == "0")
                {
                    ReturnDataTable rdt = ConvertDt("ABCD", frmPanelTA.dataGridViewPIIN1, frmPanelTA.dataGridViewPIIN2);
                    ExportDataToExcel(Path.Combine(Application.StartupPath, "ABCD.xlsx"), "ABCD", rdt.dataTable, rdt.dataTable2);
                }
                else if (mode == "1")
                {
                    if (uploadSpcData == "1")
                    {
                        try
                        {
                            //開始將DATAGRIDVIEW轉成ARRAY
                            measureDataSummary = DataGridViewToArray(dataSource);
                            GenerateXMLFile(measureDataSummary, dataSource);
                        }
                        catch (Exception ex)
                        {
                            WriteToAILog("上報失敗，Message : " + ex.ToString(), "Error", true);
                            return;
                        }
                    }
                    //紀錄量測記錄
                    WriteToMeasureLog(glassIDNowAI, lineIDNowAI, userID, "AI");
                    //寫入Excel
                    GenerateMeasureDataToXlsx(DataSourceType.AI);
                    string result = overSpecAIFlag == "0" ? "OK" : overSpecAIFlag == "1" ? "OOC" : "OOS";
                    AppendRichTextBox(DateTime.Now.ToString("[HH:mm:ss]") + " " + glassIDNowAI + " " + lineIDNowAI + " " + prodNowAI + " " + result, frmPanelAI.richTextBoxAIResult, Color.Green);

                }

                //輸出完成清空資料
                imageDirDict.Remove(glassIDNowAI);
                waitMeasureDict.Remove(glassIDNowAI);
                ClearMeasureData(DataSourceType.AI);
            }
        }
        private void ClearMeasureData(DataSourceType dataSource)
        {
            if (dataSource == DataSourceType.TA)
            {
                if (mode == "0")
                {
                    //清空現有資料
                    frmPanelTA.dataGridViewPIIN1.Rows.Clear();
                    frmPanelTA.dataGridViewPIIN2.Rows.Clear();
                    frmABCD.PictureBoxDelect();

                }
                else if (mode == "1")
                {
                    //量測結束清除資料
                    frmLCIN.CancelMeasure();
                    frmPanelTA.dataGridViewLCIN1.Rows.Clear();

                    UpdateTextBox("", frmPanelTA.textBoxUnit);  //更新目前檢查基板子機資訊
                }
                UpdateTextBox("", frmPanelTA.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
            }
            else
            {
                if (mode == "0")
                {
                    //清空現有資料
                    frmPanelAI.dataGridViewPIIN1.Rows.Clear();
                    frmPanelAI.dataGridViewPIIN2.Rows.Clear();
                    frmABCD.PictureBoxDelect();

                }
                else if (mode == "1")
                {
                    //量測結束清除資料
                    frmAI.CancelMeasure();
                    frmPanelAI.dataGridViewLCIN1.Rows.Clear();

                    UpdateTextBox("", frmPanelAI.textBoxUnit);  //更新目前檢查基板子機資訊
                }
                UpdateTextBox("", frmPanelAI.textBoxGlassIdNow);  //更新目前檢查基板ID資訊
            }
        }
        //將dataTable輸出EXCEL
        public void ExportDataToExcel(string localFilePath, string type, DataTable TableName = null, DataTable TableName2 = null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //設定文件標題
            saveFileDialog.Title = "匯出Excel文件";
            //設定文件型別
            saveFileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx";
            //設定預設文件型別顯示順序  
            saveFileDialog.FilterIndex = 1;
            //是否自動在文件名中新增副檔名
            saveFileDialog.AddExtension = true;
            //是否記憶上次開啟的目錄
            saveFileDialog.RestoreDirectory = true;
            //設定預設文件名
            saveFileDialog.FileName = type;
            //資料初始化
            //int TotalCount = 0;     //總行數
            //int RowRead = 0;    //已讀行數
            //decimal Percent = 0;    //百分比
            //int excelColumn = 0;


            //初始化文件路徑


            //秒鐘
            Stopwatch timer = new Stopwatch();
            timer.Start();

            if (type == "ABCD")
            {
                //DataTable格式
           
                if (File.Exists(localFilePath))
                {
                    //創建Excel檔案

                    //打開excel報表
                    FileInfo FileInfoXLS = new FileInfo(localFilePath);
                    ExcelPackage ep = new ExcelPackage(FileInfoXLS);

                    if (ep.Workbook.Worksheets[lineIDNow] == null)
                    {
                        ep.Workbook.Worksheets.Copy("Sample", lineIDNow);
                    }
                    else
                    {

                    }

                    ExcelWorksheet sheet1 = ep.Workbook.Worksheets[lineIDNow];//取得Sheet1
                    int startRowNumber = sheet1.Dimension.Start.Row;//起始列編號，從1算起
                    int endRowNumber = sheet1.Dimension.End.Row;//結束列編號，從1算起
                    int startColumn = sheet1.Dimension.Start.Column;//開始欄編號，從1算起
                    int endColumn = sheet1.Dimension.End.Column;//結束欄編號，從1算起
                    
                    ////開始合併儲存格
                    //for(int x = 1; x < 8; x++)
                    //{
                    //    //合併儲存格
                    //    sheet1.Cells[endRowNumber + 1, x, endRowNumber + 8, x].Merge = true;
                    //}
                    ////合併工程師確認欄儲存格
                    //sheet1.Cells[endRowNumber + 1, 16, endRowNumber + 8, 16].Merge = true;
                    //填入ID與工號等資訊
                    
                    sheet1.Cells[endRowNumber + 1, 1].Value = DateTime.Now.ToString("yyyy/MM/dd");   //日期
                    sheet1.Cells[endRowNumber + 1, 2].Value = DateTime.Now.ToString("HH:mm:ss");   //時間

                    sheet1.Cells[endRowNumber + 1, 4].Value = glassIDNow;   //GlassID
                    sheet1.Cells[endRowNumber + 1, 5].Value = labelUserID.Text;   //工號

                    ////寫入ABCD標籤
                    //string[] title = new string[] { "A", "B", "C", "D", "A", "B", "C", "D" };
                    //for(int c = 1; c < 9; c++)
                    //{
                    //    sheet1.Cells[endRowNumber + c, 8].Value = title[c - 1];
                    //    sheet1.Cells[endRowNumber + c, 12].Value = title[c - 1];
                    //    //ABCD標籤變更底色
                    //    sheet1.Cells[endRowNumber + c, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    sheet1.Cells[endRowNumber + c, 8].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    //    sheet1.Cells[endRowNumber + c, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //    sheet1.Cells[endRowNumber + c, 12].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                    //}

                    
                    //填入Mark確認資料
                    sheet1.Cells[endRowNumber + 1, 6].Value = TableName2.Rows[0][3].ToString();    //MK1
                    sheet1.Cells[endRowNumber + 1, 7].Value = TableName2.Rows[0][6].ToString();    //MK2
                    //填入ABCD量測資料
                    for (int a = 0; a < 8; a++)
                    {
                        sheet1.Cells[endRowNumber + 1, 8 + a].Value = Convert.ToDouble(TableName.Rows[a%4][(a / 4) * 3 + 3].ToString());
                        if (frmPanelTA.dataGridViewPIIN1.Rows[a % 4].Cells[(a / 4) * 3 + 3].Style.BackColor == Color.Red)
                        {
                            sheet1.Cells[endRowNumber + 1, 8 + a].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            sheet1.Cells[endRowNumber + 1, 8 + a].Style.Fill.BackgroundColor.SetColor(Color.Red);
                        }
                        else if (frmPanelTA.dataGridViewPIIN1.Rows[a % 4].Cells[(a / 4) * 3 + 3].Style.BackColor == Color.Green)
                        {
                            sheet1.Cells[endRowNumber + 1, 8 + a].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            sheet1.Cells[endRowNumber + 1, 8 + a].Style.Fill.BackgroundColor.SetColor(Color.Green);
                        }
                    }
                    //設定格式
                    //sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 1].Style.Numberformat.Format = "yyyy-mm-dd";
                    //sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 1].Style.Numberformat.Format = "HHmm";
                    sheet1.Cells[endRowNumber + 1, 8, endRowNumber + 1, 15].Style.Numberformat.Format = "0.00";

                    //增加框線
                    //下框線
                    sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 16].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    //上框線
                    sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 16].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    //右框線
                    sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 16].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    //左框線
                    sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 16].Style.Border.Left.Style = ExcelBorderStyle.Thin;

                    //設定置中
                    sheet1.Cells[endRowNumber + 1, 1, endRowNumber + 1, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    //設置顏色


                    //建立檔案串流
                    FileStream OutputStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                    //把剛剛的Excel物件真實存進檔案裡
                    ep.SaveAs(OutputStream);
                    //關閉串流
                    OutputStream.Close();
                }
            }
        }
        public ReturnDataTable ConvertDt(string type, DataGridView Arrays, DataGridView Arrays2 = null)
        {
            //將DataGridView的HeadText寫入List中

            List<string> ColumnNames = new List<string> { };
            List<string> ColumnNames2 = new List<string> { };

            if (type == "ABCD")
            {
                for (int x = 0; x < frmPanelTA.dataGridViewPIIN1.ColumnCount; x++)
                {
                    ColumnNames.Add(frmPanelTA.dataGridViewPIIN1.Columns[x].Name);
                }
                for (int x = 0; x < frmPanelTA.dataGridViewPIIN1.ColumnCount; x++)
                {
                    ColumnNames2.Add(frmPanelTA.dataGridViewPIIN2.Columns[x].Name);
                }
            }
            

            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();

            foreach (string ColumnName in ColumnNames)
            {
                dt.Columns.Add(ColumnName, typeof(string));
            }
            foreach (string ColumnName in ColumnNames2)
            {
                dt2.Columns.Add(ColumnName, typeof(string));
            }

            for (int i1 = 0; i1 < Arrays.RowCount; i1++)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < ColumnNames.Count; i++)
                {
                    if (Arrays[i, i1].Value != null)
                    {
                        dr[i] = Arrays[i, i1].Value.ToString();
                    }
                    else
                    {
                        dr[i] = " ";
                    }
                }
                dt.Rows.Add(dr);
            }
            for (int i1 = 0; i1 < Arrays2.RowCount; i1++)
            {
                DataRow dr2 = dt2.NewRow();
                for (int i = 0; i < ColumnNames2.Count; i++)
                {
                    if (Arrays2[i, i1].Value != null)
                    {
                        dr2[i] = Arrays2[i, i1].Value.ToString();
                    }
                    else
                    {
                        dr2[i] = " ";
                    }
                }
                dt2.Rows.Add(dr2);
            }

            return new ReturnDataTable { dataTable = dt, dataTable2 = dt2 };

        }

        //因C#沒有複製資料夾的METHOD，在此創造一個METHOD
        private static void CopyDirectory(string srcDirectory, string dstDirectory)
        {
            if (!Directory.Exists(dstDirectory))
            {
                try
                {
                    Directory.CreateDirectory(dstDirectory);
                }
                catch
                {

                }
            }

            DirectoryInfo sdir = new DirectoryInfo(srcDirectory);
            foreach (FileInfo fi in sdir.GetFiles())
            {
                try
                {
                    File.Copy(fi.FullName, dstDirectory + Path.DirectorySeparatorChar + fi.Name, true);
                }
                catch
                {

                }
            }
            foreach (DirectoryInfo di in sdir.GetDirectories())
            {
                try
                {
                    CopyDirectory(di.FullName, dstDirectory + Path.DirectorySeparatorChar + di.Name);
                }
                catch
                {

                }
            }
        }

        private void TransImageToABCDDir(string glassID, string lineID)
        {
            //組合路徑
            string imageDir = Path.Combine(resultImagePath, "PI" + lineID.Substring(4, 2), DateTime.Now.ToString("yyyyMMdd"), glassID);
            string imageTargetDir = Path.Combine(resultImagePath, "ABCD", lineID,  glassID);
            WriteToLog("組合影像路徑 : " + imageDir, lineID);
            if (!Directory.Exists(imageDir))
            {
                //重新組合備選路徑
                imageDir = Path.Combine(resultImagePath, "PI" + lineID.Substring(4, 2), DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), glassID);
                WriteToLog("未發現影像資料夾，組合備選路徑 : " + imageDir, lineID);
            }

            if (Directory.Exists(imageDir))
            {
                if (!Directory.Exists(imageTargetDir))
                {
                    try
                    {
                        Directory.CreateDirectory(imageTargetDir);
                    }
                    catch(Exception ex)
                    {
                        WriteToLog("創建目標資料夾失敗 : " + imageTargetDir, lineID);
                        WriteToLog("創建目標資料夾失敗，Message : " + ex.ToString(), lineID);
                    }
                }
                try
                {
                    DirectoryCopy(imageDir, imageTargetDir, true);
                    WriteToLog("傳送影像資料夾完畢 : " + imageTargetDir, lineID);
                }
                catch(Exception ex)
                {
                    WriteToLog("傳送影像資料夾失敗，Message : " + ex.ToString(), lineID);
                }
            }


        }

        private void TransImageToServer(string glassID, string lineID)
        {
            //組合路徑
            string imageDir = Path.Combine(resultImagePath, "ABCD", lineID, glassID);
            string imageTargetDir = Path.Combine(resultImagePath, "PI" + lineID.Substring(4, 2), DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), glassID);
            WriteToLog("組合影像目標路徑 : " + imageTargetDir, lineID);
            if (!Directory.Exists(imageTargetDir))
            {
                //重新組合備選路徑
                imageTargetDir = Path.Combine(resultImagePath, "PI" + lineID.Substring(4, 2), DateTime.Now.ToString("yyyyMMdd"), glassID);
                WriteToLog("未發現影像目標資料夾，組合備選路徑 : " + imageDir, lineID);
            }

            if (Directory.Exists(imageDir))
            {
                try
                {
                    DirectoryCopy(imageDir, imageTargetDir, true);
                    WriteToLog("傳送影像資料夾完畢 : " + imageTargetDir, lineID);
                }
                catch (Exception ex)
                {
                    WriteToLog("傳送影像資料夾失敗，Message : " + ex.ToString(), lineID);
                }
            }
        }
        private void ChoseType()
        {
            while (true)
            {
                frmTypeChose frmTypeChose = new frmTypeChose();
                DialogResult dr = frmTypeChose.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (frmTypeChose.GetMsg() == "9")   //代表未選擇
                    {
                        MessageBox.Show("請選擇機台類別");
                    }
                    else
                    {
                        switch (frmTypeChose.GetMsg())
                        {
                            case "PIIN-ABCD":
                                mode = "0";
                                break;
                            case "LCIN-SealDistance":
                                mode = "1";
                                break;
              
                        }
                        break;
                    }
                }
                else if (dr == DialogResult.Cancel)
                {
                    Application.Exit();
                    break;
                }
            }
        }

        private void 系統設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemSetting.ReadINI();
            frmSystemSetting.ShowDialog();
        }

        public void ReadINI()
        {
            GetValue("PATH", "ImageServer", out outString, IniFilePath);
            imageServer = outString;
            GetValue("PATH", "FileServer", out outString, IniFilePath);
            fileServer = outString;
            GetValue("PATH", "ReMeasurePath", out outString, IniFilePath);
            reMeasurePath = outString;
            GetValue("PATH", "AIServer", out outString, IniFilePath);
            aiServer = outString;
            GetValue("PATH", "RecipeDir", out outString, IniFilePath);
            recipeDir = outString;
            GetValue("SQL Server", "Constr", out outString, IniFilePath);
            constrSample = outString;
            GetValue("SQL Server", "IP", out outString, IniFilePath);
            ip = outString;
            GetValue("SQL Server", "Port", out outString, IniFilePath);
            port = outString;
            GetValue("SQL Server", "UserID", out outString, IniFilePath);
            userId = outString;
            GetValue("SQL Server", "Password", out outString, IniFilePath);
            password = outString;
            GetValue("PATH", "ResultPath", out outString, IniFilePath);
            resultPath = outString;
            GetValue("PATH", "ResultImagePath", out outString, IniFilePath);
            resultImagePath = outString;
            GetValue("FEATURES", "UploadSPCData", out outString, IniFilePath);
            uploadSpcData = outString;
            GetValue("FEATURES", "DpiX", out outString, IniFilePath);
            dpiX = outString;
            GetValue("FEATURES", "DpiY", out outString, IniFilePath);
            dpiY = outString;
            GetValue("FEATURES", "IBWMode", out outString, IniFilePath);
            ibwMode = outString;
            GetValue("FEATURES", "AIMode", out outString, IniFilePath);
            aiMode = outString;
            GetValue("FEATURES", "AISamplingRate", out outString, IniFilePath);
            aiSamplingRate = outString;
            GetValue("FEATURES", "LocalLineID", out outString, IniFilePath);
            localLineID = outString;
            GetValue("FEATURES", "ADMImageSort", out outString, IniFilePath);
            admImageSortString = outString;
            
            admImageSort.Clear();
            foreach (string strTemp in admImageSortString.Split(','))
            {
                admImageSort.Add(strTemp.Replace(" ", ""));
            }
        }

        //frmSystemSetting關閉後要再讀取一次ini設定
        private void frmSystemSetting_Closed(object sender, FormClosedEventArgs e)
        {
            ReadINI();
        }

        //frmRateSetting關閉後要先清除字典再讀一次rate table
        private void frmRateSetting_Closed(object sender, FormClosedEventArgs e)
        {
            admRate.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "ADMRateTable.txt"), Encoding.Default))
            {
                ReadADMRate(sr);
            }
                
        }

        //frmHeadSetting關閉後要再讀取一次ini設定
        private void frmHeadSetting_Closed(object sender, FormClosedEventArgs e)
        {
            //edcItem.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "EDCItem.txt"), Encoding.Default))
            {
                ReadEDCItem(sr);
            }
        }

        //frmPidSetting關閉後要先清除字典再讀一次rate table
        private void frmRecipe_Closed(object sender, FormClosedEventArgs e)
        {
            recipeDict.Clear();
            ReadRecipe();

        }
        //frmSpecSetting關閉後要先清除字典再讀一次rate table
        private void frmSpecSetting_Closed(object sender, FormClosedEventArgs e)
        {
            //specSetting.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "SPEC.txt"), Encoding.Default))
            {
                ReadSpec(sr);
            }

        }
        //frmSpecSettingABCD關閉後要先清除字典再讀一次rate table
        private void frmSpecSettingABCD_Closed(object sender, FormClosedEventArgs e)
        {
            specSettingABCD.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "SPEC_ABCD.txt"), Encoding.Default))
            {
                ReadSpecABCD(sr);
            }

        }
        //frmUserID關閉後要先清除字典再讀一次rate table
        private void frmUserID_Closed(object sender, FormClosedEventArgs e)
        {
            loginTable.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "PIUser.txt"), Encoding.Default))
            {
                ReadLoginTable(sr);
            }

        }
        //frmSpecSetting關閉後要先清除字典再讀一次rate table
        private void frmABCDNumSetting_Closed(object sender, FormClosedEventArgs e)
        {
            aBCDCount.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "ABCDCount.txt"), Encoding.Default))
            {
                ReadABCDCount(sr);
            }

        }
        private void 比例尺設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LevelCheck() == "ENG")
            {
                frmRateSetting.ReadTable();
                frmRateSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("權限不足，需要ENG以上權限", "權限警報");
                return;
            }
        }

        private void ReadEDCItem(StreamReader edcRateDataRead)
        {
            string edcRateDataReadData = "";
        
            while (true)
            {
                edcRateDataReadData = edcRateDataRead.ReadLine();
                if (edcRateDataReadData != "" && edcRateDataReadData != null)
                {
                    if (edcRateDataReadData.Substring(0, 1) == "@")
                    {
                        continue;
                    }
                }
                if (edcRateDataReadData == "" || edcRateDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] edcItemDtatList = edcRateDataReadData.Split(',');
                    //if (!edcItem.ContainsKey(edcItemDtatList[0]))
                    //    edcItem.Add(edcItemDtatList[0], edcItemDtatList[1]);
                }
            }
        }
        private void ReadPIDProd(StreamReader pidProdDataRead)
        {
            string pidProdDataReadData = "";
       
            while (true)
            {
                pidProdDataReadData = pidProdDataRead.ReadLine();
                if (pidProdDataReadData != "" && pidProdDataReadData != null)
                {
                    if (pidProdDataReadData.Substring(0, 1) == "@")
                    {
                        continue;
                    }
                }
                if (pidProdDataReadData == "" || pidProdDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] pidProdDtatList = pidProdDataReadData.Split(',');
                    //if (!pidProd.ContainsKey(pidProdDtatList[0]))
                    //    pidProd.Add(pidProdDtatList[0], pidProdDtatList[1]);
                }
            }
        }
        private void ReadAIModel(StreamReader aiModelRead)
        {
            string aiModelDataReadData = "";

            while (true)
            {
                aiModelDataReadData = aiModelRead.ReadLine();
                if (aiModelDataReadData != "" && aiModelDataReadData != null)
                {
                    if (aiModelDataReadData.Substring(0, 1) == "@")
                    {
                        continue;
                    }
                }
                if (aiModelDataReadData == "" || aiModelDataReadData == null)
                {
                    break;
                }
                else
                {
                    string[] aiModelDtatList = aiModelDataReadData.Split(',');
                    //if (!modelNumLCIN.ContainsKey(aiModelDtatList[0]))
                    //    modelNumLCIN.Add(aiModelDtatList[0], new ModelNum { Corner = aiModelDtatList[1], Normal = aiModelDtatList[2], Overlap = aiModelDtatList[3] });
                }
            }
        }
        ////生成上報SPC用的XML檔案
        private void GenerateXMLFile(List<List<string>> measureData, DataSourceType dataSource)
        {
            if (mode == "0")    //PIIN
            {
                //初始化各節點名稱
                List<string> element = new List<string> { "glass_id", "group_id", "lot_id", "product_id", "pfcd", "eqp_id", "sub_eqp_id", "ec_code", "route_no", "route_version"
                , "owner", "recipe_id", "operation", "ope_id", "ope_no", "proc_id", "rtc_flag", "pnp", "chamber", "cassette_id", "line_batch_id", "split_id", "cldate", "cltime", "mes_link_key", "rework_count", "operator"
                , "reserve_field_1", "reserve_field_2", "item_name", "item_type", "item_value"};

            }
            else if (mode == "1")   //LCIN
            {

                string glassID = "";
                string prod = "";
                string pid = "";
                string lineID = "";

                if (dataSource == DataSourceType.TA)
                {
                    glassID = glassIDNow;
                    prod = prodNow;
                    pid = pidNow;
                    lineID = lineIDNow;
                }
                else if (dataSource == DataSourceType.AI)
                {
                    glassID = glassIDNowAI;
                    prod = prodNowAI;
                    pid = pidNowAI;
                    lineID = lineIDNowAI;
                }

                //迴圈分析輸入資料

                //解析此產品用到多少Head

                //先拆解PFCD
                if (measureData[0][1].Length > 4)
                {
                    prod = measureData[0][1].Substring(2, 4);
                }
                else
                {
                    prod = measureData[0][1];
                }
                int headCount = 0;
                List<string> headList = recipeDict[pid + lineID].headList;
                headCount = headList.Count();



                //inputEdcData : {[Item_Name1, Item_Type1, Item_Value1], [Item_Name2, Item_Type2, Item_Value2]........}

                //初始化各節點名稱
                List<string> element = new List<string> { "glass_id", "group_id", "lot_id", "product_id", "pfcd", "eqp_id", "sub_eqp_id", "ec_code", "route_no", "route_version"
                , "owner", "recipe_id", "operation", "ope_id", "ope_no", "proc_id", "rtc_flag", "pnp", "chamber", "cassette_id", "line_batch_id", "split_id", "cldate", "cltime", "mes_link_key", "rework_count", "operator"
                , "reserve_field_1", "reserve_field_2", "item_name", "item_type", "item_value"};

                //收集xml資料
                List<string> listTemp = CollentXMLData(dataSource);

                //初始化一個xml實例
                XmlDocument myXmlDoc = new XmlDocument();
                //創建xml版本宣告
                XmlNode node = myXmlDoc.CreateXmlDeclaration("1.0", null, null);
                myXmlDoc.AppendChild(node);
                //創建xml根節點
                XmlElement rootElement = myXmlDoc.CreateElement("EDC");
                //將根節點加入到xml文件中（AppendChild）
                myXmlDoc.AppendChild(rootElement);
                for (int i = 0; i < 29; i++)
                {
                    //初始化第一層的子節點
                    XmlElement secondLevelElement1 = myXmlDoc.CreateElement(element[i]);
                    //寫入第一層子節點的值（SetAttribute）
                    if (i == 22)    //上報日期改寫現在日期
                    {
                        secondLevelElement1.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    else if (i == 23)   //上報時間改寫現在時間
                    {
                        secondLevelElement1.InnerText = DateTime.Now.ToString("HH:mm:ss");
                    }
                    else
                    {
                        secondLevelElement1.InnerText = listTemp[i];
                    }

                    //將第一層子節點加入到根節點中
                    rootElement.AppendChild(secondLevelElement1);
                }
                //創建第一層第29個子節點
                XmlElement firstLevelElement129 = myXmlDoc.CreateElement("datas");
                rootElement.AppendChild(firstLevelElement129);
                //創建第二層第1個子節點並依附在1-29下
                //firstLevelElement129.AppendChild(firstLevelElement21);
                XmlElement firstLevelElement21 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement22 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement23 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement24 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement25 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement26 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement27 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement28 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement29 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement20 = myXmlDoc.CreateElement("iary");

                XmlElement firstLevelElement31 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement32 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement33 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement34 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement35 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement36 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement37 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement38 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement39 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement30 = myXmlDoc.CreateElement("iary");

                XmlElement firstLevelElement41 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement42 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement43 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement44 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement45 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement46 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement47 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement48 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement49 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement40 = myXmlDoc.CreateElement("iary");

                XmlElement firstLevelElement70 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement71 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement72 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement73 = myXmlDoc.CreateElement("iary");
                XmlElement firstLevelElement74 = myXmlDoc.CreateElement("iary");
                //開始填入EDC ITEM內容
                string[] typeList = new string[] { "Normal", "Overlap", "Corner" };
                //順序Normal, Overlap, Corner
                foreach (string strTemp in typeList)
                {
                    if (strTemp == "Normal")
                    {
                        for (int x = 0; x < headCount; x++)
                        {
                            if (x == 0)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                //secondLevelElement31.InnerText = "Panel" + headList[x] + "_" + xmlData[1][0];   //寫入Overlao Item_Name
                                //secondLevelElement31.InnerText = "Panel" + headList[x] + "_" + xmlData[2][0];   //寫入Corner Item_Name
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement21.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement21.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement21.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement21);
                            }
                            else if (x == 1)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement22.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement22.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement22.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement22);
                            }
                            else if (x == 2)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement23.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement23.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement23.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement23);
                            }
                            else if (x == 3)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement24.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement24.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement24.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement24);
                            }
                            else if (x == 4)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement25.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement25.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement25.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement25);
                            }
                            else if (x == 5)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement26.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement26.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement26.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement26);
                            }
                            else if (x == 6)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement27.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement27.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement27.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement27);
                            }
                            else if (x == 7)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement28.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement28.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement28.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement28);
                            }
                            else if (x == 8)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement29.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement29.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement29.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement29);
                            }
                            else if (x == 9)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "seal" + headList[x] + "_" + "LCIN";   //寫入Normal Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement20.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement20.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[1][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement20.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement20);
                            }
                        }
                        ////開始填寫量測時間的測項

                        ////初始化第三層的子節點(EDC ITEM : Item_Name)
                        //XmlElement secondLevelElement34 = myXmlDoc.CreateElement(element[30]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute
                        //secondLevelElement34.InnerText = "MeasureDate";   //寫入Normal Item_Name

                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement49.AppendChild(secondLevelElement34);
                        ////初始化第三層的子節點(EDC ITEM : Item_Type)
                        //XmlElement secondLevelElement35 = myXmlDoc.CreateElement(element[31]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement35.InnerText = "EDC";
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement49.AppendChild(secondLevelElement35);
                        ////初始化第三層的子節點(EDC ITEM : Item_Vaule)
                        //XmlElement secondLevelElement36 = myXmlDoc.CreateElement(element[32]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement36.InnerText = xmlData[0][23] + xmlData[0][24];
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement49.AppendChild(secondLevelElement36);
                        ////將第二層結點依附到第一層結點
                        //firstLevelElement129.AppendChild(firstLevelElement49);

                        ////開始填寫子機的測項

                        ////初始化第三層的子節點(EDC ITEM : Item_Name)
                        //XmlElement secondLevelElement37 = myXmlDoc.CreateElement(element[30]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute
                        //secondLevelElement37.InnerText = "Seal Unit";   //寫入Normal Item_Name

                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement50.AppendChild(secondLevelElement37);
                        ////初始化第三層的子節點(EDC ITEM : Item_Type)
                        //XmlElement secondLevelElement38 = myXmlDoc.CreateElement(element[31]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement38.InnerText = "EDC";
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement50.AppendChild(secondLevelElement38);
                        ////初始化第三層的子節點(EDC ITEM : Item_Vaule)
                        //XmlElement secondLevelElement39 = myXmlDoc.CreateElement(element[32]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement39.InnerText = xmlData[0][6].Substring(5, 1);
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement50.AppendChild(secondLevelElement39);
                        ////將第二層結點依附到第一層結點
                        //firstLevelElement129.AppendChild(firstLevelElement50);
                    }
                    else if (strTemp == "Overlap")
                    {
                        for (int x = 0; x < headCount; x++)
                        {
                            if (x == 0)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name
                                                                                                       //將第三層子節點加入第二層節點中
                                firstLevelElement31.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement31.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement31.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement31);

                            }
                            else if (x == 1)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement32.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement32.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement32.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement32);
                            }
                            else if (x == 2)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement33.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement33.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement33.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement33);
                            }
                            else if (x == 3)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement34.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement34.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement34.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement34);
                            }
                            else if (x == 4)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement35.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement35.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement35.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement35);
                            }
                            else if (x == 5)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement36.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement36.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement36.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement36);
                            }
                            else if (x == 6)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement37.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement37.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement37.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement37);
                            }
                            else if (x == 7)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement38.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement38.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement38.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement38);
                            }
                            else if (x == 8)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement39.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement39.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement39.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement39);
                            }
                            else if (x == 9)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_overlap";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement30.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement30.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[2][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement30.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement30);
                            }
                        }
                        ////開始填寫量測時間的測項

                        ////初始化第三層的子節點(EDC ITEM : Item_Name)
                        //XmlElement secondLevelElement34 = myXmlDoc.CreateElement(element[29]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute
                        //secondLevelElement34.InnerText = "MeasureDate";   //寫入Normal Item_Name

                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement51.AppendChild(secondLevelElement34);
                        ////初始化第三層的子節點(EDC ITEM : Item_Type)
                        //XmlElement secondLevelElement35 = myXmlDoc.CreateElement(element[30]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement35.InnerText = "EDC";
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement51.AppendChild(secondLevelElement35);
                        ////初始化第三層的子節點(EDC ITEM : Item_Vaule)
                        //XmlElement secondLevelElement36 = myXmlDoc.CreateElement(element[31]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement36.InnerText = xmlData[1][23] + xmlData[1][24];
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement51.AppendChild(secondLevelElement36);
                        ////將第二層結點依附到第一層結點
                        //firstLevelElement129.AppendChild(firstLevelElement51);

                        ////開始填寫子機的測項

                        ////初始化第三層的子節點(EDC ITEM : Item_Name)
                        //XmlElement secondLevelElement37 = myXmlDoc.CreateElement(element[29]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute
                        //secondLevelElement37.InnerText = "Seal Unit";   //寫入Normal Item_Name

                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement52.AppendChild(secondLevelElement37);
                        ////初始化第三層的子節點(EDC ITEM : Item_Type)
                        //XmlElement secondLevelElement38 = myXmlDoc.CreateElement(element[30]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement38.InnerText = "EDC";
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement52.AppendChild(secondLevelElement38);
                        ////初始化第三層的子節點(EDC ITEM : Item_Vaule)
                        //XmlElement secondLevelElement39 = myXmlDoc.CreateElement(element[31]);
                        ////寫入第三層子節點的值(EDC Data)（SetAttribute）
                        //secondLevelElement39.InnerText = xmlData[1][6].Substring(5, 1);
                        ////將第三層子節點加入第二層節點中
                        //firstLevelElement52.AppendChild(secondLevelElement39);
                        ////將第二層結點依附到第一層結點
                        //firstLevelElement129.AppendChild(firstLevelElement52);
                    }
                    else if (strTemp == "Corner")
                    {
                        for (int x = 0; x < headCount; x++)
                        {
                            if (x == 0)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name
                                                                                                      //將第三層子節點加入第二層節點中
                                firstLevelElement41.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement41.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement41.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement41);

                            }
                            else if (x == 1)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement42.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement42.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement42.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement42);
                            }
                            else if (x == 2)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement43.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement43.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement43.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement43);
                            }
                            else if (x == 3)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement44.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement44.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement44.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement44);
                            }
                            else if (x == 4)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement45.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement45.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement45.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement45);
                            }
                            else if (x == 5)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement46.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement46.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement46.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement46);
                            }
                            else if (x == 6)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement47.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement47.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement47.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement47);
                            }
                            else if (x == 7)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement48.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement48.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement48.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement48);
                            }
                            else if (x == 8)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement49.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement49.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement49.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement49);
                            }
                            else if (x == 9)
                            {
                                //初始化第三層的子節點(EDC ITEM : Item_Name)
                                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute
                                secondLevelElement31.InnerText = "Panel" + headList[x] + "_corner";   //寫入Overlao Item_Name

                                //將第三層子節點加入第二層節點中
                                firstLevelElement40.AppendChild(secondLevelElement31);
                                //初始化第三層的子節點(EDC ITEM : Item_Type)
                                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement32.InnerText = "EDC";
                                //將第三層子節點加入第二層節點中
                                firstLevelElement40.AppendChild(secondLevelElement32);
                                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                                secondLevelElement33.InnerText = measureData[3][x];
                                //將第三層子節點加入第二層節點中
                                firstLevelElement40.AppendChild(secondLevelElement33);
                                //將第二層結點依附到第一層結點
                                firstLevelElement129.AppendChild(firstLevelElement40);
                            }
                        }

                    }

                }

                //開始填寫量測時間的測項

                //初始化第三層的子節點(EDC ITEM : Item_Name)
                XmlElement secondLevelElement34 = myXmlDoc.CreateElement(element[29]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute
                secondLevelElement34.InnerText = "MeasureDate";   //寫入Normal Item_Name

                //將第三層子節點加入第二層節點中
                firstLevelElement70.AppendChild(secondLevelElement34);
                //初始化第三層的子節點(EDC ITEM : Item_Type)
                XmlElement secondLevelElement35 = myXmlDoc.CreateElement(element[30]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement35.InnerText = "EDC";
                //將第三層子節點加入第二層節點中
                firstLevelElement70.AppendChild(secondLevelElement35);
                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                XmlElement secondLevelElement36 = myXmlDoc.CreateElement(element[31]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement36.InnerText = listTemp[22] + listTemp[23];
                //將第三層子節點加入第二層節點中
                firstLevelElement70.AppendChild(secondLevelElement36);
                //將第二層結點依附到第一層結點
                firstLevelElement129.AppendChild(firstLevelElement70);

                //開始填寫子機的測項

                //初始化第三層的子節點(EDC ITEM : Item_Name)
                XmlElement secondLevelElement37 = myXmlDoc.CreateElement(element[29]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute
                secondLevelElement37.InnerText = "Seal Unit";   //寫入Normal Item_Name

                //將第三層子節點加入第二層節點中
                firstLevelElement71.AppendChild(secondLevelElement37);
                //初始化第三層的子節點(EDC ITEM : Item_Type)
                XmlElement secondLevelElement38 = myXmlDoc.CreateElement(element[30]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement38.InnerText = "EDC";
                //將第三層子節點加入第二層節點中
                firstLevelElement71.AppendChild(secondLevelElement38);
                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                XmlElement secondLevelElement39 = myXmlDoc.CreateElement(element[31]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement39.InnerText = sealUnitDict[glassID];
                //將第三層子節點加入第二層節點中
                firstLevelElement71.AppendChild(secondLevelElement39);
                //將第二層結點依附到第一層結點
                firstLevelElement129.AppendChild(firstLevelElement71);

                //組合xml檔案名稱
                string xmlFileName = listTemp[22] + listTemp[23] + "_" + listTemp[5] + "_" + listTemp[0] + ".xml";
                //将xml文件保存到指定的路径下
                if (!Directory.Exists(Path.Combine(xmlSavePath, listTemp[6], listTemp[5])))
                {
                    try
                    {
                        Directory.CreateDirectory(Path.Combine(xmlSavePath, listTemp[6], listTemp[5]));
                    }
                    catch (Exception ex)
                    {
                        CallWarningForm("創建xml儲存路徑時發生錯誤，Message : " + ex.ToString(), "創建xml儲存路徑時發生錯誤");
                        return;
                    }
                }

                string xmlFilePathLocal = Path.Combine(xmlSavePath, listTemp[6], listTemp[5], xmlFileName);
                //string xmlFilePathLocal = "D" + xmlFilePath.Substring(1);
                myXmlDoc.Save(xmlFilePathLocal);
                if (dataSource == DataSourceType.TA)
                    MessageBox.Show("GlassID : " + glassID + " SPC上報完成");
            }


        }   //生成XML檔案
        private List<List<string>> DataGridViewToArray(DataSourceType dataSource)
        {
            List<List<string>> rtn = new List<List<string>> { };
            List<string> glassData = new List<string> { };
            List<string> dataNormal = new List<string> { };
            List<string> dataOverlap = new List<string> { };
            List<string> dataCorner = new List<string> { };
            if (mode == "0")
            {

            }
            else if (mode == "1")
            {
                DataGridView dataGridView = null;
                string glassID;
                string prod;
                if (dataSource == DataSourceType.TA)
                {
                    dataGridView = frmPanelTA.dataGridViewLCIN1;
                }
                else if (dataSource == DataSourceType.AI)
                {
                    dataGridView = frmPanelAI.dataGridViewLCIN1;
                }
                for (int x = 0; x < dataGridView.RowCount; x++)
                {
                    
                    if (dataGridView.Rows[x].Cells[1].Value.ToString() == "Corner")    //Corner要比較資料1和資料2取大的寫入
                    {
                        if (Convert.ToDouble(dataGridView.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(dataGridView.Rows[x].Cells[3].Value.ToString()))
                        {
                            dataCorner.Add(dataGridView.Rows[x].Cells[2].Value.ToString());
                        }
                        else
                        {
                            dataCorner.Add(dataGridView.Rows[x].Cells[3].Value.ToString());
                        }
                    }
                    else if (dataGridView.Rows[x].Cells[1].Value.ToString() == "Normal Seal")
                    {
                        dataNormal.Add(dataGridView.Rows[x].Cells[2].Value.ToString());
                    }
                    else if (dataGridView.Rows[x].Cells[1].Value.ToString() == "Overlap")
                    {
                        dataOverlap.Add(dataGridView.Rows[x].Cells[2].Value.ToString());
                    }
                }
                glassData.Add(glassIDNow);    //寫入ID
                glassData.Add("L4" + prodNow + "KK0111");  //寫入PFCD

                //將所有資料寫入回傳的array中
                rtn.Add(glassData);   //寫入ID與PFCD
                rtn.Add(dataNormal);
                rtn.Add(dataOverlap);
                rtn.Add(dataCorner);
            }
            return rtn;
        }
        private List<string> CollentXMLData(DataSourceType dataSource)   //收集XML DATA
        {
            //宣告變數
            string lineID = "";
            string glassID = "";
            string prod = "";
            string pfcd = "";
            string productId = "";
            string eqpId = "";
            string subEqpId = "";
            string owner = "";
            string recipeId = "";
            string operation = "";
            string pnp = "";
            string cldate = "";
            string cltime = "";
            string reworkCount = "";
            string operatorId = "";
            string cassetteId = "";
            //string unloadTime = "";
            //string sealUnit = "";
            //string itemValue1 = "";
            //string itemValue2 = "";
            //string itemValue3 = "";
            //string itemValue4 = "";
            //string itemValue5 = "";
            //string itemValue6 = "";
            //string itemValue7 = "";
            //string itemValue8 = "";
            //以下為無法取得的值皆帶空白
            string groupId = "";
            string lotId = "";
            string ecCode = "";
            string routeNo = "";
            string routeVersion = "";
            string opeId = "";
            string opeNo = "";
            string procId = "";
            string rtcFlag = "";
            string chamber = "";
            string lineBatchId = "";
            string splitId = "";
            string mesLinkKey = "";
            string reserveField_1 = "";
            string reserveField_2 = "";

            //逐項收集資料
            if (dataSource == DataSourceType.TA)
            {
                lineID = lineIDNow;
                glassID = glassIDNow;
                prod = prodNow;

            }
            else if (dataSource == DataSourceType.AI)
            {
                lineID = lineIDNowAI;
                glassID = glassIDNowAI;
                prod = prodNowAI;
                
            }

            pfcd = "L4" + prod + "KK0111";
            productId = "L4" + prod + "KK0111";
            //eqpId = lineIDNow + (Convert.ToInt16(unitIDNow) + 50).ToString();
            eqpId = "TPRB" + lineID.Substring(4, 2) + (Convert.ToInt16(sealUnitDict[glassID]) + 50).ToString();
            subEqpId = "TPRB" + lineID.Substring(4, 2) + "00";
            owner = "PROD";
            recipeId = recipeIDNow;
            operation = operationNum;
            pnp = "P";
            cldate = DateTime.Now.ToString("yyyyMMdd");
            cltime = DateTime.Now.ToString("HHmmss");
            reworkCount = "00";
            operatorId = labelUserID.Text;
            //組合資料回傳

            List<string> rtnFormatData = new List<string> { glassID , groupId, lotId, productId, pfcd, eqpId, subEqpId, ecCode, routeNo, routeVersion, owner, recipeId, operation, opeId, opeNo, procId, rtcFlag, pnp, chamber, cassetteId, lineBatchId, splitId, cldate, cltime, mesLinkKey
                            , reworkCount, operatorId, reserveField_1, reserveField_2};
            return rtnFormatData;
        }
        //建立鍵盤keyPress事件，空白鍵點擊計算鈕
        private void Form1_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                ControlLossFoucs();
                if (mode == "0")
                {
                    frmABCD.CalculateData();
                }
                else if (mode == "1")
                {
                    //觸發計算
                    frmLCIN.CalculateData();
                    //計算完畢切換下一張圖片
                    frmLCIN.ChangeImage("D");
                }
                
            }
        }

        private void head編號設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LevelCheck() == "ENG" || LevelCheck() == "PM")
            {
                frmHeadSetting.ReadTable();
                frmHeadSetting.ShowDialog();
            }
            else
            {
                MessageBox.Show("權限不足，需要PM以上權限", "權限警報");
                return;
            }
        }
        //刪除資料夾
        public void DeleteSrcFolder(string file)
        {
            //去除資料夾和子檔案的只讀屬性
            //去除資料夾的只讀屬性
            System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            //去除檔案的只讀屬性
            System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
            //判斷資料夾是否還存在
            if (Directory.Exists(file))
            {
                foreach (string f in Directory.GetFileSystemEntries(file))
                {
                    if (File.Exists(f))
                    {
                        //如果有子檔案刪除檔案
                        File.Delete(f);
                    }
                    else
                    {
                        //迴圈遞迴刪除子資料夾 
                        DeleteSrcFolder1(f);
                    }
                }
                //刪除空資料夾
                Directory.Delete(file);
            }
        }
        public void DeleteSrcFolder1(string file)
        {
            //去除資料夾和子檔案的只讀屬性
            //去除資料夾的只讀屬性
            System.IO.DirectoryInfo fileInfo = new DirectoryInfo(file);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            //去除檔案的只讀屬性
            System.IO.File.SetAttributes(file, System.IO.FileAttributes.Normal);
            //判斷資料夾是否還存在
            if (Directory.Exists(file))
            {
                foreach (string f in Directory.GetFileSystemEntries(file))
                {
                    if (File.Exists(f))
                    {
                        //如果有子檔案刪除檔案
                        File.Delete(f);
                    }
                    else
                    {
                        //迴圈遞迴刪除子資料夾 
                        DeleteSrcFolder1(f);
                    }
                }
                //刪除空資料夾
                Directory.Delete(file);
            }
        }

        private void WriteToMeasureLog(string glassID, string lineId, string userID, string userName)
        {
            dtNow = DateTime.Now.ToString("yyyMMdd");
            string logFileDir = @"History\" + dtNow + @"\" + dtNow + ".txt";
            try
            {
                
                if (File.Exists(@"History\" + dtNow) == false)
                {
                    Directory.CreateDirectory(@"History\" + dtNow);
                }
                if (File.Exists(logFileDir) == false)
                {
                    File.Create(logFileDir).Close();
                }

                FileStream logFs = new FileStream(logFileDir, FileMode.Append);
                StreamWriter logWriter = new StreamWriter(logFs);
                string dtNow2 = DateTime.Now.ToString("yyyyMMddHHmmss");
                
                logWriter.WriteLine(dtNow2 + "@" + lineId + "@" + " @" + glassID.Substring(0, 12) + "@" + userID + "@" + userName);
                logWriter.Close();
                logFs.Close();
            }
            catch
            {

            }

        }

        private void 規格設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LevelCheck() == "ENG")
            {
                if (mode == "0")
                {
                    frmSpecSettingABCD.ReadTable();
                    frmSpecSettingABCD.ShowDialog();
                }
                else if (mode == "1")
                {
                    frmSpecSetting.ReadTable();
                    frmSpecSetting.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("權限不足，需要ENG以上權限", "權限警報");
                return;
            }
        }

        private void 使用者帳戶設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LevelCheck() == "ENG")
            {
                frmUserID.ReadTable();
                frmUserID.ShowDialog();

            }
            else
            {
                MessageBox.Show("權限不足");
                return;
            }

        }

        private void pID設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPidSetting.ReadTable();
            frmPidSetting.ShowDialog();
        }

        private void 基板歷史紀錄ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未開放");
        }
        private void CallAlarmForm(string command)
        {
            
            //先關閉舊的
            try
            {
              
                IntPtr player = FindWindow(null, "frmAlarmForm");
                if (player != IntPtr.Zero)
                {
                    /* 傳送關閉的指令 */
                    SendMessage(player, SC_CLOSE, 0, 0);
                }
            }
            catch
            {

            }
            frmAlarmForm frmAlarmForm = new frmAlarmForm();
            frmAlarmForm.Text = "frmAlarmForm";
            frmAlarmForm.SetMessage(command);
            frmAlarmForm.Show();
        }

        private void CallMessageForm(string command)
        {

            ////先關閉舊的
            //try
            //{

            //    IntPtr player = FindWindow(null, "frmAlarmForm");
            //    if (player != IntPtr.Zero)
            //    {
            //        /* 傳送關閉的指令 */
            //        SendMessage(player, SC_CLOSE, 0, 0);
            //    }
            //}
            //catch
            //{

            //}
            frmMessageForm frmMessageForm = new frmMessageForm();
            frmMessageForm.Text = "frmMessageForm";
            frmMessageForm.SetMessage(command);
            frmMessageForm.Show();
        }

        //檢查規格並標記顏色
        private string CheckUploadDataSpec(DataSourceType dataSource)
        {
            string rtn = "0";
            if (dataSource == DataSourceType.TA)
            {
                if (mode == "0")
                {
                    for (int x = 0; x < frmPanelTA.dataGridViewPIIN1.RowCount; x++)
                    {
                        string specOOSMax = "";
                        string specOOSMin = "";
                        string specOOCMax = "";
                        string specOOCMin = "";

                        switch (x)
                        {
                            case 0:
                                specOOSMax = specSettingABCD[prodNow].aOOSMax;
                                specOOSMin = specSettingABCD[prodNow].aOOSMin;
                                specOOCMax = specSettingABCD[prodNow].aOOCMax;
                                specOOCMin = specSettingABCD[prodNow].aOOCMin;
                                break;
                            case 1:
                                specOOSMax = specSettingABCD[prodNow].bOOSMax;
                                specOOSMin = specSettingABCD[prodNow].bOOSMin;
                                specOOCMax = specSettingABCD[prodNow].bOOCMax;
                                specOOCMin = specSettingABCD[prodNow].bOOCMin;
                                break;
                            case 2:
                                specOOSMax = specSettingABCD[prodNow].cOOSMax;
                                specOOSMin = specSettingABCD[prodNow].cOOSMin;
                                specOOCMax = specSettingABCD[prodNow].cOOCMax;
                                specOOCMin = specSettingABCD[prodNow].cOOCMin;
                                break;
                            case 3:
                                specOOSMax = specSettingABCD[prodNow].dOOSMax;
                                specOOSMin = specSettingABCD[prodNow].dOOSMin;
                                specOOCMax = specSettingABCD[prodNow].dOOCMax;
                                specOOCMin = specSettingABCD[prodNow].dOOCMin;
                                break;
                        }
                        //檢查OOS / OOC
                        //左上
                        if (frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Value.ToString() != "")
                        {

                            if (Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Value.ToString()) > Convert.ToDouble(specOOSMax) || Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Value.ToString()) < Convert.ToDouble(specOOSMin))
                            {
                                frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Style.BackColor = Color.Red;
                                rtn = "2";
                            }
                            else if (Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Value.ToString()) > Convert.ToDouble(specOOCMax) || Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Value.ToString()) < Convert.ToDouble(specOOCMin))
                            {
                                frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff1"].Style.BackColor = Color.Green;
                                rtn = "1";
                            }
                        }
                        //右下
                        if (frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Value != null && frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Value.ToString() != "")
                        {

                            if (Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Value.ToString()) > Convert.ToDouble(specOOSMax) || Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Value.ToString()) < Convert.ToDouble(specOOSMin))
                            {
                                frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Style.BackColor = Color.Red;
                                rtn = "2";
                            }
                            else if (Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Value.ToString()) > Convert.ToDouble(specOOCMax) || Convert.ToDouble(frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Value.ToString()) < Convert.ToDouble(specOOCMin))
                            {
                                frmPanelTA.dataGridViewPIIN1.Rows[x].Cells["diff2"].Style.BackColor = Color.Green;
                                rtn = "1";
                            }
                        }
                    }
                }
                else if (mode == "1")
                {

                    //N_OOS_MAX , N_OOS_MIN, N_OOC_MAX, N_OOC_MIN, O_OOS_MAX, O_OOS_MIN, O_OOC_MAX, O_OOC_MIN, C_OOS_MAX, C_OOS_MIN, C_OOC_MAX, C_OOC_MIN
                    RecpieLCIN recpieData = recipeDict[pidNow + lineIDNow];
                    for (int x = 0; x < frmPanelTA.dataGridViewLCIN1.RowCount; x++)
                    {
                        if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Normal Seal")
                        {
                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value != null && frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查OOS
                                if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.normalOOSMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.normalOOSMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查OOC
                                else if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.normalOOCMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.normalOOCMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.White;
                                }
                            }

                        }
                        else if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Overlap")
                        {
                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value != null && frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查OOS
                                if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.overlapOOSMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.overlapOOSMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查OOC
                                else if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.overlapOOCMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.overlapOOCMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.White;
                                }
                            }

                        }
                        else if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Corner")
                        {
                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value != null && frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查資料1OOS
                                if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOSMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOSMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查資料1OOC
                                else if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOCMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.overlapOOCMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.White;
                                }
                            }

                            if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value != null && frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查資料2OOS
                                if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOSMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOSMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查資料2OOC
                                else if (Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOCMax) || Convert.ToDouble(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOCMin))
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Style.BackColor = Color.White;
                                }
                            }

                        }
                    }
                }
            }
            else
            {
                if (mode == "0")
                {

                }
                else
                {
                    //檢查AI
                    //N_OOS_MAX , N_OOS_MIN, N_OOC_MAX, N_OOC_MIN, O_OOS_MAX, O_OOS_MIN, O_OOC_MAX, O_OOC_MIN, C_OOS_MAX, C_OOS_MIN, C_OOC_MAX, C_OOC_MIN
                    RecpieLCIN recpieData = recipeDict[pidNowAI + lineIDNowAI];
                    for (int x = 0; x < frmPanelAI.dataGridViewLCIN1.RowCount; x++)
                    {
                        if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Normal Seal")
                        {
                            if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value != null && frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查OOS
                                if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.normalOOSMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.normalOOSMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查OOC
                                else if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.normalOOCMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.normalOOCMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.White;
                                }
                            }

                        }
                        else if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Overlap")
                        {
                            if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value != null && frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查OOS
                                if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.overlapOOSMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.overlapOOSMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查OOC
                                else if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.overlapOOCMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.overlapOOCMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.White;
                                }
                            }

                        }
                        else if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Corner")
                        {
                            if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value != null && frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查資料1OOS
                                if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOSMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOSMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查資料1OOC
                                else if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOCMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOCMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[2].Style.BackColor = Color.White;
                                }
                            }

                            if (frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value != null && frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString().Replace(" ", "") != "")
                            {
                                //檢查資料2OOS
                                if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOSMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOSMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Style.BackColor = Color.Red;
                                    rtn = "2";
                                }
                                //檢查資料2OOC
                                else if (Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) > Convert.ToDouble(recpieData.cornerOOCMax) || Convert.ToDouble(frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Value.ToString()) < Convert.ToDouble(recpieData.cornerOOCMin))
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Style.BackColor = Color.Yellow;
                                    rtn = "1";
                                }
                                else
                                {
                                    frmPanelAI.dataGridViewLCIN1.Rows[x].Cells[3].Style.BackColor = Color.White;
                                }
                            }

                        }
                    }
                }
                
                
            }
            

            return rtn;
        }

        private string LevelCheck()
        {
            string rtn = "";
            if (loginTable.ContainsKey(labelUserID.Text))
            {
                rtn = loginTable[labelUserID.Text][2];
            }
            return rtn;
        }

        private void 比例尺校正ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("未開放");
            return;
        }

        private void aBCD量測數量設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmABCDNumSetting.ReadTable();
            frmABCDNumSetting.ShowDialog();
        }

        private void mainForm_Closing(object sender, FormClosingEventArgs e)
        {
            //若正在量測中關閉視窗，則刪除暫存檔
            if (checking)
            {
                DeleteTemp();
            }
        }

        private void DeleteTemp()
        {
            //刪除量測中識別用旗標暫存檔
            if (File.Exists(Path.Combine(glassDirNow, "checking.tmp")))
            {
                try
                {
                    File.Delete(Path.Combine(glassDirNow, "checking.tmp"));
                }
                catch
                {
                    WriteToLog("刪除暫存檔失敗 : " + Path.Combine(glassDirNow, "checking.tmp", "checking.tmp"), "system");
                    MessageBox.Show("刪除暫存檔失敗，請手動刪除 : " + Path.Combine(glassDirNow, "checking.tmp", "checking.tmp"));
                }
            }
        }

        //檢查AI是否已有結果產生
        private void WaitForAIOutput()
        {
            while(true)
            {
                
                foreach(KeyValuePair<string, WaitMeasureID> item in waitMeasureDict.ToArray())
                {
                    if (item.Value != null && item.Value.aiDataExistFlag == false)
                    {
                        int count = 0;
                        bool cornerFlag = false;
                        bool normalFlag = false;
                        bool overlapFlag = false;

                        string aiFilePathCorner1 = Path.Combine(aiServer, "Output", item.Value.dateTime + "_" + "2299" + "_" + "XXXXXXX" + "_" + item.Value.glassID.Substring(0, 10) + "_RES.TXT");
                        string aiFilePathNormal1 = Path.Combine(aiServer, "Output", item.Value.dateTime + "_" + "2297" + "_" + "XXXXXXX" + "_" + item.Value.glassID.Substring(0, 10) + "_RES.TXT");
                        string aiFilePathOverlap1 = Path.Combine(aiServer, "Output", item.Value.dateTime + "_" + "2298" + "_" + "XXXXXXX" + "_" + item.Value.glassID.Substring(0, 10) + "_RES.TXT");
                        while (true)
                        {
                            
                            Thread.Sleep(1000);
                            count++;
                            cornerFlag = File.Exists(aiFilePathCorner1) ? true : false;
                            normalFlag = File.Exists(aiFilePathNormal1) ? true : false;
                            overlapFlag = File.Exists(aiFilePathOverlap1) ? true : false;
                            if (aiMode == "1")
                            {
                                if (buttonAIProcessStart.Text == "AI Process Stop")
                                {
                                    //if (Directory.Exists(Path.Combine(aiServer, "Output")))
                                    //{
                                    //    foreach (string file in Directory.GetFiles(Path.Combine(aiServer, "Output")))
                                    //    {
                                    //        if (Path.GetFileName(file).Length > 18)
                                    //        {
                                    //            switch (Path.GetFileName(file).Substring(15, 4))
                                    //            {
                                    //                case "2299":
                                    //                    cornerFlag = true;
                                    //                    waitMeasureDict[item.Value.glassID].dateTime = Path.GetFileName(file).Substring(0, 14);
                                    //                    break;
                                    //                case "2298":
                                    //                    normalFlag = true;
                                    //                    waitMeasureDict[item.Value.glassID].dateTime = Path.GetFileName(file).Substring(0, 14);
                                    //                    break;
                                    //                case "2297":
                                    //                    overlapFlag = true;
                                    //                    waitMeasureDict[item.Value.glassID].dateTime = Path.GetFileName(file).Substring(0, 14);
                                    //                    break;
                                    //            }
                                    //        }

                                    //    }
                                    //}


                                    if (cornerFlag == true)
                                        waitMeasureDict[item.Key].aiJudgedCornerFlag = true;
                                    if (normalFlag == true)
                                        waitMeasureDict[item.Key].aiJudgedNormalFlag = true;
                                    if (overlapFlag == true)
                                        waitMeasureDict[item.Key].aiJudgedOverlapFlag = true;

                                    if ((cornerFlag == true && normalFlag == true && overlapFlag == true))
                                    {
                                        WriteToAILog("AI Response takes  : " + _timerAI.ElapsedMilliseconds + " Milliseconds", "Debug", true);
                                        _timerAI.Reset();

                                        waitMeasureDict[item.Key].aiDataExistFlag = true;

                                        waitMeasureDict[item.Key].aiDataExistFlag = true;

                                        break;
                                    }
                                    else if (count >= int.Parse(waitAITime))
                                    {

                                        waitMeasureDict[item.Key].aiDataExistFlag = true;

                                        waitMeasureDict[item.Key].aiDataExistFlag = true;

                                        waitMeasureDict[item.Key].measureByAIorTA = "TA";   //時間到還未收到AI回復資料，量測權責轉給TA
                                        break;
                                    }
                                }
                                
                            }
                            else
                            {
                                waitMeasureDict[item.Key].aiDataExistFlag = true;
                                waitMeasureDict[item.Key].measureByAIorTA = "TA";   //非AI模式，量測權責轉給TA
                                break;
                            }
                        }
                    }
                    
                }
            }
            
        }

        private void aI模型編號設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAIModelSetting.ReadTable();
            frmAIModelSetting.ShowDialog();
        }
        private void frmAIModelSetting_Closed(object sender, FormClosedEventArgs e)
        {
            //modelNumLCIN.Clear();
            using (StreamReader sr = new StreamReader(Path.Combine(Application.StartupPath, "AIModel.txt"), Encoding.Default))
            {
                ReadAIModel(sr);
            }
        }

        private void buttonPanelAI_Click(object sender, EventArgs e)
        {
            if (buttonPanelChange.Text == "AI")
            {
                buttonCreateExcelData.Visible = false;
                buttonAIProcessStart.Visible = true;
                frmAI.MdiParent = this;
                frmAI.Parent = splitContainer1.Panel2;
                frmAI.WindowState = FormWindowState.Maximized;
                frmAI.Show();

                frmPanelAI.MdiParent = this;
                frmPanelAI.Parent = splitContainer2.Panel2;
                frmPanelAI.WindowState = FormWindowState.Maximized;
                frmPanelAI.Show();

                buttonJudgeOK.Visible = false;
                buttonJudgeCancel.Visible = false;
                buttonPanelChange.Text = "TA";
            }
            else if (buttonPanelChange.Text == "TA")
            {
                buttonCreateExcelData.Visible = true;
                buttonAIProcessStart.Visible = false;
                frmLCIN.MdiParent = this;
                frmLCIN.Parent = splitContainer1.Panel2;
                frmLCIN.WindowState = FormWindowState.Maximized;
                frmLCIN.Show();

                frmPanelTA.MdiParent = this;
                frmPanelTA.Parent = splitContainer2.Panel2;
                frmPanelTA.WindowState = FormWindowState.Maximized;
                frmPanelTA.Show();

                buttonJudgeOK.Visible = true;
                buttonJudgeCancel.Visible = true;
                buttonPanelChange.Text = "AI";
            }

        }

        private void buttonPanelTA_Click(object sender, EventArgs e)
        {
            frmLCIN.MdiParent = this;
            frmLCIN.Parent = splitContainer1.Panel2;
            frmLCIN.WindowState = FormWindowState.Maximized;
            frmLCIN.Show();

            frmPanelTA.MdiParent = this;
            frmPanelTA.Parent = splitContainer2.Panel2;
            frmPanelTA.WindowState = FormWindowState.Maximized;
            frmPanelTA.Show();

            buttonJudgeOK.Visible = true;
            buttonJudgeCancel.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void buttonLogin_Click_1(object sender, EventArgs e)
        {
            if (buttonLogin.Text == "登入")
            {
                frmLogin input = new frmLogin();
                DialogResult dr = input.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string[] rtnData = input.GetMsg();
                    if (loginTable.ContainsKey(rtnData[0]))
                    {
                        if (rtnData[1] == loginTable[rtnData[0]][1])
                        {
                            userID = rtnData[0];
                            LoginSuccess();
                            if (labelUserName.Text == "GOD")
                            {
                                checkBoxPause.Visible = true;
                            }

                        }
                        else
                        {
                            MessageBox.Show("密碼錯誤");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("無此工號");
                        return;
                    }

                }
            }
            else if (buttonLogin.Text == "登出")
            {
                DialogResult result = MessageBox.Show("確定要登出?", "登出", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                {
                    LogoutSuccess();
                    checkBoxPause.Visible = false;
                }

            }
        }
        private void CreateExcelData()
        {
            //檢查資料完整性
            if (frmPanelTA.dataGridViewLCIN1.RowCount > 0)
            {
                for (int x = 0; x < frmPanelTA.dataGridViewLCIN1.RowCount; x++)
                {
                    if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() != "Corner")
                    {
                        if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value == null)
                        {
                            MessageBox.Show(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "資料不完整，請先完成量測");
                            return;
                        }
                    }
                    else if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() == "Corner")
                    {
                        if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[2].Value == null)
                        {
                            MessageBox.Show(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "資料不完整，請先完成量測");
                            return;
                        }
                        if (frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[3].Value == null)
                        {
                            MessageBox.Show(frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[0].Value.ToString() + "，" + frmPanelTA.dataGridViewLCIN1.Rows[x].Cells[1].Value.ToString() + "資料不完整，請先完成量測");
                            return;
                        }
                    }
                }

                //開始將DATAGRIDVIEW轉成ARRAY
                measureDataSummary = DataGridViewToArray(DataSourceType.TA);

                if (measureDataSummary.Count == 4)
                {
                    frmExcelCopyData.UpdataDataGridView(measureDataSummary);
                    frmExcelCopyData.ShowDialog();
                }
            }
        }
        private void buttonJudgeOK_Click_1(object sender, EventArgs e)
        {
            FinishMeasure(DataSourceType.TA);
        }

        private void buttonJudgeCancel_Click_1(object sender, EventArgs e)
        {
            CancelMeasure(DataSourceType.TA);
        }

        private void buttonCreateExcelData_Click(object sender, EventArgs e)
        {
            CreateExcelData();
        }


        private void GenerateMeasureDataToXlsx(DataSourceType dataSource)
        {
            if (mode == "0")
            {

            }
            else if (mode == "1")
            {
                DataGridView dataGridView = null;
                string glassID = "";
                string prod = "";
                string lineID = "";
                string pid = "";
                if (dataSource == DataSourceType.TA)
                {
                    dataGridView = frmPanelTA.dataGridViewLCIN1;
                    glassID = glassIDNow;
                    prod = prodNow;
                    lineID = lineIDNow;
                    pid = pidNow;
                }
                else if (dataSource == DataSourceType.AI)
                {
                    dataGridView = frmPanelAI.dataGridViewLCIN1;
                    glassID = glassIDNowAI;
                    prod = prodNowAI;
                    lineID = lineIDNowAI;
                    pid = pidNowAI;
                }
                string xlsxPathLocal = Path.Combine(Application.StartupPath, "MeasureData", "LCIN" + lineIDNowAI.Substring(4, 2) + " 自動拍照機 - " + prodNowAI + " 線寬量測DATA.xlsx");
                string xlsxPath = Path.Combine(resultPath, "LCIN" + lineIDNowAI.Substring(4, 2) + " 自動拍照機 - " + prodNowAI + " 線寬量測DATA.xlsx");

                if (!Directory.Exists(Path.Combine(Application.StartupPath, "MeasureData")))
                {

                    Directory.CreateDirectory(Path.Combine(Application.StartupPath, "MeasureData"));
                }

                if (!Directory.Exists(resultPath))
                {
                    try
                    {
                        Directory.CreateDirectory(resultPath);
                    }
                    catch
                    {
                        CallWarningForm("未發現紀錄表資料夾 : " + resultPath + "，請確定路徑是否設定錯誤?", "未發現紀錄表資料夾");
                        return;
                    }
                }

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //設定文件標題
                saveFileDialog.Title = "匯出Excel文件";
                //設定文件型別
                saveFileDialog.Filter = "Excel 工作簿(*.xlsx)|*.xlsx";
                //設定預設文件型別顯示順序  
                saveFileDialog.FilterIndex = 1;
                //是否自動在文件名中新增副檔名
                saveFileDialog.AddExtension = true;
                //是否記憶上次開啟的目錄
                saveFileDialog.RestoreDirectory = true;
                //設定預設文件名
                saveFileDialog.FileName = "LCIN" + lineID.Substring(4, 2) + " 自動拍照機 - " + prod + " 線寬量測DATA.xlsx";
                FileStream fs;
                FileInfo fileInfo;
                ExcelPackage ep;

                if (File.Exists(xlsxPathLocal))
                {
                    //打開excel報表
                    fileInfo = new FileInfo(xlsxPathLocal);
                    ep = new ExcelPackage(fileInfo);
                }
                else
                {
                    CallWarningForm("未發現Excel紀錄表 : " + xlsxPathLocal, "未發現Excel紀錄表");
                    return;
                }
                string[] _type = new string[] { "Corner", "Normal", "Overlap" };
                ExcelWorksheet sheetCorner = ep.Workbook.Worksheets[_type[0]];//取得Sheet1
                ExcelWorksheet sheetNormal = ep.Workbook.Worksheets[_type[1]];//取得Sheet2
                ExcelWorksheet sheetOverlap = ep.Workbook.Worksheets[_type[2]];//取得Sheet3
                                                                               //填入量測資料
                                                                               //先取得行數
                int rowIndexCorner = sheetCorner.Dimension == null ? 0 : sheetCorner.Dimension.End.Row;   //取得結束列編號
                int rowIndexNormal = sheetNormal.Dimension == null ? 0 : sheetNormal.Dimension.End.Row;   //取得結束列編號
                int rowIndexOverlap = sheetOverlap.Dimension == null ? 0 : sheetOverlap.Dimension.End.Row;   //取得結束列編號

                //填寫common資料
                rowIndexCorner++;
                sheetCorner.Cells[rowIndexCorner, 1].Value = "AI_001";   //線別
                sheetCorner.Cells[rowIndexCorner, 2].Value = lineID.Substring(5, 1);   //線別
                sheetCorner.Cells[rowIndexCorner, 3].Value = prod;   //產品
                sheetCorner.Cells[rowIndexCorner, 4].Value = pid;   //PID
                sheetCorner.Cells[rowIndexCorner, 5].Value = DateTime.Now.ToString("MMdd");   //日期
                sheetCorner.Cells[rowIndexCorner, 6].Value = DateTime.Now.ToString("mm:ss");   //時間
                sheetCorner.Cells[rowIndexCorner, 7].Value = glassID;   //ID
                sheetCorner.Cells[rowIndexCorner, 8].Value = sealUnitDict[glassID];   //子機

                rowIndexNormal++;
                sheetNormal.Cells[rowIndexNormal, 1].Value = "AI_001";   //線別
                sheetNormal.Cells[rowIndexNormal, 2].Value = lineID.Substring(5, 1);   //線別
                sheetNormal.Cells[rowIndexNormal, 3].Value = prod;   //產品
                sheetNormal.Cells[rowIndexNormal, 4].Value = pid;   //PID
                sheetNormal.Cells[rowIndexNormal, 5].Value = DateTime.Now.ToString("MMdd");   //日期
                sheetNormal.Cells[rowIndexNormal, 6].Value = DateTime.Now.ToString("mm:ss");   //時間
                sheetNormal.Cells[rowIndexNormal, 7].Value = glassID;   //ID
                sheetNormal.Cells[rowIndexNormal, 8].Value = sealUnitDict[glassID];   //子機

                rowIndexOverlap++;
                sheetOverlap.Cells[rowIndexOverlap, 1].Value = "AI_001";   //線別
                sheetOverlap.Cells[rowIndexOverlap, 2].Value = lineID.Substring(5, 1);   //線別
                sheetOverlap.Cells[rowIndexOverlap, 3].Value = prod;   //產品
                sheetOverlap.Cells[rowIndexOverlap, 4].Value = pid;   //PID
                sheetOverlap.Cells[rowIndexOverlap, 5].Value = DateTime.Now.ToString("MMdd");   //日期
                sheetOverlap.Cells[rowIndexOverlap, 6].Value = DateTime.Now.ToString("mm:ss");   //時間
                sheetOverlap.Cells[rowIndexOverlap, 7].Value = glassID;   //ID
                sheetOverlap.Cells[rowIndexOverlap, 8].Value = sealUnitDict[glassID];   //子機

                int cellIndexCorner = 8;
                int cellIndexNormal = 8;
                int cellIndexOverlap = 8;
                int cellIndexCornerOrg = cellIndexCorner;
                int cellIndexNormalOrg = cellIndexNormal;
                int cellIndexOverlapOrg = cellIndexOverlap;
                //遍歷DataGridView
                for (int x = 0; x < dataGridView.RowCount; x++)
                {
                    switch (dataGridView.Rows[x].Cells["dataType"].Value.ToString())
                    {
                        case "Corner":
                            cellIndexCorner++;
                            sheetCorner.Cells[rowIndexCorner, cellIndexCorner].Value = Convert.ToDouble(dataGridView.Rows[x].Cells["Value1"].Value.ToString());
                            cellIndexCorner++;
                            sheetCorner.Cells[rowIndexCorner, cellIndexCorner].Value = Convert.ToDouble(dataGridView.Rows[x].Cells["Value2"].Value.ToString());
                            break;
                        case "Normal":
                            cellIndexNormal++;
                            sheetNormal.Cells[rowIndexNormal, cellIndexNormal].Value = Convert.ToDouble(dataGridView.Rows[x].Cells["Value1"].Value.ToString());
                            break;
                        case "Overlap":
                            cellIndexOverlap++;
                            sheetOverlap.Cells[rowIndexOverlap, cellIndexOverlap].Value = Convert.ToDouble(dataGridView.Rows[x].Cells["Value1"].Value.ToString());
                            break;
                    }
                }

                //設定小數格式
                sheetCorner.Cells[rowIndexCorner, cellIndexCornerOrg, rowIndexCorner, cellIndexCorner].Style.Numberformat.Format = "0.00";
                sheetNormal.Cells[rowIndexNormal, cellIndexNormalOrg, rowIndexNormal, cellIndexNormal].Style.Numberformat.Format = "0.00";
                sheetOverlap.Cells[rowIndexOverlap, cellIndexOverlapOrg, rowIndexOverlap, cellIndexOverlap].Style.Numberformat.Format = "0.00";
                //建立檔案串流
                try
                {
                    using (FileStream OutputStream = new FileStream(xlsxPathLocal, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        //把剛剛的Excel物件真實存進檔案裡
                        ep.SaveAs(OutputStream);
                        //關閉串流
                        OutputStream.Close();
                    }

                    //上傳
                    try
                    {
                        File.Copy(xlsxPathLocal, xlsxPath, true);
                    }
                    catch
                    {
                        CallWarningForm("上傳量測紀錄表失敗，請確認 : " + xlsxPath + " 是否被開啟", "上傳量測紀錄表失敗");
                        return;
                    }
                }
                catch
                {
                    CallWarningForm("寫入量測紀錄表失敗，請確認 : " + xlsxPathLocal + " 是否被開啟", "寫入量測紀錄表失敗");
                    return;
                }
            }
        }
        private void recipe設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LevelCheck() == "ENG")
            {
                frmRecipe.ReadTable();
                frmRecipe.ShowDialog();
            }
            else
            {
                MessageBox.Show("權限不足，需要ENG以上權限", "權限警報");
                return;
            }
        }

        private void ReadRecipe()
        {
            recipeDict.Clear();
            if(mode == "0")
            {
                
            }
            else if (mode == "1")
            {
             
                for (int i = 1; i < 6; i++)
                {
                    if (Directory.Exists(Path.Combine(recipeDir, "LCIN", "TPAB0" + i.ToString())))
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(recipeDir, "LCIN", "TPAB0" + i.ToString()));
                        FileInfo[] recipeList = dirInfo.GetFiles("*.RCP");
                        if (recipeList.Count() > 0)
                        {
                            foreach (FileInfo file in recipeList)
                            {
                                string fileName = file.Name.Split('.')[0];

                                

                          
                                GetValue("Prod Data", "PID", out outString, file.FullName);
                                string pid = outString;
                                GetValue("Prod Data", "LineID", out outString, file.FullName);
                                string lineID = outString;

                                ReadRecipeData(file.FullName, lineID, pid);
                            }
                        }
                    }

                }

            }
        }
        private void ReadRecipeData(string filePath, string lineID, string pid)
        {
            if (File.Exists(filePath))
            {
                GetValue("Prod Data", "ProdName", out outString, filePath);
                string ProdName = outString;
                GetValue("Prod Data", "PID", out outString, filePath);
                string PID = outString;
                GetValue("Prod Data", "LineID", out outString, filePath);
                string LineID = outString;

                GetValue("Head Setting", "Head1", out outString, filePath);
                string Head1 = outString;
                GetValue("Head Setting", "Head2", out outString, filePath);
                string Head2 = outString;
                GetValue("Head Setting", "Head3", out outString, filePath);
                string Head3 = outString;
                GetValue("Head Setting", "Head4", out outString, filePath);
                string Head4 = outString;
                GetValue("Head Setting", "Head5", out outString, filePath);
                string Head5 = outString;
                GetValue("Head Setting", "Head6", out outString, filePath);
                string Head6 = outString;
                GetValue("Head Setting", "Head7", out outString, filePath);
                string Head7 = outString;
                GetValue("Head Setting", "Head8", out outString, filePath);
                string Head8 = outString;
                GetValue("Head Setting", "Head9", out outString, filePath);
                string Head9 = outString;
                GetValue("Head Setting", "Head10", out outString, filePath);
                string Head10 = outString;

                GetValue("SPEC Setting", "Normal_OOS_Max", out outString, filePath);
                string NormalOOSMax = outString;
                GetValue("SPEC Setting", "Normal_OOS_Min", out outString, filePath);
                string NormalOOSMin = outString;
                GetValue("SPEC Setting", "Normal_OOC_Max", out outString, filePath);
                string NormalOOCMax = outString;
                GetValue("SPEC Setting", "Normal_OOC_Min", out outString, filePath);
                string NormalOOCMin = outString;

                GetValue("SPEC Setting", "Overlap_OOS_Max", out outString, filePath);
                string OverlapOOSMax = outString;
                GetValue("SPEC Setting", "Overlap_OOS_Min", out outString, filePath);
                string OverlapOOSMin = outString;
                GetValue("SPEC Setting", "Overlap_OOC_Max", out outString, filePath);
                string OverlapOOCMax = outString;
                GetValue("SPEC Setting", "Overlap_OOC_Min", out outString, filePath);
                string OverlapOOCMin = outString;

                GetValue("SPEC Setting", "Corner_OOS_Max", out outString, filePath);
                string CornerOOSMax = outString;
                GetValue("SPEC Setting", "Corner_OOS_Min", out outString, filePath);
                string CornerOOSMin = outString;
                GetValue("SPEC Setting", "Corner_OOC_Max", out outString, filePath);
                string CornerOOCMax = outString;
                GetValue("SPEC Setting", "Corner_OOC_Min", out outString, filePath);
                string CornerOOCMin = outString;

                GetValue("AI Model Setting", "Corner_AI_Model", out outString, filePath);
                string ModelCorner = outString;
                GetValue("AI Model Setting", "Normal_AI_Model", out outString, filePath);
                string ModelNormal = outString;
                GetValue("AI Model Setting", "Overlap_AI_Model", out outString, filePath);
                string ModelOverlap = outString;

                GetValue("Sampling Rate Setting", "AI_Sampling_Rate", out outString, filePath);
                string AISamplingRate = outString;

                GetValue("AI DefectCode Type", "DefectCode_Type", out outString, filePath);
                string AIDefectCodeType = outString;



                RecpieLCIN recipe = new RecpieLCIN
                {
                    prodName = ProdName,
                    pid = PID,
                    lineID = LineID,
                    head1 = Head1,
                    head2 = Head2,
                    head3 = Head3,
                    head4 = Head4,
                    head5 = Head5,
                    head6 = Head6,
                    head7 = Head7,
                    head8 = Head8,
                    head9 = Head9,
                    head10 = Head10,
                    normalOOSMax = NormalOOSMax,
                    normalOOSMin = NormalOOSMin,
                    normalOOCMax = NormalOOCMax,
                    normalOOCMin = NormalOOCMin,
                    overlapOOSMax = OverlapOOSMax,
                    overlapOOSMin = OverlapOOSMin,
                    overlapOOCMax = OverlapOOCMax,
                    overlapOOCMin = OverlapOOCMin,
                    cornerOOSMax = CornerOOSMax,
                    cornerOOSMin = CornerOOSMin,
                    cornerOOCMax = CornerOOCMax,
                    cornerOOCMin = CornerOOCMin,
                    aiModelCorner = ModelCorner,
                    aiModelNormal = ModelNormal,
                    aiModelOverlap = ModelOverlap,
                    aiSamplingRate = AISamplingRate,
                    aiDefectCodeType = AIDefectCodeType
                };

                //組合headList
                GenerateHeadList.GenerateHeadListProperties(recipe);

                //檢查是否有空白的屬性
                if (ForeachClass.ForeachClassProperties<RecpieLCIN>(recipe))
                {
                    MessageBox.Show("Recipe : " + filePath + " 存在空白資料請確認!");
                }
                else
                {
                    if (!recipeDict.ContainsKey(pid + lineID))  //將recipe物件加入字典中
                        recipeDict.Add(pid + lineID, recipe);
                }
            }
            else
            {
                MessageBox.Show("Recipe File Not Found in : " + filePath);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frmAIData.ShowDialog();
        }

        private void buttonAIProcessStart_Click(object sender, EventArgs e)
        {
            if (buttonAIProcessStart.Text == "AI Process Start")
            {
                buttonAIProcessStart.Text = "AI Process Stop";
                buttonAIProcessStart.BackColor = Color.Red;

                //啟動WaitForAIOutput 執行緒
                threadWaitForAIOutput = new Thread(WaitForAIOutput);
                threadWaitForAIOutput.Name = "threadWaitForAIOutput";
                threadWaitForAIOutput.IsBackground = true;
                threadWaitForAIOutput.Start();
                //啟動AI Process
                timerAIProcess.Interval = 1000;
                timerAIProcess.Enabled = true;
                timerAIProcess.Start();
            }
            else
            {
                buttonAIProcessStart.Text = "AI Process Start";
                buttonAIProcessStart.BackColor = Color.Aqua;
                threadWaitForAIOutput.Abort();
                timerAIProcess.Stop();
                timerAIProcess.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prodNowAI = "E024";
            glassIDNowAI = "T12345678901";
            lineIDNowAI = "TPAB04";
            pidNowAI = "508";
            sealUnitDict.Add("T12345678901", "1");

            //寫入量測資料DataGridView Head
            frmPanelAI.dataGridViewLCIN1.Rows.Clear();
            //先填好Panel編號
            List<string> panelNumSplit = null;
            string[] typeList = new string[] { "Normal Seal", "Overlap", "Corner" };

            panelNumSplit = recipeDict[pidNowAI + lineIDNowAI].headList;
            for (int x = 0; x < panelNumSplit.Count() * 3; x++)
            {
                frmPanelAI.dataGridViewLCIN1.Rows.Add();
            }
            int rowCount = 0;
            for (int x = 0; x < panelNumSplit.Count(); x++)
            {
                foreach (string strTemp in typeList)
                {
                    frmPanelAI.dataGridViewLCIN1.Rows[rowCount].Cells[0].Value = "P" + panelNumSplit[x];
                    frmPanelAI.dataGridViewLCIN1.Rows[rowCount].Cells[1].Value = strTemp;
                    rowCount++;
                }

            }

            for(int c = 0; c < panelNumSplit.Count() * 3; c++)
            {
                switch (frmPanelAI.dataGridViewLCIN1.Rows[c].Cells[1].Value.ToString().ToUpper())
                {
                    case "CORNER":
                        frmPanelAI.dataGridViewLCIN1.Rows[c].Cells[2].Value = "1.00";
                        frmPanelAI.dataGridViewLCIN1.Rows[c].Cells[3].Value = "1.01";
                        break;
                    case "NORMAL SEAL":
                        frmPanelAI.dataGridViewLCIN1.Rows[c].Cells[2].Value = "1.02";
                        break;
                    case "OVERLAP":
                        frmPanelAI.dataGridViewLCIN1.Rows[c].Cells[2].Value = "1.03";
                        break;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GenerateMeasureDataToXlsx(DataSourceType.AI);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //開始將DATAGRIDVIEW轉成ARRAY
            measureDataSummary = DataGridViewToArray(DataSourceType.AI);
            GenerateXMLFile(measureDataSummary, DataSourceType.AI);
        }
    }
    public class PIINSpec
    {
        public string prodName { get; set; }
        public string aOOSMax { get; set; }
        public string aOOSMin { get; set; }
        public string bOOSMax { get; set; }
        public string bOOSMin { get; set; }
        public string cOOSMax { get; set; }
        public string cOOSMin { get; set; }
        public string dOOSMax { get; set; }
        public string dOOSMin { get; set; }
        public string aOOCMax { get; set; }
        public string aOOCMin { get; set; }
        public string bOOCMax { get; set; }
        public string bOOCMin { get; set; }
        public string cOOCMax { get; set; }
        public string cOOCMin { get; set; }
        public string dOOCMax { get; set; }
        public string dOOCMin { get; set; }
    }
    public class ReturnDataTable
    {
        public DataTable dataTable { get; set; }
        public DataTable dataTable2 { get; set; }
    }
    public class BoolAICheck
    {
        public bool Corner { get; set; }
        public bool Normal { get; set; }
        public bool Overlap { get; set; }
    }
    public class ForeachClass
    {
        /// <summary>
        /// 反射遍歷目標物件屬性與值檢查是否有空值
        /// </summary>
        /// <typeparam name="T">目標類型</typeparam>
        /// <param name="model">目標</param>
        public static bool ForeachClassProperties<T>(T model)
        {
            Type t = model.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            List<string> headList = new List<string> { };
            bool rtn = false;
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(model, null);
        
                if (value == null || value.ToString() == "")
                {
                    if (name.Substring(0, 4).ToUpper() != "HEAD")   //因head設定有可能會是空白，故略過不檢查
                        rtn = true;
                }
                else
                {
                    if (name.Length >= 4 && name.Substring(0, 4).ToUpper() == "HEAD")
                        headList.Add(value.ToString());
                }
                
            }
            //將組合後的headList寫入物件屬性
            
            return rtn;
        }
    }
    public class GenerateHeadList
    {
        /// <summary>
        /// 反射遍歷目標物件屬性與值組合headList
        /// </summary>

        public static void GenerateHeadListProperties(RecpieLCIN recipe)
        {
            Type t = recipe.GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            List<string> headList = new List<string> { };
         
            foreach (PropertyInfo item in PropertyList)
            {
                string name = item.Name;
                object value = item.GetValue(recipe, null);

                if (value != null && value.ToString() != "")
                {
                    if (name.Length >= 4 && name.Substring(0, 4).ToUpper() == "HEAD")
                        headList.Add(value.ToString());
                }
         

            }
            //將組合後的headList寫入物件屬性
            recipe.headList = headList;
        }
    }

    

}
