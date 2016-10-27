// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UrlResolver.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Infrastructure
{
    public class UrlResolver
    {
        private const string TestUrl = "https://sandbox.zbozi.cz/action/{0}/conversion/backend";

        private const string Url = "https://www.zbozi.cz/action/{0}/conversion/backend";

        public string Resolve(string shopId, bool useSandbox = false)
        {
            var url = useSandbox ? TestUrl : Url;
            return string.Format(url, shopId);
        }
    }
}