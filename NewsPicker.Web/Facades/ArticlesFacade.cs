using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NewsPicker.Shared.Models;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Database.Models;
using NewsPicker.Database;

namespace NewsPicker.Web.Facades
{
    public class ArticlesFacade
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        public List<ArticleDTO> GetTopArticlesByCategoryId(int categoryId, int timePeriodId)
        {
            var categorySources = db.Categories.FirstOrDefault(c => c.Id == categoryId).Sources.Select(s => s.Id);
            var categoryArticles = db.Articles.Where(a => a.Sources.Any(s => categorySources.Contains(s.Id)));

            categoryArticles = GetTopArticles(categoryArticles, timePeriodId);
            return categoryArticles.ProjectTo<ArticleDTO>().ToList();
        }

        public List<ArticleDTO> GetTopArticlesByCountryId(int countryId, int timePeriodId)
        {
            var countryCategories = db.Categories.Where(c => c.CountryId == countryId).Select(c => c.Id);
            var countrySources = db.Sources.Where(s => s.Categories.Any(c => countryCategories.Contains(c.Id))).Select(s => s.Id);
            var countryArticles = db.Articles.Where(a => a.Sources.Any(s => countrySources.Contains(s.Id)));
            countryArticles = GetTopArticles(countryArticles, timePeriodId);

            return countryArticles.ProjectTo<ArticleDTO>().ToList();
        }

        private IQueryable<Article> GetTopArticles(IQueryable<Article> articles, int timePeriodId)
        {
            DateTime? startDate = DateTime.UtcNow.AddHours(-timePeriodId);

            // Filter by date period and minimum engagement
            articles = articles.Where(a => startDate <= a.CreatedDate && a.EngagementCount >= 1);
            // Order by engagement count
            articles = articles.OrderByDescending(a => a.EngagementCount);
            // Get top 10 distinct articles
            articles = articles.Take(10);

            return articles;
        }
    }
}