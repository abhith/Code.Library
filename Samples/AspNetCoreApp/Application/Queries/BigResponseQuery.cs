using Flurl.Http;
using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreApp.Application.Queries
{
    public class BigResponseQuery : IRequest<string>
    {
    }

    /// <summary>
    /// This is to test the performance impact when we pair with logging behavior and request response logging.
    /// </summary>
    public class BigResponseQueryHandler : IRequestHandler<BigResponseQuery, string>
    {
        public async Task<string> Handle(BigResponseQuery request, CancellationToken cancellationToken)
        {
            var data = await "https://piggyvault.in/swagger/v1/swagger.json".GetJsonAsync();
            return JsonSerializer.Serialize(data);
        }
    }
}