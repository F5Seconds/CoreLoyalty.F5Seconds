using CoreLoyalty.F5Seconds.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Domain.Entities
{
    public class MaLoi : AuditableBaseEntity
    {
        public string MaF5s { get; set; }
        public string MoTaF5s { get; set; }
        public string MaUrbox { get; set; }
        public string MoTaUrbox { get; set; }
        public string MaGotIt { get; set; }
        public string MoTaGotIt { get; set; }
    }
}
