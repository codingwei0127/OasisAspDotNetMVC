using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMSdemo.Models
{
    public class RegressionM
    {
        //迴歸資料分析讀資料
        public ArrayList GetData()
        {
            //Read SQL


            //Declare Parameters
            //int RowsNum = 6;
            //string[] Time = new string[RowsNum];
            string[] Time = { "2022-06" };

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("Time", Time);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }

        //迴歸資料分析查詢資料
        public ArrayList SearchData(Dictionary<string, string> data)
        {
            double PLR2coeff = 0, PLRcoeff = 0, PLRconstant = 0;
            double OAcoeff = 0, OAconstant = 0;
            List<double> AxisX = new List<double>();
            List<double> AxisY = new List<double>();
            double[] RegressionX = new double[11];
            double[] RegressionY = new double[11];
            string Formula = "";
            double Rsquare = 0;

            if (data["option"] == "PLR_kW")
            {
                //Read SQL
                //order by PLR
                //連資料庫時刪掉下面這段
                double[] PLRAxisX = { 68, 75, 61, 53, 88, 45, 83, 59, 86, 70 };
                double[] PLRAxisY = { 438.756392798386, 428.103663639209, 408.822850559977, 412.291073413043, 409.658841759519, 383.279, 412.768536253572, 406.951583068295, 437.527071880209, 412.851512765142 };
                for (int i = 0; i < PLRAxisX.Length; i++) AxisX.Add(PLRAxisX[i]);
                for (int i = 0; i < PLRAxisY.Length; i++) AxisY.Add(PLRAxisY[i]);
                //連資料庫時刪掉上面這段

                PLR2coeff = 206.3;//讀資料庫
                PLRcoeff = -122.12;//讀資料庫
                PLRconstant = 399.345;//讀資料庫
                double minPLR = 0, maxPLR = 0, deltaPLR = 0;
                minPLR = 45;//連資料庫時刪掉這行
                maxPLR = 88;//連資料庫時刪掉上面這段這行並取消註解下兩行
                //minPLR = AxisX[0];
                //maxPLR = AxisX[AxisX.Count - 1];
                deltaPLR = (maxPLR - minPLR) / 10;
                double NowPLR = minPLR;
                for (int i = 0; i < 11; i++)
                {
                    RegressionX[i] = NowPLR;
                    RegressionY[i] = PLR2coeff * Math.Pow(NowPLR / 100, 2) + PLRcoeff * NowPLR / 100 + PLRconstant;
                    NowPLR = NowPLR + deltaPLR;
                }
                string JudgePLR = "", JudgeConstant = "";
                if (PLRcoeff > 0) JudgePLR = "+ " + PLRcoeff.ToString();
                else JudgePLR = "- " + (PLRcoeff * (-1)).ToString();
                if (PLRconstant > 0) JudgeConstant = "+ " + PLRconstant.ToString();
                else JudgeConstant = "- " + (PLRconstant * (-1)).ToString();
                Formula = "Y = " + PLR2coeff.ToString() + "X² " + JudgePLR + "X " + JudgeConstant;
                Rsquare = 0.5689;//讀資料庫
            }
            else
            {
                //Read SQL
                //order by OA
                //連資料庫時刪掉下面這段
                double[] OAAxisX = { 27.39, 27.55, 27.59, 27.53, 27.56, 27.56, 27.55, 27.62, 27.64, 27.74 };
                double[] OAAxisY = { 472, 556, 557, 550, 552, 548, 539, 556, 541, 527 };
                for (int i = 0; i < OAAxisX.Length; i++) AxisX.Add(OAAxisX[i]);
                for (int i = 0; i < OAAxisY.Length; i++) AxisY.Add(OAAxisY[i]);
                //連資料庫時刪掉上面這段

                OAcoeff = -391.227;//讀資料庫
                OAconstant = 11285.4;//讀資料庫
                double minOA = 0, maxOA = 0, deltaOA = 0;
                minOA = AxisX[0];
                maxOA = AxisX[AxisX.Count - 1];
                deltaOA = (maxOA - minOA) / 10;
                double NowOA = minOA;
                for (int i = 0; i < 11; i++)
                {
                    RegressionX[i] = NowOA;
                    RegressionY[i] = OAcoeff * NowOA + OAconstant;
                    NowOA = NowOA + deltaOA;
                }
                string JudgeConstant = "";
                if (OAconstant > 0) JudgeConstant = "+ " + OAconstant.ToString();
                else JudgeConstant = "- " + (OAconstant * (-1)).ToString();
                Formula = "Y = " + OAcoeff.ToString() + "X " + JudgeConstant;
                Rsquare = 0.17;//讀資料庫
            }

            ArrayList ReturnArray = new ArrayList();
            Dictionary<string, object> temp_dict;
            temp_dict = new Dictionary<string, object>();
            temp_dict.Add("AxisX", AxisX);
            temp_dict.Add("AxisY", AxisY);
            temp_dict.Add("RegressionX", RegressionX);
            temp_dict.Add("RegressionY", RegressionY);
            temp_dict.Add("Formula", Formula);
            temp_dict.Add("Rsquare", Rsquare);
            Console.WriteLine("temp_dict", temp_dict);
            ReturnArray.Add(temp_dict);
            return ReturnArray;
        }
    }
}