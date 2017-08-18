using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using NewsPicker.Shared.Models;
using NewsPicker.Api.Authorization;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Database.Models;
using NewsPicker.Database;

namespace NewsPicker.Api.Controllers
{
    public class ArticlesController : ApiKeyAuthorizationController
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        [HttpGet]
        [ResponseType(typeof(List<ArticleDTO>))]
        public List<ArticleDTO> GetTopArticlesByCategoryId(int categoryId, TimePeriodValue timePeriodValue = TimePeriodValue.DAY)
        {
            var categorySources = db.Categories.FirstOrDefault(c => c.Id == categoryId).Sources.Select(s => s.Id);
            var categoryArticles = db.Articles.Where(a => a.Sources.Any(s => categorySources.Contains(s.Id)));

            categoryArticles = GetTopArticles(categoryArticles, timePeriodValue);
            return categoryArticles.ProjectTo<ArticleDTO>().ToList();
        }

        [HttpGet]
        [ResponseType(typeof(List<ArticleDTO>))]
        public List<ArticleDTO> GetTopArticlesByCountryId(int countryId, TimePeriodValue timePeriodValue = TimePeriodValue.DAY)
        {
            var countryCategories = db.Categories.Where(c => c.CountryId == countryId).Select(c => c.Id);
            var countrySources = db.Sources.Where(s => s.Categories.Any(c => countryCategories.Contains(c.Id))).Select(s => s.Id);
            var countryArticles = db.Articles.Where(a => a.Sources.Any(s => countrySources.Contains(s.Id)));

            countryArticles = GetTopArticles(countryArticles, timePeriodValue);
            return countryArticles.ProjectTo<ArticleDTO>().ToList();
        }

        [NonAction]
        private IQueryable<Article> GetTopArticles(IQueryable<Article> articles, TimePeriodValue timePeriodValue)
        {
            DateTime? startDate = DateTime.UtcNow.AddHours(-(int)timePeriodValue);

            // Filter by date period
            articles = articles.Where(a => startDate <= a.CreatedDate && a.EngagementCount >= 1);
            // Order by share count
            articles = articles.OrderByDescending(a => a.EngagementCount);
            // Get top 10 distinct articles
            articles = articles.Take(10);

            return articles;
        }
    }
}