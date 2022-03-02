using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox
{
    public class UrboxTransReqRepositoryAsync : GenericRepositoryAsync<UrboxTransactionRequest>, IUrboxTransReqRepositoryAsync
    {
        private readonly DbSet<UrboxTransactionRequest> _urboxTransactionRequests;
        public UrboxTransReqRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _urboxTransactionRequests = dbContext.Set<UrboxTransactionRequest>();
        }
    }
}
