using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyService
    {
        Task<int> Insert(Company company);
        Task Update(Company company);
        Task Delete(int id);
        Task<IEnumerable<Company>> GetAll();
        Task<IEnumerable<Company>> GetByStatus(CompanyStatus companyStatus);
        Task<Company> GetById(int id);
        Task<bool> Exists(string document);
    }
}
