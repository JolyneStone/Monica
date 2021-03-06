﻿using System;
using System.Globalization;
using System.Reflection;

namespace Rye.AspectFlare.DynamicProxy
{
    public class ProxyProvider : IProxyProvider
    {
        public ProxyProvider(IProxyConfiguration configuration,
            IProxyCollection collection,
            IProxyTypeGenerator typeGenerator,
            IProxyValidator validator)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
            _generator = typeGenerator ?? throw new ArgumentNullException(nameof(typeGenerator));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        private static readonly object _sync = new object();
        private readonly IProxyConfiguration _configuration;
        private readonly IProxyCollection _collection;
        private readonly IProxyTypeGenerator _generator;
        private readonly IProxyValidator _validator;

        public bool TryGetProxyType(Type classType, out Type proxyType)
        {
            proxyType = null;
            try
            {
                proxyType = GetProxyType(classType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryGetProxyType(Type interfaceType, Type classType, out Type proxyType)
        {
            proxyType = null;
            try
            {
                proxyType = GetProxyType(interfaceType, classType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Type GetProxyType(Type classType)
        {
            if (classType == null)
            {
                throw new ArgumentNullException(nameof(classType));
            }

            if (!_validator.Validate(classType))
            {
                throw new ArgumentException($"{classType.FullName} is an illegal type");
            }

            var proxyType = _collection.GetProxyType(null, classType);
            if (proxyType == null)
            {
                lock (_sync)
                {
                    proxyType = _collection.GetProxyType(null, classType);
                    if (proxyType == null)
                    {
                        proxyType = _generator.GenerateProxyByClass(classType);
                    }

                    _collection.Add(null, classType, proxyType);
                }
            }

            return proxyType;
        }

        public Type GetProxyType(Type interfaceType, Type classType)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (classType == null)
            {
                throw new ArgumentNullException(nameof(classType));
            }

            if (!_validator.Validate(interfaceType, classType))
            {
                throw new ArgumentException($"{interfaceType.FullName} or {classType.FullName} are an illegal type");
            }

            var proxyType = _collection.GetProxyType(interfaceType, classType);
            if (proxyType == null)
            {
                lock (_sync)
                {
                    proxyType = _collection.GetProxyType(interfaceType, classType);
                    if (proxyType == null)
                    {
                        proxyType = _generator.GenerateProxyByInterface(interfaceType, classType);
                    }

                    _collection.Add(interfaceType, classType, proxyType);
                }
            }

            return proxyType;
        }

        public object GetProxy(Type classType, params object[] parameters)
        {
            var proxyType = GetProxyType(classType);
            return _configuration.ProxyAssemblyBuilder.CreateInstance(
                proxyType.FullName,
                false,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                parameters,
                CultureInfo.CurrentCulture,
                null
            );
        }

        public object GetProxy(Type interfaceType, Type classType, params object[] parameters)
        {
            var proxyType = GetProxyType(interfaceType, classType);
            return _configuration.ProxyAssemblyBuilder.CreateInstance(
                proxyType.FullName,
                false,
                BindingFlags.Public | BindingFlags.Instance,
                null,
                parameters,
                CultureInfo.CurrentCulture,
                null
            );
        }
    }
}
