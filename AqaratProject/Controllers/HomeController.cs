using AqaratProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AqaratProject.Controllers
{
    public class HomeController : Controller
    {
        private AqarEntities3 db = new AqarEntities3();
        // GET: Home
        public ActionResult HomePage()
        {
            return View();
        }
    }
}