using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsEngine.Models;

namespace NewsEngine.Controllers
{
    public class TestDBController : Controller
    {
        private ApplicationDbContext testDB= new ApplicationDbContext();
        public ActionResult InitializeDB()
        {
            testDB.Messages.Add(new Message());
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult AddMessage()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddMessage(Message message)
        {
            testDB.Messages.Add(message);
            testDB.SaveChanges();
            return RedirectToAction("ShowMessages");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = testDB.Messages.Find(id);
            if (message == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            else
            {
                testDB.Messages.Remove(message);
                testDB.SaveChanges();
            }
            return RedirectToAction("ShowMessages");
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult EditMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = testDB.Messages.Find(id);
            if (message == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(message);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult EditMessage(Message message)
        {
            if (message == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testDB.Entry(message).State = EntityState.Modified;
            testDB.SaveChanges();
            return RedirectToAction("ShowMessages");
        }

        public ActionResult ShowMessages(Tag tag)
        {
            if (tag.Id == 0)
            {
                ViewBag.Messages = testDB.Messages.OrderBy(messages => messages.CurrentDate).ToList<Message>();
            }
            else
            {
                List<Message> messages = new List<Message>();
                foreach(Message message in testDB.Messages.ToList<Message>())
                {
                    foreach(Tag currentTag in message.Tags)
                    {
                        if (currentTag.Id == tag.Id)
                        {
                            messages.Add(message);
                        }
                    }
                }
                ViewBag.Messages = messages;
            }
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                testDB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}