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

        public Task<IEnumerable<Company>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetByStatus(CompanyStatus companyStatus)
        {
            throw new NotImplementedException();
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

        public Task Update(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
