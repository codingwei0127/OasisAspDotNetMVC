using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class EleBaseLineM
    {
        //用電基準分析讀資料
        public ArrayList GetData()
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            string[] Time = { "2022-06" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //用電基準分析查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] ActualEle = new double[RowsNum];
            //double[] BaselineEle = new double[RowsNum];
            //double[] SaveEle = new double[RowsNum];
            //double[] SaveEleRate = new double[RowsNum];
            //string[] Performance = new string[RowsNum];
            //string[] TextColor = new string[RowsNum];
            //double TotalActualEle = 0, TotalBaselineEle = 0;
            string[] Time = { "2022-06-01", "2022-06-02", "2022-06-03", "2022-06-04", "2022-06-05", "2022-06-06", "2022-06-07", "2022-06-08", "2022-06-09", "2022-06-10", "2022-06-11", "2022-06-12", "2022-06-13", "2022-06-14", "2022-06-15", "2022-06-16", "2022-06-17", "2022-06-18", "2022-06-19", "2022-06-20", "2022-06-21", "2022-06-22", "2022-06-23", "2022-06-24", "2022-06-25", "2022-06-26", "2022-06-27", "2022-06-28", "2022-06-29", "2022-06-30" };
            double[] ActualEle = { 985.4, 958.17, 966.3, 360.7, 338.4, 914.76, 1121.5, 985.4, 958.17, 966.3, 360.7, 338.4, 914.76, 1121.5, 985.4, 958.17, 966.3, 360.7, 338.4, 914.76, 1121.5, 985.4, 958.17, 966.3, 360.7, 338.4, 914.76, 1121.5, 985.4, 958.17 };
            double[] BaselineEle = { 891.8, 883.06, 879.45, 337.9, 386.6, 917.2, 904.3, 891.8, 883.06, 879.45, 337.9, 386.6, 917.2, 904.3, 891.8, 883.06, 879.45, 337.9, 386.6, 917.2, 904.3, 891.8, 883.06, 879.45, 337.9, 386.6, 917.2, 904.3, 891.8, 883.06 };
            double[] SaveEle = { -93.6, -75.11, -86.85, -22.8, 48.2, 2.44, -217.2, -93.6, -75.11, -86.85, -22.8, 48.2, 2.44, -217.2, -93.6, -75.11, -86.85, -22.8, 48.2, 2.44, -217.2, -93.6, -75.11, -86.85, -22.8, 48.2, 2.44, -217.2, -93.6, -75.11 };
            double[] SaveEleRate = { -10.5, -8.51, -9.88, -6.75, 12.47, 0.27, -24.02, -10.5, -8.51, -9.88, -6.75, 12.47, 0.27, -24.02, -10.5, -8.51, -9.88, -6.75, 12.47, 0.27, -24.02, -10.5, -8.51, -9.88, -6.75, 12.47, 0.27, -24.02, -10.5, -8.51 };
            string[] Performance = { "全時段耗能", "平常日用電正常", "平常日用電正常", "假日用電正常", "假日節能", "平常日用電正常", "平常日用電異常", "全時段耗能", "平常日用電正常", "平常日用電正常", "假日用電正常", "假日節能", "平常日用電正常", "平常日用電異常", "全時段耗能", "平常日用電正常", "平常日用電正常", "假日用電正常", "假日節能", "平常日用電正常", "平常日用電異常", "全時段耗能", "平常日用電正常", "平常日用電正常", "假日用電正常", "假日節能", "平常日用電正常", "平常日用電異常", "全時段耗能", "平常日用電正常" };
            string[] TextColor = { "#EE964B", "black", "black", "black", "#32c944", "black", "red", "#EE964B", "black", "black", "black", "#32c944", "black", "red", "#EE964B", "black", "black", "black", "#32c944", "black", "red", "#EE964B", "black", "black", "black", "#32c944", "black", "red", "#EE964B", "black" };
            double TotalActualEle = 0, TotalBaselineEle = 0, TotalSaveEle = 0, TotalSaveEleRate = 0;
            for (int i = 0; i < ActualEle.Length; i++) TotalActualEle = TotalActualEle + ActualEle[i];
            for (int i = 0; i < BaselineEle.Length; i++) TotalBaselineEle = TotalBaselineEle + BaselineEle[i];
            TotalSaveEle = TotalBaselineEle - TotalActualEle;
            if (TotalBaselineEle != 0) TotalSaveEleRate = TotalSaveEle / TotalBaselineEle * 100;

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("ActualEle", ActualEle);
            temp_dict.Add("BaselineEle", BaselineEle);
            temp_dict.Add("SaveEle", SaveEle);
            temp_dict.Add("SaveEleRate", SaveEleRate);
            temp_dict.Add("Performance", Performance);
            temp_dict.Add("TextColor", TextColor);
            temp_dict.Add("TotalActualEle", TotalActualEle);
            temp_dict.Add("TotalBaselineEle", TotalBaselineEle);
            temp_dict.Add("TotalSaveEle", TotalSaveEle);
            temp_dict.Add("TotalSaveEleRate", TotalSaveEleRate);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}