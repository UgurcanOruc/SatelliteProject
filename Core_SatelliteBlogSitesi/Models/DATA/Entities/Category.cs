using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int HitRate { get; set; }
        public ICollection<UserCategory> UserCategories { get; set; }
        public ICollection<ArticleCategory> ArticleCategories { get; set; }
    }
}
