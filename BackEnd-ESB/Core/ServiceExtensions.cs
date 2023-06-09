﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
       }
    }
}
