﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Raven.Jwt
{
    public enum JwtTokenType : byte
    {
        AccessToken = 1,
        RefreshToken = 2
    }
}
