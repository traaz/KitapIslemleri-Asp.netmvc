using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Kitap
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string KİtapAdi { get; set; }
        public string Aciklama { get; set; }
        public string Yazar { get; set; }
        public double Fiyat { get; set; }
        [ValidateNever]
        public int KitapTuruId { get; set; }
        [ForeignKey("KitapTuruId")]
        [ValidateNever]
        public KitapTuru KitapTuru { get; set; }
    }
}
