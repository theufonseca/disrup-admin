using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo
    {
        public int Id { get; init; }
        public int CompanyId { get; set; }
        public string Name { get; init; } = default!;
        public string Url { get; init; } = default!;
        public bool IsThumb { get; private set; } = default!;
        public DateTime CreateDate { get; init; } = default!;

        public void SetIsThumb(bool isThumb) => IsThumb = isThumb;
    }
}