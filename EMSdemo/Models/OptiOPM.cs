using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class OptiOPM
    {
        //佳化開機策略讀資料
        public ArrayList GetData()
        {
            DateTime localDate = DateTime.Today;
            string Today = string.Format("{0:yyyy-MM-dd}", localDate);

            //Read SQL


            //Declare Parameters
            double AvgLoad = 0, PeakLoad = 0, PeakTdb = 0, PeakRH = 0, BestKWRT = 0;
            string PeakTime = "";
            //int OptRowsNum = 6;
            //string[] BestOpt = new string[4];
            //string[] CH1opt = new string[OptRowsNum];
            //string[] CH2opt = new string[OptRowsNum];
            //string[] CH3opt = new string[OptRowsNum];
            //string[] CHSPopt = new string[OptRowsNum];
            //double[] KWRT = new double[OptRowsNum];
            AvgLoad = 860;
            PeakLoad = 900;
            PeakTime = "13:00:00";
            PeakTdb = 21.4;
            PeakRH = 73.46;
            BestKWRT = 0.9;
            string[] BestOpt = { "開機", "開機", "關機", "關機" };
            string[] CH1opt = { "開機", "關機" };
            string[] CH2opt = { "開機", "關機" };
            string[] CH3opt = { "關機", "開機" };
            string[] CHSPopt = { "關機", "關機" };
            double[] KWRT = { 0.9, 0.95 };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("AvgLoad", AvgLoad);
            temp_dict.Add("PeakLoad", PeakLoad);
            temp_dict.Add("PeakTime", PeakTime);
            temp_dict.Add("PeakTdb", PeakTdb);
            temp_dict.Add("PeakRH", PeakRH);
            temp_dict.Add("BestOpt", BestOpt);
            temp_dict.Add("BestKWRT", BestKWRT);
            temp_dict.Add("CH1opt", CH1opt);
            temp_dict.Add("CH2opt", CH2opt);
            temp_dict.Add("CH3opt", CH3opt);
            temp_dict.Add("CHSPopt", CHSPopt);
            temp_dict.Add("KWRT", KWRT);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}