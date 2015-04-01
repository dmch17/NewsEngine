using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsEngine.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NewsEngine.DALObjects
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ApplicationUser userAdmin = new ApplicationUser() { UserName = "Admin", Id = "Admin" };
            IdentityRole roleAdmin = new IdentityRole("admin");

            userManager.Create(userAdmin, "qwerty");
            roleManager.Create(roleAdmin);
            userManager.AddToRole(userAdmin.Id, roleAdmin.Name);

            ApplicationUser userTest = new ApplicationUser() { UserName = "TestUser", Id = "TestUser" };
            IdentityRole roleTest = new IdentityRole("user");

            userManager.Create(userTest, "123456");
            roleManager.Create(roleTest);
            userManager.AddToRole(userTest.Id, roleTest.Name);

            List<Message> messages = new List<Message>();
            Tag tagNews = new Tag { TagText = "Новость" };
            Tag tagText = new Tag { TagText = "Пометка" };
            int randomTag;
            for (int currentMessage = 10; currentMessage > 0; currentMessage--)
            {
                Message message = new Message() { MessageTitle = String.Format("Титул новости {0}", currentMessage),
                    Text = String.Format("Текст новости {0}", currentMessage), Author = userAdmin};
                if (currentMessage == 2 || currentMessage == 4)
                {
                    for (int currentReply = 5; currentReply > 0; currentReply--)
                    {
                        Reply reply = new Reply() { Text = String.Format("Текст ответа {0}", currentReply), Message = message, Author = userTest };
                        context.Replies.Add(reply);
                    }
                }
                randomTag = new Random().Next(-1,2);
                if (randomTag == -1)
                {
                    message.Tags.Add(tagNews);
                    tagNews.Messages.Add(message);
                }
                else if (randomTag == 0)
                {
                    message.Tags.Add(tagText);
                    tagText.Messages.Add(message);
                }
                else
                {
                    message.Tags.Add(tagText);
                    message.Tags.Add(tagNews);
                    tagText.Messages.Add(message);
                    tagNews.Messages.Add(message);
                }
                context.Messages.Add(message);
            }
            context.Tags.Add(tagNews);
            context.Tags.Add(tagText);
            context.SaveChanges();

        }
    }
}