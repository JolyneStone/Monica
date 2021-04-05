﻿using System;

namespace Rye.Jwt
{
    /// <summary>
    /// JwtToken模型
    /// </summary>
    public class JsonWebToken
    {
        /// <summary>
        /// 用于业务身份认证的AccessToken
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// AccessToken有效期，UTC标准
        /// </summary>
        public long AccessExpires { get; set; }

        /// <summary>
        /// 用于刷新AccessToken的RefreshToken
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// RefreshToken有效期，UTC标准
        /// </summary>
        public long RefreshExpires { get; set; }

        /// <summary>
        /// AssccToken是否过期
        /// </summary>
        public bool IsAccessExpired()
        {
            var now = DateTimeOffset.UtcNow;
            long nowTick = now.Ticks; //now.ToJsGetTime().ParseByLong();
            return AccessExpires > nowTick;
        }

        /// <summary>
        /// RefreshToken是否过期
        /// </summary>
        public bool IsRefreshExpired()
        {
            var now = DateTimeOffset.UtcNow;
            long nowTick = now.Ticks; //now.ToJsGetTime().ParseByLong();
            return RefreshExpires > nowTick;
        }
    }
}
