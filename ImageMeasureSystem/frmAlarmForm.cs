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
    public partial class frmAlarmForm : Form
    {
        public frmAlarmForm()
        {
            InitializeComponent();
        }
        public void SetMessage(string command)
        {
            textBoxMessage.Text = command;
            this.TopMost = true;
        }
    }
}
