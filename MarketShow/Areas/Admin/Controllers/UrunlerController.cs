using MarketShow.Areas.Admin.Controllers;
using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketShow.Areas.Admin.Controllers
{
    public class UrunlerController : AdminBaseController
    {
        // GET: Admin/Urunler
        public ActionResult Index()
        {
            return View(db.Urunler.ToList());
        }

        public ActionResult Yeni()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Yeni(Urun urun, HttpPostedFileBase resim)
        {
            ResimHataKontrolu(resim);

            // formdan gelen veriler modelimize uygunsa
            if (ModelState.IsValid)
            {
                // resim gönderilmişse yükle
                var dosyaAd = ResimYukle(resim);

                urun.ResimYolu = dosyaAd;

                db.Urunler.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sil(int id)
        {
            Urun silinecek = db.Urunler.Find(id);

            if (!string.IsNullOrEmpty(silinecek.ResimYolu))
            {
                string resimTamYolu = Server.MapPath("~/Upload/" + silinecek.ResimYolu);

                if (System.IO.File.Exists(resimTamYolu))
                {
                    System.IO.File.Delete(resimTamYolu);
                }
            }

            db.Urunler.Remove(silinecek);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Duzenle(int id)
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View(db.Urunler.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Duzenle(Urun urun, HttpPostedFileBase resim)
        {
            ResimHataKontrolu(resim);

            if (ModelState.IsValid)
            {
                var dosyaAd = ResimYukle(resim);

                Urun dbUrun = db.Urunler.Find(urun.Id);

                if (!string.IsNullOrEmpty(dosyaAd))
                {
                    // resim değiştirmeden önce mevcut resim varsa sil
                    if (!string.IsNullOrEmpty(dbUrun.ResimYolu))
                    {
                        var resimTamYolu = Server.MapPath("~/Upload/" + dbUrun.ResimYolu);

                        if (System.IO.File.Exists(resimTamYolu))
                        {
                            System.IO.File.Delete(resimTamYolu);
                        }

                    }

                    dbUrun.ResimYolu = dosyaAd;
                }

                dbUrun.UrunAd = urun.UrunAd;
                dbUrun.BirimFiyat = urun.BirimFiyat;
                dbUrun.KategoriId = urun.KategoriId;
                dbUrun.Aciklama = urun.Aciklama;
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            ViewBag.KategoriId = new SelectList(db.Kategoriler.ToList(), "Id", "KategoriAd");

            return View(urun);
        }



        private void ResimHataKontrolu(HttpPostedFileBase resim)
        {
            if (resim != null)
            {
                // dosya türü kontrolü
                if (!resim.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("resim", "Lütfen bir resim dosyası seçiniz.");
                }
                // dosya boyutu kontrolü
                if (resim.ContentLength > 2 * 1024 * 1024)
                {
                    ModelState.AddModelError("resim", "Resim dosya boyutu 2MB'dan küçük olmalıdır.");
                }
                // dosya en boy oranı 1:1 olsun
                Bitmap bmp = new Bitmap(resim.InputStream);
                if (bmp.Width != bmp.Height)
                {
                    ModelState.AddModelError("resim", "Resim 1:1 (Kare) boyutunda olmalıdır.");
                }


            }
        }

        private string ResimYukle(HttpPostedFileBase resim)
        {
            if (resim != null && resim.ContentLength > 0)
            {
                var ext = Path.GetExtension(resim.FileName);
                var dosyaAd = Guid.NewGuid() + ext;
                var kaydetYol = Server.MapPath("~/Upload/" + dosyaAd);
                resim.SaveAs(kaydetYol);
                return dosyaAd;
            }

            return "";
        }
    }
}