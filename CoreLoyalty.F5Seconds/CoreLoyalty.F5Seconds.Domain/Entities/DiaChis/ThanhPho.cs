using CoreLoyalty.F5Seconds.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Domain.Entities.DiaChis
{
    public class ThanhPho : AuditableBaseEntity
    {
        public ThanhPho()
        {
            QuanHuyens = new HashSet<QuanHuyen>();
        }
        public string Ten { get; set; }
        public bool TrangThai { get; set; }
        public virtual ICollection<QuanHuyen> QuanHuyens { get; set; }
    }
}
