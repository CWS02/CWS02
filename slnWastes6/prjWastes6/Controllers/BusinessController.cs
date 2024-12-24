using CPC02.Function;
using DocumentFormat.OpenXml.EMMA;
using prjWastes6.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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

        #region 訪談記錄
        [HttpGet]
        public ActionResult RecordList(INTRA model)
        {
            var data = _db.INTRB.Where(x => x.INT999 == model.INT000).ToList();
            ViewBag.INT000 = model.INT000;
            return View(data);
        }

        [HttpGet]
        public ActionResult RecordEdit(INTRB model)
        {
            ViewBag.IsUpdate = false;
            if (model.INT000 != null)
            {
                model = _db.INTRB.Find(model.INT000);
                ViewBag.IsUpdate = true;
            }
            ViewBag.INT000 = model.INT999;
            return View(model);
        }

        [HttpPost]
        public ActionResult RecordEdit(INTRB model, bool IsUpdate = false)
        {
            if (IsUpdate)
            {
                var existingDevice = _db.INTRB.Find(model.INT000);
                if (existingDevice != null)
                {
                    existingDevice.INT001 = model.INT001;
                    existingDevice.INT002 = model.INT002;
                    existingDevice.INT004 = model.INT004;
                   
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
                model.INT000 = Guid.NewGuid().ToString();
                model.IP = Request.UserHostAddress;
                model.Status = 0;
                _db.INTRB.Add(model);
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

            return RedirectToAction("RecordList", new {INT000=model.INT999});
        }

        #endregion
        #region 下載上傳
        [HttpPost]
        public ActionResult UploadRecord(INTRB model, HttpPostedFileBase fileUpload)
        {
            if (fileUpload != null && fileUpload.ContentLength > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf" };
                var fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    TempData["SuccessMessage"] = "只允許上傳圖片或PDF檔案。";
                    return RedirectToAction("RecordList", new { INT000 = model.INT999 });
                }

                var fileName = model.INT000.ToString() + fileExtension;

                var uploadPath = Path.Combine(Server.MapPath("~/image/Business/UploadRecord"), fileName);

                try
                {
                    fileUpload.SaveAs(uploadPath);

                    var existingDevice = _db.INTRB.Find(model.INT000);
                    if (existingDevice != null)
                    {
                        existingDevice.INT003 = fileName; 
                        existingDevice.IP = Request.UserHostAddress;

                        _db.SaveChanges();
                        TempData["SuccessMessage"] = "更新成功！";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "找不到指定的記錄。";
                    }
                }
                catch (Exception ex)
                {
                    TempData["SuccessMessage"] = $"更新失敗：{ex.Message}";
                }
            }

            return RedirectToAction("RecordList", new { INT000 = model.INT999 });
        }

        [HttpGet]
        public ActionResult DownloadRecord(INTRB model)
        {
            var record = _db.INTRB.Find(model.INT000);
            if (record == null || string.IsNullOrEmpty(record.INT003))
            {
                TempData["SuccessMessage"] = "找不到檔案。";
                return RedirectToAction("RecordList");
            }

            var filePath = Path.Combine(Server.MapPath("~/image/Business/UploadRecord"), record.INT003);

            if (!System.IO.File.Exists(filePath))
            {
                TempData["SuccessMessage"] = "檔案不存在。";
                return RedirectToAction("RecordList", new { INT000 = model.INT999 });
            }

            return File(filePath, "application/octet-stream", Path.GetFileName(filePath));
        }
        #endregion
        #region 下載PDF
        [HttpGet]
        public ActionResult PrintRecord(INTRA model)
        {
            model = _db.INTRA.Find(model.INT000);

            string filePath = Server.MapPath("~/file/Business/客戶訪談記錄.docx");
            byte[] template = System.IO.File.ReadAllBytes(filePath);

            var data = new Dictionary<string, string>
            {
                ["1"] = model.INT001,
                ["2"] = model.INT002,
                ["3"] = model.INT003 ,
                ["4"] = model.INT004,
                ["5"] = model.INT005,
                ["6"] = model.INT003,
                ["7"] = model.INT003,
                ["8"] = model.INT003,
                ["9"] = model.INT003,
                ["10"] = model.INT003,
                ["11"] = model.INT003,
                ["12"] = model.INT003,
                ["13"] = model.INT003,
                ["14"] = model.INT003,
                ["15"] = model.INT003,
            };

            byte[] resultFile = WordRender.GenerateDocx(template, data);
            string path = Server.MapPath("~/file/Business");
            string outputFilePath = System.IO.Path.Combine(path, $"Business_{model.INT000}.docx");

            // 生成 Word 文件
            System.IO.File.WriteAllBytes(outputFilePath, resultFile);

            // 生成 PDF 文件
            bool conversionResult = WordRender.ConvertDocToPdf(path, $"{path}\\Business_{model.INT000}.docx");
            if (conversionResult)
            {
                string pdfFilePath = System.IO.Path.ChangeExtension(outputFilePath, ".pdf");
                byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfFilePath);

                // 刪除暫存檔案
                if (System.IO.File.Exists(pdfFilePath))
                {
                    System.IO.File.Delete(pdfFilePath); // 刪除 PDF
                }
                if (System.IO.File.Exists(outputFilePath))
                {
                    System.IO.File.Delete(outputFilePath); // 刪除 Word
                }

                return File(pdfBytes, "application/pdf");
            }
            else
            {
                ViewBag.Message = "PDF生成失敗";
                return View();
            }
        }
        #endregion
    }
}