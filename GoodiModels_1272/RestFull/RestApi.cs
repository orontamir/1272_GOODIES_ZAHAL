using GoodiModels_1272.DataModel;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;

namespace GoodiModels_1272.RestFull
{
    public class RestApi
    {
        private readonly ILog log = LogManager.GetLogger(typeof(RestApi));


        private static RestApi m_instance;

        private RestApi()
        {


        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        /// <returns>The singleton instance</returns>
        public static RestApi Instance()
        {
            if (m_instance == null)
            {
                m_instance = new RestApi();
            }
            return m_instance;
        }

        public bool IsTokenValid(string token)
        {
            string url = ConfigurationManager.AppSettings["ValidateTokenUrl"];
            string apikey = ConfigurationManager.AppSettings["APIKey"];
            string xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <ValidateToken   xmlns=""http://tempuri.org/"" >
                    <ApiKey>{apikey}</ApiKey>
                    <token>{token}</token>
                    </ValidateToken>
                  </soap:Body>
                </soap:Envelope>";
            string ans = POST(url, apikey, xml);
            if (ans.Contains("Error message"))
                return false;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ans);
            if (doc.InnerText == "2000")
            {
                return true;
            }
            log.Debug($"Token number: {token} is not valid or expired");
            return false;

        }

        private string POST(string url, string apiKey, string xml)
        {
            string result;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add(@"SOAP:Action");
                request.Headers.Add("Token", apiKey);
                request.ContentType = "text/xml;charset=\"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                log.Debug($"XML body sending to goodi system: {xml} ");
                soapEnvelopeXml.LoadXml(xml);

                using (Stream stream = request.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                    {
                        result = rd.ReadToEnd();
                        log.Debug($"XML Response: {result} ");
                    }
                }
            }
            catch (Exception ex)
            {
                result = $"Error message: {ex.Message}";
                log.Error(result);
            }
            return result;
        }


        public Token GetNewToken()
        {
            string url = ConfigurationManager.AppSettings["GetNewTokenUrl"];
            string apikey = ConfigurationManager.AppSettings["APIKey"];
            string xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <GetNewToken  xmlns=""http://tempuri.org/"" >
                    <ApiKey>{apikey}</ApiKey>
                    </GetNewToken>
                  </soap:Body>
                </soap:Envelope>";
            string ans = POST(url, apikey, xml);
            if (ans.Contains("Error message"))
                return null;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ans);
            return new Token { TokenNumber = doc.InnerText, Stemp_Tar = DateTime.Now.AddHours(12) };
        }

        public bool ExecuteTransaction(ExecuteTransaction transaction, string token, out string errorMessage, out string errorcode, out ExecuteTransactionResponse executeTransactionResponse)
        {
            errorcode = "";
            errorMessage = "";

            string apikey = token;
            //func-ExecuteTransaction
            string url = ConfigurationManager.AppSettings["ExecuteTransactionUrl"];
            string xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                      <ExecuteTransaction xmlns=""http://tempuri.org/"" >
                      <Price>{transaction.Price}</Price>
                      <Amount>{transaction.Amount}</Amount>
                      <Kod_makor>{transaction.Kod_makor}</Kod_makor>
                      <Kod_station>{transaction.Kod_station}</Kod_station>
                      <Kod_hetken>{transaction.Kod_hetken}</Kod_hetken>
                      <Mispar_hetken>{transaction.Mispar_hetken}</Mispar_hetken>
                      <Tidluk_date>{transaction.Tidluk_date}</Tidluk_date>
                      <Tidluk_time>{transaction.Tidluk_time}</Tidluk_time>
                      <Kod_tazkik>{transaction.Kod_tazkik}</Kod_tazkik>
                      <Station_order>{transaction.Station_order}</Station_order>
                    </ExecuteTransaction>
                  </soap:Body>
                </soap:Envelope>";
            string ans = POST(url, apikey, xml);
            if (ans.Contains("Error message"))
            {
                errorMessage = ans;
                executeTransactionResponse = null;
                return false;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ans);
            executeTransactionResponse = new ExecuteTransactionResponse(doc.InnerText);

            if (executeTransactionResponse.ResponseCode == "2000")
            {
                return true;
            }
            errorcode = doc.InnerText;
            errorMessage = GetErrorMessage(errorcode);
            return false;
        }

        public bool GetBalance(Balance balance, string token, out string errorMessage, out string errorcode, out BalanceResponse balanceResponse)
        {
            errorcode = "";
            errorMessage = "";
            string apikey = token;
            //func-ExecuteTransaction
            string url = ConfigurationManager.AppSettings["GetBalanceUrl"];
            string xml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
                  <soap:Body>
                    <GetBalance xmlns=""http://tempuri.org/"" >
                      <ApiKey>{apikey}</ApiKey>
                      <Carid>{balance.Cardid}</Carid>
                      <Mispar_hetken>{balance.Mispar_hetken}</Mispar_hetken>
                      <Pin_code>{balance.Pin_code}</Pin_code>
                      <Kod_station>{balance.Kod_station}</Kod_station>
                      <Pump_price>{balance.Pump_price}</Pump_price>
                      <Kod_tazkik>{balance.Kod_tazkik}</Kod_tazkik>
                    </GetBalance>
                  </soap:Body>
                </soap:Envelope>";
            string ans = POST(url, apikey, xml);
            if (ans.Contains("Error message"))
            {
                errorMessage = ans;
                balanceResponse = null;
                return false;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ans);
            balanceResponse = new BalanceResponse(doc.InnerText);

            if (balanceResponse.ResponseCode == "2000")
            {
                return true;
            }
            errorcode = doc.InnerText;
            errorMessage = GetErrorMessage(errorcode);
            return false;
        }

        private string GetErrorMessage(string errorcode)
        {
            switch (errorcode)
            {
                case "1000":
                    log.Error("טוקן לא חוקי / לא בתוקף");
                    return "Invalid Token / Token expierd";
                case "1001":
                    log.Error("רכב לא מורשה לתדלק בתחנה זו");
                    return "A vehicle is not authorized to refuel at this station";
                case "1002":
                    log.Error("רכב לא פעיל / חסום");
                    return "Inactive vehicle/ blocked vehicle";
                case "1003":
                    log.Error("קוד סודי שגוי");
                    return "Incorrect secret code";
                case "1004":
                    log.Error("חסר קוד סודי");
                    return "Missing secret code";
                case "1005":
                    log.Error("אין יתרה לשימוש");
                    return "There is no balance to use";
                case "1006":
                    log.Error("לא נמצא הקצאה לטווח שעות זה");
                    return "No assignment was found for this time frame";
                case "1007":
                    log.Error("מספר כרטיס / דלקן – חסום");
                    return "Card / Delken number - blocked";
                case "1008":
                    log.Error("מספר כרטיס / דלקן לא קיים");
                    return "Card / Delken number does not exist";
                case "1009":
                    log.Error("שגיאה לא ידוע");
                    return "Error unknown";
                case "1010":
                    log.Error("אין תקשורת לשרתי גודי ");
                    return "There is no communication to Goodi's servers";
                case "1011":
                    log.Error("סוג תזקיק שגוי");
                    return "Wrong tazkik type";
                case "1012":
                    log.Error("רכב לא ידוע ");
                    return "Unknown vehicle";
                default:
                    log.Error("בעיה לא ידוע בשרת של גודי ");
                    return "Unknown problem with Goody's server";

            }
        }
    }
}