using CoreLoyalty.F5Seconds.Domain.Common;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs
{
    public class CuaHang : AuditableBaseEntity
    {
        public CuaHang()
        {
            ThuongHieus = new HashSet<ThuongHieu>();
            Products = new HashSet<Product>();
        }
        public string Ten { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public int PhuongXaId { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string NguoiDaiDien { get; set; }
        public double KinhDo { get; set; }
        public double ViDo { get; set; }
        public virtual PhuongXa PhuongXa { get; set; }
        public virtual ICollection<ThuongHieu> ThuongHieus { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
