using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketShow.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? kategoriId, string ara, string sirala)
        {
            // IQueryable türündeki nesneler üzerinde sorgulama metotları çalıştırılabilir
            IQueryable<Urun> sorgu = db.Urunler;

            // eğer kategoriId url'de belirtilmişse sorgumuza onu da kat
            if (kategoriId != null)
            {
                sorgu = sorgu.Where(x => x.KategoriId == kategoriId);
            }

            // arama kelimesi girildiyse ürün adlarında arama yap
            if (!string.IsNullOrEmpty(ara))
            {
                sorgu = sorgu.Where(x => x.UrunAd.Contains(ara));
            }

            switch (sirala)
            {
                case "fiyatArtan":
                    sorgu = sorgu.OrderBy(x => x.BirimFiyat);
                    break;
                case "fiyatAzalan":
                    sorgu = sorgu.OrderByDescending(x => x.BirimFiyat);
                    break;
                case "isimArtan":
                    sorgu = sorgu.OrderBy(x => x.UrunAd);
                    break;
                case "isimAzalan":
                    sorgu = sorgu.OrderByDescending(x => x.UrunAd);
                    break;
                default:
                    break;
            }

            ViewBag.sirala = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Value = "fiyatArtan", Text = "Fiyata Göre Artan" },
                new SelectListItem { Value = "fiyatAzalan", Text = "Fiyata Göre Azalan" },
                new SelectListItem { Value = "isimArtan", Text = "İsme Göre (A-Z)" },
                new SelectListItem { Value = "isimAzalan", Text = "İsme Göre (Z-A)" },
            }, "Value", "Text", sirala);

            return View(sorgu.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult KategorilerPartial()
        {
            return PartialView("_Kategoriler",
                db.Kategoriler.OrderBy(x => x.KategoriAd).ToList());
        }
        
    }
}