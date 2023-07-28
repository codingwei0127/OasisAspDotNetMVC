using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class IndexM
    {

        //首頁讀資料
        public ArrayList RealTimeData()
        {
            // 畫家版本

            //命名一個資料桌子，底下這個桌子叫做dt03
            DataTable dtRTD = new DataTable();


            //底下這行是打開SQL連線的字串，包含IP、資料庫名稱、使用者ID、密碼 ，而且這行字串叫做Conn03
            //using (SqlConnection Conn02 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))
            //using (SqlConnection Conn02 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            using (SqlConnection Conn02 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))

            {

                string RD = "SELECT TOP (1) [Time],[Current],[Voltage],[Power]FROM [Oasis].[dbo].[Meter$] ORDER BY Time DESC";



                SqlCommand cmd02 = new SqlCommand(RD, Conn02);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas3 = new SqlDataAdapter(cmd02);
                //開啟Conn03的連線
                Conn02.Open();
                //bas3裡面的東西塞進dt3
                bas3.Fill(dtRTD);
                //關閉Conn03的連線
                Conn02.Close();


            }

            //即時資訊一筆
            int Row = dtRTD.Rows.Count;
            string[] Time = new string[Row];
            double[] Power = new double[Row];






            if (dtRTD.Rows.Count > 0)
            {

                for (int i = 0; i < Row; i++)
                {
                    Time[i] = dtRTD.Rows[i][0].ToString().Replace(" ", "");
                    Power[i] = Convert.ToDouble(dtRTD.Rows[i][3]);
                }

            }






            //命名一個陣列清單叫做ReturnArray
            ArrayList ReturnArray = new ArrayList();
            //有一個字典叫做temp_dict
            Dictionary<string, object> temp_dict;
            //這本temp_dict字典是<string, object>的類型(前面是名字，後面是裝的東西的類型，所以這個代表前面是字串，後面是東西)
            temp_dict = new Dictionary<string, object>
                {
                    { "RealTime", Time },
                    { "RealTimePower", Power },                
                //全部電表資訊



                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;

            // 畫家版本 end
        }
        //首頁讀資料
        public ArrayList GetData()
        {
            // 畫家版本

                //命名一個資料桌子，底下這個桌子叫做dt03
                DataTable dt03 = new DataTable();
                DataTable dt04 = new DataTable();
                //冷氣1號
                DataTable dtAC1 = new DataTable();
                //冷氣2號
                DataTable dtAC2 = new DataTable();
                //冷氣3號
                DataTable dtAC3 = new DataTable();
                //冷氣4號
                DataTable dtAC4 = new DataTable();
                //冷氣5號
                DataTable dtAC5 = new DataTable();
                //冷氣6號
                DataTable dtAC6 = new DataTable();

                //底下這行是打開SQL連線的字串，包含IP、資料庫名稱、使用者ID、密碼 ，而且這行字串叫做Conn03
                // 綠洲現場: using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
                //using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))
                using (SqlConnection Conn03 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))

                {
                    //有一個SQL的命令，叫做cmd03，然後把你的.指令打在後面

                    //string fff= "SELECT TOP(1000)[Timea] ,[T1a],[T2a] ,[Qa],[V],[C] ,[P],[PF] FROM[Hank].[dbo].[工作表2] WHERE Timea>= ' " + start + "' AND Timea <= ' " + end + "'order by Timea";
                    //string fff = "SELECT TOP(1000)[Timea] ,[T1a],[T2a] ,[Qa],[V],[C] ,[P],[PF] FROM[Hank].[dbo].[工作表2] order by Timea";

                    //即時日期/耗電 *1 
                    string fff = "SELECT TOP (1) [Time],[Current],[Voltage],[Power] FROM [Oasis].[dbo].[Meter$] ORDER BY Time DESC";

                    //當日耗電圖表(only 1 day), 估計一天最多收到150筆資料(winform5分鐘一筆)
                    //string ddd = "SELECT TOP (1000) [Time],[Current],[Voltage],[Power] FROM [Oasis].[dbo].[Meter$] ORDER BY Time ";

                    string ddd = "SELECT TOP (20) [Time],[Current],[Voltage],[Power] FROM [Oasis].[dbo].[Meter$] WHERE (Time >= convert(varchar(10), Getdate(), 120)and Time<convert(varchar(10),dateadd(d, 1, Getdate()),120)) ORDER BY Time DESC";

                    string AC1 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC1$]ORDER BY Time DESC";

                    string AC2 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC2$] ORDER BY Time DESC";
                    string AC3 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC3$] ORDER BY Time DESC";

                    string AC4 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC4$] ORDER BY Time DESC";

                    string AC5 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC5$] ORDER BY Time DESC";

                    string AC6 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC6$] ORDER BY Time DESC";


                    SqlCommand cmd03 = new SqlCommand(fff, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter bas3 = new SqlDataAdapter(cmd03);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    bas3.Fill(dt03);
                    //關閉Conn03的連線
                    Conn03.Close();

                    SqlCommand cmd04 = new SqlCommand(ddd, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter bas4 = new SqlDataAdapter(cmd04);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    bas4.Fill(dt04);
                    //關閉Conn03的連線
                    Conn03.Close();

                    //1號冷氣
                    SqlCommand cmdAC1 = new SqlCommand(AC1, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter basAC1 = new SqlDataAdapter(cmdAC1);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    basAC1.Fill(dtAC1);
                    //關閉Conn03的連線
                    Conn03.Close();

                    //2號冷氣
                    SqlCommand cmdAC2 = new SqlCommand(AC2, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter basAC2 = new SqlDataAdapter(cmdAC2);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    basAC2.Fill(dtAC2);
                    //關閉Conn03的連線
                    Conn03.Close();

                    //3號冷氣
                    SqlCommand cmdAC3 = new SqlCommand(AC3, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter basAC3 = new SqlDataAdapter(cmdAC3);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    basAC3.Fill(dtAC3);
                    //關閉Conn03的連線
                    Conn03.Close();

                    //4號冷氣
                    SqlCommand cmdAC4 = new SqlCommand(AC4, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter basAC4 = new SqlDataAdapter(cmdAC4);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    basAC4.Fill(dtAC4);
                    //關閉Conn03的連線
                    Conn03.Close();

                    //5號冷氣
                    SqlCommand cmdAC5 = new SqlCommand(AC5, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter basAC5 = new SqlDataAdapter(cmdAC5);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    basAC5.Fill(dtAC5);
                    //關閉Conn03的連線
                    Conn03.Close();

                    //6號冷氣
                    SqlCommand cmdAC6 = new SqlCommand(AC6, Conn03);
                    //bas3存放上面那個命令(cmd03)
                    SqlDataAdapter basAC6 = new SqlDataAdapter(cmdAC6);
                    //開啟Conn03的連線
                    Conn03.Open();
                    //bas3裡面的東西塞進dt3
                    basAC6.Fill(dtAC6);
                    //關閉Conn03的連線
                    Conn03.Close();

                }

                //即時資訊一筆
                int Row = dt03.Rows.Count;
                string[] Time = new string[Row];
                double[] Power = new double[Row];

                //當日耗電圖表
                int Rowall = dt04.Rows.Count;
                string[] Timeall = new string[Rowall];
                double[] Powerall = new double[Rowall];

                //冷氣1號
                int RowAC1 = dtAC1.Rows.Count;
                string[] TimeAC1 = new string[RowAC1];
                string[] OperationAC1 = new string[RowAC1];
                string[] TempAC1 = new string[RowAC1];
                //冷氣2號
                int RowAC2 = dtAC2.Rows.Count;
                string[] TimeAC2 = new string[RowAC2];
                string[] OperationAC2 = new string[RowAC2];
                string[] TempAC2 = new string[RowAC2];
                //冷氣3號
                int RowAC3 = dtAC3.Rows.Count;
                string[] TimeAC3 = new string[RowAC3];
                string[] OperationAC3 = new string[RowAC3];
                string[] TempAC3 = new string[RowAC3];
                //冷氣4號
                int RowAC4 = dtAC4.Rows.Count;
                string[] TimeAC4 = new string[RowAC4];
                string[] OperationAC4 = new string[RowAC4];
                string[] TempAC4 = new string[RowAC4];
                //冷氣5號
                int RowAC5 = dtAC5.Rows.Count;
                string[] TimeAC5 = new string[RowAC5];
                string[] OperationAC5 = new string[RowAC5];
                string[] TempAC5 = new string[RowAC5];
                //冷氣6號
                int RowAC6 = dtAC6.Rows.Count;
                string[] TimeAC6 = new string[RowAC6];
                string[] OperationAC6 = new string[RowAC6];
                string[] TempAC6 = new string[RowAC6];



                if (dt03.Rows.Count > 0)
                {
                    for (int i = 0; i < Row; i++)
                    {
                        Time[i] = dt03.Rows[i][0].ToString().Replace(" ", "");
                        Power[i] = Convert.ToDouble(dt03.Rows[i][3]);
                    }
                }


                if (dt04.Rows.Count > 0)
                {
                    for (int i = 0; i < Rowall; i++)
                    {
                        Timeall[i] = dt04.Rows[i][0].ToString().Replace(" ", "");
                        Powerall[i] = Convert.ToDouble(dt04.Rows[i][3]);
                    }
                }


                //1號冷氣
                if (dtAC1.Rows.Count > 0)
                {
                    for (int i = 0; i < RowAC1; i++)
                    {
                        TimeAC1[i] = dtAC1.Rows[i][0].ToString().Replace(" ", "");
                        OperationAC1[i] = dtAC1.Rows[i][1].ToString().Replace(" ", "");
                        TempAC1[i] = dtAC1.Rows[i][4].ToString().Replace(" ", "");

                    }
                }
                //2號冷氣
                if (dtAC2.Rows.Count > 0)
                {
                    for (int i = 0; i < RowAC2; i++)
                    {
                        TimeAC2[i] = dtAC2.Rows[i][0].ToString().Replace(" ", "");
                        OperationAC2[i] = dtAC2.Rows[i][1].ToString().Replace(" ", "");
                        TempAC2[i] = dtAC2.Rows[i][4].ToString().Replace(" ", "");

                    }
                }
                //3號冷氣
                if (dtAC3.Rows.Count > 0)
                {
                    for (int i = 0; i < RowAC3; i++)
                    {
                        TimeAC3[i] = dtAC3.Rows[i][0].ToString().Replace(" ", "");
                        OperationAC3[i] = dtAC3.Rows[i][1].ToString().Replace(" ", "");
                        TempAC3[i] = dtAC3.Rows[i][4].ToString().Replace(" ", "");

                    }
                }
                //4號冷氣
                if (dtAC4.Rows.Count > 0)
                {
                    for (int i = 0; i < RowAC4; i++)
                    {
                        TimeAC4[i] = dtAC4.Rows[i][0].ToString().Replace(" ", "");
                        OperationAC4[i] = dtAC4.Rows[i][1].ToString().Replace(" ", "");
                        TempAC4[i] = dtAC4.Rows[i][4].ToString().Replace(" ", "");

                    }
                }
                //5號冷氣
                if (dtAC5.Rows.Count > 0)
                {
                    for (int i = 0; i < RowAC5; i++)
                    {
                        TimeAC5[i] = dtAC5.Rows[i][0].ToString().Replace(" ", "");
                        OperationAC5[i] = dtAC5.Rows[i][1].ToString().Replace(" ", "");
                        TempAC5[i] = dtAC5.Rows[i][4].ToString().Replace(" ", "");

                    }
                }
                //6號冷氣
                if (dtAC6.Rows.Count > 0)
                {
                    for (int i = 0; i < RowAC6; i++)
                    {
                        TimeAC6[i] = dtAC6.Rows[i][0].ToString().Replace(" ", "");
                        OperationAC6[i] = dtAC6.Rows[i][1].ToString().Replace(" ", "");
                        TempAC6[i] = dtAC6.Rows[i][4].ToString().Replace(" ", "");

                    }
                }



                //命名一個陣列清單叫做ReturnArray
                ArrayList ReturnArray = new ArrayList();
                //有一個字典叫做temp_dict
                Dictionary<string, object> temp_dict;
                //這本temp_dict字典是<string, object>的類型(前面是名字，後面是裝的東西的類型，所以這個代表前面是字串，後面是東西)
                temp_dict = new Dictionary<string, object>
                {
                    { "Time", Time },
                    { "Power", Power },
                    { "Row", Row },
                    //全部電表資訊
                    { "Timeall", Timeall },
                    { "Powerall", Powerall },
                    { "Rowall", Rowall },
                    //1號冷氣
                    { "TimeAC1", TimeAC1 },
                    { "OperationAC1", OperationAC1 },
                    { "TempAC1",TempAC1 },
                    { "RowAC1", RowAC1 },
                    //2號冷氣
                    { "TimeAC2", TimeAC2 },
                    { "OperationAC2", OperationAC2 },
                    { "TempAC2",TempAC2 },
                    { "RowAC2", RowAC2 },
                    //3號冷氣
                    { "TimeAC3", TimeAC3 },
                    { "OperationAC3", OperationAC3 },
                    { "TempAC3",TempAC3},
                    { "RowAC3", RowAC3 },
                    //4號冷氣
                    { "TimeAC4", TimeAC4 },
                    { "OperationAC4", OperationAC4},
                    { "TempAC4",TempAC4 },
                    { "RowAC4", RowAC4 },
                    //5號冷氣
                    { "TimeAC5", TimeAC5 },
                    { "OperationAC5", OperationAC5 },
                    { "TempAC5",TempAC5 },
                    { "RowAC5", RowAC5 },
                    //6號冷氣
                    { "TimeAC6", TimeAC6 },
                    { "OperationAC6", OperationAC6 },
                    { "TempAC6",TempAC6 },
                    { "RowAC6", RowAC6 },


                };
                //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
                Console.WriteLine("temp_dict", temp_dict);
                //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
                ReturnArray.Add(temp_dict);
                //傳回ReturnArray
                return ReturnArray;
           
            // 畫家版本 end
        }

        //首頁即時需量圖表DPtime button變換顯示區間
        public ArrayList ChangeData(Dictionary<string, string> data)
        {
            DateTime localDate = DateTime.Today;
            string Today = string.Format("{0:yyyy-MM-dd}", localDate);
            string Month = string.Format("{0:yyyy-MM}", localDate);
            string Year = string.Format("{0:yyyy}", localDate);

            //Read SQL
            //if (data["ID"] == "DPday"){

            //}
            //else{
            //  //最新三小時資料
            //}

            //Declare Parameters
            //DemandPower--------------------------------------------------
            //int DPRowsNum = 6;
            //string[] Time = new string[DPRowsNum];
            //double[] BuildingDP = new double[DPRowsNum];
            //double[] HvacDP = new double[DPRowsNum];
            double ContractDP = 2600;//需讀取契約容量
            ContractDP = 2600;

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            //有真實資料後這段if else直接刪掉
            if (data["ID"] == "DPday")
            {
                string[] Time = { "2022-06-20 00:00", "2022-06-20 00:15", "2022-06-20 00:30", "2022-06-20 00:45", "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45" };
                double[] BuildingDP = { 1914.76, 1931.9, 1985.4, 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55, 1931.9, 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55 };
                double[] HvacDP = { 1081.34, 1036.8, 1005.46, 1029.26, 1088.74, 1016.13, 1063.93, 1088.51, 1029.57, 1036.8, 1005.46, 1029.26, 1088.74, 1016.13, 1063.93, 1088.51 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("BuildingDP", BuildingDP);
                temp_dict.Add("HvacDP", HvacDP);
            }
            else
            {
                string[] Time = { "2022-06-20 00:45", "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45" };
                double[] BuildingDP = { 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55, 1931.9, 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55 };
                double[] HvacDP = { 1029.26, 1088.74, 1016.13, 1063.93, 1088.51, 1029.57, 1036.8, 1005.46, 1029.26, 1088.74, 1016.13, 1063.93, 1088.51 };
                temp_dict.Add("Time", Time);
                temp_dict.Add("BuildingDP", BuildingDP);
                temp_dict.Add("HvacDP", HvacDP);
            }
            //有真實資料後取消註解這三行
            //temp_dict.Add("Time", Time);
            //temp_dict.Add("BuildingDP", BuildingDP);
            //temp_dict.Add("HvacDP", HvacDP);
            temp_dict.Add("ContractDP", ContractDP);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //首頁刷新資料
        public ArrayList UpdateData(Dictionary<string, string> data)
        {
            DateTime localDate = DateTime.Today;
            string Today = string.Format("{0:yyyy-MM-dd}", localDate);
            string Month = string.Format("{0:yyyy-MM}", localDate);
            string Year = string.Format("{0:yyyy}", localDate);

            //Read SQL
            //if (data["ID"] == "DPday"){

            //}
            //else{
            //  //最新三小時資料
            //}

            //Declare Parameters
            //DemandPower--------------------------------------------------
            //int DPRowsNum = 6;
            //string[] Time = new string[DPRowsNum];
            //double[] BuildingDP = new double[DPRowsNum];
            //double[] HvacDP = new double[DPRowsNum];
            double InstantDP = 0, PeakDP = 0, ContractDP = 2600;
            int SurpassTimes = 0;
            //有真實資料後取消註解這段
            //if (BuildingDP.Length > 0) InstantDP = BuildingDP[BuildingDP.Length - 1];
            //ContractDP = 2600;//需讀取契約容量
            //PeakDP = 1985.4;//需讀取該日最大值(圖表僅顯示三小時資料)
            //SurpassTimes = 0;//需讀取該日超約次數(圖表僅顯示三小時資料)
            //double ContractDP = 2600;//需讀取契約容量

            //Electric-----------------------------------------------------
            double ThisYearMin = 0, ThisMonthMin = 0, TodayMin = 0, TodayMax = 10;
            double ThisYearEle = 0, ThisMonthEle = 0, TodayEle = 0;

            ThisYearEle = TodayMax - ThisYearMin;
            ThisMonthEle = TodayMax - ThisMonthMin;
            TodayEle = TodayMax - TodayMin;
            if (ThisYearEle < 0) ThisYearEle = 0;
            if (ThisMonthEle < 0) ThisMonthEle = 0;
            if (TodayEle < 0) TodayEle = 0;

            //若不更新設備則刪除下面這段
            //Chillers-----------------------------------------------------
            //string[] CHstatus = new string[4];
            //double[] CH1v = new double[3];
            //double[] CH2v = new double[3];
            //double[] CH3v = new double[3];
            //double[] CHSPv = new double[3];
            string CH1s = "", CH2s = "", CH3s = "", CHSPs = "";
            CH1s = "開機";
            CH2s = "開機";
            CH3s = "關機";
            CHSPs = "關機";
            string[] CHstatus = { CH1s, CH2s, CH3s, CHSPs };
            double[] CH1v = { 0, 0, 0 };
            double[] CH2v = { 0, 0, 0 };
            double[] CH3v = { 0, 0, 0 };
            double[] CHSPv = { 0, 0, 0 };

            //CHP----------------------------------------------------------
            //string[] CHPstatus = new string[4];
            //double[] CHPv = new double[4];
            string CHP1s = "", CHP2s = "", CHP3s = "", CHPSPs = "";
            CHP1s = "開機";
            CHP2s = "開機";
            CHP3s = "關機";
            CHPSPs = "關機";
            string[] CHPstatus = { CHP1s, CHP2s, CHP3s, CHPSPs };
            double[] CHPv = { 60, 60, 0, 0 };

            //CWP----------------------------------------------------------
            //string[] CWPstatus = new string[4];
            //double[] CWPv = new double[4];
            string CWP1s = "", CWP2s = "", CWP3s = "", CWPSPs = "";
            CWP1s = "開機";
            CWP2s = "開機";
            CWP3s = "關機";
            CWPSPs = "關機";
            string[] CWPstatus = { CWP1s, CWP2s, CWP3s, CWPSPs };
            double[] CWPv = { 60, 60, 0, 0 };

            //ZP-----------------------------------------------------------
            //string[] ZPstatus = new string[4];
            //double[] ZPv = new double[4];
            string ZP1s = "", ZP2s = "", ZP3s = "", ZPSPs = "";
            ZP1s = "開機";
            ZP2s = "開機";
            ZP3s = "關機";
            ZPSPs = "關機";
            string[] ZPstatus = { ZP1s, ZP2s, ZP3s, ZPSPs };
            double[] ZPv = { 60, 60, 0, 0 };

            //CT-----------------------------------------------------------
            //string[] CTstatus = new string[8];
            //double[] CTv = new double[8];
            string CT1s = "", CT2s = "", CT3s = "", CT4s = "", CT5s = "", CT6s = "", CT7s = "", CT8s = "";
            CT1s = "開機";
            CT2s = "開機";
            CT3s = "關機";
            CT4s = "關機";
            CT5s = "關機";
            CT6s = "關機";
            CT7s = "關機";
            CT8s = "關機";
            string[] CTstatus = { CT1s, CT2s, CT3s, CT4s, CT5s, CT6s, CT7s, CT8s };
            double[] CTv = { 60, 60, 0, 0, 0, 0, 0, 0 };
            //若不更新設備則刪除上面這段

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            //有真實資料後這段if else直接刪掉
            if (data["ID"] == "DPday")
            {
                string[] Time = { "2022-06-20 00:00", "2022-06-20 00:15", "2022-06-20 00:30", "2022-06-20 00:45", "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45", "2022-06-20 04:00" };
                double[] BuildingDP = { 1914.76, 1931.9, 1985.4, 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55, 1931.9, 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55, 1958.17 };
                double[] HvacDP = { 1081.34, 1036.8, 1005.46, 1029.26, 1088.74, 1016.13, 1063.93, 1088.51, 1029.57, 1036.8, 1005.46, 1029.26, 1088.74, 1016.13, 1063.93, 1088.51, 1029.26 };
                if (BuildingDP.Length > 0) InstantDP = BuildingDP[BuildingDP.Length - 1];
                PeakDP = 1985.4;
                SurpassTimes = 0;
                temp_dict.Add("Time", Time);
                temp_dict.Add("BuildingDP", BuildingDP);
                temp_dict.Add("HvacDP", HvacDP);
                temp_dict.Add("InstantDP", InstantDP);
                temp_dict.Add("PeakDP", PeakDP);
                temp_dict.Add("SurpassTimes", SurpassTimes);
            }
            else
            {
                string[] Time = { "2022-06-20 01:00", "2022-06-20 01:15", "2022-06-20 01:30", "2022-06-20 01:45", "2022-06-20 02:00", "2022-06-20 02:15", "2022-06-20 02:30", "2022-06-20 02:45", "2022-06-20 03:00", "2022-06-20 03:15", "2022-06-20 03:30", "2022-06-20 03:45", "2022-06-20 04:00" };
                double[] BuildingDP = { 1966.3, 1957.2, 1909.08, 1952.52, 1935.55, 1931.9, 1958.17, 1966.3, 1957.2, 1909.08, 1952.52, 1935.55, 1958.17 };
                double[] HvacDP = { 1088.74, 1016.13, 1063.93, 1088.51, 1029.57, 1036.8, 1005.46, 1029.26, 1088.74, 1016.13, 1063.93, 1088.51, 1029.26 };
                if (BuildingDP.Length > 0) InstantDP = BuildingDP[BuildingDP.Length - 1];
                PeakDP = 1985.4;
                SurpassTimes = 0;
                temp_dict.Add("Time", Time);
                temp_dict.Add("BuildingDP", BuildingDP);
                temp_dict.Add("HvacDP", HvacDP);
                temp_dict.Add("InstantDP", InstantDP);
                temp_dict.Add("PeakDP", PeakDP);
                temp_dict.Add("SurpassTimes", SurpassTimes);
            }
            //有真實資料後取消註解這段
            //temp_dict.Add("Time", Time);
            //temp_dict.Add("BuildingDP", BuildingDP);
            //temp_dict.Add("HvacDP", HvacDP);
            //temp_dict.Add("InstantDP", InstantDP);
            //temp_dict.Add("PeakDP", PeakDP);
            //temp_dict.Add("SurpassTimes", SurpassTimes);
            temp_dict.Add("ContractDP", ContractDP);
            //若不改變電的數值則刪掉這三行
            temp_dict.Add("ThisYearEle", ThisYearEle);
            temp_dict.Add("ThisMonthEle", ThisMonthEle);
            temp_dict.Add("TodayEle", TodayEle);
            //若不更新設備則刪除下面這段
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
            //若不更新設備則刪除上面這段
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}