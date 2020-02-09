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
    public partial class frmUserID : Form
    {

        //Login Table用字典
        public static Dictionary<string, List<string>> loginTable = new Dictionary<string, List<string>>();

        public frmUserID()
        {
            InitializeComponent();
        }

        private void UpdateDataGridView()
        {
            //確認是否有搜尋到此資料的flag
            int checkFlag = 0;
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["userID"].Value.ToString() == textBoxUserID.Text)
                {
                    dataGridView1.Rows[x].Cells["userName"].Value = textBoxUserName.Text;
                    dataGridView1.Rows[x].Cells["password"].Value = textBoxPassWord.Text;
                    dataGridView1.Rows[x].Cells["level"].Value = comboBoxLevel.SelectedItem.ToString();
                    checkFlag++;
                }
            }
            if (checkFlag == 0)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["userID"].Value = textBoxUserID.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["userName"].Value = textBoxUserName.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["password"].Value = textBoxPassWord.Text;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells["level"].Value = comboBoxLevel.SelectedItem.ToString();
            }
        }

        private void DeleteDataGridView()
        {
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                if (dataGridView1.Rows[x].Cells["userID"].Value.ToString() == textBoxUserID.Text)
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[x]);
                }
            }
        }
        //dataGridView滑鼠點擊事件
        private void DataGridView1_CellMouseClick(Object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((DataGridView)sender == dataGridView1)
            {
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null && dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    textBoxUserID.Text = dataGridView1.Rows[e.RowIndex].Cells["userID"].Value.ToString();
                    textBoxUserName.Text = dataGridView1.Rows[e.RowIndex].Cells["userName"].Value.ToString();
                    textBoxPassWord.Text = dataGridView1.Rows[e.RowIndex].Cells["password"].Value.ToString();
                    if (dataGridView1.Rows[e.RowIndex].Cells["level"].Value != null && dataGridView1.Rows[e.RowIndex].Cells["level"].Value.ToString().Replace(" ", "") != "")
                    {
                        switch (dataGridView1.Rows[e.RowIndex].Cells["level"].Value.ToString())
                        {
                            case "ENG":
                                comboBoxLevel.SelectedIndex = 0;
                                break;
                            case "PM":
                                comboBoxLevel.SelectedIndex = 1;
                                break;
                            case "TA":
                                comboBoxLevel.SelectedIndex = 2;
                                break;
                        }
                    }
                }
            }
        }

        private void buttonSaveData_Click(object sender, EventArgs e)
        {
            //show Messageox詢問是否確定要儲存
            DialogResult result = MessageBox.Show("是否確定要儲存變更設定?", "確認視窗", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                SaveTable();
            }
        }

        //將字典寫入Table
        private void SaveTable()
        {
            FileStream fs = new FileStream(Path.Combine(Application.StartupPath, "PIUser.txt"), FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            for (int x = 0; x < dataGridView1.RowCount; x++)
            {
                sw.WriteLine(dataGridView1.Rows[x].Cells["userID"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["userName"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["passWord"].Value.ToString() + "," + dataGridView1.Rows[x].Cells["level"].Value.ToString());
            }

            sw.Close();
            fs.Close();
            MessageBox.Show("儲存完成");
            this.Close();
        }

        public void ReadTable()
        {
            dataGridView1.Rows.Clear();
            loginTable = frmMain.loginTable;
            foreach (var item in loginTable)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = item.Key;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = item.Value[0];
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = item.Value[1];
            
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = item.Value[2];
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
    }
}
