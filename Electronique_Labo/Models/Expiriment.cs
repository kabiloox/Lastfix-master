using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Electronique_Labo.Models
{
    public class Expiriment
    {
        public int Id { get; set; }
        
        public string Titre { get; set; }
        public string Image { get; set; }
        public string But { get; set; }
        public string ResultatAttendue { get; set; }
        public string Notes { get; set; }
        public DateTime DateTime { get; set; }


        //----------------ICollection---------------------------//

        public virtual ICollection<Images> Imageses { get; set; }
        public virtual ICollection<Outil> Outils { get; set; }
        public virtual ICollection<Conseil> Conseils { get; set; }
        public virtual ICollection<GoogleDriveFile> GoogleDriveFiles { get; set; }

        //---------------Clé Etrangère Niveau---------------------------//

        public int NiveauId { get; set; }
        public virtual Niveau Niveaus { get; set; }

        //---------------Clé Etrangère Secteur---------------------------//

        public int SecteurId { get; set; }
        public virtual Secteur Secteurs { get; set; }

        //---------------FiliereSelectionner----------------------------
        public string IdFilliere { get; set; }
        [NotMapped]
        public IEnumerable<Fillier> Filliers { get; set; }
        [NotMapped]
        public string[] SelectedArrayFilliere { get; set; }

        //User Id
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}