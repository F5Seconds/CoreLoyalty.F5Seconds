using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.ChannelUpdateState.Interfaces
{
    public interface IVietCapitalHttpClientService
    {
        Task PostVietCapitalStateUpdate(ChannelUpdateStateDto payload);
    }
}
