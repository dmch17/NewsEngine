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
        public ActionResult AddMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMessage(Message message)
        {
            testDB.Messages.Add(message);
            testDB.SaveChanges();
            return RedirectToAction("ShowMessages");
        }

        [HttpGet]
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
        public ActionResult EditMessage(Message message)
        {
            if (message == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int? id = (testDB.Messages.Find(message.Id)).Id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            Message newMessage = testDB.Messages.Find(message.Id);
            newMessage.Text = message.Text;
            newMessage.MessageTitle = message.MessageTitle;
            testDB.Entry(newMessage).State = EntityState.Modified;
            //testDB.Entry(message).State = EntityState.Modified;
            testDB.SaveChanges();
            return RedirectToAction("ShowMessages");
        }

        public ActionResult ShowMessages()
        {
            ViewBag.Messages = testDB.Messages.OrderBy(messages=>messages.CurrentDate).ToList<Message>();
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