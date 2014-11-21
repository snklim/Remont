using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Remont.Common.Model;
using Remont.DAL;

namespace Remont.WebUI.Controllers
{
    public class HomeController : Controller
    {
	    readonly EntityRepository<Table> _tableRepository = new EntityRepository<Table>();

        // GET: Home
        public ActionResult Index()
        {
	        ViewBag.Tables = _tableRepository.GetAll();

            return View();
        }
    }
}