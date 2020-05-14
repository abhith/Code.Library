using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Code.Library.AspNetCore.Middleware
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ProblemDetails> AddResponseDetails { get; set; }

        /// <summary>
        /// NID is the namespace identifier, and may include letters, digits, and -
        /// Ref: https://en.wikipedia.org/wiki/Uniform_Resource_Name
        /// </summary>
        public string Nid { get; set; }
    }
}