using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Sockets;
using System.Web.Mvc;
using EMSdemo.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.Streaming;
using Modbus.Device;
using System.Linq;

namespace EMSdemo.Controllers
{
    public class HomeController : Controller
    {
        //首頁
        public ActionResult Index()
        {
            return View();
        }

        //首頁讀資料
        public JsonResult IndexVal()
        {
            IndexM IndexM = new IndexM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = IndexM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //首頁刷新資料
        public JsonResult UpdateIndexVal(Dictionary<string, string> data)
        {
            IndexM IndexM = new IndexM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = IndexM.UpdateData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //首頁即時需量圖表DPtime button變換顯示區間
        public JsonResult ChangeChartData(Dictionary<string, string> data)
        {
            IndexM IndexM = new IndexM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = IndexM.ChangeData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //即時資訊->工作排程
        public ActionResult AirSchedule()
        {
            return View();
        }

        //即時資訊讀資料
        public JsonResult InformationVal()
        {
            InformationM InformationM = new InformationM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = InformationM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //即時資訊圖表換冰機資料
        public JsonResult ChangeCHData(Dictionary<string, string> data)
        {
            InformationM InformationM = new InformationM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = InformationM.ChangeCH(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //報表輸出
        public ActionResult OutReport()
        {
            return View();
        }

        //報表輸出讀資料
        public JsonResult OutReportVal()
        {
            OutReportM OutReportM = new OutReportM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = OutReportM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //報表輸出產生月報表
        public JsonResult OutExcel(Dictionary<string, string> data)
        {
            OutReportM OutReportM = new OutReportM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = OutReportM.OutReport(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //報表輸出冰水機群組系統能源效率
        public JsonResult OutEffFile(Dictionary<string, string> data)
        {
            OutReportM OutReportM = new OutReportM();
            ArrayList ReturnArray = new ArrayList();
            if (data["FileType"] == "Excel") ReturnArray = OutReportM.EffExcel(data);
            else ReturnArray = OutReportM.EffWord(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //網頁輸出表格成Excel
        public JsonResult OutputTableReport(Dictionary<string, string> data, string[][] allArrays)
        {
            OutputTableM OutputTableM = new OutputTableM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = OutputTableM.GoExcel(data, allArrays);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }
    
        public JsonResult HistoryVal() // Home/Electricity_一樓總耗電highchart
        {
            HistoryM HistoryM = new HistoryM();
            ArrayList ReturnArray = HistoryM.GetHistoryData();
            return Json(ReturnArray);
        }

        public JsonResult OneFloorSwitch(string OneFloor_Mode) // Home/Electricity_一樓總耗電highchart
        {
            HistoryM HistoryM = new HistoryM();
            ArrayList ReturnArray = HistoryM.OneFloorElectricityChart(OneFloor_Mode);
            return Json(ReturnArray);
        }
        public JsonResult HistoryVal_search(string start, string end)
        {
            HistoryM HistoryM = new HistoryM();
            ArrayList ReturnArray = HistoryM.GetHistoryData_search(start, end);
            return Json(ReturnArray);
        }
        public JsonResult History_YearMonth(string Time, float Mode) // 傳入年分或月分
        {
            HistoryM HistoryM = new HistoryM();
            ArrayList ReturnArray = HistoryM.GetHistoryData_YearMonth(Time, Mode);
            return Json(ReturnArray);
        }
        public JsonResult schedule_sql()  // 顯示已設定的SQL排程時間
        {
            Schedule_SQL Schedule_SQL = new Schedule_SQL();
            ArrayList ReturnArray = Schedule_SQL.Search();
            return Json(ReturnArray);
        }
        public void schedule(Dictionary<string, string> data) // 讓設定好的排程時間進SQL
        {

            var array = data["start"].Split(',');

            var jilaymogun = "";
            if (array[28] == "1號冷氣")
            {
                jilaymogun = "1";
            }
            if (array[28] == "2號冷氣")
            {
                jilaymogun = "2";
            }
            if (array[28] == "3號冷氣")
            {
                jilaymogun = "3";
            }
            if (array[28] == "4號冷氣")
            {
                jilaymogun = "4";
            }
            if (array[28] == "5號冷氣")
            {
                jilaymogun = "5";
            }
            if (array[28] == "6號冷氣")
            {
                jilaymogun = "6";
            }
            if (array[28] == "燈具")
            {
                jilaymogun = "7";
            }



            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO [Oasis].[dbo].[schedule_" + jilaymogun + "] ([Time_0],[Time_1],[Time_2],[Time_3],[Time_4],[Time_5],[Time_6],[Time_7],[Time_8],[Time_9],[Time_10],[Time_11],[Time_12],[Time_13],[Time_14],[Time_15],[Time_16],[Time_17],[Time_18],[Time_19],[Time_20],[Time_21],[Time_22],[Time_23],[Time_24],[Time_25],[Time_26],[Time_27],[Current_Time]) VALUES (@value0," +
                //SqlCommand insert = new SqlCommand("INSERT INTO [coding_wei].[dbo].[schedule_" + jilaymogun + "] ([Time_0],[Time_1],[Time_2],[Time_3],[Time_4],[Time_5],[Time_6],[Time_7],[Time_8],[Time_9],[Time_10],[Time_11],[Time_12],[Time_13],[Time_14],[Time_15],[Time_16],[Time_17],[Time_18],[Time_19],[Time_20],[Time_21],[Time_22],[Time_23],[Time_24],[Time_25],[Time_26],[Time_27],[Current_Time]) VALUES (@value0," +
                    "@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11" +
                    ",@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22," +
                    "@value23,@value24,@value25,@value26,@value27,@value28)", Conn01);

                Conn01.Open();// 開啟連線

                for (var x = 0; x <= 27; x++) // 設定時間 0~27
                {
                    insert.Parameters.AddWithValue("@value" + x, array[x]);
                }
                insert.Parameters.AddWithValue("@value28", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }



        }


        [HttpPost]
        public ActionResult ExporttoExcel002(string start, string end) // Excel報表輸出
        {
            Excel excel = new Excel();
            DataTable excel02 = excel.New(start, end);

            SXSSFWorkbook hssfworkbook = new SXSSFWorkbook();

            SXSSFSheet sheet = (SXSSFSheet)hssfworkbook.CreateSheet("彙總表"); //新建資料
            //ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

            //設定樣式
            ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
            IFont headerfont = hssfworkbook.CreateFont();

            sheet.DefaultRowHeight = 15 * 25; //前面數字調高
            sheet.SetColumnWidth(0, 22 * 256);
            sheet.SetColumnWidth(1, 22 * 135);
            sheet.SetColumnWidth(2, 22 * 145);
            sheet.SetColumnWidth(3, 22 * 135);

            //sheet.GetRow(0).CreateCell(0).SetCellValue("Time");
            //sheet.GetRow(0).CreateCell(1).SetCellValue("Tchw_In(°C)");
            //sheet.GetRow(0).CreateCell(2).SetCellValue("Tchw_Out(°C)");
            //sheet.GetRow(0).CreateCell(3).SetCellValue("Flow(CMH)");

            var newDataFormat = hssfworkbook.CreateDataFormat();
            var style = hssfworkbook.CreateCellStyle();
            style.DataFormat = newDataFormat.GetFormat("yyyy/MM/dd HH:mm:ss");

            //綠洲:[Time],[Current],[Voltage],[Power]
            //List<string> strList = new List<string> { "Timea", "Tchw_In(°C)", "Tchw_Out(°C)", "Flow(CMH)", "RT" };
            List<string> strList = new List<string> { "時間", "電流(A)", "電壓(V)", "耗電量(kW)" };

            for (int j = 0; j < strList.Count; j++)
            {
                if (j == 0)
                    sheet.CreateRow(0).CreateCell(j).SetCellValue(strList[j]);
                else
                    sheet.GetRow(0).CreateCell(j).SetCellValue(strList[j]);
            }

            int i;
            for (i = 1; i <= excel02.Rows.Count; i++)
            {

                var row = sheet.CreateRow(i);

                row.CreateCell(1).SetCellValue((double)excel02.Rows[i - 1][1]);

                row.CreateCell(2).SetCellValue((double)excel02.Rows[i - 1][2]);

                row.CreateCell(3).SetCellValue((double)excel02.Rows[i - 1][3]);

                //row.CreateCell(4).SetCellValue((double)excel02.Rows[i - 1][4]);

                var cell = row.CreateCell(0);

                string d = excel02.Rows[i - 1][0].ToString();
                //IDataFormat dataFormatCustom = hssfworkbook.CreateDataFormat();
                //cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("yyyy-MM-dd HH:mm:ss");
                cell.SetCellValue(DateTime.Parse(d));
                cell.CellStyle = style;
            }
            var excelDatas = new MemoryStream();
            hssfworkbook.Write(excelDatas);
            return File(excelDatas.ToArray(), "application/vnd.ms-excel", string.Format($"城市綠洲數據.xlsx"));
        }


        public ActionResult Download() // Excel 報表輸出
        {
            return View();
        }
        public ActionResult History() // apexchart 耗電歷史查詢
        {
            return View();
        }


        
        public JsonResult HistoryVal_2(string start, string end)
        {
            HistoryM HistoryM = new HistoryM();
            ArrayList ReturnArray01 = HistoryM.GetData(start, end);
            return Json(ReturnArray01);
        }
        public ActionResult LightSchedule() // 照明排程
        {
            return View();
        }

        public ActionResult Identify() // 影像辨識
        {
            return View();
        }

        
        public ActionResult AirControl() // 即時控制_空調控制
        {
            return View();
        }

        
        public ActionResult LightControl() // 即時控制_照明控制
        {
            return View();
        }

        public JsonResult LightHistory() // 照明即時控制_當前開關狀態
        {
            LightOnOff LightOnOff = new LightOnOff();
            ArrayList ReturnArray = LightOnOff.LightData();
            return Json(ReturnArray);
        }
        public JsonResult LightHistory_Yesterday() // 照明即時控制_當前開關狀態
        {
            LightOnOff LightOnOff = new LightOnOff();
            ArrayList ReturnArray = LightOnOff.LightData_Yesterday();
            return Json(ReturnArray);
        }
        


        public void TryLa_2(string start, string end) // 測試傳值
        {

        }
        public ActionResult TryLa() // 測試畫面
        {
            return View();
        }

        public void In(Dictionary<string, string> data) // 照明即時控制
        {
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[Light] ([Time],[OnOff]) VALUES (@value0,@value1)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                var Float_LightMode = float.Parse(data["LightMode"]);
                insert.Parameters.AddWithValue("@value1", Float_LightMode);                  
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }

            TcpClient tcpClient3 = new TcpClient();
            tcpClient3.Connect("10.15.1.10", 503);
            ModbusIpMaster master3 = ModbusIpMaster.CreateIp(tcpClient3);
            master3.Transport.Retries = 0; 
            master3.Transport.ReadTimeout = 10000;

            string vIn = data["LightMode"];
            int vOut = Convert.ToInt32(vIn); // convert string to int
            vOut -= 1;

            bool vOut_2 = Convert.ToBoolean(vOut); // out should be: bool(1 or 0)
            master3.WriteSingleCoil(128, 0, vOut_2); // vOut_2: true 或是 false
        }

        // SQL排程器
        public void Timing_begins(Dictionary<string, string> data) // 讓設定好的排程時間進SQL
        {

            var array = data["start"].Split(',');

            var jilaymogun = "";
            if (array[28] == "燈具")
            {
                jilaymogun = "7";
            }
            else
            {
                jilaymogun = array[28].FirstOrDefault().ToString();
            }
        
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO [Oasis].[dbo].[schedule_" + jilaymogun + "] ([Time_0],[Time_1],[Time_2],[Time_3],[Time_4],[Time_5],[Time_6],[Time_7],[Time_8],[Time_9],[Time_10],[Time_11],[Time_12],[Time_13],[Time_14],[Time_15],[Time_16],[Time_17],[Time_18],[Time_19],[Time_20],[Time_21],[Time_22],[Time_23],[Time_24],[Time_25],[Time_26],[Time_27],[Current_Time]) VALUES (@value0," +
                    "@value1,@value2,@value3,@value4,@value5,@value6,@value7,@value8,@value9,@value10,@value11" +
                    ",@value12,@value13,@value14,@value15,@value16,@value17,@value18,@value19,@value20,@value21,@value22," +
                    "@value23,@value24,@value25,@value26,@value27,@value28)", Conn01);

                Conn01.Open();// 開啟連線

                for (var x = 0; x <= 27; x++) // 設定時間 0~27
                {
                    insert.Parameters.AddWithValue("@value" + x, array[x]);
                }
                insert.Parameters.AddWithValue("@value28", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }

        } // public void Timing_begins()

        public void AC1_Control(Dictionary<string, string> data) // 空調即時控制 
        {            
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[AC1$] ([Time],[Operation],[Mode],[Speed],[Temperture]) " +
                    "VALUES (@value0,@value1,@value2,@value3,@value4)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                // string to float 
                var Float1_OnOff = float.Parse(data["AC1_OnOff"]);
                insert.Parameters.AddWithValue("@value1", Float1_OnOff); // 冷氣開關

                insert.Parameters.AddWithValue("@value2", 0); // 看手冊!

                var Float1_Speed = float.Parse(data["AC1_Speed"]);
                insert.Parameters.AddWithValue("@value3", Float1_Speed); // 風量

                var Float1_Temp = float.Parse(data["AC1_Temp"]);
                insert.Parameters.AddWithValue("@value4", Float1_Temp); // 溫度

                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }

            TcpClient tcpClient = new TcpClient("10.15.1.10", 504);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 1; 
            master.Transport.ReadTimeout = 300; 

            ushort AC1_OnOff = Convert.ToUInt16(data["AC1_OnOff"]);
            master.WriteSingleRegister(1, 32, AC1_OnOff);

            ushort AC1_Temp = Convert.ToUInt16(data["AC1_Temp"]);
            master.WriteSingleRegister(1, 35, AC1_Temp);
        
            ushort AC1_Speed = Convert.ToUInt16(data["AC1_Speed"]); //請從手冊找風量的語法
            master.WriteSingleRegister(1, 34, AC1_Speed);
            /*
             *  1號開關(1,32,0~1)
             *  1號溫度(1,35,16~30)
             * */
        }

        public void AC2_Control(Dictionary<string, string> data) // 空調即時控制 
        {
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[AC2$] ([Time],[Operation],[Mode],[Speed],[Temperture]) " +
                    "VALUES (@value0,@value1,@value2,@value3,@value4)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.Parameters.AddWithValue("@value1", data["AC2_OnOff"]); // 冷氣開關            
                insert.Parameters.AddWithValue("@value2", 0); // 看手冊!
                insert.Parameters.AddWithValue("@value3", data["AC2_Speed"]); // 風量
                insert.Parameters.AddWithValue("@value4", data["AC2_Temp"]); // 溫度
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }
            TcpClient tcpClient = new TcpClient("10.15.1.10", 504);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 1;
            master.Transport.ReadTimeout = 300;

            ushort AC2_OnOff = Convert.ToUInt16(data["AC2_OnOff"]);
            master.WriteSingleRegister(2, 32, AC2_OnOff);

            ushort AC2_Temp = Convert.ToUInt16(data["AC2_Temp"]);
            master.WriteSingleRegister(2, 35, AC2_Temp);

            ushort AC2_Speed = Convert.ToUInt16(data["AC2_Speed"]); //請從手冊找風量的語法
            master.WriteSingleRegister(2, 34, AC2_Speed);
        }
        public void AC3_Control(Dictionary<string, string> data) // 空調即時控制 
        {
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[AC3$] ([Time],[Operation],[Mode],[Speed],[Temperture]) " +
                    "VALUES (@value0,@value1,@value2,@value3,@value4)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.Parameters.AddWithValue("@value1", data["AC3_OnOff"]); // 冷氣開關            
                insert.Parameters.AddWithValue("@value2", 0); // 看手冊!
                insert.Parameters.AddWithValue("@value3", data["AC3_Speed"]); // 風量
                insert.Parameters.AddWithValue("@value4", data["AC3_Temp"]); // 溫度
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }
            TcpClient tcpClient = new TcpClient("10.15.1.10", 504);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 1;
            master.Transport.ReadTimeout = 300;

            ushort AC3_OnOff = Convert.ToUInt16(data["AC3_OnOff"]);
            master.WriteSingleRegister(3, 32, AC3_OnOff);

            ushort AC3_Temp = Convert.ToUInt16(data["AC3_Temp"]);
            master.WriteSingleRegister(3, 35, AC3_Temp);

            ushort AC3_Speed = Convert.ToUInt16(data["AC3_Speed"]); //請從手冊找風量的語法
            master.WriteSingleRegister(3, 34, AC3_Speed);

        }
        public void AC4_Control(Dictionary<string, string> data) // 空調即時控制 
        {
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[AC4$] ([Time],[Operation],[Mode],[Speed],[Temperture]) " +
                    "VALUES (@value0,@value1,@value2,@value3,@value4)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.Parameters.AddWithValue("@value1", data["AC4_OnOff"]); // 冷氣開關            
                insert.Parameters.AddWithValue("@value2", 0); // 看手冊!
                insert.Parameters.AddWithValue("@value3", data["AC4_Speed"]); // 風量
                insert.Parameters.AddWithValue("@value4", data["AC4_Temp"]); // 溫度
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }
            TcpClient tcpClient = new TcpClient("10.15.1.10", 504);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 1;
            master.Transport.ReadTimeout = 300;

            ushort AC4_OnOff = Convert.ToUInt16(data["AC4_OnOff"]);
            master.WriteSingleRegister(4, 32, AC4_OnOff);

            ushort AC4_Temp = Convert.ToUInt16(data["AC4_Temp"]);
            master.WriteSingleRegister(4, 35, AC4_Temp);

            ushort AC4_Speed = Convert.ToUInt16(data["AC4_Speed"]); //請從手冊找風量的語法
            master.WriteSingleRegister(4, 34, AC4_Speed);
        }
        public void AC5_Control(Dictionary<string, string> data) // 空調即時控制 
        {
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[AC5$] ([Time],[Operation],[Mode],[Speed],[Temperture]) " +
                    "VALUES (@value0,@value1,@value2,@value3,@value4)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.Parameters.AddWithValue("@value1", data["AC5_OnOff"]); // 冷氣開關            
                insert.Parameters.AddWithValue("@value2", 0); // 看手冊!
                insert.Parameters.AddWithValue("@value3", data["AC5_Speed"]); // 風量
                insert.Parameters.AddWithValue("@value4", data["AC5_Temp"]); // 溫度
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }
            TcpClient tcpClient = new TcpClient("10.15.1.10", 504);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 1;
            master.Transport.ReadTimeout = 300;

            ushort AC5_OnOff = Convert.ToUInt16(data["AC5_OnOff"]);
            master.WriteSingleRegister(5, 32, AC5_OnOff);

            ushort AC5_Temp = Convert.ToUInt16(data["AC5_Temp"]);
            master.WriteSingleRegister(5, 35, AC5_Temp);

            ushort AC5_Speed = Convert.ToUInt16(data["AC5_Speed"]); //請從手冊找風量的語法
            master.WriteSingleRegister(5, 34, AC5_Speed);
        }
        public void AC6_Control(Dictionary<string, string> data) // 空調即時控制 
        {
            using (SqlConnection Conn01 = new SqlConnection("Data Source=B01-TMOF;Initial Catalog=Oasis;Persist Security Info=True;User ID=sa;Password=123456"))
            {
                SqlCommand insert = new SqlCommand("INSERT INTO " +
                    "[Oasis].[dbo].[AC6$] ([Time],[Operation],[Mode],[Speed],[Temperture]) " +
                    "VALUES (@value0,@value1,@value2,@value3,@value4)", Conn01);
                Conn01.Open();// 開啟連線
                insert.Parameters.AddWithValue("@value0", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                insert.Parameters.AddWithValue("@value1", data["AC6_OnOff"]); // 冷氣開關            
                insert.Parameters.AddWithValue("@value2", 0); // 看手冊!
                insert.Parameters.AddWithValue("@value3", data["AC6_Speed"]); // 風量
                insert.Parameters.AddWithValue("@value4", data["AC6_Temp"]); // 溫度
                insert.ExecuteNonQuery();//執行insert
                Conn01.Close();//斷開連線
            }
            TcpClient tcpClient = new TcpClient("10.15.1.10", 504);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(tcpClient);
            master.Transport.Retries = 1;
            master.Transport.ReadTimeout = 300;

            ushort AC6_OnOff = Convert.ToUInt16(data["AC6_OnOff"]);
            master.WriteSingleRegister(6, 32, AC6_OnOff);

            ushort AC6_Temp = Convert.ToUInt16(data["AC6_Temp"]);
            master.WriteSingleRegister(6, 35, AC6_Temp);

            ushort AC6_Speed = Convert.ToUInt16(data["AC6_Speed"]); //請從手冊找風量的語法
            master.WriteSingleRegister(6, 34, AC6_Speed);   
        }
        public JsonResult TOTAL_KWH()
        {
            HistoryM HistoryM = new HistoryM();
            ArrayList ReturnArray = HistoryM.TOTAL_KWH();
            return Json(ReturnArray);
        }
        public JsonResult Identify_SQL()  // 影像辨識_歷史紀錄表格
        {
            IdentifyM IdentifyM = new IdentifyM();
            ArrayList ReturnArray = IdentifyM.Identify_Search();
            return Json(ReturnArray);
        }

        //首頁讀即時時間
        public JsonResult RealTime()
        {
            IndexM IndexM = new IndexM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = IndexM.RealTimeData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }
    } // HomeController
}