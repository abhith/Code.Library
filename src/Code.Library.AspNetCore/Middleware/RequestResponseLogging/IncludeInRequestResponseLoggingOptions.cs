namespace Code.Library.AspNetCore.Middleware.RequestResponseLogging
{
    public class IncludeInRequestResponseLoggingOptions
    {
        /// <summary>
        /// Set this to true to enable request headers logging
        /// </summary>
        public bool RequestHeaders { get; set; } = false;
    }
}