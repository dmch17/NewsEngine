using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace NewsEngine.Models
{
    public class Reply
    {
        public int Id { get; set; }
        [Display(Name="Текст комментария")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public String Text { get; set; }
        public virtual Message Message { get; set; }
        public virtual ApplicationUser Author { get; set; }
    }
}