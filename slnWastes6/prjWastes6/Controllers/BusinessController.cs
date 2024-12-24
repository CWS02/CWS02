using prjWastes6.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWastes6.Controllers
{
    public class BusinessController : Controller
    {
        // GET: Business
        CPCContext _db = new CPCContext(); 

        #region 客戶訪談記錄
        [HttpGet]
        public ActionResult InterviewRecordList(bool? Per)
        {
            var model = _db.INTRA
            .OrderByDescending(e => e.INT008)
            .ToList();

            ViewBag.Per = false;
            if (Per == true)
            {
                ViewBag.Per = true;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult InterviewRecordEdit(INTRA model)
        {
            ViewBag.IsUpdate = false;
            if (model.INT000 != null)
            {
                model = _db.INTRA.Find(model.INT000);
                ViewBag.IsUpdate = true;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult InterviewRecordEdit(INTRA model, bool IsUpdate = false)
        {
            if (IsUpdate)
            {
                var existingDevice = _db.INTRA.Find(model.INT000);
                if (existingDevice != null)
                {
                    existingDevice.INT001 = model.INT001;
                    existingDevice.INT002 = model.INT002;
                    existingDevice.INT003 = model.INT003;
                    existingDevice.INT004 = model.INT004;
                    existingDevice.INT005 = model.INT005;
                    existingDevice.INT006 = model.INT006;
                    existingDevice.INT007 = model.INT007;
                    existingDevice.INT008 = model.INT008;
                    existingDevice.INT009 = model.INT009;
                    existingDevice.INT010 = model.INT010;
                    existingDevice.INT011 = model.INT011;
                    existingDevice.INT012 = model.INT012;
                    existingDevice.INT013 = model.INT013;
                    existingDevice.INT014 = model.INT014;
                    existingDevice.INT015 = model.INT015;
                    existingDevice.INT016 = model.INT016;
                    existingDevice.INT017 = model.INT017;
                    existingDevice.INT018 = model.INT018;

                    existingDevice.IP = Request.UserHostAddress;

                    try
                    {
                        _db.SaveChanges();
                        TempData["SuccessMessage"] = "更新成功！";
                    }
                    catch
                    {
                        TempData["SuccessMessage"] = "更新失敗！";
                    }
                }
            }
            else
            {
                model.INT000= Guid.NewGuid().ToString(); 
                model.IP = Request.UserHostAddress;
                model.Status = 0;
                _db.INTRA.Add(model);
                try
                {
                    _db.SaveChanges();
                    TempData["SuccessMessage"] = "新增成功！";
                }
                catch
                {
                    TempData["SuccessMessage"] = "新增失敗！";
                }
            }

            return RedirectToAction("InterviewRecordList");
        }
#endregion

        #region 到訪記錄
        #endregion

        #region 電話記錄
        #endregion

    }
}