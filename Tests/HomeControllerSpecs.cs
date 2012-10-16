using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using Machine.Specifications;
using MvcApplication1;
using MvcApplication1.Controllers;
using MvcApplication1.Models;
using SpecsFor.Mvc;


namespace Tests
{
    public class SmokerTester
    {
        readonly MvcWebApp _mvcWebApp;

        public SmokerTester(MvcWebApp mvcWebApp)
        {
            _mvcWebApp = mvcWebApp;
        }

        public void Execute(Assembly assemblyToScanForControllerTypes)
        {
            var controllerTypes = assemblyToScanForControllerTypes.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)).ToList();
            foreach (var controllerType in controllerTypes)
            {
                var methodInfos = controllerType.GetMethods().Where(type => typeof(ActionResult).IsAssignableFrom(type.ReturnType));
                foreach (var methodInfo in methodInfos)
                {
                    var parameterTypes = methodInfo.GetParameters().Select(i => i.ParameterType);
                    var controllerActionCallExpression = Helper.CreateCallExpressionForMVCController(methodInfo, parameterTypes);

                    var method = typeof(MvcWebApp).GetMethod("NavigateTo").MakeGenericMethod(controllerType);
                    

                    //try
                    //{
                        //_mvcWebApp.NavigateTo<HomeController>(x=>x.Index());
                        method.Invoke(_mvcWebApp, new object[] { controllerActionCallExpression });
                        var allText = _mvcWebApp.AllText();
                        allText.ShouldNotContain("Server Error in");
                    //}
                    //catch (Exception exception)
                    //{
                    //    var controllerName = controllerType.Name;
                    //    var actionName = methodInfo.Name;
                    //    var message = String.Format("There was an error when invoking controller '{0}' with action '{1}'. See inner exception", controllerName, actionName);

                    //    throw new Exception(message, exception);
                    //}
                }
            }
        }
    }

    [Subject(typeof(HomeController))]
    public class When_smoke_testing_all_sites
    {

        static MvcWebApp SUT;
        static SmokerTester SmokerTester;
        Establish context = () =>
        {
            SUT = new MvcWebApp();
            SmokerTester = new SmokerTester(SUT);
        };

        Because of = () => SmokerTester.Execute(typeof(HomeController).Assembly);

        It should_be_no_errors = () => true.ShouldBeTrue();
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
        public static LambdaExpression CreateCallExpressionForMVCController(MethodInfo methodInfo, IEnumerable<Type> parameters)
        {
            var controllerType = methodInfo.DeclaringType;
            if (controllerType == null)
                throw new InvalidOperationException(string.Format("Could not determine controller type for method {0}", methodInfo.Name));

            var tArgs = new List<Type> { controllerType, typeof(void) };

            var delegateType = Expression.GetDelegateType(tArgs.ToArray());

            var parameter = Expression.Parameter(controllerType, "x");

            var expression = Expression.Lambda(delegateType, Expression.Call(parameter, methodInfo, parameters.Select(Expression.Default)), parameter);

            return expression;
        }

        public static Expression<Action> CreateCallExpression(Type type, MethodInfo methodInfo, IEnumerable<Type> parameters)
        {
            var parameter = Expression.Parameter(type, "x");
            return Expression.Lambda<Action>(
                Expression.Call(parameter, methodInfo, parameters.Select(Expression.Default)), parameter);
        }

        public static Expression<Action<T>> CreateCallExpression<T>(MethodInfo method, IEnumerable<Type> parameters) where T : Controller
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            return Expression.Lambda<Action<T>>(
                Expression.Call(parameter, method, parameters.Select(Expression.Default)), parameter);
        }
    }
}
