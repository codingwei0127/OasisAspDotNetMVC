using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class OutputTableM
    {
        public ArrayList GoExcel(Dictionary<string, string> data, string[][] allArrays)
        {
            string[] TableData = allArrays[0];
            string TemplateFile = "", SheetName = "", filename = "", SavePlace = "";
            if (data["outputType"] == "EleWarning")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\EleWarningReport.xlsx");
                SheetName = "用電異常紀錄";
                filename = string.Format("OO案場用電異常紀錄報表_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/EleWarningReport/" + filename;
            }
            else if (data["outputType"] == "EffWarning")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\EffWarningReport.xlsx");
                SheetName = "性能異常紀錄";
                filename = string.Format("OO案場{0}性能異常紀錄報表_{1}.xlsx", data["option"], DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/EffWarningReport/" + filename;
            }
            else if (data["outputType"] == "DPWarning")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\DPWarningReport.xlsx");
                SheetName = "需量超約紀錄";
                filename = string.Format("OO案場需量超約紀錄報表_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/DPWarningReport/" + filename;
            }
            else if (data["outputType"] == "EleBaseline")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\EleBaselineReport.xlsx");
                SheetName = "用電基準分析";
                filename = string.Format("OO案場用電基準分析報表_{0}.xlsx", DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/EleBaselineReport/" + filename;
            }
            else if (data["outputType"] == "COPBaseline")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\COPBaselineReport.xlsx");
                SheetName = "空調性能分析";
                filename = string.Format("OO案場{0}空調性能分析報表_{1}.xlsx", data["SelectedCH"], DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/COPBaselineReport/" + filename;
            }
            else if (data["outputType"] == "EleHistory")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\EleHistoryReport.xlsx");
                SheetName = "用電歷史資訊";
                filename = string.Format("OO案場{0}用電歷史報表_{1}.xlsx", data["option"], DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/EleHistoryReport/" + filename;
            }
            else if (data["outputType"] == "ACHistory")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\ACHistoryReport.xlsx");
                SheetName = "空調系統資訊";
                filename = string.Format("OO案場空調系統{0}資訊報表_{1}.xlsx", data["equipmentID"], DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/ACHistoryReport/" + filename;
            }
            else if (data["outputType"] == "SBData")
            {
                TemplateFile = HttpContext.Current.Server.MapPath("~\\ExcelReportExample\\SBDataReport.xlsx");
                SheetName = "電盤資訊";
                filename = string.Format("OO案場{0}電盤資訊{1}報表_{2}.xlsx", data["SelectedTime"], data["SelectPeriod"], DateTime.Now.ToString("yyyyMMdd"));
                SavePlace = "~/AllReport/SBDataReport/" + filename;
            }

            FileStream file1 = new FileStream(TemplateFile, FileMode.Open, FileAccess.Read);//開啟讀取樣版檔
            XSSFWorkbook workbook = new XSSFWorkbook(file1);
            //新增試算表
            XSSFSheet sheet = (XSSFSheet)workbook.GetSheet(SheetName);

            //顏色----------------------------------------------------------------------------
            XSSFColor LightBlue = new XSSFColor();
            LightBlue.SetRgb(new byte[] { 221, 235, 247 });
            XSSFColor LightGreen = new XSSFColor();
            LightGreen.SetRgb(new byte[] { 198, 224, 180 });

            //字型----------------------------------------------------------------------------
            XSSFFont fontstyle1 = workbook.CreateFont() as XSSFFont;//微軟正黑體(表格內文字)
            fontstyle1.FontName = "微軟正黑體";
            XSSFFont fontstyle2 = workbook.CreateFont() as XSSFFont;//粗體微軟正黑體(表格合計數字)
            fontstyle2.FontName = "微軟正黑體";
            fontstyle2.IsBold = true;

            //格式---------------------------------------------------------------------------
            XSSFCellStyle ACHistoryTitle = workbook.CreateCellStyle() as XSSFCellStyle;//綠色標題(空調系統查詢表頭)
            ACHistoryTitle.SetFillForegroundColor(LightGreen);
            ACHistoryTitle.FillPattern = FillPattern.SolidForeground;
            ACHistoryTitle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Double;
            ACHistoryTitle.SetFont(fontstyle1);

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

            XSSFCellStyle TableSum = workbook.CreateCellStyle() as XSSFCellStyle;//微軟正黑體(表格合計粗體字數字)
            TableSum.SetFillForegroundColor(LightBlue);
            TableSum.FillPattern = FillPattern.SolidForeground;
            TableSum.SetFont(fontstyle2);

            XSSFCellStyle TableSumComma2 = workbook.CreateCellStyle() as XSSFCellStyle;//千分位+小數點2位(表格合計粗體字數字)
            TableSumComma2.SetFillForegroundColor(LightBlue);
            TableSumComma2.FillPattern = FillPattern.SolidForeground;
            TableSumComma2.DataFormat = Comma.GetFormat("#,##0.00");
            TableSumComma2.SetFont(fontstyle2);

            //產生報表------------------------------------------------------------------------
            XSSFRow ReportTime = (XSSFRow)sheet.GetRow(3);
            ReportTime.CreateCell(2).SetCellValue(DateTime.Now.ToString("yyyy / MM / dd"));
            ReportTime.GetCell(2).CellStyle = style1;

            int TableRows = Convert.ToInt32(data["tbodyrows"]);
            int TableCols = Convert.ToInt32(data["tbodycols"]);
            //產生報表表格--------------------------------------------------------------------
            //用電異常紀錄
            if (data["outputType"] == "EleWarning")
            {
                int StartRow = 6;
                for (int i = 0; i < TableRows; i++)
                {
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                    headerRow.CreateCell(1).SetCellValue(TableData[6 * i].ToString());
                    for (int col = 1; col <= 4; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[6 * i + col]));
                    headerRow.CreateCell(6).SetCellValue(TableData[6 * i + 5].ToString());
                    if (i == TableRows - 1)
                    {
                        headerRow.GetCell(1).CellStyle = LastTd;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                        headerRow.GetCell(6).CellStyle = LastTd;
                    }
                    else
                    {
                        headerRow.GetCell(1).CellStyle = style1;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                        headerRow.GetCell(6).CellStyle = style1;
                    }
                    StartRow = StartRow + 1;
                }
            }
            //性能異常紀錄&需量超約紀錄
            else if (data["outputType"] == "EffWarning" || data["outputType"] == "DPWarning")
            {
                //性能異常紀錄選取的冰機
                if (data["outputType"] == "EffWarning")
                {
                    ReportTime.CreateCell(4).SetCellValue(data["option"]);
                    ReportTime.GetCell(4).CellStyle = style1;
                }

                int StartRow = 6;
                for (int i = 0; i < TableRows; i++)
                {
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                    headerRow.CreateCell(1).SetCellValue(TableData[4 * i].ToString());
                    for (int col = 1; col <= 2; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[4 * i + col]));
                    headerRow.CreateCell(4).SetCellValue(TableData[4 * i + 3].ToString());
                    if (i == TableRows - 1)
                    {
                        headerRow.GetCell(1).CellStyle = LastTd;
                        for (int col = 2; col <= 3; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                        headerRow.GetCell(4).CellStyle = LastTd;
                    }
                    else
                    {
                        headerRow.GetCell(1).CellStyle = style1;
                        for (int col = 2; col <= 3; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                        headerRow.GetCell(4).CellStyle = style1;
                    }
                    StartRow = StartRow + 1;
                }
            }
            //用電基準分析
            else if (data["outputType"] == "EleBaseline")
            {
                int StartRow = 6;
                for (int i = 0; i < TableRows; i++)
                {
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                    headerRow.CreateCell(1).SetCellValue(TableData[6 * i]);
                    for (int col = 1; col <= 4; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[6 * i + col]));
                    headerRow.CreateCell(6).SetCellValue(TableData[6 * i + 5]);
                    if (i == TableRows - 2)
                    {
                        headerRow.GetCell(1).CellStyle = LastTd;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                        headerRow.GetCell(6).CellStyle = LastTd;
                    }
                    else if (i == TableRows - 1)
                    {
                        headerRow.GetCell(1).CellStyle = TableSum;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = TableSumComma2;
                        headerRow.GetCell(6).CellStyle = TableSum;
                    }
                    else
                    {
                        headerRow.GetCell(1).CellStyle = style1;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                        headerRow.GetCell(6).CellStyle = style1;
                    }
                    StartRow = StartRow + 1;
                }
            }
            //空調性能分析
            else if (data["outputType"] == "COPBaseline")
            {
                //空調性能分析選取的冰機
                ReportTime.CreateCell(4).SetCellValue(data["SelectedCH"]);
                ReportTime.GetCell(4).CellStyle = style1;

                int StartRow = 6;
                for (int i = 0; i < TableRows; i++)
                {
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                    headerRow.CreateCell(1).SetCellValue(TableData[4 * i].ToString());
                    for (int col = 1; col <= 3; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[4 * i + col]));
                    if (i == TableRows - 1)
                    {
                        headerRow.GetCell(1).CellStyle = LastTd;
                        for (int col = 2; col <= 4; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                    }
                    else
                    {
                        headerRow.GetCell(1).CellStyle = style1;
                        for (int col = 2; col <= 4; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                    }
                    StartRow = StartRow + 1;
                }
            }
            //用電歷史查詢
            else if (data["outputType"] == "EleHistory")
            {
                //用電歷史查詢選取的電盤
                XSSFRow ReportBoard = (XSSFRow)sheet.GetRow(4);
                ReportBoard.CreateCell(2).SetCellValue(data["option"]);
                ReportBoard.GetCell(2).CellStyle = style1;

                int StartRow = 7;
                for (int i = 0; i < TableRows; i++)
                {
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                    headerRow.CreateCell(1).SetCellValue(TableData[3 * i].ToString());
                    for (int col = 1; col <= 2; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[3 * i + col]));
                    if (i == TableRows - 1)
                    {
                        headerRow.GetCell(1).CellStyle = LastTd;
                        for (int col = 2; col <= 3; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                    }
                    else
                    {
                        headerRow.GetCell(1).CellStyle = style1;
                        for (int col = 2; col <= 3; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                    }
                    StartRow = StartRow + 1;
                }
            }
            //空調系統查詢
            else if (data["outputType"] == "ACHistory")
            {
                string[] TheadContent = allArrays[1];
                //空調系統查詢選取的設備
                XSSFRow ReportBoard = (XSSFRow)sheet.GetRow(3);
                ReportBoard.CreateCell(4).SetCellValue(data["equipmentID"]);
                ReportBoard.GetCell(4).CellStyle = style1;
                //表格表頭
                XSSFRow TheadRow = (XSSFRow)sheet.CreateRow(5);
                for (int col = 0; col < Convert.ToInt32(data["theadcols"]); col++) TheadRow.CreateCell(col + 1).SetCellValue(TheadContent[col]);
                for (int col = 0; col < Convert.ToInt32(data["theadcols"]); col++) TheadRow.GetCell(col + 1).CellStyle = ACHistoryTitle;
                //表格內容
                int StartRow = 6;
                //冰機
                if (data["equipmentID"] == "CH1" || data["equipmentID"] == "CH2" || data["equipmentID"] == "CH3" || data["equipmentID"] == "CHSP")
                {
                    for (int i = 0; i < TableRows; i++)
                    {
                        XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                        for (int col = 0; col <= 1; col++) headerRow.CreateCell(col + 1).SetCellValue(TableData[8 * i + col].ToString());
                        for (int col = 2; col <= 7; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[8 * i + col]));
                        if (i == TableRows - 1)
                        {
                            for (int col = 1; col <= 2; col++) headerRow.GetCell(col).CellStyle = LastTd;
                            for (int col = 3; col <= 8; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                        }
                        else
                        {
                            for (int col = 1; col <= 2; col++) headerRow.GetCell(col).CellStyle = style1;
                            for (int col = 3; col <= 8; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                        }
                        StartRow = StartRow + 1;
                    }
                }
                //除了冰機的設備
                else
                {
                    for (int i = 0; i < TableRows; i++)
                    {
                        XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                        for (int col = 0; col <= 2; col++) headerRow.CreateCell(col + 1).SetCellValue(TableData[4 * i + col].ToString());
                        headerRow.CreateCell(4).SetCellValue(Convert.ToDouble(TableData[4 * i + 3]));
                        if (i == TableRows - 1)
                        {
                            for (int col = 1; col <= 3; col++) headerRow.GetCell(col).CellStyle = LastTd;
                            headerRow.GetCell(4).CellStyle = LastTdComma2;
                        }
                        else
                        {
                            for (int col = 1; col <= 3; col++) headerRow.GetCell(col).CellStyle = style1;
                            headerRow.GetCell(4).CellStyle = Comma2Value;
                        }
                        StartRow = StartRow + 1;
                    }
                }
            }
            //電盤資訊查詢
            else if (data["outputType"] == "SBData")
            {
                string[] TheadContent = allArrays[1];
                //電盤資訊查詢選取的週期
                XSSFRow ReportFrequence = (XSSFRow)sheet.GetRow(4);
                ReportFrequence.CreateCell(2).SetCellValue(data["SelectPeriod"] + "報表");
                ReportFrequence.GetCell(2).CellStyle = style1;
                //電盤資訊查詢選取的時間
                XSSFRow ReportSelectTime = (XSSFRow)sheet.GetRow(5);
                ReportSelectTime.CreateCell(2).SetCellValue(data["SelectedTime"]);
                ReportSelectTime.GetCell(2).CellStyle = style1;
                //表格表頭
                XSSFRow TheadRow = (XSSFRow)sheet.CreateRow(7);
                for (int col = 0; col < Convert.ToInt32(data["theadcols"]); col++) TheadRow.CreateCell(col + 1).SetCellValue(TheadContent[col]);
                for (int col = 0; col < Convert.ToInt32(data["theadcols"]); col++) TheadRow.GetCell(col + 1).CellStyle = ACHistoryTitle;
                //表格內容
                int StartRow = 8;
                for (int i = 0; i < TableRows; i++)
                {
                    XSSFRow headerRow = (XSSFRow)sheet.CreateRow(StartRow);
                    headerRow.CreateCell(1).SetCellValue(TableData[5 * i].ToString());
                    for (int col = 1; col <= 4; col++) headerRow.CreateCell(col + 1).SetCellValue(Convert.ToDouble(TableData[5 * i + col]));
                    if (i == TableRows - 2)
                    {
                        headerRow.GetCell(1).CellStyle = LastTd;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = LastTdComma2;
                    }
                    else if (i == TableRows - 1)
                    {
                        headerRow.GetCell(1).CellStyle = TableSum;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = TableSumComma2;
                    }
                    else
                    {
                        headerRow.GetCell(1).CellStyle = style1;
                        for (int col = 2; col <= 5; col++) headerRow.GetCell(col).CellStyle = Comma2Value;
                    }
                    StartRow = StartRow + 1;
                }
            }

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
    }
}