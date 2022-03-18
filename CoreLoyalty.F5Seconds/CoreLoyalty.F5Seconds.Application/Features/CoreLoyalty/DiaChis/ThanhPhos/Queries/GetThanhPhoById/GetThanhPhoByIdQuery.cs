using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Exceptions;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetThanhPhoById
{
    public class GetThanhPhoByIdQuery : IRequest<Response<ThanhPho>>
    {
        public int Id { get; set; }
    }
    public class GetThanhPhoByIdQueryHandler : IRequestHandler<GetThanhPhoByIdQuery, Response<ThanhPho>>
    {
        private readonly IThanhPhoRepositoryAsync _ThanhPhoRepositoryAsync;
        private readonly IMapper _mapper;
        public GetThanhPhoByIdQueryHandler(IThanhPhoRepositoryAsync ThanhPhoRepositoryAsync, IMapper mapper)
        {
            _ThanhPhoRepositoryAsync = ThanhPhoRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<ThanhPho>> Handle(GetThanhPhoByIdQuery request, CancellationToken cancellationToken)
        {
            var ThanhPho = await _ThanhPhoRepositoryAsync.GetThanhPhoByIdAsync(request.Id);
            if (ThanhPho == null)  throw new ApiException($"Thành phố Not Found.");
            return new Response<ThanhPho>(ThanhPho);
        }
    }
}
