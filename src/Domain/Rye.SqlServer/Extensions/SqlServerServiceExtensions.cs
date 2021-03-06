﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using System;

namespace Rye.SqlServer
{
    public static class SqlServerServiceExtensions
    {
        /// <summary>
        /// 增加Sql Server模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="providerType"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerModule(this IServiceCollection services, Type providerType)
        {
            if (providerType is null)
            {
                throw new ArgumentNullException(nameof(providerType));
            }

            var module = new SqlServerModule(providerType);
            return services.AddModule<SqlServerModule>(module);
        }

        /// <summary>
        /// 增加Sql Server模块
        /// </summary>
        /// <param name="services"></param>
        /// <param name="providerFunc"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerModule(this IServiceCollection services, Func<IServiceProvider, object> providerFunc)
        {
            if (providerFunc is null)
            {
                throw new ArgumentNullException(nameof(providerFunc));
            }

            var module = new SqlServerModule(providerFunc);
            return services.AddModule<SqlServerModule>(module);
        }

        /// <summary>
        /// 增加Sql Server模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerModule<T>(this IServiceCollection services)
            where T: SqlServerConnectionProvider
        {
            var module = new SqlServerModule(typeof(T));
            return services.AddModule<SqlServerModule>(module);
        }

        /// <summary>
        /// 增加Sql Server模块
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="providerFunc"></param>
        /// <returns></returns>
        public static IServiceCollection AddSqlServerModule<T>(this IServiceCollection services, Func<IServiceProvider, T> providerFunc)
        {
            if (providerFunc is null)
            {
                throw new ArgumentNullException(nameof(providerFunc));
            }

            object func(IServiceProvider serviceProvider) => (object)providerFunc(serviceProvider);
            var module = new SqlServerModule(func);
            return services.AddModule<SqlServerModule>(module);
        }

        public static IServiceCollection AddSqlServerDbConnectionProvider<T>(this IServiceCollection services)
            where T : SqlServerConnectionProvider
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAddSingleton<SqlServerConnectionProvider, T>();
            return services;
        }

        public static IServiceCollection AddSqlServerDbConnectionProvider<T>(this IServiceCollection services, Func<IServiceProvider, T> providerFunc)
            where T : SqlServerConnectionProvider
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (providerFunc is null)
            {
                throw new ArgumentNullException(nameof(providerFunc));
            }
            services.TryAddSingleton<SqlServerConnectionProvider>(providerFunc);
            return services;
        }

        public static IServiceCollection AddSqlServerDbConnectionProvider(this IServiceCollection services, Type providerType)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (providerType is null)
            {
                throw new ArgumentNullException(nameof(providerType));
            }

            services.TryAddSingleton(typeof(SqlServerConnectionProvider), providerType);
            return services;
        }

        public static IServiceCollection AddSqlServerDbConnectionProvider(this IServiceCollection services, Func<IServiceProvider, object> providerFunc)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (providerFunc is null)
            {
                throw new ArgumentNullException(nameof(providerFunc));
            }

            services.TryAddSingleton(typeof(SqlServerConnectionProvider), providerFunc);
            return services;
        }

        //public static IServiceCollection AddSqlServerInternalService(this IServiceCollection serviceCollection)
        //{
        //    if (serviceCollection is null)
        //    {
        //        throw new ArgumentNullException(nameof(serviceCollection));
        //    }

        //    serviceCollection.RemoveAll<IAppInfoService>();
        //    serviceCollection.TryAddSingleton<IAppInfoService, SqlServerAppInfoService>();
        //    return serviceCollection;
        //}

        //public static IServiceCollection AddSqlServerPersmission<TPermissionKey>(this IServiceCollection serviceCollection)
        //    where TPermissionKey: IEquatable<TPermissionKey>
        //{
        //    serviceCollection.RemoveAll<IPermissionService<TPermissionKey>>();
        //    serviceCollection.TryAddSingleton<IPermissionService<TPermissionKey>, SqlServerPermissionService<TPermissionKey>>();
        //    return serviceCollection;
        //}

        //public static IServiceCollection AddSqlServerDbConnectionProvider<T>(this IServiceCollection services, ServiceLifetime serviceLifetime)
        //    where T : SqlServerConnectionProvider
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    services.TryAdd(new ServiceDescriptor(typeof(SqlServerConnectionProvider), typeof(T), serviceLifetime));
        //    return services;
        //}

        //public static IServiceCollection AddSqlServerDbConnectionProvider<T>(this IServiceCollection services, Func<IServiceProvider, T> providerFunc, ServiceLifetime serviceLifetime)
        //    where T : SqlServerConnectionProvider
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    if (providerFunc is null)
        //    {
        //        throw new ArgumentNullException(nameof(providerFunc));
        //    }
        //    services.TryAdd(new ServiceDescriptor(typeof(SqlServerConnectionProvider), providerFunc, serviceLifetime));
        //    return services;
        //}

        //public static IServiceCollection AddSqlServerDbConnectionProvider(this IServiceCollection services, Type providerType, ServiceLifetime serviceLifetime)
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    if (providerType is null)
        //    {
        //        throw new ArgumentNullException(nameof(providerType));
        //    }

        //    services.TryAdd(new ServiceDescriptor(typeof(SqlServerConnectionProvider), providerType, serviceLifetime));
        //    return services;
        //}

        //public static IServiceCollection AddSqlServerDbConnectionProvider(this IServiceCollection services, Func<IServiceProvider, object> providerFunc, ServiceLifetime serviceLifetime)
        //{
        //    if (services is null)
        //    {
        //        throw new ArgumentNullException(nameof(services));
        //    }

        //    if (providerFunc is null)
        //    {
        //        throw new ArgumentNullException(nameof(providerFunc));
        //    }

        //    services.TryAdd(new ServiceDescriptor(typeof(SqlServerConnectionProvider), providerFunc, serviceLifetime));
        //    return services;
        //}
    }
}
