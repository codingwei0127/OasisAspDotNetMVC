using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class DPWarningM
    {
        //需量超約紀錄查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] BuildingDP = new double[RowsNum];
            //double[] ContractDP = new double[RowsNum];
            //string[] Message = new string[RowsNum];
            string[] Time = { "2022-06-20 10:45:00", "2022-06-21 11:15:00", "2022-06-22 11:30:00", "2022-06-23 12:45:00" };
            double[] BuildingDP = { 2669.7, 2640.77, 2644.877, 2611.617 };
            double[] ContractDP = { 2600, 2600, 2600, 2600 };
            string[] Message = { "2022-06-20 10:45:00 需量超約: 2669.7 (空調需量為1543.7)", "2022-06-21 11:15:00 需量超約: 2640.77 (空調需量為1571.7)", "2022-06-22 11:30:00 需量超約: 2644.877 (空調需量為1572.3)", "2022-06-23 12:45:00 需量超約: 2611.617 (空調需量為1549.3)" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("BuildingDP", BuildingDP);
            temp_dict.Add("ContractDP", ContractDP);
            temp_dict.Add("Message", Message);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}