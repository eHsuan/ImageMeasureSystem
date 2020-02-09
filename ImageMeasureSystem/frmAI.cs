using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using LCIN_AI;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Threading;


namespace ImageMeasureSystem
{
    public partial class frmAI : Form
    {
        private class MapAdress
        {
            public int mapX1 { get; set; }
            public int mapY1 { get; set; }
            public int mapX2 { get; set; }
            public int mapY2 { get; set; }
        }
        int x;
        int y;
        int clearFlag = 0;
        List<Point> points = new List<Point> { };
        List<string> imagePathList = new List<string> { };
        frmPictureBoxZoomIn frmPictureBoxZoomIn = new frmPictureBoxZoomIn();
        funSpecialFunction fsf = new funSpecialFunction();
        
        Bitmap bp = null;
        Graphics g1 = null;

        string fixMode;
        string mode = "";
        string dpiX;
        string dpiY;

        //全局变量
        object _obj = new object();
        object _obj2 = new object();
        object _obj3 = new object();
        object _obj4 = new object();
        object _obj5 = new object();
        object _obj6 = new object();
        object _obj7 = new object();
        object _obj8 = new object();
        object _obj9 = new object();
        object _obj10 = new object();
        GraphicsPath drawPath = null;
        PictureBox drawArea = new System.Windows.Forms.PictureBox();
        //標記目前圖檔種類
        string imageName;
        //標記目前圖檔編號
        int imageNum = 0;
        string maxImage;
        string outString;
        //public static string completeFlag = "0";    //標記是否所有圖片皆看完的Flag
        Dictionary<string, string> admRate = null;  //比例尺字典設定
        FinalFilterAIData finalFilterAIDataCorner = new FinalFilterAIData { };
        FinalFilterAIData finalFilterAIDataNormal = new FinalFilterAIData { };
        FinalFilterAIData finalFilterAIDataOverlap = new FinalFilterAIData { };

        FinalFilterAIData finalFilterAIDataCorner_AIOrg = new FinalFilterAIData { };    //紀錄AI原始資料用來比較人員
        FinalFilterAIData finalFilterAIDataNormal_AIOrg = new FinalFilterAIData { };    //紀錄AI原始資料用來比較人員
        FinalFilterAIData finalFilterAIDataOverlap_AIOrg = new FinalFilterAIData { };    //紀錄AI原始資料用來比較人員

        List<List<string>> drawStringList = new List<List<string>> { }; //將量測結果的文字存在array給背景畫布使用
        string lineIDNow;
        FilterAIData SealEdgeDataCorner;
        FilterAIData SealEdgeDataNormal;
        FilterAIData SealEdgeDataOverlap;

        //建立畫筆
        Pen pen1 = new Pen(Color.Yellow, 4);
        Pen pen2 = new Pen(Color.Blue, 10);
        Pen pen3 = new Pen(Color.Red, 3);
        Pen pen4 = new Pen(Color.Blue, 3);
        AI _ai = new AI();
        WaitMeasureID measureID;
        bool aiMeasuredFlag = false;    //標記目前這張圖是否曾經讀取過AI量測資料
        string imagePathNow;
        ImageType imageType;

        //呼叫委派Main UI修改控制項
        private delegate void PictureBoxRefreshCallBack(PictureBox ctrl);
        private void PictureBoxRefresh(PictureBox ctrl)
        {
            if (this.InvokeRequired)
            {
                PictureBoxRefreshCallBack uu = new PictureBoxRefreshCallBack(PictureBoxRefresh);
                this.Invoke(uu, ctrl);
            }

            else
            {
                lock (_obj)
                {
                    ctrl.Refresh();
                    Thread.Sleep(100);
                }

            }
        }

        //呼叫委派Main UI修改控制項
        private delegate void UpdatePictureBoxImageCallBack(PictureBox ctrl, Image image);
        private void UpdatePictureBoxImage(PictureBox ctrl, Image image)
        {
            
            if (this.InvokeRequired)
            {
                UpdatePictureBoxImageCallBack uu = new UpdatePictureBoxImageCallBack(UpdatePictureBoxImage);
                try
                {
                    this.Invoke(uu, ctrl, image);
                }
                catch
                {

                }
            }

            else
            {
                lock (_obj2)
                {
                    ctrl.Image = image;
                }

            }
            
        }

        //呼叫委派Main UIClear DataGridView
        private delegate void DataGridViewClearCallBack(DataGridView ctl);
        private void DataGridViewClear(DataGridView ctl)
        {
            
            if (this.InvokeRequired)
            {
                DataGridViewClearCallBack uu = new DataGridViewClearCallBack(DataGridViewClear);
                try
                {
                    this.Invoke(uu, ctl);
                }
                catch
                {

                }
            }

            else
            {
                lock (_obj3)
                {
                    ctl.Rows.Clear();
                }

            }
            
            
        }

        //呼叫委派Main UI Add DataGridView Row
        private delegate void AddDataGridViewRowCallBack(DataGridView ctl, int count);
        private void AddDataGridViewRow(DataGridView ctl, int count)
        {
            if (this.InvokeRequired)
            {
                AddDataGridViewRowCallBack uu = new AddDataGridViewRowCallBack(AddDataGridViewRow);
                this.Invoke(uu, ctl, count);
            }

            else
            {
                lock (_obj4)
                {
                    for (int c = 0; c < count; c++)
                    {
                        ctl.Rows.Add();
                    }
                }
                    
                
            }
        }

        ////呼叫委派Main UI Set DataGridView Value
        //private delegate void SetDataGridViewValueCallBack(DataGridView ctl, string value, int rowIndex, int cellIndex);
        //private void SetDataGridViewValue(DataGridView ctl, string value, int rowIndex, int cellIndex)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        SetDataGridViewValueCallBack uu = new SetDataGridViewValueCallBack(SetDataGridViewValue);
        //        this.Invoke(uu, ctl, value, rowIndex, cellIndex);
        //    }

        //    else
        //    {
        //        ctl.Rows[rowIndex].Cells[cellIndex].Value = value;

        //    }
        //}
        //呼叫委派Main UI Set DataGridView Value
        private delegate void SetDataGridViewValueCallBack(DataGridView ctl, List<List<string>> data);
        private void SetDataGridViewValue(DataGridView ctl, List<List<string>> data)
        {
            if (this.InvokeRequired)
            {
                SetDataGridViewValueCallBack uu = new SetDataGridViewValueCallBack(SetDataGridViewValue);
                this.Invoke(uu, ctl, data);
            }

            else
            {
                lock (_obj5)
                {
                    for (int c = 0; c < data.Count; c++)
                    {
                        for (int v = 0; v < data[0].Count; v++)
                        {
                            ctl.Rows[c].Cells[v].Value = data[c][v];
                        }
                    }
                }
                    

            }
        }
        //呼叫委派Main UI Update Label
        private delegate void UpdateLabelCallBack(Label ctl, string value);
        private void UpdateLabel(Label ctl, string value)
        {
            if (this.InvokeRequired)
            {
                UpdateLabelCallBack uu = new UpdateLabelCallBack(UpdateLabel);
                this.Invoke(uu, ctl, value);
            }

            else
            {
                lock (_obj6)
                {
                    ctl.Text = value;
                }
                    
            }
        }

        //呼叫委派Main UI Set PictureBox Size
        private delegate void SetPictureBoxSizeCallBack(PictureBox ctl, Size size);
        private void SetPictureBoxSize(PictureBox ctl, Size size)
        {
            if (this.InvokeRequired)
            {
                SetPictureBoxSizeCallBack uu = new SetPictureBoxSizeCallBack(SetPictureBoxSize);
                try
                {
                    this.Invoke(uu, ctl, size);
                }
                catch
                {

                }
            }

            else
            {
                lock (_obj7)
                {
                    ctl.Size = size;
                }
                    
            }
        }

        public frmAI()
        {
           
            InitializeComponent();
            //宣告ini路徑
            IniFilePath = frmMain.IniFilePath;
            // 找出字體大小,並算出比例
            float dpiX, dpiY;
            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;
            dpiY = graphics.DpiY;
            int intPercent = (dpiX == 96) ? 100 : (dpiX == 120) ? 125 : 150;



            // 針對字體變更Label的大小
            if (intPercent == 125)
            {
                foreach (Control x in this.Controls)
                {
                    if (x is Label)
                    {
                        x.Font = new Font("微軟正黑體", 10, FontStyle.Bold);
                    }
                    else if (x is Button)
                    {
                        x.Size = new Size(x.Size.Width + 10, x.Size.Height + 10);
                    }
                }
            }
            else if (intPercent == 96)
            {
                foreach (Control x in this.Controls)
                {
                    if (x is Label)
                    {
                        x.Font = new Font("微軟正黑體", 14, FontStyle.Bold);
                    }
                }
                this.Size = new Size(1287, 993);
            }


        }
        //更新滑鼠座標
    

        //取得滑鼠座標並計算
        private void Form1_Con(object sender, MouseEventArgs e)
        {
            
            fixMode = frmMain.fixMode;
            if (labelImageCount.Text != "XX")
            {
                PictureBox pb = (PictureBox)sender;
                if (labelMeasureType.Text != "Corner")
                {
                    if (dataGridViewMeasureData.RowCount < 2)
                    {
                        dataGridViewMeasureData.Rows.Add();
                        if (fixMode == "0") //一般模式
                        {
                            if (dataGridViewMeasureData.RowCount == 2)
                            {
                                //判斷是否為垂直
                                if (fsf.CheckVerticalOrParallel(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), e.Location.X.ToString(), e.Location.Y.ToString()))
                                {
                                    //是垂直
                                    
                                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString();    //垂直量測X2強制改為X1鎖定X軸
                                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                                    Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                                    points.Add(p);
                                }
                                else
                                {
                                    //是水平
                                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString();        //水平量測Y2強制改為Y1鎖定Y軸
                                    Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                                    points.Add(p);

                                }
                            }
                            else
                            {
                                //若為第一組座標則不做任何修正
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                                points.Add(e.Location);
                            }
                        }
                        else if (fixMode == "1")    //校正模式
                        {
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                            points.Add(e.Location);
                        }
                        pb.Invalidate();
                    }
                }
                else if (labelMeasureType.Text == "Corner")
                {
                    if (dataGridViewMeasureData.RowCount < 4)
                    {
                        dataGridViewMeasureData.Rows.Add();
                        if (dataGridViewMeasureData.RowCount == 2)
                        {
                            //判斷是否為垂直
                            if (fsf.CheckVerticalOrParallel(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), e.Location.X.ToString(), e.Location.Y.ToString()))
                            {
                                //是垂直
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString();    //垂直量測X2強制改為X1鎖定X軸
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                                Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                                points.Add(p);
                            }
                            else
                            {
                                //是水平
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString();        //水平量測Y2強制改為Y1鎖定Y軸
                                Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                                points.Add(p);

                            }
                        }
                        else if (dataGridViewMeasureData.RowCount == 4)
                        {
                            //判斷是否為垂直
                            if (fsf.CheckVerticalOrParallel(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString(), e.Location.X.ToString(), e.Location.Y.ToString()))
                            {
                                //是垂直
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString();    //垂直量測X2強制改為X1鎖定X軸
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                                Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                                points.Add(p);
                            }
                            else
                            {
                                //是水平
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                                dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString();        //水平量測Y2強制改為Y1鎖定Y軸
                                Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                                points.Add(p);

                            }
                        }
                        else if (dataGridViewMeasureData.RowCount == 6)
                        {
                            //Corner第三組座標不做任何判斷，量測絕對值距離

                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                            Point p = new Point(Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value), Convert.ToInt16(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value));
                            points.Add(p);

                        }
                        else if (dataGridViewMeasureData.RowCount == 1 || dataGridViewMeasureData.RowCount == 3 || dataGridViewMeasureData.RowCount == 5)
                        {
                            //若為第一組座標則不做任何修正
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                            dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                            points.Add(e.Location);
                        }
                        pb.Invalidate();
                    }
                }
            }
            //檢查是否曾經讀取過AI資料，是的話代表人員清除重量，要將重量的資料覆蓋掉AI的資料
            if (aiMeasuredFlag)
            {
                switch (labelMeasureType.Text.ToUpper())
                {
                    case "CORNER":
                        if (dataGridViewMeasureData.RowCount >= 4)  //滑鼠點擊觸發後資料齊全才觸發改寫AI資料
                        {
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeH1.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[0].Cells[1].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeH1.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[0].Cells[2].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeH2.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[1].Cells[1].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeH2.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[1].Cells[2].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeV1.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[2].Cells[1].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeV1.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[2].Cells[2].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeV2.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[3].Cells[1].Value);
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].edgeV2.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[3].Cells[2].Value);
                            //turn true人員改寫資料記錄用flag
                            finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]].BeChangedFlag = true;
                        }
                        break;
                    case "NORMAL":
                        if (dataGridViewMeasureData.RowCount >= 2)
                        {
                            finalFilterAIDataNormal.finalFilterAIData[imagePathList[imageNum]].edgeV1.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[0].Cells[1].Value);
                            finalFilterAIDataNormal.finalFilterAIData[imagePathList[imageNum]].edgeV1.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[0].Cells[2].Value);
                            finalFilterAIDataNormal.finalFilterAIData[imagePathList[imageNum]].edgeV2.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[1].Cells[1].Value);
                            finalFilterAIDataNormal.finalFilterAIData[imagePathList[imageNum]].edgeV2.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[1].Cells[2].Value);
                            finalFilterAIDataNormal.finalFilterAIData[imagePathList[imageNum]].BeChangedFlag = true;
                        }
                        break;
                    case "OVERLAP":
                        if (dataGridViewMeasureData.RowCount >= 2)
                        {
                            finalFilterAIDataOverlap.finalFilterAIData[imagePathList[imageNum]].edgeV1.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[0].Cells[1].Value);
                            finalFilterAIDataOverlap.finalFilterAIData[imagePathList[imageNum]].edgeV1.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[0].Cells[2].Value);
                            finalFilterAIDataOverlap.finalFilterAIData[imagePathList[imageNum]].edgeV2.adressX = Convert.ToInt16(dataGridViewMeasureData.Rows[1].Cells[1].Value);
                            finalFilterAIDataOverlap.finalFilterAIData[imagePathList[imageNum]].edgeV2.adressY = Convert.ToInt16(dataGridViewMeasureData.Rows[1].Cells[2].Value);
                            finalFilterAIDataOverlap.finalFilterAIData[imagePathList[imageNum]].BeChangedFlag = true;
                        }
                        break;
                }
            }
        }

       

        //點擊滑鼠畫點和線
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            lock(_obj10)
            {
                fixMode = frmMain.fixMode;
                if (labelImageCount.Text != "XX")
                {
                    e.Graphics.DrawLine(Pens.Red, 0, y, pictureBox1.Width, y);  //水平線
                    e.Graphics.DrawLine(Pens.Red, x, 0, x, pictureBox1.Height);  //垂直線
                    Rectangle circle = new Rectangle(0, 0, 2, 2);

                    foreach (Point pt in points)
                    {
                        circle.X = pt.X - 3;
                        circle.Y = pt.Y - 3;
                        e.Graphics.DrawEllipse(pen1, circle);
                    }


                    try
                    {
                        if (dataGridViewMeasureData.RowCount >= 2)    //第一組量測點
                        {
                            e.Graphics.DrawLine(pen3, float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()));
                            string distanceString = "";
                            string drawString = "";
                            if (fixMode == "0")     //一般模式
                            {
                                distanceString = CalculateDistanceForXY(float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()).ToString());
                                drawString = Math.Round(Convert.ToDouble(distanceString) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                            }
                            else
                            {
                                //校正模式
                                distanceString = CalculateDistance(float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()).ToString());
                                drawString = Math.Round(Convert.ToDouble(distanceString), 3).ToString();
                            }


                            System.Drawing.Font drawFont = new System.Drawing.Font("Red", 24);
                            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                            if (fsf.CheckVerticalOrParallel(float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()).ToString()))
                            {
                                //若為垂直則文字作標X取X1和X2大者，Y取Y1與Y2中心
                                float labelX = 0F;
                                if (Convert.ToDouble(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()))
                                {
                                    labelX = float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()) + 10;
                                }
                                else
                                {
                                    labelX = float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()) + 10;
                                }

                                float labelY = ((float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString())) / 2) - 30;
                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                                e.Graphics.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                                drawStringList.Add(new List<string> { drawString, "V", labelX.ToString(), labelY.ToString() });
                            }
                            else
                            {
                                //若為水平則文字坐標X取X1與X2中心，Y取Y1和Y2大者
                                float labelX = ((float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString())) / 2) - 50;
                                float labelY = 0F;
                                if (Convert.ToDouble(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()))
                                {
                                    labelY = float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()) - 60;
                                }
                                else
                                {
                                    labelY = float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()) - 60;
                                }

                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                                e.Graphics.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                                drawStringList.Add(new List<string> { drawString, "P", labelX.ToString(), labelY.ToString() });
                            }
                        }
                        if (dataGridViewMeasureData.RowCount == 4)    //第二組量測點
                        {
                            e.Graphics.DrawLine(pen4, float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()));
                            string distanceString = CalculateDistanceForXY(float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()).ToString());
                            string drawString = Math.Round(Convert.ToDouble(distanceString) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();

                            System.Drawing.Font drawFont = new System.Drawing.Font("Blue", 24);
                            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                            if (fsf.CheckVerticalOrParallel(float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()).ToString()))
                            {

                                //若為垂直則文字作標X取X1和X2大者，Y取Y1與Y2中心
                                float labelX = 0F;
                                if (Convert.ToDouble(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()))
                                {
                                    labelX = float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()) + 10;
                                }
                                else
                                {
                                    labelX = float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()) + 10;
                                }

                                float labelY = ((float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString())) / 2) - 30;
                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                                e.Graphics.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                                drawStringList.Add(new List<string> { drawString, "V", labelX.ToString(), labelY.ToString() });
                            }
                            else
                            {

                                //若為水平則文字坐標X取X1與X2中心，Y取Y1和Y2大者
                                float labelX = ((float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString())) / 2) - 50;
                                float labelY = 0F;
                                if (Convert.ToDouble(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()))
                                {
                                    labelY = float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()) - 60;
                                }
                                else
                                {
                                    labelY = float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()) - 60;
                                }

                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                                e.Graphics.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                                drawStringList.Add(new List<string> { drawString, "P", labelX.ToString(), labelY.ToString() });
                            }
                        }
                        else if (dataGridViewMeasureData.RowCount == 6)   //第三組量測座標
                        {
                            e.Graphics.DrawLine(pen4, float.Parse(dataGridViewMeasureData.Rows[4].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[4].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[5].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[3].Cells[5].Value.ToString()));
                            string distanceString = CalculateDistance(float.Parse(dataGridViewMeasureData.Rows[4].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[4].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[5].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[5].Cells[2].Value.ToString()).ToString());
                            string drawString = Math.Round(Convert.ToDouble(distanceString) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();

                            System.Drawing.Font drawFont = new System.Drawing.Font("Gold", 24);
                            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Gold);
                            if (fsf.CheckVerticalOrParallel(float.Parse(dataGridViewMeasureData.Rows[4].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[4].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[5].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[5].Cells[2].Value.ToString()).ToString()))
                            {

                                //若為垂直則文字作標X取X1和X2大者，Y取Y1與Y2中心
                                float labelX = 0F;
                                if (Convert.ToDouble(dataGridViewMeasureData.Rows[5].Cells[1].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[4].Cells[1].Value.ToString()))
                                {
                                    labelX = float.Parse(dataGridViewMeasureData.Rows[5].Cells[1].Value.ToString()) + 10;
                                }
                                else
                                {
                                    labelX = float.Parse(dataGridViewMeasureData.Rows[4].Cells[1].Value.ToString()) + 10;
                                }

                                float labelY = ((float.Parse(dataGridViewMeasureData.Rows[4].Cells[2].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[5].Cells[2].Value.ToString())) / 2) - 30;
                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                                e.Graphics.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                                drawStringList.Add(new List<string> { drawString, "V", labelX.ToString(), labelY.ToString() });
                            }
                            else
                            {

                                //若為水平則文字坐標X取X1與X2中心，Y取Y1和Y2大者
                                float labelX = ((float.Parse(dataGridViewMeasureData.Rows[4].Cells[1].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[5].Cells[1].Value.ToString())) / 2) - 50;
                                float labelY = 0F;
                                if (Convert.ToDouble(dataGridViewMeasureData.Rows[5].Cells[2].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[4].Cells[2].Value.ToString()))
                                {
                                    labelY = float.Parse(dataGridViewMeasureData.Rows[4].Cells[2].Value.ToString()) - 60;
                                }
                                else
                                {
                                    labelY = float.Parse(dataGridViewMeasureData.Rows[5].Cells[2].Value.ToString()) - 60;
                                }

                                System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                                drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                                e.Graphics.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                                drawStringList.Add(new List<string> { drawString, "P", labelX.ToString(), labelY.ToString() });
                            }
                        }

                    }
                    catch
                    {

                    }
                    if (clearFlag == 1)
                    {
                        this.Refresh();
                        clearFlag = 0;
                    }
                }
            }
            
            
            

        }

        private void buttonSample_Click(object sender, EventArgs e)
        {
            ClearMeasureData();
        }

        private void ClearMeasureData()  //清空量測資料
        {
            DataGridViewClear(dataGridViewMeasureData);
        
            points.Clear();
            PictureBoxRefresh(pictureBox1);
            
        }
        public void CancelMeasure()     //取消量測
        {
            pictureBox1.Image = null;   //清空圖檔
            UpdateLabel(labelImageCount, "XX");
            UpdateLabel(labelImageNum, "XX");
            UpdateLabel(labelMeasureType, "XX");

            imagePathList.Clear();  //清空圖檔路徑清單
            DataGridViewClear(dataGridViewMeasureData);
         
            points.Clear();
            PictureBoxRefresh(pictureBox1);
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewMeasureData.RowCount > 0)
            {
                dataGridViewMeasureData.Rows.Remove(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1]);
                points.RemoveAt(points.Count() - 1);
                PictureBoxRefresh(pictureBox1);
            }
            
        }

        private void frmABCD_Load(object sender, EventArgs e)
        {
            GetValue("FEATURES", "MaxImage", out outString);
            maxImage = outString;
            
        }

        public void CalculateData()
        {
            MeasureDataFromLCIN measureDataFromLCIN = new MeasureDataFromLCIN { };
            switch(labelMeasureType.Text)
            {
                case "Normal":
                    if (dataGridViewMeasureData.RowCount == 2)
                    {
                        //先判斷是垂直還是水平
                        if (fsf.CheckVerticalOrParallel(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()))
                        {

                        }
                        string distance = CalculateDistanceForXY(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString());

                        List<string> dataSendReady = new List<string> { distance };
                        measureDataFromLCIN.panelCount = labelImageNum.Text;
                        measureDataFromLCIN.measureType = labelMeasureType.Text;
                        measureDataFromLCIN.measureData = dataSendReady;
                        measureDataFromLCIN.dataSourceFromAIorTA = DataSourceType.AI;
                        //檢查是否為未被人員修改過的AI DATA，是的話TURN true changedFlag
                        if (SealEdgeDataNormal != null && SealEdgeDataNormal.BeChangedFlag == true)
                            measureDataFromLCIN.beChangedFlag = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "Overlap":
                    if (dataGridViewMeasureData.RowCount == 2)
                    {
                        //先判斷是垂直還是水平
                        if (fsf.CheckVerticalOrParallel(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()))
                        {

                        }
                        string distance = CalculateDistanceForXY(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString());

                        List<string> dataSendReady = new List<string> { distance };
                        measureDataFromLCIN.panelCount = labelImageNum.Text;
                        measureDataFromLCIN.measureType = labelMeasureType.Text;
                        measureDataFromLCIN.measureData = dataSendReady;
                        measureDataFromLCIN.dataSourceFromAIorTA = DataSourceType.AI;
                        //檢查是否為未被人員修改過的AI DATA，是的話TURN true changedFlag
                        if (SealEdgeDataOverlap != null && SealEdgeDataOverlap.BeChangedFlag == true)
                            measureDataFromLCIN.beChangedFlag = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "Corner":
                    if (dataGridViewMeasureData.RowCount == 4)
                    {
                        string distance1 = CalculateDistanceForXY(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString());
                        string distance2 = CalculateDistanceForXY(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString(), dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString(), dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString());

                        List<string> dataSendReady = new List<string> { distance1, distance2 };
                        measureDataFromLCIN.panelCount = labelImageNum.Text;
                        measureDataFromLCIN.measureType = labelMeasureType.Text;
                        measureDataFromLCIN.measureData = dataSendReady;
                        measureDataFromLCIN.dataSourceFromAIorTA = DataSourceType.AI;
                        if (SealEdgeDataCorner != null && SealEdgeDataCorner.BeChangedFlag == true)
                            measureDataFromLCIN.beChangedFlag = true;
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "Other":
                    break;
            }
            
            //組合儲存影像路徑
            string savePath = Path.Combine(Application.StartupPath, "Temp", frmMain.glassIDNowAI);
            if (!Directory.Exists(savePath))
            {
                try
                {
                    Directory.CreateDirectory(savePath);
                }
                catch
                {
                    MessageBox.Show("創建影像暫存時發生錯誤");
                }
            }
            lock(_obj9)
            {
                DrawImageToBitmap(savePath);
            }
            

            
           


            frmMain.measureDataFromLCIN.Add(measureDataFromLCIN);   //將資料物件加入main UI List

            
          
        }
        //計算絕對距離
        private string CalculateDistance(string mapX1, string mapY1, string mapX2, string mapY2)
        {
            string rtn = "";

            rtn = Math.Round(Math.Sqrt(Math.Pow(Math.Abs((Convert.ToDouble(mapX2) - Convert.ToDouble(mapX1))), 2) + Math.Pow(Math.Abs((Convert.ToDouble(mapY1) - Convert.ToDouble(mapY2))), 2)), 2).ToString();
            return rtn;
        }
        //只計算X距離或Y距離
        private string CalculateDistanceForXY(string mapX1, string mapY1, string mapX2, string mapY2)
        {
            string rtn = "";
            //先判斷是垂直還是水平
            if (fsf.CheckVerticalOrParallel(mapX1, mapY1, mapX2, mapY2))
            {
                //垂直
                rtn = Math.Round(Math.Abs((Convert.ToDouble(mapY2) - Convert.ToDouble(mapY1))), 2).ToString();
            }
            else
            {
                //水平
                rtn = Math.Round(Math.Abs((Convert.ToDouble(mapX2) - Convert.ToDouble(mapX1))), 2).ToString();
            }
            return rtn;
        }

        public void PictureBoxBuild()
        {
            //先讀取frmMain的admRate字典
            admRate = frmMain.admRate;
            
            //讀取目前線別
            lineIDNow = frmMain.lineIDNowAI;
            imagePathList = frmMain.imagePathListAI;
            //填入圖檔
            Image img = Image.FromFile(imagePathList[0]);
            imageName = Path.GetFileName(imagePathList[0]);
            imagePathNow = imagePathList[0];
            imageNum = 0;
            UpdatePictureBoxImage(pictureBox1, img);
            UpdateLabel(labelImageNum, imageName.Split('_')[3].Split('-')[0]);
            UpdateLabel(labelImageCount, "1 / " + imagePathList.Count().ToString());

            if (imageName.Split('_')[3].Split('-')[1].Substring(0, 3).Equals("nor", StringComparison.OrdinalIgnoreCase))
            {
                imageType = ImageType.Normal;
                UpdateLabel(labelMeasureType, "Normal");
                //確認是否有AI資料
                if (measureID.aiJudgedNormalFlag)
                    if (finalFilterAIDataNormal.finalFilterAIData.ContainsKey(imagePathList[0]))
                    {
                        SealEdgeDataNormal = finalFilterAIDataNormal.finalFilterAIData[imagePathList[0]];
                        SetAIDataInPoints(SealEdgeDataNormal, ImageType.Normal);
                        CalculateData();
                    }
            }
            else if (imageName.Split('_')[3].Split('-')[1].Substring(0, 3).Equals("ove", StringComparison.OrdinalIgnoreCase))
            {
                imageType = ImageType.Overlap;
                UpdateLabel(labelMeasureType, "Overlap");
                //確認是否有AI資料
                if (measureID.aiJudgedOverlapFlag)
                    if (finalFilterAIDataOverlap.finalFilterAIData.ContainsKey(imagePathList[0]))
                    {
                        SealEdgeDataOverlap = finalFilterAIDataOverlap.finalFilterAIData[imagePathList[0]];
                        SetAIDataInPoints(SealEdgeDataOverlap, ImageType.Overlap);
                        CalculateData();
                    }
            }
            else if (imageName.Split('_')[3].Split('-')[1].Substring(0, 3).Equals("cor", StringComparison.OrdinalIgnoreCase))
            {
                imageType = ImageType.Corner;
                UpdateLabel(labelMeasureType, "Corner");
                //確認是否有AI資料
                if (measureID.aiJudgedCornerFlag)
                    if (finalFilterAIDataCorner.finalFilterAIData.ContainsKey(imagePathList[0]))
                    {
                        SealEdgeDataCorner = finalFilterAIDataCorner.finalFilterAIData[imagePathList[0]];
                        SetAIDataInPoints(SealEdgeDataCorner, ImageType.Corner);
                        CalculateData();
                    }
            }
            else if (imageName.Split('_')[3].Split('-')[1].Substring(0, 3).Equals("oth", StringComparison.OrdinalIgnoreCase))
            {
                imageType = ImageType.Other;
                UpdateLabel(labelMeasureType, "Other");
            }
           
            PictureBoxRefresh(pictureBox1);
            Thread.Sleep(100);
        }
        //載入dll讀取INI
        [DllImport("kernel32.dll")]
        private static extern long WritePrivateProfileString(string section, string key, string value, string filepath);

        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder returnvalue, int buffersize, string filepath);
        private string IniFilePath;

        //宣告StringBuilder使用GetPrivateProfileString取得ini設定
        private void GetValue(string section, string key, out string value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            GetPrivateProfileString(section, key, "", stringBuilder, 1024, IniFilePath);
            value = stringBuilder.ToString();
        }
        //private void PictureBoxClick(object sender, EventArgs e)
        //{
        //    //檢查是否已存在放大的灰階圖視窗
        //    IntPtr player = FindWindow(null, "ABCD Measure");
        //    if (player != IntPtr.Zero)
        //    {
        //        /* 傳送關閉的指令 */
        //        SendMessage(player, SC_CLOSE, 0, 0);
        //    }
        //    PictureBox pictureBoxDemo = sender as PictureBox;
        //    //frmPictureBoxZoomIn frmPictureBoxZoomIn = new frmPictureBoxZoomIn();
        //    //frmPictureBoxZoomIn.pictureBox1.Image = pictureBoxDemo.Image;
        //    //frmPictureBoxZoomIn.Show();
        //    frmABCD frmABCD = new frmABCD();
        //    frmABCD.pictureBox1.Image = pictureBoxDemo.Image;
        //    frmABCD.ShowDialog();
        //}
        public void PictureBoxDelect()
        {
            UpdatePictureBoxImage(pictureBox1, null);
   
            //清除量測資訊
            ClearMeasureData();
            GC.Collect();   //啟動垃圾回收機制釋放資源
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

        public void ChangeImage()
        {
            for(int x = imageNum + 1; x < imagePathList.Count; x++ )
            {
                
                imageNum = x;
                //變更圖檔
                Image img = Image.FromFile(imagePathList[imageNum]);
                imageName = Path.GetFileName(imagePathList[imageNum]);
                imagePathNow = imagePathList[imageNum];
                //清除量測資訊
                ClearMeasureData();
                aiMeasuredFlag = false;
                UpdatePictureBoxImage(pictureBox1, img);
                UpdateLabel(labelImageNum, imageName.Split('_')[3].Substring(0, 3));

                switch (imageName.Split('_')[3].Split('-')[1].Substring(0, 3).ToUpper())
                {
                    case "NOR":
                        ReadImageInformation(ImageType.Normal);
                        break;
                    case "COR":
                        ReadImageInformation(ImageType.Corner);
                        break;
                    case "OVE":
                        ReadImageInformation(ImageType.Overlap);
                        break;
                    case "OTH":
                        ReadImageInformation(ImageType.Other);
                        break;
                }
                UpdateLabel(labelImageCount, (imageNum + 1).ToString() + " / " + imagePathList.Count().ToString());

                //ChangeUI(labelMeasureType.Text);    //轉換UI
                if (imageNum >= imagePathList.Count - 1)
                {
                    frmMain.completeFlag = "1";
                }
                PictureBoxRefresh(pictureBox1);
                //Thread.Sleep(500);
            }
        }

        private void ReadImageInformation(ImageType type)
        {
            switch (type)
            {
                case ImageType.Normal:
                    imageType = ImageType.Normal;
                    UpdateLabel(labelMeasureType, "Normal");
   
                    //確認是否有AI資料
                    if (measureID.aiJudgedNormalFlag)
                        if (finalFilterAIDataNormal.finalFilterAIData.ContainsKey(imagePathList[imageNum]))
                        {
                            SealEdgeDataNormal = finalFilterAIDataNormal.finalFilterAIData[imagePathList[imageNum]];
                            SetAIDataInPoints(SealEdgeDataNormal, ImageType.Normal);
                            aiMeasuredFlag = true;
                            if (!SealEdgeDataNormal.BeChangedFlag)
                                CalculateData();
                        }

                    break;
                case ImageType.Corner:
                    imageType = ImageType.Corner;
                    UpdateLabel(labelMeasureType, "Corner");
                    if (measureID.aiJudgedCornerFlag)
                        if (finalFilterAIDataCorner.finalFilterAIData.ContainsKey(imagePathList[imageNum]))
                        {
                            SealEdgeDataCorner = finalFilterAIDataCorner.finalFilterAIData[imagePathList[imageNum]];
                            SetAIDataInPoints(SealEdgeDataCorner, ImageType.Corner);
                            aiMeasuredFlag = true;
                            if (!SealEdgeDataCorner.BeChangedFlag)
                                CalculateData();
                        }
                    break;
                case ImageType.Overlap:
                    imageType = ImageType.Overlap;
                    UpdateLabel(labelMeasureType, "Overlap");
                    if (measureID.aiJudgedOverlapFlag)
                        if (finalFilterAIDataOverlap.finalFilterAIData.ContainsKey(imagePathList[imageNum]))
                        {
                            SealEdgeDataOverlap = finalFilterAIDataOverlap.finalFilterAIData[imagePathList[imageNum]];
                            SetAIDataInPoints(SealEdgeDataOverlap, ImageType.Overlap);
                            aiMeasuredFlag = true;
                            if (!SealEdgeDataOverlap.BeChangedFlag)
                                CalculateData();
                        }
                    break;
                case ImageType.Other:
                    UpdateLabel(labelMeasureType, "Other");
                    break;
            }
        }

        private void DrawImageToBitmap(string savePathOrg)
        {
            
            
            if (labelImageCount.Text != "XX")
            {
                lock (_obj8)
                {
                    Image image = (Image)pictureBox1.Image.Clone();  //複製一個副本出來儲存避免資源衝突
                    bp = new Bitmap(image, new Size(Convert.ToInt16(dpiX), Convert.ToInt16(dpiY)));
                    g1 = Graphics.FromImage(bp);
                }
                    

                Rectangle circle = new Rectangle(0, 0, 5, 5);
                foreach (Point pt in points)
                {
                    circle.X = pt.X - 3;
                    circle.Y = pt.Y - 3;
                    g1.DrawEllipse(pen1, circle);

                }
                if (dataGridViewMeasureData.RowCount >= 2)
                {
                    g1.DrawLine(pen3, float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()));
                    string distanceString = CalculateDistanceForXY(float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()).ToString());
                    string drawString = Math.Round(Convert.ToDouble(distanceString) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();
                    System.Drawing.Font drawFont = new System.Drawing.Font("Red", 24);
                    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                    if (fsf.CheckVerticalOrParallel(float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()).ToString()))
                    {

                        //若為垂直則文字作標X取X1和X2大者，Y取Y1與Y2中心
                        float labelX = 0F;
                        if (Convert.ToDouble(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()))
                        {
                            labelX = float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()) + 10;
                        }
                        else
                        {
                            labelX = float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()) + 10;
                        }

                        float labelY = ((float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString())) / 2) - 30;
                        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                        g1.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                        drawStringList.Add(new List<string> { drawString, "V", labelX.ToString(), labelY.ToString() });
                    }
                    else
                    {
                        //若為水平則文字坐標X取X1與X2中心，Y取Y1和Y2大者
                        float labelX = ((float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString())) / 2) - 50;
                        float labelY = 0F;
                        if (Convert.ToDouble(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()))
                        {
                            labelY = float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()) - 60;
                        }
                        else
                        {
                            labelY = float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()) - 60;
                        }

                        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                        g1.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                        drawStringList.Add(new List<string> { drawString, "P", labelX.ToString(), labelY.ToString() });
                    }
                }
                if (dataGridViewMeasureData.RowCount == 4)
                {
                    g1.DrawLine(pen4, float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()));
                    string distanceString = CalculateDistanceForXY(float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()).ToString());
                    string drawString = Math.Round(Convert.ToDouble(distanceString) * Convert.ToDouble(admRate[lineIDNow]), 3).ToString();

                    System.Drawing.Font drawFont = new System.Drawing.Font("Blue", 24);
                    System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
                    if (fsf.CheckVerticalOrParallel(float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()).ToString(), float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()).ToString()))
                    {

                        //若為垂直則文字作標X取X1和X2大者，Y取Y1與Y2中心
                        float labelX = 0F;
                        if (Convert.ToDouble(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()))
                        {
                            labelX = float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString()) + 10;
                        }
                        else
                        {
                            labelX = float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()) + 10;
                        }

                        float labelY = ((float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString())) / 2) - 30;
                        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                        g1.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                        drawStringList.Add(new List<string> { drawString, "V", labelX.ToString(), labelY.ToString() });
                    }
                    else
                    {

                        //若為水平則文字坐標X取X1與X2中心，Y取Y1和Y2大者
                        float labelX = ((float.Parse(dataGridViewMeasureData.Rows[2].Cells[1].Value.ToString()) + float.Parse(dataGridViewMeasureData.Rows[3].Cells[1].Value.ToString())) / 2) - 50;
                        float labelY = 0F;
                        if (Convert.ToDouble(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()) > Convert.ToDouble(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()))
                        {
                            labelY = float.Parse(dataGridViewMeasureData.Rows[2].Cells[2].Value.ToString()) - 60;
                        }
                        else
                        {
                            labelY = float.Parse(dataGridViewMeasureData.Rows[3].Cells[2].Value.ToString()) - 60;
                        }

                        System.Drawing.StringFormat drawFormat = new System.Drawing.StringFormat();
                        drawFormat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                        g1.DrawString(drawString, drawFont, drawBrush, labelX, labelY, drawFormat);
                        drawStringList.Add(new List<string> { drawString, "P", labelX.ToString(), labelY.ToString() });
                    }
                }

                //if (dataGridView1.RowCount >= 2)
                //{

                //    g1.DrawLine(pen3, float.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString()));
                //}
                //if (dataGridView1.RowCount == 4)
                //{
                //    g1.DrawLine(pen4, float.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString()));

                //}
                string savePath = Path.Combine(savePathOrg, imageName.Substring(0, imageName.Count() - 3) + "jpg");
                if (Directory.Exists(savePathOrg))
                {
                    bp.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }

            


        }
        

       
        //依照圖檔類型改變UI介面的判斷function
        private void ChangeUI(string imageType)
        {
            if (imageType == "Other")
            {
                labelDontMeasure.Visible = true;
                dataGridViewMeasureData.Visible = false;
            }
            else
            {
                labelDontMeasure.Visible = false;
                dataGridViewMeasureData.Visible = true;
            }
        }
        
        public void MainThread()
        {
            //從frmMain讀取WaitMeasureID物件
            measureID = frmMain.waitMeasureDict[frmMain.glassIDNowAI];
            //清除舊資料
            finalFilterAIDataCorner = null;
            finalFilterAIDataNormal = null;
            finalFilterAIDataOverlap = null;

            //讀取PICTUREBOX大小設定
            dpiX = frmMain.dpiX;
            dpiY = frmMain.dpiY;
            SetPictureBoxSize(pictureBox1, new Size(Convert.ToInt16(dpiX), Convert.ToInt16(dpiY)));

            //讀取原圖解析度
            double zoomRateX = 0;
            double zoomRateY = 0;
            using(Bitmap bm = new Bitmap(measureID.imageNameListCorner[0], true))
            {
                zoomRateX = Convert.ToInt16(dpiX) / Convert.ToDouble(bm.Width);
                zoomRateY = Convert.ToInt16(dpiY) / Convert.ToDouble(bm.Height);
            }
            //檢查是否需要確認AI資料
            //Corner
            if (measureID.aiJudgedCornerFlag)
            {
                finalFilterAIDataCorner = _ai.ReadAIData(Path.Combine(frmMain.aiServer, "Output"), measureID, int.Parse(frmMain.dpiX), int.Parse(frmMain.dpiY), ImageType.Corner, zoomRateX, zoomRateY, measureID.aiDefectCodeType);
                finalFilterAIDataCorner_AIOrg = finalFilterAIDataCorner;
            }

            if (measureID.aiJudgedNormalFlag)
            {
                finalFilterAIDataNormal = _ai.ReadAIData(Path.Combine(frmMain.aiServer, "Output"), measureID, int.Parse(frmMain.dpiX), int.Parse(frmMain.dpiY), ImageType.Normal, zoomRateX, zoomRateY, measureID.aiDefectCodeType);
                finalFilterAIDataNormal_AIOrg = finalFilterAIDataNormal;
            }

            if (measureID.aiJudgedOverlapFlag)
            {
                finalFilterAIDataOverlap = _ai.ReadAIData(Path.Combine(frmMain.aiServer, "Output"), measureID, int.Parse(frmMain.dpiX), int.Parse(frmMain.dpiY), ImageType.Overlap, zoomRateX, zoomRateY, measureID.aiDefectCodeType);
                finalFilterAIDataOverlap_AIOrg = finalFilterAIDataOverlap;
            }
            PictureBoxBuild();
            ChangeImage();
        }

        public void AIProcess()
        {
            PictureBoxDelect();
            MainThread();
            frmMain.threadAIProcessCompleteFlag = true;

        }


        //校正兩點之間的座標避免出現斜向量測
        private MapAdress FixMeasureMap(MapAdress map, string direct)
        {
            if (direct.ToUpper() == "H")
            {
                return new MapAdress { mapX1 = map.mapX1, mapY1 = map.mapY1, mapX2 = map.mapX2, mapY2 = map.mapY1 };    //若為水平則將Y2改為Y1
            }
            else
            {
                return new MapAdress { mapX1 = map.mapX1, mapY1 = map.mapY1, mapX2 = map.mapX1, mapY2 = map.mapY2 };    //若為垂直則將X2改為X1
            }
        }
        private void SetAIDataInPoints(FilterAIData filterAIData, ImageType type)
        {
            if (type == ImageType.Corner)
            {
                MapAdress mapH = new MapAdress
                {
                    mapX1 = filterAIData.edgeH1.adressX,
                    mapY1 = filterAIData.edgeH1.adressY,
                    mapX2 = filterAIData.edgeH2.adressX,
                    mapY2 = filterAIData.edgeH2.adressY,
                };
                MapAdress mapV = new MapAdress
                {
                    mapX1 = filterAIData.edgeV1.adressX,
                    mapY1 = filterAIData.edgeV1.adressY,
                    mapX2 = filterAIData.edgeV2.adressX,
                    mapY2 = filterAIData.edgeV2.adressY,
                };
                //校正座標避免斜向量測
                if (!filterAIData.BeChangedFlag)    //若資料被人員改寫過代表不是第一次讀取了不用再做第二次校正
                {
                    mapH = FixMeasureMap(mapH, "V");    //CH00水平缺陷代表的是垂直距離故為V
                    mapV = FixMeasureMap(mapV, "H");    //CV00水平缺陷代表的是垂直距離故為H
                }

                AddDataGridViewRow(dataGridViewMeasureData, 4);

                List<List<string>> data = new List<List<string>> 
                {
                    new List<string>{ "1", mapH.mapX1.ToString(), mapH.mapY1.ToString()},
                    new List<string>{ "2", mapH.mapX2.ToString(), mapH.mapY2.ToString()},
                    new List<string>{ "3", mapV.mapX1.ToString(), mapV.mapY1.ToString()},
                    new List<string>{ "4", mapV.mapX2.ToString(), mapV.mapY2.ToString()}
                };
                SetDataGridViewValue(dataGridViewMeasureData, data);

                //SetDataGridViewValue(dataGridViewMeasureData, "1", 0, 0);
                //SetDataGridViewValue(dataGridViewMeasureData, mapH.mapX1.ToString(), 0, 1);
                //SetDataGridViewValue(dataGridViewMeasureData, mapH.mapY1.ToString(), 0, 2);
                points.Add(new Point(Convert.ToInt16(mapH.mapX1), Convert.ToInt16(mapH.mapY1)));

                //SetDataGridViewValue(dataGridViewMeasureData, "2", 1, 0);
                //SetDataGridViewValue(dataGridViewMeasureData, mapH.mapX2.ToString(), 1, 1);
                //SetDataGridViewValue(dataGridViewMeasureData, mapH.mapY2.ToString(), 1, 2);
                points.Add(new Point(Convert.ToInt16(mapH.mapX2), Convert.ToInt16(mapH.mapY2)));

                //SetDataGridViewValue(dataGridViewMeasureData, "3", 2, 0);
                //SetDataGridViewValue(dataGridViewMeasureData, mapV.mapX1.ToString(), 2, 1);
                //SetDataGridViewValue(dataGridViewMeasureData, mapV.mapY1.ToString(), 2, 2);
                points.Add(new Point(Convert.ToInt16(mapV.mapX1), Convert.ToInt16(mapH.mapY1)));

                //SetDataGridViewValue(dataGridViewMeasureData, "4", 3, 0);
                //SetDataGridViewValue(dataGridViewMeasureData, mapV.mapX2.ToString(), 3, 1);
                //SetDataGridViewValue(dataGridViewMeasureData, mapV.mapY2.ToString(), 3, 2);
                points.Add(new Point(Convert.ToInt16(mapV.mapX2), Convert.ToInt16(mapH.mapY2)));
            }
            else

            {
                MapAdress map = new MapAdress
                {
                    mapX1 = filterAIData.edgeV1.adressX,
                    mapY1 = filterAIData.edgeV1.adressY,
                    mapX2 = filterAIData.edgeV2.adressX,
                    mapY2 = filterAIData.edgeV2.adressY,
                };

                //校正座標避免斜向量測
                map = FixMeasureMap(map, filterAIData.isHorizontal);

                AddDataGridViewRow(dataGridViewMeasureData, 2);

                List<List<string>> data = new List<List<string>>
                {
                    new List<string>{ "1", map.mapX1.ToString(), map.mapY1.ToString()},
                    new List<string>{ "2", map.mapX2.ToString(), map.mapY2.ToString()}
                };
                SetDataGridViewValue(dataGridViewMeasureData, data);
                //SetDataGridViewValue(dataGridViewMeasureData, "1", 0, 0);
                //SetDataGridViewValue(dataGridViewMeasureData, map.mapX1.ToString(), 0, 1);
                //SetDataGridViewValue(dataGridViewMeasureData, map.mapY1.ToString(), 0, 2);
                points.Add(new Point(Convert.ToInt16(map.mapX1), Convert.ToInt16(map.mapY1)));

                //SetDataGridViewValue(dataGridViewMeasureData, "2", 1, 0);
                //SetDataGridViewValue(dataGridViewMeasureData, map.mapX2.ToString(), 1, 1);
                //SetDataGridViewValue(dataGridViewMeasureData, map.mapY2.ToString(), 1, 2);
                points.Add(new Point(Convert.ToInt16(map.mapX2), Convert.ToInt16(map.mapY2)));

            }
            
        }

        public void GenerateAIXlsx(DataGridView dataGridViewHuman, DataGridView dataGridViewAI)
        {
            string xlsxPath = Path.Combine(Application.StartupPath, "Log", DateTime.Now.ToString("yyyyMMdd"), "AI_Data.xlsx");
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
            saveFileDialog.FileName = "AI_Data.xlsx";
            FileStream fs;
            FileInfo fileInfo;
            ExcelPackage ep;

            if (!Directory.Exists(Path.Combine(Application.StartupPath, "Log", DateTime.Now.ToString("yyyyMMdd"))))
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, "Log", DateTime.Now.ToString("yyyyMMdd")));
            
            if (File.Exists(xlsxPath))
            {
                //打開excel報表
                fileInfo = new FileInfo(xlsxPath);
                ep = new ExcelPackage(fileInfo);
            }
            else
            {
                //創建excel報表
                ep = new ExcelPackage();
            }
            

            if (ep.Workbook.Worksheets["AI_Data"] == null)
                ep.Workbook.Worksheets.Add("AI_Data");
            
            ExcelWorksheet sheet1 = ep.Workbook.Worksheets["AI_Data"];//取得Sheet AI_Data
            //int startRowNumber = sheet1.Dimension == null ? 1 : sheet1.Dimension.Start.Row;//起始列編號，從1算起
            int endRowNumber = sheet1.Dimension == null ? 1 : sheet1.Dimension.End.Row;//結束列編號，從1算起
            //int startColumn = sheet1.Dimension.Start.Column;//開始欄編號，從1算起
            //int endColumn = sheet1.Dimension.End.Column;//結束欄編號，從1算起

            //若結束為第一行則填入headtitle
            if (endRowNumber == 1)
            {
                string[] headTitle = { "GlassID", "Prod", "HeadNum", "Type", "Value1", "Value2" };
                int c = 1;
                foreach (string tempStr in headTitle)
                {
                    sheet1.Cells[endRowNumber, c].Value = tempStr;
                    c++;
                }
                endRowNumber++;
            }
            
            for (int x = 0; x < dataGridViewHuman.RowCount; x++)
            {
                sheet1.Cells[endRowNumber, 1].Value = measureID.glassID;
                sheet1.Cells[endRowNumber + 1, 1].Value = measureID.glassID;
                sheet1.Cells[endRowNumber, 2].Value = measureID.prod;
                sheet1.Cells[endRowNumber + 1, 2].Value = measureID.prod;
                sheet1.Cells[endRowNumber, 3].Value = dataGridViewHuman.Rows[x].Cells[0].Value;
                sheet1.Cells[endRowNumber + 1, 3].Value = dataGridViewAI.Rows[x].Cells[0].Value;
                sheet1.Cells[endRowNumber, 4].Value = dataGridViewHuman.Rows[x].Cells[1].Value;
                sheet1.Cells[endRowNumber + 1, 4].Value = dataGridViewAI.Rows[x].Cells[1].Value + "_AI";
                sheet1.Cells[endRowNumber, 5].Value = Convert.ToDouble(dataGridViewHuman.Rows[x].Cells[2].Value);
                sheet1.Cells[endRowNumber + 1, 5].Value = Convert.ToDouble(dataGridViewAI.Rows[x].Cells[2].Value);
                if (dataGridViewHuman.Rows[x].Cells[3].Value != null)
                {
                    sheet1.Cells[endRowNumber, 6].Value = Convert.ToDouble(dataGridViewHuman.Rows[x].Cells[3].Value);
                    sheet1.Cells[endRowNumber + 1, 6].Value = Convert.ToDouble(dataGridViewAI.Rows[x].Cells[3].Value);
                }
                endRowNumber += 2;
            }

            //建立檔案串流
            FileStream OutputStream = new FileStream(xlsxPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            //把剛剛的Excel物件真實存進檔案裡
            ep.SaveAs(OutputStream);
            //關閉串流
            OutputStream.Close();
        }
        
        private void CalculateAllMeasureData()
        {

        }
    }

}
