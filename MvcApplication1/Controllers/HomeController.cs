using System.Web.Mvc;
using MvcApplication1.Models;
using NHibernate;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Speichern(Test test)
        {
            var isAjaxRequest = HttpContext.Request.IsAjaxRequest();
            var isValid = ModelState.IsValid;
            return View("Index");
        }
    }

    public class NhibernateController : Controller
    {
        public NhibernateController()
        {
             ISessionFactory
        }
    }
}
