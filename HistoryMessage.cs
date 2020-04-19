using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace binarydotcomanalyzer
{
    public class InnerHistory
    {
        [JsonProperty("prices")]
        public double[] Prices { get; set; }

        [JsonProperty("times")]
        public long[] Times { get; set; }
    }

    class HistoryMessage
    {
        [JsonProperty("history")]
        public InnerHistory History { get; set; }
    }

}
// finish?