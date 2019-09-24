using MarketShow.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketShow.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public List<SepetOge> Sepet
        {
            get
            {
                if (Session["sepet"] == null)
                {
                    Session["sepet"] = new List<SepetOge>();
                }

                return (List<SepetOge>)Session["sepet"];
            }
        }

        // OnActionExecuting herhangi bir action çalışmadan önce çağrılır
        // eğer henüz bir sepet nesnesi yoksa onu bu aşamada oluşturuyoruz
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["sepet"] == null)
            {
                Session["sepet"] = new List<SepetOge>();
            }

            base.OnActionExecuting(filterContext);
        }

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