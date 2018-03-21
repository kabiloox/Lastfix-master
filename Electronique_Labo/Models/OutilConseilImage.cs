using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Electronique_Labo.Models
{
    [NotMapped]
    public class OutilConseilImage
    {
        public static void SaveOutil(string[] Outildata, int idExpiriment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string[] Outli = Outildata;
            if(Outli==null)return;
            if (Outli.Length > 0)
            {
                foreach (string nameoutil in Outli)
                {
                    if (nameoutil != "")
                    {
                        var outil = new Outil
                        {
                            ExpirimentId = idExpiriment,
                            Nom = nameoutil
                        };
                        db.Outils.Add(outil);
                        db.SaveChanges();
                    }

                }
            }
        }

        public static void SaveConseil(string[] ConseilData,int idExpiriment)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string[] Conseil = ConseilData;
            if(Conseil==null)return;
            if (Conseil.Length > 0)
            {
                foreach (string nameConseil in Conseil)
                {
                    if (nameConseil != "")
                    {
                        var conseil = new Conseil
                        {
                            ExpirimentId = idExpiriment,
                            Nom = nameConseil
                        };
                        db.Conseils.Add(conseil);
                        db.SaveChanges();
                    }

                }
            }
        }

        public static void saveImage(IEnumerable<HttpPostedFileBase> UploadImage, List<string> UploadTitle,int ExpirimentId)
        {
            ApplicationDbContext db=new ApplicationDbContext();
            List<string> titList = UploadTitle;
            IEnumerable<HttpPostedFileBase> GroupImage = UploadImage;
           if(GroupImage==null)return;
            foreach (var file in GroupImage)
            {
                if (file.FileName != null && file.ContentLength > 0)
                {
                    foreach (var titre in titList)
                    {
                        var image = new Images();
                        var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/GroupsImage"), file.FileName);
                        file.SaveAs(path);

                        image.Image = file.FileName;
                        image.Titre = titre;
                        image.ExpirimentId = ExpirimentId;
                        db.Imageses.Add(image);
                        db.SaveChanges();
                        titList.Remove(titre);
                        goto CONTINUE;

                    }

                    CONTINUE: continue;
                }
            }
        }
    }
}