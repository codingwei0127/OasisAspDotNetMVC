using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class EffWarningM
    {
        //性能預警查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] RealCOP = new double[RowsNum];
            //double[] BaseLineCOP = new double[RowsNum];
            //string[] Message = new string[RowsNum];
            string[] Time = { "2022-06-20", "2022-06-22" };
            double[] RealCOP = { 4.89, 3.27 };
            double[] BaseLineCOP = { 3.72, 3.84 };
            string[] Message = { "2022-06-20 CH1高於基準性能過多: 4.89 (31.45 %)", "2022-06-22 CH1低於基準性能過多: 3.27 (-14.84 %)" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("RealCOP", RealCOP);
            temp_dict.Add("BaseLineCOP", BaseLineCOP);
            temp_dict.Add("Message", Message);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}