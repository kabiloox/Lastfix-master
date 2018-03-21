﻿using System;
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
            return View();
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

            var vm = new ViewModel()
            {
                Niveaus = db.Niveaus.ToList(),
                Filliers = db.Filliers.ToList(),
                Secteurs = db.Secteurs.ToList(),
                Outils = db.Outils.Where(s => s.ExpirimentId == id).ToList(),
                Conseils = db.Conseils.Where(s => s.ExpirimentId == id).ToList(),
                Imageses = db.Imageses.Where(s => s.ExpirimentId == id).ToList(),
                Expiriment = db.Expiriments.Find(id)
            };

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
            var GroupsImage = UpdateImage.ToList();
            var list=db.Imageses.Where(s => s.ExpirimentId == viewModel.Expiriment.Id).ToList();
            db.Imageses.RemoveRange(list);
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
            OutilConseilImage.saveImage(UploadImage, UploadTitle, viewModel.Expiriment.Id);
            return RedirectToAction("Index");
        }
    }
}