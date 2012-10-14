using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using Machine.Specifications;
using MvcApplication1;
using MvcApplication1.Controllers;
using MvcApplication1.Models;
using SpecsFor.Mvc;


namespace Tests
{
    [Subject(typeof(HomeController))]
    public class When_navigating_to_Home_About
    {

        static MvcWebApp SUT;

        Establish context = () =>
        {
            SUT = new MvcWebApp();
        };

        Because of = () =>
        {
            var methodInfo = typeof (HomeController).GetMethod("Index");
            var parameterInfos = methodInfo.GetParameters();

            var callExpression = Helper.CreateCallExpression<HomeController>(methodInfo, parameterInfos
                    .Select(parameterInfo => Expression.Parameter(parameterInfo.ParameterType,  parameterInfo.Name)).ToArray());

            SUT.NavigateTo(callExpression);
        };

        It should_show_some_content = () => SUT.AllText().ShouldNotBeEmpty();
    }

    [Subject(typeof(HomeController))]
    public class When_navigating_to_Home_Index
    {
        static MvcWebApp SUT;

        Establish context = () =>
        {
            SUT = new MvcWebApp();
        };

        Because of = () => SUT.NavigateTo<HomeController>(x => x.Index(new SearchFilter(), 1));

        It should_show_some_content = () => SUT.AllText().ShouldNotBeEmpty();
    }

    public class BaseContext : IAssemblyContext
    {
        static SpecsForIntegrationHost _host;

        public void OnAssemblyStart()
        {
            var config = new SpecsForMvcConfig();
            config.UseIISExpress()
                  .With(Project.Named("MvcApplication1"));

            config.BuildRoutesUsing(RouteConfig.RegisterRoutes);
            config.UseBrowser(BrowserDriver.InternetExplorer);
            _host = new SpecsForIntegrationHost(config);
            _host.Start();
        }

        public void OnAssemblyComplete()
        {
            _host.Shutdown();
        }
    }

    public static class Helper
    {
        public static Expression<Action<T>> CreateCallExpression<T>(MethodInfo method, ParameterExpression[] arguments)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            return Expression.Lambda<Action<T>>(
                Expression.Call(parameter, method, arguments), parameter);
        }
 
    }
}
