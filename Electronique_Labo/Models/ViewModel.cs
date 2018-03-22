using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Electronique_Labo.Models
{
    public class ViewModel
    {
        public Expiriment Expiriment { get; set; }
        public List<Expiriment> Expiriments { get; set; }

        public Images Images { get; set; }
        public List<Images> Imageses { get; set; }

        public Outil Outil { get; set; }
        public List<Outil> Outils { get; set; }

        public Niveau Niveau { get; set; }
        public List<Niveau> Niveaus { get; set; }

        public Fillier Fillier { get; set; }
        public List<Fillier> Filliers { get; set; }

        public Conseil Conseil { get; set; }
        public List<Conseil> Conseils { get; set; }

        public Secteur Secteur { get; set; }
        public List<Secteur> Secteurs { get; set; }

        public GoogleDriveFile GoogleDriveFile { get; set; }
        public List<GoogleDriveFile> GoogleDriveFiles { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public IndexViewModel IndexViewModel { get; set; }
        public profileimg Profileimg { get; set; }

        public Favorite Favorite { get; set; }
        public List<Favorite> Favorites { get; set; }
    }
}