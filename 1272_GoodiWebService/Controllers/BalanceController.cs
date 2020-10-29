
using GoodiModels_1272.DataModel;
using GoodiModels_1272.RestFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace _1272_GoodiWebService.Controllers
{
    public class BalanceController : ApiController
    {
        // GET: api/Balance
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // POST: api/Balance
        public HttpResponseMessage Post([FromBody]Balance value)
        {
            BalanceResponse balanceResponse = new BalanceResponse(); ;
            if (string.IsNullOrEmpty(value.Token))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Token not exist"); ;
            }
            try
            {
                string errorMessage = "";
                string errorCode = "";

                
                bool result = RestApi.Instance().GetBalance(value, value.Token, out errorMessage, out errorCode, out balanceResponse);
                if (!result)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Exception when try to  get Balance, error message: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                //Error log
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Exception when try to  get Balance, error message: {ex.Message}");
            }
            return Request.CreateResponse(HttpStatusCode.OK, balanceResponse.ToString());
        }

   
    }
}
