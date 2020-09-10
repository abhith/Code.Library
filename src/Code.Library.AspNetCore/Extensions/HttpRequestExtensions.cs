using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Code.Library.AspNetCore.Extensions
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// Return true if HTTP request contain Prefer header with value 'return=representation'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool HasPreferHeaderWithReturnRepresentation(this HttpRequest request) => request.Headers.TryGetValue("Prefer", out var header) && header.Any(h => IsReturnRepresentation(h));

        private static bool IsReturnRepresentation(string value) => value.Split(';').Any(v => v.Equals("return=representation"));
    }
}