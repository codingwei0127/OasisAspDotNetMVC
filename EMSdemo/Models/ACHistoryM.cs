using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class ACHistoryM
    {
        //空調系統查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //Read SQL
            //string sqlcommandstring = "";
            //if (data["equipmentType"] == "Chiller") { }
            //else if (data["equipmentType"] == "CHPump") { }
            //else if (data["equipmentType"] == "CWPump") { }
            //else if (data["equipmentType"] == "ZPump") { }
            //else if (data["equipmentType"] == "CTower") { }
            //DataTable dt1 = new DataTable();
            //SqlCommand sqlCommand = new SqlCommand(sqlcommandstring, conn);
            //SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            //conn.Open();
            //da.Fill(dt1);
            //conn.Close();

            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            //double[] kW = new double[RowsNum];
            //double[] Tchwin = new double[RowsNum];
            //double[] Tchwout = new double[RowsNum];
            //double[] Tcwin = new double[RowsNum];
            //double[] Tcwout = new double[RowsNum];
            //double[] CHFlow = new double[RowsNum];
            //string[] CHPstatus = new string[RowsNum];
            //string[] CHPcolor = new string[RowsNum];
            //double[] CHPf = new double[RowsNum];
            //string[] CWPstatus = new string[RowsNum];
            //string[] CWPcolor = new string[RowsNum];
            //double[] CWPf = new double[RowsNum];
            //string[] ZPstatus = new string[RowsNum];
            //string[] ZPcolor = new string[RowsNum];
            //double[] ZPf = new double[RowsNum];
            //string[] CTstatus = new string[RowsNum];
            //string[] CTcolor = new string[RowsNum];
            //double[] CTf = new double[RowsNum];
            string[] Time = { "2022-06-20 00:00:00", "2022-06-20 00:03:00", "2022-06-20 00:06:00" };
            double[] kW = { 331, 328, 327 };
            double[] Tchwin = { 8.74, 8.66, 8.65 };
            double[] Tchwout = { 6.03, 5.97, 5.97 };
            double[] Tcwin = { 28.2, 28.2, 28.25 };
            double[] Tcwout = { 32.32, 32.28, 32.32 };
            double[] CHFlow = { 8391, 8382, 8370 };
            string[] CHPstatus = { "開機", "開機", "開機" };
            string[] CHPcolor = { "#32c944", "#32c944", "#32c944" };//關機red
            double[] CHPf = { 45.5, 45.49, 45.49 };
            string[] CWPstatus = { "關機", "關機", "關機" };
            string[] CWPcolor = { "red", "red", "red" };//開機#32c944
            double[] CWPf = { 45.5, 45.49, 45.49 };
            string[] ZPstatus = { "開機", "開機", "關機" };
            string[] ZPcolor = { "#32c944", "#32c944", "red" };
            double[] ZPf = { 45.5, 45.49, 0 };
            string[] CTstatus = { "關機", "開機", "開機" };
            string[] CTcolor = { "red", "#32c944", "#32c944" };
            double[] CTf = { 0, 45.49, 45.49 };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            if (data["equipmentType"] == "Chiller")
            {
                temp_dict.Add("kW", kW);
                temp_dict.Add("Tchwin", Tchwin);
                temp_dict.Add("Tchwout", Tchwout);
                temp_dict.Add("Tcwin", Tcwin);
                temp_dict.Add("Tcwout", Tcwout);
                temp_dict.Add("CHFlow", CHFlow);
            }
            else if (data["equipmentType"] == "CHPump")
            {
                temp_dict.Add("CHPstatus", CHPstatus);
                temp_dict.Add("CHPcolor", CHPcolor);
                temp_dict.Add("CHPf", CHPf);
            }
            else if (data["equipmentType"] == "CWPump")
            {
                temp_dict.Add("CWPstatus", CWPstatus);
                temp_dict.Add("CWPcolor", CWPcolor);
                temp_dict.Add("CWPf", CWPf);
            }
            else if (data["equipmentType"] == "ZPump")
            {
                temp_dict.Add("ZPstatus", ZPstatus);
                temp_dict.Add("ZPcolor", ZPcolor);
                temp_dict.Add("ZPf", ZPf);
            }
            else if (data["equipmentType"] == "CTower")
            {
                temp_dict.Add("CTstatus", CTstatus);
                temp_dict.Add("CTcolor", CTcolor);
                temp_dict.Add("CTf", CTf);
            }
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}