using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public int ExpirimentId { get; set; }

        public virtual Expiriment Expiriment { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}