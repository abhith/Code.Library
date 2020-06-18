using System.Collections.Generic;

namespace Code.Library.AspNetCore.Middleware
{
    public class ExcludePathsInRequestResponseLoggingOptions
    {
        public ICollection<string> RequestBody { get; set; } = new List<string>();
        public ICollection<string> ResponseBody { get; set; } = new List<string>();
    }

    public class RequestResponseLoggingOptions
    {
        public bool ExcludeHealthChecks { get; set; } = true;
        public ExcludePathsInRequestResponseLoggingOptions ExcludePaths { get; set; } = new ExcludePathsInRequestResponseLoggingOptions();
    }
}