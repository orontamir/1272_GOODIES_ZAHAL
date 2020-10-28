using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1272_GOODIES_ZAHAL.DataModel
{
    public class BalanceResponse
    {
        public string ResponseCode { get; set; }
        public string Kod_tazkik { get; set; }
        public string Balance { get; set; }
        public BalanceResponse(string line)
        {
            var response = line.Split('|');
            if (response.Length == 3)
            {
                ResponseCode = response[0];
                Kod_tazkik = response[1];
                Balance = response[2];
            }
            else
            {
                ResponseCode = line;
            }
        }
    }
}
