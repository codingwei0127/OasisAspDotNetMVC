using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class Excel
    {
        public DataTable New(string fff, string yyy)
        {

            DataTable dt5 = new DataTable();

            using (SqlConnection search12con = new SqlConnection("Data Source=140.124.47.155;Initial Catalog=Oasis;Persist Security Info=True;User ID=remote test;Password=123456"))
            //using (SqlConnection search12con = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Integrated Security=True"))
            //綠洲現場: using (SqlConnection search12con = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                //string helloworld = "SELECT TOP (1000) [Time],[CH2_kW/RT real],[CH_kW/RT max]FROM[FTIS_EPISTAR].[dbo].[CH_02 1day] WHERE Time>= ' " + fff + "' AND Time <= ' " + yyy + "'order by Time";
                //string helloworld = "SELECT TOP (1000) DATEADD(DAY, DATEDIFF(DAY, 0, Timea), 0) AS Timea,[T1a],[T2a],[Qa],[V],[C],[P],[PF] FROM [Hank].[dbo].[工作表2] WHERE Timea>= ' " + fff + "' AND Timea <= ' " + yyy + "'order by Timea";
                //string helloworld = "SELECT TOP (1000) DATEADD(DAY, DATEDIFF(DAY, 0, Timea), 0) AS Timea,[T1a],[T2a],[Qa],[V],[C],[P],[PF] FROM [Hank].[dbo].[工作表2] WHERE Timea>= ' " + fff + "' AND Timea <= ' " + yyy + "'order by Timea";
                //string helloworld = "SELECT TOP(1000)[Time],[Current],[Voltage],[Power] FROM[Sucket_insert].[dbo].[Meter] WHERE Time>= ' " + fff + "' AND Time <= ' " + yyy + "'order by Time";
                string helloworld = "SELECT TOP(100000)[Time],[Current],[Voltage],[Power] FROM[Oasis].[dbo].[Meter$] WHERE Time>= ' " + fff + "' AND Time <= DATEADD(DAY, 1, ' " + yyy + "' ) order by Time";

                SqlCommand sqlCommand5 = new SqlCommand(helloworld, search12con);
                SqlDataAdapter da5 = new SqlDataAdapter(sqlCommand5);
                da5.Fill(dt5);
            }
            return dt5;
        }
    }
}