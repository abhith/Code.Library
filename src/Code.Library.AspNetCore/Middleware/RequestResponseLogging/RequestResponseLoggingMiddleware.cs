using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code.Library.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Code.Library.AspNetCore.Middleware.RequestResponseLogging
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly RequestResponseLoggingOptions _options;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, RequestResponseLoggingOptions options)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            if (_options.Exclude.Paths.Any(path => context.Request.Path.Value.Contains(path, System.StringComparison.CurrentCultureIgnoreCase)))
            {
                await _next(context);
            }
            else
            {
                await LogRequest(context);
                await LogResponse(context);
            }
        }

        private static string GetRequestHeaders(HttpContext context)
        {
            var builder = new StringBuilder();
            foreach (var header in context.Request.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            return builder.ToString();
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }

        private async Task LogRequest(HttpContext context)
        {
            if (_options.Exclude.RequestBody.Any(path => context.Request.Path.Value.Contains(path, StringComparison.CurrentCultureIgnoreCase)))
            {
                if (_options.Include.RequestHeaders && context.Request.Headers.Count > 0)
                {
                    using (_logger.BeginPropertyScope(("RequestHeaders", GetRequestHeaders(context))))
                    {
                        _logger.LogInformation("----- Handling HTTP Request {RequestUrl} (***)", context.Request.GetDisplayUrl());
                    }
                }
                else
                {
                    _logger.LogInformation("----- Handling HTTP Request {RequestUrl} (***)", context.Request.GetDisplayUrl());
                }
            }
            else
            {
                context.Request.EnableBuffering();
                await using var requestStream = _recyclableMemoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStream);

                if (_options.Include.RequestHeaders && context.Request.Headers.Count > 0)
                {
                    using (_logger.BeginPropertyScope(("RequestHeaders", GetRequestHeaders(context))))
                    {
                        _logger.LogInformation("----- Handling HTTP Request {RequestUrl} ({@RequestBody})", context.Request.GetDisplayUrl(), ReadStreamInChunks(requestStream));
                    }
                }
                else
                {
                    _logger.LogInformation("----- Handling HTTP Request {RequestUrl} ({@RequestBody})", context.Request.GetDisplayUrl(), ReadStreamInChunks(requestStream));
                }

                context.Request.Body.Position = 0;
            }
        }

        private async Task LogResponse(HttpContext context)
        {
            if (_options.Exclude.ResponseBody.Any(path => context.Request.Path.Value.Contains(path, System.StringComparison.CurrentCultureIgnoreCase)))
            {
                await _next(context);
                _logger.LogInformation("----- Handled HTTP Request {RequestUrl} (***)", context.Request.GetDisplayUrl());
            }
            else
            {
                var originalBodyStream = context.Response.Body;

                await using var responseBody = _recyclableMemoryStreamManager.GetStream();
                context.Response.Body = responseBody;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                _logger.LogInformation("----- Handled HTTP Request {RequestUrl} ({@ResponseBody})", context.Request.GetDisplayUrl(), text);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}