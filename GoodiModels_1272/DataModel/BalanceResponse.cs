using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoodiModels_1272.DataModel
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


        public BalanceResponse()
        {

        }
        public override string ToString()
        {
            JObject json = new JObject(
                       new JProperty("ResponseCode", ResponseCode),
                       new JProperty("Kod_tazkik", Kod_tazkik),
                       new JProperty("Balance", Balance)
                       );
            return json.ToString();
        }
    }
}