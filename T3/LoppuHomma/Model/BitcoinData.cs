using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Model
{
    public class BitcoinData
    {
        public List<List<object>> Prices { get; set; }
        public List<List<object>> Market_caps { get; set; }
        public List<List<object>> Total_volumes { get; set; }
    }

}


