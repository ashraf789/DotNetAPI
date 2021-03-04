using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace API.Middleware
{
    public static class AuthenticationMiddleware
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {
            var secret = ConfigHelpers.JWT_SECRET;
            var key = Encoding.ASCII.GetBytes(secret);

            //services.AddAuthentication(x =>
            //{ 
            //}).AddJwtBearer()

            return services;
        }
    }
}