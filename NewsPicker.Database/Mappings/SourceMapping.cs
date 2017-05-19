using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NewsPicker.Shared.DTO.Source;
using NewsPicker.Database.Models;
using NewsPicker.Database.AutoMapper;

namespace NewsPicker.Database.Mappings
{
    public class SourceMapping : IMapping
    {
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Source, SourceDTO>();
        }
    }
}