﻿using BrokerListService.Helpers.Interface;
using BrokerListService.Helpers;
using BrokerListService.Models;
using BrokerListService.Service.Interface;
using BrokerListService.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BrokerListService.OpenAPIClients;
using BrokerListService.Repositories.Interface;
using BrokerListService.Repositories;

namespace BrokerListService.Extensions
{
    public static class BrokerListServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IBrokerListGenrateService, BrokerListGenerateService>();
            services.AddHttpClient<IBrokerClient, BrokerClient>();
            services.AddScoped<IHeadquarterBrokerRepository, HeadquarterBrokerRepository>();
            services.AddScoped<IBranchBrokerRepository, BranchBrokerRepository>();
            services.AddScoped<IBrokerService, BrokerService>();
            services.AddScoped<IBrokerHelper, BrokerHelper>();
            return services;
        }
    }
}
