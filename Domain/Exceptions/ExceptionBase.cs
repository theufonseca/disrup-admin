using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ExceptionBase : Exception
    {
        public string Title { get; init; }
        public string Detail { get; init; }

        public ExceptionBase(string title = "Unexpected error occurs", string detail = "Unexpected error occurs", 
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            Title = title;
            Detail = detail;
        }
    }
}
