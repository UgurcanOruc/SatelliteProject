using Core_SatelliteBlogSitesi.Models.DAL;
using Core_SatelliteBlogSitesi.Models.Methods;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core_SatelliteBlogSitesi.Models.DATA
{
    public class ArticleRepository : IArticleRepository
    {
        private ProjectContext _context;

        [Obsolete]
        public IHostingEnvironment Environment;

        [Obsolete]
        public ArticleRepository(ProjectContext context, IHostingEnvironment environment)
        {
            _context = context;
            Environment = environment;
        }

        public IQueryable<Article> Articles => _context.Articles;


        public Article GetByID(int id)
        {
            return _context.Articles.Find(id);
        }



        [Obsolete]
        public bool InsertArticle(IFormFile makaleResmi, string makaleBaşlığı, string makaleİçeriği, List<int> konular, int id)
        {
            short readTime = GeneralMethods.CalculateReadTime(makaleİçeriği);

            string uniqueFileName = GeneralMethods.UploadImage(makaleResmi, Environment);

            Article article = NewArticle(id, makaleBaşlığı, makaleİçeriği, uniqueFileName, readTime);
            _context.Articles.Add(article);
            _context.SaveChanges();

            NewArticleCategory(article.ArticleID, konular);

            return _context.SaveChanges() > 0;
        }


        [Obsolete]
        public bool UpdateArticle(IFormFile makaleResmi, string makaleBaşlığı, string makaleİçeriği, List<int> konular, int makaleId)
        {
            string uniqueFileName = string.Empty;

            Article article = _context.Articles.Find(makaleId);

            if (makaleResmi == null)
                uniqueFileName = article.ArticleImage;

            else
            {
                GeneralMethods.DeleteImage(article.ArticleImage);

                uniqueFileName = GeneralMethods.UploadImage(makaleResmi, Environment);
            }

            article.ArticleImage = uniqueFileName;
            article.Head = makaleBaşlığı;
            article.Content = makaleİçeriği;
            article.ReadTime = GeneralMethods.CalculateReadTime(makaleİçeriği);

            _context.Articles.Update(article);

            DeleteArticleCategory(makaleId);

            NewArticleCategory(article.ArticleID, konular);

            return _context.SaveChanges() > 0;
        }


        
        public Article NewArticle(int userId, string articleHead, string articleContent, string articleImage, short readTime)
        {
            Article article = new Article()
            {
                UserID = userId,
                Head = articleHead,
                Content = articleContent,
                ArticleImage = articleImage,
                ReadTime = readTime,
                ReleaseDate = DateTime.Now,
                HitRate = 0,
            };

            return article;
        }


        public void NewArticleCategory(int articleId, List<int> categories)
        {
            foreach (int item in categories)
            {
                ArticleCategory articleCategory = new ArticleCategory()
                {
                    ArticleID = articleId,
                    CategoryID = item,
                };
                _context.ArticleCategories.Add(articleCategory);
                _context.SaveChanges();
            }
        }


        private void DeleteArticleCategory(int articleId)
        {
            List<ArticleCategory> categories = _context.ArticleCategories.Where(x => x.ArticleID == articleId).ToList();

            foreach (ArticleCategory item in categories)
            {
                _context.ArticleCategories.Remove(item);
                _context.SaveChanges();
            }
        }


        public void IncreaseHitRate(int id)
        {
            Article article = _context.Articles.Where(x => x.ArticleID == id).First();
            article.HitRate++;
            _context.Articles.Update(article);
            _context.SaveChanges();
        }


        public void IncreaseHitRateOfCategory(int id)
        {
            Category category = _context.Categories.Where(x => x.CategoryID == id).First();
            category.HitRate++;
            _context.Categories.Update(category);
        }



        public bool DeleteArticle(int id)
        {
            Article article = GetByID(id);

            GeneralMethods.DeleteImage(article.ArticleImage);

            _context.Articles.Remove(article);

            List<ArticleCategory> konuları = _context.ArticleCategories.Where(x => x.ArticleID == id).ToList();

            foreach (ArticleCategory item in konuları)
                _context.ArticleCategories.Remove(item);

            return _context.SaveChanges() > 0;
        }
    }
}
