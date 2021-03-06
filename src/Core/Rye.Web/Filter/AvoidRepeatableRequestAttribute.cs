﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

using Rye.Web.Options;
using Rye.Web.ResponseProvider.AvoidRepeatableRequestAttr;

using System;
using System.Net;
using System.Threading.Tasks;

namespace Rye.Web.Filter
{
    /// <summary>
    /// 防止重复提交
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AvoidRepeatableRequestAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        public int Order => -10;

        private static IAvoidRepeatableRequestResponseProvider _provider;

        protected static IAvoidRepeatableRequestResponseProvider Provider
        {
            get
            {
                if (_provider != null)
                    return _provider;

                _provider = App.GetRequiredService<IOptions<RyeWebOptions>>().Value.AvoidRepeatableRequest.Provider;
                return _provider;
            }
        }

        public AvoidRepeatableRequestAttribute()
        {
            Seconds = 5;
        }

        public AvoidRepeatableRequestAttribute(int seconds)
        {
            Seconds = seconds;
        }

        /// <summary>
        /// 默认为五秒
        /// </summary>
        public int Seconds { get; set; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var httpContext = context.HttpContext;
            //var action = actionContext.RouteData.Values["action"];
            //var controller = actionContext.RouteData.Values["controller"];
            var key = $"{httpContext.GetRemoteIpAddressToIPv4()}_{httpContext.Request.GetRequestUrlAddress()}";
            IDistributedCache cache = App.GetService<IDistributedCache>();
            if (cache.Exist(key))
            {
                context.Result = Provider.CreateResponse(new AvoidRepeatableRequestContext(httpContext, key));
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else
            {
                cache.Set(key, 1, Seconds);
            }

            await next();
        }
    }
}
