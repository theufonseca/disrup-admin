using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.RabbitMQ.Services
{
    public class CompanyNotificationService : ICompanyNotificationService
    {
        public Task NotifyCompanyChanges(CompanyNotificationStatus status, int companyId)
        {
            return Task.CompletedTask;
            //throw new NotImplementedException();
        }
    }
}
