using System;
using System.Collections.Generic;
using System.Linq;
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
            return View(new Message());
        }

        [HttpPost]
        public ActionResult AddMessage(Message message)
        {
            if (message.MessageTitle == null || message.Text == null)
                throw new ArgumentNullException();
            else
            {
                testDB.Messages.Add(message);
                testDB.SaveChanges();
                return RedirectToAction("ShowMessages");
            }
        }

        public ActionResult ShowMessages()
        {
            ViewBag.Messages = testDB.Messages.OrderBy(messages=>messages.CurrentDate).ToList<Message>();
            return View();
        }
    }
}