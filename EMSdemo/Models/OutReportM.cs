using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class OutReportM
    {
        //報表輸出讀資料
        public ArrayList GetData()
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Month = new string[RowsNum];
            //string[] Year = new string[RowsNum];
            string[] Month = { "2022-06" };
            string[] Year = { "2022" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Month", Month);
            temp_dict.Add("Year", Year);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //報表輸出產生月報表
        public ArrayList OutReport(Dictionary<string, string> data)
        {
            //Read SQL



            //Output Report
            string TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\EleOutputReport.xlsx");
            string SheetName = "用電資訊";
            string filename = string.Format("OO案場{0}用電報表_{1}.xlsx", data["SearchMonth"], DateTime.Now.ToString("yyyyMMdd"));
            string SavePlace = "~/AllReport/OutputReportExcel/" + filename;

            FileStream file1 = new FileStream(TemplateFile, FileMode.Open, FileAccess.Read);//開啟讀取樣版檔
            XSSFWorkbook workbook = new XSSFWorkbook(file1);
            //新增試算表
            XSSFSheet sheet = (XSSFSheet)workbook.GetSheet(SheetName);

            //顏色----------------------------------------------------------------------------
            XSSFColor LightGreen = new XSSFColor();
            LightGreen.SetRgb(new byte[] { 226, 239, 218 });
            XSSFColor LightYellow = new XSSFColor();
            LightYellow.SetRgb(new byte[] { 255, 242, 204 });

            //字型----------------------------------------------------------------------------
            XSSFFont fontstyle1 = workbook.CreateFont() as XSSFFont;//微軟正黑體(表格內文字)
            fontstyle1.FontName = "微軟正黑體";

            XSSFFont fontstyle2 = workbook.CreateFont() as XSSFFont;//粗體字(表格合計數字)
            fontstyle2.FontName = "微軟正黑體";
            fontstyle2.IsBold = true;

            XSSFFont fontstyle3 = workbook.CreateFont() as XSSFFont;//微軟正黑體(最後一列趴數)
            fontstyle3.FontName = "微軟正黑體";
            fontstyle3.FontHeightInPoints = 10;

            //格式---------------------------------------------------------------------------
            XSSFCellStyle style1 = workbook.CreateCellStyle() as XSSFCellStyle;//微軟正黑體(表格內資料)
            style1.SetFont(fontstyle1);

            XSSFCellStyle LastTd = workbook.CreateCellStyle() as XSSFCellStyle;//微軟正黑體加底框線(表格最後一行)
            LastTd.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            LastTd.SetFont(fontstyle1);

            XSSFDataFormat Comma = (XSSFDataFormat)workbook.CreateDataFormat();
            XSSFCellStyle Comma2Value = workbook.CreateCellStyle() as XSSFCellStyle;//千分位+小數點2位
            Comma2Value.DataFormat = Comma.GetFormat("#,##0.00");
            Comma2Value.SetFont(fontstyle1);

            XSSFCellStyle LastTdComma2 = workbook.CreateCellStyle() as XSSFCellStyle;//千分位+小數點2位(表格最後一行)
            LastTdComma2.DataFormat = Comma.GetFormat("#,##0.00");
            LastTdComma2.SetFont(fontstyle1);
            LastTdComma2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            XSSFCellStyle TableSum = workbook.CreateCellStyle() as XSSFCellStyle;//表格合計
            TableSum.SetFillForegroundColor(LightGreen);
            TableSum.FillPattern = FillPattern.SolidForeground;
            TableSum.SetFont(fontstyle2);

            XSSFCellStyle BuildingSumComma2 = workbook.CreateCellStyle() as XSSFCellStyle;//表格合計千分位+小數點2位
            BuildingSumComma2.DataFormat = Comma.GetFormat("#,##0.00");
            BuildingSumComma2.SetFillForegroundColor(LightGreen);
            BuildingSumComma2.FillPattern = FillPattern.SolidForeground;
            BuildingSumComma2.SetFont(fontstyle2);

            XSSFCellStyle HvacSumComma2 = workbook.CreateCellStyle() as XSSFCellStyle;//表格合計千分位+小數點2位
            HvacSumComma2.DataFormat = Comma.GetFormat("#,##0.00");
            HvacSumComma2.SetFillForegroundColor(LightYellow);
            HvacSumComma2.FillPattern = FillPattern.SolidForeground;
            HvacSumComma2.SetFont(fontstyle2);

            XSSFDataFormat Percent = (XSSFDataFormat)workbook.CreateDataFormat();
            XSSFCellStyle BuildingPercent = workbook.CreateCellStyle() as XSSFCellStyle;//趴數小數點2位
            BuildingPercent.DataFormat = Comma.GetFormat("(#0.00 %)");
            BuildingPercent.SetFillForegroundColor(LightGreen);
            BuildingPercent.FillPattern = FillPattern.SolidForeground;
            BuildingPercent.SetFont(fontstyle3);

            XSSFCellStyle HvacPercent = workbook.CreateCellStyle() as XSSFCellStyle;//趴數小數點2位
            HvacPercent.DataFormat = Comma.GetFormat("(#0.00 %)");
            HvacPercent.SetFillForegroundColor(LightYellow);
            HvacPercent.FillPattern = FillPattern.SolidForeground;
            HvacPercent.SetFont(fontstyle3);

            //產生報表------------------------------------------------------------------------
            XSSFRow ReportTime = (XSSFRow)sheet.GetRow(3);
            ReportTime.CreateCell(2).SetCellValue(DateTime.Now.ToString("yyyy / MM / dd"));
            ReportTime.GetCell(2).CellStyle = style1;

            int StartRow = 6;
            int TableRowsNum = 4;//讀資料庫行數
            string[] Time = { "2022-06-20 10:45:00", "2022-06-21 11:15:00", "2022-06-22 11:30:00", "2022-06-23 12:45:00" };
            double[] Building = { 23000, 23000, 23000, 23000 };
            double[] VCB1 = { 13000, 13000, 13000, 13000 };
            double[] VCB2 = { 2500, 2500, 2500, 2500 };
            double[] VCB3 = { 2500, 2500, 2500, 2500 };
            double[] VCB4 = { 2500, 2500, 2500, 2500 };
            double[] BuildingOthers = { 2500, 2500, 2500, 2500 };
            double[] Chiller = { 6500, 6500, 6500, 6500 };
            double[] CHP = { 1500, 1500, 1500, 1500 };
            double[] CWP = { 1500, 1500, 1500, 1500 };
            double[] ZP = { 1500, 1500, 1500, 1500 };
            double[] CT = { 1500, 1500, 1500, 1500 };
            double[] HvacOthers = { 500, 500, 500, 500 };
            double TotalBuilding = 0, TotalVCB1 = 0, TotalVCB2 = 0, TotalVCB3 = 0, TotalVCB4 = 0, TotalBuildingOthers = 0;
            double TotalChiller = 0, TotalCHP = 0, TotalCWP = 0, TotalZP = 0, TotalCT = 0, TotalHvacOthers = 0;
            for (int i = 0; i < TableRowsNum; i++)
            {
                TotalBuilding = TotalBuilding + Building[i];
                TotalVCB1 = TotalVCB1 + VCB1[i];
                TotalVCB2 = TotalVCB2 + VCB2[i];
                TotalVCB3 = TotalVCB3 + VCB3[i];
                TotalVCB4 = TotalVCB4 + VCB4[i];
                TotalBuildingOthers = TotalBuildingOthers + BuildingOthers[i];
                TotalChiller = TotalChiller + Chiller[i];
                TotalCHP = TotalCHP + CHP[i];
                TotalCWP = TotalCWP + CWP[i];
                TotalZP = TotalZP + ZP[i];
                TotalCT = TotalCT + CT[i];
                TotalHvacOthers = TotalHvacOthers + HvacOthers[i];

                XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                headerRow.CreateCell(1).SetCellValue(Time[i]);
                headerRow.CreateCell(2).SetCellValue(Building[i]);
                headerRow.CreateCell(3).SetCellValue(VCB1[i]);
                headerRow.CreateCell(4).SetCellValue(VCB2[i]);
                headerRow.CreateCell(5).SetCellValue(VCB3[i]);
                headerRow.CreateCell(6).SetCellValue(VCB4[i]);
                headerRow.CreateCell(7).SetCellValue(BuildingOthers[i]);
                headerRow.CreateCell(8).SetCellValue(Chiller[i]);
                headerRow.CreateCell(9).SetCellValue(CHP[i]);
                headerRow.CreateCell(10).SetCellValue(CWP[i]);
                headerRow.CreateCell(11).SetCellValue(ZP[i]);
                headerRow.CreateCell(12).SetCellValue(CT[i]);
                headerRow.CreateCell(13).SetCellValue(HvacOthers[i]);
                if (i == TableRowsNum - 1)
                {
                    headerRow.GetCell(1).CellStyle = LastTd;
                    for (int col = 2; col <= 13; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                }
                else
                {
                    headerRow.GetCell(1).CellStyle = style1;
                    for (int col = 2; col <= 13; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                }
                StartRow = StartRow + 1;
            }
            //Summary
            XSSFRow SummaryRow = (XSSFRow)sheet.CreateRow(StartRow);
            SummaryRow.CreateCell(1).SetCellValue("總計");
            SummaryRow.CreateCell(2).SetCellValue(TotalBuilding);
            SummaryRow.CreateCell(3).SetCellValue(TotalVCB1);
            SummaryRow.CreateCell(4).SetCellValue(TotalVCB2);
            SummaryRow.CreateCell(5).SetCellValue(TotalVCB3);
            SummaryRow.CreateCell(6).SetCellValue(TotalVCB4);
            SummaryRow.CreateCell(7).SetCellValue(TotalBuildingOthers);
            SummaryRow.CreateCell(8).SetCellValue(TotalChiller);
            SummaryRow.CreateCell(9).SetCellValue(TotalCHP);
            SummaryRow.CreateCell(10).SetCellValue(TotalCWP);
            SummaryRow.CreateCell(11).SetCellValue(TotalZP);
            SummaryRow.CreateCell(12).SetCellValue(TotalCT);
            SummaryRow.CreateCell(13).SetCellValue(TotalHvacOthers);
            SummaryRow.GetCell(1).CellStyle = TableSum;
            for (int col = 2; col <= 7; col++) SummaryRow.GetCell(col).CellStyle = BuildingSumComma2;
            for (int col = 8; col <= 13; col++) SummaryRow.GetCell(col).CellStyle = HvacSumComma2;
            StartRow = StartRow + 1;
            //Percent
            double TotalBuildingDominator = TotalBuilding;
            double TotalVCB1Dominator = TotalVCB1;
            if (TotalBuildingDominator == 0) TotalBuildingDominator = 1;
            if (TotalVCB1Dominator == 0) TotalVCB1Dominator = 1;
            XSSFRow PercentRow = (XSSFRow)sheet.CreateRow(StartRow);
            PercentRow.CreateCell(1).SetCellValue("");
            PercentRow.CreateCell(2).SetCellValue(TotalBuilding / TotalBuildingDominator);
            PercentRow.CreateCell(3).SetCellValue(TotalVCB1 / TotalBuildingDominator);
            PercentRow.CreateCell(4).SetCellValue(TotalVCB2 / TotalBuildingDominator);
            PercentRow.CreateCell(5).SetCellValue(TotalVCB3 / TotalBuildingDominator);
            PercentRow.CreateCell(6).SetCellValue(TotalVCB4 / TotalBuildingDominator);
            PercentRow.CreateCell(7).SetCellValue(TotalBuildingOthers / TotalBuildingDominator);
            PercentRow.CreateCell(8).SetCellValue(TotalChiller / TotalVCB1Dominator);
            PercentRow.CreateCell(9).SetCellValue(TotalCHP / TotalVCB1Dominator);
            PercentRow.CreateCell(10).SetCellValue(TotalCWP / TotalVCB1Dominator);
            PercentRow.CreateCell(11).SetCellValue(TotalZP / TotalVCB1Dominator);
            PercentRow.CreateCell(12).SetCellValue(TotalCT / TotalVCB1Dominator);
            PercentRow.CreateCell(13).SetCellValue(TotalHvacOthers / TotalVCB1Dominator);
            PercentRow.GetCell(1).CellStyle = TableSum;
            for (int col = 2; col <= 7; col++) PercentRow.GetCell(col).CellStyle = BuildingPercent;
            for (int col = 8; col <= 13; col++) PercentRow.GetCell(col).CellStyle = HvacPercent;

            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(SavePlace), FileMode.Create);
            workbook.Write(file);
            file.Close();

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("filename", filename);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //報表輸出冰水機群組系統能源效率Excel
        public ArrayList EffExcel(Dictionary<string, string> data)
        {
            //Read SQL



            //Output Report
            string TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\ChillersEfficiency.xlsx");
            string SheetName = "冰水機群組系統能源效率";
            string filename = string.Format("OO案場{0}年度冰水機群組系統能源效率報表_{1}.xlsx", data["SearchYear"], DateTime.Now.ToString("yyyyMMdd"));
            string SavePlace = "~/AllReport/ChillersEffExcel/" + filename;

            FileStream file1 = new FileStream(TemplateFile, FileMode.Open, FileAccess.Read);//開啟讀取樣版檔
            XSSFWorkbook workbook = new XSSFWorkbook(file1);
            //新增試算表
            XSSFSheet sheet = (XSSFSheet)workbook.GetSheet(SheetName);

            //顏色----------------------------------------------------------------------------
            XSSFColor LightGreen = new XSSFColor();
            LightGreen.SetRgb(new byte[] { 226, 239, 218 });

            //字型----------------------------------------------------------------------------
            XSSFFont fontstyle1 = workbook.CreateFont() as XSSFFont;//微軟正黑體(表格內文字)
            fontstyle1.FontName = "微軟正黑體";

            XSSFFont fontstyle2 = workbook.CreateFont() as XSSFFont;//粗體字(表格合計數字)
            fontstyle2.FontName = "微軟正黑體";
            fontstyle2.IsBold = true;

            //格式---------------------------------------------------------------------------
            XSSFCellStyle style1 = workbook.CreateCellStyle() as XSSFCellStyle;//微軟正黑體(表格內資料)
            style1.SetFont(fontstyle1);
            style1.Alignment = HorizontalAlignment.Left;

            XSSFDataFormat Comma = (XSSFDataFormat)workbook.CreateDataFormat();
            XSSFCellStyle Comma2Value = workbook.CreateCellStyle() as XSSFCellStyle;//千分位+小數點2位
            Comma2Value.DataFormat = Comma.GetFormat("#,##0.00");
            Comma2Value.SetFont(fontstyle1);

            XSSFCellStyle LastTdComma2 = workbook.CreateCellStyle() as XSSFCellStyle;//千分位+小數點2位(表格最後一行)
            LastTdComma2.DataFormat = Comma.GetFormat("#,##0.00");
            LastTdComma2.SetFont(fontstyle1);
            LastTdComma2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            XSSFCellStyle SumComma2 = workbook.CreateCellStyle() as XSSFCellStyle;//表格合計千分位+小數點2位
            SumComma2.DataFormat = Comma.GetFormat("#,##0.00");
            SumComma2.SetFillForegroundColor(LightGreen);
            SumComma2.FillPattern = FillPattern.SolidForeground;
            SumComma2.SetFont(fontstyle2);

            //產生報表------------------------------------------------------------------------
            XSSFRow ReportTime = (XSSFRow)sheet.GetRow(3);
            ReportTime.CreateCell(2).SetCellValue(DateTime.Now.ToString("yyyy / MM / dd"));
            ReportTime.GetCell(2).CellStyle = style1;
            ReportTime.CreateCell(4).SetCellValue(Convert.ToInt32(data["SearchYear"]));
            ReportTime.GetCell(4).CellStyle = style1;

            int StartRow = 6;
            double[] kWh = { 156432, 154329, 153755, 153755, 165432, 153755, 0, 0, 0, 0, 0, 0 };
            double[] RT = { 210000, 220000, 219650, 219650, 221650, 228065, 0, 0, 0, 0, 0, 0 };
            double[] kWRT = { 0.745, 0.701, 0.7, 0.7, 0.746, 0.674, 0, 0, 0, 0, 0, 0 };
            double TotalkWh = 0, TotalRT = 0, TotalkWRT = 0;
            for (int i = 0; i < 12; i++)
            {
                TotalkWh = TotalkWh + kWh[i];
                TotalRT = TotalRT + RT[i];
                XSSFRow headerRow = (XSSFRow)sheet.GetRow(StartRow);
                headerRow.CreateCell(2).SetCellValue(kWh[i]);
                headerRow.CreateCell(3).SetCellValue(RT[i]);
                headerRow.CreateCell(4).SetCellValue(kWRT[i]);
                if (i == 11) for (int col = 2; col <= 4; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                else for (int col = 2; col <= 4; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                StartRow = StartRow + 1;
            }
            double TotalRTDominator = TotalRT;
            if (TotalRTDominator == 0) TotalRTDominator = 1;
            TotalkWRT = TotalkWh / TotalRT;
            XSSFRow SummaryRow = (XSSFRow)sheet.GetRow(StartRow);
            SummaryRow.CreateCell(2).SetCellValue(TotalkWh);
            SummaryRow.CreateCell(3).SetCellValue(TotalRT);
            SummaryRow.CreateCell(4).SetCellValue(TotalkWRT);
            for (int col = 2; col <= 4; col++) SummaryRow.GetCell(col).CellStyle = SumComma2;

            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(SavePlace), FileMode.Create);
            workbook.Write(file);
            file.Close();

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("filename", filename);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //報表輸出冰水機群組系統能源效率Excel
        public ArrayList EffWord(Dictionary<string, string> data)
        {
            //Read SQL



            //Output Report
            string TemplateFile = HttpContext.Current.Server.MapPath("~\\WordReportExample\\ChillersEfficiency.docx");
            string filename = string.Format("OO案場{0}年度冰水機群組系統能源效率報表_{1}.docx", data["SearchYear"], DateTime.Now.ToString("yyyyMMdd"));
            string SavePlace = "~/AllReport/ChillersEffWord/" + filename;

            FileStream file1 = new FileStream(TemplateFile, FileMode.Open, FileAccess.Read);//開啟讀取樣版檔
            XWPFDocument m_Docx = new XWPFDocument(file1);

            //產生報表------------------------------------------------------------------------
            double[] kWh = { 156432, 154329, 153755, 153755, 165432, 153755, 0, 0, 0, 0, 0, 0 };
            double[] RT = { 210000, 220000, 219650, 219650, 221650, 228065, 0, 0, 0, 0, 0, 0 };
            double[] kWRT = { 0.745, 0.701, 0.7, 0.7, 0.746, 0.674, 0, 0, 0, 0, 0, 0 };
            foreach (var para in m_Docx.Paragraphs)
            {
                string oldtext = para.ParagraphText;
                string newTime = DateTime.Now.ToString("yyyy / MM / dd");
                string outputYear = data["SearchYear"];
                //以下為替換文件模版中的關鍵字
                para.ReplaceText("#TIME#", newTime);
                para.ReplaceText("#YEAR#", outputYear);
            }
            var Table = m_Docx.Tables[2];
            for (int row = 0; row < 12; row++)
            {
                Table.GetRow(row + 1).GetCell(2).SetText(string.Format("{0:N2}", kWh[row]));
                Table.GetRow(row + 1).GetCell(3).SetText(string.Format("{0:N2}", RT[row]));
                Table.GetRow(row + 1).GetCell(4).SetText(string.Format("{0:N2}", kWRT[row]));
            }

            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(HttpContext.Current.Server.MapPath(SavePlace), FileMode.Create);
            m_Docx.Write(file);
            file.Close();
            file.Dispose();
            file1.Close();
            file1.Dispose();

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("filename", filename);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}