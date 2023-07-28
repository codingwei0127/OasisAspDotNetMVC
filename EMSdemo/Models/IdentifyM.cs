using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class IdentifyM
    {
        public ArrayList Identify_Search() 
        {

            DataTable IdentifyRecord = new DataTable();
            string Top10_IdentifyRecord = "SELECT TOP (10) [Time],[People],[Speed]FROM[Oasis].[dbo].[Identify] ORDER BY Time DESC";

            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand cmd01 = new SqlCommand(Top10_IdentifyRecord, Conn01);
                SqlDataAdapter bas1 = new SqlDataAdapter(cmd01);

                Conn01.Open();
                bas1.Fill(IdentifyRecord);
                Conn01.Close();
            }

            string[] Time = new string[10];
            double[] People = new double[10];
            double[] Speed = new double[10];


            if (IdentifyRecord.Rows.Count >= 1)
            {
                for (var x = 0; x <= 9; x++) 
                {
                    Time[x] = IdentifyRecord.Rows[x][0].ToString().Replace(" ", "");
                    People[x] = Convert.ToDouble(IdentifyRecord.Rows[x][1]);
                    Speed[x] = Convert.ToDouble(IdentifyRecord.Rows[x][2]);
                }
            }

            //命名一個陣列清單叫做ReturnArray
            ArrayList ReturnArray = new ArrayList();
            //有一個字典叫做temp_dict
            Dictionary<string, object> temp_dict;
            //這本temp_dict字典是<string, object>的類型(前面是名字，後面是裝的東西的類型，所以這個代表前面是字串，後面是東西)
            temp_dict = new Dictionary<string, object>
                {
                    { "Time" , Time },
                    { "People" , People },
                    { "Speed" , Speed }
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