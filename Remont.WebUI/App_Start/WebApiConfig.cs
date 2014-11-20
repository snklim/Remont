using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Remont.Common.Model;
using Remont.Common.Repository;
using Remont.DAL;
using Remont.WebUI.Infrastructure;

namespace Remont.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

            container.RegisterType<IRepository<Customer, int>, EntityRepository<Customer, int>>();
            container.RegisterType<IRepository<Order, int>, EntityRepository<Order, int>>();
            container.RegisterType<IRepository<OrderStatus, int>, EntityRepository<OrderStatus, int>>();
            container.RegisterType<IRepository<Table, int>, EntityRepository<Table, int>>();
            container.RegisterType<IRepository<Column, int>, EntityRepository<Column, int>>();
	        container.RegisterType<IRepository<Row, int>, TableSpecificRepository<Row>>();
			container.RegisterType<IRepository<Cell, int>, EntityRepository<Cell, int>>();

            config.DependencyResolver = new UnityResolver(container);

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
