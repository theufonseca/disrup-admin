using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
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
    public class CompanyService : ICompanyService
    {
        private readonly IMapper mapper;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public CompanyService(IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            this.mapper = mapper;
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Exists(string document)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var company = await dataContext.Company
                .FirstOrDefaultAsync(x => x.Document == document);

            return company != null;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            List<Company> companies = new();
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var companiesModel = await dataContext.Company.Include(x => x.Photos).ToListAsync();

            if(companiesModel is not null && companiesModel.Any())
                companies.AddRange(companiesModel.Select(x => mapper.Map<Company>(x)));

            return companies;
        }

        public async Task<Company?> GetById(int id)
        {
            Company? company = null;
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var companyModel = await dataContext.Company.Where(x => x.Id == id)
                .Include(x => x.Photos).FirstOrDefaultAsync();

            if (companyModel != null)
                company = mapper.Map<Company>(companyModel);

            return company;
        }

        public async Task<IEnumerable<Company>> GetByStatus(CompanyStatus companyStatus)
        {
            List<Company> companies = new();
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var companiesModel = await dataContext.Company.Where(x => x.Status == companyStatus)
                .Include(x => x.Photos).ToListAsync();

            if(companiesModel is not null && companiesModel.Any())
                companies.AddRange(companiesModel.Select(x => mapper.Map<Company>(x)));

            return companies;
        }

        public async Task<int> Insert(Company company)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var companyModel = mapper.Map<CompanyModel>(company);

            if (companyModel is null)
                throw new ArgumentException("Error when inserting company");

            dataContext.Company.Add(companyModel);
            await dataContext.SaveChangesAsync();

            return companyModel.Id;
        }

        public async Task Update(Company company)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

            var companyModel = mapper.Map<CompanyModel>(company);

            dataContext.Company.Update(companyModel);
            await dataContext.SaveChangesAsync();
        }
    }
}
