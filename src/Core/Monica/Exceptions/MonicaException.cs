﻿using System;
using System.Runtime.Serialization;

namespace Monica.Exceptions
{
    /// <summary>
    /// Monica 内部异常类
    /// </summary>
    [Serializable]
    public class MonicaException : Exception
    {
        /// <summary>
        /// 初始化<see cref="MonicaException"/>类的新实例
        /// </summary>
        public MonicaException()
        { }

        /// <summary>
        /// 使用指定错误消息初始化<see cref="MonicaException"/>类的新实例。
        /// </summary>
        /// <param name="message">描述错误的消息</param>
        public MonicaException(string message)
            : base(message)
        { }

        /// <summary>
        /// 使用异常消息与一个内部异常实例化一个<see cref="MonicaException"/>类的新实例
        /// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="inner">用于封装在<see cref="MonicaException"/>内部的异常实例</param>
        public MonicaException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// 使用可序列化数据实例化一个<see cref="MonicaException"/>类的新实例
        /// </summary>
        /// <param name="info">保存序列化对象数据的对象。</param>
        /// <param name="context">有关源或目标的上下文信息。</param>
        protected MonicaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}