using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ImageMeasureSystem
{
    public partial class frmPanelTA : Form
    {
        public frmPanelTA()
        {
            InitializeComponent();
        }
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if (frmMain.mode == "0")
            {

            }
            else
            {
                frmMain.readRequest = new ReadRequest
                {
                    glassID = dataGridViewIdList.Rows[e.RowIndex].Cells["glassID"].Value.ToString(),
                    lineID = dataGridViewIdList.Rows[e.RowIndex].Cells["lineID"].Value.ToString(),
                    sealUnit = dataGridViewIdList.Rows[e.RowIndex].Cells["unitID"].Value.ToString(),
                    pid = dataGridViewIdList.Rows[e.RowIndex].Cells["pid"].Value.ToString(),
                    prodName = dataGridViewIdList.Rows[e.RowIndex].Cells["prodName"].Value.ToString()
                };
            }
            
        }
        
    }
    public class ReadRequest
    {
        public string glassID { get; set; }
        public string lineID { get; set; }
        public string sealUnit { get; set; }
        public string pid { get; set; }
        public string prodName { get; set; }
    }
}
