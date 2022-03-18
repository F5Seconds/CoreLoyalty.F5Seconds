using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;

namespace CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.CreateThanhPho
{
    public class CreateThanhPhoCommand : IRequest<Response<int>>
    {
        public string Ten {get;set;}
        public bool TrangThai {get;set;}

        public class CreateThanhPhoCommandHandler : IRequestHandler<CreateThanhPhoCommand, Response<int>>
        {
            private readonly IThanhPhoRepositoryAsync _ThanhPhoRepositoryAsync;
            private readonly IMapper _mapper;
            public CreateThanhPhoCommandHandler(IThanhPhoRepositoryAsync ThanhPhoRepositoryAsync, IMapper mapper)
            {
                _ThanhPhoRepositoryAsync = ThanhPhoRepositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<int>> Handle(CreateThanhPhoCommand request, CancellationToken cancellationToken)
            {
                var thanhPho = _mapper.Map<ThanhPho>(request);
                await _ThanhPhoRepositoryAsync.AddAsync(thanhPho);
                return new Response<int>(thanhPho.Id);
            }
        }
    }
}
