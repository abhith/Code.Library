using System;
using System.Collections.Generic;
using System.Text;

namespace Code.Library.AspNetCore.Middlewares
{
    public class ApiError
    {
        public string Code { get; set; }
        public string Detail { get; set; }
        public string Id { get; internal set; }
        public string Links { get; set; }
        public ApiErrorSource Source { get; set; }
        public short Status { get; internal set; }
        public string Title { get; internal set; }
    }

    public class ApiErrorSource
    {
        public string JsonPointer { get; set; }
        public string Parameter { get; set; }
    }
}