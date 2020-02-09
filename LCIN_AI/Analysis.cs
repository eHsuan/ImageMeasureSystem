using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LCIN_AI
{
    public class FinalFilterAIData
    {
        public Dictionary<string, FilterAIData> finalFilterAIData { get; set; }
        
    }
    public class FilterAIData   //篩選完後每張圖的Seal邊界資料
    {
        public bool BeChangedFlag { get; set; } = false;
        public string isHorizontal { get; set; }
        public SealEdge edgeH1 { get; set; }
        public SealEdge edgeH2 { get; set; }
        public SealEdge edgeV1 { get; set; }
        public SealEdge edgeV2 { get; set; }
    }
    public class SealEdge
    {
        public int adressX { get; set; }
        public int adressY { get; set; }

    }
    public class DefectData //每個圖檔中的個別AI判定資料
    {
        public string _code { get; set; }
        public int value { get; set; }
        public int mapX1 { get; set; }
        public int mapY1 { get; set; }
        public int mapX2 { get; set; }
        public int mapY2 { get; set; }
    }
    public class AIData //每個圖檔名稱的全部AI判定資料
    {
        public string imagePath { get; set; }
        public List<DefectData> _data { get; set; }
    }
    
    public class Analysis
    {
        public FinalFilterAIData FilterSealEdge(ImageType type, string AIFilePath, int imageSizeX, int imageSizeY, double zoomRateX, double zoomRateY, string aiDefectCodeType)
        {
            //分析整理AI Output
            Dictionary<string, AIData> aiDataDict = ReadAIFile(AIFilePath);
            FinalFilterAIData _rtn = new FinalFilterAIData { };
            
            switch(type)
            {
                case ImageType.Corner:
                    _rtn = AnalysisAIDataCorner(ImageType.Corner, aiDataDict, imageSizeX, imageSizeY, zoomRateX, zoomRateY, aiDefectCodeType);
                    break;
                case ImageType.Normal:
                    _rtn = AnalysisAIDataNormal(aiDataDict, imageSizeX, imageSizeY, zoomRateX, zoomRateY, aiDefectCodeType);
                    break;
                case ImageType.Overlap:
                    _rtn = AnalysisAIDataOverlap(aiDataDict, imageSizeX, imageSizeY, zoomRateX, zoomRateY, aiDefectCodeType);
                    break;
            }
            return _rtn;
        }
        private Dictionary<string, AIData> ReadAIFile(string AIFilePath)
        {
            
            Dictionary<string, AIData> _rtn = new Dictionary<string, AIData> { };
            
            StreamReader sr = new StreamReader(AIFilePath);
            while(true)
            {
                List<DefectData> _dataList = new List<DefectData> { };
                string _readData = sr.ReadLine();
                if (_readData == null || _readData == "")
                {
                    break;
                }
                else if (_readData == "#")
                {
                    continue;
                }

                string[] splitTemp = _readData.Split(',');
                for (int x = 0; x < (splitTemp.Count() - 3) / 6; x++)
                {
                    _dataList.Add(new DefectData
                    {
                        _code = splitTemp[x * 6 + 3],
                        value = int.Parse(splitTemp[x * 6 + 4].Replace("%", "")),
                        mapX1 = int.Parse(splitTemp[x * 6 + 5]), 
                        mapY1 = int.Parse(splitTemp[x * 6 + 7]), 
                        mapX2 = int.Parse(splitTemp[x * 6 + 6]), 
                        mapY2 = int.Parse(splitTemp[x * 6 + 8])});
                }
                _rtn.Add(splitTemp[0], new AIData { imagePath = splitTemp[0], _data = _dataList });
            }
            return _rtn;
        }

        private FinalFilterAIData AnalysisAIDataCorner(ImageType type, Dictionary<string, AIData> aiData, int imageSizeX, int imageSizeY, double zoomRateX, double zoomRateY, string aiDefectCodeType)
        {
            Dictionary<string, FilterAIData> rtn = new Dictionary<string, FilterAIData> { };
            
            SealEdge mapCenter = new SealEdge { adressX = Convert.ToInt16((imageSizeX / 2) / zoomRateX), adressY = Convert.ToInt16((imageSizeY / 2) / zoomRateY) };
            //迴圈篩選AI座標

            foreach (KeyValuePair<string, AIData> aIDataTemp in aiData)
            {
                DefectData filterDataH1 = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 1 };
                DefectData filterDataH2 = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 0 };
                DefectData filterDataV1 = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 1 };
                DefectData filterDataV2 = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 0 };
                foreach (DefectData defectDataTemp in aIDataTemp.Value._data)
                {
                    if (aiDefectCodeType == "1")    //AI DefectCode為多種
                    {
                        //比較各DefectCode取最高分，資料存放 : CH00 = filterDataH1, CH01 = filterDataH2, CV00 = filterDataV1, CV01 = filterDataV2
                        switch (defectDataTemp._code)
                        {
                            case "CH00":
                                filterDataH1 = defectDataTemp.value > filterDataH1.value ? defectDataTemp : filterDataH1;
                                break;
                            case "CH01":
                                filterDataH2 = defectDataTemp.value > filterDataH2.value ? defectDataTemp : filterDataH2;
                                break;
                            case "CV00":
                                filterDataV1 = defectDataTemp.value > filterDataV1.value ? defectDataTemp : filterDataV1;
                                break;
                            case "CV01":
                                filterDataV2 = defectDataTemp.value > filterDataV2.value ? defectDataTemp : filterDataV2;
                                break;
                        }
                    }
                    else
                    {
                        //AI DefectCode為單一種
                        //水平CH00
                        if (defectDataTemp._code == "CH00")
                        {
                            if (filterDataH1.value > filterDataH2.value)
                            {
                                filterDataH2 = defectDataTemp.value > filterDataH2.value ? defectDataTemp : filterDataH2;
                            }
                            else
                            {
                                filterDataH1 = defectDataTemp.value > filterDataH1.value ? defectDataTemp : filterDataH1;
                            }
                        }
                        else if (defectDataTemp._code == "CV00")
                        {
                            if (filterDataV1.value > filterDataV2.value)
                            {
                                filterDataV2 = defectDataTemp.value > filterDataV2.value ? defectDataTemp : filterDataV2;
                            }
                            else
                            {
                                filterDataV1 = defectDataTemp.value > filterDataV1.value ? defectDataTemp : filterDataV1;
                            }
                        }
                    }



                }
                if (!rtn.ContainsKey(aIDataTemp.Value.imagePath))
                    rtn.Add(aIDataTemp.Value.imagePath, new FilterAIData
                    {
                        edgeH1 = new SealEdge { adressX = Convert.ToInt16(((filterDataH1.mapX1 + filterDataH1.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataH1.mapY1 + filterDataH1.mapY2) / 2) * zoomRateY) },
                        edgeH2 = new SealEdge { adressX = Convert.ToInt16(((filterDataH2.mapX1 + filterDataH2.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataH2.mapY1 + filterDataH2.mapY2) / 2) * zoomRateY) },
                        edgeV1 = new SealEdge { adressX = Convert.ToInt16(((filterDataV1.mapX1 + filterDataV1.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataV1.mapY1 + filterDataV1.mapY2) / 2) * zoomRateY) },
                        edgeV2 = new SealEdge { adressX = Convert.ToInt16(((filterDataV2.mapX1 + filterDataV2.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataV2.mapY1 + filterDataV2.mapY2) / 2) * zoomRateY) }
                    });
                
            }
            return new FinalFilterAIData { finalFilterAIData = rtn };


        }
        private FinalFilterAIData AnalysisAIDataNormal(Dictionary<string, AIData> aiData, int imageSizeX, int imageSizeY, double zoomRateX, double zoomRateY, string aiDefectCodeType)
        {
            Dictionary<string, FilterAIData> rtn = new Dictionary<string, FilterAIData> { };
            string state = "";
            SealEdge mapCenter = new SealEdge { adressX = Convert.ToInt16((imageSizeX / 2) / zoomRateX), adressY = Convert.ToInt16((imageSizeY / 2) / zoomRateY) };
            //迴圈篩選AI座標

            foreach (KeyValuePair<string, AIData> aIDataTemp in aiData)
            {
                DefectData filterDataL = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 1 };
                DefectData filterDataR = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 0 };
                
                foreach (DefectData defectDataTemp in aIDataTemp.Value._data)
                {
                    if (aiDefectCodeType == "1")    //AI DefectCode為多種
                    {
                        //比較各DefectCode取最高分，資料存放 : NH00 NV00 = filterDataL, NH01 NV01 = filterDataR
                        switch (defectDataTemp._code)
                        {
                            case "NV00":
                                filterDataL = defectDataTemp.value > filterDataL.value ? defectDataTemp : filterDataL;
                                state = "H";    //AI偵測對象是V時代表要量測的距離方向是H的
                                break;
                            case "NH00":
                                filterDataL = defectDataTemp.value > filterDataL.value ? defectDataTemp : filterDataL;
                                state = "V";    //AI偵測對象是H時代表要量測的距離方向是V的
                                break;
                            case "NV01":
                                filterDataR = defectDataTemp.value > filterDataR.value ? defectDataTemp : filterDataR;
                                state = "H";
                                break;
                            case "NH01":
                                filterDataR = defectDataTemp.value > filterDataR.value ? defectDataTemp : filterDataR;
                                state = "V";
                                break;
                        }
                    }
                    else
                    {
                        //AI DefectCode為單一種
                        if (defectDataTemp.value > filterDataL.value)
                        {
                            filterDataL = defectDataTemp;
                        }
                        else if (defectDataTemp.value > filterDataR.value)
                        {
                            filterDataR = defectDataTemp;
                        }

                        if (defectDataTemp._code == "NV00") //AI偵測對象是V時代表要量測的距離方向是H的
                        {
                            state = "H";
                        }
                        else
                        {
                            state = "V";
                        }
                    }


                }
                if (!rtn.ContainsKey(aIDataTemp.Value.imagePath))
                    rtn.Add(aIDataTemp.Value.imagePath, new FilterAIData
                    {
                        edgeV1 = new SealEdge { adressX = Convert.ToInt16(((filterDataL.mapX1 + filterDataL.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataL.mapY1 + filterDataL.mapY2) / 2) * zoomRateY) },
                        edgeV2 = new SealEdge { adressX = Convert.ToInt16(((filterDataR.mapX1 + filterDataR.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataR.mapY1 + filterDataR.mapY2) / 2) * zoomRateY) },
                        isHorizontal = state
                    });
            }
            return new FinalFilterAIData { finalFilterAIData = rtn, };
        }
        private FinalFilterAIData AnalysisAIDataOverlap(Dictionary<string, AIData> aiData, int imageSizeX, int imageSizeY, double zoomRateX, double zoomRateY, string aiDefectCodeType)
        {
            Dictionary<string, FilterAIData> rtn = new Dictionary<string, FilterAIData> { };
            string state = "";
            SealEdge mapCenter = new SealEdge { adressX = Convert.ToInt16((imageSizeX / 2) / zoomRateX), adressY = Convert.ToInt16((imageSizeY / 2) / zoomRateY) };
            //迴圈篩選AI座標

            foreach (KeyValuePair<string, AIData> aIDataTemp in aiData)
            {
                DefectData filterDataL = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 1 };
                DefectData filterDataR = new DefectData { mapX1 = 0, mapY1 = 0, mapX2 = 0, mapY2 = 0, value = 0 };
                foreach (DefectData defectDataTemp in aIDataTemp.Value._data)
                {
                    if (aiDefectCodeType == "1")    //AI DefectCode為多種
                    {
                        //比較各DefectCode取最高分，資料存放 : OH00 OV00 = filterDataL, OH01 OV01 = filterDataR
                        switch (defectDataTemp._code)
                        {
                            case "OV00":
                                filterDataL = defectDataTemp.value > filterDataL.value ? defectDataTemp : filterDataL;
                                state = "H";    //AI偵測對象是V時代表要量測的距離方向是H的
                                break;
                            case "OH00":
                                filterDataL = defectDataTemp.value > filterDataL.value ? defectDataTemp : filterDataL;
                                state = "V";    //AI偵測對象是H時代表要量測的距離方向是V的
                                break;
                            case "OV01":
                                filterDataR = defectDataTemp.value > filterDataR.value ? defectDataTemp : filterDataR;
                                state = "H";
                                break;
                            case "OH01":
                                filterDataR = defectDataTemp.value > filterDataR.value ? defectDataTemp : filterDataR;
                                state = "V";
                                break;
                        }
                    }
                    else
                    {
                        //AI DefectCode為單一種
                        if (defectDataTemp.value > filterDataL.value)
                        {
                            filterDataL = defectDataTemp;
                        }
                        else if (defectDataTemp.value > filterDataR.value)
                        {
                            filterDataR = defectDataTemp;
                        }
                        if (defectDataTemp._code == "OV00") //AI偵測對象是V時代表要量測的距離方向是H的
                        {
                            state = "H";
                        }
                        else
                        {
                            state = "V";
                        }
                    }

                }
                if (!rtn.ContainsKey(aIDataTemp.Value.imagePath))
                    rtn.Add(aIDataTemp.Value.imagePath, new FilterAIData
                    {
                        edgeV1 = new SealEdge { adressX = Convert.ToInt16(((filterDataL.mapX1 + filterDataL.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataL.mapY1 + filterDataL.mapY2) / 2) * zoomRateY) },
                        edgeV2 = new SealEdge { adressX = Convert.ToInt16(((filterDataR.mapX1 + filterDataR.mapX2) / 2) * zoomRateX), adressY = Convert.ToInt16(((filterDataR.mapY1 + filterDataR.mapY2) / 2) * zoomRateY) },
                        isHorizontal = state
                    });
                
            }
            return new FinalFilterAIData { finalFilterAIData = rtn };
        }
        private int CheckQuadrant(SealEdge center, SealEdge map)    //檢查座標在哪個象限
        {
            return map.adressX > center.adressX ? (map.adressY > center.adressY ? 1 : 2) : (map.adressY > center.adressY ? 4 : 3);
        }
       
    }
}
