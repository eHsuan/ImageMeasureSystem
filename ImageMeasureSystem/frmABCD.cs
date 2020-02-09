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

namespace ImageMeasureSystem
{
    public partial class frmABCD : Form
    {
        int x;
        int y;
        int clearFlag = 0;
        List<Point> points = new List<Point> { };
        List<string> imagePathList = new List<string> { };
        frmPictureBoxZoomIn frmPictureBoxZoomIn = new frmPictureBoxZoomIn();
        funSpecialFunction fsf = new funSpecialFunction();
        Bitmap bp = null;
        Graphics g1 = null;
        string dpiX;
        string dpiY;

        GraphicsPath drawPath = null;
        PictureBox drawArea = new System.Windows.Forms.PictureBox();
        //標記目前圖檔種類
        string imageName;
        //標記目前圖檔編號
        int imageNum = 0;
        string maxImage;
        string outString;
        public frmABCD()
        {
           
            InitializeComponent();
            //宣告ini路徑
            IniFilePath = Application.StartupPath + "\\FixedImageJudgeSystem.ini";
            // 找出字體大小,並算出比例
            float dpiX, dpiY;
            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;
            dpiY = graphics.DpiY;
            int intPercent = (dpiX == 96) ? 100 : (dpiX == 120) ? 125 : 150;

            // 針對字體變更Label的大小
            if (intPercent == 125)
            {
                foreach(Control x in this.Controls)
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


            //drawArea.Image = bp;
            //g1 = Graphics.FromImage(bp);
            //g2 = drawArea.CreateGraphics();
            //drawArea = new System.Windows.Forms.PictureBox();
            //g2.DrawRectangle(new Pen(new SolidBrush(Color.Red), 10), 10, 10, 20, 40);

            //drawPath = new System.Drawing.Drawing2D.GraphicsPath();
            //drawPen = new Pen(Color.Blue, 4);


            //this.Height = this.Height * (intPercent / 100);
            //pictureBox1.Size = new Size(pictureBox1.Size.Width * (intPercent / 100), pictureBox1.Size.Height * (intPercent / 100));
        }

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
                ctrl.Refresh();

            }
        }
        //更新滑鼠座標
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            x = e.Location.X;
            y = e.Location.Y;
            labelNowX.Text = Convert.ToString(x);
            labelNowY.Text = Convert.ToString(y);
            this.Invalidate();      //促使表單重畫(Form1_Paint函式)
            pb.Invalidate();
        }

        //取得滑鼠座標並計算
        private void Form1_Con(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (dataGridViewMeasureData.RowCount < 2)
            {
                if (labelImageName.Text == "MT" || labelImageName.Text == "MB")
                {
                    if (dataGridViewMeasureData.RowCount >= 1)
                    {
                        dataGridViewMeasureData.Rows.Add();
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
                        dataGridViewMeasureData.Rows.Add();
                        dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                        dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                        dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                        points.Add(e.Location);
                        pb.Invalidate();
                    }
                }
                else
                {
                    dataGridViewMeasureData.Rows.Add();
                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[0].Value = dataGridViewMeasureData.RowCount.ToString();
                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[1].Value = Convert.ToString(x);
                    dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1].Cells[2].Value = Convert.ToString(y);
                    points.Add(e.Location);
                    pb.Invalidate();
                }
                
            }

            
        }

        private void CrossLine(object sender, PaintEventArgs e)
        {
            
        }

        //點擊滑鼠畫點和線
        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Red, 0, y, pictureBox1.Width, y);  //水平線
            e.Graphics.DrawLine(Pens.Red, x, 0, x, pictureBox1.Height);  //垂直線
            Rectangle circle = new Rectangle(0, 0, 5, 5);
            foreach (Point pt in points)
            {
                circle.X = pt.X - 3;
                circle.Y = pt.Y - 3;
                e.Graphics.DrawEllipse(Pens.Red, circle);
            }
            
            if (dataGridViewMeasureData.RowCount == 2)
            {
                e.Graphics.DrawLine(Pens.Red, float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()));
                
            }
            if (clearFlag == 1)
            {

                this.Refresh();
                clearFlag = 0;
            }
            

        }

        private void buttonSample_Click(object sender, EventArgs e)
        {
            ClearMeasureData();
        }

        private void ClearMeasureData()
        {
            dataGridViewMeasureData.Rows.Clear();

            points.Clear();
            pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewMeasureData.Rows.Remove(dataGridViewMeasureData.Rows[dataGridViewMeasureData.RowCount - 1]);
            points.RemoveAt(points.Count() - 1);
            pictureBox1.Refresh();
        }

        private void frmABCD_Load(object sender, EventArgs e)
        {
            GetValue("FEATURES", "MaxImage", out outString);
            maxImage = outString;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CalculateData();
        }
        public void CalculateData()
        {
            List<List<string>> dataSend = new List<List<string>> { };
            if (imageName.Split('.')[0].Substring(0, 2) == "MK")
            {
                List<string> dataSendRdy = new List<string> { };

                dataSendRdy.Add("MK");


                if (checkBoxOK.Checked == true)
                {
                    dataSendRdy.Add("OK");
                    dataSendRdy.Add("OK");
                    dataSendRdy.Add(imageName.Split('.')[0].Substring(2, 2));
                }
                else
                {
                    dataSendRdy.Add("NG");
                    dataSendRdy.Add("NG");
                    dataSendRdy.Add(imageName.Split('.')[0].Substring(2, 2));
                }
                if (dataSendRdy.Count > 0)
                {
                    dataSend.Add(dataSendRdy);
                }
            }
            else
            {
                for (int x = 0; x < dataGridViewMeasureData.RowCount; x++)
                {
                    List<string> dataSendRdy = new List<string> { };
                    if (imageName.Split('.')[0].Substring(0, 2) == "LT")
                    {
                        if (x == 0)
                        {
                            dataSendRdy.Add("A");
                        }
                        else if (x == 1)
                        {
                            dataSendRdy.Add("C");
                        }
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[1].Value.ToString());
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[2].Value.ToString());
                        dataSendRdy.Add(imageName.Split('.')[0].Substring(2, 2));
                    }
                    else if (imageName.Split('.')[0].Substring(0, 2) == "RB")
                    {
                        if (x == 0)
                        {
                            dataSendRdy.Add("B");
                        }
                        else if (x == 1)
                        {
                            dataSendRdy.Add("D");
                        }
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[1].Value.ToString());
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[2].Value.ToString());
                        dataSendRdy.Add(imageName.Split('.')[0].Substring(2, 2));
                    }
                    else if (imageName.Split('.')[0].Substring(0, 2) == "MT")
                    {
                        if (x == 0)
                        {
                            dataSendRdy.Add("MA");
                        }
                        else if (x == 1)
                        {
                            dataSendRdy.Add("MC");
                        }
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[1].Value.ToString());
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[2].Value.ToString());
                        dataSendRdy.Add(imageName.Split('.')[0].Substring(2, 2));
                    }
                    else if (imageName.Split('.')[0].Substring(0, 2) == "MB")
                    {
                        if (x == 0)
                        {
                            dataSendRdy.Add("MB");
                        }
                        else if (x == 1)
                        {
                            dataSendRdy.Add("MD");
                        }
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[1].Value.ToString());
                        dataSendRdy.Add(dataGridViewMeasureData.Rows[x].Cells[2].Value.ToString());
                        dataSendRdy.Add(imageName.Split('.')[0].Substring(2, 2));
                    }

                    if (dataSendRdy.Count > 0)
                    {
                        dataSend.Add(dataSendRdy);
                    }
                }
            }


            //組合儲存影像路徑
            string savePath = Path.Combine(Application.StartupPath, "Temp", frmMain.glassIDNow);
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
            DrawImageToBitmap(savePath);

            frmMain.measureData = dataSend;

            //呼叫下一張影像
            if (dataGridViewMeasureData.RowCount == 2 || (imageName.Substring(0, 2) == "MK" && (checkBoxNG.Checked != false || checkBoxOK.Checked != false)))
            {
                NextImage();
            }
        }
        public void PictureBoxBuild()
        {
            //讀取PICTUREBOX大小設定
            dpiX = frmMain.dpiX;
            dpiY = frmMain.dpiY;
            pictureBox1.Size = new Size(Convert.ToInt16(dpiX), Convert.ToInt16(dpiY));
            imagePathList = frmMain.imagePathList;
            //填入圖檔
            Image img = Image.FromFile(imagePathList[0]);
            imageName = Path.GetFileName(imagePathList[0]);
            imageNum = 0;
            pictureBox1.Image = img;
            labelImageName.Text = imageName.Split('.')[0].Substring(0, 2);
            labelImageNum.Text = imageName.Split('.')[0].Substring(2, 2);
            if (labelImageName.Text == "MK")
            {
                dataGridViewMeasureData.Visible = false;
                labelCheckMark.Visible = true;
                checkBoxOK.Visible = true;
                checkBoxNG.Visible = true;
            }
            else
            {
                dataGridViewMeasureData.Visible = true;
                labelCheckMark.Visible = false;
                checkBoxOK.Visible = false;
                checkBoxNG.Visible = false;
            }

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
            pictureBox1.Image = null;
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

        private void pictureBoxLeft_Click(object sender, EventArgs e)
        {
            PreviousImage();
        }

        private void pictureBoxRight_Click(object sender, EventArgs e)
        {

            NextImage();
        }

        private void NextImage()
        {
            if (imagePathList.Count != 0)
            {
                if (imagePathList.Count < imageNum + 2)
                {
                    MessageBox.Show("沒有下一張圖片");
                }
                else
                {
                    //變更圖檔
                    Image img = Image.FromFile(imagePathList[imageNum + 1]);
                    imageName = Path.GetFileName(imagePathList[imageNum + 1]);
                    imageNum = imageNum + 1;
                    pictureBox1.Image = img;
                    labelImageName.Text = imageName.Split('.')[0].Substring(0, 2);
                    labelImageNum.Text = imageName.Split('.')[0].Substring(2, 2);
                    //清除量測資訊
                    ClearMeasureData();
                    if (imageNum >= imagePathList.Count - 1)
                    {
                        frmMain.completeFlag = "1";
                    }

                    if (labelImageName.Text == "MK")
                    {
                        dataGridViewMeasureData.Visible = false;
                        labelCheckMark.Visible = true;
                        checkBoxOK.Checked = false;
                        checkBoxNG.Checked = false;
                        checkBoxOK.Visible = true;
                        checkBoxNG.Visible = true;
                    }
                    else
                    {
                        dataGridViewMeasureData.Visible = true;
                        labelCheckMark.Visible = false;
                        checkBoxOK.Visible = false;
                        checkBoxNG.Visible = false;
                    }
                }
            }
        }
        private void PreviousImage()
        {
            if (imagePathList.Count != 0)
            {
                if (imageNum == 0)
                {
                    MessageBox.Show("沒有上一張圖片");
                }
                else
                {
                    //變更圖檔
                    Image img = Image.FromFile(imagePathList[imageNum - 1]);
                    imageName = Path.GetFileName(imagePathList[imageNum - 1]);
                    imageNum = imageNum - 1;
                    pictureBox1.Image = img;
                    labelImageName.Text = imageName.Split('.')[0].Substring(0, 2);
                    labelImageNum.Text = imageName.Split('.')[0].Substring(2, 2);
                    //清除量測資訊
                    ClearMeasureData();

                    if (labelImageName.Text == "MK")
                    {
                        dataGridViewMeasureData.Visible = false;
                        labelCheckMark.Visible = true;
                        checkBoxOK.Visible = true;
                        checkBoxNG.Visible = true;
                    }
                    else
                    {
                        dataGridViewMeasureData.Visible = true;
                        labelCheckMark.Visible = false;
                        checkBoxOK.Visible = false;
                        checkBoxNG.Visible = false;
                    }
                }
            }
        }

        private void labelImageName_TextChanged(object sender, EventArgs e)
        {
            if (labelImageName.Text == "LT" || labelImageName.Text == "RB")
            {
                labelMeasureType.Text = "角落";
                labelMeasureType.ForeColor = Color.Red;
            }
            else if (labelImageName.Text.Substring(0, 1) == "M")
            {
                labelMeasureType.Text = "中間";
                labelMeasureType.ForeColor = Color.Blue;
            }
        }
        
        private void DrawImageToBitmap(string savePathOrg)
        {
            bp = new Bitmap(pictureBox1.Image, new Size(850, 850));
            g1 = Graphics.FromImage(bp);
            
            Rectangle circle = new Rectangle(0, 0, 5, 5);
            foreach (Point pt in points)
            {
                circle.X = pt.X - 3;
                circle.Y = pt.Y - 3;
                g1.DrawEllipse(Pens.Red, circle);
                
            }
            if (dataGridViewMeasureData.RowCount == 2)
            {
                //g1.DrawLine(Pens.Red, 50, 50, 100, 100);

                g1.DrawLine(Pens.Red, float.Parse(dataGridViewMeasureData.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridViewMeasureData.Rows[1].Cells[2].Value.ToString()));
                //g1.DrawImage(bp, float.Parse(dataGridView1.Rows[0].Cells[1].Value.ToString()), float.Parse(dataGridView1.Rows[0].Cells[2].Value.ToString()), float.Parse(dataGridView1.Rows[1].Cells[1].Value.ToString()), float.Parse(dataGridView1.Rows[1].Cells[2].Value.ToString()));
            }
            //g1.DrawImage(bp, -600, -600);
            string savePath = Path.Combine(savePathOrg, imageName.Substring(0, imageName.Count() - 3) + "jpg");
            if (Directory.Exists(savePathOrg))
            {
                bp.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            
        }

        private void checkBoxOK_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOK.Checked == true)
            {
                checkBoxNG.Checked = false;
            }
        }

        private void checkBoxNG_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNG.Checked == true)
            {
                checkBoxOK.Checked = false;
            }
        }

        public void CancelMeasure()     //取消量測
        {
            pictureBox1.Image = null;   //清空圖檔
            labelImageName.Text = "XX";
            labelImageNum.Text = "XX";
            labelMeasureType.Text = "XX";
            imagePathList.Clear();  //清空圖檔路徑清單
            dataGridViewMeasureData.Rows.Clear();
            points.Clear();
            PictureBoxRefresh(pictureBox1);

        }
    }
}
