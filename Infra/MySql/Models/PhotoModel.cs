using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.MySql.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool IsThumb { get; set; }
        public DateTime CreateDate { get; set; }

        public CompanyModel Company { get; set; }
    }
}
