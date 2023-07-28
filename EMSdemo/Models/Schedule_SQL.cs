using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class Schedule_SQL
    {
        public ArrayList Search() // 工作排程: 1~6號冷氣 + 燈具
        {

            DataTable AC1_Schedule = new DataTable();
            DataTable AC2_Schedule = new DataTable();
            DataTable AC3_Schedule = new DataTable();
            DataTable AC4_Schedule = new DataTable();
            DataTable AC5_Schedule = new DataTable();
            DataTable AC6_Schedule = new DataTable();
            DataTable Light_Schedule = new DataTable();
            DataTable yesterdayLightSchedule = new DataTable();

            string yesterday_LightSchedule = "SELECT TOP(1)" +
                "[Time_0],[Time_1],[Time_2],[Time_3],[Time_4],[Time_5],[Time_6],[Time_7],[Time_8],[Time_9],[Time_10],[Time_11],[Time_12],[Time_13],[Time_14],[Time_15],[Time_16],[Time_17],[Time_18],[Time_19],[Time_20],[Time_21],[Time_22],[Time_23],[Time_24],[Time_25],[Time_26],[Time_27],[Current_Time]" +
                " FROM[Oasis].[dbo].[schedule_7]" +
                "WHERE([Current_Time] >= convert(varchar(10), dateadd(d, -1, Getdate()), 120)" +
                "and[Current_Time] <= convert(varchar(10), dateadd(d, 0, Getdate()), 120))" +
                "ORDER BY[Current_Time] DESC";

            string ACandLight_Schedule_1 = "SELECT TOP (1) [Time_0],[Time_1],[Time_2]," +
            "[Time_3],[Time_4],[Time_5],[Time_6],[Time_7],[Time_8],[Time_9],[Time_10],[Time_11],[Time_12]" +
            ",[Time_13],[Time_14],[Time_15],[Time_16],[Time_17],[Time_18],[Time_19],[Time_20],[Time_21]" +
            ",[Time_22],[Time_23],[Time_24],[Time_25],[Time_26],[Time_27],[Current_Time]" +
            "FROM[Oasis].[dbo].[schedule_";

            string ACandLight_Schedule_2 = "] ORDER BY[Current_Time] DESC";

            //using (SqlConnection Conn01 = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            //using (SqlConnection Conn01 = new SqlConnection("Data Source=DESKTOP-UN8FNAR;Initial Catalog=coding_wei;Persist Security Info=True;User ID=sa;Password=123456"))
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))

            {
                SqlCommand cmd01 = new SqlCommand(ACandLight_Schedule_1 + "1" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas1 = new SqlDataAdapter(cmd01);

                SqlCommand cmd02 = new SqlCommand(ACandLight_Schedule_1 + "2" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas2 = new SqlDataAdapter(cmd02);

                SqlCommand cmd03 = new SqlCommand(ACandLight_Schedule_1 + "3" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas3 = new SqlDataAdapter(cmd03);

                SqlCommand cmd04 = new SqlCommand(ACandLight_Schedule_1 + "4" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas4 = new SqlDataAdapter(cmd04);

                SqlCommand cmd05 = new SqlCommand(ACandLight_Schedule_1 + "5" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas5 = new SqlDataAdapter(cmd05);

                SqlCommand cmd06 = new SqlCommand(ACandLight_Schedule_1 + "6" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas6 = new SqlDataAdapter(cmd06);

                SqlCommand cmd07 = new SqlCommand(ACandLight_Schedule_1 + "7" + ACandLight_Schedule_2, Conn01);
                SqlDataAdapter bas7 = new SqlDataAdapter(cmd07);

                SqlCommand cmd08 = new SqlCommand(yesterday_LightSchedule, Conn01);
                SqlDataAdapter bas8 = new SqlDataAdapter(cmd08);

                Conn01.Open();

                bas1.Fill(AC1_Schedule);
                bas2.Fill(AC2_Schedule);
                bas3.Fill(AC3_Schedule);
                bas4.Fill(AC4_Schedule);
                bas5.Fill(AC5_Schedule);
                bas6.Fill(AC6_Schedule);
                bas7.Fill(Light_Schedule);
                bas8.Fill(yesterdayLightSchedule);
 
                Conn01.Close();
            }

            double[] AC1_Time = new double[28]; // c#預設新的array裡面全都是0
            double[] AC2_Time = new double[28];
            double[] AC3_Time = new double[28];
            double[] AC4_Time = new double[28];
            double[] AC5_Time = new double[28];
            double[] AC6_Time = new double[28];
            double[] Light_Time = new double[28];
            double[] Yesterday_Light = new double[28]; // 抓昨日的燈控的工作排程

            if (AC1_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++) // 如果資料表Rows大於0
                {
                    AC1_Time[x] = Convert.ToDouble(AC1_Schedule.Rows[0][x]);
                }
            }
            if (AC2_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    AC2_Time[x] = Convert.ToDouble(AC2_Schedule.Rows[0][x]);
                }
            }
            if (AC3_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    AC3_Time[x] = Convert.ToDouble(AC3_Schedule.Rows[0][x]);
                }
            }
            if (AC4_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    AC4_Time[x] = Convert.ToDouble(AC4_Schedule.Rows[0][x]);
                }
            }
            if (AC5_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    AC5_Time[x] = Convert.ToDouble(AC5_Schedule.Rows[0][x]);
                }
            }
            if (AC6_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    AC6_Time[x] = Convert.ToDouble(AC6_Schedule.Rows[0][x]);
                }
            }
            if (Light_Schedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    Light_Time[x] = Convert.ToDouble(Light_Schedule.Rows[0][x]);
                }
            }
            
            if (yesterdayLightSchedule.Rows.Count >= 1)
            {
                for (var x = 0; x <= 27; x++)
                {
                    Yesterday_Light[x] = Convert.ToDouble(yesterdayLightSchedule.Rows[0][x]);
                }
            }
            //命名一個陣列清單叫做ReturnArray
            ArrayList ReturnArray = new ArrayList();
            //有一個字典叫做temp_dict
            Dictionary<string, object> temp_dict;
            //這本temp_dict字典是<string, object>的類型(前面是名字，後面是裝的東西的類型，所以這個代表前面是字串，後面是東西)
            temp_dict = new Dictionary<string, object>
                {
                    { "AC1_Time" , AC1_Time },
                    { "AC2_Time" , AC2_Time },
                    { "AC3_Time" , AC3_Time },
                    { "AC4_Time" , AC4_Time },
                    { "AC5_Time" , AC5_Time },
                    { "AC6_Time" , AC6_Time },
                    { "Light_Time" , Light_Time },
                    { "Yesterday_Light" , Yesterday_Light }              
                };
            //應該是寫在哪裡說temp_dict這個叫temp_dict這個名字
            Console.WriteLine("temp_dict", temp_dict);
            //ReturnArray這個陣列裡面.塞進(temp_dict這個東西)
            ReturnArray.Add(temp_dict);
            //傳回ReturnArray
            return ReturnArray;
        }
    }
}