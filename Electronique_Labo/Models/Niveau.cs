using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Niveau
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        //--------ICollection-------------------
        public virtual ICollection<Fillier> Filliers { get; set; }
        public virtual ICollection<Expiriment> Expiriments { get; set; }

    }
}