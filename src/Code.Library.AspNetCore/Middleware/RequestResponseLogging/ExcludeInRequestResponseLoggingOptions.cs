using System.Collections.Generic;

namespace Code.Library.AspNetCore.Middleware.RequestResponseLogging
{
    public class ExcludeInRequestResponseLoggingOptions
    {
        /// <summary>
        /// This will exclude logging for all endpoints with endpoint name "Health checks". This is enabled by default.
        /// </summary>
        public bool HealthChecks { get; set; } = true;

        /// <summary>
        /// No logs for matching paths
        /// </summary>
        public ICollection<string> Paths { get; set; } = new List<string>() { "/hc", "/liveness" };

        /// <summary>
        /// Request body will not be logged for matching paths
        /// </summary>
        public ICollection<string> RequestBody { get; set; } = new List<string>();

        /// <summary>
        /// Response body will not be logged for matching paths
        /// </summary>
        public ICollection<string> ResponseBody { get; set; } = new List<string>();
    }
}