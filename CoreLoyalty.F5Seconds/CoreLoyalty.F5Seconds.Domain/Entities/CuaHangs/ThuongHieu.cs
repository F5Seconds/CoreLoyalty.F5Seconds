using CoreLoyalty.F5Seconds.Domain.Common;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs
{
    public class ThuongHieu : AuditableBaseEntity
    {
        public ThuongHieu()
        {
            LinhVucs = new HashSet<LinhVuc>();
            CuaHangs = new HashSet<CuaHang>();
        }
        public string Ten { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public bool TrangThai { get; set; }
        public int PhuongXaId { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string NguoiDaiDien { get; set; }
        public virtual PhuongXa PhuongXa { get; set; }
        public virtual ICollection<LinhVuc> LinhVucs { get; set; }
        public virtual ICollection<CuaHang> CuaHangs { get; set; }
    }
}
