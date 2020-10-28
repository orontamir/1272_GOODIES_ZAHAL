using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1272_GOODIES_ZAHAL.DataModel
{
    public class ExecuteTransaction
    {
        public int ID { get; set; }
        public double PRICE { get; set; }
        public double AMOUNT { get; set; }
        public int KOD_MAKOR { get; set; }
        public int KOD_STATION { get; set; }
        public int KOD_HETKEN { get; set; }
        public string MISPAR_HETKEN { get; set; }
        public string TIDLUK_DATE { get; set; }
        public string TIDLUK_TIME { get; set; }
        public int KOD_TAZKIK { get; set; }
        public string STATION_ORDER { get; set; }
        public string ERROR_MESSAGE { get; set; }
        

       


    }
}
