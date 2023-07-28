using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class PredictLoadM
    {
        //負載預測讀資料
        public ArrayList GetData()
        {
            DateTime localDate = DateTime.Today;
            string Today = string.Format("{0:yyyy-MM-dd}", localDate);

            //Read SQL


            //Declare Parameters
            //string[] Time = new string[4];
            //double[] Tdb = new double[4];
            //double[] RH = new double[4];
            //double[] Load = new double[4];
            string[] Time = { "2022-06-20 07:00", "2022-06-20 10:00", "2022-06-20 13:00", "2022-06-20 16:00" };
            double[] Tdb = { 16.76, 19.9, 21.4, 20.17 };
            double[] RH = { 78.34, 75.8, 73.46, 72.26 };
            double[] Load = { 800, 850, 900, 850 };

            double maxValue = Load.Max();
            int maxIndex = Load.ToList().IndexOf(maxValue);
            string maxTimeWhole = Time[maxIndex];
            string[] maxTimeArray = maxTimeWhole.Split(' ');
            string maxTime = maxTimeArray[1];
            double maxTdb = Tdb[maxIndex];
            double maxRH = RH[maxIndex];
            double TotalValue = 0, avgValue = 0;
            for (int i = 0; i < 4; i++) TotalValue = TotalValue + Load[i];
            avgValue = TotalValue / 4;

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            temp_dict.Add("Load", Load);
            temp_dict.Add("avgValue", avgValue);
            temp_dict.Add("maxValue", maxValue);
            temp_dict.Add("maxTime", maxTime);
            temp_dict.Add("maxTdb", maxTdb);
            temp_dict.Add("maxRH", maxRH);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}