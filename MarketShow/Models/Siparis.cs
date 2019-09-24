using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketShow.Models
{
    [Table("Siparisler")]
    public class Siparis
    {
        public int Id { get; set; }

        [ForeignKey("Musteri")]
        public string MusteriId { get; set; }

        public int SehirId { get; set; }

        [Required]
        public string Ad { get; set; }

        [Required]
        public string Soyad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Adres { get; set; }

        public string Adres2 { get; set; }

        [Required]
        public string PostaKodu { get; set; }

        public decimal OdemeTutari { get; set; }

        [Required]
        public DateTime? SiparisZamani { get; set; }


        public virtual ApplicationUser Musteri { get; set; }
        public virtual Sehir Sehir { get; set; }
        public virtual ICollection<SiparisDetay> SiparisDetaylar { get; set; }
    }
}