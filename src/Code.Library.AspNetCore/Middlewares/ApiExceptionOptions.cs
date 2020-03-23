using Microsoft.AspNetCore.Http;
using System;

namespace Code.Library.AspNetCore.Middlewares
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}