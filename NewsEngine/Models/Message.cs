using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace NewsEngine.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Display(Name = "Текст")]
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public String Text { get; set; }
        [Display(Name = "Заголовок")]
        [Required]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Длина титула должна быть от 5 до 15 символов")]
        public String MessageTitle { get; set; }
        public DateTime CurrentDate { get; private set; }

        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ApplicationUser Author { get; set; }

        public Message()
        {
            Tags = new List<Tag>();
            CurrentDate = DateTime.Now;
        }
    }
}