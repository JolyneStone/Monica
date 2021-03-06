﻿using System;

namespace Rye.AspectFlare.DynamicProxy
{
    public interface IProxyCollection
    {
        void Add(Type interfaceType, Type classType, Type proxyType);
        Type GetProxyType(Type interfaceType, Type classType);
    }
}
