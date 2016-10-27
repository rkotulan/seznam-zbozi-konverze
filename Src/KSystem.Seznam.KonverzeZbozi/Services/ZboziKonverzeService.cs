// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZboziKonverzeService.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Services
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    using KSystem.Seznam.KonverzeZbozi.Entities;
    using KSystem.Seznam.KonverzeZbozi.Extensions;
    using KSystem.Seznam.KonverzeZbozi.Infrastructure;

    using Newtonsoft.Json;

    public class ZboziKonverzeService
    {
        private readonly JsonSerializerSettings jsonSerializerSettings;

        private readonly string shopId;

        private readonly UrlResolver urlResolver;

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
            this.jsonSerializerSettings = new JsonSerializerSettings();
            this.jsonSerializerSettings.ContractResolver = new CamelCaseContractResolver();
        }

        public async Task<ZboziSendResponse> SendAsync(ZboziOrder zboziOrder)
        {
            var postBytes = this.GetJsonString(zboziOrder).ToUtf8Bytes();

            var request = this.GetWebRequest(zboziOrder);
            await this.WritePostDataToRequest(request, postBytes);
            var result = await this.GetStringResultFromRequest(request);

            return JsonConvert.DeserializeObject<ZboziSendResponse>(result, this.jsonSerializerSettings);
        }

        private string GetJsonString(ZboziOrder zboziOrder)
        {
            return JsonConvert.SerializeObject(zboziOrder, Formatting.Indented, this.jsonSerializerSettings);
        }

        private async Task<string> GetStringResultFromRequest(WebRequest request)
        {
            var response = await request.GetResponseAsync();
            if (response != null)
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return await reader.ReadToEndAsync();
                }
            }

            return string.Empty;
        }

        private HttpWebRequest GetWebRequest(ZboziOrder zboziOrder)
        {
            var request = WebRequest.CreateHttp(this.urlResolver.Resolve(this.shopId, zboziOrder.Sandbox));
            request.Method = "POST";
            request.ContentType = "application/json";

            return request;
        }

        private async Task WritePostDataToRequest(WebRequest request, byte[] postBytes)
        {
            using (var postStream = await request.GetRequestStreamAsync())
            {
                await postStream.WriteAsync(postBytes, 0, postBytes.Length);
            }
        }
    }
}