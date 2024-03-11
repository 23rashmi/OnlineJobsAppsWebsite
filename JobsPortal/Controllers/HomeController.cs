using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace JobsPortal.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Home
        public ActionResult Index()
        {
            log.Info("Accessed HomeController's Index action.");
            return View();
        }
        public ActionResult About()
        {
            log.Info("Accessed HomeController's About action.");
            return View();
        }
        public ActionResult Contact()
        {
            log.Info("Accessed HomeController's Contact action.");
            return View();
        }
    }
}