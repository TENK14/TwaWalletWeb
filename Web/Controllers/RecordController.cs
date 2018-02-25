using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwaWallet.Model;

namespace TwaWallet.Web.Controllers
{
    public class RecordController : Controller
    {
        // GET: Record
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Random()
        {
            var record = new Record() { Description = "Fiktivní záznam 01" };

            return View(record);
        }
    }
}