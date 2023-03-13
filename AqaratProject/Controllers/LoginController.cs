using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using AqaratProject.Models;
namespace AqaratProject.Controllers
{
    public class LoginController : Controller
    {
        int func(int x = 1, int y = 0)
        {
            return x + y;
        }


        private AqarEntities3 db = new AqarEntities3();

        // GET: Login
        public ActionResult LoginPage()
        {
            int a = func(y : 4);
            return View();
        }
        public ActionResult test()
        {
            return View();
        }

        public ActionResult signup()
        {
            return View();
        }

        public ActionResult saveSignup(user model)
        {
            
            db.users.Add(model);
            db.SaveChanges();
            return RedirectToAction("LoginPage");
        }

        public ActionResult login()
        {
            return View();
        }
        
        public ActionResult saveLogin(Login login)
        {
            string subject = $"New login";
            string body = $"U have successfully logged in to our Aqarat management system";
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

            var obj = db.users.Where(us => us.email == login.email &&
            us.password == login.password).SingleOrDefault();
            if (login.email.Contains("Admin@gmail.com") && login.password.Contains("Admin12345"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else 
                Session["ID"] = obj.id;
            //ViewBag.message = obj.id;
            
            if (obj!=null)
            {
                db.Logins.Add(login);
                db.SaveChanges();
                return RedirectToAction("UserPage","User");
            }
            
            else
            {
                return RedirectToAction("test");
            }
        }
        public ActionResult Logout()
        {
            return RedirectToAction("HomePage","Home");
        }
    }
}