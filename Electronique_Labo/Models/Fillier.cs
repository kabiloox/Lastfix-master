using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Fillier
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Abrv { get; set; }

        //---------------Clé Etrangère---------------------------//
        public int NiveauId { get; set; }
        public virtual Niveau Niveaus { get; set; }
    }
}