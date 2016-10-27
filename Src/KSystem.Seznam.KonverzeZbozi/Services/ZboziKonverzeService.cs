// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZboziKonverzeService.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Services
{
    using System.IO;
    using System.Net;
    using System.Text;

    using KSystem.Seznam.KonverzeZbozi.Entities;
    using KSystem.Seznam.KonverzeZbozi.Infrastructure;

    using Newtonsoft.Json;

    public class ZboziKonverzeService
    {
        private readonly string shopId;

        private readonly UrlResolver urlResolver;

        private readonly JsonSerializerSettings jsonSerializerSettings;

        /// <summary>
        ///     Service pro odesláí objednávky na zbozi.cz pro měření konverzí.
        /// </summary>
        /// <param name="shopId">
        ///     ID provozovny, získáte v administraci vaší provozovny, případně na testovacím Sandboxu. Toto ID se využívá ve
        ///     frontend (JavaScript) i backend kódu.
        /// </param>
        public ZboziKonverzeService(string shopId)
        {
            this.shopId = shopId;
            this.urlResolver = new UrlResolver();
            this.jsonSerializerSettings = new JsonSerializerSettings
                                              {
                                                  ContractResolver = new CamelCaseContractResolver()
                                              };
        }

        public ZboziSendResponse Send(ZboziOrder zboziOrder)
        {
            var json = this.GetJsonString(zboziOrder);
            var postBytes = Encoding.UTF8.GetBytes(json);

            var request = this.GetWebRequest(zboziOrder, postBytes.Length);
            this.WritePostDataToRequest(request, postBytes);

            var result = GetStringResultFromRequest(request);
            return JsonConvert.DeserializeObject<ZboziSendResponse>(result, this.jsonSerializerSettings);
        }

        private static string GetStringResultFromRequest(HttpWebRequest request)
        {
            var response = (HttpWebResponse)request.GetResponse();
            string result;
            using (var rdr = new StreamReader(response.GetResponseStream()))
            {
                result = rdr.ReadToEnd();
            }
            return result;
        }

        private string GetJsonString(ZboziOrder zboziOrder)
        {
            return JsonConvert.SerializeObject(zboziOrder, Formatting.Indented, this.jsonSerializerSettings);
        }

        private HttpWebRequest GetWebRequest(ZboziOrder zboziOrder, long contentLength)
        {
            var request = WebRequest.CreateHttp(this.urlResolver.Resolve(this.shopId, zboziOrder.Sandbox));
            request.Method = "POST";

            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = contentLength;
            return request;
        }

        private void WritePostDataToRequest(HttpWebRequest request, byte[] postBytes)
        {
            var requestStream = request.GetRequestStream();
            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
        }
    }
}