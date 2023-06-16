using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.UseCases.CompanyFilter
{
    public class Filters
    {
        public Filters()
        {
            Categories = new List<string>();
        }

        public string? Name { get; set; }
        public List<string> Categories { get; set; }
    }
}