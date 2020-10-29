using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodiModels_1272.DataModel
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
        public ExecuteTransactionResponse()
        {

        }
        public override string ToString()
        {
            JObject json = new JObject(
                       new JProperty("ResponseCode", ResponseCode),
                       new JProperty("Balance", Balance),
                       new JProperty("orderId", orderId)
                       );
            return json.ToString();
        }
    }
}