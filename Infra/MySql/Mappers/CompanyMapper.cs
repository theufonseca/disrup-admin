using AutoMapper;
using Domain.Entities;
using Infra.MySql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.MySql.Mappers
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper()
        {
            CreateMap<Company, CompanyModel>();
            CreateMap<CompanyModel, Company>();
        }
    }
}
