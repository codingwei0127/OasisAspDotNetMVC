using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSdemo.Models;

namespace EMSdemo.Controllers
{
    public class FaultDiagnosisController : Controller
    {
        //用電預警
        public ActionResult EleWarning()
        {
            return View();
        }

        //用電預警查詢資料
        public JsonResult EleWarningSearch(Dictionary<string, string> data)
        {
            EleWarningM EleWarningM = new EleWarningM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EleWarningM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //性能預警
        public ActionResult EffWarning()
        {
            return View();
        }

        //性能預警查詢資料
        public JsonResult EffWarningSearch(Dictionary<string, string> data)
        {
            EffWarningM EffWarningM = new EffWarningM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EffWarningM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //需量超約紀錄
        public ActionResult DPWarning()
        {
            return View();
        }

        //需量超約紀錄查詢資料
        public JsonResult DPWarningSearch(Dictionary<string, string> data)
        {
            DPWarningM DPWarningM = new DPWarningM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = DPWarningM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }
    }
}