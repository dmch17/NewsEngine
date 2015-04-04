using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace NewsEngine.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Display(Name = "Тег")]
        public string TagText { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public Tag()
        {
            Messages = new List<Message>();
        }
    }
}