using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSdemo.Models;

namespace EMSdemo.Controllers
{
    public class AnalysisController : Controller
    {
        //能源流向分析
        public ActionResult EnergyDirection()
        {
            return View();
        }

        //能源流向分析讀資料
        public JsonResult EnergyDirectionVal()
        {
            EnergyDirectionM EnergyDirectionM = new EnergyDirectionM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EnergyDirectionM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //能源流向分析查詢資料
        public JsonResult EnergyDirectionSearch(Dictionary<string, string> data)
        {
            EnergyDirectionM EnergyDirectionM = new EnergyDirectionM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EnergyDirectionM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //用電基準分析
        public ActionResult EleBaseLine()
        {
            return View();
        }

        //用電基準分析讀資料
        public JsonResult EleBaseLineVal()
        {
            EleBaseLineM EleBaseLineM = new EleBaseLineM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EleBaseLineM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //用電基準分析查詢資料
        public JsonResult EleBaseLineSearch(Dictionary<string, string> data)
        {
            EleBaseLineM EleBaseLineM = new EleBaseLineM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EleBaseLineM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //空調性能分析
        public ActionResult COPBaseLine()
        {
            return View();
        }

        //空調性能分析讀資料
        public JsonResult COPBaseLineVal()
        {
            COPBaseLineM COPBaseLineM = new COPBaseLineM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = COPBaseLineM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //空調性能分析查詢資料
        public JsonResult COPBaseLineSearch(Dictionary<string, string> data)
        {
            COPBaseLineM COPBaseLineM = new COPBaseLineM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = COPBaseLineM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //迴歸資料分析
        public ActionResult Regression()
        {
            return View();
        }

        //迴歸資料分析讀資料
        public JsonResult RegressionVal()
        {
            RegressionM RegressionM = new RegressionM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = RegressionM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //迴歸資料分析查詢資料
        public JsonResult RegressionSearch(Dictionary<string, string> data)
        {
            RegressionM RegressionM = new RegressionM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = RegressionM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }
    }
}