using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class InformationM
    {
        //即時資訊讀資料
        public ArrayList GetData()
        {
            DateTime localDate = DateTime.Today;
            string Today = string.Format("{0:yyyy-MM-dd}", localDate);

            //Read SQL


            //Declare Parameters
            //OA-----------------------------------------------------------
            double Tdb = 0, Twb = 0, RH = 0, TotalRT = 0;
            Tdb = 20;
            Twb = 28;
            RH = 60;
            TotalRT = 1000;

            //Chilles------------------------------------------------------
            string CH1s = "", CH2s = "", CH3s = "", CHSPs = "";
            CH1s = "開機";
            CH2s = "關機";
            CH3s = "關機";
            CHSPs = "關機";
            string[] CHstatus = { CH1s, CH2s, CH3s, CHSPs };
            //double[] CH1v = new double[10];
            //double[] CH2v = new double[10];
            //double[] CH3v = new double[10];
            //double[] CHSPv = new double[10];
            double[] CH1v = { 7, 12, 200, 32, 36, 250, 800, 1000, 0.8, 2.5 };
            double[] CH2v = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] CH3v = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            double[] CHSPv = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //int CHRowsNum = 6;
            //string[] Time = new string[CHRowsNum];
            //double[] Tchwin = new double[CHRowsNum];
            //double[] Tchwout = new double[CHRowsNum];
            //double[] Tcwin = new double[CHRowsNum];
            //double[] Tcwout = new double[CHRowsNum];
            //double[] CHFlow = new double[CHRowsNum];
            string[] Time = { "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45" };
            double[] Tchwin = { 6.8, 6.4, 7.9, 6.4, 6.5, 6.9, 7.2, 6.6, 6.2, 6.6, 6.1, 7.5 };
            double[] Tchwout = { 8.7, 8.6, 8.9, 8.1, 8.1, 8.2, 8.1, 8.1, 8.7, 8.8, 8.9, 8.9 };
            double[] Tcwin = { 30.5, 30.2, 30.5, 30.4, 30.1, 30.6, 30.2, 30.9, 30.5, 30.7, 30.6, 30.7 };
            double[] Tcwout = { 33.1, 33.3, 33.6, 33.1, 33.4, 33.1, 33.6, 33.3, 33.2, 33.7, 33.6, 33.5 };
            double[] CHFlow = { 214.76, 231.9, 285.4, 258.17, 266.3, 257.2, 209.08, 252.52, 235.55, 286.72, 263.48, 249.14 };

            //CHP----------------------------------------------------------
            //string[] CHPstatus = new string[4];
            //double[] CHPv = new double[4];
            string CHP1s = "", CHP2s = "", CHP3s = "", CHPSPs = "";
            CHP1s = "開機";
            CHP2s = "關機";
            CHP3s = "關機";
            CHPSPs = "關機";
            string[] CHPstatus = { CHP1s, CHP2s, CHP3s, CHPSPs };
            double[] CHPv = { 60, 0, 0, 0 };

            //CWP----------------------------------------------------------
            //string[] CWPstatus = new string[4];
            //double[] CWPv = new double[4];
            string CWP1s = "", CWP2s = "", CWP3s = "", CWPSPs = "";
            CWP1s = "開機";
            CWP2s = "關機";
            CWP3s = "關機";
            CWPSPs = "關機";
            string[] CWPstatus = { CWP1s, CWP2s, CWP3s, CWPSPs };
            double[] CWPv = { 60, 0, 0, 0 };

            //ZP-----------------------------------------------------------
            //string[] ZPstatus = new string[4];
            //double[] ZPv = new double[4];
            string ZP1s = "", ZP2s = "", ZP3s = "", ZPSPs = "";
            ZP1s = "開機";
            ZP2s = "關機";
            ZP3s = "關機";
            ZPSPs = "關機";
            string[] ZPstatus = { ZP1s, ZP2s, ZP3s, ZPSPs };
            double[] ZPv = { 60, 0, 0, 0 };

            //CT-----------------------------------------------------------
            string CT1s = "", CT2s = "", CT3s = "", CT4s = "", CT5s = "", CT6s = "", CT7s = "", CT8s = "";
            CT1s = "開機";
            CT2s = "關機";
            CT3s = "關機";
            CT4s = "關機";
            CT5s = "關機";
            CT6s = "關機";
            CT7s = "關機";
            CT8s = "關機";
            string[] CTstatus = { CT1s, CT2s, CT3s, CT4s, CT5s, CT6s, CT7s, CT8s };
            double[] CTv = { 60, 0, 0, 0, 0, 0, 0, 0 };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Tdb", Tdb);
            temp_dict.Add("Twb", Twb);
            temp_dict.Add("RH", RH);
            temp_dict.Add("TotalRT", TotalRT);
            temp_dict.Add("CHstatus", CHstatus);
            temp_dict.Add("CH1v", CH1v);
            temp_dict.Add("CH2v", CH2v);
            temp_dict.Add("CH3v", CH3v);
            temp_dict.Add("CHSPv", CHSPv);
            temp_dict.Add("CHPstatus", CHPstatus);
            temp_dict.Add("CHPv", CHPv);
            temp_dict.Add("CWPstatus", CWPstatus);
            temp_dict.Add("CWPv", CWPv);
            temp_dict.Add("ZPstatus", ZPstatus);
            temp_dict.Add("ZPv", ZPv);
            temp_dict.Add("CTstatus", CTstatus);
            temp_dict.Add("CTv", CTv);
            temp_dict.Add("Time", Time);
            temp_dict.Add("Tchwin", Tchwin);
            temp_dict.Add("Tchwout", Tchwout);
            temp_dict.Add("Tcwin", Tcwin);
            temp_dict.Add("Tcwout", Tcwout);
            temp_dict.Add("CHFlow", CHFlow);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //首頁即時需量圖表DPtime button變換顯示區間
        public ArrayList ChangeCH(Dictionary<string, string> data)
        {
            DateTime localDate = DateTime.Today;
            string Today = string.Format("{0:yyyy-MM-dd}", localDate);

            //Read SQL
            //data["SelectedCH"]

            //Declare Parameters
            //int CHRowsNum = 6;
            //string[] Time = new string[CHRowsNum];
            //double[] Tchwin = new double[CHRowsNum];
            //double[] Tchwout = new double[CHRowsNum];
            //double[] Tcwin = new double[CHRowsNum];
            //double[] Tcwout = new double[CHRowsNum];
            //double[] CHFlow = new double[CHRowsNum];

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            //有真實資料後這段if else直接刪掉
            if (data["SelectedCH"] == "CH1")
            {
                string[] Time = { "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45" };
                double[] Tchwin = { 6.8, 6.4, 7.9, 6.4, 6.5, 6.9, 7.2, 6.6, 6.2, 6.6, 6.1, 7.5 };
                double[] Tchwout = { 8.7, 8.6, 8.9, 8.1, 8.1, 8.2, 8.1, 8.1, 8.7, 8.8, 8.9, 8.9 };
                double[] Tcwin = { 30.5, 30.2, 30.5, 30.4, 30.1, 30.6, 30.2, 30.9, 30.5, 30.7, 30.6, 30.7 };
                double[] Tcwout = { 33.1, 33.3, 33.6, 33.1, 33.4, 33.1, 33.6, 33.3, 33.2, 33.7, 33.6, 33.5 };
                double[] CHFlow = { 214.76, 231.9, 285.4, 258.17, 266.3, 257.2, 209.08, 252.52, 235.55, 286.72, 263.48, 249.14 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("Tchwin", Tchwin);
                temp_dict.Add("Tchwout", Tchwout);
                temp_dict.Add("Tcwin", Tcwin);
                temp_dict.Add("Tcwout", Tcwout);
                temp_dict.Add("CHFlow", CHFlow);
            }
            else
            {
                string[] Time = { "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45" };
                double[] Tchwin = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] Tchwout = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] Tcwin = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] Tcwout = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                double[] CHFlow = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("Tchwin", Tchwin);
                temp_dict.Add("Tchwout", Tchwout);
                temp_dict.Add("Tcwin", Tcwin);
                temp_dict.Add("Tcwout", Tcwout);
                temp_dict.Add("CHFlow", CHFlow);
            }
            //有真實資料後取消註解這段
            //temp_dict.Add("Time", Time);
            //temp_dict.Add("Tchwin", Tchwin);
            //temp_dict.Add("Tchwout", Tchwout);
            //temp_dict.Add("Tcwin", Tcwin);
            //temp_dict.Add("Tcwout", Tcwout);
            //temp_dict.Add("CHFlow", CHFlow);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}