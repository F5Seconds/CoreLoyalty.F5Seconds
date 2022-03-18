using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.CreateThanhPho;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetAllThanhPhos;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using VietCapital.Partner.F5Seconds.Application.Features.ThanhPhos.Queries.GetAllThanhPhos;

namespace CoreLoyalty.F5Seconds.Application.Mappings
{
    public class GeneralProfile: Profile
    {
        public GeneralProfile()
        {
            #region ThanhPho
            CreateMap<ThanhPho, GetAllThanhPhosViewModel>().ReverseMap();
            CreateMap<CreateThanhPhoCommand,ThanhPho>();
            CreateMap<GetAllThanhPhosQuery, GetAllThanhPhosParameter>();

            #endregion

        }
        
    }
}
