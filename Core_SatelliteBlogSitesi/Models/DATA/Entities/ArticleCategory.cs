using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class ArticleCategory
    {
        public int ArticleID { get; set; }
        public Article Article { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }

    }
}
