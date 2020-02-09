using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageMeasureSystem
{
    public partial class frmWarningForm : Form
    {
        public frmWarningForm()
        {
            InitializeComponent();
        }
        //呼叫委派Main UI修改控制項
        private delegate void UpdateLabelCallBack(string value, Label ctl);
        private void UpdateUI(string value, Label ctl)
        {
            if (this.InvokeRequired)
            {
                UpdateLabelCallBack uu = new UpdateLabelCallBack(UpdateUI);
                this.Invoke(uu, value, ctl);
            }

            else
            {
                ctl.Text = value;
            }
        }
        public void SetMessage(string command)
        {
            textBoxMessage.Text = command;
            this.TopMost = true;
        }
    }
}
