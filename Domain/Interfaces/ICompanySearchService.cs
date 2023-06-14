using Domain.Entities;
using Domain.UseCases.CompanyFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanySearchService
    {
        Task<IEnumerable<Company>> Filter(Filters filters);
    }
}
