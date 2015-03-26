using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsEngine.Models
{
    public class Message
    {
        public int id { get; set; }

        public String Text { get; set; }
        public String MessageTitle { get; set; }
        public DateTime CurrentDate { get; private set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public Message()
        {
            CurrentDate = DateTime.Now;
        }
    }
}