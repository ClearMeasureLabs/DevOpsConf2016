using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevOpsConf2016.Extensions
{
    public static class Extensions
    {
        public static string EncodeToSHA1(this string value )
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new System.Text.ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}