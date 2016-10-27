// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Rudolf Kotulán">
//   Copyright © Rudolf Kotulán All Rights Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace KSystem.Seznam.KonverzeZbozi.Extensions
{
    using System.Text;

    public static class StringExtensions
    {
        public static byte[] ToUtf8Bytes(this string value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}