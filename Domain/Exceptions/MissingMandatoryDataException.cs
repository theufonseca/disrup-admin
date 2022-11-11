using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MissingMandatoryDataException : ExceptionBase
    {
        public MissingMandatoryDataException(List<string> fields)
        {
            Title = "Missing Mandatory Data";
            Detail = $"Fields: {string.Join(',', fields)}";
        }
    }
}
