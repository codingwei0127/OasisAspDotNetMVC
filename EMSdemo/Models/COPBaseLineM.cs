using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class COPBaseLineM
    {
        //空調性能分析讀資料
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

        //空調性能分析查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] ActualCOP = new double[RowsNum];
            //double[] BaselineCOP = new double[RowsNum];
            //double[] COPRate = new double[RowsNum];
            //string[] TextColor = new string[RowsNum];
            string[] Time = { "2022-06-01", "2022-06-02", "2022-06-03", "2022-06-04", "2022-06-05", "2022-06-06", "2022-06-07", "2022-06-08", "2022-06-09", "2022-06-10", "2022-06-11", "2022-06-12", "2022-06-13", "2022-06-14", "2022-06-15", "2022-06-16", "2022-06-17", "2022-06-18", "2022-06-19", "2022-06-20", "2022-06-21", "2022-06-22", "2022-06-23", "2022-06-24", "2022-06-25", "2022-06-26", "2022-06-27", "2022-06-28", "2022-06-29", "2022-06-30" };
            double[] ActualCOP = { 3.53, 3.98, 3.67, 0, 0, 4.89, 3.27, 3.53, 3.98, 3.67, 0, 0, 4.89, 3.27, 3.53, 3.98, 3.67, 0, 0, 4.89, 3.27, 3.53, 3.98, 3.67, 0, 0, 4.89, 3.27, 3.53, 3.98 };
            double[] BaselineCOP = { 3.67, 3.91, 3.74, 0, 0, 3.72, 3.84, 3.67, 3.91, 3.74, 0, 0, 3.72, 3.84, 3.67, 3.91, 3.74, 0, 0, 3.72, 3.84, 3.67, 3.91, 3.74, 0, 0, 3.72, 3.84, 3.67, 3.91 };
            double[] COPRate = { -3.8, 1.79, -1.87, 0, 0, 31.45, -14.84, -3.8, 1.79, -1.87, 0, 0, 31.45, -14.84, -3.8, 1.79, -1.87, 0, 0, 31.45, -14.84, -3.8, 1.79, -1.87, 0, 0, 31.45, -14.84, -3.8, 1.79 };
            string[] TextColor = { "black", "black", "black", "black", "black", "#32c944", "red", "black", "black", "black", "black", "black", "#32c944", "red", "black", "black", "black", "black", "black", "#32c944", "red", "black", "black", "black", "black", "black", "#32c944", "red", "black", "black" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("ActualCOP", ActualCOP);
            temp_dict.Add("BaselineCOP", BaselineCOP);
            temp_dict.Add("COPRate", COPRate);
            temp_dict.Add("TextColor", TextColor);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}