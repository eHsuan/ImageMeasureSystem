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
    public partial class frmSpecSetting : Form
    {
        //各產品使用的SPEC字典
        public static Dictionary<string, string[]> specSetting = new Dictionary<string, string[]>();
        public frmSpecSetting()
        {
            InitializeComponent();
        }
        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            //specSetting = frmMain.specSetting;
            foreach (var item in specSetting)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = item.Key;
                string[] valueList = item.Value;
                for (int x = 0; x < valueList.Count(); x++)
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[x + 1].Value = valueList[x];
                }

            }
        }
        //將字典寫入Table
        private void SaveTable()
        {

            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "SPEC.txt"), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                string strTemp = "";
                for (int c = 0; c < 13; c++)
                {
                    if (dataGridView1.Rows[x].Cells[c].Value != null && dataGridView1.Rows[x].Cells[c].Value.ToString() != "")
                    {
                        if (strTemp == "")
                        {
                            strTemp = dataGridView1.Rows[x].Cells["prodName"].Value.ToString() + ",";
                        }
                        else if (c == 1)
                        {
                            strTemp = strTemp + dataGridView1.Rows[x].Cells[c].Value.ToString().Replace(" ", "");
                        }
                        else
                        {
                            strTemp = strTemp + "&" + dataGridView1.Rows[x].Cells[c].Value.ToString().Replace(" ", "");
                        }
                    }
                }
                sw.WriteLine(strTemp);
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("儲存完成");
            this.Close();
        }
        private void UpdateDataGridView()
        {
            //更改前檢查資訊是否合法
            if (textBoxProd.Text.Count() != 4 )
            {
                MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                return;
            }
            foreach (Control ctl in this.Controls)
            {
                if (ctl is TextBox)
                {
                    if(ctl.Text.Replace(" ", "") == "")
                    {
                        MessageBox.Show("輸入資料為空白，請確認");
                        return;
                    }
                    else
                    {
                        if (ctl.Name != "textBoxProd")
                        {
                            //檢查是否為合法的double
                            if (!CheckDouble(ctl.Text))
                            {
                                MessageBox.Show("輸入的規格格式錯誤，請確認", "Warning");
                                return ;
                            }
                         
                        }


                    }
                }
            }
            

            //確認是否有搜尋到此資料的flag
            int checkFlag = 0;
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["prodName"].Value.ToString() == textBoxProd.Text)
                {
                    dataGridView1.Rows[x].Cells[1].Value = textBoxNormalOOSMax.Text;
                    dataGridView1.Rows[x].Cells[2].Value = textBoxNormalOOSMin.Text;
                    dataGridView1.Rows[x].Cells[3].Value = textBoxNormalOOCMax.Text;
                    dataGridView1.Rows[x].Cells[4].Value = textBoxNormalOOCMin.Text;

                    dataGridView1.Rows[x].Cells[5].Value = textBoxOverlapOOSMax.Text;
                    dataGridView1.Rows[x].Cells[6].Value = textBoxOverlapOOSMin.Text;
                    dataGridView1.Rows[x].Cells[7].Value = textBoxOverlapOOCMax.Text;
                    dataGridView1.Rows[x].Cells[8].Value = textBoxOverlapOOCMin.Text;

                    dataGridView1.Rows[x].Cells[9].Value = textBoxCornerOOSMax.Text;
                    dataGridView1.Rows[x].Cells[10].Value = textBoxCornerOOSMin.Text;
                    dataGridView1.Rows[x].Cells[11].Value = textBoxCornerOOCMax.Text;
                    dataGridView1.Rows[x].Cells[12].Value = textBoxCornerOOCMin.Text;


                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = textBoxProd.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = textBoxNormalOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = textBoxNormalOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = textBoxNormalOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Value = textBoxNormalOOCMin.Text;

                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Value = textBoxOverlapOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[6].Value = textBoxOverlapOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Value = textBoxOverlapOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[8].Value = textBoxOverlapOOCMin.Text;

                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[9].Value = textBoxCornerOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[10].Value = textBoxCornerOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[11].Value = textBoxCornerOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[12].Value = textBoxCornerOOCMin.Text;

            }
        }

        //檢查是否為合法的Double
        private bool CheckDouble(string str)
        {
            try
            {
                Convert.ToDouble(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DeleteDataGridView()
        {
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["prodName"].Value.ToString() == textBoxProd.Text)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[x]);
                }
            }
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定要儲存變更設定?", "確認視窗", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                SaveTable();
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
        //dataGridView滑鼠點擊事件
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((DataGridView)sender == dataGridView1 && e.RowIndex >= 0)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null && dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    textBoxProd.Text = dataGridView1.Rows[e.RowIndex].Cells["prodName"].Value.ToString();
                    textBoxNormalOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBoxNormalOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBoxNormalOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBoxNormalOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                    textBoxOverlapOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBoxOverlapOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBoxOverlapOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBoxOverlapOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                    textBoxCornerOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBoxCornerOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    textBoxCornerOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    textBoxCornerOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                }

            }

        }

    }
}
