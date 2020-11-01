
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
        //// GET: api/Balance
        ///// <summary>
        /////  GET: api/Balance
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        // POST: api/Balance
        /// <summary>
        /// POST: api/Balance
        /// Send Balance command to Goodi system
        /// Its mandatory to fill Token field
        /// </summary>
        /// <param name="value">Json with all data Balance need</param>
        /// <returns>OK if succeeded</returns>
        /// <returns>Error code and error message if failed</returns>
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
