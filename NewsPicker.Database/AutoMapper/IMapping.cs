using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsPicker.Database.AutoMapper
{
    public interface IMapping
    {
        void CreateMap(IMapperConfigurationExpression config);
    }
}