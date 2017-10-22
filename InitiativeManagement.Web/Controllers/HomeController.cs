using Aspose.Words;
using Aspose.Words.Tables;
using AutoMapper;
using InitiativeManagement.Common;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.App_Start;
using InitiativeManagement.Web.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace InitiativeManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;
        private IProductService _productService;
        private ICommonService _commonService;
        private ApplicationUserManager _userManager;

        public HomeController(IProductCategoryService productCategoryService,
            IProductService productService,
            ICommonService commonService,
            ApplicationUserManager userManager)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
            _userManager = userManager;
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;

            var lastestProductModel = _productService.GetLastest(3);
            var topSaleProductModel = _productService.GetHotProduct(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;

            try
            {
                homeViewModel.Title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
                homeViewModel.MetaKeyword = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
                homeViewModel.MetaDescription = _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
            }
            catch
            {
            }

            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }

        public ActionResult Test()
        {
            //string baseURL = Request.Url.Authority;

            //if (Request.ServerVariables["HTTPS"] == "on")
            //{
            //    baseURL = "https://" + baseURL;
            //}
            //else
            //{
            //    baseURL = "http://" + baseURL;
            //}
            var model = _userManager.Users.ToList();

            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            var headerTemplate = "<h3 align='center'>Danh sách sáng kiến kinh nghiệm cấp tỉnh</h3>";

            builder.InsertHtml(headerTemplate);

            Table table = builder.StartTable();

            foreach (var item in model)
            {
                // Insert a cell
                builder.InsertCell();
                builder.Write(item.FullName);
                // Insert a cell
                builder.InsertCell();
                builder.Write(item.Email);
                // Insert a cell
                builder.InsertCell();
                builder.Write(item.UserName);

                builder.EndRow();
            }

            builder.EndTable();

            var fileName = "danh_sach_de_tai.docx";
            doc.Save(System.Web.HttpContext.Current.Response, fileName, ContentDisposition.Inline, null);

            //string refURL = Request.UrlReferrer.AbsoluteUri;

            //string html = new WebClient().DownloadString(refURL);

            // To make the relative image paths work, base URL must be included in head section
            //html = html.Replace("</head>", string.Format("<base href='{0}'></base></head>", baseURL));

            //MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(html));
            //Document doc = new Document(stream);
            //string fileName = Guid.NewGuid().ToString() + ".doc";
            //doc.Save(System.Web.HttpContext.Current.Response, fileName, ContentDisposition.Inline, null);

            System.Web.HttpContext.Current.Response.End();
            return View();
        }

        private static void CopyHeadersFootersFromPreviousSection(Section section)
        {
            Section previousSection = (Section)section.PreviousSibling;

            if (previousSection == null)
                return;

            section.HeadersFooters.Clear();

            foreach (HeaderFooter headerFooter in previousSection.HeadersFooters)
                section.HeadersFooters.Add(headerFooter.Clone(true));
        }
    }
}