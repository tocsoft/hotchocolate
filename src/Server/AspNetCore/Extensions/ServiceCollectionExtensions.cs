using System;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution.Configuration;
using HotChocolate.Execution;
using HotChocolate.Configuration;
using HotChocolate.Server;
using HotChocolate.Execution.Batching;
using HotChocolate.Types.Relay;
using System.Collections.Generic;
using HotChocolate.Contracts;
#if ASPNETCLASSIC
using HotChocolate.AspNetClassic.Interceptors;
using HttpContext = Microsoft.Owin.IOwinContext;
#else
using HotChocolate.AspNetCore.Subscriptions;
using HotChocolate.AspNetCore.Interceptors;
using Microsoft.AspNetCore.Http;
#endif

namespace HotChocolate
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            ISchemaBuilder schemaBuilder)
        {
            return services
                .AddGraphQLWithName(string.Empty, schemaBuilder);
        }

        public static IServiceCollection AddGraphQLWithName(this IServiceCollection services,
            string schemaName,
            ISchemaBuilder schemaBuilder)
        {
            return services
                .AddGraphQLSchema(schemaName, schemaBuilder)
#if !ASPNETCLASSIC
                //.AddGraphQLSubscriptions()
#endif
                .AddJsonSerializer()
                .AddQueryExecutor(schemaName)
                .AddBatchQueryExecutor(schemaName);
        }


        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            ISchema schema)
        {
            return services.AddGraphQLWithName(string.Empty, schema);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            ISchema schema)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName);
            return services.AddSchema(schemaName, schema)
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            ISchema schema,
            Action<IQueryExecutionBuilder> build)
        {
            return services.AddGraphQLWithName(string.Empty, schema, build);
        }

        public static IServiceCollection AddGraphQLWithName(
           this IServiceCollection services,
           string schemaName,
           ISchema schema,
           Action<IQueryExecutionBuilder> build)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }

            QueryExecutionBuilder builder = QueryExecutionBuilder.New();
            build(builder);
            builder.Populate(schemaName, services);

            return services.AddSchema(schemaName, schema)
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
           this IServiceCollection services,
           Func<IServiceProvider, ISchema> schemaFactory)
        {
            return services.AddGraphQLWithName(string.Empty, schemaFactory);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            Func<IServiceProvider, ISchema> schemaFactory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (schemaFactory == null)
            {
                throw new ArgumentNullException(nameof(schemaFactory));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName);
            return services.AddSchema(schemaName, schemaFactory)
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            Func<IServiceProvider, ISchema> schemaFactory,
            Action<IQueryExecutionBuilder> build)
        {
            return services.AddGraphQLWithName(string.Empty, schemaFactory, build);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            Func<IServiceProvider, ISchema> schemaFactory,
            Action<IQueryExecutionBuilder> build)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (schemaFactory == null)
            {
                throw new ArgumentNullException(nameof(schemaFactory));
            }

            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }

            QueryExecutionBuilder builder = QueryExecutionBuilder.New();
            build(builder);
            builder.Populate(schemaName, services);

            return services.AddSchema(schemaName, schemaFactory)
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            Action<ISchemaConfiguration> configure)
        {
            return services.AddGraphQL(string.Empty, configure);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            Action<ISchemaConfiguration> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName);
            return services.AddSchema(schemaName, s => Schema.Create(c =>
            {
                c.RegisterServiceProvider(s);
                configure(c);
            }))
            .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            Action<ISchemaConfiguration> configure,
            Action<IQueryExecutionBuilder> build)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }

            QueryExecutionBuilder builder = QueryExecutionBuilder.New();
            build(builder);
            builder.Populate(services);

            return services.AddGraphQLWithName(string.Empty, configure, build);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            Action<ISchemaConfiguration> configure,
            Action<IQueryExecutionBuilder> build)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }

            QueryExecutionBuilder builder = QueryExecutionBuilder.New();
            build(builder);
            builder.Populate(schemaName, services);

            return services.AddSchema(schemaName, s => Schema.Create(c =>
            {
                c.RegisterServiceProvider(s);
                configure(c);
            }))
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            string schemaSource,
            Action<ISchemaConfiguration> configure)
        {
            return services.AddGraphQLWithName(string.Empty, schemaSource, configure);
        }

        public static IServiceCollection AddGraphQLWithName(
           this IServiceCollection services,
            string schemaName,
           string schemaSource,
           Action<ISchemaConfiguration> configure)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(schemaSource))
            {
                throw new ArgumentNullException(nameof(schemaSource));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName);

            return services.AddSchema(schemaName, s =>
                Schema.Create(schemaSource, c =>
                {
                    c.RegisterServiceProvider(s);
                    configure(c);
                }))
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            string schemaSource,
            Action<ISchemaConfiguration> configure,
            Action<IQueryExecutionBuilder> build)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(schemaSource))
            {
                throw new ArgumentNullException(nameof(schemaSource));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }

            QueryExecutionBuilder builder = QueryExecutionBuilder.New();
            build(builder);
            builder.Populate(services);

            return services.AddGraphQLWithName(string.Empty, schemaSource, configure, build);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            string schemaSource,
            Action<ISchemaConfiguration> configure,
            Action<IQueryExecutionBuilder> build)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(schemaSource))
            {
                throw new ArgumentNullException(nameof(schemaSource));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            if (build == null)
            {
                throw new ArgumentNullException(nameof(build));
            }

            QueryExecutionBuilder builder = QueryExecutionBuilder.New();
            build(builder);
            builder.Populate(schemaName, services);

            return services.AddSchema(schemaName, s =>
                Schema.Create(schemaSource, c =>
                {
                    c.RegisterServiceProvider(s);
                    configure(c);
                }))
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            ISchema schema,
            IQueryExecutionOptionsAccessor options)
        {
            return services.AddGraphQLWithName(string.Empty, schema, options);
        }

        public static IServiceCollection AddGraphQLWithName(
           this IServiceCollection services,
           string schemaName,
           ISchema schema,
           IQueryExecutionOptionsAccessor options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (schema == null)
            {
                throw new ArgumentNullException(nameof(schema));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName, options);
            return services.AddSchema(schemaName, schema)
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            Func<IServiceProvider, ISchema> schemaFactory,
            IQueryExecutionOptionsAccessor options)
        {
            return services.AddGraphQLWitName(string.Empty, schemaFactory, options);
        }

        public static IServiceCollection AddGraphQLWitName(
            this IServiceCollection services,
            string schemaName,
            Func<IServiceProvider, ISchema> schemaFactory,
            IQueryExecutionOptionsAccessor options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (schemaFactory == null)
            {
                throw new ArgumentNullException(nameof(schemaFactory));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName, options);
            return services.AddSchema(schemaName, schemaFactory)
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            Action<ISchemaConfiguration> configure,
            IQueryExecutionOptionsAccessor options)
        {
            return services.AddGraphQLWithName(string.Empty, configure, options);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            Action<ISchemaConfiguration> configure,
            IQueryExecutionOptionsAccessor options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName, options);

            return services.AddSchema(schemaName, s =>
                Schema.Create(c =>
                {
                    c.RegisterServiceProvider(s);
                    configure(c);
                }))
                .AddBatchQueryExecutor(schemaName);
        }

        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            string schemaSource,
            Action<ISchemaConfiguration> configure,
            IQueryExecutionOptionsAccessor options)
        {
            return services.AddGraphQLWithName(string.Empty, schemaSource, configure, options);
        }

        public static IServiceCollection AddGraphQLWithName(
            this IServiceCollection services,
            string schemaName,
            string schemaSource,
            Action<ISchemaConfiguration> configure,
            IQueryExecutionOptionsAccessor options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (string.IsNullOrEmpty(schemaSource))
            {
                throw new ArgumentNullException(nameof(schemaSource));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            QueryExecutionBuilder.BuildDefault(services, schemaName, options);

            return services.AddSchema(schemaName, s =>
                Schema.Create(schemaSource, c =>
                {
                    c.RegisterServiceProvider(s);
                    configure(c);
                }))
                .AddBatchQueryExecutor(schemaName);
        }

        [Obsolete("Use different overload.", true)]
        public static IServiceCollection AddGraphQL(
            this IServiceCollection services,
            IQueryExecutor executor)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            return services
                .AddSingleton<INamedQueryExecutorProvider>(sp => new NamedQueryExecutorProvider(() => sp.GetServices<INamedQueryExecutor>()))
                .AddSingleton(new NamedQueryExecutor(string.Empty, executor))
                .AddSingleton(s =>
                    new NamedSchema(string.Empty, s.GetRequiredService<IQueryExecutor>().Schema))
                .AddJsonSerializer();
        }

#if !ASPNETCLASSIC

        public static IServiceCollection AddWebSocketConnectionInterceptor(
            this IServiceCollection services,
            OnConnectWebSocketAsync interceptor)
        {
            return services
                .AddSingleton<ISocketConnectionInterceptor<HttpContext>>(
                    new SocketConnectionDelegateInterceptor(interceptor));
        }
#endif

        public static IServiceCollection AddQueryRequestInterceptor(
            this IServiceCollection services,
            OnCreateRequestAsync interceptor)
        {
            return services
                .AddSingleton<IQueryRequestInterceptor<HttpContext>>(
                    new QueryRequestDelegateInterceptor(interceptor));
        }

        private static IServiceCollection AddSchema(
            this IServiceCollection services,
            ISchema schema)
        {
            return AddSchema(services, string.Empty, sp => schema);
        }

        private static IServiceCollection AddSchema(
            this IServiceCollection services,
            Func<IServiceProvider, ISchema> factory)
        {
            return services.AddSchema(string.Empty, factory);
        }

        private static IServiceCollection AddSchema(
          this IServiceCollection services,
          string schemaName,
          ISchema schema)
        {
            return AddSchema(services, sp => schema);
        }

        private static IServiceCollection AddSchema(
            this IServiceCollection services,
            string schemaName,
            Func<IServiceProvider, ISchema> factory)
        {
            services.AddSingleton<IIdSerializer, IdSerializer>();
            services.AddSingleton<INamedSchemaProvider>(sp => new NamedSchemaProvider(() => sp.GetServices<INamedSchema>()));
            services.AddSingleton<INamedSchema>(sp => new NamedSchema(schemaName, factory(sp)));
            services.AddJsonSerializer();
#if !ASPNETCLASSIC
            //  services.AddGraphQLSubscriptions();
#endif
            return services;
        }

        private static IServiceCollection AddJsonSerializer(
            this IServiceCollection services)
        {
            return services
                .AddJsonQueryResultSerializer()
                .AddJsonArrayResponseStreamSerializer();
        }
    }
}
