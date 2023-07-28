using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class EnergyDirectionM
    {
        //能源流向分析讀資料
        public ArrayList GetData()
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] BuildingY = new string[RowsNum];
            //string[] BuildingM = new string[RowsNum];
            //string[] HvacY = new string[RowsNum];
            //string[] HvacM = new string[RowsNum];
            string[] BuildingY = { "2022", "2021" };
            string[] BuildingM = { "2022-06" };
            string[] HvacY = { "2022" };
            string[] HvacM = { "2022-06" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("BuildingY", BuildingY);
            temp_dict.Add("BuildingM", BuildingM);
            temp_dict.Add("HvacY", HvacY);
            temp_dict.Add("HvacM", HvacM);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //能源流向分析查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            //string SqlCommandString = "";
            //if (data["SearchType"] == "Building" && data["SearchTime"] == "Y") {
            //    SqlCommandString = "SELECT * FROM [EnergyDirection_year] WHERE DataTime LIKE '%'+@selectedTime+'%' AND Type = 'Y'";
            //}
            //else if (data["SearchType"] == "Building" && data["SearchTime"] == "M"){
            //    SqlCommandString = "SELECT * FROM [EnergyDirection_year] WHERE DataTime LIKE '%'+@selectedTime+'%' AND Type = 'M'";
            //}
            //else if (data["SearchType"] == "Hvac" && data["SearchTime"] == "Y"){
            //    SqlCommandString = "SELECT * FROM [AIRCONDITION_year] WHERE DataTime LIKE '%'+@selectedTime+'%' AND Type = 'Y'";
            //}
            //else if (data["SearchType"] == "Hvac" && data["SearchTime"] == "M"){
            //    SqlCommandString = "SELECT * FROM [AIRCONDITION_year] WHERE DataTime LIKE '%'+@selectedTime+'%' AND Type = 'M'";
            //}
            ////Read SQL
            //DataTable EDData = new DataTable();
            //if (SqlCommandString != "")
            //{
            //    SqlCommand ED_select = new SqlCommand(SqlCommandString, conn);
            //    ED_select.Parameters.AddWithValue("@selectedTime", data["SelectedTime"]);
            //    SqlDataAdapter EDAdapter = new SqlDataAdapter(ED_select);
            //    conn.Open();
            //    EDAdapter.Fill(EDData);
            //    conn.Close();
            //}

            //Declare Parameters
            //int ColsNum = EDData.Columns.Count;
            //List<double> LabelData = new List<double>(); //Total+ChartData
            //double[] ChartData = new double[ColsNum];


            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            //連接資料庫後刪除下面這段
            if (data["SearchType"] == "Building")
            {
                List<double> LabelData = new List<double>();
                double[] LabelStore2022 = { 23000, 13000, 2500, 2500, 2500, 2500 };
                double[] LabelStore2021 = { 5000, 1000, 1000, 1000, 1000, 1000 };
                List<double> ChartData = new List<double>();
                double[] Store2022 = { 13000, 2500, 2500, 2500, 2500 };
                double[] Store2021 = { 1000, 1000, 1000, 1000, 1000 };
                if (data["SelectedTime"] == "2022")
                {
                    for (int i = 0; i < 5; i++) ChartData.Add(Store2022[i]);
                    for (int i = 0; i < 6; i++) LabelData.Add(LabelStore2022[i]);
                }
                else if (data["SelectedTime"] == "2021")
                {
                    for (int i = 0; i < 5; i++) ChartData.Add(Store2021[i]);
                    for (int i = 0; i < 6; i++) LabelData.Add(LabelStore2021[i]);
                }
                else
                {
                    for (int i = 0; i < 5; i++) ChartData.Add(Store2022[i]);
                    for (int i = 0; i < 6; i++) LabelData.Add(LabelStore2022[i]);
                }
                temp_dict.Add("LabelData", LabelData);
                temp_dict.Add("ChartData", ChartData);
            }
            else
            {
                double[] LabelData = { 12912.57, 8195.2, 1420.6, 1114.5, 1169.28, 438.73, 574.26 };
                double[] ChartData = { 8195.2, 1420.6, 1114.5, 1169.28, 438.73, 574.26 };
                temp_dict.Add("LabelData", LabelData);
                temp_dict.Add("ChartData", ChartData);
            }
            //連接資料庫後刪除上面這段
            //temp_dict.Add("LabelData", ChartData);
            //temp_dict.Add("ChartData", ChartData);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}