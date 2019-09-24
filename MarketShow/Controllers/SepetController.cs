using Microsoft.AspNet.Identity;
using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarketShow.Controllers;

namespace NeArarsanMarket.Controllers
{
    public class SepetController : BaseController
    {
        // GET: Sepet
        public ActionResult Index()
        {
            return View(Sepet);
        }

        // GET: Sepet/Odeme
        [Authorize]
        public ActionResult Odeme()
        {
            ViewBag.SehirId = new SelectList(db.Sehirler.ToList(), "Id", "SehirAd");

            return View();
        }


        // POST: Sepet/Odeme
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Odeme(OdemeViewModel odemeVM)
        {
            // formdan gelen ödeme tutarı ile sepetteki ödeme tutarı aynı değilse
            if (odemeVM.OdemeTutari != Sepet.Sum(x => x.Adet * x.BirimFiyat))
            {
                ModelState.AddModelError("OdemeTutari", "Sepetinizde değişiklik yaptığınız için ödeme tutarı değişmiştir. Tekrar deneyiniz.");
            }

            if (ModelState.IsValid)
            {
                // ödemeyi al
                bool odemeSonuc = OdemeyiAl(odemeVM.OdemeTutari, odemeVM.KKNo, odemeVM.KKSonKullanmaTarihi, odemeVM.KKCvv);

                // ödeme başarısızsa
                if (!odemeSonuc)
                {
                    ModelState.AddModelError("OdemeTutari", "Kredi kartınızdan ödeme alınamadı. Lütfen kart bilgilerinizi kontrol ediniz.");
                }

                // ödeme başarılıysa
                if (odemeSonuc)
                {
                    // sipariş ve sipariş detaylarını veritabanına kaydet
                    Siparis siparis = new Siparis
                    {
                        MusteriId = User.Identity.GetUserId(),
                        SehirId = odemeVM.SehirId,
                        Ad = odemeVM.Ad,
                        Soyad = odemeVM.Soyad,
                        Email = odemeVM.Email,
                        Adres = odemeVM.Adres,
                        Adres2 = odemeVM.Adres2,
                        PostaKodu = odemeVM.PostaKodu,
                        OdemeTutari = odemeVM.OdemeTutari,
                        SiparisZamani = DateTime.Now,
                        SiparisDetaylar = new List<SiparisDetay>()
                    };

                    foreach (var item in Sepet)
                    {
                        siparis.SiparisDetaylar.Add(new SiparisDetay
                        {
                            UrunId = item.UrunId,
                            UrunAd = item.UrunAd,
                            BirimFiyat = item.BirimFiyat,
                            Adet = item.Adet
                        });
                    }

                    db.Siparisler.Add(siparis);
                    db.SaveChanges();
                    Sepet.Clear();

                    TempData["SiparisId"] = siparis.Id;
                    return RedirectToAction("OdemeAlindi", "Sepet");
                }
            }

            ViewBag.SehirId = new SelectList(db.Sehirler.ToList(), "Id", "SehirAd");

            return View();
        }

        private bool OdemeyiAl(decimal odemeTutari, string kKNo, string kKSonKullanmaTarihi, int kKCvv)
        {
            // anlaşmalı olduğumuz ödeme sisteminin apisini bu metoda entegre edebilirsiniz
            return true;
        }

        [HttpPost]
        public ActionResult SepetOgeSil(int id)
        {
            Sepet.RemoveAll(x => x.UrunId == id);
            decimal toplamTutar = Sepet.Sum(x => x.Adet * x.BirimFiyat);

            return Json(new
            {
                ToplamTutarTL = string.Format("{0:0.00}₺", toplamTutar),
                ToplamOgeAdet = Sepet.Count
            });
        }

        [HttpPost]
        public ActionResult SepeteEkle(int id)
        {
            // sepette bu id'ye ait bir kayıt varsa döndür
            SepetOge oge = Sepet.FirstOrDefault(x => x.UrunId == id);

            // sepete daha önce bu ürün eklenmemişse
            if (oge == null)
            {
                Urun urun = db.Urunler.Find(id);
                oge = new SepetOge
                {
                    UrunId = id,
                    UrunAd = urun.UrunAd,
                    BirimFiyat = urun.BirimFiyat,
                    Adet = 1,
                    ResimYolu = urun.ResimYolu
                };
                Sepet.Add(oge);
            }
            else
            {
                oge.Adet++;
            }

            return Json(new { ToplamOgeAdet = Sepet.Count });
        }

        public ActionResult OdemeAlindi()
        {
            return View();
        }
    }
}