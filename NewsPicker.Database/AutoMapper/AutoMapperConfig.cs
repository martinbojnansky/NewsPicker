using AutoMapper;
using NewsPicker.Database.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsPicker.Database.AutoMapper
{
    public class AutoMapperConfig
    {
        public static void CreateMap(IMapperConfigurationExpression config)
        {
            new CountryMapping().CreateMap(config);
            new CategoryMapping().CreateMap(config);
            new SourceMapping().CreateMap(config);
            new ArticleMapping().CreateMap(config);
        }
    }
}