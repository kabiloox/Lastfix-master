using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Secteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        //-------Clé Etrangère

        public virtual ICollection<Expiriment> Expiriments { get; set; }

    }
}