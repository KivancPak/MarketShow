using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketShow.Models
{
    public class SepetOge
    {
        public int UrunId { get; set; }

        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public int Adet { get; set; }

        public string ResimYolu { get; set; }

        public decimal Tutar()
        {
            return Adet * BirimFiyat;
        }

        public string TutarMetin()
        {
            return string.Format("{0:0.00}", Tutar());
        }
    }
}