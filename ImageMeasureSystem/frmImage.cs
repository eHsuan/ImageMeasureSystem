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
    public partial class frmImage : Form
    {
        frmPictureBoxZoomIn frmPictureBoxZoomIn = new frmPictureBoxZoomIn();
        frmABCD frmABCD = new frmABCD();
        string maxImage;
        string outString;
        public frmImage()
        {
            InitializeComponent();
            //宣告ini路徑
            IniFilePath = Application.StartupPath + "\\FixedImageJudgeSystem.ini";
        }
        public void PictureBoxBuild(List<string> imagePathList)
        {
            if (imagePathList[0] != "NoFolder")
            {
                int imageCount = imagePathList.Count();
                for (int i = 1; i < imageCount + 1; i++)
                {
                    //限制最大生成圖檔數量
                    if (int.Parse(maxImage) < i)
                    {
                        break;
                    }
                    //動態生成PictureBox
                    string pictureBoxName = "pictureBox" + i.ToString();
                    PictureBox pictureBoxDemo = new PictureBox();
                    pictureBoxDemo.Name = pictureBoxName;
                    pictureBoxDemo.Width = 800;
                    pictureBoxDemo.Height = 753;
                    pictureBoxDemo.Location = new Point(12 + (410 * ((i - 1) % 2)), 12 + (410 * (((i - 1) / 2))));

                    //填入圖檔
                    Image img = Image.FromFile(imagePathList[i - 1]);
                    Bitmap pictureBoxImg = new Bitmap(img, 400, 400);
                    pictureBoxDemo.Image = pictureBoxImg;
                    pictureBoxDemo.Click += PictureBoxClick;    //動態產生點擊事件
                    Controls.Add(pictureBoxDemo);   //將新產生的物件加入控制項群組
                }
                for (int z = 1; z < imageCount + 1; z++)
                {
                    //限制最大生成圖檔數量
                    if (int.Parse(maxImage) < z)
                    {
                        break;
                    }
                    string labelName = "label" + z.ToString();
                    Label labelDemo = new Label();
                    labelDemo.Name = labelName;
                    labelDemo.Text = z.ToString();
                    labelDemo.AutoSize = false;
                    labelDemo.Width = 40;
                    labelDemo.Height = 40;
                    labelDemo.Font = new Font("新細明體", 16);
                    labelDemo.ForeColor = Color.Red;
                    labelDemo.Location = new Point(12 + (410 * ((z - 1) % 2)), 12 + (410 * (((z - 1) / 2))));
                    Controls.Add(labelDemo);   //將新產生的物件加入控制項群組
                    labelDemo.BringToFront();   //將控件置頂
                }
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
        private void PictureBoxClick(object sender, EventArgs e)
        {
            //檢查是否已存在放大的灰階圖視窗
            IntPtr player = FindWindow(null, "ABCD Measure");
            if (player != IntPtr.Zero)
            {
                /* 傳送關閉的指令 */
                SendMessage(player, SC_CLOSE, 0, 0);
            }
            PictureBox pictureBoxDemo = sender as PictureBox;
            //frmPictureBoxZoomIn frmPictureBoxZoomIn = new frmPictureBoxZoomIn();
            //frmPictureBoxZoomIn.pictureBox1.Image = pictureBoxDemo.Image;
            //frmPictureBoxZoomIn.Show();
            frmABCD frmABCD = new frmABCD();
            frmABCD.pictureBox1.Image = pictureBoxDemo.Image;
            frmABCD.ShowDialog();
        }
        public void PictureBoxDelect()
        {
            //foreach (Control c in this.Controls)
            //{
            //    if (c is PictureBox)
            //    {
            //        c.Dispose();
            //        this.Controls.Remove(c);
            //    }
            //    //else if (c is Bitmap)
            //    //{

            //    //}
            //}
            int x = 1;
            while (true)
            {
                string pictureBoxName = "pictureBox" + x.ToString();
                string labelName = "label" + x.ToString();
                try
                {
                    PictureBox pic = this.Controls.Find(pictureBoxName, true)[0] as PictureBox;
                    Label lab = this.Controls.Find(labelName, true)[0] as Label;
                    pic.Dispose();
                    lab.Dispose();
                    this.Controls.Remove(pic);
                    this.Controls.Remove(lab);
                    x++;
                }
                catch
                {
                    break;
                }

            }

            foreach (Control c in this.Controls)
            {
                Console.WriteLine(c.Name);
                if (c is PictureBox)
                {
                    if (c.Name.Substring(0, 10) == "pictureBox")
                    { // 多一層保護
                        c.Dispose();
                        this.Controls.Remove(c);
                    }
                }
            }
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

        private void frmImage_Load(object sender, EventArgs e)
        {
            GetValue("FEATURES", "MaxImage", out outString);
            maxImage = outString;
        }
    }
}
