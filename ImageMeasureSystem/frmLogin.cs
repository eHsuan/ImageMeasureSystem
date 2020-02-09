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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        private string[] msg;

        public string[] GetMsg()
        {
            return msg;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            msg = new string[] { textBox1.Text, textBox2.Text };
        }
    }
}
