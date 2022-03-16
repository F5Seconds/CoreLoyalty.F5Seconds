using CoreLoyalty.F5Seconds.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs
{
    public class LinhVuc : AuditableBaseEntity
    {
        public LinhVuc()
        {
            ThuongHieus = new HashSet<ThuongHieu>();
        }
        public string Ten { get; set; }
        public bool TrangThai { get; set; }
        public virtual ICollection<ThuongHieu> ThuongHieus { get; set; }
    }
}
