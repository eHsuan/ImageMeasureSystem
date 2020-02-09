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
    public partial class frmTypeChose : Form
    {
        public frmTypeChose()
        {
            InitializeComponent();
            this.Text = "機台選擇";
        }
        private string msg;
       
        public string GetMsg()
        {
            return msg;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem != null)
            {
                msg = comboBox1.SelectedItem.ToString();
            }
            else
            {
                msg = "9";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
