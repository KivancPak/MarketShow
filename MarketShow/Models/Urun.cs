using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace MarketShow.Models
{
    [Table("Urunler")]
    public class Urun
    {
        public int Id { get; set; }


        [Display(Name = "Kategori")]
        public int KategoriId { get; set; }


        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage ="Ürün adı alanı zorunludur.")]
        [StringLength(200,ErrorMessage = "Ürün adı uzunluğu 200 karakteri geçemez.")]
        public string UrunAd { get; set; }


        [Display(Name = "Açıklama")]
        [StringLength(50,ErrorMessage ="Açıklama 50 Krakterden fazla olamaz.")]
        public string Aciklama { get; set; }


        [Display(Name = "BirimFiyat")]
        [Required(ErrorMessage = "Birim fiyat alanı zorunludur.")]
        public decimal BirimFiyat { get; set; }


        [StringLength(200)]
        [Display(Name = "ResimYolu")]
        public string ResimYolu { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
}