using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace ADMImageJudgeSystem
{
    public partial class frmAlarm : Form
    {
        frmMain frmMain = new frmMain();
        Thread change;
        Thread top;
        int x = 0;
        public frmAlarm()
        {
            InitializeComponent();
            //註冊timerpictureBox;提示色塊的Tick事件
            timerpictureBox.Tick += new EventHandler(timerpictureBox_Tick);
            //註冊timerNow;視窗置頂的Tick事件
            timerTopMost.Tick += new EventHandler(timerTopMost_Tick);
        }

        private void timerpictureBox_Tick(object sender, EventArgs e)
        {
            if (x == 0)
            {
                pictureBoxTop.BackColor = Color.Red;
                pictureBoxButton.BackColor = Color.Red;
                x++;
            }
            else
            {
                pictureBoxTop.BackColor = Color.Silver;
                pictureBoxButton.BackColor = Color.Silver;
                x--;
            }
            
        }
        private void timerTopMost_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
        }
        //呼叫委派Main UI修改控制項
        private delegate void UpdatePictureBoxCallBack(string vaule, PictureBox ctl);
        private void UpdatePictureBox(string vaule, PictureBox ctl)
        {
            if (this.InvokeRequired)
            {
                UpdatePictureBoxCallBack uu = new UpdatePictureBoxCallBack(UpdatePictureBox);
                this.Invoke(uu, vaule, ctl);
            }

            else
            {
                if (vaule == "Red")
                {
                    ctl.BackColor = Color.Red;
                }
                else if (vaule == "Silver")
                {
                    ctl.BackColor = Color.Silver;
                }
                
            }
        }
        //呼叫委派Main UI修改控制項
        private delegate void UpdateFormCallBack(frmAlarm ctl);
        private void UpdateForm(frmAlarm ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateFormCallBack uu = new UpdateFormCallBack(UpdateForm);
                this.Invoke(uu, ctl);
            }

            else
            {
                ctl.TopMost = true;
            }
        }
        private void ChangeColor()
        {
            while(true)
            {
                UpdatePictureBox("Red", pictureBoxTop);
                UpdatePictureBox("Red", pictureBoxButton);
                Thread.Sleep(500);
                UpdatePictureBox("Silver", pictureBoxTop);
                UpdatePictureBox("Silver", pictureBoxButton);
                Thread.Sleep(500);
            }
        }
        private void WinformTop()
        {
            while (true)
            {
                UpdateForm(this);
            }
        }
        private void ThreadChange()
        {
            change = new Thread(ChangeColor);
            change.IsBackground = true;
            change.Start();
        }
        private void ThreadTop()
        {
            top = new Thread(WinformTop);
            top.IsBackground = true;
            top.Start();
        }

        private void frmAlarm_Load(object sender, EventArgs e)
        {
            timerpictureBox.Interval = 1000;
            timerpictureBox.Enabled = true;
            timerpictureBox.Start();
            timerTopMost.Interval = 1000;
            timerTopMost.Enabled = true;
            timerTopMost.Start();
            //ThreadChange();
            //ThreadTop();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            //change.Abort();
            //top.Abort();
            try
            {
                timerpictureBox.Stop();
                timerTopMost.Stop();
            }
            catch
            {

            }
            this.Close();
        }
        public void WriteToAlarmMessage(string message)
        {
            textBoxAlarmMessage.Text = message;
        }
    }
    
}
