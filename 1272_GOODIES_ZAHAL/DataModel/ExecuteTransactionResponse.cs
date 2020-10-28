using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1272_GOODIES_ZAHAL.DataModel
{
    public class ExecuteTransactionResponse
    {
        public string ResponseCode { get; set; }
        public string Balance { get; set; }
        public string orderId { get; set; }
        public ExecuteTransactionResponse(string line)
        {
            var response = line.Split('|');
            if (response.Length == 3)
            {
                ResponseCode = response[0];
                Balance = response[1];
                orderId = response[2];
            }
            else
            {
                ResponseCode = line;
            }
        }
    }
}
