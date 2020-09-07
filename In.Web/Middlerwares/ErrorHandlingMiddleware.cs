using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using In.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace In.Web.Middlerwares
{
    /// <summary>
    /// Middleware for handling exceptions.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        /// <summary>
        /// A function that can process an HTTP request.
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the class. 
        /// </summary>
        /// <param name="next">A function that can process an HTTP request.</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Execute current chain requests.
        /// </summary>
        /// <param name="context">Http context.</param>
        public async Task Invoke(HttpContext context)
        {
            var bodyAsText = string.Empty;
            try
            {
                if (context.Request.Headers.TryGetValue("Content-Type", out var headerValues)
                    && headerValues.Count > 0
                    && headerValues[0].Contains("application/json"))
                {
                    using (var injectedRequestStream = new MemoryStream())
                    {
                        using (var bodyReader = new StreamReader(context.Request.Body))
                        {
                            bodyAsText = await bodyReader.ReadToEndAsync();
                            var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                            await injectedRequestStream.WriteAsync(bytesToWrite, 0, bytesToWrite.Length);
                            injectedRequestStream.Seek(0, SeekOrigin.Begin);
                            context.Request.Body = injectedRequestStream;
                        }

                        await _next.Invoke(context);
                    }
                }
                else
                {
                    await _next.Invoke(context);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = GetHttpStatusCode(exception);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int) code;

            return context.Response.WriteAsync(GenerateResponseMessage(exception));
        }

        private HttpStatusCode GetHttpStatusCode(Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case ArgumentNullException _:
                case InvalidOperationException _:
                case BadRequestException _:
                {
                    code = HttpStatusCode.BadRequest;
                    break;
                }
                case UnauthorizedAccessException _:
                case AuthenticationException _:
                case AuthException _:
                {
                    code = HttpStatusCode.Unauthorized;
                    break;
                }
            }

            return code;
        }

        private string GenerateResponseMessage(Exception exception)
        {
            return JsonConvert.SerializeObject(
                new MiddlewareExceptionWrapper(exception.Message),
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
