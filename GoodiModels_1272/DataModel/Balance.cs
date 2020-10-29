using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodiModels_1272.DataModel
{
    public class Balance : BaseModel
    {
        public int Cardid { get; set; }
        public double Mispar_hetken { get; set; }
        public double Pin_code { get; set; }
        public int Kod_station { get; set; }
        public int Pump_price { get; set; }
        public int Kod_tazkik { get; set; }

    }
}