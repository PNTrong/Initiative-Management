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

namespace InitiativeManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        //    private IProductCategoryService _productCategoryService;
        //    private IProductService _productService;
        //    private ICommonService _commonService;
        //    private IInitiativeService _initiativeService;
        //    private ApplicationUserManager _userManager;

        //    public HomeController(IProductCategoryService productCategoryService,
        //        IProductService productService,
        //        ICommonService commonService,
        //         IInitiativeService initiativeService,
        //        ApplicationUserManager userManager)
        //    {
        //        _productCategoryService = productCategoryService;
        //        _commonService = commonService;
        //        _productService = productService;
        //        _userManager = userManager;
        //        this._initiativeService = initiativeService;
        //    }

        //    [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        //    public ActionResult Index()
        //    {
        //        var slideModel = _commonService.GetSlides();
        //        var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
        //        var homeViewModel = new HomeViewModel();
        //        homeViewModel.Slides = slideView;

        //        var lastestProductModel = _productService.GetLastest(3);
        //        var topSaleProductModel = _productService.GetHotProduct(3);
        //        var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
        //        var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
        //        homeViewModel.LastestProducts = lastestProductViewModel;
        //        homeViewModel.TopSaleProducts = topSaleProductViewModel;

        //        try
        //        {
        //            homeViewModel.Title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
        //            homeViewModel.MetaKeyword = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
        //            homeViewModel.MetaDescription = _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
        //        }
        //        catch
        //        {
        //        }

        //        return View(homeViewModel);
        //    }

        //    [ChildActionOnly]
        //    [OutputCache(Duration = 3600)]
        //    public ActionResult Footer()
        //    {
        //        var footerModel = _commonService.GetFooter();
        //        var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
        //        return PartialView(footerViewModel);
        //    }

        //    [ChildActionOnly]
        //    public ActionResult Header()
        //    {
        //        return PartialView();
        //    }

        //    [ChildActionOnly]
        //    [OutputCache(Duration = 3600)]
        //    public ActionResult Category()
        //    {
        //        var model = _productCategoryService.GetAll();
        //        var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
        //        return PartialView(listProductCategoryViewModel);
        //    }

        //    public string Test()
        //    {
        //        var initiatives = _initiativeService.GetMulti();

        //        Document doc = new Document();
        //        DocumentBuilder builder = new DocumentBuilder(doc);

        //        builder.PageSetup.Orientation = Orientation.Landscape;
        //        Font font = builder.Font;
        //        font.Size = 13;
        //        font.Bold = true;
        //        font.Name = "Arial";

        //        var headerTemplate = "<p style='text-align:left;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UBND TỈNH QUẢNG NAM&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
        //        " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
        //        "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</strong></p>" +
        //        "<p style='text-align:left;'><strong><span style='text-decoration:underline;'> SỞ KHOA HỌC VÀ CÔNG NGHỆ</span>" +
        //        " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
        //        " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
        //        "<span style='text-decoration:underline;'> Độc lập -Tự do -Hạnh phúc </ span ></ strong ></p></br></br>" +
        //        "<p style='text-align:center;'><strong> DANH MỤC SÁNG KIẾN THUỘC LĨNH VỰC QUẢN LÝ VÀ HOẠT ĐỘNG ĐOÀN ĐỘI</strong></p></br>";

        //        builder.InsertHtml(headerTemplate);

        //        Table table = builder.StartTable();

        //        builder.InsertCell();
        //        builder.Write("TT");
        //        // Insert a cell
        //        builder.InsertCell();
        //        builder.Write("Tên sáng kiến");
        //        // Insert a cell
        //        builder.InsertCell();
        //        builder.Write("Mô tả sáng kiến");
        //        // Insert a cell
        //        builder.InsertCell();
        //        builder.Write("Ý kiến tổ thẩm định");

        //        builder.InsertCell();
        //        builder.Write("Điểm trung bình Tổ thẩm định");

        //        builder.EndRow();

        //        var count = 1;
        //        font.Bold = false;

        //        foreach (var initiative in initiatives)
        //        {
        //            // Insert a cell
        //            builder.InsertCell();
        //            builder.Write(count.ToString());
        //            // Insert a cell
        //            builder.InsertCell();
        //            builder.Write(initiative.Title);
        //            // Insert a cell
        //            builder.InsertCell();
        //            builder.InsertHtml(GetInitiativeInfo(initiative));
        //            // Insert a cell
        //            builder.InsertCell();
        //            builder.Write("");

        //            builder.InsertCell();
        //            builder.Write("");

        //            builder.EndRow();

        //            count++;
        //        }

        //        builder.EndTable();

        //        var fileName = "danh_sach_de_tai.docx";

        //        doc.Save(System.Web.HttpContext.Current.Response, fileName, ContentDisposition.Inline, null);

        //        System.Web.HttpContext.Current.Response.End();
        //        return "";
        //    }

        //    private string GetInitiativeInfo(Initiative initiative)
        //    {
        //        var info = "<p><strong>1. Thông tin chung:</strong></p>" +
        //           "<p><strong>- Ngày sáng kiến được áp dụng lần đầu: " + initiative.DeploymentTime + "</strong></p>" +
        //           "<p><strong>2. Bản chất của sáng kiến</strong></p>" +
        //           "<p>2.1. Tình trạng của giải pháp đã biết</p>" +
        //           "<p>" + initiative.KnowSolutionContent + "</p>" +
        //           "<p>2.2. Nội dung giải pháp đề nghị công nhận là sáng kiến</p>" +
        //           "<p>" + initiative.ImprovedContent + "</p>" +
        //           "<p>2.3. Khả năng áp dụng</p>" +
        //           "<p>" + initiative.InitiativeApplicability + "</p>" +
        //           "<p><strong>3. Hiệu quả đem lại</strong></p>" +
        //           "<p>" + initiative.AchievedByAnothers + "</p>";

        //        return info;
        //    }

        //    private static void CopyHeadersFootersFromPreviousSection(Section section)
        //    {
        //        Section previousSection = (Section)section.PreviousSibling;

        //        if (previousSection == null)
        //            return;

        //        section.HeadersFooters.Clear();

        //        foreach (HeaderFooter headerFooter in previousSection.HeadersFooters)
        //            section.HeadersFooters.Add(headerFooter.Clone(true));
        //    }
        //}
    }
}