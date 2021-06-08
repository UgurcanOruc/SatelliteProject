using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class User
    {
        public int UserID { get; set; }

        [Display(Name = "Ad")]
        [MaxLength(20, ErrorMessage = "İsim En Fazla 20 Karakter Olabilir.")]
        [MinLength(3, ErrorMessage = "İsim En Az 3 Karakter Olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string FirstName { get; set; }

        [Display(Name = "Soyad")]
        [MaxLength(20, ErrorMessage = "Soy İsim En Fazla 20 Karakter Olabilir.")]
        [MinLength(3, ErrorMessage = "Soy İsim En Az 3 Karakter Olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string LastName { get; set; }

        [Display(Name = "Mail Adresi")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string Email { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [MaxLength(10, ErrorMessage = "Kullanıcı Adı En Fazla 10 Karakter Olabilir.")]
        [MinLength(5, ErrorMessage = "Kullanıcı Adı En Az 5 Karakter Olabilir.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string UserName { get; set; }


        [Display(Name = "Açıklama")]
        [MaxLength(255, ErrorMessage = "Açıklama En Fazla 255 Karakter Olabilir.")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Bu Alan Boş Bırakılamaz.")]
        public string URL { get; set; }


        public string Avatar { get; set; }
        public int HitRate { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivationCode { get; set; }

        public virtual List<Article> Articles { get; set; }
        public ICollection<UserCategory> UserCategories { get; set; }
    }
}
