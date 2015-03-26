using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsEngine.Models;

namespace NewsEngine.DALObjects
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            List<Message> messages = new List<Message>();
            for (int currentMessage = 10; currentMessage > 0; currentMessage--)
            {
                Message message = new Message() { MessageTitle = String.Format("Титул новости {0}", currentMessage),
                    Text = String.Format("Текст новости {0}", currentMessage)};
                context.Messages.Add(message);
                if (currentMessage == 2 || currentMessage == 4)
                {
                    for (int currentReply = 5; currentReply > 0; currentReply--)
                    {
                        Reply reply = new Reply() { Text = String.Format("Тектс ответа {0}", currentReply), Message = message };
                        context.Replies.Add(reply);
                    }
                }
            }
            context.SaveChanges();

        }
    }
}