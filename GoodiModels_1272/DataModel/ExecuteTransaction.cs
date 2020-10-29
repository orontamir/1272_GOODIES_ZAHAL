using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodiModels_1272.DataModel
{
    public class ExecuteTransaction : BaseModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public int Kod_makor { get; set; }
        public int Kod_station { get; set; }
        public int Kod_hetken { get; set; }
        public string Mispar_hetken { get; set; }
        public string Tidluk_date { get; set; }
        public string Tidluk_time { get; set; }
        public int Kod_tazkik { get; set; }
        public string Station_order { get; set; }






    }
}