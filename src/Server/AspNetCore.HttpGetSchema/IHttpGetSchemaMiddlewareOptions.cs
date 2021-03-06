#if ASPNETCLASSIC
using Microsoft.Owin;
using HttpContext = Microsoft.Owin.IOwinContext;
using HttpResponse = Microsoft.Owin.IOwinResponse;
using RequestDelegate = Microsoft.Owin.OwinMiddleware;
#else
using Microsoft.AspNetCore.Http;
#endif

#if ASPNETCLASSIC
namespace HotChocolate.AspNetClassic
#else
using System;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore
#endif
{
    public interface IHttpGetSchemaMiddlewareOptions
        : IPathOptionAccessor
    {
        Func<HttpContext, ValueTask<string>> SchemaNameProvider { get; }
    }
}
