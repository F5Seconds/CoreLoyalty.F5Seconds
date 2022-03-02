using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories
{
    public class TransactionResFailRepositoryAsync : GenericRepositoryAsync<GotItTransactionResFail>, IGotItTransResFailRepositoryAsync
    {
        private readonly DbSet<GotItTransactionResFail> _transactionResFail;

        public TransactionResFailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _transactionResFail = dbContext.Set<GotItTransactionResFail>();
        }
    }
}
