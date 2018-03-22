using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Electronique_Labo.Models;
using Microsoft.AspNet.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace Electronique_Labo
{
    [Authorize]
    public class ExpirimentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Expiriment
        public ActionResult Index()
        {
            var IdUser = User.Identity.GetUserId();
            var Vn = new ViewModel
            {
                Expiriments = db.Expiriments.Where(s=>s.ApplicationUserId== IdUser).ToList(),
                Niveaus = db.Niveaus.ToList()
            };
            return View(Vn);
        }

        public ActionResult Create()
        {
            var vm = new ViewModel()
            {
                Niveaus = db.Niveaus.ToList(),
                Filliers = db.Filliers.ToList(),
                Secteurs = db.Secteurs.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ViewModel VN, HttpPostedFileBase ImageeExPrincipale, string[] Outildata, string[] ConseilData, IEnumerable<HttpPostedFileBase> UploadImage, List<string> UploadTitle, IEnumerable<HttpPostedFileBase> UploadDrive, List<string> UploadTitleDrive)
        {
            if (ModelState.IsValid)
            {
                //Ajouter Expiriment----------------------------------------------
                var ImageExprirmentPath = Path.Combine(Server.MapPath("~/Images/ImageExpiriment"), ImageeExPrincipale.FileName);
                ImageeExPrincipale.SaveAs(ImageExprirmentPath);
                VN.Expiriment.ApplicationUserId = User.Identity.GetUserId();
                VN.Expiriment.DateTime = DateTime.Now;
                VN.Expiriment.Image = ImageeExPrincipale.FileName;
                VN.Expiriment.IdFilliere = string.Join(",", VN.Expiriment.SelectedArrayFilliere);
                db.Expiriments.Add(VN.Expiriment);
                db.SaveChanges();
                //------------------------------Ajouter Outils--------------------------------------

                OutilConseilImage.SaveOutil(Outildata, VN.Expiriment.Id);

                //------------------------------Ajouter Conseil--------------------------------------

                OutilConseilImage.SaveConseil(ConseilData, VN.Expiriment.Id);

                //Ajouter Image----------------------------------------------------------------------
                OutilConseilImage.saveImage(UploadImage, UploadTitle, VN.Expiriment.Id);
                //Ajouter Fichier pour telecharger
                GoogleDriveFilesRepository.FileUpload(UploadDrive, UploadTitleDrive, VN.Expiriment.Id);

                return RedirectToAction("Index");
            }

            return View();
        }
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return HttpNotFound();
            }
            var exp = db.Expiriments.Find(id);
            if (exp == null)
            {
                return HttpNotFound();
            }
            var vm = new ViewModel();      
            vm.Niveaus = db.Niveaus.ToList();
            vm.Filliers = db.Filliers.ToList();
            vm.Secteurs = db.Secteurs.ToList();
            vm.Outils = db.Outils.Where(s => s.ExpirimentId == id).ToList();
            vm.Conseils = db.Conseils.Where(s => s.ExpirimentId == id).ToList();
            vm.Imageses = db.Imageses.Where(s => s.ExpirimentId == id).ToList();
            vm.GoogleDriveFiles = db.GoogleDriveFiles.Where(s => s.ExpirimentId == id).ToList();
            vm.Expiriment = exp;
            vm.Expiriment.SelectedArrayFilliere= exp.IdFilliere.Split(',').ToArray();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ViewModel viewModel, HttpPostedFileBase EditImage, string[] Outildata, string[] ConseilData,List<string> UpdateImage, IEnumerable<HttpPostedFileBase> UploadImage, List<string> UploadTitle)
        {
            string oldImage = Path.Combine(Server.MapPath("~/Images/ImageExpiriment"), viewModel.Expiriment.Image);
            if (EditImage != null)
            {
                System.IO.File.Delete(oldImage);
                var ImageExprirmentPath = Path.Combine(Server.MapPath("~/Images/ImageExpiriment"), EditImage.FileName);
                EditImage.SaveAs(ImageExprirmentPath);
                viewModel.Expiriment.Image = EditImage.FileName;
            }
            viewModel.Expiriment.IdFilliere = string.Join(",", viewModel.Expiriment.SelectedArrayFilliere);
            viewModel.Expiriment.DateTime = DateTime.Now;
            db.Entry(viewModel.Expiriment).State = EntityState.Modified;
            db.SaveChanges();
            // Update Outils --------------------------------------------
      
            var outils = db.Outils.ToList();
            outils = outils.Where(s => s.ExpirimentId == viewModel.Expiriment.Id).ToList();
            db.Outils.RemoveRange(outils);
            db.SaveChanges();
            OutilConseilImage.SaveOutil(Outildata, viewModel.Expiriment.Id);

            //Update Conseil------------------------------------------
            var Conseil = db.Conseils.ToList();
            Conseil = Conseil.Where(s => s.ExpirimentId == viewModel.Expiriment.Id).ToList();
            db.Conseils.RemoveRange(Conseil);
            OutilConseilImage.SaveConseil(ConseilData, viewModel.Expiriment.Id);
            //Update Groups Image-----------------------------------------------------------------------
            var list = db.Imageses.Where(s => s.ExpirimentId == viewModel.Expiriment.Id).ToList();
            db.Imageses.RemoveRange(list);
            if (UpdateImage != null) {
                var GroupsImage = UpdateImage.ToList();
          
                db.SaveChanges();
                foreach (var img in GroupsImage)
                {
                    var images = new Images
                    {
                        Image = img,
                        ExpirimentId = viewModel.Expiriment.Id
                    };
                    db.Imageses.Add(images);
                    db.SaveChanges();
                }
            }
            OutilConseilImage.saveImage(UploadImage, UploadTitle, viewModel.Expiriment.Id);

            //Update Google Drive
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var Expiriment = db.Expiriments.Find(id);
            db.Expiriments.Remove(Expiriment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetExpiriment(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var exp = db.Expiriments.Find(id);
            if (exp == null)
            {
                return HttpNotFound();
            }
            var ImageUser = exp.ApplicationUserId;
            var vm = new ViewModel();
            vm.Niveaus = db.Niveaus.ToList();
            vm.Filliers = db.Filliers.ToList();
            vm.Secteur = db.Secteurs.Single(s=>s.Id== exp.SecteurId);
            vm.Outils = db.Outils.Where(s => s.ExpirimentId == id).ToList();
            vm.Conseils = db.Conseils.Where(s => s.ExpirimentId == id).ToList();
            vm.Imageses = db.Imageses.Where(s => s.ExpirimentId == id).ToList();
            vm.GoogleDriveFiles = db.GoogleDriveFiles.Where(s => s.ExpirimentId == id).ToList();
            vm.Profileimg = db.Profileimgs.Single(s => s.ApplicationUserId == ImageUser);
            vm.Expiriment = exp;
            vm.Expiriment.SelectedArrayFilliere = exp.IdFilliere.Split(',').ToArray();

            return View(vm);
        }
        public ActionResult MyExpSearch(int? id,string title)
        {
            var IdUser = User.Identity.GetUserId();
            //listerecherche.Where(s => s.Nom_Produit.IndexOf(txtrecherche.Text, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
            var Vn = new ViewModel();
            if (title == null)
            {
                Vn.Expiriments = db.Expiriments.Where(s => s.ApplicationUserId == IdUser).ToList()
                    .Where(s => s.NiveauId == id).ToList();
            }
            else
            {
                if (id == null)
                {
                    Vn.Expiriments = db.Expiriments.Where(s => s.ApplicationUserId == IdUser).ToList()
                        .Where(s => s.Titre.IndexOf(title, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();
                }
                else
                {
                    Vn.Expiriments = db.Expiriments.Where(s => s.ApplicationUserId == IdUser).ToList()
                        .Where(s => s.NiveauId == id).ToList()
                        .Where(s => s.Titre.IndexOf(title, StringComparison.CurrentCultureIgnoreCase) != -1).ToList();


                }
            }
            Vn.Niveaus = db.Niveaus.ToList();
            return View("Index",Vn);
        }
        //Favorite Controller
      
        public ActionResult Addtofavorite(int id)
        {

            var userId = User.Identity.GetUserId();
            var medcinId = id;
            var check = db.Favorites.Where(a => a.ExpirimentId == id && a.ApplicationUserId == userId).ToList();
            if (check.Count < 1)
            {
                var favorie = new Favorite();
                favorie.ExpirimentId = id;
                favorie.ApplicationUserId = userId;
                db.Favorites.Add(favorie);
                db.SaveChanges();
                TempData["Validation"] = "Expiriment Ajouter dans favorite ";

            }
            else
            {
                TempData["Validation"] = "Expiriment existe D'éja dans favorite";


            }
            return RedirectToAction("FavoriteIndex");

        }

        public ActionResult FavoriteIndex()
        {
            var IdUser = User.Identity.GetUserId();
            var Vn = new ViewModel
            {
                Favorites = db.Favorites.Where(s => s.ApplicationUserId == IdUser).ToList()
            };
            return View(Vn);
        }
    }
}