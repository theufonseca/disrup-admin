using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company
    {
        public int? Id { get; init; }
        public DateTime CreateDate { get; private set; }
        public CompanyStatus Status { get; private set; }

        //Legals Info
        public string Document { get; init; } = default!;
        public string LegalName { get; init; } = default!;
        public string FantasyName { get; init; } = default!;
        public string Name { get; init; } = default!;

        //Contact / Instituional Infos
        public string? Site { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? PlaystoreLink { get; init; }
        public string? AppleStoreLink { get; init; }
        public string? LinkedInLink { get; init; }
        public string? InstagramLink { get; init; }
        public string? FacebookLink { get; init; }
        public string? TwitterLink { get; init; }
        public string? YoutubeChannelLink { get; init; }

        //Disrup Infos
        public string? SolvedProblemDescription { get; init; }
        public string? Mission { get; init; }
        public string? Vision { get; init; }
        public string? CompanyValues { get; init; }
        public string? DisrupIdeia { get; init; }
        public string? SocietyContribuition { get; init; }
        public string? WorkEnvironment { get; init; }

        public List<Photo>? Photos { get; private set; }

        public bool HasPhotos() => Photos is not null && Photos.Any();

        public void SetStatus(CompanyStatus status) => this.Status = status;

        public void SetCreateDate() => CreateDate = DateTime.Now;

        public void CopyToUpdate(Company newCompany)
        {
            newCompany.CreateDate = CreateDate;
            newCompany.Status = Status;
        }

        public void AddPhoto(Photo photo)
        {
            if (Photos is null)
                Photos = new();

            Photos.Add(photo);
        }

        public void AddPhotoRange(IEnumerable<Photo> photos)
        {
            if (Photos is null)
                Photos = new();

            Photos.AddRange(photos);
        }

        public void ValidateMandatoryData()
        {
            List<string> mandatoryFielsNotFilled = new();

            if (string.IsNullOrEmpty(Document)) mandatoryFielsNotFilled.Add(nameof(Document));
            if (string.IsNullOrEmpty(LegalName)) mandatoryFielsNotFilled.Add(nameof(LegalName));
            if (string.IsNullOrEmpty(FantasyName)) mandatoryFielsNotFilled.Add(nameof(FantasyName));
            if (string.IsNullOrEmpty(Name)) mandatoryFielsNotFilled.Add(nameof(Name));
            if(mandatoryFielsNotFilled.Any()) throw new MissingMandatoryDataException(mandatoryFielsNotFilled);
        }

        public void ValidatePatternData()
        {
            ValidatePhone();
        }

        private void ValidatePhone()
        {
            if (string.IsNullOrEmpty(Phone))
                return;

            var expectedLength = new int[] { 13, 14 };

            if (!expectedLength.Any(x => x == Phone.Length))
                throw new PhoneFormatException(Phone, Phone.Length, expectedLength.ToList());
        }

        
    }
}
