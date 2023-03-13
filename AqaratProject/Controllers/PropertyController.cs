using AqaratProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Mail;
using System.Net;

namespace AqaratProject.Controllers
{
    public class PropertyController : Controller
    {
        public AqarEntities3 db = new AqarEntities3();

        // GET: Property
        public ActionResult ShowAll()
        {
            var AllProperties = db.Properties.ToList();
            return View(AllProperties);
        }
        public ActionResult Details(int id)
        {
            ViewBag.Prop_id = id;
            return View(db.Properties.SingleOrDefault(pro => pro.id == id));
        }
        public ActionResult Search(string location)
        {
            ViewBag.Location = location;
            List<Property> props =
                db.Properties.Where(pro => pro.Location.Contains(location)).ToList();
            return View("ShowAll",props);
        }
        public ActionResult AddProperty()
        {
            return View();
        }
     
        public ActionResult saveAddProp(Property prop,HttpPostedFileBase Image)
        {

            string path = Path.Combine(Server.MapPath("~/img"), Image.FileName);
            Image.SaveAs(path);
            prop.property_image = Image.FileName;

            var UserID = (int)Session["ID"];
            prop.user_id = UserID;
            var login = db.users.Where(us => us.id == UserID).FirstOrDefault();
            string subject = $"Added Successfully";
            string body = $"U have successfully Added your property";
            MailMessage mc = new MailMessage("osmanali.software@gmail.com", login.email);
            mc.Subject = subject;
            mc.Body = body;
            mc.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Timeout = 1000000;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            NetworkCredential nc = new NetworkCredential("osmanali.software@gmail.com", "xkcypnydeeclfefu");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mc);
            db.Properties.Add(prop);
            db.SaveChanges();
            return RedirectToAction("ShowAll");
        }
        
    }
}