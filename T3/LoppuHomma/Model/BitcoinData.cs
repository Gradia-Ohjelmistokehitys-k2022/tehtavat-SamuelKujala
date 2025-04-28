using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoppuHomma.Model
{
    public class BitcoinData
    {
        public List<List<double>> market_caps { get; set; }
        public List<List<double>> prices { get; set; }
        public List<List<double>> total_volumes { get; set; }
    }
}


