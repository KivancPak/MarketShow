using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketShow.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public abstract class AdminBaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/AdminBase
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                db.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}