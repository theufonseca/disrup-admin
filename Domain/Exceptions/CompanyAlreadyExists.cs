using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CompanyAlreadyExists : ExceptionBase
    {
        public CompanyAlreadyExists(string document)
        {
            Title = "Company with the same document already exists";
            Detail = $"The company with document '{document}' already exists";
        }
    }
}
