using System.Collections.Generic;
using System.Web.Mvc;
using FizzWare.NBuilder;
using MvcApplication1.Models;
using System.Linq;
using MvcContrib.Pagination;

namespace MvcApplication1.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index(SearchFilter filter, int page = 1)
        {
            //page = page == 0 ?  1 : page;

            const int pageSize = 10;
            var viewModel = new ViewModel();

            var list = Builder<Data>.CreateListOfSize(100).Build();
            //var pagedList = list.Where(x => x.Name.Contains(filter.Name)).AsPagination(pageNumber, pageSize);
            var pagedList = list.AsPagination(page, pageSize);

            viewModel.List = pagedList;
            viewModel.SearchFilter = filter ?? new SearchFilter();
            viewModel.Test = "HAHAHAH";
            if (Request.IsAjaxRequest())
            {
                return PartialView("Partial/PagedGrid", viewModel);
            }

            
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
