using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetAllThanhPhos;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VietCapital.Partner.F5Seconds.Domain.Const;

namespace VietCapital.Partner.F5Seconds.Application.Features.ThanhPhos.Queries.GetAllThanhPhos
{
    public class GetAllThanhPhosQuery : IRequest<Response<object>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string Search { get; set; } = "";
    }
    public class GetAllThanhPhosQueryHandler : IRequestHandler<GetAllThanhPhosQuery, Response<object>>
    {
        private readonly IThanhPhoRepositoryAsync _ThanhPhoRepositoryAsync;
        private readonly IMapper _mapper;
        public GetAllThanhPhosQueryHandler(IThanhPhoRepositoryAsync ThanhPhoRepositoryAsync, IMapper mapper)
        {
            _ThanhPhoRepositoryAsync = ThanhPhoRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<object>> Handle(GetAllThanhPhosQuery request, CancellationToken cancellationToken)
        {
            var filter = _mapper.Map<GetAllThanhPhosParameter>(request);
            var ThanhPhos = await _ThanhPhoRepositoryAsync.GetAllPagedListAsync(filter);
            if (ThanhPhos == null) return new Response<object>(false, null, ResponseConst.NotData);
            return new Response<object>(true, new
            {
                ThanhPhos.CurrentPage,
                ThanhPhos.TotalPages,
                ThanhPhos.PageSize,
                ThanhPhos.TotalCount,
                ThanhPhos.HasPrevious,
                ThanhPhos.HasNext,
                Data = _mapper.Map<ICollection<ThanhPho>>(ThanhPhos),
            });
        }
    }
}
