﻿using Rye.EventBus.Abstractions;

namespace Rye.EventBus.Redis
{
    public interface IRedisEventBus: IEventBus, IRedisEventPublisher, IRedisEventSubscriber
    {
    }
}
