using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.MySql.Models
{
    public class CompanyModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public CompanyStatus Status { get; set; }
        public string Document { get; set; }
        public string LegalName { get; set; }
        public string FantasyName { get; set; }
        public string Name { get; set; }
        public string? Site { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? PlaystoreLink { get; set; }
        public string? AppleStoreLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? TwitterLink { get; set; }
        public string? YoutubeChannelLink { get; set; }
        public string? SolvedProblemDescription { get; set; }
        public string? Mission { get; set; }
        public string? Vision { get; set; }
        public string? CompanyValues { get; set; }
        public string? DisrupIdeia { get; set; }
        public string? SocietyContribuition { get; set; }
        public string? WorkEnvironment { get; set; }
        public string? Category1 { get; init; }
        public string? Category2 { get; init; }
        public string? Category3 { get; init; }
        public string? Category4 { get; init; }
        public string? Category5 { get; init; }

        public ICollection<PhotoModel> Photos { get; set; }
    }
}
