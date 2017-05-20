using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NewsPicker.Database;
using NewsPicker.Database.Models;
using NewsPicker.Shared.DTO.Article;
using NewsPicker.Database.AutoMapper;

namespace NewsPicker.Database.Mappings
{
    public class ArticleMapping : IMapping
    {
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Article, ArticleDTO>();
        }
    }
}