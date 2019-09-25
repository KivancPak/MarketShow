using MarketShow.Areas.Admin.Controllers;
using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketShow.Areas.Admin.Controllers
{
    public class KategorilerController : AdminBaseController
    {
        // GET: Admin/Kategoriler
        public ActionResult Index()
        {
            return View(db.Kategoriler.ToList());
        }

        // GET: Admin/Kategoriler/Yeni
        public ActionResult Yeni()
        {
            return View();
        }

        // POST: Admin/Kategoriler/Yeni
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategoriler.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: Admin/Kategoriler/Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            Kategori silinecek = db.Kategoriler.Find(id);
            db.Kategoriler.Remove(silinecek);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Admin/Kategoriler/Duzenle/1
        public ActionResult Duzenle(int id)
        {
            return View(db.Kategoriler.Find(id));
        }

        // POST: Admin/Kategoriler/Duzenle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kategori).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}