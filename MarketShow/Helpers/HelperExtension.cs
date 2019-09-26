using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketShow.Helpers
{
    public static class HelperExtensions
    {
        public static List<SepetOge> Sepet(this HtmlHelper htmlHelper)
        {
            // Session üzerinde tutulan sepet nesnesini getir ve kendi türüne dönüştür
            var sepet = htmlHelper.ViewContext.HttpContext.Session["sepet"] as List<SepetOge>;

            return sepet;
        }

        public static decimal SepetTutar(this HtmlHelper htmlHelper)
        {
            var sepet = htmlHelper.ViewContext.HttpContext.Session["sepet"] as List<SepetOge>;


            return sepet == null ? 0 : sepet.Sum(x => x.Adet * x.BirimFiyat);
        }

        public static int SepetAdet(this HtmlHelper htmlHelper)
        {
            // eğer session'da sepet adıyla bir nesne yoksa yani null ise sepet değişkenine null ata
            // eğer varsa List<SepetOge> türüne dönüştür ve öyle ata
            var sepet = htmlHelper.ViewContext.HttpContext.Session["sepet"] as List<SepetOge>;

            return sepet == null ? 0 : sepet.Count;
        }

        // this keyword'ü ile HtmlHelper class'ını extend ettik
        // artık view üzerinde @Html nesnesi üzerinden bu metodu kullanabilirz
        public static MvcHtmlString UrunImg(this HtmlHelper htmlHelper, string resimYolu, object htmlAttributes)
        {

            if (string.IsNullOrEmpty(resimYolu))
            {
                resimYolu = "~/Images/no-product-image.png";
            }
            else
            {
                resimYolu = "~/Upload/" + resimYolu;
            }

            resimYolu = VirtualPathUtility.ToAbsolute(resimYolu);

            // https://stackoverflow.com/a/9669688

            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) as IDictionary<string, object>;

            TagBuilder tag = new TagBuilder("img");
            tag.MergeAttributes(attributes);
            tag.MergeAttribute("src", resimYolu);


            return new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));
        }
    }
}