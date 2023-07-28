using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSdemo.Models;

namespace EMSdemo.Controllers
{
    public class OptimizationController : Controller
    {
        //天氣預測
        public ActionResult Weather()
        {
            return View();
        }

        //天氣預測讀資料
        public JsonResult WeatherVal()
        {
            WeatherM WeatherM = new WeatherM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = WeatherM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //負載預測
        public ActionResult PredictLoad()
        {
            return View();
        }

        //負載預測讀資料
        public JsonResult PredictLoadVal()
        {
            PredictLoadM PredictLoadM = new PredictLoadM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = PredictLoadM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //佳化開機策略
        public ActionResult OptiOP()
        {
            return View();
        }

        //佳化開機策略讀資料
        public JsonResult OptiOPVal()
        {
            OptiOPM OptiOPM = new OptiOPM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = OptiOPM.GetData();
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }
    }
}