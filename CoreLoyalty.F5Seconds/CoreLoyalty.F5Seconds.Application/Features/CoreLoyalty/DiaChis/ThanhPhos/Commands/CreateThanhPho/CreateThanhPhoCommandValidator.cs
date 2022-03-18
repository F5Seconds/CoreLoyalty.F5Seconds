
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using FluentValidation;

namespace CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.CreateThanhPho
{
    public class CreateThanhPhoCommandValidator : AbstractValidator<CreateThanhPhoCommand>
    {
        private readonly IThanhPhoRepositoryAsync _ThanhPhoRepositoryAsync;

        public CreateThanhPhoCommandValidator(IThanhPhoRepositoryAsync ThanhPhoRepositoryAsync)
        {
            _ThanhPhoRepositoryAsync = ThanhPhoRepositoryAsync;
            RuleFor(p => p.Ten)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(p => p.TrangThai)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull();
        }
    }
}
