using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Model
{
    public class BitcoinData
    {
        public List<List<object>> prices { get; set; }
        public List<List<object>> market_caps { get; set; }
        public List<List<object>> total_volumes { get; set; }
    }

}


