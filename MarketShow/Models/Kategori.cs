using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MarketShow.Models
{
    [Table("Kategoriler")]
    public class Kategori
    {
        public Kategori()
        {
            Urunler = new HashSet<Urun>();  
        }
        public int Id { get; set; }

        [Display(Name = "Kategori Adı")]
        [Required(ErrorMessage = "Kategori alanı zorunludur.")]
        [StringLength(200, ErrorMessage = "Kategori adı uzunluğu 200 karakteri geçemez.")]
        public string KategoriAd { get; set; }


        public virtual ICollection<Urun> Urunler { get; set; }
    }
}