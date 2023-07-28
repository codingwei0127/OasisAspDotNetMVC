using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class SwitchBoardM
    {
        //電盤資訊變更選擇週期
        public ArrayList ChangePeriod(Dictionary<string, string> data)
        {
            //Read SQL
            //if (data["SelectPeriod"] == "Year") SQLcommand ...
            //if (data["SelectPeriod"] == "Month") SQLcommand ...
            //if (data["SelectPeriod"] == "Week") SQLcommand ...

            //Declare Parameters
            //int RowsNum = 6;
            //string Time[] = new string[RowsNum];

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            //temp_dict.Add("Time", Time);
            //連接資料庫後刪除下面這段
            if (data["SelectPeriod"] == "Year")
            {
                string[] Time = { "2022" };
                temp_dict.Add("Time", Time);
            }
            else if (data["SelectPeriod"] == "Month")
            {
                string[] Time = { "2022-06", "2022-07", "2022-08", "2022-09", "2022-10", "2022-11", "2022-12" };
                temp_dict.Add("Time", Time);
            }
            else if (data["SelectPeriod"] == "Week")
            {
                string[] Time = { "2022-25", "2022-26" };
                temp_dict.Add("Time", Time);
            }
            //連接資料庫後刪除上面這段
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //電盤資訊查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL
            //if (data["SelectPeriod"] == "Year") SQLcommand ...
            //if (data["SelectPeriod"] == "Month") SQLcommand ...
            //if (data["SelectPeriod"] == "Week") SQLcommand ...
            //if (data["SelectPeriod"] == "Day") SQLcommand ...

            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] Total = new double[RowsNum];
            //double[] VCB1 = new double[RowsNum];
            //double[] VCB2 = new double[RowsNum];
            //double[] VCB3 = new double[RowsNum];
            //double[] VCB4 = new double[RowsNum];
            //double[] Others = new double[RowsNum];
            //double[] Diff = new double[6];//電表+Total
            //double[] Percent = new double[6];//電表+Total

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            //temp_dict.Add("Time", Time);
            //temp_dict.Add("Total", Total);
            //temp_dict.Add("VCB1", VCB1);
            //temp_dict.Add("VCB2", VCB2);
            //temp_dict.Add("VCB3", VCB3);
            //temp_dict.Add("VCB4", VCB4);
            //temp_dict.Add("Others", Others);
            //temp_dict.Add("Diff", Diff);
            //temp_dict.Add("Percent", Percent);
            //連接資料庫後刪除下面這段
            if (data["SelectPeriod"] == "Year")
            {
                string[] Time = { "2022", "2021" };
                double[] Total = { 500, 530 };
                double[] VCB1 = { 120, 120};
                double[] VCB2 = { 90, 110};
                double[] VCB3 = { 100, 90};
                double[] VCB4 = { 90, 110};
                double[] Others = { 100, 100};
                double[] Diff = { 0, -20, 10, -20, 0, -30};
                double[] Percent = { 0, -18.18, 11.11, -18.18, 0, -6};
                temp_dict.Add("Time", Time);
                temp_dict.Add("Total", Total);
                temp_dict.Add("VCB1", VCB1);
                temp_dict.Add("VCB2", VCB2);
                temp_dict.Add("VCB3", VCB3);
                temp_dict.Add("VCB4", VCB4);
                temp_dict.Add("Others", Others);
                temp_dict.Add("Diff", Diff);
                temp_dict.Add("Percent", Percent);
            }
            else if (data["SelectPeriod"] == "Month")
            {
                string[] Time = { "2022-06", "2022-07" };
                double[] Total = { 548, 570 };
                double[] VCB1 = { 70, 150 };
                double[] VCB2 = { 100, 60 };
                double[] VCB3 = { 140, 190 };
                double[] VCB4 = { 138, 70 };
                double[] Others = { 100, 100 };
                double[] Diff = { -80, 40, -50, 68, 0, 22 };
                double[] Percent = { -53.33, 66.67, -26.32, 97.14, 0, 22 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("Total", Total);
                temp_dict.Add("VCB1", VCB1);
                temp_dict.Add("VCB2", VCB2);
                temp_dict.Add("VCB3", VCB3);
                temp_dict.Add("VCB4", VCB4);
                temp_dict.Add("Others", Others);
                temp_dict.Add("Diff", Diff);
                temp_dict.Add("Percent", Percent);
            }
            else if (data["SelectPeriod"] == "Week")
            {
                string[] Time = { "2022-25", "2022-26", "2022-27", "2022-28", "2022-29" };
                double[] Total = { 500, 530, 548, 570, 640 };
                double[] VCB1 = { 120, 120, 70, 150, 120 };
                double[] VCB2 = { 90, 110, 100, 60, 120 };
                double[] VCB3 = { 100, 90, 140, 190, 130 };
                double[] VCB4 = { 90, 110, 138, 70, 170 };
                double[] Others = { 100, 100, 100, 100, 100 };
                double[] Diff = { 0, -20, 10, -20, 0, -30 };
                double[] Percent = { 0, -18.18, 11.11, -18.18, 0, -6 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("Total", Total);
                temp_dict.Add("VCB1", VCB1);
                temp_dict.Add("VCB2", VCB2);
                temp_dict.Add("VCB3", VCB3);
                temp_dict.Add("VCB4", VCB4);
                temp_dict.Add("Others", Others);
                temp_dict.Add("Diff", Diff);
                temp_dict.Add("Percent", Percent);
            }
            else if (data["SelectPeriod"] == "Day")
            {
                string[] Time = { "2022-07-01", "2022-07-02", "2022-07-03", "2022-07-04", "2022-07-05" };
                double[] Total = { 500, 530, 548, 570, 640 };
                double[] VCB1 = { 120, 120, 70, 150, 120 };
                double[] VCB2 = { 90, 110, 100, 60, 120 };
                double[] VCB3 = { 100, 90, 140, 190, 130 };
                double[] VCB4 = { 90, 110, 138, 70, 170 };
                double[] Others = { 100, 100, 100, 100, 100 };
                double[] Diff = { 0, -20, 10, -20, 0, -30 };
                double[] Percent = { 0, -18.18, 11.11, -18.18, 0, -6 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("Total", Total);
                temp_dict.Add("VCB1", VCB1);
                temp_dict.Add("VCB2", VCB2);
                temp_dict.Add("VCB3", VCB3);
                temp_dict.Add("VCB4", VCB4);
                temp_dict.Add("Others", Others);
                temp_dict.Add("Diff", Diff);
                temp_dict.Add("Percent", Percent);
            }
            //連接資料庫後刪除上面這段
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}