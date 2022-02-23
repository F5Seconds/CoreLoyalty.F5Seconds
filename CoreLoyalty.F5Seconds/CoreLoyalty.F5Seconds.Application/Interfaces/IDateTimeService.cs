using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
