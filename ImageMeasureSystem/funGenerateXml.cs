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
using System.Xml;

namespace ImageMeasureSystem
{
    class funGenerateXml
    {
        //建立xml檔案METHOD
        public void GenerateXMLFile(string xmlFilePath, string[] inputSpecData, List<string[]> inputEdcData)
        {
            //inputSpecData : glass_id, group_id, lot_id, product_id, pfcd, eqp_id, sub_eqp_id, ec_code, route_no, route_version
            //, owner, recipe_id, operation, ope_id, ope_no, proc_id, rtc_flag, pnp, chamber, cassette_id, line_batch_id, split_id, cldate, cltime, mes_link_key
            //, rework_count, operator, reserve_field_1, reserve_field_2 

            //inputEdcData : {[Item_Name1, Item_Type1, Item_Value1], [Item_Name2, Item_Type2, Item_Value2]........}

            //初始化各節點名稱
            List<string> element = new List<string> { "glass_id", "group_id", "lot_id", "product_id", "pfcd", "eqp_id", "sub_eqp_id", "ec_code", "route_no", "route_version"
            , "owner", "recipe_id", "operation", "ope_id", "ope_no", "proc_id", "rtc_flag", "pnp", "chamber", "cassette_id", "line_batch_id", "split_id", "cldate", "cltime", "mes_link_key", "rework_count", "operator"
            , "reserve_field_1", "reserve_field_2", "item_name", "item_type", "item_value"};

            //初始化一個xml實例
            XmlDocument myXmlDoc = new XmlDocument();
            //創建xml版本宣告
            XmlNode node = myXmlDoc.CreateXmlDeclaration("1.0", null, null);
            myXmlDoc.AppendChild(node);
            //創建xml根節點
            XmlElement rootElement = myXmlDoc.CreateElement("EDC");
            //將根節點加入到xml文件中（AppendChild）
            myXmlDoc.AppendChild(rootElement);
            for (int i = 0; i < 29; i++)
            {
                //初始化第一層的子節點
                XmlElement secondLevelElement1 = myXmlDoc.CreateElement(element[i]);
                //寫入第一層子節點的值（SetAttribute）
                secondLevelElement1.InnerText = inputSpecData[i];
                //將第一層子節點加入到根節點中
                rootElement.AppendChild(secondLevelElement1);
            }
            //創建第一層第29個子節點
            XmlElement firstLevelElement129 = myXmlDoc.CreateElement("datas");
            rootElement.AppendChild(firstLevelElement129);
            //創建第二層第1個子節點
            XmlElement firstLevelElement21 = myXmlDoc.CreateElement("iary");
            firstLevelElement129.AppendChild(firstLevelElement21);
            //開始填入EDC ITEM內容
            foreach (string[] edcItem in inputEdcData)
            {
                //初始化第三層的子節點(EDC ITEM : Item_Name)
                XmlElement secondLevelElement31 = myXmlDoc.CreateElement(element[29]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement31.InnerText = edcItem[0];
                //將第三層子節點加入第二層節點中
                firstLevelElement21.AppendChild(secondLevelElement31);
                //初始化第三層的子節點(EDC ITEM : Item_Type)
                XmlElement secondLevelElement32 = myXmlDoc.CreateElement(element[30]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement32.InnerText = edcItem[1];
                //將第三層子節點加入第二層節點中
                firstLevelElement21.AppendChild(secondLevelElement32);
                //初始化第三層的子節點(EDC ITEM : Item_Vaule)
                XmlElement secondLevelElement33 = myXmlDoc.CreateElement(element[31]);
                //寫入第三層子節點的值(EDC Data)（SetAttribute）
                secondLevelElement33.InnerText = edcItem[2];
                //將第三層子節點加入第二層節點中
                firstLevelElement21.AppendChild(secondLevelElement33);

            }
            //将xml文件保存到指定的路径下
            string[] pathSplit = xmlFilePath.Split('\\');
            if (!Directory.Exists(Path.Combine(Application.StartupPath, "XML")))
            {
                try
                {
                    Directory.CreateDirectory(Path.Combine(Application.StartupPath, "XML"));
                }
                catch
                {

                }

            }
            string xmlFilePathLocal = Path.Combine(Application.StartupPath, "XML", pathSplit[pathSplit.Count() - 1]);
            //string xmlFilePathLocal = "D" + xmlFilePath.Substring(1);
            myXmlDoc.Save(xmlFilePathLocal);
            
            try
            {
                File.Copy(xmlFilePathLocal, xmlFilePath, true);
                
            }
            catch (Exception ex)
            {
                
            }


        }

    }
}
