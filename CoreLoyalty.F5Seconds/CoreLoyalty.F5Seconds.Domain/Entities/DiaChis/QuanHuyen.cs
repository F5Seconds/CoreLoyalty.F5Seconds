using CoreLoyalty.F5Seconds.Domain.Common;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Domain.Entities.DiaChis
{
    public class QuanHuyen : AuditableBaseEntity
    {
        public QuanHuyen()
        {
            PhuongXas = new HashSet<PhuongXa>();
        }
        public string Ten { get; set; }
        public string TenDayDu { get; set; }
        public bool TrangThai { get; set; }
        public int ThanhPhoId { get; set; }
        public virtual ThanhPho ThanhPho { get; set; }
        public virtual ICollection<PhuongXa> PhuongXas { get; set; }
    }
}
