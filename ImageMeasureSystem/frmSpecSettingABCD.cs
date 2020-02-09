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
    public partial class frmSpecSettingABCD : Form
    {
        //各產品使用的SPEC字典
        public static Dictionary<string, PIINSpec> specSetting = new Dictionary<string, PIINSpec>();
        public frmSpecSettingABCD()
        {
            InitializeComponent();
        }
        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            specSetting = frmMain.specSettingABCD;
            foreach (var item in specSetting)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = item.Key;

                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = item.Value.aOOSMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = item.Value.aOOSMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = item.Value.aOOCMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Value = item.Value.aOOCMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Value = item.Value.bOOSMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[6].Value = item.Value.bOOSMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Value = item.Value.bOOCMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[8].Value = item.Value.bOOCMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[9].Value = item.Value.cOOSMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[10].Value = item.Value.cOOSMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[11].Value = item.Value.cOOCMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[12].Value = item.Value.cOOCMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[13].Value = item.Value.dOOSMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[14].Value = item.Value.dOOSMin;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[15].Value = item.Value.dOOCMax;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[16].Value = item.Value.dOOCMin;
            }
        }
        //將字典寫入Table
        private void SaveTable()
        {

            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "SPEC_ABCD.txt"), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                string strTemp = "";
                for (int c = 0; c < 17; c++)
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
                    dataGridView1.Rows[x].Cells[1].Value = textBoxAOOSMax.Text;
                    dataGridView1.Rows[x].Cells[2].Value = textBoxAOOSMin.Text;
                    dataGridView1.Rows[x].Cells[3].Value = textBoxAOOCMax.Text;
                    dataGridView1.Rows[x].Cells[4].Value = textBoxAOOCMin.Text;

                    dataGridView1.Rows[x].Cells[5].Value = textBoxBOOSMax.Text;
                    dataGridView1.Rows[x].Cells[6].Value = textBoxBOOSMin.Text;
                    dataGridView1.Rows[x].Cells[7].Value = textBoxBOOCMax.Text;
                    dataGridView1.Rows[x].Cells[8].Value = textBoxBOOCMin.Text;

                    dataGridView1.Rows[x].Cells[9].Value = textBoxCOOSMax.Text;
                    dataGridView1.Rows[x].Cells[10].Value = textBoxCOOSMin.Text;
                    dataGridView1.Rows[x].Cells[11].Value = textBoxCOOCMax.Text;
                    dataGridView1.Rows[x].Cells[12].Value = textBoxCOOCMin.Text;

                    dataGridView1.Rows[x].Cells[13].Value = textBoxDOOSMax.Text;
                    dataGridView1.Rows[x].Cells[14].Value = textBoxDOOSMin.Text;
                    dataGridView1.Rows[x].Cells[15].Value = textBoxDOOCMax.Text;
                    dataGridView1.Rows[x].Cells[16].Value = textBoxDOOCMin.Text;

                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = textBoxProd.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = textBoxAOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = textBoxAOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = textBoxAOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Value = textBoxAOOCMin.Text;

                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Value = textBoxBOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[6].Value = textBoxBOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[7].Value = textBoxBOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[8].Value = textBoxBOOCMin.Text;

                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[9].Value = textBoxCOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[10].Value = textBoxCOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[11].Value = textBoxCOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[12].Value = textBoxCOOCMin.Text;

                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[13].Value = textBoxDOOSMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[14].Value = textBoxDOOSMin.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[15].Value = textBoxDOOCMax.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[16].Value = textBoxDOOCMin.Text;

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
                    textBoxAOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBoxAOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBoxAOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBoxAOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                    textBoxBOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    textBoxBOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBoxBOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBoxBOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

                    textBoxCOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    textBoxCOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                    textBoxCOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    textBoxCOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();

                    textBoxDOOSMax.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                    textBoxDOOSMin.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                    textBoxDOOCMax.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                    textBoxDOOCMin.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                }

            }

        }

    }
}
