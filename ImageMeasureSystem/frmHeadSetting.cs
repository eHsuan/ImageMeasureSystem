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
    public partial class frmHeadSetting : Form
    {
        //各產品使用的測項名稱字典
        public static Dictionary<string, string> edcItem = new Dictionary<string, string>();
        public frmHeadSetting()
        {
            InitializeComponent();
        }
        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            //edcItem = frmMain.edcItem;
            foreach (var item in edcItem)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = item.Key;
                string[] valueList = item.Value.Split('&');
                for (int x = 0; x < valueList.Count(); x++)
                {
                    dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[x + 1].Value = valueList[x];
                }
                
            }
        }

        //將字典寫入Table
        private void SaveTable()
        {
            
            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "EDCItem.txt"), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                string strTemp = "";
                for (int c = 1; c < 11; c++)
                {
                    if (dataGridView1.Rows[x].Cells["head" + c.ToString()].Value != null && dataGridView1.Rows[x].Cells["head" + c.ToString()].Value.ToString() != "")
                    {
                        if (strTemp == "")
                        {
                            strTemp = dataGridView1.Rows[x].Cells["prodName"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["head" + c.ToString()].Value.ToString().Replace(" ", "");
                        }
                        else
                        {
                            strTemp = strTemp + "&" + dataGridView1.Rows[x].Cells["head" + c.ToString()].Value.ToString().Replace(" ", "");
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
            if (textBoxProd.Text.Count() != 4)
            {
                MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                return;
            }
            if (textBoxHead1.Text.Replace(" ", "") != "")
            {
                if (textBoxHead1.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead2.Text.Replace(" ", "") != "")
            {
                if (textBoxHead2.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead3.Text.Replace(" ", "") != "")
            {
                if (textBoxHead3.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead4.Text.Replace(" ", "") != "")
            {
                if (textBoxHead4.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead5.Text.Replace(" ", "") != "")
            {
                if (textBoxHead5.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead6.Text.Replace(" ", "") != "")
            {
                if (textBoxHead6.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead7.Text.Replace(" ", "") != "")
            {
                if (textBoxHead7.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead8.Text.Replace(" ", "") != "")
            {
                if (textBoxHead8.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead9.Text.Replace(" ", "") != "")
            {
                if (textBoxHead9.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }
            if (textBoxHead10.Text.Replace(" ", "") != "")
            {
                if (textBoxHead10.Text.Count() != 2)
                {
                    MessageBox.Show("輸入資料格式錯誤，請確認", "Warning");
                    return;
                }
            }

            //確認是否有搜尋到此資料的flag
            int checkFlag = 0;
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["prodName"].Value.ToString() == textBoxProd.Text)
                {
                    dataGridView1.Rows[x].Cells["head1"].Value = textBoxHead1.Text;
                    dataGridView1.Rows[x].Cells["head2"].Value = textBoxHead2.Text;
                    dataGridView1.Rows[x].Cells["head3"].Value = textBoxHead3.Text;
                    dataGridView1.Rows[x].Cells["head4"].Value = textBoxHead4.Text;
                    dataGridView1.Rows[x].Cells["head5"].Value = textBoxHead5.Text;
                    dataGridView1.Rows[x].Cells["head6"].Value = textBoxHead6.Text;
                    dataGridView1.Rows[x].Cells["head7"].Value = textBoxHead7.Text;
                    dataGridView1.Rows[x].Cells["head8"].Value = textBoxHead8.Text;
                    dataGridView1.Rows[x].Cells["head9"].Value = textBoxHead9.Text;
                    dataGridView1.Rows[x].Cells["head10"].Value = textBoxHead10.Text;
                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["prodName"].Value = textBoxProd.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head1"].Value = textBoxHead1.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head2"].Value = textBoxHead2.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head3"].Value = textBoxHead3.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head4"].Value = textBoxHead4.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head5"].Value = textBoxHead5.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head6"].Value = textBoxHead6.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head7"].Value = textBoxHead7.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head8"].Value = textBoxHead8.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head9"].Value = textBoxHead9.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["head10"].Value = textBoxHead10.Text;
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

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDataGridView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteDataGridView();
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
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
                    //先清空textbox
                    foreach(Control ctrl in Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ctrl.Text = "";
                        }
                    }
                    textBoxProd.Text = dataGridView1.Rows[e.RowIndex].Cells["prodName"].Value.ToString();
                    for (int x = 1; x < 11; x++)
                    {
                        if (dataGridView1.Rows[e.RowIndex].Cells["head" + x.ToString()].Value != null && dataGridView1.Rows[e.RowIndex].Cells["head" + x.ToString()].Value.ToString().Replace(" ", "") != "")
                        {
                            ((TextBox)Controls["textBoxHead" + x.ToString()]).Text = dataGridView1.Rows[e.RowIndex].Cells["head" + x.ToString()].Value.ToString();
                        }
                        
                    }
                    
                 
                }

            }

        }
    }
}
