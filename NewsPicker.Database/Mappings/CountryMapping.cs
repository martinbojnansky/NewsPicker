using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using NewsPicker.Shared.DTO.Country;
using NewsPicker.Database.Models;
using NewsPicker.Database.AutoMapper;

namespace NewsPicker.Database.Mappings
{
    public class CountryMapping : IMapping
    {
        public void CreateMap(IMapperConfigurationExpression config)
        {
            config.CreateMap<Country, CountryDTO>();
        }
    }
}