using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageMeasureSystem
{
    class funSpecialFunction
    {
        //判斷兩組座標之間的距離為水平(false)或垂直(true)
        public bool CheckVerticalOrParallel(string mapX1, string mapY1, string mapX2, string mapY2)
        {
            if (Math.Abs(Convert.ToDouble(mapX1) - Convert.ToDouble(mapX2)) > Math.Abs(Convert.ToDouble(mapY1) - Convert.ToDouble(mapY2)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
