﻿using Microsoft.AspNetCore.Authorization;

using System;

namespace Monica.Authorization.Abstraction.Attributes
{
    /// <summary>
    /// 限制有权限的用户访问
    /// </summary>
    public class AuthCodeAttribute : AuthorizeAttribute
    {
        public AuthCodeAttribute(string authCode)
        {
            AuthCode = authCode ?? throw new ArgumentNullException(nameof(authCode));
            Policy = "MonicaPermission";
        }

        public string AuthCode { get; }
    }
}
