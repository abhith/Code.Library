using Destructurama.Attributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApp.Models
{
    public class SensitiveContent
    {
        [LogMasked]
        public string Password { get; set; }
    }
}