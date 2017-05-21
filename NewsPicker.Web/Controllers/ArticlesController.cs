﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NewsPicker.Shared.Enums;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Database.Models;
using NewsPicker.Database;

namespace NewsPicker.Web.Controllers
{
    public class ArticlesController
    {
        private NewsPickerDatabase db = new NewsPickerDatabase();

        public List<ArticleDTO> GetTopArticlesByCategoryId(int categoryId, TimePeriod timePeriod = TimePeriod.DAY)
        {
            IQueryable<Article> articles;

            // Filter by category
            articles = db.Articles.Where(a => a.Source.Categories.FirstOrDefault(c => c.Id == categoryId) != null);
            articles = GetTopArticles(articles, timePeriod);

            return articles.ProjectTo<ArticleDTO>().ToList();
        }

        public List<ArticleDTO> GetTopArticlesByCountryId(int countryId, TimePeriod timePeriod = TimePeriod.DAY)
        {
            IQueryable<Article> articles;

            // Filter by country
            articles = db.Articles.Where(a => a.Source.Categories.FirstOrDefault(c => c.CountryId == countryId) != null);
            articles = GetTopArticles(articles, timePeriod);

            return articles.ProjectTo<ArticleDTO>().ToList();
        }

        private IQueryable<Article> GetTopArticles(IQueryable<Article> articles, TimePeriod timePeriod)
        {
            DateTime? startDate = DateTime.UtcNow.AddHours(-(int)timePeriod);

            // Filter by date period
            articles = articles.Where(a => startDate <= a.CreatedDate);
            // Order by share count
            articles = articles.OrderByDescending(a => a.ShareCount);
            // Take top 10
            articles = articles.Take(10);

            return articles;
        }
    }
}