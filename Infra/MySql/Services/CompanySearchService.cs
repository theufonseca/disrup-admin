using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.UseCases.CompanyFilter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.MySql.Services
{
    public class CompanySearchService : ICompanySearchService
    {
        private readonly IMapper mapper;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public CompanySearchService(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            this.mapper = mapper;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<IEnumerable<Company>> Filter(Filters filters)
        {
            List<Company> companies = new();
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var companiesModel = await dataContext.Company
                .Where(x => (string.IsNullOrEmpty(filters.Name) || (x.Name.Contains(filters.Name) || x.FantasyName.Contains(filters.Name) || x.LegalName.Contains(filters.Name))))
                .Include(x => x.Photos)
                .ToListAsync();

            if (companiesModel is not null && companiesModel.Any())
                companies.AddRange(companiesModel.Select(x => mapper.Map<Company>(x)));

            return companies;
        }
    }
}
