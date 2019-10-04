﻿using System;

namespace Kira.AlasFx.Domain
{
    /// <summary>
    /// 无主键实体接口
    /// </summary>
    public interface IEntity
    {
    }

    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity<TKey> : IEntity where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// 主键, 如果使用EF Core, 请将该属性设置为计算列
        /// </summary>
        TKey Key { get; set; }
    }
}