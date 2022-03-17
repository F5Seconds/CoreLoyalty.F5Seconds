using CoreLoyalty.F5Seconds.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.Repositories
{
    public interface IMaLoiRepositoryAsync : IGenericRepositoryAsync<MaLoi>
    {
        Task<MaLoi> FindByMaLoiUrbox(string maUrbox);
        Task<MaLoi> FindByMaLoiGotIt(string maGotIt);
    }
}
