
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
    public class ExecuteTransactionController : ApiController
    {
        //// GET: api/ExecuteTransaction
        ///// <summary>
        ///// GET: api/ExecuteTransaction
        ///// </summary>
        ///// <returns></returns>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // POST: api/ExecuteTransaction
        /// <summary>
        ///  POST: api/ExecuteTransaction
        ///  Send Execute Transaction to goodi system
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody]ExecuteTransaction value)
        {
            ExecuteTransactionResponse executeTransactionResponse = new ExecuteTransactionResponse();
            if (string.IsNullOrEmpty(value.Token))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Token not exist"); ;
            }
            try
            {
                string errorMessage = "";
                string errorCode = "";
               
               
                bool result = RestApi.Instance().ExecuteTransaction(value, value.Token, out errorMessage, out errorCode, out executeTransactionResponse);
                if (!result)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Exception when try to Execute Transaction, error message: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                //Error log
                return Request.CreateResponse(HttpStatusCode.InternalServerError, $"Exception when try to Execute Transaction, error message: {ex.Message}");
            }
            return Request.CreateResponse(HttpStatusCode.OK, executeTransactionResponse.ToString());

        }

      
    }
}
