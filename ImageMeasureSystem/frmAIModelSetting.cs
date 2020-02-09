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
using LCIN_AI;

namespace ImageMeasureSystem
{
    public partial class frmAIModelSetting : Form
    {

        //各產品使用的測項名稱字典
        public static Dictionary<string, ModelNum> modelNumLCIN = new Dictionary<string, ModelNum>();

        public frmAIModelSetting()
        {
            InitializeComponent();
        }
        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            //modelNumLCIN = frmMain.modelNumLCIN;
            foreach (var item in modelNumLCIN)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = item.Key;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["modelCorner"].Value = item.Value.Corner;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["modelNormal"].Value = item.Value.Normal;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["modelOverlap"].Value = item.Value.Overlap;
            }
        }
        //將字典寫入Table
        private void SaveTable()
        {
            using (FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "AIModel.txt"), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int x = 0; x < dataGridView1.RowCount; x++)
                    {
                        sw.WriteLine(dataGridView1.Rows[x].Cells["prodName"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["modelCorner"].Value.ToString()
                             + "," + dataGridView1.Rows[x].Cells["modelNormal"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["modelOverlap"].Value.ToString());

                    }
                }
                    
            }
                
            MessageBox.Show("儲存完成");
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxModelCorner.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Corner模型編號欄位為空白，請確認");
                return;
            }
            if (textBoxModelNormal.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Normal模型編號欄位為空白，請確認");
                return;
            }
            if (textBoxModelOverlap.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Overlap模型編號欄位為空白，請確認");
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
                if (dataGridView1.Rows[x].Cells["prodName"].Value.ToString() == textBoxProdName.Text)
                {
                    dataGridView1.Rows[x].Cells["modelCorner"].Value = textBoxModelCorner.Text;
                    dataGridView1.Rows[x].Cells["modelNormal"].Value = textBoxModelNormal.Text;
                    dataGridView1.Rows[x].Cells["modelOverlap"].Value = textBoxModelOverlap.Text;
                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = textBoxProdName.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["modelCorner"].Value = textBoxModelCorner.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["modelNormal"].Value = textBoxModelNormal.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["modelOverlap"].Value = textBoxModelOverlap.Text;
            }
        }

        private void DeleteDataGridView()
        {
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["prodName"].Value.ToString() == textBoxProdName.Text)
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
            if (textBoxModelCorner.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Corner模型編號欄位為空白，請確認");
                return;
            }
            if (textBoxModelNormal.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Normal模型編號欄位為空白，請確認");
                return;
            }
            if (textBoxModelOverlap.Text.Replace(" ", "") == "")
            {
                MessageBox.Show("Overlap模型編號欄位為空白，請確認");
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
                    
                    textBoxProdName.Text = dataGridView1.Rows[e.RowIndex].Cells["prodName"].Value.ToString();
                    textBoxModelCorner.Text = dataGridView1.Rows[e.RowIndex].Cells["modelCorner"].Value.ToString();
                    textBoxModelNormal.Text = dataGridView1.Rows[e.RowIndex].Cells["modelNormal"].Value.ToString();
                    textBoxModelOverlap.Text = dataGridView1.Rows[e.RowIndex].Cells["modelOverlap"].Value.ToString();
                }

            }

        }
    }
}
