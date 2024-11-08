using prjWastes6.Models.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjWastes6.Controllers
{
    public class CommonController : Controller
    {
        private readonly CommonDAO _CommonDAO;

        public CommonController() 
        {
            _CommonDAO = new CommonDAO();
        }
        // GET: Common
        [HttpPost]
        public JsonResult CheckPwd(string id, string code, string password)
        {
            try
            {
                var codes = code.Split(',').ToList();

                bool correct = _CommonDAO.CheckPassword( codes, password);

                if (correct)
                {
                    return Json(new { success = true, message = "密碼正確"});
                }
                else
                {
                    return Json(new { success = false, message = "密碼錯誤" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "發生錯誤：" + ex.Message });
            }
        }
    }
}