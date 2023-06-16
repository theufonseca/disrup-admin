using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.UseCases.CompanyFilter;
using Infra.MySql.Models;
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
                                    .Where(x =>
                                        x.Status == Domain.Enums.CompanyStatus.ACTIVE &&
                                        (string.IsNullOrEmpty(filters.Name) ||
                                            x.Name.Contains(filters.Name) ||
                                            x.FantasyName.Contains(filters.Name) ||
                                            x.LegalName.Contains(filters.Name)))
                                    .Include(x => x.Photos)
                                    .ToListAsync();

            companiesModel = companiesModel.Where(x =>
                !filters.Categories.Any() ||
                filters.Categories.Any(y =>
                    (x.Category1 != null && x.Category1.ToUpper().Contains(y.ToUpper())) ||
                    (x.Category2 != null && x.Category2.ToUpper().Contains(y.ToUpper())) ||
                    (x.Category3 != null && x.Category3.ToUpper().Contains(y.ToUpper())) ||
                    (x.Category4 != null && x.Category4.ToUpper().Contains(y.ToUpper())) ||
                    (x.Category5 != null && x.Category5.ToUpper().Contains(y.ToUpper()))
                )
            ).ToList();

            if (companiesModel is not null && companiesModel.Any())
                companies.AddRange(companiesModel.Select(x => mapper.Map<Company>(x)));

            return companies;
        }
    }

    public static class CompanySearchExtension
    {
        public static IQueryable<CompanyModel> GetWhere(this DbSet<CompanyModel> Company, Filters filters)
        {
            //Company.Where(x => x.Status == Domain.Enums.CompanyStatus.ACTIVE);

            if(!string.IsNullOrEmpty(filters.Name))
                Company.Where(x => x.Name.Contains(filters.Name) || x.FantasyName.Contains(filters.Name) || x.LegalName.Contains(filters.Name));

            return Company;
        }
    }
}
