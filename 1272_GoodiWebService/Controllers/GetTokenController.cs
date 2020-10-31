
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
    public class GetTokenController : ApiController
    {
        // GET: api/GetToken
        /// <summary>
        /// GET: api/GetToken
        /// Get Token from Goodi system
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            try
            {
                Token token = GoodiModels_1272.DataBase.DBParser.Instance().GetToken();
                if (token == null || token.Stemp_Tar < DateTime.Now || !RestApi.Instance().IsTokenValid(token.TokenNumber))
                {
                    token = RestApi.Instance().GetNewToken();
                    if (token != null)
                    {
                        GoodiModels_1272.DataBase.DBParser.Instance().UpdateToken(token);
                        return token.TokenNumber;
                    }
                    return null;
                }
                return token.TokenNumber;
            }
            catch (Exception ex)
            {
                //insert log error
                return null;
            }
        }


        // POST: api/GetToken
        /// <summary>
        /// POST: api/GetToken
        /// </summary>
        /// <param name="value"></param>
        public void Post([FromBody]Token value)
        {
        }

      
    }
}
