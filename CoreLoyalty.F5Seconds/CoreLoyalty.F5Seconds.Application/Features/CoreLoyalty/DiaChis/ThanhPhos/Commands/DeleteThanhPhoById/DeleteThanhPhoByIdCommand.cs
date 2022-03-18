using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Exceptions;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
namespace CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.DeleteThanhPho
{
    public class DeleteThanhPhoByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteThanhPhoByIdCommandHandler : IRequestHandler<DeleteThanhPhoByIdCommand, Response<int>>
        {
            private readonly IThanhPhoRepositoryAsync _ThanhPhoRepositoryAsync;
            private readonly IMapper _mapper;
            public DeleteThanhPhoByIdCommandHandler(IThanhPhoRepositoryAsync ThanhPhoRepositoryAsync, IMapper mapper)
            {
                _ThanhPhoRepositoryAsync = ThanhPhoRepositoryAsync;
                _mapper = mapper;
            }
            public async Task<Response<int>> Handle(DeleteThanhPhoByIdCommand command, CancellationToken cancellationToken)
            {
                var ThanhPho = await _ThanhPhoRepositoryAsync.GetByIdAsync(command.Id);
                if (ThanhPho == null) throw new ApiException($"ThanhPho Not Found.");
                await _ThanhPhoRepositoryAsync.DeleteAsync(ThanhPho);
                return new Response<int>(ThanhPho.Id);
            }
        }
    }
}
