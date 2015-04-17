using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewsEngine.Models;
using Microsoft.AspNet.Identity;
using PagedList.Mvc;
using PagedList;

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
            message.Author = GetUserById(this.User.Identity.GetUserId());
            testDB.Messages.Add(message);

            var image = this.Request.Files["image"];
            if (image != null && !String.IsNullOrEmpty(image.FileName))
            {
                if(image.FileName.EndsWith(".jpg") || image.FileName.EndsWith(".png") || image.FileName.EndsWith(".gif"))
                {
                    File file = new File() { Ext = NewsEngine.Models.File.GetExt(image.FileName) };
                    testDB.Images.Add(file);
                    testDB.SaveChanges();
                    var filePath = Server.MapPath(Url.Content("~/Images/" + file.DiskName));
                    image.SaveAs(filePath);
                    message.Image = file;
                }
                else
                {
                    ModelState.AddModelError("", "Файл с неправильным расширением");
                    return View(message);
                }
            }

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

            var image = this.Request.Files["image"];
            if (image != null && !String.IsNullOrEmpty(image.FileName))
            {
                if (image.FileName.EndsWith(".jpg") || image.FileName.EndsWith(".png") || image.FileName.EndsWith(".gif"))
                {
                    File file = new File() { Ext = NewsEngine.Models.File.GetExt(image.FileName) };
                    testDB.Images.Add(file);
                    testDB.SaveChanges();
                    var filePath = Server.MapPath(Url.Content("~/Images/" + file.DiskName));
                    image.SaveAs(filePath);
                    message.Image = file;
                }
                else
                {
                    ModelState.AddModelError("", "Файл с неправильным расширением");
                    return View(message);
                }
            }
            testDB.SaveChanges();
            return RedirectToAction("ShowMessages");
        }

        public ActionResult ShowMessages(int? page, int? tagId)
        {
            int pageNumber = page == null ? 1 : (int)page;
            if (tagId == null)
            {
                return View(testDB.Messages.OrderByDescending(message => message.CurrentDate).ToPagedList(pageNumber, 5));
            }
            else
            {
                ViewBag.TagId = tagId;
                List<Message> messages = new List<Message>();
                foreach (Message message in testDB.Messages.ToList<Message>())
                {
                    foreach (Tag currentTag in message.Tags)
                    {
                        if (currentTag.Id == tagId)
                        {
                            messages.Add(message);
                        }
                    }
                }
                return View(messages.OrderByDescending(message => message.CurrentDate).ToPagedList(pageNumber, 5));
            }
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddReply(int? messageId)
        {
            return View(new Reply { Message = testDB.Messages.FirstOrDefault(message => message.Id == messageId)});
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddReply(Reply reply)
        {
            reply.Author = GetUserById(this.User.Identity.GetUserId());
            reply.Message = testDB.Messages.FirstOrDefault(message => message.Id == reply.Message.Id);
            testDB.Replies.Add(reply);
            testDB.SaveChanges();
            return RedirectToAction("ShowMessage", new { id = reply.Message.Id });
        }

        [HttpGet]
        [Authorize]
        public ActionResult EditReply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reply reply = testDB.Replies.Find(id);
            if (reply == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(reply);
        }


        [HttpGet]
        [Authorize]
        public ActionResult DeleteImage(Message message)
        {
            message.Image = null;
            message.ImageId = null; ;
            testDB.Entry(message).State = EntityState.Modified;
            testDB.SaveChanges();
            return RedirectToAction("EditMessage", message);
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditReply(Reply reply)
        {
            if (reply == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            testDB.Entry(reply).State = EntityState.Modified;
            testDB.SaveChanges();
            return RedirectToAction("ShowMessage", new { id = reply.Message.Id });
        }

        public ActionResult ShowMessage(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = testDB.Messages.Find(id);
            return View(message);
        }

        public PartialViewResult LolComment(String comment)
        {
            ViewBag.Comment = comment;
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                testDB.Dispose();
            }
            base.Dispose(disposing);
        }

        private ApplicationUser GetUserById(String userId)
        {
            return testDB.Users.FirstOrDefault(user => user.Id == userId);
        }
    }
}