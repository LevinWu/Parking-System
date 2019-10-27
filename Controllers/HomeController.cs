
using FIT5032.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FIT5032.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Map()
        {
            ViewBag.Message = "Your Map page.";

            return View();
        }


        public ActionResult SendEmail()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SendEmail(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;

                    var attachment = Request.Files["attachment"];
                    if (attachment.ContentLength > 0)
                    {
                        String path = Path.Combine(Server.MapPath("~/Content/Attachment/"), attachment.FileName);
                        attachment.SaveAs(path);
                        EmailSender es = new EmailSender();
                        es.SendA(toEmail, subject, contents, path, attachment.FileName);

                    }
                    else
                    {
                        EmailSender es = new EmailSender();
                        es.Send(toEmail, subject, contents);

                    }
                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult BulkEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BulkEmail(FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationDbContext db = new ApplicationDbContext();
                    //get All Emails
                    var users = db.Users.ToList();
                    var Emaillist = new List<EmailAddress>();
                    foreach (var user in users)
                    {
                        Emaillist.Add(new EmailAddress(user.Email, user.UserName));
                    }

                    var subject = "Notification";
                    var contents = "This is the notification from Easy parking";
                    var attachment = Request.Files["attachment"];
                    if (attachment.ContentLength > 0)
                    {
                        String path = Path.Combine(Server.MapPath("~/Content/Attachment/"), attachment.FileName);
                        attachment.SaveAs(path);
                        BulkEmail bk = new BulkEmail();
                        bk.SendBA(Emaillist, subject, contents,path, attachment.FileName);
                    }
                    else
                    {
                        BulkEmail bk = new BulkEmail();
                        bk.SendB(Emaillist, subject, contents);
                    }
                    ViewBag.Result = "Email has been send.";
                    ModelState.Clear();
                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }



    }
}