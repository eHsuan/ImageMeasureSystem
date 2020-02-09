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
    public partial class frmExcelCopyData : Form
    {
        string lineIDNow;
        string sealUnitNow;
        string prodNow;
        string recipeIDNow;
        string glassIDNow;
        public frmExcelCopyData()
        {
            InitializeComponent();
        }
        public void UpdataDataGridView(List<List<string>> measureData)
        {
            if (measureData.Count == 4)
            {
                dataGridView1.Rows.Clear();
                lineIDNow = frmMain.lineIDNow;
                sealUnitNow = frmMain.unitIDNow;
                prodNow = frmMain.prodNow;
                recipeIDNow = frmMain.recipeIDNow;
                glassIDNow = frmMain.glassIDNow;
            
                int x = 0;
                foreach (List<string> listTemp in measureData)
                {
                    if (x == 0)
                    {
                        x++;
                    }
                    else
                    {
                        dataGridView1.Rows.Add();
                        if (x == 1)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["type"].Value = "Normal";
                        }
                        else if (x == 2)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["type"].Value = "Overlap";
                        }
                        else if (x ==3)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["type"].Value = "Corner";
                        }
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["lineID"].Value = lineIDNow;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = prodNow;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["pid"].Value = recipeIDNow;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["date"].Value = DateTime.Now.ToString("yyyy/MM/dd");
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["time"].Value = DateTime.Now.ToString("HH:mm");
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["glassID"].Value = glassIDNow;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["sealUnit"].Value = sealUnitNow;
                        int c = 1;
                        foreach(string strTemp in listTemp)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["value" + c.ToString()].Value = strTemp;
                            c++;
                        }
                        x++;
                    }
                }
            }
            
        }
    }
}
