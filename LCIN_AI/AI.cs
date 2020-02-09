using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace LCIN_AI
{
    public class AI
    {
        Analysis alss = new Analysis();
        public void AIFunction(string glassID, string lineID)
        {

        }
        public void GenerateAIFile(WaitMeasureID waitMeasureID, _ImageList images, string aiServer, ModelNum modelNumLCIN)
        {
            if (waitMeasureID.glassID != "Error")
            {
                //宣告變數

                string writeReady = null;
                if (!Directory.Exists(Path.Combine("Input")))
                    Directory.CreateDirectory(Path.Combine("Input"));

                string aiFilePathCorner = Path.Combine(aiServer, "Input", waitMeasureID.dateTime + "_" + "2299" + "_XXXXXXX_" + waitMeasureID.glassID.Substring(0, 10) + ".txt");
                string aiFilePathNormal = Path.Combine(aiServer, "Input", waitMeasureID.dateTime + "_" + "2298" + "_XXXXXXX_" + waitMeasureID.glassID.Substring(0, 10) + ".txt");
                string aiFilePathOverlap = Path.Combine(aiServer, "Input", waitMeasureID.dateTime + "_" + "2297" + "_XXXXXXX_" + waitMeasureID.glassID.Substring(0, 10) + ".txt");

                string aiFileLocalPathCorner = Path.Combine("Input", waitMeasureID.dateTime + "_" + "2299" + "_XXXXXXX_" + waitMeasureID.glassID.Substring(0, 10) + ".txt");
                string aiFileLocalPathNormal = Path.Combine("Input", waitMeasureID.dateTime + "_" + "2298" + "_XXXXXXX_" + waitMeasureID.glassID.Substring(0, 10) + ".txt");
                string aiFileLocalPathOverlap = Path.Combine("Input", waitMeasureID.dateTime + "_" + "2297" + "_XXXXXXX_" + waitMeasureID.glassID.Substring(0, 10) + ".txt");
                //開始組合Corner AI File 內容
                if (images.imageNameListCorner.Count != 0)
                {
                    writeReady = null;
                    writeReady = writeReady + "#LCD4" + "\r\n"; //填寫廠別
                    writeReady = writeReady + "#" + modelNumLCIN.Corner + "\r\n"; //填寫模型編號

                    //填寫圖檔路徑
                    int x = 1;
                    foreach (string tempStr in images.imageNameListCorner)
                    {
                        writeReady = writeReady + tempStr + "," + waitMeasureID.prod + "," + "_" + x.ToString() + "\r\n";
                        x++;
                    }
                    using (StreamWriter wrtieData = new StreamWriter(aiFileLocalPathCorner))
                    {
                        try
                        {
                            wrtieData.WriteLine(writeReady);
                            wrtieData.Close();

                        }
                        catch
                        {

                        }
                    }
                    //上傳AI File
                    try
                    {
                        File.Copy(aiFileLocalPathCorner, aiFilePathCorner, true);
                    }
                    catch
                    {

                    }
                }
                //開始組合Normal AI File 內容
                if (images.imageNameListNormal.Count != 0)
                {
                    writeReady = null;
                    writeReady = writeReady + "#LCD4" + "\r\n"; //填寫廠別
                    writeReady = writeReady + "#" + modelNumLCIN.Normal + "\r\n"; //填寫模型編號

                    //填寫圖檔路徑
                    int x = 1;
                    foreach (string tempStr in images.imageNameListNormal)
                    {
                        writeReady = writeReady + tempStr + "," + waitMeasureID.prod + "," + "_" + x.ToString() + "\r\n";
                        x++;
                    }
                    using (StreamWriter wrtieData = new StreamWriter(aiFileLocalPathNormal))
                    {
                        try
                        {
                            wrtieData.WriteLine(writeReady);
                            wrtieData.Close();

                        }
                        catch
                        {

                        }
                    }
                    //上傳AI File
                    try
                    {
                        File.Copy(aiFileLocalPathNormal, aiFilePathNormal, true);
                    }
                    catch
                    {

                    }
                }
                //開始組合Overlap AI File 內容
                if (images.imageNameListOverlap.Count != 0)
                {
                    writeReady = null;
                    writeReady = writeReady + "#LCD4" + "\r\n"; //填寫廠別
                    writeReady = writeReady + "#" + modelNumLCIN.Overlap + "\r\n"; //填寫模型編號

                    //填寫圖檔路徑
                    int x = 1;
                    foreach (string tempStr in images.imageNameListOverlap)
                    {
                        writeReady = writeReady + tempStr + "," + waitMeasureID.prod + "," + "_" + x.ToString() + "\r\n";
                        x++;
                    }
                    using (StreamWriter wrtieData = new StreamWriter(aiFileLocalPathOverlap))
                    {
                        try
                        {
                            wrtieData.WriteLine(writeReady);
                            wrtieData.Close();

                        }
                        catch
                        {

                        }
                    }
                    //上傳AI File
                    try
                    {
                        File.Copy(aiFileLocalPathOverlap, aiFilePathOverlap, true);
                    }
                    catch
                    {

                    }
                }
            }

        }
        public FinalFilterAIData ReadAIData(string aiFileDir, WaitMeasureID waitMeasureID, int imageSizeX, int imageSizeY, ImageType type, double zoomRateX, double zoomRateY, string aiDefectCodeType)
        {
            switch (type)
            {
                case ImageType.Corner:
                    string cornerPath = Path.Combine(aiFileDir, waitMeasureID.dateTime + "_" + "2299" + "_" + "XXXXXXX" + "_" + waitMeasureID.glassID.Substring(0, 10) + "_RES.txt");
                    return alss.FilterSealEdge(ImageType.Corner, cornerPath, imageSizeX, imageSizeY, zoomRateX, zoomRateY, aiDefectCodeType);
                case ImageType.Normal:
                    string normalPath = Path.Combine(aiFileDir, waitMeasureID.dateTime + "_" + "2298" + "_" + "XXXXXXX" + "_" + waitMeasureID.glassID.Substring(0, 10) + "_RES.txt");
                    return alss.FilterSealEdge(ImageType.Normal, normalPath, imageSizeX, imageSizeY, zoomRateX, zoomRateY, aiDefectCodeType);
                case ImageType.Overlap:
                    string overlapPath = Path.Combine(aiFileDir, waitMeasureID.dateTime + "_" + "2297" + "_" + "XXXXXXX" + "_" + waitMeasureID.glassID.Substring(0, 10) + "_RES.txt");
                    return alss.FilterSealEdge(ImageType.Overlap, overlapPath, imageSizeX, imageSizeY, zoomRateX, zoomRateY, aiDefectCodeType);
                default:
                    return new FinalFilterAIData { };
            }
            //站點=>2299 : Corner , 2298 : Normal, 2297 : Overlap

        }
    }
    public class ModelNum
    {
        public string Corner { get; set; }
        public string Normal { get; set; }
        public string Overlap { get; set; }
    }
    public class _ImageList
    {
        public List<string> imageNameListABCD { get; set; }
        public List<string> imageNameListCorner { get; set; }
        public List<string> imageNameListNormal { get; set; }
        public List<string> imageNameListOverlap { get; set; }
    }
    public class WaitMeasureID
    {
        public string glassID { get; set; }
        public string lineID { get; set; }
        public bool aiDataExistFlag { get; set; }   //識別是否有AI Output檔案
        public bool aiJudgedCornerFlag { get; set; }
        public bool aiJudgedNormalFlag { get; set; }
        public bool aiJudgedOverlapFlag { get; set; }
        public string prod { get; set; }
        public string pid { get; set; }
        public List<string> imageNameListABCD { get; set; }
        public List<string> imageNameListCorner { get; set; }
        public List<string> imageNameListNormal { get; set; }
        public List<string> imageNameListOverlap { get; set; }
        public string dateTime { get; set; }
        public bool aiInputSendedFlag { get; set; } //識別是否已發送Input給AI
        public bool measuringFlag { get; set; }
        public string measureByAIorTA { get; set; } = "AI"; //標示此ID當前是屬於AI還是TA量測權責
        public string aiDefectCodeType { get; set; }    //標示此ID的AI DefectCode是單一種還是多種
    }
  
}
