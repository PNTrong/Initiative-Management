using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.Models;
using System.Web.Mvc;

namespace InitiativeManagement.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }

        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var model = Mapper.Map<Page, PageViewModel>(page);
            return View(model);
        }
    }
}