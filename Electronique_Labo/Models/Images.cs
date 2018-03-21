using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Images
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Image { get; set; }
        //Clé Etrangère
        public int ExpirimentId { get; set; }

        public Expiriment Expiriments { get; set; }
    }
}