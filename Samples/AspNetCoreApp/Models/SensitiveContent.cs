using Destructurama.Attributed;

namespace AspNetCoreApp.Models
{
    public class SensitiveContent
    {
        [LogMasked]
        public string Password { get; set; }
    }
}