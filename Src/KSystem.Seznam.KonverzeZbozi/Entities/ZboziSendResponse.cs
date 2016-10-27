// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZboziSendResponse.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Entities
{
    public class ZboziSendResponse
    {
        public int ApiVersion { get; set; }

        public string ImpressionData { get; set; }

        public int Status { get; set; }

        public string StatusMessage { get; set; }
    }
}