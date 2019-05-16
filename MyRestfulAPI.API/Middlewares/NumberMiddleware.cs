using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyRestfulAPI.API.Middlewares
{
    public class NumberMiddleware
    {
        private readonly RequestDelegate _next;

        public NumberMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 中间件中可以拿到请求上下文
        /// </summary>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var value = context.Request.Query["value"].ToString();
            await _next(context);
        }
    }
}
