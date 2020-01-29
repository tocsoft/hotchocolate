using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using HotChocolate.AspNetCore.Subscriptions;
using System.Threading.Tasks;

namespace HotChocolate.AspNetCore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseGraphQL(
            this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder
                .UseGraphQL(new QueryMiddlewareOptions());
        }

        public static IApplicationBuilder UseGraphQL(
            this IApplicationBuilder applicationBuilder,
            PathString path)
        {
            var options = new QueryMiddlewareOptions
            {
                Path = path.HasValue ? path : new PathString("/")
            };

            return applicationBuilder
                .UseGraphQL(options);
        }

        public static IApplicationBuilder UseGraphQL(
            this IApplicationBuilder applicationBuilder,
            QueryMiddlewareOptions options)
        {
            if (applicationBuilder == null)
            {
                throw new ArgumentNullException(nameof(applicationBuilder));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var stringSchemeName = options.SchemaName ?? string.Empty;
            var schemenameFunction = options.SchemaNameProvider ?? ((o) =>
            {
                return new ValueTask<string>(stringSchemeName);
            });

            applicationBuilder
                .UseGraphQLHttpPost(new HttpPostMiddlewareOptions
                {
                    Path = options.Path,
                    SchemaNameProvider = schemenameFunction,
                    ParserOptions = options.ParserOptions,
                    MaxRequestSize = options.MaxRequestSize
                })
                .UseGraphQLHttpGet(new HttpGetMiddlewareOptions
                {
                    SchemaNameProvider = schemenameFunction,
                    Path = options.Path
                })
                .UseGraphQLHttpGetSchema(new HttpGetSchemaMiddlewareOptions
                {
                    SchemaNameProvider = schemenameFunction,
                    Path = options.Path.Add(new PathString("/schema"))
                });

            //if (options.EnableSubscriptions)
            //{
            //    applicationBuilder.UseGraphQLSubscriptions(
            //        new SubscriptionMiddlewareOptions
            //        {
            //            SchemaNameProvider = schemenameFunction,
            //            ParserOptions = options.ParserOptions,
            //            Path = options.SubscriptionPath
            //        });
            //}

            return applicationBuilder;
        }
    }
}
