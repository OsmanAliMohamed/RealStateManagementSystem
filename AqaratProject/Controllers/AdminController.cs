using AqaratProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AqaratProject.Controllers
{
    public class AdminController : Controller
    {
        private AqarEntities3 db = new AqarEntities3();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Properties.ToList());
        }
        public ActionResult DeleteProperty(int id)
        {
            var prop = db.Properties.Find(id);
            db.Properties.Remove(prop);
            db.SaveChanges();
            return RedirectToAction("Index");   
        }
        
       
    }
}