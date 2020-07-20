namespace Code.Library.AspNetCore.Middleware.RequestResponseLogging
{
    public class RequestResponseLoggingOptions
    {
        /// <summary>
        /// Excluded areas from logging
        /// </summary>
        public ExcludeInRequestResponseLoggingOptions Exclude { get; set; } = new ExcludeInRequestResponseLoggingOptions();

        public IncludeInRequestResponseLoggingOptions Include { get; set; } = new IncludeInRequestResponseLoggingOptions();
    }
}