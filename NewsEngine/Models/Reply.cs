using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsEngine.Models
{
    public class Reply
    {
        public int Id { get; set; }
        public String Text { get; set; }
        public virtual Message Message { get; set; }
    }
}