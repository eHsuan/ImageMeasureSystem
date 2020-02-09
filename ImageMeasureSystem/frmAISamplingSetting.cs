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
    public partial class frmAISamplingSetting : Form
    {

        //各產品使用的測項名稱字典
        public static Dictionary<string, string> pidProd = new Dictionary<string, string>();

        public frmAISamplingSetting()
        {
            InitializeComponent();
        }
        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            //pidProd = frmMain.pidProd;
            foreach (var item in pidProd)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = item.Key;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = item.Value;
            }
        }
        //將字典寫入Table
        private void SaveTable()
        {
            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "PID.txt"), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                sw.WriteLine(dataGridView1.Rows[x].Cells["pid"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["prodName"].Value.ToString());
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("儲存完成");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxPID.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("PID欄位為空白，請確認");
                return;
            }
            if (textBoxProdName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("產品名稱欄位為空白，請確認");
                return;
            }
            UpdateDataGridView();
        }

        private void UpdateDataGridView()
        {
            //確認是否有搜尋到此資料的flag
            int checkFlag = 0;
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["pid"].Value.ToString() == textBoxPID.Text)
                {
                    dataGridView1.Rows[x].Cells["prodName"].Value = textBoxProdName.Text;
                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["pid"].Value = textBoxPID.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = textBoxProdName.Text;
            }
        }

        private void DeleteDataGridView()
        {
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["pid"].Value.ToString() == textBoxPID.Text)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[x]);
                }
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBoxPID.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("PID欄位為空白，請確認");
                return;
            }
            if (textBoxProdName.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("產品名稱欄位為空白，請確認");
                return;
            }
            DeleteDataGridView();
        }

        //dataGridView滑鼠點擊事件
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((DataGridView)sender == dataGridView1 && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null && dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    textBoxPID.Text = dataGridView1.Rows[e.RowIndex].Cells["pid"].Value.ToString();
                    textBoxProdName.Text = dataGridView1.Rows[e.RowIndex].Cells["prodName"].Value.ToString();
                }

            }

        }
    }
}
