using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public interface IArticleRepository
    {
        IQueryable<Article> Articles { get; }
        bool InsertArticle(IFormFile makaleResmi, string makaleBaşlığı, string makaleİçeriği, List<int> konular, int id);
        Article GetByID(int id);
        bool DeleteArticle(int id);
        bool UpdateArticle(IFormFile makaleResmi, string makaleBaşlığı, string makaleİçeriği, List<int> konular, int makaleId);
        void IncreaseHitRate(int id);
        void IncreaseHitRateOfCategory(int id);
        Article NewArticle(int userId, string articleHead, string articleContent, string articleImage, short readTime);
        void NewArticleCategory(int articleId, List<int> categories);

        
    }
}
