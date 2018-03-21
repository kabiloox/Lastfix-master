using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Outil
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        //Clé Etrangère
        public int ExpirimentId { get; set; }

        public virtual Expiriment Expiriments { get; set; }
    }
}