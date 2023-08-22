﻿using Application.Common.Behaviors;
using Domain.Common.Events;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped(typeof(IRequestPreProcessor<>), typeof(LogginBehavior<>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehavior<,>));

            return services;
        }
    }
}
