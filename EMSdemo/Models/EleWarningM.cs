using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class EleWarningM
    {
        //用電預警查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] RealkWh = new double[RowsNum];
            //double[] BaseLinekWh = new double[RowsNum];
            //double[] Error = new double[RowsNum];
            //double[] ErrorRate = new double[RowsNum];
            //string[] Message = new string[RowsNum];
            //string[] TextColor = new string[RowsNum];
            string[] Time = { "2022-06-20", "2022-06-21", "2022-06-22", "2022-06-23" };
            double[] RealkWh = { 8804, 9265, 9177, 8598 };
            double[] BaseLinekWh = { 8702, 8247, 9171, 9641 };
            double[] Error = { -102, -1018, -6, 1043 };
            double[] ErrorRate = { -1.17, -12.34, -0.07, 10.82 };
            string[] Message = { "2022-06-20 用電狀況正常: 8804 (介於基準用電正負10%: -1.17 %)", "2022-06-21 用電狀況耗能: 9265 (高於基準用電10%: -12.34 %)", "2022-06-22 用電狀況正常: 9177 (介於基準用電正負10%: -0.07 %)", "2022-06-23 用電狀況節能: 8598 (低於基準用電10%: 10.82 %)" };
            string[] TextColor = { "black", "red", "black", "#32c944" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("RealkWh", RealkWh);
            temp_dict.Add("BaseLinekWh", BaseLinekWh);
            temp_dict.Add("Error", Error);
            temp_dict.Add("ErrorRate", ErrorRate);
            temp_dict.Add("Message", Message);
            temp_dict.Add("TextColor", TextColor);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}