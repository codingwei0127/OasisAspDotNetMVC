using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class EleHistoryM
    {
        //用電歷史查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] kW = new double[RowsNum];
            //double[] GagekWh = new double[RowsNum];
            string[] Time = { "2022-06-20 00:00:00", "2022-06-20 00:03:00", "2022-06-20 00:06:00" };
            double[] kW = { 0, 0, 0 };
            double[] GagekWh = { 1000, 1000, 1000 };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("kW", kW);
            temp_dict.Add("GagekWh", GagekWh);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}