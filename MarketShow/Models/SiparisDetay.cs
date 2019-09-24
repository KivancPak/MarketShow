using MarketShow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketShow.Models
{
    [Table("SiparisDetaylar")]
    public class SiparisDetay
    {
        public int Id { get; set; }

        public int SiparisId { get; set; }

        public int UrunId { get; set; }

        public string UrunAd { get; set; }

        public decimal BirimFiyat { get; set; }

        public int Adet { get; set; }


        public virtual Urun Urun { get; set; }
        public virtual Siparis Siparis { get; set; }
    }
}