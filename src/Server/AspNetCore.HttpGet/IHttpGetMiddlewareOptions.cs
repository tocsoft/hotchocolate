#if ASPNETCLASSIC
using Microsoft.Owin;
using HttpContext = Microsoft.Owin.IOwinContext;
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
    public interface IHttpGetMiddlewareOptions
        : IPathOptionAccessor
    {
        Func<HttpContext, ValueTask<string>> SchemaNameProvider { get; }
    }
}
