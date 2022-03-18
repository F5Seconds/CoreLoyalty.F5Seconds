using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Exceptions;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.DeleteThanhPho

{
    public class UpdateThanhPhoCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public bool TrangThai { get; set; }
        public class UpdateThanhPhoCommandHandler : IRequestHandler<UpdateThanhPhoCommand, Response<int>>
        {
            private readonly IThanhPhoRepositoryAsync _ThanhPhoRepositoryAsync;
            private readonly IMapper _mapper;
            public UpdateThanhPhoCommandHandler(IThanhPhoRepositoryAsync ThanhPhoRepositoryAsync, IMapper mapper)
            {
                _ThanhPhoRepositoryAsync = ThanhPhoRepositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateThanhPhoCommand command, CancellationToken cancellationToken)
            {
                var ThanhPho = await _ThanhPhoRepositoryAsync.GetByIdAsync(command.Id);
                if (ThanhPho == null)
                {
                    throw new ApiException($"Thành Phố Not Found.");
                }
                else
                {
                    ThanhPho.Ten = command.Ten;
                    ThanhPho.TrangThai = command.TrangThai;
                    await _ThanhPhoRepositoryAsync.UpdateAsync(ThanhPho);
                    return new Response<int>(ThanhPho.Id);
                }
            }
        }
    }
}
