﻿using Microsoft.Extensions.DependencyInjection;
using Rye.Enums;
using Rye.Module;
using System;

namespace Rye.EventBus.Application
{
    /// <summary>
    /// 适用于单体应用的事件总线模块，适用于单体应用
    /// </summary>
    public class ApplicationEventBusModule : IStartupModule
    {
        public ModuleLevel Level => ModuleLevel.FrameWork;

        public uint Order => 2;

        private readonly int _bufferSize;

        public ApplicationEventBusModule(int bufferSize)
        {
            _bufferSize = bufferSize;
        }

        public void ConfigueServices(IServiceCollection services)
        {
            services.AddApplicationEventBus(_bufferSize);
        }

        public void Configure(IServiceProvider serviceProvider)
        {
        }
    }
}
