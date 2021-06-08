using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class Article
    {
        public int ArticleID { get; set; }
        public int UserID { get; set; }

        [Display(Name = "Başlık")]
        [MaxLength(150, ErrorMessage = "Başlık En Fazla 150 Karakter Olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string Head { get; set; }

        [Display(Name = "Makale İçeriği")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string Content { get; set; }

        [Display(Name = "Fotoğraf")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string ArticleImage { get; set; }
        public short ReadTime { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public int HitRate { get; set; }

        public virtual User User { get; set; }
        public ICollection<ArticleCategory> ArticleCategories { get; set; }

    }
}
