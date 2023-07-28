using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class HistoryM
    {
        public ArrayList GetHistoryData_YearMonth(string TimeQuery, float QueryMode)
        {
            string query = "";

            if (QueryMode == 1) // 整年
            {
                query = "SELECT MONTH ([Time]) AS [Time],AVG([Power]) AS[Power] FROM[Oasis].[dbo].[Meter$] WHERE YEAR([Time])= ' " + TimeQuery + "' GROUP BY MONTH([Time]) ORDER BY MONTH([Time]) ";
            }
            else if (QueryMode == 2) // 整月
            {
                query = "  SELECT DAY([Time]) AS [Time],AVG([Power]) AS [Power]FROM[Oasis].[dbo].[Meter$] WHERE (MONTH([Time])= ' " + TimeQuery + "' AND YEAR([Time])= 2022) GROUP BY DAY([Time]) ORDER BY DAY([Time]) ";
            }
            else if (QueryMode == 3)
            {
                string[] subs = TimeQuery.Split('-');
                string YearM = subs[0]; 
                string MonthM = subs[1]; 
                string DayM = subs[2]; 

                query = "SELECT [Time],[Power] FROM[Oasis].[dbo].[Meter$] WHERE (YEAR([Time])= ' " + YearM + "'  " +
                    "AND MONTH([Time])=' " + MonthM + "' AND DAY([Time])=' " + DayM + "')ORDER BY[Time]";            
            }

            DataTable dtQuery = new DataTable();
            using (SqlConnection Conn01 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))

            //using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand cmd01 = new SqlCommand(query, Conn01);
                SqlDataAdapter bas1 = new SqlDataAdapter(cmd01);
                Conn01.Open();
                bas1.Fill(dtQuery);
                Conn01.Close();
            }

            string[] Time = new string[dtQuery.Rows.Count];
            double[] Power = new double[dtQuery.Rows.Count];

            if (dtQuery.Rows.Count > 0) 
            {
                for (int i = 0; i < dtQuery.Rows.Count; i++)
                {
                    Time[i] = dtQuery.Rows[i][0].ToString().Replace(" ", "");
                    Power[i] = Convert.ToDouble(dtQuery.Rows[i][1]);
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
                    { "Power", Power }
                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;
        }

        
        public ArrayList GetHistoryData_search(string start, string end)
        {


            //命名一個資料桌子，底下這個桌子叫做dt03
            DataTable dt03 = new DataTable();

            //底下這行是打開SQL連線的字串，包含IP、資料庫名稱、使用者ID、密碼 ，而且這行字串叫做Conn03
            //using (SqlConnection Conn03 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            //綠洲: 無密碼 using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))

            using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                //有一個SQL的命令，叫做cmd03，然後把你的.指令打在後面
                //string fff = "SELECT TOP(1000)[Time],[Current],[Voltage],[Power] FROM[Sucket_insert].[dbo].[Meter] WHERE Time>= ' " + start + "' AND Time <= ' " + end + "'order by Time DESC";
                //string fff = "SELECT TOP(1000) [Time],[Current],[Voltage],[Power] FROM[Oasis].[dbo].[Meter$] WHERE Time>= ' " + start + "' AND Time <= ' " + end + "'order by Time DESC";
                string fff = "SELECT *FROM[Oasis].[dbo].[Meter$] WHERE Time>= ' " + start + "' AND Time <= DATEADD(DAY, 1, ' " + end + "' ) ORDER BY Time";


                SqlCommand cmd03 = new SqlCommand(fff, Conn03);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas3 = new SqlDataAdapter(cmd03);
                //開啟Conn03的連線
                Conn03.Open();
                //bas3裡面的東西塞進dt3
                bas3.Fill(dt03);
                //關閉Conn03的連線
                Conn03.Close();
            }

            int Row = dt03.Rows.Count;
            string[] Time = new string[Row];
            double[] Power = new double[Row];

            if (dt03.Rows.Count > 0)
            {
                for (int i = 0; i < Row; i++)
                {
                    Time[i] = dt03.Rows[i][0].ToString().Replace(" ", "");
                    Power[i] = Convert.ToDouble(dt03.Rows[i][3]);
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
                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;

        }


        //首頁: 每台冷氣的總冷凍噸 表格
        public ArrayList TOTAL_KWH()
        {

            //命名一個資料桌子，底下這個桌子叫做dt03
            DataTable dt07 = new DataTable();

            //底下這行是打開SQL連線的字串，包含IP、資料庫名稱、使用者ID、密碼 ，而且這行字串叫做Conn03
            //using (SqlConnection Conn03 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                string ddd = "SELECT TOP (1) [Time],[Total_kW1],[Total_kW2],[Total_kW3],[Total_kW4],[Total_kW5],[Total_kW6] FROM [Oasis].[dbo].[Total_kWh] WHERE (Time >= convert(varchar(10), Getdate(), 120)and Time<convert(varchar(10),dateadd(d, 1, Getdate()),120)) ORDER BY Time DESC";

                SqlCommand cmd03 = new SqlCommand(ddd, Conn03);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas3 = new SqlDataAdapter(cmd03);
                //開啟Conn03的連線
                Conn03.Open();
                //bas3裡面的東西塞進dt3
                bas3.Fill(dt07);
                //關閉Conn03的連線
                Conn03.Close();
            }

            int Row = dt07.Rows.Count;
            string[] Time = new string[Row];
            double[] KWH1 = new double[Row];
            double[] KWH2 = new double[Row];
            double[] KWH3 = new double[Row];
            double[] KWH4 = new double[Row];
            double[] KWH5 = new double[Row];
            double[] KWH6 = new double[Row];


            if (dt07.Rows.Count > 0)
            {
                for (int i = 0; i < Row; i++)
                {
                    Time[i] = dt07.Rows[i][0].ToString().Replace(" ", "");
                    KWH1[i] = Convert.ToDouble(dt07.Rows[i][1]);
                    KWH2[i] = Convert.ToDouble(dt07.Rows[i][2]);
                    KWH3[i] = Convert.ToDouble(dt07.Rows[i][3]);
                    KWH4[i] = Convert.ToDouble(dt07.Rows[i][4]);
                    KWH5[i] = Convert.ToDouble(dt07.Rows[i][5]);
                    KWH6[i] = Convert.ToDouble(dt07.Rows[i][6]);

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
                    { "KWH1", KWH1 },
                    { "KWH2", KWH2 },
                    { "KWH3", KWH3 },
                    { "KWH4", KWH4 },
                    { "KWH5", KWH5 },
                    { "KWH6", KWH6 }
                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;
        }

        public ArrayList OneFloorElectricityChart(string OneFloorMode)
        {

            string Query = "";
            if (OneFloorMode == "1")
            {
                Query = "SELECT * FROM [Oasis].[dbo].[Meter$] WHERE (Time >= convert(varchar(10), Getdate(), 120)and Time<convert(varchar(10),dateadd(d, 1, Getdate()),120)) ORDER BY Time"; ;
            }
            else if (OneFloorMode == "2")
            {
                Query = "SELECT TOP (1000) [Time],[Current],[Voltage],[Power]FROM[Oasis].[dbo].[Meter$] WHERE(Time >= dateadd(HOUR, -1, GETDATE()) AND Time <= dateadd(HOUR, 0, GETDATE()))order by time"; ;
            }
            else if (OneFloorMode == "3")
            {
                Query = "SELECT TOP (1000) [Time],[Current],[Voltage],[Power]FROM[Oasis].[dbo].[Meter$] WHERE(Time >= dateadd(HOUR, -4, GETDATE()) AND Time <= dateadd(HOUR, 0, GETDATE()))order by time"; ;
            }
            DataTable dtCurrentDayOnce = new DataTable();

            //using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))
            // using (SqlConnection Conn01 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {



                SqlCommand cmd01 = new SqlCommand(Query, Conn01);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas1 = new SqlDataAdapter(cmd01);

                Conn01.Open();
                bas1.Fill(dtCurrentDayOnce);
                Conn01.Close();
            }

            //即時資訊一筆
            int Row = dtCurrentDayOnce.Rows.Count;
            string[] Time = new string[Row];
            double[] Power = new double[Row];

            if (dtCurrentDayOnce.Rows.Count > 0)
            {
                for (int i = 0; i < Row; i++)
                {
                    Time[i] = dtCurrentDayOnce.Rows[i][0].ToString().Replace(" ", "");
                    Power[i] = Convert.ToDouble(dtCurrentDayOnce.Rows[i][3]);
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
                    { "Power", Power }
                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;
        } // OneFloorElectricityChart()
        public ArrayList GetHistoryData()
        {
            //命名一個資料桌子，底下這個桌子叫做dtCurrentDayOnce
            DataTable dtCurrentDayOnce = new DataTable();
            DataTable dtCurrentDayPlot = new DataTable();
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
            //using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))
            //using (SqlConnection Conn03 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))

            {
                //有一個SQL的命令，叫做cmd03，然後把你的.指令打在後面

                //string CurrentDayOnce= "SELECT TOP(1000)[Timea] ,[T1a],[T2a] ,[Qa],[V],[C] ,[P],[PF] FROM[Hank].[dbo].[工作表2] WHERE Timea>= ' " + start + "' AND Timea <= ' " + end + "'order by Timea";
                //string CurrentDayOnce = "SELECT TOP(1000)[Timea] ,[T1a],[T2a] ,[Qa],[V],[C] ,[P],[PF] FROM[Hank].[dbo].[工作表2] order by Timea";

                //即時日期/耗電 *1 
                string CurrentDayOnce = "SELECT TOP (1) [Time],[Current],[Voltage],[Power] FROM [Oasis].[dbo].[Meter$] ORDER BY Time DESC";

                //當日耗電圖表(only 1 day), 估計一天最多收到150筆資料(winform5分鐘一筆)
                //string CurrentDayPlot = "SELECT TOP (1000) [Time],[Current],[Voltage],[Power] FROM [Oasis].[dbo].[Meter$] ORDER BY Time ";

                string CurrentDayPlot = "SELECT TOP (20) [Time],[Current],[Voltage],[Power] FROM [Oasis].[dbo].[Meter$] WHERE (Time >= convert(varchar(10), Getdate(), 120)and Time<convert(varchar(10),dateadd(d, 1, Getdate()),120)) ORDER BY Time";

                string AC1 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC1$]ORDER BY Time DESC";

                string AC2 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC2$] ORDER BY Time DESC";
                string AC3 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC3$] ORDER BY Time DESC";

                string AC4 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC4$] ORDER BY Time DESC";

                string AC5 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC5$] ORDER BY Time DESC";

                string AC6 = "SELECT TOP (1) [Time],[Operation],[Mode],[Speed],[Temperture] FROM [Oasis].[dbo].[AC6$] ORDER BY Time DESC";


                SqlCommand cmd03 = new SqlCommand(CurrentDayOnce, Conn03);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas3 = new SqlDataAdapter(cmd03);
                //開啟Conn03的連線
                Conn03.Open();
                //bas3裡面的東西塞進dt3
                bas3.Fill(dtCurrentDayOnce);
                //關閉Conn03的連線
                Conn03.Close();

                SqlCommand cmd04 = new SqlCommand(CurrentDayPlot, Conn03);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas4 = new SqlDataAdapter(cmd04);
                //開啟Conn03的連線
                Conn03.Open();
                //bas3裡面的東西塞進dt3
                bas4.Fill(dtCurrentDayPlot);
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
            int Row = dtCurrentDayOnce.Rows.Count;
            string[] Time = new string[Row];
            double[] Power = new double[Row];

            //當日耗電圖表
            int Rowall = dtCurrentDayPlot.Rows.Count;
            string[] Timeall = new string[Rowall];
            double[] Powerall = new double[Rowall];

            //冷氣1號
            int RowAC1 = dtAC1.Rows.Count;
            string[] TimeAC1 = new string[RowAC1];
            string[] OperationAC1 = new string[RowAC1];
            string[] TempAC1 = new string[RowAC1];
            string[] SpeedAC1 = new string[RowAC1];

            //冷氣2號
            int RowAC2 = dtAC2.Rows.Count;
            string[] TimeAC2 = new string[RowAC2];
            string[] OperationAC2 = new string[RowAC2];
            string[] TempAC2 = new string[RowAC2];
            string[] SpeedAC2 = new string[RowAC2];

            //冷氣3號
            int RowAC3 = dtAC3.Rows.Count;
            string[] TimeAC3 = new string[RowAC3];
            string[] OperationAC3 = new string[RowAC3];
            string[] TempAC3 = new string[RowAC3];
            string[] SpeedAC3 = new string[RowAC3];

            //冷氣4號
            int RowAC4 = dtAC4.Rows.Count;
            string[] TimeAC4 = new string[RowAC4];
            string[] OperationAC4 = new string[RowAC4];
            string[] TempAC4 = new string[RowAC4];
            string[] SpeedAC4 = new string[RowAC4];

            //冷氣5號
            int RowAC5 = dtAC5.Rows.Count;
            string[] TimeAC5 = new string[RowAC5];
            string[] OperationAC5 = new string[RowAC5];
            string[] TempAC5 = new string[RowAC5];
            string[] SpeedAC5 = new string[RowAC5];

            //冷氣6號
            int RowAC6 = dtAC6.Rows.Count;
            string[] TimeAC6 = new string[RowAC6];
            string[] OperationAC6 = new string[RowAC6];
            string[] TempAC6 = new string[RowAC6];
            string[] SpeedAC6 = new string[RowAC6];




            if (dtCurrentDayOnce.Rows.Count > 0)
            {
                for (int i = 0; i < Row; i++)
                {
                    Time[i] = dtCurrentDayOnce.Rows[i][0].ToString().Replace(" ", "");
                    Power[i] = Convert.ToDouble(dtCurrentDayOnce.Rows[i][3]);
                }
            }


            if (dtCurrentDayPlot.Rows.Count > 0)
            {
                for (int i = 0; i < Rowall; i++)
                {
                    Timeall[i] = dtCurrentDayPlot.Rows[i][0].ToString().Replace(" ", ""); // out: 2022/7/18下午01:00:00
                    Powerall[i] = Convert.ToDouble(dtCurrentDayPlot.Rows[i][3]);
                }
            }

            // 日期後處理


            //1號冷氣
            if (dtAC1.Rows.Count > 0)
            {
                for (int i = 0; i < RowAC1; i++)
                {
                    TimeAC1[i] = dtAC1.Rows[i][0].ToString().Replace(" ", "");
                    OperationAC1[i] = dtAC1.Rows[i][1].ToString().Replace(" ", "");
                    TempAC1[i] = dtAC1.Rows[i][4].ToString().Replace(" ", "");

                    SpeedAC1[i] = dtAC1.Rows[i][3].ToString().Replace(" ", "");
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
                    SpeedAC2[i] = dtAC2.Rows[i][3].ToString().Replace(" ", "");

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
                    SpeedAC3[i] = dtAC3.Rows[i][3].ToString().Replace(" ", "");

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
                    SpeedAC4[i] = dtAC4.Rows[i][3].ToString().Replace(" ", "");

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
                    SpeedAC5[i] = dtAC5.Rows[i][3].ToString().Replace(" ", "");

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
                    SpeedAC6[i] = dtAC6.Rows[i][3].ToString().Replace(" ", "");

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
                    { "SpeedAC1", SpeedAC1 },
                    //2號冷氣
                    { "TimeAC2", TimeAC2 },
                    { "OperationAC2", OperationAC2 },
                    { "TempAC2",TempAC2 },
                    { "RowAC2", RowAC2 },
                    { "SpeedAC2", SpeedAC2 },

                    //3號冷氣
                    { "TimeAC3", TimeAC3 },
                    { "OperationAC3", OperationAC3 },
                    { "TempAC3",TempAC3},
                    { "RowAC3", RowAC3 },
                    { "SpeedAC3", SpeedAC3 },

                    //4號冷氣
                    { "TimeAC4", TimeAC4 },
                    { "OperationAC4", OperationAC4},
                    { "TempAC4",TempAC4 },
                    { "RowAC4", RowAC4 },
                    { "SpeedAC4", SpeedAC4 },
                    //5號冷氣
                    { "TimeAC5", TimeAC5 },
                    { "OperationAC5", OperationAC5 },
                    { "TempAC5",TempAC5 },
                    { "RowAC5", RowAC5 },
                    { "SpeedAC5", SpeedAC5 },
                    //6號冷氣
                    { "TimeAC6", TimeAC6 },
                    { "OperationAC6", OperationAC6 },
                    { "TempAC6",TempAC6 },
                    { "RowAC6", RowAC6 },
                    { "SpeedAC6", SpeedAC6 }

                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;
        }
        public ArrayList GetData(string start, string end)
        {


            //命名一個資料桌子，底下這個桌子叫做dt03
            DataTable dt03 = new DataTable();

            //底下這行是打開SQL連線的字串，包含IP、資料庫名稱、使用者ID、密碼 ，而且這行字串叫做Conn03
            //using (SqlConnection Conn03 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            //綠洲: 無密碼 using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))

            using (SqlConnection Conn03 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            //家裡using (SqlConnection Conn01 = new SqlConnection("Data Source=DESKTOP-UN8FNAR;Initial Catalog=coding_wei;Persist Security Info=True;User ID=sa;Password=123456"))

            {
                //有一個SQL的命令，叫做cmd03，然後把你的.指令打在後面
                //string fff = "SELECT TOP(1000)[Time],[Current],[Voltage],[Power] FROM[Sucket_insert].[dbo].[Meter] WHERE Time>= ' " + start + "' AND Time <= ' " + end + "'order by Time DESC";
                //string fff = "SELECT TOP(1000) [Time],[Current],[Voltage],[Power] FROM[Oasis].[dbo].[Meter$] WHERE Time>= ' " + start + "' AND Time <= ' " + end + "'order by Time DESC";
                string fff = "SELECT TOP(1000) [Time],[Current],[Voltage],[Power] FROM[Oasis].[dbo].[Meter$] WHERE Time>= ' " + start + "' AND Time <= DATEADD(DAY, 1, ' " + end + "' ) ORDER BY Time";


                SqlCommand cmd03 = new SqlCommand(fff, Conn03);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter bas3 = new SqlDataAdapter(cmd03);
                //開啟Conn03的連線
                Conn03.Open();
                //bas3裡面的東西塞進dt3
                bas3.Fill(dt03);
                //關閉Conn03的連線
                Conn03.Close();
            }

            int Row = dt03.Rows.Count;
            string[] Time = new string[Row];
            double[] Power = new double[Row];

            if (dt03.Rows.Count > 0)
            {
                for (int i = 0; i < Row; i++)
                {
                    Time[i] = dt03.Rows[i][0].ToString().Replace(" ", "");
                    Power[i] = Convert.ToDouble(dt03.Rows[i][3]);
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
                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;

        } // GetData(start, end)
  

    } // HistoryM()
}