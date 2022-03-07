using System.Collections.Generic;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.Const
{
    public static class ChannelConst
    {
        public static Dictionary<string,ChannelProperty> DictionaryChannel = new Dictionary<string, ChannelProperty>()
        {
            {
                "VIETCAPITAL",new ChannelProperty()
                {
                    Name = "VIETCAPITAL",
                    Queue = "vietCapitalStateQueue"
                } 
            }
        };
    }

    public class ChannelProperty
    {
        public string Name { get; set; }
        public string Queue { get; set; }
    }
}
