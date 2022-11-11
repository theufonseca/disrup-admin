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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; init; }
        public int CompanyId { get; set; }
        public string Name { get; init; } = default!;
        public string Url { get; init; } = default!;
        public bool IsThumb { get; init; } = default!;
        public DateTime CreateDate { get; init; } = default!;
    }
}