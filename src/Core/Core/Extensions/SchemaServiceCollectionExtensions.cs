using System;
using Microsoft.Extensions.DependencyInjection;
using HotChocolate.Execution.Configuration;
using HotChocolate.Execution;
using HotChocolate.Execution.Batching;
using System.Linq;
using HotChocolate.Types.Relay;
using System.Collections.Generic;
using HotChocolate.Contracts;

namespace HotChocolate
{
    public static class SchemaServiceCollectionExtensions
    {
        public static IServiceCollection AddGraphQLSchema(
          this IServiceCollection services,
          Action<ISchemaBuilder> build)
        {
            return services.AddGraphQLSchema(string.Empty, build);
        }

        public static IServiceCollection AddGraphQLSchema(
            this IServiceCollection services,
            string name,
            Action<ISchemaBuilder> build)
        {
            return services
                .AddSingleton<INamedSchemaProvider>(sp => new NamedSchemaProvider(() => sp.GetServices<INamedSchema>()))
                .AddSingleton<INamedSchema>(sp =>
                {
                    var builder = SchemaBuilder.New();
                    build(builder);
                    builder.AddServices(sp);
                    return builder.Create().WithName(name);
                }).AddSingleton<IIdSerializer, IdSerializer>();
        }

        public static IServiceCollection AddGraphQLSchema(
            this IServiceCollection services,
            ISchemaBuilder builder)
        {
            return services.AddGraphQLSchema(string.Empty, builder);
        }

        public static IServiceCollection AddGraphQLSchema(
            this IServiceCollection services,
            string name,
            ISchemaBuilder builder)
        {
            return services
                .AddSingleton<INamedSchemaProvider>(sp => new NamedSchemaProvider(() => sp.GetServices<INamedSchema>()))
                .AddSingleton<INamedSchema>(
                sp => builder.AddServices(sp).Create().WithName(name))
                .AddSingleton<IIdSerializer, IdSerializer>();
        }

        public static IServiceCollection AddQueryExecutor(
            this IServiceCollection services)
        {
            QueryExecutionBuilder.BuildDefault(services);
            return services;
        }
        public static IServiceCollection AddQueryExecutor(
            this IServiceCollection services,
            string schemaName)
        {
            QueryExecutionBuilder.BuildDefault(services);
            return services;
        }

        public static IServiceCollection AddQueryExecutor(
            this IServiceCollection services,
            IQueryExecutionOptionsAccessor options)
        {
            QueryExecutionBuilder.BuildDefault(services, options);
            return services;
        }

        public static IServiceCollection AddQueryExecutor(
            this IServiceCollection services,
            Action<IQueryExecutionBuilder> configure)
        {
            var builder = QueryExecutionBuilder.New();
            configure(builder);
            builder.Populate(services);
            return services;
        }

        public static IServiceCollection AddQueryExecutor(
            this IServiceCollection services,
            Action<IQueryExecutionBuilder> configure,
            bool lazyExecutor)
        {
            var builder = QueryExecutionBuilder.New();
            configure(builder);
            builder.Populate(services, lazyExecutor);
            return services;
        }

        public static IServiceCollection AddBatchQueryExecutor(
            this IServiceCollection services,
            string name)
        {
            return services
                .AddSingleton<IBatchQueryExecutorProvider>(sb =>
                {
                    return new BatchQueryExecutorProvider(() => sb.GetServices<INamedBatchQueryExecutor>());
                })
                .AddSingleton<INamedBatchQueryExecutor>(sb =>
                {
                    var providor = sb.GetService<INamedQueryExecutorProvider>();
                    var errorHandler = sb.GetService<IErrorHandler>();
                    var executor = new BatchQueryExecutor(providor.GetQueryExecutor(name), errorHandler);
                    return new NamedBatchQueryExecutor(name, executor);
                });
        }

        public static IServiceCollection AddBatchQueryExecutor(
            this IServiceCollection services)
        {
            return services.AddBatchQueryExecutor(string.Empty);
        }

        public static IServiceCollection AddJsonQueryResultSerializer(
            this IServiceCollection services)
        {
            return services
                .AddQueryResultSerializer<JsonQueryResultSerializer>();
        }

        public static IServiceCollection AddQueryResultSerializer<T>(
            this IServiceCollection services)
            where T : class, IQueryResultSerializer
        {
            return services
                .RemoveService<IQueryResultSerializer>()
                .AddSingleton<IQueryResultSerializer, T>();
        }

        public static IServiceCollection AddQueryResultSerializer<T>(
            this IServiceCollection services,
            Func<IServiceProvider, T> factory)
            where T : IQueryResultSerializer
        {
            return services
                .RemoveService<IQueryResultSerializer>()
                .AddSingleton<IQueryResultSerializer>(sp => factory(sp));
        }

        public static IServiceCollection AddJsonArrayResponseStreamSerializer(
            this IServiceCollection services)
        {
            return services
            .AddResponseStreamSerializer<JsonArrayResponseStreamSerializer>();
        }

        public static IServiceCollection AddResponseStreamSerializer<T>(
            this IServiceCollection services)
            where T : class, IResponseStreamSerializer
        {
            return services
                .RemoveService<IResponseStreamSerializer>()
                .AddSingleton<IResponseStreamSerializer, T>();
        }

        public static IServiceCollection AddResponseStreamSerializer<T>(
            this IServiceCollection services,
            Func<IServiceProvider, T> factory)
            where T : IResponseStreamSerializer
        {
            return services
                .RemoveService<IResponseStreamSerializer>()
                .AddSingleton<IResponseStreamSerializer>(sp => factory(sp));
        }

        public static IServiceCollection AddErrorFilter(
            this IServiceCollection services,
            Func<IError, IError> errorFilter)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (errorFilter == null)
            {
                throw new ArgumentNullException(nameof(errorFilter));
            }

            return services.AddSingleton<IErrorFilter>(
                new FuncErrorFilterWrapper(errorFilter));
        }

        public static IServiceCollection AddErrorFilter(
            this IServiceCollection services,
            Func<IServiceProvider, IErrorFilter> factory)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return services.AddSingleton(factory);
        }

        public static IServiceCollection AddErrorFilter<T>(
            this IServiceCollection services)
            where T : class, IErrorFilter
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IErrorFilter, T>();
        }

        private static IServiceCollection RemoveService<TService>(
            this IServiceCollection services)
        {
            foreach (var serviceDescriptor in services.Where(t =>
                t.ServiceType == typeof(TService)).ToArray())
            {
                services.Remove(serviceDescriptor);
            }
            return services;
        }
    }
}
