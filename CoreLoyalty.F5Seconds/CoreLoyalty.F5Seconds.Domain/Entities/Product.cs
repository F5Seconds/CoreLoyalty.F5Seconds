using CoreLoyalty.F5Seconds.Domain.Common;
using CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs;
using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public Product()
        {
            CuaHangs = new HashSet<CuaHang>();
        }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int Type { get; set; } = 1;
        public int Size { get; set; } = 0;
        public string Partner { get; set; }
        public string BrandName { get; set; }
        public string BrandLogo { get; set; }
        public bool Status { get; set; }
        public virtual ICollection<CuaHang> CuaHangs { get; set; }
    }
}
