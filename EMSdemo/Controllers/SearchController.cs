using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMSdemo.Models;

namespace EMSdemo.Controllers
{
    public class SearchController : Controller
    {
        //用電歷史
        public ActionResult EleHistory()
        {
            return View();
        }

        //用電歷史查詢資料
        public JsonResult EleHistorySearch(Dictionary<string, string> data)
        {
            EleHistoryM EleHistoryM = new EleHistoryM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = EleHistoryM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //空調系統查詢
        public ActionResult ACHistory()
        {
            return View();
        }

        //空調系統查詢資料
        public JsonResult ACHistorySearch(Dictionary<string, string> data)
        {
            ACHistoryM ACHistoryM = new ACHistoryM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = ACHistoryM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //電盤資訊查詢
        public ActionResult SwitchBoardData()
        {
            return View();
        }

        //電盤資訊變更選擇週期
        public JsonResult SBChangePeriod(Dictionary<string, string> data)
        {
            SwitchBoardM SwitchBoardM = new SwitchBoardM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = SwitchBoardM.ChangePeriod(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }

        //電盤資訊查詢資料
        public JsonResult SwitchBoardSearch(Dictionary<string, string> data)
        {
            SwitchBoardM SwitchBoardM = new SwitchBoardM();
            ArrayList ReturnArray = new ArrayList();
            ReturnArray = SwitchBoardM.SearchData(data);
            return Json(ReturnArray, JsonRequestBehavior.AllowGet);
        }
    }
}