using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
   
    public class KitapTuru
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Kitap Tür Adı Boş Bırakılmaz!")]
        
        public string Ad { get; set; }

    }
}
