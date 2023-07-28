using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class LightOnOff
    {
        public ArrayList LightData()
        {

            DataTable LightDataTable = new DataTable();
            //using (SqlConnection LightConn = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            using (SqlConnection LightConn = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                string LightHistoryData = "SELECT TOP(1000)[Time],[OnOff] FROM[Oasis].[dbo].[Light]" +
                    "WHERE(Time >= convert(varchar(10), dateadd(d, -7, Getdate()), 120)" +
                    "and Time <= convert(varchar(10), dateadd(d, 1, Getdate()), 120))ORDER BY Time DESC";
                SqlCommand LightCmd = new SqlCommand(LightHistoryData, LightConn);
                //bas3存放上面那個命令(cmd03)
                SqlDataAdapter Lightbas = new SqlDataAdapter(LightCmd);
                //開啟Conn03的連線
                LightConn.Open();
                //bas3裡面的東西塞進dt3
                Lightbas.Fill(LightDataTable);
                //關閉Conn03的連線
                LightConn.Close();
            }

            int Row = LightDataTable.Rows.Count;
            string[] Time = new string[Row];
            double[] OnOff = new double[Row];

            if (LightDataTable.Rows.Count > 0)
            {
                for (int i = 0; i < Row; i++)
                {
                    Time[i] = LightDataTable.Rows[i][0].ToString().Replace(" ", "");
                    OnOff[i] = Convert.ToDouble(LightDataTable.Rows[i][1]);
                }
            }

            ArrayList ReturnArray = new ArrayList();         
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>
            {
                { "Time", Time },
                { "OnOff", OnOff },
                { "Row", Row }
            };
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        public ArrayList LightData_Yesterday()
        {

            DataTable LightOnTable = new DataTable();
            DataTable LightOffTable = new DataTable();

            //using (SqlConnection LightConn = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            using (SqlConnection LightConn = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))

            {
                string YesterdayOn_Time = "SELECT TOP(1)[Time],[OnOff] FROM[Oasis].[dbo].[Light]WHERE(Time >= convert(varchar(10), dateadd(d, -1, Getdate()), 120)and Time < convert(varchar(10), dateadd(d, 0, Getdate()), 120))  AND[OnOff] = 2 ORDER BY Time DESC";
                string YesterdayOff_Time = "SELECT TOP (1) [Time],[OnOff]FROM[Oasis].[dbo].[Light]WHERE(Time >= convert(varchar(10), dateadd(d, -1, Getdate()), 120)and Time < convert(varchar(10), dateadd(d, 0, Getdate()), 120))  AND[OnOff] = 1ORDER BY Time DESC";

                SqlCommand Oncmd = new SqlCommand(YesterdayOn_Time, LightConn);
                SqlDataAdapter Onbas = new SqlDataAdapter(Oncmd);

                SqlCommand Offcmd = new SqlCommand(YesterdayOff_Time, LightConn);
                SqlDataAdapter Offbas = new SqlDataAdapter(Offcmd);

                LightConn.Open();
                Onbas.Fill(LightOnTable);
                Offbas.Fill(LightOffTable);
                LightConn.Close();
            }

            int OnRow = LightOnTable.Rows.Count;
            string[] TimeOn = new string[OnRow];
            double[] On = new double[OnRow];

            int OffRow = LightOffTable.Rows.Count;
            string[] TimeOff = new string[OffRow];
            double[] Off = new double[OffRow];

            if (OnRow > 0)
            {
                TimeOn[0] = LightOnTable.Rows[0][0].ToString().Replace(" ", "");
                On[0] = Convert.ToDouble(LightOnTable.Rows[0][1]);
            }
            if (OffRow > 0)
            {
                TimeOff[0] = LightOffTable.Rows[0][0].ToString().Replace(" ", "");
                Off[0] = Convert.ToDouble(LightOffTable.Rows[0][1]);                
            }

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>
            {
                { "TimeOn", TimeOn },
                { "TimeOff", TimeOff }
            };
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }


    }
}