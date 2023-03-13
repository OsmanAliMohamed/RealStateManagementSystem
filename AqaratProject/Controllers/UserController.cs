using AqaratProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace AqaratProject.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private AqarEntities3 db = new AqarEntities3();
        public ActionResult UserPage()
        {
            var currentID = (int)Session["ID"];
            var user = db.users.Where(us => us.id == currentID).SingleOrDefault();
            if(user.email.Contains("osmanali.software@gmail.com") && user.password.Contains("Osman_Ali@123"))
            {
                user.img = "team_4.jpg";
            }
            if(user.email.Contains("alikotb38@gmail.com") && user.password.Contains("Ali_Kotb@123"))
            {
                user.img = "team_5.jpg";
            }
            return View(db.users.Where(us => us.id == currentID).SingleOrDefault());
        }
        public ActionResult MyProfile()
        {
            var userId = (int)Session["ID"];
            return View(db.users.Where(us=>us.id == userId).SingleOrDefault());
        }
        public ActionResult MyProperties()
        {
            var userId = (int)Session["ID"];
            var AllProperties = db.Properties.Where(prop => prop.user_id == userId).ToList();
            return View(AllProperties);
        }
        public ActionResult DeleteProperty(int id)
        {
            var prop = db.Properties.Find(id);
            db.Properties.Remove(prop);
            db.SaveChanges();
            return RedirectToAction("MyProperties");
        }
        public ActionResult Edit(user newUser, int id)
        {
            user use = db.users.FirstOrDefault(u => u.id == id);
            use.name = newUser.name;
            use.email = newUser.email;
            use.number = newUser.number;
            use.address = newUser.address;
            use.password = newUser.password;
            db.SaveChanges();
            return RedirectToAction("MyProfile");
        }
        public ActionResult Buy(int Prop_id)
        {
            var userId = (int)Session["ID"];
            var CurrnetUser = db.users.Where(us => us.id == userId).SingleOrDefault();
            var property = db.Properties.Where(prop => prop.id == Prop_id).SingleOrDefault();
            var Owner = db.users.Where(us => us.id == property.user_id).SingleOrDefault();
            string OwnerName = Owner.name;
            string PropertyUserEmail = Owner.email;
            string subject = $"New buyer";
            string body = $"Dear {OwnerName}, \n \t You have new buyer for your department \n this is the data about the buyer \n name : {CurrnetUser.name} \n Phone Number : {CurrnetUser.number}    ";
            MailMessage mc = new MailMessage("osmanali.software@gmail.com", PropertyUserEmail);
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

            return RedirectToAction("UserPage");

        }
        public ActionResult ContactUs()
        {
            var UserID = (int)Session["ID"];
            var login = db.users.Where(us => us.id == UserID).FirstOrDefault();
            string subject = $"Warnning ";
            string body = $"I Have A problem In The System, Help Me{login.email}";
            MailMessage mc = new MailMessage(login.email,"osmanali.software@gmail.com");
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
            return RedirectToAction("UserPage");
        }
    }
    }