using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class WeatherM
    {
        //天氣預測讀資料
        public ArrayList GetData()
        {
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





            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
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
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}