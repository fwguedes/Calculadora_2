using Calculadora.ConfigurationUtility;
using Calculadora.Interfaces;
using CalculadoraAPI.Interfaces;
using CalculadoraAPI.Manager;
using CalculadoraAPI.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CalculadoraAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            Calculadora.Dependency.DependencyInjector.Register<ICalculatorAPIManager, CalculatorAPIManager>();
            Calculadora.Dependency.DependencyInjector.Register<ICalculatorAPIProcessor, CalculatorAPIProcessor>();
            Calculadora.Dependency.DependencyInjector.Register<IRabbitAPIProcessor, RabbitAPIProcessor>();
            Calculadora.Dependency.DependencyInjector.Register<ICalculatorConfiguration, CalculatorConfiguration>();
            
        }
    }
}
