using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;
using System.Diagnostics;


namespace ImageMeasureSystem
{
    public partial class frmRateSetting : Form
    {

        //ADMRate用字典
        private Dictionary<string, string> admRate = new Dictionary<string, string>();

        public frmRateSetting()
        {
            InitializeComponent();
        }

        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            admRate = frmMain.admRate;
            foreach (var item in admRate)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = item.Key;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = item.Value;
            }
        }
        //將字典寫入Table
        private void SaveTable()
        {
            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "ADMRateTable.txt"), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                sw.WriteLine(dataGridView1.Rows[x].Cells["LineID"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["Ratio"].Value.ToString());
            }
           
            sw.Close();
            fs.Close();
            MessageBox.Show("儲存完成");
            this.Close();
        }
        

        private void buttonJudgeOK_Click(object sender, EventArgs e)
        {
            //show Messageox詢問是否確定要儲存
            DialogResult result = MessageBox.Show("是否確定要儲存變更設定?", "確認視窗", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                SaveTable();
            }
            
        }
        //dataGridView滑鼠點擊事件
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((DataGridView)sender == dataGridView1 && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null && dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    textBoxLineID.Text = dataGridView1.Rows[e.RowIndex].Cells["LineID"].Value.ToString();
                    textBoxRate.Text = dataGridView1.Rows[e.RowIndex].Cells["Ratio"].Value.ToString();
                }
                    
            }
                
        }
        private void UpdateDataGridView()
        {
            //確認是否有搜尋到此資料的flag
            int checkFlag = 0;
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["LineID"].Value.ToString() == textBoxLineID.Text)
                {
                    dataGridView1.Rows[x].Cells["Ratio"].Value = textBoxRate.Text;
                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["LineID"].Value = textBoxLineID.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["Ratio"].Value = textBoxRate.Text;
            }
        }

        private void DeleteDataGridView()
        {
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["LineID"].Value.ToString() == textBoxLineID.Text)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[x]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteDataGridView();
        }

        private void frmRateSetting_Load(object sender, EventArgs e)
        {

        }
    }
}
