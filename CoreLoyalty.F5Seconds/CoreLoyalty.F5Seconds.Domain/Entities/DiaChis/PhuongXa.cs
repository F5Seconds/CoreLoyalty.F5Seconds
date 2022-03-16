using CoreLoyalty.F5Seconds.Domain.Common;

namespace CoreLoyalty.F5Seconds.Domain.Entities.DiaChis
{
    public class PhuongXa : AuditableBaseEntity
    {
        public string Ten { get; set; }
        public string TenDayDu { get; set; }
        public bool TrangThai { get; set; }
        public int QuanHuyenId { get; set; }
        public virtual QuanHuyen QuanHuyen { get; set; }
    }
}
