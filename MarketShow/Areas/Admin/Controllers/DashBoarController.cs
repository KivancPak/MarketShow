using MarketShow.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MarketShow.Areas.Admin.Controllers
{
    public class DashBoardController : AdminBaseController
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var sonuc = db.Siparisler
                .GroupBy(s => DbFunctions.TruncateTime(s.SiparisZamani))
                .Select(group => new CiroGunluk
                {
                    Gun = group.Key,
                    SiparisAdet = group.Count(),
                    Ciro = group.Sum(gs => gs.OdemeTutari)
                })
                .ToList();

            return View(sonuc);
        }
    }
}