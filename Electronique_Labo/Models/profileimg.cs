using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class profileimg

    {
        public int Id { get; set; }
        [Required(ErrorMessage = "tu dois choisir une image")]
        public string image { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}