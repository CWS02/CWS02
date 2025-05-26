using System;
using System.Collections.Generic;
using System.Linq;
using prjWastes6.Models;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Dapper;
using System.Configuration;
using prjWastes6.Models.DataAccess;
using NPOI.XSSF.UserModel; 
using NPOI.SS.UserModel; 
using System.IO;
using DocumentFormat.OpenXml.EMMA;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Wordprocessing;
using NPOI.SS.Formula.Functions;


namespace prjWastes6.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private int DefaultPageSize = 10;
        ESGContext _db = new ESGContext();

        TWNCPCContext _TWNCPCdb = new TWNCPCContext();
        private readonly keyenecDAO _keyenecDAO;
        private readonly CommonDAO _CommonDAO;

        public HomeController()
        {
            _keyenecDAO = new keyenecDAO();
            _CommonDAO = new CommonDAO();
        }
        // GET: ESG
        [AllowAnonymous]
        public ActionResult Index()
        {
            //取得所有項目放入WASTES
            var wastes = _db.WASTES.ToList();
            //若Session["Member"]為空，表示會員未登入
            if (Session["Member"] == null)
            {
                //指定Index.cshtml套用_Layout.cshtml，View使用products模型
                return View("Index", "_Layout", wastes);
            }
            //會員登入狀態
            //指定Index.cshtml套用_LayoutMember.cshtml，View使用products模型
            return View("Index", "_LayoutMember", wastes);
        }
        //Get: Home/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        //Post: Home/Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string fUserId, string fPwd)
        {
            // 依帳密取得會員並指定給member
            var member = _db.tMember
                .Where(m => m.fUserId == fUserId && m.fPwd == fPwd)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                ViewBag.Message = "帳密錯誤，登入失敗";
                return View();
            }
            //使用Session變數記錄歡迎詞
            Session["WelCome"] = member.fName + ":登入中";
            //使用Session變數記錄登入的會員物件
            Session["Member"] = member;
            //執行Home控制器的Index動作方法
            return RedirectToAction("Index");
        }
        //Get:Home/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        //Post:Home/Register
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(tMember pMember)
        {
            //若模型沒有通過驗證則顯示目前的View
            if (ModelState.IsValid == false)
            {
                return View();
            }
            // 依帳號取得會員並指定給member
            var member = _db.tMember
                .Where(m => m.fUserId == pMember.fUserId)
                .FirstOrDefault();
            //若member為null，表示會員未註冊
            if (member == null)
            {
                //將會員記錄新增到tMember資料表
                _db.tMember.Add(pMember);
                _db.SaveChanges();
                //執行Home控制器的Login動作方法
                return RedirectToAction("Login");
            }
            ViewBag.Message = "此帳號己有人使用，註冊失敗";
            return View();
        }

        //Get:Index/Logout
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Session.Clear();  //清除Session變數資料
            return RedirectToAction("Index");
        }





        //20231229搜尋年月日及廢棄物代碼:搜尋
        [AllowAnonymous]
        /*public ActionResult List(string scrapCode = "")
        {
        ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
        var customers = _db.WASTES.ToList();
            
            DateTime startDate;
        DateTime endDate;

        if (!String.IsNullOrEmpty(Request.QueryString["startDate"]))
        {
        startDate = DateTime.Parse(Request.QueryString["startDate"]);
        }
        else
        {
        startDate = DateTime.MinValue;
        }

        if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
        {
        endDate = DateTime.Parse(Request.QueryString["endDate"]);
        }
        else
        {
        endDate = DateTime.MaxValue;
        }


        var ESGs = _db.WASTES
        .OrderByDescending(x => x.REMOVAL_DATE)
        .Where(x => x.REMOVAL_DATE >= startDate &&
        x.REMOVAL_DATE <= endDate)
        .ToList();
          if (!string.IsNullOrEmpty(scrapCode))
                {
                ESGs = ESGs.Where(x => x.SCRAP_CODE == scrapCode).ToList();


    }


            decimal totalCarbonDioxide = ESGs
     .Where(x => x.CARBON_DIOXIDE != null)
     .Sum(x => x.CARBON_DIOXIDE.Value);

            ViewBag.TotalCarbonDioxide = totalCarbonDioxide;
            // 20240325將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);

    }*/
 
        public ActionResult List(DateTime? startDate = null, DateTime? endDate = null,string scrapCode="")
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            var customers = _db.WASTES.ToList();

            var query = _db.WASTES.OrderByDescending(x => x.REMOVAL_DATE).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(x => x.REMOVAL_DATE >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.REMOVAL_DATE <= endDate.Value);
            if(scrapCode!="")
            {
                query = query.Where(x => x.SCRAP_CODE == scrapCode);
            }
            var ESGs = query.ToList();

            decimal totalCarbonDioxide = ESGs
                .Where(x => x.CARBON_DIOXIDE != null)
                .Sum(x => x.CARBON_DIOXIDE.Value);

            ViewBag.TotalCarbonDioxide = totalCarbonDioxide;

            return View(ESGs);
        }

        [AllowAnonymous]
        public ActionResult Keyenec(DateTime? startDate = null )
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            IEnumerable<dynamic> keyenec = _keyenecDAO.keyenecData(startDate);
            ViewBag.KeyenecData = keyenec;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ExportKeyenec(DateTime? startDate)
        {
            var data = _keyenecDAO.keyenecData(startDate);

            using (var workbook = new XSSFWorkbook())
            {
                var sheet = workbook.CreateSheet("空壓機用電量表");
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 1, 3));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 4, 10));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 11, 18));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 19, 22));
                sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 1, 23, 23));

                var headerRow = sheet.CreateRow(0);
                headerRow.CreateCell(0).SetCellValue("");
                headerRow.CreateCell(1).SetCellValue("日期");
                headerRow.CreateCell(4).SetCellValue("最大/最小/每日累計(KWH)");
                headerRow.CreateCell(11).SetCellValue("日累計用電量(KWH)");
                headerRow.CreateCell(19).SetCellValue("每日流量");
                headerRow.CreateCell(23).SetCellValue("壓縮空氣系統效率值系統效率值 (kW/CMM)");

                var headerRow2 = sheet.CreateRow(1);
                headerRow2.CreateCell(0).SetCellValue("序");
                headerRow2.CreateCell(1).SetCellValue("年");
                headerRow2.CreateCell(2).SetCellValue("月");
                headerRow2.CreateCell(3).SetCellValue("日");
                headerRow2.CreateCell(4).SetCellValue("空壓1");
                headerRow2.CreateCell(5).SetCellValue("乾燥1");
                headerRow2.CreateCell(6).SetCellValue("空壓2");
                headerRow2.CreateCell(7).SetCellValue("乾燥2");
                headerRow2.CreateCell(8).SetCellValue("空壓3");
                headerRow2.CreateCell(9).SetCellValue("乾燥3");
                headerRow2.CreateCell(10).SetCellValue("水塔");
                headerRow2.CreateCell(11).SetCellValue("空壓1用電");
                headerRow2.CreateCell(12).SetCellValue("乾燥1用電");
                headerRow2.CreateCell(13).SetCellValue("空壓2用電");
                headerRow2.CreateCell(14).SetCellValue("乾燥2用電");
                headerRow2.CreateCell(15).SetCellValue("空壓3用電");
                headerRow2.CreateCell(16).SetCellValue("乾燥3用電");
                headerRow2.CreateCell(17).SetCellValue("水塔用電");
                headerRow2.CreateCell(18).SetCellValue("總用電量(KWH)");
                headerRow2.CreateCell(19).SetCellValue("最大");
                headerRow2.CreateCell(20).SetCellValue("最小");
                headerRow2.CreateCell(21).SetCellValue("平均");
                headerRow2.CreateCell(22).SetCellValue("總量(m3/h)");

                int rowIndex = 2;
                int index = 1;
                ICellStyle thousandStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                thousandStyle.DataFormat = format.GetFormat("#,##0"); 

                ICellStyle thousandDecimalStyle = workbook.CreateCellStyle();
                thousandDecimalStyle.DataFormat = format.GetFormat("#,##0.00"); 

                foreach (var item in data)
                {
                    var row = sheet.CreateRow(rowIndex++);
                    row.CreateCell(0).SetCellValue(index++);
                    row.CreateCell(1).SetCellValue(item.KEY01);
                    row.CreateCell(2).SetCellValue(item.KEY02);
                    row.CreateCell(3).SetCellValue(item.KEY03);
                    row.CreateCell(4).SetCellValue($"{item.MAX空壓1}/{item.MIN空壓1}/{item.SUM空壓1}");
                    row.CreateCell(5).SetCellValue($"{item.MAX乾燥1}/{item.MIN乾燥1}/{item.SUM乾燥1}");
                    row.CreateCell(6).SetCellValue($"{item.MAX空壓2}/{item.MIN空壓2}/{item.SUM空壓2}");
                    row.CreateCell(7).SetCellValue($"{item.MAX乾燥2}/{item.MIN乾燥2}/{item.SUM乾燥2}");
                    row.CreateCell(8).SetCellValue($"{item.MAX空壓3}/{item.MIN空壓3}/{item.SUM空壓3}");
                    row.CreateCell(9).SetCellValue($"{item.MAX乾燥3}/{item.MIN乾燥3}/{item.SUM乾燥3}");
                    row.CreateCell(10).SetCellValue($"{item.MAX水塔}/{item.MIN水塔}/{item.SUM水塔}");

                    ICell cell11 = row.CreateCell(11);
                    cell11.SetCellValue((double)item.空壓1用電);
                    cell11.CellStyle = thousandStyle;

                    ICell cell12 = row.CreateCell(12);
                    cell12.SetCellValue((double)item.乾燥1用電);
                    cell12.CellStyle = thousandStyle;

                    ICell cell13 = row.CreateCell(13);
                    cell13.SetCellValue((double)item.空壓2用電);
                    cell13.CellStyle = thousandStyle;

                    ICell cell14 = row.CreateCell(14);
                    cell14.SetCellValue((double)item.乾燥2用電);
                    cell14.CellStyle = thousandStyle;

                    ICell cell15 = row.CreateCell(15);
                    cell15.SetCellValue((double)item.空壓3用電);
                    cell15.CellStyle = thousandStyle;

                    ICell cell16 = row.CreateCell(16);
                    cell16.SetCellValue((double)item.乾燥3用電);
                    cell16.CellStyle = thousandStyle;

                    ICell cell17 = row.CreateCell(17);
                    cell17.SetCellValue((double)item.水塔用電);
                    cell17.CellStyle = thousandStyle;

                    ICell cell18 = row.CreateCell(18);
                    cell18.SetCellValue((double)item.總用電);
                    cell18.CellStyle = thousandStyle;

                    ICell cell19 = row.CreateCell(19);
                    cell19.SetCellValue((double)item.mMAX);
                    cell19.CellStyle = thousandDecimalStyle;

                    ICell cell20 = row.CreateCell(20);
                    cell20.SetCellValue((double)item.mMIN);
                    cell20.CellStyle = thousandDecimalStyle;

                    ICell cell21 = row.CreateCell(21);
                    cell21.SetCellValue((double)item.mAVG);
                    cell21.CellStyle = thousandDecimalStyle;

                    ICell cell22 = row.CreateCell(22);
                    cell22.SetCellValue((double)item.mSUM);
                    cell22.CellStyle = thousandDecimalStyle;

                    ICell cell23 = row.CreateCell(23);
                    cell23.SetCellValue((double)item.mCMM);
                    cell23.CellStyle = thousandDecimalStyle;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.Write(stream);

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "空壓機用電量表.xlsx");
                }
            }
        }

        [AllowAnonymous]
        public ActionResult SGS_Parameter(string item)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }

            IQueryable<SGS_Parameter> modelQuery = _db.SGS_Parameter.Where(s => s.PAR007 == 0);

            if (!string.IsNullOrEmpty(item))
            {
                modelQuery = modelQuery.Where(s => s.PAR001.Contains(item));
            }

            var model = modelQuery.ToList();
            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult SGS_ParameterEdit(SGS_Parameter model)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }

            ViewBag.IsUpdate = false;
            if (model.PAR000 != null)
            {
                model = _db.SGS_Parameter.FirstOrDefault(s=>s.PAR000==model.PAR000);
                ViewBag.IsUpdate = true;
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SGS_ParameterEdit(SGS_Parameter model,bool IsUpdate=false)
        {
            if (IsUpdate)
            {
                var oldmodel = _db.SGS_Parameter.Find(model.PAR000);
                oldmodel.PAR001 = model.PAR001;
                oldmodel.PAR002 = model.PAR002;
                oldmodel.PAR003 = model.PAR003;
                oldmodel.PAR004 = model.PAR004;
                oldmodel.PAR005 = Request.UserHostAddress;
                oldmodel.PAR006 = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
            }
            else
            {
                model.PAR000 = Guid.NewGuid().ToString();
                model.PAR005 = Request.UserHostAddress;
                model.PAR006 = DateTime.Now.ToString("yyyyMMdd HH:mm:ss");
                model.PAR007 = 0;
                _db.SGS_Parameter.Add(model);                
            }
            try
            {
                _db.SaveChanges();
            }
            catch
            {
            }
            return RedirectToAction("SGS_Parameter", "Home");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SGS_ParameterSetting()
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }

            var model = _db.SGS_ParameterSetting.OrderBy(x=>x.CreateTime).ToList();

            var coefficientOptions = _db.SGS_Parameter
                .Select(p => new {
                    Id = p.PAR000, 
                    Display = p.PAR002 + "-" + p.PAR003 + "-" + p.PAR001 + "-" + p.PAR004
                })
                .ToList();

            ViewBag.CoefficientOptions = coefficientOptions;

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateInput(false)] 
        public JsonResult UpdateCoefficient(Guid id, Guid coefficientId)
        {
            try
            {
                var setting = _db.SGS_ParameterSetting.FirstOrDefault(p => p.PAR000 == id);
                if (setting == null)
                    return Json(new { success = false, message = "找不到資料" }, JsonRequestBehavior.AllowGet);

                setting.PAR001 = coefficientId;
                setting.UpdateTime = DateTime.Now;
                setting.IP =Request.UserHostAddress;
                _db.SaveChanges();

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult SGS_Calculate(SGS_Search search)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }

            var coefficientOptions = (from p in _db.SGS_Parameter
            join s in _db.SGS_ParameterSetting
            on p.PAR000 equals s.PAR001.ToString() into joined
            from s in joined.DefaultIfEmpty()
            select new
            {
                Id = p.PAR000,
                Label = p.PAR002 + "-" + p.PAR001 + "-" + p.PAR004,
                Value = p.PAR002
            }).ToList();

            var allParameters = _db.SGS_ParameterSetting
            .Select(s => new
            {
                Id = s.PAR001,
                Category = s.PAR003,
                Category2 = s.PAR004,
            }).ToList();

            ViewBag.AllParameters = allParameters;
            ViewBag.CoefficientOptions = coefficientOptions;


            if (search.category == "electricity")
            {
                var query = _db.ELECTRICITY_BILL
               .Where(x => x.FACTORY == search.factory);
                if (!string.IsNullOrEmpty(search.year))
                {
                    query = query.Where(x => x.FROM_BILLING_PERIOD.Value.Year.ToString() == search.year);
                }
                var result = query
                               .GroupBy(x => x.FACTORY)
                               .Select(g => new ElectricitySummaryViewModel
                               {
                                   Factory = g.Key,
                                   SumPeakElectricity = g.Sum(x => x.PEAK_ELECTRICITY),
                                   SumHalfSpikePower = g.Sum(x => x.HALF_SPIKE_POWER),
                                   SumSaturdayHalfPeak = g.Sum(x => x.SATURDAY_HALF_PEAK),
                                   SumOffPeakElectricity = g.Sum(x => x.OFF_PEAK_ELECTRICITY),
                                   SumTotalElectricity = g.Sum(x => x.TOTAL_ELECTRICITY),
                                   SumTotalBillTax = g.Sum(x => x.TOTAL_BILL_TAX),
                                   SumCarbonPeriod = g.Sum(x => x.CARBON_PERIOD)
                               }).ToList();

                return View(result);
            }
            else if (search.category == "water")
            {
                var query = _db.WATER_BILL
               .Where(x => x.FACTORY == search.factory && x.METER_DIAMETER.ToString() == search.waterdiameter);
                if (!string.IsNullOrEmpty(search.year))
                {
                    query = query.Where(x => x.FROM_BILLING_PERIOD.Value.Year.ToString() == search.year);
                }
                var result = query
                               .GroupBy(x => x.FACTORY)
                               .Select(g => new WaterSummaryViewModel
                               {
                                   Factory = g.Key,
                                   Waterdiameter = search.waterdiameter,
                                   SumNUMBERPOINTERS = g.Sum(x => x.NUMBER_POINTERS),
                                   SumTOTALWATER = g.Sum(x => x.TOTAL_WATER),
                                   SumTOTALBILLTAX = g.Sum(x => x.TOTAL_BILL_TAX),
                                   SumCARBONPERIOD = g.Sum(x => x.CARBON_PERIOD),
                               }).ToList();

                return View(result);
            }
            else if (search.category == "waste")
            {
                var query = _db.WASTES
               .Where(x => x.TREATMENT == search.methods && x.SCRAP_CODE == search.code);
                if (!string.IsNullOrEmpty(search.year))
                {
                    query = query.Where(x => x.REMOVAL_DATE.Value.Year.ToString() == search.year);
                }
                var result = query
                               .GroupBy(x => x.TREATMENT)
                               .Select(g => new WasteSummaryViewModel
                               {
                                   methods =search.methods,
                                   code = search.code,
                                   SumDECLAREDWEIGHT = g.Sum(x => x.DECLARED_WEIGHT),
                                   SumCKILOMETERS = g.Sum(x => x.KILOMETERS),
                                   SumACTIVITYDATA = g.Sum(x => x.ACTIVITY_DATA),
                                   SumCARBONEMISSIONFACTOR = g.Sum(x => x.CARBON_EMISSION_FACTOR),
                                   SumCCARBONDIOXIDE = g.Sum(x => x.CARBON_DIOXIDE),
                               }).ToList();

                return View(result);
            }
            else if (search.category == "fireextin")
            {
                var query = _db.FireExtin
                    .Where(x => x.FE010 == search.factory && x.FE005 == search.content)
                    .GroupBy(x => new { x.FE010, x.FE005 })
                    .Select(g => new FireExtinViewModel
                    {
                        Factory = g.Key.FE010,
                        content = g.Key.FE005,
                        SumFE002 = g.Sum(x => x.FE002),
                        Sumton = g.Sum(x =>
                            (x.FE001 != null && !x.FE001.Contains("乾粉") && x.FE002.HasValue && x.FE006.HasValue)
                            ? (x.FE002.Value * x.FE006.Value / 1000)
                            : 0),
                        Sumkg = g.Sum(x =>
                            (x.FE001 != null && !x.FE001.Contains("乾粉") && x.FE002.HasValue && x.FE006.HasValue)
                            ? (x.FE002.Value * x.FE006.Value)
                            : 0)
                    })
                    .ToList();


                return View(query);
            }
           else if (search.category == "wd40")
            {  
                if(search.year!=null)
                {
                    var query = _db.WD40A
                   .Where(x => x.WD003.Contains(search.year))
                   .GroupBy(x => x.WD003.Contains(search.year))
                   .Select(g => new WD40ViewModel
                   {
                       SumWD011 =g.Sum(x=>x.WD011),
                   })
                   .ToList();

                    return View(query);
                }
            }
            else if (search.category == "coldcoal")
            {
                if (search.factory != null)
                {
                    var validCC007Values = new List<string> { "R-134A", "HFC-134A", "R-407C", "R-410A" };

                    var query = _db.ColdCoal
                        .Where(x => x.CC010 == search.factory && validCC007Values.Contains(x.CC007))
                        .GroupBy(x => new { x.CC007, x.CC010 })
                        .Select(g => new ColdCoalViewModel
                        {
                            Code=g.Key.CC007,
                            SumCC012 = g.Sum(x => x.CC012),
                        })
                        .ToList();

                    return View(query);
                }
            }
            return View();
        }

        [AllowAnonymous]
        public JsonResult SGS_ParameterDelete(SGS_Parameter model, string password,string code)
        {
            var codes = code.Split(',').ToList();
            var user = _db.tMember.FirstOrDefault(p => p.fPwd == password);
            if (user == null)
            {
                return Json(new { success = false, message = "密碼錯誤" });
            }
            else
            {
                model = _db.SGS_Parameter.Find(model.PAR000);
                model.PAR007 = 1;
                try
                {
                    _db.SaveChanges();
                    return Json(new { success = true, message = "刪除成功" });
                }
                catch
                {
                    return Json(new { success = false, message = "刪除失敗" });
                }
            }

        }

        [AllowAnonymous]
        public JsonResult SGS_CheckDuplicate(SGS_Parameter model, string password,string code)
        {
            var isDuplicate = _db.SGS_Parameter.Any(p => p.PAR001 == model.PAR001 && p.PAR003 == model.PAR003 && p.PAR004== model.PAR004 && p.PAR007 == 0);
            return Json(new { isDuplicate });
        }
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult Create(WASTES customer)
            //20240326廢棄物新增PDF上傳
            
        public ActionResult Create(WASTES customer, HttpPostedFileBase pdfFile)
        {
            string custId = customer.DOCUMENT_ID;
            var temp = _db.WASTES
                .Where(m => m.DOCUMENT_ID == custId).FirstOrDefault();
            
            if (temp == null)
            {//HomeController_20231226紀錄登打時間
                customer.data = DateTime.Now;
                //從Session讀取當前登錄用戶
                var username = Session["Member"] as tMember;

                //將當前登錄用戶賦值給CreatedBy屬性
                customer.CreatedBy = username.fUserId;

                // 上傳PDF 檔案
                if (pdfFile != null && pdfFile.ContentLength > 0)
                {
                    // 讀取上傳的 PDF 檔案內容
                    var pdfContent = new byte[pdfFile.ContentLength];
                    pdfFile.InputStream.Read(pdfContent, 0, pdfFile.ContentLength);

                    // 將 PDF 檔案內容保存到模型中
                    customer.PDF_CONTENT = pdfContent;
                }

                _db.WASTES.Add(customer);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            ViewBag.Msg = "聯單編號重複";

            return View();
        }
        // GET: Edit Waste
        [AllowAnonymous]
        public ActionResult Edit(string id)
        {
            var waste = _db.WASTES.FirstOrDefault(x => x.DOCUMENT_ID == id);
            WASTES photo = _db.WASTES.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }

            return View(waste);
        }
        // POST: Edit Waste
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult Edit(WASTES waste)
            // 上傳PDF 檔案
            public ActionResult Edit(WASTES waste, HttpPostedFileBase pdfFile)

        {
            var model = _db.WASTES.FirstOrDefault(x => x.DOCUMENT_ID == waste.DOCUMENT_ID);

            //20250327水費要顯示 PDF 檔案

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {

                model.PDF_CONTENT = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(model.PDF_CONTENT, 0, pdfFile.ContentLength);
            }
            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {

                model.REMOVAL_DATE = waste.REMOVAL_DATE;
                model.CLEANER_CODE = waste.CLEANER_CODE;
                model.PURGE_NAME = waste.PURGE_NAME;
                model.NUMBER_CARRIER = waste.NUMBER_CARRIER;
                model.TREATMENT = waste.TREATMENT;
                model.FINAL_DISPOSER_CODE = waste.FINAL_DISPOSER_CODE;
                model.FINAL_DISPOSER_NAME = waste.FINAL_DISPOSER_NAME;
                model.FINAL_DISPOSER = waste.FINAL_DISPOSER;
                model.SCRAP_CODE = waste.SCRAP_CODE;
                model.SCRAP_NAME = waste.SCRAP_NAME;
                model.DECLARED_WEIGHT = waste.DECLARED_WEIGHT;
                model.KILOMETERS = waste.KILOMETERS;
                model.ACTIVITY_DATA = waste.ACTIVITY_DATA;
                model.CARBON_EMISSION_FACTOR = waste.CARBON_EMISSION_FACTOR;
                model.CARBON_DIOXIDE = waste.CARBON_DIOXIDE;
                model.VOUCHER_NUMBER = waste.VOUCHER_NUMBER;
            }

            

            _db.SaveChanges();

            return RedirectToAction("List");
        }
        //20250326 廢棄物要顯示 PDF 檔案

        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile2(string id)
        {
            var waste = _db.WASTES.FirstOrDefault(x => x.DOCUMENT_ID == id);
            if (waste != null && waste.PDF_CONTENT != null && waste.PDF_CONTENT.Length > 0)
            {
                return File(waste.PDF_CONTENT, "application/pdf");
            }
            return null;
        }

        //20231229搜尋年月日
        [AllowAnonymous]
        /*public ActionResult ElectricityList(string factory = "")
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            var currentUser = Session["Member"] as tMember;
            // 檢索當前用戶的UpdateRight值
            string updateRight = GetUserUpdateRight(currentUser);
            var list = _db.tMember.ToList();


            var customers = _db.ELECTRICITY_BILL.ToList();
            DateTime startDate;
            DateTime endDate;

            if (!String.IsNullOrEmpty(Request.QueryString["startDate"]))
            {
                startDate = DateTime.Parse(Request.QueryString["startDate"]);
            }
            else
            {
                startDate = DateTime.MinValue;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
            {
                endDate = DateTime.Parse(Request.QueryString["endDate"]);
            }
            else
            {
                endDate = DateTime.MaxValue;
            }


            var ESGs = _db.ELECTRICITY_BILL
            .OrderByDescending(x => x.FROM_BILLING_PERIOD)
            .Where(x => x.FROM_BILLING_PERIOD >= startDate &&
            x.FROM_BILLING_PERIOD <= endDate)
            .ToList();
            if (!string.IsNullOrEmpty(factory))
            {
                ESGs = ESGs.Where(x => x.FACTORY == factory).ToList();
            }
            



            ViewBag.UpdateRight = updateRight;
            // 20240326將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }
            return View(ESGs);

        }*/

        //2024043020240430修改為一個搜索按鈕
        public ActionResult ElectricityList(string factory = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            var currentUser = Session["Member"] as tMember;
            string updateRight = GetUserUpdateRight(currentUser);

            var customers = _db.ELECTRICITY_BILL.ToList();

            var query = _db.ELECTRICITY_BILL.OrderByDescending(x => x.FROM_BILLING_PERIOD).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(x => x.FROM_BILLING_PERIOD >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.FROM_BILLING_PERIOD <= endDate.Value);

            if (!string.IsNullOrEmpty(factory))
                query = query.Where(x => x.FACTORY == factory);

            var ESGs = query.ToList();

            ViewBag.UpdateRight = updateRight;

            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);
        }


        // 添加一個方法來檢索當前用戶的UpdateRight值
        private string GetUserUpdateRight(tMember user)
        {
            if (user != null)
            {
                // 假設UpdateRight是tMember類中的屬性
                return user.UpdateRight;
            }

            // 返回默認值或處理用戶不可用的情況
            return "N";
        }

        [AllowAnonymous]
        public ActionResult ElectricityCreate()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult ElectricityCreate(ELECTRICITY_BILL customer)
        //20240322新增PDF上傳
        public ActionResult ElectricityCreate(ELECTRICITY_BILL customer, HttpPostedFileBase pdfFile)

        {
            if (ModelState.IsValid)
            {
                string custId = customer.DOCUMENT_ID;
                var temp = _db.ELECTRICITY_BILL
                    .Where(m => m.DOCUMENT_ID == custId)
                    .FirstOrDefault();

                if (temp == null)
                {
                    // HomeController_20231226紀錄登打時間
                    customer.data = DateTime.Now;

                    // 從 Session 讀取當前登錄用戶
                    var username = Session["Member"] as tMember;

                    // 將當前登錄用戶賦值給 CreatedBy 屬性
                    customer.CreatedBy = username.fUserId;

                    // 上傳PDF 檔案
                    if (pdfFile != null && pdfFile.ContentLength > 0)
                    {
                        // 讀取上傳的 PDF 檔案內容
                        var pdfContent = new byte[pdfFile.ContentLength];
                        pdfFile.InputStream.Read(pdfContent, 0, pdfFile.ContentLength);

                        // 將 PDF 檔案內容保存到模型中
                        customer.PDF_CONTENT = pdfContent;
                    }



                    _db.ELECTRICITY_BILL.Add(customer);



                    _db.SaveChanges();  // 保存更改到資料庫
                    return RedirectToAction("ElectricityList");
                }

                ViewBag.Msg = "通知單號碼重複";
                return View();
            }

            // 如果模型驗證失敗，返回 View 以顯示錯誤消息
            return View(customer);
        }

        // GET: Edit Electricity
        [AllowAnonymous]
        public ActionResult ElectricityEdit(string id)
        {
            var electricity = _db.ELECTRICITY_BILL.FirstOrDefault(x => x.DOCUMENT_ID == id);
            ELECTRICITY_BILL photo = _db.ELECTRICITY_BILL.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }


            return View(electricity);
        }
        // POST: Edit Waste
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult ElectricityEdit(ELECTRICITY_BILL electricity)
            // 上傳PDF 檔案
            public ActionResult ElectricityEdit(ELECTRICITY_BILL electricity, HttpPostedFileBase pdfFile)
        {
            var model = _db.ELECTRICITY_BILL.FirstOrDefault(x => x.DOCUMENT_ID == electricity.DOCUMENT_ID);
            //20250327電費要顯示 PDF 檔案

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {

                model.PDF_CONTENT = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(model.PDF_CONTENT, 0, pdfFile.ContentLength);
            }
            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {

                model.FACTORY = electricity.FACTORY;
                model.ELECTRICITY_SIGNAL = electricity.ELECTRICITY_SIGNAL;
                model.FROM_BILLING_PERIOD = electricity.FROM_BILLING_PERIOD;
                model.UNTIL_BILLING_PERIOD = electricity.UNTIL_BILLING_PERIOD;
                model.PEAK_ELECTRICITY = electricity.PEAK_ELECTRICITY;
                model.HALF_SPIKE_POWER = electricity.HALF_SPIKE_POWER;
                model.SATURDAY_HALF_PEAK = electricity.SATURDAY_HALF_PEAK;
                model.OFF_PEAK_ELECTRICITY = electricity.OFF_PEAK_ELECTRICITY;
                model.TOTAL_ELECTRICITY = electricity.TOTAL_ELECTRICITY;
                model.TOTAL_BILL_TAX = electricity.TOTAL_BILL_TAX;
                model.CARBON_PERIOD = electricity.CARBON_PERIOD;
                model.CARBON_EMISSION_FACTOR = electricity.CARBON_EMISSION_FACTOR;
                model.CARBON_DIOXIDE = electricity.CARBON_DIOXIDE;
                model.VOUCHER_NUMBER = electricity.VOUCHER_NUMBER;
            }
            

            _db.SaveChanges();

            return RedirectToAction("ElectricityList");
        }

        //20250326電費要顯示 PDF 檔案

        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile1(string id)
        {
            var electricityBill = _db.ELECTRICITY_BILL.FirstOrDefault(x => x.DOCUMENT_ID == id);
            if (electricityBill != null && electricityBill.PDF_CONTENT != null && electricityBill.PDF_CONTENT.Length > 0)
            {
                return File(electricityBill.PDF_CONTENT, "application/pdf");
            }
            return null;
        }


        [AllowAnonymous]
        //20231229搜尋年月日
        /*public ActionResult WaterList(string factory = "")
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.WATER_BILL.ToList();
            DateTime startDate;
            DateTime endDate;

            if (!String.IsNullOrEmpty(Request.QueryString["startDate"]))
            {
                startDate = DateTime.Parse(Request.QueryString["startDate"]);
            }
            else
            {
                startDate = DateTime.MinValue;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
            {
                endDate = DateTime.Parse(Request.QueryString["endDate"]);
            }
            else
            {
                endDate = DateTime.MaxValue;
            }


            var ESGs = _db.WATER_BILL
            .OrderByDescending(x => x.FROM_BILLING_PERIOD)
            .Where(x => x.FROM_BILLING_PERIOD >= startDate &&
            x.FROM_BILLING_PERIOD <= endDate)
            .ToList();
            if (!string.IsNullOrEmpty(factory))
            {
                ESGs = ESGs.Where(x => x.FACTORY == factory).ToList();
            }
            // 20240325將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);

        }*/
        //20240430搜尋按鈕剩一顆就好
        public ActionResult WaterList(string factory = "", DateTime? startDate = null, DateTime? endDate = null)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            

            var customers = _db.WATER_BILL.ToList();

            var query = _db.WATER_BILL.OrderByDescending(x => x.FROM_BILLING_PERIOD).AsQueryable();

            if (startDate.HasValue)
                query = query.Where(x => x.FROM_BILLING_PERIOD >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(x => x.FROM_BILLING_PERIOD <= endDate.Value);

            if (!string.IsNullOrEmpty(factory))
                query = query.Where(x => x.FACTORY == factory);

            var ESGs = query.ToList();

           

            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);
        }

        [AllowAnonymous]
        //20240104水費新增
        public ActionResult WaterCreate()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult WaterCreate(WATER_BILL customer)
        //20240322新增PDF上傳
                
        public ActionResult WaterCreate(WATER_BILL customer, HttpPostedFileBase pdfFile)
        {
            //20240424文件大小超過 3MB跳出提示
            if (pdfFile != null && pdfFile.ContentLength > 3 * 1024 * 1024)
            {
                // 文件大小超過 3MB
                //ModelState.AddModelError("pdfFile", "上傳的 PDF 文件大小不能超過 2MB。");
                // 將錯誤訊息添加到 TempData,以便在視圖中顯示
                TempData["ErrorMessage"] = "上傳的 PDF 文件大小不能超過 3MB。";
                return View(customer);
            }

            if (ModelState.IsValid)
            {
                string custId = customer.DOCUMENT_ID;
                var temp = _db.WATER_BILL
                    .Where(m => m.DOCUMENT_ID == custId)
                    .FirstOrDefault();

                if (temp == null)
                {
                    

                    // HomeController_20231226紀錄登打時間
                    customer.data = DateTime.Now;

                    // 從 Session 讀取當前登錄用戶
                    var username = Session["Member"] as tMember;

                    // 將當前登錄用戶賦值給 CreatedBy 屬性
                    customer.CreatedBy = username.fUserId;

                    
                    // 上傳PDF 檔案
                    if (pdfFile != null && pdfFile.ContentLength > 0)
                    {
                        // 讀取上傳的 PDF 檔案內容
                        var pdfContent = new byte[pdfFile.ContentLength];
                        pdfFile.InputStream.Read(pdfContent, 0, pdfFile.ContentLength);

                        // 將 PDF 檔案內容保存到模型中
                        customer.PDF_CONTENT = pdfContent;
                    }


                    _db.WATER_BILL.Add(customer);
                    


                    _db.SaveChanges();  // 保存更改到資料庫
                    return RedirectToAction("WaterList");
                }

                ViewBag.Msg = "單據號碼重複";
            }

            // 如果模型驗證失敗，返回 View 以顯示錯誤消息
            return View(customer);
        }


        // GET: Edit Water
        [AllowAnonymous]
        
         public ActionResult WaterEdit(string id)
        {
            var water = _db.WATER_BILL.FirstOrDefault(x => x.DOCUMENT_ID == id);
            WATER_BILL photo = _db.WATER_BILL.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }
            return View(water);
        }
        // POST: Edit Water
        [AllowAnonymous]
        [HttpPost]
        //public ActionResult WaterEdit(WATER_BILL water)
        // 上傳PDF 檔案
        public ActionResult WaterEdit(WATER_BILL water, HttpPostedFileBase pdfFile)
        {

            var model = _db.WATER_BILL.FirstOrDefault(x => x.DOCUMENT_ID == water.DOCUMENT_ID);
            //20250325水費要顯示 PDF 檔案

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {
                
                model.PDF_CONTENT = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(model.PDF_CONTENT, 0, pdfFile.ContentLength);
            }
            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                model.FACTORY = water.FACTORY;
                model.WATER_SIGNAL = water.WATER_SIGNAL;
                model.FROM_BILLING_PERIOD = water.FROM_BILLING_PERIOD;
                model.UNTIL_BILLING_PERIOD = water.UNTIL_BILLING_PERIOD;
                model.TYPE = water.TYPE;
                model.METER_NUMBER = water.METER_NUMBER;
                model.METER_DIAMETER = water.METER_DIAMETER;
                model.NUMBER_POINTERS = water.NUMBER_POINTERS;
                model.TOTAL_WATER = water.TOTAL_WATER;
                model.TOTAL_BILL_TAX = water.TOTAL_BILL_TAX;
                model.CARBON_PERIOD = water.CARBON_PERIOD;
                model.CARBON_EMISSION_FACTOR = water.CARBON_EMISSION_FACTOR;
                model.CARBON_DIOXIDE = water.CARBON_DIOXIDE;
                model.VOUCHER_NUMBER = water.VOUCHER_NUMBER;
            }           
            

            _db.SaveChanges();

            return RedirectToAction("WaterList");
        }

        //20250325水費要顯示 PDF 檔案

        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile(string id)
        {
            var waterBill = _db.WATER_BILL.FirstOrDefault(x => x.DOCUMENT_ID == id);
            if (waterBill != null && waterBill.PDF_CONTENT != null && waterBill.PDF_CONTENT.Length > 0)
            {
                return File(waterBill.PDF_CONTENT, "application/pdf");
            }
            return null;
        }


        [AllowAnonymous]
        //20240116員工通勤-新增
        public ActionResult GHG_MST_COMMUTECreate()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult GHG_MST_COMMUTECreate(GHG_MST_COMMUTE customer)
        {
            if (ModelState.IsValid)
            {
                string custId = customer.USER_ID;
                var temp = _db.GHG_MST_COMMUTE
                    .Where(m => m.USER_ID == custId)
                    .FirstOrDefault();

                if (temp == null)
                {
                    // HomeController_20231226紀錄登打時間
                    customer.data = DateTime.Now;

                    // 從 Session 讀取當前登錄用戶
                    var username = Session["Member"] as tMember;

                    // 將當前登錄用戶賦值給 CreatedBy 屬性
                    customer.CreatedBy = username.fUserId;

                    _db.GHG_MST_COMMUTE.Add(customer);



                    _db.SaveChanges();  // 保存更改到資料庫
                    return RedirectToAction("GHG_MST_COMMUTE_Index");
                }

                ViewBag.Msg = "員工編號重複";
            }

            // 如果模型驗證失敗，返回 View 以顯示錯誤消息
            return View(customer);
        }



        int pageSize = 10;
        [AllowAnonymous]
        // GET: Home
        public ActionResult GHG_MST_COMMUTE_Index(string userId, int page = 1)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            int currentPage = page < 1 ? 1 : page;
            var allProducts = _db.GHG_MST_COMMUTE.ToList(); // 最初獲取所有數據
            var products = _db.GHG_MST_COMMUTE.AsQueryable();

            // 如果提供了參數，則應用篩選
            if (!string.IsNullOrEmpty(userId))
            {
                products = products.Where(x => x.USER_ID == userId);
            }

           

            products = products.OrderBy(x => x.USER_ID);

            var filteredProducts = products;

            // 根據年份進行附加篩選


            var result = filteredProducts.ToPagedList(currentPage, pageSize);

            // 將數據和篩選條件存儲在 ViewBag 中，以便在視圖中使用
            ViewBag.UserId = userId;
            
            ViewBag.AllProducts = allProducts;
            // 將 year 儲存到 ViewBag
         

            return View(result);
        }

        // GET: Edit GHG_MST_COMMUTE
        [AllowAnonymous]
        public ActionResult GHG_MST_COMMUTE_Edit(string id)
        {
            var commute = _db.GHG_MST_COMMUTE.FirstOrDefault(x => x.USER_ID == id);
            

            return View(commute);
        }
        // POST: Edit GHG_MST_COMMUTE
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GHG_MST_COMMUTE_Edit(GHG_MST_COMMUTE commute)
        {
            var model = _db.GHG_MST_COMMUTE.FirstOrDefault(x => x.USER_ID == commute.USER_ID);

            model.USER_NAME = commute.USER_NAME;
            model.TRANSPORTATION = commute.TRANSPORTATION;
            model.ONEWAY_KM = commute.ONEWAY_KM;
            model.DOUBLE_KM = commute.DOUBLE_KM;

            _db.SaveChanges();

            return RedirectToAction("GHG_MST_COMMUTE_Index");
        }

        //GHG_MST_COMMUTE內容跟CAT_THREE_EMPLOYEE_COMMUTING內容
        //使用員工工號查詢
        //使用部門查詢
        [AllowAnonymous]
        public ActionResult CombinedIndex(string userId, string departmentName, int? year, int page = 1)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }

            DateTime today = DateTime.Today;
            int currentPage = page < 1 ? 1 : page; // Assign a value

            var combinedData = (from emp in _db.CAT_THREE_EMPLOYEE_COMMUTING
                                join commute in _db.GHG_MST_COMMUTE on emp.USER_ID equals commute.USER_ID
                                where (string.IsNullOrEmpty(userId) || emp.USER_ID == userId) &&
                                      (string.IsNullOrEmpty(departmentName) || emp.DEPARTMENT_NAME.Contains(departmentName))

                                group new { emp, commute } by emp.USER_ID into groupedData
                                select new CombinedViewModel
                                {
                                    USER_ID = groupedData.Key,
                                    USER_NAME = groupedData.FirstOrDefault().emp.USER_NAME,
                                    FIRST_WORK_DATE = groupedData.FirstOrDefault().emp.FIRST_WORK_DATE,
                                    LAST_WORK_DATE = groupedData.FirstOrDefault().emp.LAST_WORK_DATE,
                                    Status = string.IsNullOrEmpty(groupedData.FirstOrDefault().emp.LAST_WORK_DATE) ? "在職" : "離職",
                                    USER_ADDRESS = groupedData.FirstOrDefault().emp.USER_ADDRESS,
                                    DEPARTMENT_NAME = groupedData.FirstOrDefault().emp.DEPARTMENT_NAME,
                                    TITLE_NAME = groupedData.FirstOrDefault().emp.TITLE_NAME,
                                    SHIFT_NAME = groupedData.FirstOrDefault().emp.SHIFT_NAME,
                                    CLOCK_IN = groupedData.FirstOrDefault().emp.CLOCK_IN,
                                    CLOCK_OUT = groupedData.FirstOrDefault().emp.CLOCK_OUT,
                                    WORKING_HOURS = groupedData.FirstOrDefault().emp.WORKING_HOURS,
                                    UNIT = groupedData.FirstOrDefault().emp.UNIT,
                                    USEDT = groupedData.FirstOrDefault().commute.USEDT,
                                    TRANSPORTATION = groupedData.FirstOrDefault().commute.TRANSPORTATION,
                                    ONEWAY_KM = groupedData.FirstOrDefault().commute.ONEWAY_KM,
                                    DOUBLE_KM = groupedData.FirstOrDefault().commute.DOUBLE_KM,
                                    WORK_DATE_COUNT = groupedData.Count(e => e.emp.WORK_DATE.Substring(0, 4) ==year.ToString()),
                                }).OrderBy(m => m.USER_ID).ToList();

            var deptNames = _db.CAT_THREE_EMPLOYEE_COMMUTING
                .Select(x => x.DEPARTMENT_NAME)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var emissionFactors = _db.BRM_MST_EMISSION_FACTOR

         .Where(x => x.EF_YEAR.ToString() == year.ToString() &&
                     _db.GHG_MST_COMMUTE.Any(y => y.TRANSPORTATION == x.EF_NAME))
                    .ToDictionary(x => x.EF_NAME, x => x.EF_VALUE);

            foreach (var item in combinedData)
            {
                if (emissionFactors.ContainsKey(item.TRANSPORTATION))
                {
                    item.EmissionFactor = emissionFactors[item.TRANSPORTATION];
                }
                else
                {
                    item.EmissionFactor = 0; 
                }
                item.WORK_DATE = item.WORK_DATE_COUNT;

                if (item.DOUBLE_KM != null)
                {
                    item.ActivityData = Convert.ToDecimal(item.WORK_DATE) * (decimal)item.DOUBLE_KM;
                }
                else
                {
                    item.ActivityData = 0; 
                }

                //碳排放量 = 活動數據 x 碳排係數
                item.Emissions = item.ActivityData * item.EmissionFactor;

                //CO2 排放量 = 碳排放量 * (44/12) / 1000,算出KG再換算為噸 = 公斤 / 1000
                item.CO2e = item.Emissions * (44.0m / 12.0m) / 1000;
            }
            decimal totalCO2e = 0;

            foreach (var item in combinedData.Where(x => x.Status != "離職"))
            {
                // 計算CO2e的現有程式碼

                totalCO2e += item.CO2e;
            }

            ViewBag.TotalCO2e = totalCO2e;
            ViewBag.DeptNames = new SelectList(deptNames);
            ViewBag.UserId = userId;
            ViewBag.DepartmentName = departmentName;
            ViewBag.TotalCO2e = totalCO2e;
            ViewBag.Year = year ?? DateTime.Now.Year;
            return View(combinedData.ToList());
        }


        //出勤天數:跳出視窗:詳細放大版
        //CAT_THREE_EMPLOYEE_COMMUTING內容
        // GET: Home
        [AllowAnonymous]
        public ActionResult CAT_THREE_EMPLOYEE_COMMUTING_Index1(string userId, string departmentName, int? year, int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var allProducts = _db.CAT_THREE_EMPLOYEE_COMMUTING.ToList(); // 最初獲取所有數據
            var products = _db.CAT_THREE_EMPLOYEE_COMMUTING.AsQueryable();

            // 如果提供了參數，則應用篩選
            if (!string.IsNullOrEmpty(userId))
            {
                products = products.Where(x => x.USER_ID == userId);
            }

            if (!string.IsNullOrEmpty(departmentName))
            {
                products = products.Where(x => x.DEPARTMENT_NAME.Contains(departmentName));
            }

            products = products.OrderBy(x => x.USER_ID);

            var filteredProducts = products;

            // 根據年份進行附加篩選
            if (year.HasValue)
            {
                filteredProducts = filteredProducts.Where(x => x.WORK_DATE.Substring(0, 4) == year.Value.ToString());
            }

            var result = filteredProducts.ToPagedList(currentPage, pageSize);

            // 將數據和篩選條件存儲在 ViewBag 中，以便在視圖中使用
            ViewBag.UserId = userId;
            ViewBag.DepartmentName = departmentName;
            ViewBag.AllProducts = allProducts;
            // 將 year 儲存到 ViewBag
            ViewBag.Year = year;

            return View(result);
        }

        //20240328出差申請單欄位設計-太複雜
        [AllowAnonymous]
        public ActionResult BUSINESS_TRIP_List()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
         
            var ESGs = _db.BUSINESS_TRIP.ToList();            

            return View(ESGs);

        }
        //20240328出差申請單欄位設計
        [AllowAnonymous]
        public ActionResult BUSINESS_TRIP_List2()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            var ESGs = _db.TRIP_APPLICATION.ToList();

            return View(ESGs);

        }
        //20240408出差申請單:Word下載
        [AllowAnonymous]
        public FileResult DownloadTemplate()
        {
            // 指定 templates 資料夾的路徑
            //string filePath = Server.MapPath("~/templates/範本.docx");
            string filePath = Server.MapPath("~/templates/範本2.docx");
            //string filePath = Server.MapPath("~/templates/範本2本機.docx");
            // 回傳檔案供下載
            //return File(filePath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "範本.docx");
            return File(filePath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "範本2.docx");
            //return File(filePath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "範本2本機.docx");
        }

        [AllowAnonymous]
  
        public ActionResult BUSINESS_TRIP2_Create()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult BUSINESS_TRIP2_Create(TRIP_APPLICATION customer)
        {
            string custId = customer.DOCUMENT_ID;
            var temp = _db.TRIP_APPLICATION.FirstOrDefault(m => m.DOCUMENT_ID == custId);

            if (temp == null)
            { // 取得上一筆單據的單據編號
                var lastProduct = _db.TRIP_APPLICATION.OrderByDescending(p => p.data).FirstOrDefault();
                string lastNumber = lastProduct?.DOCUMENT_ID;

                // 產生新的單據編號
                string newNumber;
                if (string.IsNullOrEmpty(lastNumber))
                {
                    // 如果是第一筆資料, 單據編號為 "20230408001"
                    newNumber = $"{DateTime.Now.ToString("yyyyMMdd")}001";
                }
                else
                {
                    // 如果不是第一筆資料, 單據編號自動遞增
                    int lastNumberInt = int.Parse(lastNumber.Substring(8));
                    newNumber = $"{DateTime.Now.ToString("yyyyMMdd")}{(lastNumberInt + 1).ToString("D3")}";
                }
                // 20240408計算出差天數
                if (customer.UNTIL_PERIOD.HasValue && customer.FROM_PERIOD.HasValue)
                {
                    //customer.DAYS = (customer.UNTIL_PERIOD.Value.Date - customer.FROM_PERIOD.Value.Date).Days + 1;
                    //20240408計算出差天數，課長說不要加一
                    customer.DAYS = (customer.UNTIL_PERIOD.Value.Date - customer.FROM_PERIOD.Value.Date).Days;
                }
                else
                {
                    customer.DAYS = null;
                }

                customer.DOCUMENT_ID = newNumber; // 設置新的單據編號
                
                customer.data = DateTime.Now;
                var username = Session["Member"] as tMember;
                customer.CreatedBy = username?.fUserId;

                _db.TRIP_APPLICATION.Add(customer);
                _db.SaveChanges();
                return RedirectToAction("BUSINESS_TRIP_List2");
            }
            ViewBag.Msg = "聯單編號重複";
            return View(customer); 
        }


        // 20240408出差申請單-修改功能
        [AllowAnonymous]
     
        public ActionResult BUSINESS_TRIP2_Edit(string id)
        {
            var customer = _db.TRIP_APPLICATION.FirstOrDefault(x => x.DOCUMENT_ID == id);
            TRIP_APPLICATION photo = _db.TRIP_APPLICATION.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }
            return View(customer);
        }
      
        [AllowAnonymous]
        [HttpPost]
        
   
        public ActionResult BUSINESS_TRIP2_Edit(TRIP_APPLICATION customer)
        {

            var model = _db.TRIP_APPLICATION.FirstOrDefault(x => x.DOCUMENT_ID == customer.DOCUMENT_ID);
            //要顯示 PDF 檔案
            // 20240408計算出差天數
            if (customer.UNTIL_PERIOD.HasValue && customer.FROM_PERIOD.HasValue)
            {
                //customer.DAYS = (customer.UNTIL_PERIOD.Value.Date - customer.FROM_PERIOD.Value.Date).Days + 1;
                //20240408計算出差天數，課長說不要加一
                customer.DAYS = (customer.UNTIL_PERIOD.Value.Date - customer.FROM_PERIOD.Value.Date).Days;
            }
            else
            {
                customer.DAYS = null;
            }

            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {// model.DAYS = customer.DAYS;放的位置有影響，將計算好的天數賦值給模型
                model.DEPARTMENT_NAME = customer.DEPARTMENT_NAME;
                model.USER_NAME = customer.USER_NAME;
                model.OFFICE_AGENT = customer.OFFICE_AGENT;
                model.FROM_PERIOD = customer.FROM_PERIOD;
                model.UNTIL_PERIOD = customer.UNTIL_PERIOD;
                model.DAYS = customer.DAYS;
                model.TRIP_LOCATION = customer.TRIP_LOCATION;
                model.CONTACT_NUMBER = customer.CONTACT_NUMBER;
                model.APPLICATION_INFORMATION = customer.APPLICATION_INFORMATION;
                model.REASON = customer.REASON;
                model.VISIT_CONTENT = customer.VISIT_CONTENT;
               

            }
           

            _db.SaveChanges();

            return RedirectToAction("BUSINESS_TRIP_List2");
        }
        //20240409增加:計程車-出差申請單欄位

        [AllowAnonymous]
     
        public ActionResult TAXI_APPLICATIONList()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.TAXI_APPLICATION.ToList();
            
            var ESGs = _db.TAXI_APPLICATION.ToList();

            // 20240409將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);

        }
        //20240409增加:計程車-出差申請單顯示 PDF 檔案

        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile3(string id)
        {
            var taxiBill = _db.TAXI_APPLICATION.FirstOrDefault(x => x.DOCUMENT_ID == id);
            if (taxiBill != null && taxiBill.PDF_CONTENT != null && taxiBill.PDF_CONTENT.Length > 0)
            {
                return File(taxiBill.PDF_CONTENT, "application/pdf");
            }
            return null;
        }


        [AllowAnonymous]
        //20240409增加:計程車-出差申請單:新增
        public ActionResult TAXI_APPLICATIONL_Create()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        

        public ActionResult TAXI_APPLICATIONL_Create(TAXI_APPLICATION customer, HttpPostedFileBase pdfFile)
        {
            if (ModelState.IsValid)
            {
                string custId = customer.DOCUMENT_ID;
                var temp = _db.TAXI_APPLICATION
                    .Where(m => m.DOCUMENT_ID == custId)
                    .FirstOrDefault();

                if (temp == null)
                {


                    // HomeController_20231226紀錄登打時間
                    customer.data = DateTime.Now;

                    // 從 Session 讀取當前登錄用戶
                    var username = Session["Member"] as tMember;

                    // 將當前登錄用戶賦值給 CreatedBy 屬性
                    customer.CreatedBy = username.fUserId;


                    // 上傳PDF 檔案
                    if (pdfFile != null && pdfFile.ContentLength > 0)
                    {
                        // 讀取上傳的 PDF 檔案內容
                        var pdfContent = new byte[pdfFile.ContentLength];
                        pdfFile.InputStream.Read(pdfContent, 0, pdfFile.ContentLength);

                        // 將 PDF 檔案內容保存到模型中
                        customer.PDF_CONTENT = pdfContent;
                    }


                    _db.TAXI_APPLICATION.Add(customer);



                    _db.SaveChanges();  // 保存更改到資料庫
                    
                    return RedirectToAction("TAXI_APPLICATIONList"); // 修改這裡
                }

                ViewBag.Msg = "單據號碼重複";
            }

            // 如果模型驗證失敗，返回 View 以顯示錯誤消息
            return View(customer);
        }

        //20240409增加:計程車-出差申請單:修改
        [AllowAnonymous]

        public ActionResult TAXI_APPLICATIONL_Edit(string id)
        {
            var taxi = _db.TAXI_APPLICATION.FirstOrDefault(x => x.DOCUMENT_ID == id);
            TAXI_APPLICATION photo = _db.TAXI_APPLICATION.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }
            return View(taxi);
        }
      
        [AllowAnonymous]
        [HttpPost]
        
        
        public ActionResult TAXI_APPLICATIONL_Edit(TAXI_APPLICATION taxi, HttpPostedFileBase pdfFile)
        {

            var model = _db.TAXI_APPLICATION.FirstOrDefault(x => x.DOCUMENT_ID == taxi.DOCUMENT_ID);
            //顯示 PDF 檔案

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {

                model.PDF_CONTENT = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(model.PDF_CONTENT, 0, pdfFile.ContentLength);
            }
            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                model.BASE_NAME = taxi.BASE_NAME;
                model.DOCUMENT_DATA = taxi.DOCUMENT_DATA;
                model.AMOUNT = taxi.AMOUNT;
                model.KILOMETERS = taxi.KILOMETERS;
                model.REMARK = taxi.REMARK;
                model.VOUCHER_NUMBER = taxi.VOUCHER_NUMBER;
                model.INFORMATION = taxi.INFORMATION;
                
            }


            _db.SaveChanges();

            return RedirectToAction("TAXI_APPLICATIONList");
        }
        //20250410高鐵費用要顯示 PDF 檔案

        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile4(string id)
        {
            var highSpeed = _db.HIGH_SPEED_RAIL.FirstOrDefault(x => x.DOCUMENT_ID == id);
            if (highSpeed != null && highSpeed.PDF_CONTENT != null && highSpeed.PDF_CONTENT.Length > 0)
            {
                return File(highSpeed.PDF_CONTENT, "application/pdf");
            }
            return null;
        }

        //20240430加入類別-選項
        [AllowAnonymous]
        public ActionResult HIGH_SPEED_RAIL_List(string filterOption)
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            IQueryable<ACPTB> query = _TWNCPCdb.ACPTB;

            switch (filterOption)
            {
                case "高鐵":
                    query = query.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                case "飛機":
                    query = query.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "計程車":
                    query = query.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "私車公用":
                    query = query.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "公務車":
                    query = query.Where(x => x.UDF01.Contains("公務車"));
                    break;
                default:
                    query = query.Where(x => x.UDF01.Contains("高鐵") || x.UDF01.Contains("飛機") || x.UDF01.Contains("計程車") || x.UDF01.Contains("私車公用") || x.UDF01.Contains("公務車"));
                    break;
            }

            var customers = query
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.TB011,
                    x.TB001,
                    x.TB002,
                    x.TB003
                })
                .ToList();

            var viewModels = customers.Select(x => new HighSpeedRailViewModel
            {
                UDF01 = x.UDF01,
                CREATE_DATE = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == x.TB001 && y.TA002 == x.TB002)?.TA003,
                TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == x.TB001 && y.TA002 == x.TB002)?.TA015,
                UDF02 = x.UDF02,
                UDF03 = x.UDF03,
                UDF06 = x.UDF06,
                TB011 = x.TB011,
                TB001 = x.TB001,
                TB002 = x.TB002,
                TB003 = x.TB003,
                TA004 = (from acpta in _TWNCPCdb.ACPTA
                         where acpta.TA001 == x.TB001 && acpta.TA002 == x.TB002
                         join purma in _TWNCPCdb.PURMA on acpta.TA004 equals purma.MA001
                         select purma.MA002).FirstOrDefault()
            }).ToList();

            return View(viewModels);


        }





        //20240430加入類別-選項
        [AllowAnonymous]
        //20240507加入發票篩選
        //public ActionResult HIGH_SPEED_RAIL_List(string filterOption)
        public ActionResult HIGH_SPEED_RAIL_List2(string filterOption, string startDate, string endDate)
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            IQueryable<PCMTG> query = _TWNCPCdb.PCMTG;

            // 篩選發票日期
            DateTime? startDateValue = null, endDateValue = null;
            if (!string.IsNullOrEmpty(startDate))
                startDateValue = DateTime.ParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(endDate))
                endDateValue = DateTime.ParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture).AddDays(0);
            //endDateValue = DateTime.ParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture).AddDays(1); // 將結束日期設為次日

            // 篩選其他條件
            switch (filterOption)
            {
                case "高鐵":
                    query = query.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                case "飛機":
                    query = query.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "計程車":
                    query = query.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "私車公用":
                    query = query.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "公務車":
                    query = query.Where(x => x.UDF01.Contains("公務車"));
                    break;
                default:
                    query = query.Where(x => x.UDF01.Contains("高鐵") || x.UDF01.Contains("飛機") || x.UDF01.Contains("計程車") || x.UDF01.Contains("私車公用") || x.UDF01.Contains("公務車"));
                    break;
            }


            var customers = query
   .Select(x => new
   {
       x.UDF01,
       x.UDF02,
       x.UDF03,
       x.UDF06,
       x.TG024,
       x.TG013, // 保留原始日期字串
        x.TG001,
       x.TG002,
       x.TG003
   })
   .Where(y => (startDateValue == null || y.TG013.CompareTo(startDate) >= 0) && // 直接使用字串比較
               (endDateValue == null || y.TG013.CompareTo(endDate) < 0)) // 直接使用字串比較
   .ToList()
   .Select(y => new
   {
       y.UDF01,
       y.UDF02,
       y.UDF03,
       y.UDF06,
       y.TG024,
       InvoiceDate = DateTime.ParseExact(y.TG013, "yyyyMMdd", CultureInfo.InvariantCulture),
       y.TG001,
       y.TG002,
       y.TG003
   })
   .ToList();


            var viewModels = customers.Select(x => new HighSpeedRailViewModel
            {
                UDF01 = x.UDF01,
                CREATE_DATE = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == x.TG001 && y.TF002 == x.TG002)?.TF003,
                //發票日期
                //TA015 = x.TG013,
                TA015 = x.InvoiceDate.ToString("yyyyMMdd"), // 使用 InvoiceDate 並格式化為所需的日期格式
                UDF02 = x.UDF02,
                UDF03 = x.UDF03,
                UDF06 = x.UDF06,
                TB011 = x.TG024,
                TB001 = x.TG001,
                TB002 = x.TG002,
                TB003 = x.TG003,
                TA004 = (from pcmtf in _TWNCPCdb.PCMTF
                         where pcmtf.TF001 == x.TG001 && pcmtf.TF002 == x.TG002
                         join purma in _TWNCPCdb.PURMA on pcmtf.TF008 equals purma.MA001
                         select purma.MA002).FirstOrDefault()
            }).ToList();

            return View(viewModels);
        }

        //20240509把發票日跟單據日用RadioButton選出
        //20240516測試應付憑單_連結
        [AllowAnonymous]


        public ActionResult CombinedHighSpeedRailList2(string filterOption, string keyword, string startDate, string endDate, string startDocDate, string endDocDate)
        { 
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            // 取得 CombinedHighSpeedRailList 的資料:加入應付憑單連結:條件篩選
            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<ACPTB> acptbQuery = _TWNCPCdb.ACPTB            
            .Join(_TWNCPCdb.AJSTB,
     acptb => new { TB013 = acptb.TB001, TB014 = acptb.TB002, TB052 = acptb.TB003 },
     ajstb => new { TB013 = ajstb.TB001, TB014 = ajstb.TB002, TB052 = ajstb.TB052 },
     (acptb, ajstb) => new { ACPTB = acptb, AJSTB = ajstb })
        .Join(_TWNCPCdb.AJSTA,
     acptbAjstb => new { TA001 = acptbAjstb.AJSTB.TB001, TA002 = acptbAjstb.AJSTB.TB002 },
     ajsta => new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 },
     (acptbAjstb, ajsta) => new { ACPTBAJSTB = acptbAjstb, AJSTA = ajsta })

        .Where(x => (x.ACPTBAJSTB.ACPTB.UDF01.Contains("公務車") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("地鐵") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("客運") ||
        x.ACPTBAJSTB.ACPTB.UDF01.Contains("捷運") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("發電機用油") ||
        x.ACPTBAJSTB.ACPTB.UDF01.Contains("私車公用") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("計程車") || 
        x.ACPTBAJSTB.ACPTB.UDF01.Contains("飛機") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("高鐵"))
            && (string.IsNullOrEmpty(keyword) || x.ACPTBAJSTB.ACPTB.UDF01.Contains(keyword) ||
            x.ACPTBAJSTB.ACPTB.UDF02.Contains(keyword) || x.ACPTBAJSTB.ACPTB.UDF03.Contains(keyword) || 
            x.ACPTBAJSTB.ACPTB.UDF06.ToString().Contains(keyword) || x.ACPTBAJSTB.ACPTB.TB011.Contains(keyword) || 
            x.ACPTBAJSTB.ACPTB.TB001.Contains(keyword) || x.ACPTBAJSTB.ACPTB.TB002.Contains(keyword) || x.ACPTBAJSTB.ACPTB.TB003.Contains(keyword)
                          || _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.ACPTBAJSTB.ACPTB.TB001 && y.TA002 == x.ACPTBAJSTB.ACPTB.TB002 && (y.TA003.ToString().Contains(keyword) || y.TA015.ToString().Contains(keyword) || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TA004 && z.MA002.Contains(keyword))))))
        
        .Select(x => x.ACPTBAJSTB.ACPTB);


  


            //|| _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.ACPTBAJSTB.ACPTB.TB001 && y.TA002 == x.TB002 && (y.TA003.ToString().Contains(keyword) || y.TA015.ToString().Contains(keyword) || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TA004 && z.MA002.Contains(keyword))))));




            /*.Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
        x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
            && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TB011.Contains(keyword) || x.TB001.Contains(keyword) || x.TB002.Contains(keyword) || x.TB003.Contains(keyword)
            || _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 && (y.TA003.ToString().Contains(keyword) || y.TA015.ToString().Contains(keyword) || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TA004 && z.MA002.Contains(keyword))))));
            */


            /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))*/
            // 篩選單據日期:包含當天的日期
            /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
           && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));
            */

            /*.Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));*/
            //包含當天的日期

            /*.Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));
            */

            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }*/
            // 篩選單據日期:包含當天的日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }*/

            // 篩選發票日期
            /*if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }*/

            // 篩選發票日期:包含當天的日期
            /*if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }*/

            switch (filterOption)
{
    case "公務車":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("公務車"));
        break;
    case "地鐵":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("地鐵"));
        break;
    case "客運":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("客運"));
        break;
    case "捷運":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("捷運"));
        break;
    case "發電機用油":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("發電機用油"));
        break;
    case "私車公用":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("私車公用"));
        break;
    case "計程車":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("計程車"));
        break;
    case "飛機":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("飛機"));
        break;
    case "高鐵":
        acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("高鐵"));
        break;
        // 其他 case 略
}



var acptbCustomers = acptbQuery
    .Select(x => new
    {
        x.UDF01,
        x.UDF02,
        x.UDF03,
        x.UDF06,
        x.TB011,
        x.TB001,
        x.TB002,
        x.TB003
    })
    .ToList();
            /*var acptbCustomers = acptbQuery
                .Select(x => new
                {
                    UDF01 = x.ACPTBAJSTB.ACPTB.UDF01,
                    UDF02 = x.ACPTBAJSTB.ACPTB.UDF02,
                    UDF03 = x.ACPTBAJSTB.ACPTB.UDF03,
                    UDF06 = x.ACPTBAJSTB.ACPTB.UDF06,
                    TB011 = x.ACPTBAJSTB.ACPTB.TB011,
                    TB001 = x.ACPTBAJSTB.ACPTB.TB001,
                    TB002 = x.ACPTBAJSTB.ACPTB.TB002,
                    TB003 = x.ACPTBAJSTB.ACPTB.TB003,
                    AJSTB = x.ACPTBAJSTB.AJSTB,
                    AJSTA = x.ACPTBAJSTB.AJSTA
                })
                .ToList();*/


            // 篩選發票日期
            IQueryable<PCMTG> query = _TWNCPCdb.PCMTG;


// 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
IQueryable<PCMTG> pcmtgQuery = _TWNCPCdb.PCMTG.Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
    && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TG024.Contains(keyword) || x.TG013.Contains(keyword) || x.TG001.Contains(keyword) || x.TG002.Contains(keyword) || x.TG003.Contains(keyword)
    || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && (y.TF003.ToString().Contains(keyword) || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TF008 && z.MA002.Contains(keyword))))))

 /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
&& (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));*/
 //包含當天的日期
 .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
&& (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));



// 篩選單據日期
/*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
{
    pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
}*/
//包含當天的日期:篩選單據日期
if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
{
    pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
}

switch (filterOption)
{
    case "公務車":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("公務車"));
        break;
    case "地鐵":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("地鐵"));
        break;
    case "客運":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("客運"));
        break;
    case "捷運":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("捷運"));
        break;
    case "發電機用油":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("發電機用油"));
        break;
    case "私車公用":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("私車公用"));
        break;
    case "計程車":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("計程車"));
        break;
    case "飛機":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("飛機"));
        break;
    case "高鐵":
        pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("高鐵"));
        break;
        // 其他 case 略
}
// 篩選發票日期
/*pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
    && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) < 0 || string.IsNullOrEmpty(endDate)));*/

// 篩選發票日期:包含當天的日期
if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
{
    pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
    && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate)));
}
var pcmtgCustomers = pcmtgQuery
    .Select(x => new
    {
        x.UDF01,
        x.UDF02,
        x.UDF03,
        x.UDF06,
        x.TG024,
        x.TG013,
        x.TG001,
        x.TG002,
        x.TG003
    })
/*.Where(y => (startDateValue == null || y.TG013.CompareTo(startDate) >= 0) && // 直接使用字串比較
            (endDateValue == null || y.TG013.CompareTo(endDate) < 0)) // 直接使用字串比較*/

.ToList()
.Select(y => new
{
y.UDF01,
y.UDF02,
y.UDF03,
y.UDF06,
y.TG024,
       //InvoiceDate = DateTime.ParseExact(y.TG013, "yyyyMMdd", CultureInfo.InvariantCulture),
       //InvoiceDate = y.TG013,
       InvoiceDate = y.TG013.PadRight(8, '0'), // 將 TG013 補足為8位數後再賦值給 InvoiceDate

       y.TG001,
y.TG002,
y.TG003
})
.ToList();


List<HighSpeedRailViewModel> viewModels = new List<HighSpeedRailViewModel>();

// 建立 HighSpeedRailViewModel 物件,並加入 viewModels 清單
foreach (var acptbCustomer in acptbCustomers)
{
    HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
    {
        UDF01 = acptbCustomer.UDF01,
        CREATE_DATE = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA003,
        //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015,
        //TA015 = acptbCustomer.ACPTAs.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
        //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
        //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015?.ToString("yyyyMMdd"),
        //20240513包含當天的日期
        //20240513包含當天的日期
        /*TF015 = (from ajsta in _TWNCPCdb.AJSTA
                 join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                 join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                 from acpt in acptbJoin.DefaultIfEmpty()
                 select ajsta.TA004).FirstOrDefault(),

        TF016 = (from ajsta in _TWNCPCdb.AJSTA
                 join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                 join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                 from acpt in acptbJoin.DefaultIfEmpty()
                 select ajsta.TA005).FirstOrDefault(),*/


        //20240516程式碼沒報錯，但是顯示找不到
        TF015 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA004,
        TF016 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA005,
        TA015 = (from acpta in _TWNCPCdb.ACPTA
                 where acpta.TA001 == acptbCustomer.TB001
                    && acpta.TA002 == acptbCustomer.TB002
                 orderby acpta.TA015 descending
                 select acpta.TA015).FirstOrDefault(),
        UDF02 = acptbCustomer.UDF02,
        UDF03 = acptbCustomer.UDF03,
        UDF06 = acptbCustomer.UDF06,
        TB011 = acptbCustomer.TB011,
        TB001 = acptbCustomer.TB001,
        TB002 = acptbCustomer.TB002,
        TB003 = acptbCustomer.TB003,
        TA004 = (from acpta in _TWNCPCdb.ACPTA
                 where acpta.TA001 == acptbCustomer.TB001 && acpta.TA002 == acptbCustomer.TB002
                 join purma in _TWNCPCdb.PURMA on acpta.TA004 equals purma.MA001
                 select purma.MA002).FirstOrDefault()
    };

    viewModels.Add(viewModel);
}

foreach (var pcmtgCustomer in pcmtgCustomers)
{
    HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
    {
        UDF01 = pcmtgCustomer.UDF01,
        CREATE_DATE = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF003,
        TF015 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF015,
        TF016 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF016,
        // TA015 = pcmtgCustomer.TG013,
        //TA015 = pcmtgCustomer.InvoiceDate.ToString("yyyyMMdd"), // 使用 InvoiceDate 並格式化為所需的日期格式
        TA015 = pcmtgCustomer.InvoiceDate,
        //20240513包含當天的日期


        UDF02 = pcmtgCustomer.UDF02,
        UDF03 = pcmtgCustomer.UDF03,
        UDF06 = pcmtgCustomer.UDF06,
        TB011 = pcmtgCustomer.TG024,
        TB001 = pcmtgCustomer.TG001,
        TB002 = pcmtgCustomer.TG002,
        TB003 = pcmtgCustomer.TG003,
        TA004 = (from pcmtf in _TWNCPCdb.PCMTF
                 where pcmtf.TF001 == pcmtgCustomer.TG001 && pcmtf.TF002 == pcmtgCustomer.TG002
                 join purma in _TWNCPCdb.PURMA on pcmtf.TF008 equals purma.MA001
                 select purma.MA002).FirstOrDefault()
    };

    viewModels.Add(viewModel);
}

return View(viewModels);
        }


        //20240502同一個網頁顯示HIGH_SPEED_RAIL_List和HIGH_SPEED_RAIL_List2
        [AllowAnonymous]

        //20240506加入條件篩選

        //20240508加入發票日期篩選
        //public ActionResult CombinedHighSpeedRailList(string filterOption, string keyword)
        //public ActionResult CombinedHighSpeedRailList(string filterOption, string keyword, string startDate, string endDate)
        //加入單據日期篩選
        //public ActionResult CombinedHighSpeedRailList(string filterOption, string keyword, string startDate, string endDate, string startDocDate, string endDocDate)
        //public ActionResult CombinedHighSpeedRailList(string filterOption = "公務車", string keyword, string startDate, string endDate, string startDocDate, string endDocDate)
//public ActionResult CombinedHighSpeedRailList(string keyword, string startDate, string endDate, string startDocDate, string endDocDate, string filterOption = "公務車")

            //20240521加入密碼
            public ActionResult CombinedHighSpeedRailList(string keyword, string dateRangeOption, string startDocDate, string endDocDate, string startDate, string endDate, string password, string filterOption = "公務車")

        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            // 定義交通方式關鍵字
            var transportKeywords = new[] { "公務車", "地鐵", "客運", "捷運", "發電機用油", "私車公用", "計程車", "飛機", "高鐵" };



            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<ACPTB> acptbQuery = _TWNCPCdb.ACPTB
    .Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
                && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TB011.Contains(keyword) || x.TB001.Contains(keyword)
                || x.TB002.Contains(keyword) || x.TB003.Contains(keyword) || x.TB009.ToString().Contains(keyword)
                || _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 && (y.TA003.ToString().Contains(keyword) || y.TA015.ToString().Contains(keyword)
               //20240517找檔案名稱的傳票單號搜尋，可以用關鍵字找到
               /*|| _TWNCPCdb.AJSTA.Any(z => z.TA001 == y.TA001 && z.TA002 == y.TA002 && z.TA005.ToString().Contains(keyword))*/
               /*|| _TWNCPCdb.AJSTB.Any(z => z.TB001 == x.TB001 && z.TB002 == x.TB002 && _TWNCPCdb.AJSTA.Any(a => a.TA001 == z.TB001 && a.TA002 == z.TB002 && a.TA005.ToString().Contains(keyword)))*/
               || _TWNCPCdb.AJSTB.Any(z => z.TB001 == x.TB001 && _TWNCPCdb.AJSTA.Any(a => a.TA001 == z.TB001 && a.TA002 == z.TB002 && a.TA005.ToString().Contains(keyword)))


               || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TA004 && z.MA002.Contains(keyword))))))

             /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
             && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))*/
             // 篩選單據日期:包含當天的日期
             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))


            /*.Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));*/
            //包含當天的日期
            .Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));


            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }*/
            // 篩選單據日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }

            // 篩選發票日期
            /*if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }*/

            // 篩選發票日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }

            switch (filterOption)
            {
                case "公務車":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("公務車"));
                    break;
                case "地鐵":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("地鐵"));
                    break;
                case "客運":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("客運"));
                    break;
                case "捷運":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("捷運"));
                    break;
                case "發電機用油":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("發電機用油"));
                    break;
                case "私車公用":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "計程車":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "飛機":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "高鐵":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                    // 其他 case 略
            }



            var acptbCustomers = acptbQuery
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.UDF05,//20240521加入審核按紐
                    x.TB011,
                    x.TB001,
                    x.TB002,
                    x.TB003,
                    x.TB009
                })
                .ToList();




            // 篩選發票日期
            IQueryable<PCMTG> query = _TWNCPCdb.PCMTG;


            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<PCMTG> pcmtgQuery = _TWNCPCdb.PCMTG.Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
                && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TG024.Contains(keyword)
                || x.TG013.Contains(keyword) || x.TG001.Contains(keyword) || x.TG002.Contains(keyword) || x.TG003.Contains(keyword) || x.TG022.ToString().Contains(keyword)
                || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && (y.TF003.ToString().Contains(keyword)
                || y.TF016.ToString().Contains(keyword)
                || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TF008 && z.MA002.Contains(keyword))))))

             /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
        && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));*/
             //包含當天的日期
             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));



            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                            (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
            }*/
            //包含當天的日期:篩選單據日期
            if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                            (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
            }

            switch (filterOption)
            {
                case "公務車":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("公務車"));
                    break;
                case "地鐵":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("地鐵"));
                    break;
                case "客運":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("客運"));
                    break;
                case "捷運":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("捷運"));
                    break;
                case "發電機用油":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("發電機用油"));
                    break;
                case "私車公用":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "計程車":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "飛機":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "高鐵":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                    // 其他 case 略
            }
            // 篩選發票日期
            /*pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
                && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) < 0 || string.IsNullOrEmpty(endDate)));*/

            // 篩選發票日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
                && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate)));
            }
            var pcmtgCustomers = pcmtgQuery
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.UDF05,//20240521加入審核按紐
                    x.TG024,
                    x.TG013,
                    x.TG001,
                    x.TG002,
                    x.TG003,
                    //20240517加入金額
                    x.TG022
                })
   /*.Where(y => (startDateValue == null || y.TG013.CompareTo(startDate) >= 0) && // 直接使用字串比較
               (endDateValue == null || y.TG013.CompareTo(endDate) < 0)) // 直接使用字串比較*/

   .ToList()
   .Select(y => new
   {
       y.UDF01,
       y.UDF02,
       y.UDF03,
       y.UDF06,
       y.UDF05,//20240521加入審核按紐
       y.TG024,
       //InvoiceDate = DateTime.ParseExact(y.TG013, "yyyyMMdd", CultureInfo.InvariantCulture),
       //InvoiceDate = y.TG013,
       InvoiceDate = y.TG013.PadRight(8, '0'), // 將 TG013 補足為8位數後再賦值給 InvoiceDate

       y.TG001,
       y.TG002,
       y.TG003,
       y.TG022
   })
   .ToList();


            List<HighSpeedRailViewModel> viewModels = new List<HighSpeedRailViewModel>();

           


            // 建立 HighSpeedRailViewModel 物件,並加入 viewModels 清單
            foreach (var acptbCustomer in acptbCustomers)
            {
                HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
                {
                    UDF01 = acptbCustomer.UDF01,
                    CREATE_DATE = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA003,
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015,
                    //TA015 = acptbCustomer.ACPTAs.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015?.ToString("yyyyMMdd"),

                    //20240520-TF016欄位為空，TF015欄位應為空
                    /*TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                             from acpt in acptbJoin.DefaultIfEmpty()
                             where acpt.UDF01 != null && acpt.UDF01 != ""
                             select ajsta.TA004).FirstOrDefault(),*/

                    TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             where acptb.TB001 == acptbCustomer.TB001 && acptb.TB002 == acptbCustomer.TB002 && acptb.TB003 == acptbCustomer.TB003 && acptb.UDF01 != null && acptb.UDF01 != ""
                             select ajsta.TA004).FirstOrDefault(),

                    //連結有帶出但是都顯示一樣，20240520修改寫法
                    /*TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                             from acpt in acptbJoin.DefaultIfEmpty()
                             where acpt.UDF01 != null && acpt.UDF01 != ""
                             select ajsta.TA005).FirstOrDefault(),*/

                    TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             where acptb.TB001 == acptbCustomer.TB001 && acptb.TB002 == acptbCustomer.TB002 && acptb.TB003 == acptbCustomer.TB003 && acptb.UDF01 != null && acptb.UDF01 != ""
                             select ajsta.TA005).FirstOrDefault(),



                    //20240516網頁:轉不出來
                    /*TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             select ajsta.TA004).FirstOrDefault(),

                    TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             select ajsta.TA005).FirstOrDefault(),*/


                    //20240516程式碼沒報錯，但是顯示找不到
                    //TF015 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA004,
                    //TF016 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA005,
                    TA015 = (from acpta in _TWNCPCdb.ACPTA
                             where acpta.TA001 == acptbCustomer.TB001
                                && acpta.TA002 == acptbCustomer.TB002
                             orderby acpta.TA015 descending
                             select acpta.TA015).FirstOrDefault(),
                    UDF02 = acptbCustomer.UDF02,
                    UDF03 = acptbCustomer.UDF03,
                    UDF06 = acptbCustomer.UDF06,
                    UDF05 = acptbCustomer.UDF05,//20240521加入審核按紐
                    TB011 = acptbCustomer.TB011,
                    TB001 = acptbCustomer.TB001,
                    TB002 = acptbCustomer.TB002,
                    TB003 = acptbCustomer.TB003,
                    //20240517加入金額欄位
                    TB009 = acptbCustomer.TB009,
                    TA004 = (from acpta in _TWNCPCdb.ACPTA
                             where acpta.TA001 == acptbCustomer.TB001 && acpta.TA002 == acptbCustomer.TB002
                             join purma in _TWNCPCdb.PURMA on acpta.TA004 equals purma.MA001
                             select purma.MA002).FirstOrDefault()
                };

                viewModels.Add(viewModel);
            }

            foreach (var pcmtgCustomer in pcmtgCustomers)
            {
                HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
                {
                    UDF01 = pcmtgCustomer.UDF01,
                    CREATE_DATE = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF003,
                    TF015 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF015,
                    TF016 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF016,
                    // TA015 = pcmtgCustomer.TG013,
                    //TA015 = pcmtgCustomer.InvoiceDate.ToString("yyyyMMdd"), // 使用 InvoiceDate 並格式化為所需的日期格式
                    TA015 = pcmtgCustomer.InvoiceDate,
                    //20240513包含當天的日期
                    UDF02 = pcmtgCustomer.UDF02,
                    UDF03 = pcmtgCustomer.UDF03,
                    UDF06 = pcmtgCustomer.UDF06,
                    UDF05 = pcmtgCustomer.UDF05,//20240521加入審核按紐
                    TB011 = pcmtgCustomer.TG024,
                    TB001 = pcmtgCustomer.TG001,
                    TB002 = pcmtgCustomer.TG002,
                    TB003 = pcmtgCustomer.TG003,
                    //20240517加入金額欄位
                    TB009 = pcmtgCustomer.TG022,
                    TA004 = (from pcmtf in _TWNCPCdb.PCMTF
                             where pcmtf.TF001 == pcmtgCustomer.TG001 && pcmtf.TF002 == pcmtgCustomer.TG002
                             join purma in _TWNCPCdb.PURMA on pcmtf.TF008 equals purma.MA001
                             select purma.MA002).FirstOrDefault()
                };

                viewModels.Add(viewModel);
            }
            // 20240521如果密碼為空或者密碼正確，返回相應視圖
            if (string.IsNullOrEmpty(password))
            {
                return View("CombinedHighSpeedRailList", viewModels);
            }
            else if (password == "1597")
            {
                return View("CombinedHighSpeedRailList5", viewModels);
            }
            else
            {
                return View("CombinedHighSpeedRailList", viewModels);
            }
            //return View(viewModels);
        }
        [AllowAnonymous]
        //20240520票據單別單號顯示連結依照課長
        //public ActionResult CombinedHighSpeedRailList5(string filterOption, string keyword, string startDate, string endDate, string startDocDate, string endDocDate)


        public ActionResult CombinedHighSpeedRailList5(string keyword, string startDate, string endDate, string startDocDate, string endDocDate, string filterOption = "公務車")
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            // 定義交通方式關鍵字
            var transportKeywords = new[] { "公務車", "地鐵", "客運", "捷運", "發電機用油", "私車公用", "計程車", "飛機", "高鐵" };



            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<ACPTB> acptbQuery = _TWNCPCdb.ACPTB
    .Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
                && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TB011.Contains(keyword) || x.TB001.Contains(keyword)
                || x.TB002.Contains(keyword) || x.TB003.Contains(keyword) || x.TB009.ToString().Contains(keyword)
                || _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 && (y.TA003.ToString().Contains(keyword) || y.TA015.ToString().Contains(keyword)
               //20240517找檔案名稱的傳票單號搜尋，可以用關鍵字找到
               /*|| _TWNCPCdb.AJSTA.Any(z => z.TA001 == y.TA001 && z.TA002 == y.TA002 && z.TA005.ToString().Contains(keyword))*/
               /*|| _TWNCPCdb.AJSTB.Any(z => z.TB001 == x.TB001 && z.TB002 == x.TB002 && _TWNCPCdb.AJSTA.Any(a => a.TA001 == z.TB001 && a.TA002 == z.TB002 && a.TA005.ToString().Contains(keyword)))*/
               || _TWNCPCdb.AJSTB.Any(z => z.TB001 == x.TB001 && _TWNCPCdb.AJSTA.Any(a => a.TA001 == z.TB001 && a.TA002 == z.TB002 && a.TA005.ToString().Contains(keyword)))


               || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TA004 && z.MA002.Contains(keyword))))))

             /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
             && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))*/
             // 篩選單據日期:包含當天的日期
             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))


            /*.Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));*/
            //包含當天的日期
            .Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));


            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }*/
            // 篩選單據日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }

            // 篩選發票日期
            /*if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }*/

            // 篩選發票日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }

            switch (filterOption)
            {
                case "公務車":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("公務車"));
                    break;
                case "地鐵":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("地鐵"));
                    break;
                case "客運":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("客運"));
                    break;
                case "捷運":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("捷運"));
                    break;
                case "發電機用油":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("發電機用油"));
                    break;
                case "私車公用":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "計程車":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "飛機":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "高鐵":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                    // 其他 case 略
            }



            var acptbCustomers = acptbQuery
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.UDF05,//20240521加入審核按紐
                    x.TB011,
                    x.TB001,
                    x.TB002,
                    x.TB003,
                    x.TB009
                })
                .ToList();




            // 篩選發票日期
            IQueryable<PCMTG> query = _TWNCPCdb.PCMTG;


            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<PCMTG> pcmtgQuery = _TWNCPCdb.PCMTG.Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
                && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TG024.Contains(keyword)
                || x.TG013.Contains(keyword) || x.TG001.Contains(keyword) || x.TG002.Contains(keyword) || x.TG003.Contains(keyword) || x.TG022.ToString().Contains(keyword)
                || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && (y.TF003.ToString().Contains(keyword)
                || y.TF016.ToString().Contains(keyword)
                || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TF008 && z.MA002.Contains(keyword))))))

             /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
        && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));*/
             //包含當天的日期
             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));



            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                            (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
            }*/
            //包含當天的日期:篩選單據日期
            if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                            (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
            }

            switch (filterOption)
            {
                case "公務車":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("公務車"));
                    break;
                case "地鐵":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("地鐵"));
                    break;
                case "客運":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("客運"));
                    break;
                case "捷運":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("捷運"));
                    break;
                case "發電機用油":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("發電機用油"));
                    break;
                case "私車公用":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "計程車":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "飛機":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "高鐵":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                    // 其他 case 略
            }
            // 篩選發票日期
            /*pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
                && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) < 0 || string.IsNullOrEmpty(endDate)));*/

            // 篩選發票日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
                && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate)));
            }
            var pcmtgCustomers = pcmtgQuery
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.UDF05,//20240521加入審核按紐
                    x.TG024,
                    x.TG013,
                    x.TG001,
                    x.TG002,
                    x.TG003,
                    //20240517加入金額
                    x.TG022
                })
   /*.Where(y => (startDateValue == null || y.TG013.CompareTo(startDate) >= 0) && // 直接使用字串比較
               (endDateValue == null || y.TG013.CompareTo(endDate) < 0)) // 直接使用字串比較*/

   .ToList()
   .Select(y => new
   {
       y.UDF01,
       y.UDF02,
       y.UDF03,
       y.UDF06,
       y.UDF05,//20240521加入審核按紐
       y.TG024,
       //InvoiceDate = DateTime.ParseExact(y.TG013, "yyyyMMdd", CultureInfo.InvariantCulture),
       //InvoiceDate = y.TG013,
       InvoiceDate = y.TG013.PadRight(8, '0'), // 將 TG013 補足為8位數後再賦值給 InvoiceDate

       y.TG001,
       y.TG002,
       y.TG003,
       y.TG022
   })
   .ToList();


            List<HighSpeedRailViewModel> viewModels = new List<HighSpeedRailViewModel>();

            // 建立 HighSpeedRailViewModel 物件,並加入 viewModels 清單
            foreach (var acptbCustomer in acptbCustomers)
            {
                HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
                {
                    UDF01 = acptbCustomer.UDF01,
                    CREATE_DATE = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA003,
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015,
                    //TA015 = acptbCustomer.ACPTAs.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015?.ToString("yyyyMMdd"),

                    //20240520-TF016欄位為空，TF015欄位應為空
                    /*TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                             from acpt in acptbJoin.DefaultIfEmpty()
                             where acpt.UDF01 != null && acpt.UDF01 != ""
                             select ajsta.TA004).FirstOrDefault(),*/

                    TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             where acptb.TB001 == acptbCustomer.TB001 && acptb.TB002 == acptbCustomer.TB002 && acptb.TB003 == acptbCustomer.TB003 && acptb.UDF01 != null && acptb.UDF01 != ""
                             select ajsta.TA004).FirstOrDefault(),

                    //連結有帶出但是都顯示一樣，20240520修改寫法
                    /*TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                             from acpt in acptbJoin.DefaultIfEmpty()
                             where acpt.UDF01 != null && acpt.UDF01 != ""
                             select ajsta.TA005).FirstOrDefault(),*/

                    TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             where acptb.TB001 == acptbCustomer.TB001 && acptb.TB002 == acptbCustomer.TB002 && acptb.TB003 == acptbCustomer.TB003 && acptb.UDF01 != null && acptb.UDF01 != ""
                             select ajsta.TA005).FirstOrDefault(),



                    //20240516網頁:轉不出來
                    /*TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             select ajsta.TA004).FirstOrDefault(),

                    TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 }
                             select ajsta.TA005).FirstOrDefault(),*/


                    //20240516程式碼沒報錯，但是顯示找不到
                    //TF015 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA004,
                    //TF016 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA005,
                    TA015 = (from acpta in _TWNCPCdb.ACPTA
                             where acpta.TA001 == acptbCustomer.TB001
                                && acpta.TA002 == acptbCustomer.TB002
                             orderby acpta.TA015 descending
                             select acpta.TA015).FirstOrDefault(),
                    UDF02 = acptbCustomer.UDF02,
                    UDF03 = acptbCustomer.UDF03,
                    UDF06 = acptbCustomer.UDF06,
                    UDF05 = acptbCustomer.UDF05,//20240521加入審核按紐
                    TB011 = acptbCustomer.TB011,
                    TB001 = acptbCustomer.TB001,
                    TB002 = acptbCustomer.TB002,
                    TB003 = acptbCustomer.TB003,
                    //20240517加入金額欄位
                    TB009 = acptbCustomer.TB009,
                    TA004 = (from acpta in _TWNCPCdb.ACPTA
                             where acpta.TA001 == acptbCustomer.TB001 && acpta.TA002 == acptbCustomer.TB002
                             join purma in _TWNCPCdb.PURMA on acpta.TA004 equals purma.MA001
                             select purma.MA002).FirstOrDefault()
                };

                viewModels.Add(viewModel);
            }

            foreach (var pcmtgCustomer in pcmtgCustomers)
            {
                HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
                {
                    UDF01 = pcmtgCustomer.UDF01,
                    CREATE_DATE = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF003,
                    TF015 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF015,
                    TF016 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF016,
                    // TA015 = pcmtgCustomer.TG013,
                    //TA015 = pcmtgCustomer.InvoiceDate.ToString("yyyyMMdd"), // 使用 InvoiceDate 並格式化為所需的日期格式
                    TA015 = pcmtgCustomer.InvoiceDate,
                    //20240513包含當天的日期
                    UDF02 = pcmtgCustomer.UDF02,
                    UDF03 = pcmtgCustomer.UDF03,
                    UDF06 = pcmtgCustomer.UDF06,
                    UDF05 = pcmtgCustomer.UDF05,//20240521加入審核按紐
                    TB011 = pcmtgCustomer.TG024,
                    TB001 = pcmtgCustomer.TG001,
                    TB002 = pcmtgCustomer.TG002,
                    TB003 = pcmtgCustomer.TG003,
                    //20240517加入金額欄位
                    TB009 = pcmtgCustomer.TG022,
                    TA004 = (from pcmtf in _TWNCPCdb.PCMTF
                             where pcmtf.TF001 == pcmtgCustomer.TG001 && pcmtf.TF002 == pcmtgCustomer.TG002
                             join purma in _TWNCPCdb.PURMA on pcmtf.TF008 equals purma.MA001
                             select purma.MA002).FirstOrDefault()
                };

                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }


        [AllowAnonymous]
        //20240516要做出應付憑單_連結
        public ActionResult CombinedHighSpeedRailList4(string filterOption, string keyword, string startDate, string endDate, string startDocDate, string endDocDate)
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<ACPTB> acptbQuery = _TWNCPCdb.ACPTB

.Join(_TWNCPCdb.AJSTB,
     acptb => new { TB013 = acptb.TB001, TB014 = acptb.TB002, TB052 = acptb.TB003 },
     ajstb => new { TB013 = ajstb.TB001, TB014 = ajstb.TB002, TB052 = ajstb.TB052 },
     (acptb, ajstb) => new { ACPTB = acptb, AJSTB = ajstb })
        .Join(_TWNCPCdb.AJSTA,
     acptbAjstb => new { TA001 = acptbAjstb.AJSTB.TB001, TA002 = acptbAjstb.AJSTB.TB002 },
     ajsta => new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 },
     (acptbAjstb, ajsta) => new { ACPTBAJSTB = acptbAjstb, AJSTA = ajsta })

            .Where(x => (x.ACPTBAJSTB.ACPTB.UDF01.Contains("公務車") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("地鐵") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("客運") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("捷運") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("發電機用油") ||
x.ACPTBAJSTB.ACPTB.UDF01.Contains("私車公用") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("計程車") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("飛機") || x.ACPTBAJSTB.ACPTB.UDF01.Contains("高鐵"))
                && (string.IsNullOrEmpty(keyword) || x.ACPTBAJSTB.ACPTB.UDF01.Contains(keyword) || x.ACPTBAJSTB.ACPTB.UDF02.Contains(keyword) || x.ACPTBAJSTB.ACPTB.UDF03.Contains(keyword) || x.ACPTBAJSTB.ACPTB.UDF06.ToString().Contains(keyword) || x.ACPTBAJSTB.ACPTB.TB011.Contains(keyword) 
                || x.ACPTBAJSTB.ACPTB.TB001.Contains(keyword) || x.ACPTBAJSTB.ACPTB.TB002.Contains(keyword) || x.ACPTBAJSTB.ACPTB.TB003.Contains(keyword)
                       || _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.ACPTBAJSTB.ACPTB.TB001 && y.TA002 == x.ACPTBAJSTB.ACPTB.TB002 && (y.TA003.ToString().Contains(keyword) || y.TA015.ToString().Contains(keyword) || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TA004 && z.MA002.Contains(keyword))))))
                
            
    
            
             /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
             && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))*/
             // 篩選單據日期:包含當天的日期
             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))


             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.ACPTA.Any(y => y.TA003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))))
        .Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))))


             .Select(x => x.ACPTBAJSTB.ACPTB);

            /*.Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));*/
            //包含當天的日期
            
            /*.Where(x => (string.IsNullOrEmpty(startDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate)))
            && (string.IsNullOrEmpty(endDate) || _TWNCPCdb.ACPTA.Any(y => y.TA015.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate))));

            

            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }*/
            // 篩選單據日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA003.CompareTo(startDocDate) >= 0 && y.TA003.CompareTo(endDocDate) <= 0)));
            }

            // 篩選發票日期
            /*if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }*/

            // 篩選發票日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                acptbQuery = acptbQuery.Where(x => _TWNCPCdb.ACPTA.Any(y => y.TA001 == x.TB001 && y.TA002 == x.TB002 &&
                                            (y.TA015.CompareTo(startDate) >= 0 && y.TA015.CompareTo(endDate) <= 0)));
            }

            switch (filterOption)
            {
                case "公務車":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("公務車"));
                    break;
                case "地鐵":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("地鐵"));
                    break;
                case "客運":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("客運"));
                    break;
                case "捷運":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("捷運"));
                    break;
                case "發電機用油":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("發電機用油"));
                    break;
                case "私車公用":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "計程車":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "飛機":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "高鐵":
                    acptbQuery = acptbQuery.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                    // 其他 case 略
            }



            var acptbCustomers = acptbQuery
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.TB011,
                    x.TB001,
                    x.TB002,
                    x.TB003
                })
                .ToList()
            //.ToList();20240516測試帶出畫面
            .Select(y => new
             {
                y.UDF01,
                y.UDF02,
                y.UDF03,
                y.UDF06,
                y.TB011,
                y.TB001,
                y.TB002,
                y.TB003
            })
   .ToList();
            // 篩選發票日期
            IQueryable<PCMTG> query = _TWNCPCdb.PCMTG;


            // 取得 CombinedHighSpeedRailList 的資料:加入條件篩選
            IQueryable<PCMTG> pcmtgQuery = _TWNCPCdb.PCMTG.Where(x => (x.UDF01.Contains("公務車") || x.UDF01.Contains("地鐵") || x.UDF01.Contains("客運") || x.UDF01.Contains("捷運") || x.UDF01.Contains("發電機用油") ||
x.UDF01.Contains("私車公用") || x.UDF01.Contains("計程車") || x.UDF01.Contains("飛機") || x.UDF01.Contains("高鐵"))
                && (string.IsNullOrEmpty(keyword) || x.UDF01.Contains(keyword) || x.UDF02.Contains(keyword) || x.UDF03.Contains(keyword) || x.UDF06.ToString().Contains(keyword) || x.TG024.Contains(keyword) || x.TG013.Contains(keyword) || x.TG001.Contains(keyword) || x.TG002.Contains(keyword) || x.TG003.Contains(keyword)
                || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && (y.TF003.ToString().Contains(keyword) || _TWNCPCdb.PURMA.Any(z => z.MA001 == y.TF008 && z.MA002.Contains(keyword))))))

             /*.Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
        && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));*/
             //包含當天的日期
             .Where(x => (string.IsNullOrEmpty(startDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(startDocDate) >= 0 || string.IsNullOrEmpty(startDocDate)))
            && (string.IsNullOrEmpty(endDocDate) || _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 && y.TF003.CompareTo(endDocDate) <= 0 || string.IsNullOrEmpty(endDocDate))));



            // 篩選單據日期
            /*if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                            (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
            }*/
            //包含當天的日期:篩選單據日期
            if (!string.IsNullOrEmpty(startDocDate) && !string.IsNullOrEmpty(endDocDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => _TWNCPCdb.PCMTF.Any(y => y.TF001 == x.TG001 && y.TF002 == x.TG002 &&
                                            (y.TF003.CompareTo(startDocDate) >= 0 && y.TF003.CompareTo(endDocDate) <= 0)));
            }

            switch (filterOption)
            {
                case "公務車":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("公務車"));
                    break;
                case "地鐵":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("地鐵"));
                    break;
                case "客運":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("客運"));
                    break;
                case "捷運":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("捷運"));
                    break;
                case "發電機用油":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("發電機用油"));
                    break;
                case "私車公用":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("私車公用"));
                    break;
                case "計程車":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("計程車"));
                    break;
                case "飛機":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("飛機"));
                    break;
                case "高鐵":
                    pcmtgQuery = pcmtgQuery.Where(x => x.UDF01.Contains("高鐵"));
                    break;
                    // 其他 case 略
            }
            // 篩選發票日期
            /*pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
                && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) < 0 || string.IsNullOrEmpty(endDate)));*/

            // 篩選發票日期:包含當天的日期
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                pcmtgQuery = pcmtgQuery.Where(x => (string.IsNullOrEmpty(startDate) || x.TG013.CompareTo(startDate) >= 0 || string.IsNullOrEmpty(startDate))
                && (string.IsNullOrEmpty(endDate) || x.TG013.CompareTo(endDate) <= 0 || string.IsNullOrEmpty(endDate)));
            }
            var pcmtgCustomers = pcmtgQuery
                .Select(x => new
                {
                    x.UDF01,
                    x.UDF02,
                    x.UDF03,
                    x.UDF06,
                    x.TG024,
                    x.TG013,
                    x.TG001,
                    x.TG002,
                    x.TG003
                })
   /*.Where(y => (startDateValue == null || y.TG013.CompareTo(startDate) >= 0) && // 直接使用字串比較
               (endDateValue == null || y.TG013.CompareTo(endDate) < 0)) // 直接使用字串比較*/

   .ToList()
   .Select(y => new
   {
       y.UDF01,
       y.UDF02,
       y.UDF03,
       y.UDF06,
       y.TG024,
       //InvoiceDate = DateTime.ParseExact(y.TG013, "yyyyMMdd", CultureInfo.InvariantCulture),
       //InvoiceDate = y.TG013,
       InvoiceDate = y.TG013.PadRight(8, '0'), // 將 TG013 補足為8位數後再賦值給 InvoiceDate

       y.TG001,
       y.TG002,
       y.TG003
   })
   .ToList();


            List<HighSpeedRailViewModel> viewModels = new List<HighSpeedRailViewModel>();

            // 建立 HighSpeedRailViewModel 物件,並加入 viewModels 清單
            foreach (var acptbCustomer in acptbCustomers)
            {
                HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
                {
                    UDF01 = acptbCustomer.UDF01,
                    CREATE_DATE = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA003,
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015,
                    //TA015 = acptbCustomer.ACPTAs.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault()?.TA015?.ToString("yyyyMMdd"),
                    //TA015 = _TWNCPCdb.ACPTA.FirstOrDefault(y => y.TA001 == acptbCustomer.TB001 && y.TA002 == acptbCustomer.TB002)?.TA015?.ToString("yyyyMMdd"),
                    //20240513包含當天的日期
                    /*TF015 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                             from acpt in acptbJoin.DefaultIfEmpty()
                             select ajsta.TA004).FirstOrDefault(),

                    TF016 = (from ajsta in _TWNCPCdb.AJSTA
                             join ajstb in _TWNCPCdb.AJSTB on new { TA001 = ajsta.TA001, TA002 = ajsta.TA002 } equals new { TA001 = ajstb.TB001, TA002 = ajstb.TB002 }
                             join acptb in _TWNCPCdb.ACPTB on new { TB001 = ajstb.TB013, TB002 = ajstb.TB014, TB003 = ajstb.TB052 } equals new { TB001 = acptb.TB001, TB002 = acptb.TB002, TB003 = acptb.TB003 } into acptbJoin
                             from acpt in acptbJoin.DefaultIfEmpty()
                             select ajsta.TA005).FirstOrDefault(),*/


                    //20240516程式碼沒報錯，但是顯示找不到
                    TF015 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA004,
                    TF016 = _TWNCPCdb.AJSTA.FirstOrDefault()?.TA005,

                    TA015 = (from acpta in _TWNCPCdb.ACPTA
                             where acpta.TA001 == acptbCustomer.TB001
                                && acpta.TA002 == acptbCustomer.TB002
                             orderby acpta.TA015 descending
                             select acpta.TA015).FirstOrDefault(),
                    UDF02 = acptbCustomer.UDF02,
                    UDF03 = acptbCustomer.UDF03,
                    UDF06 = acptbCustomer.UDF06,
                    TB011 = acptbCustomer.TB011,
                    TB001 = acptbCustomer.TB001,
                    TB002 = acptbCustomer.TB002,
                    TB003 = acptbCustomer.TB003,
                    TA004 = (from acpta in _TWNCPCdb.ACPTA
                             where acpta.TA001 == acptbCustomer.TB001 && acpta.TA002 == acptbCustomer.TB002
                             join purma in _TWNCPCdb.PURMA on acpta.TA004 equals purma.MA001
                             select purma.MA002).FirstOrDefault()
                };

                viewModels.Add(viewModel);
            }

            foreach (var pcmtgCustomer in pcmtgCustomers)
            {
                HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
                {
                    UDF01 = pcmtgCustomer.UDF01,
                    CREATE_DATE = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF003,
                    TF015 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF015,
                    TF016 = _TWNCPCdb.PCMTF.FirstOrDefault(y => y.TF001 == pcmtgCustomer.TG001 && y.TF002 == pcmtgCustomer.TG002)?.TF016,
                    // TA015 = pcmtgCustomer.TG013,
                    //TA015 = pcmtgCustomer.InvoiceDate.ToString("yyyyMMdd"), // 使用 InvoiceDate 並格式化為所需的日期格式
                    TA015 = pcmtgCustomer.InvoiceDate,
                    //20240513包含當天的日期


                    UDF02 = pcmtgCustomer.UDF02,
                    UDF03 = pcmtgCustomer.UDF03,
                    UDF06 = pcmtgCustomer.UDF06,
                    TB011 = pcmtgCustomer.TG024,
                    TB001 = pcmtgCustomer.TG001,
                    TB002 = pcmtgCustomer.TG002,
                    TB003 = pcmtgCustomer.TG003,
                    TA004 = (from pcmtf in _TWNCPCdb.PCMTF
                             where pcmtf.TF001 == pcmtgCustomer.TG001 && pcmtf.TF002 == pcmtgCustomer.TG002
                             join purma in _TWNCPCdb.PURMA on pcmtf.TF008 equals purma.MA001
                             select purma.MA002).FirstOrDefault()
                };

                viewModels.Add(viewModel);
            }

            return View(viewModels);
        }

        //20240520:ESG交通類:顯示要輸入憑証修改密碼才可以顯示畫面CombinedHighSpeedRailList5
        [AllowAnonymous]

        public ActionResult CombinedHighSpeedRailListList(string password)

        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            if (password == "1597")
            {
                // 如果密碼正確，則返回 CombinedHighSpeedRailList5 視圖
                return View("CombinedHighSpeedRailList5");
            }
            else
            {
                // 如果密碼錯誤，則返回錯誤視圖或重新渲染表單

                return View();
            }
        }

        //20240513改用mssql語法套入


        /*public ActionResult CombinedHighSpeedRailList3(string filterOption, string keyword, string startDate, string endDate, string startDocDate, string endDocDate)
            {
                ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

                List<HighSpeedRailViewModel> viewModels = new List<HighSpeedRailViewModel>();*/

        //string connectionString = "YourConnectionString"; // 數據庫連接字符



        //TWNCPCContext _TWNCPCdb = new TWNCPCContext("Server=192.168.3.112;Database=TWNCPC;User Id=LYY03;Password=CPCLYY03;");
        //TWNCPCContext _TWNCPCdb = new TWNCPCContext("name=TWNCPCContext");

        //string connectionString = "Server=192.168.3.112;Database=TWNCPC;User Id=LYY03;Password=CPCLYY03;";
        //TWNCPCContext _TWNCPCdb = new TWNCPCContext(connectionString);

        //webform連線方式
        //set conn = Server.CreateObject("ADODB.Connection")
        // conn.open "PROVIDER=SQLOLEDB;DATA SOURCE=192.168.3.112;User ID=LLY03;Password=CPCLYY03;Initial Catalog='TWNCPC';


        /*using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        string sqlQuery = @"
        SELECT UDF01, UDF02, UDF03, UDF06, TB011, TB001, TB002, TB003, TA015 AS CREATE_DATE, null AS TF015, null AS TF016, TA004
        FROM ACPTB
        INNER JOIN ACPTA ON ACPTB.TB001 = ACPTA.TA001 AND ACPTB.TB002 = ACPTA.TA002
        WHERE (ACPTB.UDF01 LIKE '%公務車%' OR ACPTB.UDF01 LIKE '%地鐵%' OR ACPTB.UDF01 LIKE '%客運%' OR ACPTB.UDF01 LIKE '%捷運%' OR ACPTB.UDF01 LIKE '%發電機用油%'
            OR ACPTB.UDF01 LIKE '%私車公用%' OR ACPTB.UDF01 LIKE '%計程車%' OR ACPTB.UDF01 LIKE '%飛機%' OR ACPTB.UDF01 LIKE '%高鐵%')
            AND (ISNULL(@keyword, '') = '' OR ACPTB.UDF01 LIKE '%' + @keyword + '%' OR ACPTB.UDF02 LIKE '%' + @keyword + '%' OR ACPTB.UDF03 LIKE '%' + @keyword + '%' 
            OR CONVERT(VARCHAR, ACPTB.UDF06) LIKE '%' + @keyword + '%' OR ACPTB.TB011 LIKE '%' + @keyword + '%' OR ACPTB.TB001 LIKE '%' + @keyword + '%' 
            OR ACPTB.TB002 LIKE '%' + @keyword + '%' OR ACPTB.TB003 LIKE '%' + @keyword + '%' OR EXISTS (
                SELECT 1 FROM ACPTA WHERE ACPTA.TA001 = ACPTB.TB001 AND ACPTA.TA002 = ACPTB.TB002 
                AND (CONVERT(VARCHAR, ACPTA.TA003) LIKE '%' + @keyword + '%' OR CONVERT(VARCHAR, ACPTA.TA015) LIKE '%' + @keyword + '%' 
                OR EXISTS (SELECT 1 FROM PURMA WHERE PURMA.MA001 = ACPTA.TA004 AND PURMA.MA002 LIKE '%' + @keyword + '%')
            ))
            AND (ISNULL(@startDocDate, '') = '' OR EXISTS (
                SELECT 1 FROM ACPTA WHERE ACPTA.TA001 = ACPTB.TB001 AND ACPTA.TA002 = ACPTB.TB002 AND CONVERT(VARCHAR, ACPTA.TA003) >= @startDocDate
            ))
            AND (ISNULL(@endDocDate, '') = '' OR EXISTS (
                SELECT 1 FROM ACPTA WHERE ACPTA.TA001 = ACPTB.TB001 AND ACPTA.TA002 = ACPTB.TB002 AND CONVERT(VARCHAR, ACPTA.TA003) <= @endDocDate
            ))
            AND (ISNULL(@startDate, '') = '' OR EXISTS (
                SELECT 1 FROM ACPTA WHERE ACPTA.TA001 = ACPTB.TB001 AND ACPTA.TA002 = ACPTB.TB002 AND CONVERT(VARCHAR, ACPTA.TA015) >= @startDate
            ))
            AND (ISNULL(@endDate, '') = '' OR EXISTS (
                SELECT 1 FROM ACPTA WHERE ACPTA.TA001 = ACPTB.TB001 AND ACPTA.TA002 = ACPTB.TB002 AND CONVERT(VARCHAR, ACPTA.TA015) <= @endDate
            ))
        UNION
        SELECT UDF01, UDF02, UDF03, UDF06, TG024 AS TB011, TG001 AS TB001, TG002 AS TB002, TG003 AS TB003, null AS CREATE_DATE, TG013 AS TF015, null AS TF016, TA004
        FROM PCMTG
        WHERE (UDF01 LIKE '%公務車%' OR UDF01 LIKE '%地鐵%' OR UDF01 LIKE '%客運%' OR UDF01 LIKE '%捷運%' OR UDF01 LIKE '%發電機用油%'
            OR UDF01 LIKE '%私車公用%' OR UDF01 LIKE '%計程車%' OR UDF01 LIKE '%飛機%' OR UDF01 LIKE '%高鐵%')
            AND (ISNULL(@keyword, '') = '' OR UDF01 LIKE '%' + @keyword + '%' OR UDF02 LIKE '%' + @keyword + '%' OR UDF03 LIKE '%' + @keyword + '%' 
            OR CONVERT(VARCHAR, UDF06) LIKE '%' + @keyword + '%' OR TG024 LIKE '%' + @keyword + '%' OR TG013 LIKE '%' + @keyword + '%' 
            OR TG001 LIKE '%' + @keyword + '%' OR TG002 LIKE '%' + @keyword + '%' OR TG003 LIKE '%' + @keyword + '%' OR EXISTS (
                SELECT 1 FROM PCMTF WHERE PCMTF.TF001 = PCMTG.TG001 AND PCMTF.TF002 = PCMTG.TG002 
                AND (CONVERT(VARCHAR, PCMTF.TF003) LIKE '%' + @keyword + '%' OR EXISTS (SELECT 1 FROM PURMA WHERE PURMA.MA001 = PCMTF.TF008 AND PURMA.MA002 LIKE '%' + @keyword + '%'))
            ))
            AND (ISNULL(@startDocDate, '') = '' OR EXISTS (
                SELECT 1 FROM PCMTF WHERE PCMTF.TF001 = PCMTG.TG001 AND PCMTF.TF002 = PCMTG.TG002 AND CONVERT(VARCHAR, PCMTF.TF003) >= @startDocDate
            ))
            AND (ISNULL(@endDocDate, '') = '' OR EXISTS (
                SELECT 1 FROM PCMTF WHERE PCMTF.TF001 = PCMTG.TG001 AND PCMTF.TF002 = PCMTG.TG002 AND CONVERT(VARCHAR, PCMTF.TF003) <= @endDocDate
            ))
            AND (ISNULL(@startDate, '') = '' OR TG013 >= @startDate)
            AND (ISNULL(@endDate, '') = '' OR TG013 < @endDate)";

        SqlCommand command = new SqlCommand(sqlQuery, connection);
        command.Parameters.AddWithValue("@keyword", keyword);
        command.Parameters.AddWithValue("@startDocDate", startDocDate);
        command.Parameters.AddWithValue("@endDocDate", endDocDate);
        command.Parameters.AddWithValue("@startDate", startDate);
        command.Parameters.AddWithValue("@endDate", endDate);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            HighSpeedRailViewModel viewModel = new HighSpeedRailViewModel
            {
                UDF01 = reader["UDF01"].ToString(),
                CREATE_DATE = reader["CREATE_DATE"] != DBNull.Value ? reader["CREATE_DATE"].ToString() : null,
                TF015 = reader["TF015"] != DBNull.Value ? reader["TF015"].ToString() : null,
                TF016 = reader["TF016"] != DBNull.Value ? reader["TF016"].ToString() : null,
                TA015 = reader["TA015"] != DBNull.Value ? reader["TA015"].ToString() : null,
                UDF02 = reader["UDF02"].ToString(),
                UDF03 = reader["UDF03"].ToString(),
                UDF06 = reader["UDF06"] != DBNull.Value ? (decimal?)reader.GetDecimal(reader.GetOrdinal("UDF06")) : null,
                //UDF06 = reader["UDF06"].ToString(),
                TB011 = reader["TB011"] != DBNull.Value ? reader["TB011"].ToString() : null,
                TB001 = reader["TB001"].ToString(),
                TB002 = reader["TB002"].ToString(),
                TB003 = reader["TB003"].ToString(),
                TA004 = reader["TA004"].ToString()
            };

            viewModels.Add(viewModel);
        }

        reader.Close();
    }

    return View(viewModels);
}*/




        //20250410高鐵費用:新增
        [AllowAnonymous]
        
        public ActionResult HIGH_SPEED_RAIL_Create()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        //20250410高鐵費用:新增

        public ActionResult HIGH_SPEED_RAIL_Create(HIGH_SPEED_RAIL customer, HttpPostedFileBase pdfFile)
        {
            if (ModelState.IsValid)
            {
                string custId = customer.DOCUMENT_ID;
                var temp = _db.HIGH_SPEED_RAIL
                    .Where(m => m.DOCUMENT_ID == custId)
                    .FirstOrDefault();

                if (temp == null)
                {


                    // HomeController_20231226紀錄登打時間
                    customer.data = DateTime.Now;

                    // 從 Session 讀取當前登錄用戶
                    var username = Session["Member"] as tMember;

                    // 將當前登錄用戶賦值給 CreatedBy 屬性
                    customer.CreatedBy = username.fUserId;


                    // 上傳PDF 檔案
                    if (pdfFile != null && pdfFile.ContentLength > 0)
                    {
                        // 讀取上傳的 PDF 檔案內容
                        var pdfContent = new byte[pdfFile.ContentLength];
                        pdfFile.InputStream.Read(pdfContent, 0, pdfFile.ContentLength);

                        // 將 PDF 檔案內容保存到模型中
                        customer.PDF_CONTENT = pdfContent;
                    }


                    _db.HIGH_SPEED_RAIL.Add(customer);



                    _db.SaveChanges();  // 保存更改到資料庫
                    return RedirectToAction("HIGH_SPEED_RAIL_List");
                }

                ViewBag.Msg = "單據號碼重複";
            }

            // 如果模型驗證失敗，返回 View 以顯示錯誤消息
            return View(customer);
        }

        //20250410高鐵費用:修改
        [AllowAnonymous]

        public ActionResult HIGH_SPEED_RAIL_Edit(string id)
        {
            var highSpeed = _db.HIGH_SPEED_RAIL.FirstOrDefault(x => x.DOCUMENT_ID == id);
            HIGH_SPEED_RAIL photo = _db.HIGH_SPEED_RAIL.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }
            return View(highSpeed);
        }
        //20250410高鐵費用:修改
        [AllowAnonymous]
        [HttpPost]
        
        // 上傳PDF 檔案
        public ActionResult HIGH_SPEED_RAIL_Edit(HIGH_SPEED_RAIL water, HttpPostedFileBase pdfFile)
        {

            var model = _db.HIGH_SPEED_RAIL.FirstOrDefault(x => x.DOCUMENT_ID == water.DOCUMENT_ID);
            //20250410高鐵費用顯示 PDF 檔案

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {

                model.PDF_CONTENT = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(model.PDF_CONTENT, 0, pdfFile.ContentLength);
            }
            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                model.BASE_NAME = water.BASE_NAME;
                model.DOCUMENT_DATA = water.DOCUMENT_DATA;
                model.ORIGIN = water.ORIGIN;
                model.DESTINATION = water.DESTINATION;
                model.ROUND_ONE = water.ROUND_ONE;
                model.CARBON_PERIOD = water.CARBON_PERIOD;
                model.INFORMATION = water.INFORMATION;
                model.REMARK = water.REMARK;                
                model.VOUCHER_NUMBER = water.VOUCHER_NUMBER;
            }


            _db.SaveChanges();

            return RedirectToAction("HIGH_SPEED_RAIL_List");
        }
        //20250412冷氣要顯示 PDF 檔案

        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile5(string id)
        {
            var air_conditioner = _db.AIR_CONDITIONER.FirstOrDefault(x => x.MACHINE_NUMBER == id);
            if (air_conditioner != null && air_conditioner.PDF_CONTENT != null && air_conditioner.PDF_CONTENT.Length > 0)
            {
                return File(air_conditioner.PDF_CONTENT, "application/pdf");
            }
            return null;
        }


        [AllowAnonymous]
        //20250410冷氣:列表

        public ActionResult AIR_CONDITIONER_List()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.AIR_CONDITIONER.ToList();


            var ESGs = _db.AIR_CONDITIONER.ToList();

            // 20240412將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);

        }
        //20250415冷氣:修改
        [AllowAnonymous]

        public ActionResult AIR_CONDITIONER_Edit(string id)
        {
            var airconditioner = _db.AIR_CONDITIONER.FirstOrDefault(x => x.MACHINE_NUMBER == id);
            AIR_CONDITIONER photo = _db.AIR_CONDITIONER.Find(id);
            if (photo == null)
            {
                return HttpNotFound();  // 找不到
            }
            return View(airconditioner);
        }
        //20250415冷氣:修改
        [AllowAnonymous]
        [HttpPost]

        // 上傳PDF 檔案
        public ActionResult AIR_CONDITIONER_Edit(AIR_CONDITIONER airconditioner, HttpPostedFileBase pdfFile)
        {

            var model = _db.AIR_CONDITIONER.FirstOrDefault(x => x.MACHINE_NUMBER == airconditioner.MACHINE_NUMBER);
            //20250410高鐵費用顯示 PDF 檔案

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {

                model.PDF_CONTENT = new byte[pdfFile.ContentLength];
                pdfFile.InputStream.Read(model.PDF_CONTENT, 0, pdfFile.ContentLength);
            }
            if (model == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                model.BASE_NAME = airconditioner.BASE_NAME;
                model.AIR_CONDITIONER_TYPE = airconditioner.AIR_CONDITIONER_TYPE;
                model.QUANTITY = airconditioner.QUANTITY;
                model.TOTAL_HOURS = airconditioner.TOTAL_HOURS;
                model.REFRIGERANT = airconditioner.REFRIGERANT;
                model.REMARK = airconditioner.REMARK;
               
            }


            _db.SaveChanges();

            return RedirectToAction("AIR_CONDITIONER_List");
        }
        [AllowAnonymous]

        //20240418測試跟ERP的CMSMF顯示ok
        public ActionResult CMSMFList()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var ESGs = _TWNCPCdb.CMSMF.ToList();


            return View(ESGs);
        }
        //20240419增加:員工國外差旅List-PDF
        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile6(string id)
        {
            var employee_travel_abroad = _db.EMPLOYEE_TRAVEL_ABROAD.FirstOrDefault(x => x.DOCUMENT_ID == id);
            if (employee_travel_abroad != null && employee_travel_abroad.PDF_CONTENT != null && employee_travel_abroad.PDF_CONTENT.Length > 0)
            {
                return File(employee_travel_abroad.PDF_CONTENT, "application/pdf");
            }
            return null;
        }
        //20240419增加:員工國外差旅List
        [AllowAnonymous]       

        public ActionResult TRAVEL_ABROAD_List()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.EMPLOYEE_TRAVEL_ABROAD.ToList();


            var ESGs = _db.EMPLOYEE_TRAVEL_ABROAD.ToList();

            // 20240412將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);

        }

        //20240419增加:車輛用油List-PDF
        [AllowAnonymous]
        [HttpGet]
        public FileContentResult GetPdfFile7(string id)
        {
            var employee_travel_abroad = _db.VEHICLE_OIL.FirstOrDefault(x => x.VOUCHER_NUMBER == id);
            if (employee_travel_abroad != null && employee_travel_abroad.PDF_CONTENT != null && employee_travel_abroad.PDF_CONTENT.Length > 0)
            {
                return File(employee_travel_abroad.PDF_CONTENT, "application/pdf");
            }
            return null;
        }
        //20240419增加:車輛用油List
        [AllowAnonymous]

        public ActionResult VEHICLE_OIL_List()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.VEHICLE_OIL.ToList();
            DateTime startDate;
            DateTime endDate;

            if (!String.IsNullOrEmpty(Request.QueryString["startDate"]))
            {
                startDate = DateTime.Parse(Request.QueryString["startDate"]);
            }
            else
            {
                startDate = DateTime.MinValue;
            }

            if (!String.IsNullOrEmpty(Request.QueryString["endDate"]))
            {
                endDate = DateTime.Parse(Request.QueryString["endDate"]);
            }
            else
            {
                endDate = DateTime.MaxValue;
            }



            var ESGs = _db.VEHICLE_OIL.OrderByDescending(x => x.DOCUMENT_DATA)
        .Where(x => x.DOCUMENT_DATA >= startDate &&
        x.DOCUMENT_DATA <= endDate)
.ToList();

            // 20240419將 PDF 檔案內容傳遞給視圖
            foreach (var item in customers)
            {
                item.ImageMimeType = "application/pdf";
            }

            return View(ESGs);

        }
        //[AllowAnonymous]
        // 20240422增加:滅火器欄位     

        /*public ActionResult FireExtin_List()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.FireExtin.ToList();

            var ESGs = _db.FireExtin.ToList();            

            return View(ESGs);

        }*/

        //20240423增加:滅火器欄位用關鍵字搜尋
        [AllowAnonymous]
        public ActionResult FireExtin_List(string searchString, string plantArea)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";

            var fireExtins = _db.FireExtin.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                fireExtins = fireExtins.Where(fe => fe.FE001.Contains(searchString) ||
                                                    fe.FE002.ToString().Contains(searchString) ||
                                                    fe.FE003.Contains(searchString) ||
                                                    fe.FE004.Contains(searchString) ||
                                                    fe.FE005.Contains(searchString) ||
                                                    fe.FE006.ToString().Contains(searchString) ||
                                                    fe.FE007.Contains(searchString) ||
                                                    fe.FE008.Contains(searchString) ||
                                                    fe.FE009.Contains(searchString) ||
                                                    fe.FE010.Contains(searchString)

                                                    // 添加其他需要搜索的欄位
                                                    );
            }
            if (!string.IsNullOrEmpty(plantArea) && plantArea != "全部")
            {
                fireExtins = fireExtins.Where(fe => fe.FE010 == plantArea);
            }

            return View(fireExtins.ToList());
        }



        //[AllowAnonymous]
        // 20240423增加:冷媒欄位     

        /*public ActionResult ColdCoal_List()
        {
            ViewBag.Layout = "~/Views/Shared/_LayoutMember.cshtml";
            var customers = _db.ColdCoal.ToList();

            var ESGs = _db.ColdCoal.ToList();

            return View(ESGs);

        }*/
        //20240423增加:冷媒欄位用關鍵字搜尋
        //20240429增加:冷媒欄位用廠區搜尋
        [AllowAnonymous]
        public ActionResult ColdCoal_List(string searchString, string plantArea,string category)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }

            var coldCoals = _db.ColdCoal.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                coldCoals = coldCoals.Where(fe => fe.CC001.Contains(searchString) ||
                                                    fe.CC002.Contains(searchString) ||
                                                    fe.CC003.Contains(searchString) ||
                                                    fe.CC004.Contains(searchString) ||
                                                    fe.CC005.Contains(searchString) ||
                                                    fe.CC006.Contains(searchString) ||
                                                    fe.CC007.Contains(searchString) ||
                                                    fe.CC008.ToString().Contains(searchString) ||
                                                    fe.CC009.Contains(searchString) ||
                                                    fe.CC010.Contains(searchString)
                                                    // 添加其他需要搜索的欄位
                                                    );
            }
            if (plantArea == "非公司")
            {
                coldCoals = coldCoals.Where(c => c.CC010 == "私人");
            }
            else if (plantArea == "南科")
            {
                coldCoals = coldCoals.Where(c => c.CC010 == "南科");
            }
            else if (plantArea == "樹谷")
            {
                coldCoals = coldCoals.Where(c => c.CC010 == "樹谷");
            }

            if (category != null && category != "全部")
            { 
                coldCoals = coldCoals.Where(c => c.CC007 == category); 
            }

            return View(coldCoals.ToList());
        }

        #region WD40
        [AllowAnonymous]
        [HttpGet]
        public ActionResult WD40List(SGS_Search search)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            var query = _db.WD40A.Where(x => x.Status == 0);

            if (search.year != null )
            {
                query = query.Where(x =>x.WD003.Contains(search.year));
            }

            var model = query.OrderByDescending(x => x.WD004).ToList();

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult WD40Edit(WD40A model)
        {
            if (Session["Member"] == null)
            {
                return RedirectToAction("login", "Home");
            }
            ViewBag.IsUpdate = false; 
            model = _db.WD40A.FirstOrDefault(x => x.WD000 == model.WD000); 
            if (model != null)
            {
                ViewBag.IsUpdate = true; 
            }
            return View(model); 
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult WD40Edit(WD40A model, bool IsUpdate = false)
        {
            if (IsUpdate)
            {
                var old = _db.WD40A.Find(model.WD000);
                _db.Entry(old).CurrentValues.SetValues(model);
            }
            else
            {
                model.WD000 = Guid.NewGuid().ToString("N");  
                model.IP = Request.UserHostAddress;
                model.Status = 0;
                _db.WD40A.Add(model);
            }

            try
            {
                _db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("WD40List", "Home");  
        }

        [AllowAnonymous]
        public JsonResult WD40Delete(WD40A model, string password)
        {
            var user = _db.tMember.FirstOrDefault(p => p.fPwd == password);
            if (user == null)
            {
                return Json(new { success = false, message = "密碼錯誤" });
            }
            else
            {
                model = _db.WD40A.Find(model.WD000);
                model.Status = 1;
                try
                {
                    _db.SaveChanges();
                    return Json(new { success = true, message = "刪除成功" });
                }
                catch
                {
                    return Json(new { success = false, message = "刪除失敗" });
                }
            }

        }
        #endregion
    }
}