using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MarketShow.Models
{
    public class OdemeViewModel
    {
        [Required(ErrorMessage = "Zorunlu")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        public string Adres { get; set; }

        [Display(Name = "Adres (2. Satır)")]
        public string Adres2 { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "Şehir")]
        public int SehirId { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "Posta Kodu")]
        public string PostaKodu { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "Kart Sahibi")]
        public string KKSahip { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "Kredi Kart No")]
        public string KKNo { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "Son Kullanma")]
        public string KKSonKullanmaTarihi { get; set; }

        [Required(ErrorMessage = "Zorunlu")]
        [Display(Name = "CVV")]
        public int KKCvv { get; set; }

        [Required]
        public decimal OdemeTutari { get; set; }
    }
}