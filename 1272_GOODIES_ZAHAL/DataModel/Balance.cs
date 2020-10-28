using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1272_GOODIES_ZAHAL.DataModel
{
    public class Balance
    {
        public int Cardid { get; set; }
        public double Mispar_hetken { get; set; }
        public double Pin_code { get; set; }
        public int Kod_station { get; set; }
        public int Pump_price { get; set; }
        public int Kod_tazkik { get; set; }
        public string ERROR_MESSAGE { get; set; }
    }
}
