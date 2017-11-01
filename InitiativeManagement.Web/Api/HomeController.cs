using Aspose.Words;
using Aspose.Words.Tables;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.App_Start;
using InitiativeManagement.Web.Infrastructure.Core;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InitiativeManagement.Web.Api
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;
        private IInitiativeService _initiativeService;
        private IApplicationGroupService _appGroupService;
        private ApplicationUserManager _userManager;

        public HomeController(IErrorService errorService, IInitiativeService initiativeService, IApplicationGroupService appGroupService,
            ApplicationUserManager userManager) : base(errorService)

        {
            this._errorService = errorService;
            this._initiativeService = initiativeService;
            this._appGroupService = appGroupService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("TestMethod")]
        [Authorize]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }

        [HttpGet]
        [Route("permission")]
        public HttpResponseMessage GetUserRoles(HttpRequestMessage request)
        {
            var user = _userManager.FindById(User.Identity.GetUserId());

            var isAmind = user.IsAccountAdmin;

            return request.CreateResponse(HttpStatusCode.OK, new { sucess = true });
        }

        [HttpGet]
        [Route("export")]
        public void Export()
        {
            var initiatives = _initiativeService.GetMulti();

            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            builder.PageSetup.Orientation = Orientation.Landscape;
            Font font = builder.Font;
            font.Size = 13;
            font.Bold = true;
            font.Name = "Arial";

            var headerTemplate = "<p style='text-align:left;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;UBND TỈNH QUẢNG NAM&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
            " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
            "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM</strong></p>" +
            "<p style='text-align:left;'><strong><span style='text-decoration:underline;'> SỞ KHOA HỌC VÀ CÔNG NGHỆ</span>" +
            " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
            " &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;" +
            "<span style='text-decoration:underline;'> Độc lập -Tự do -Hạnh phúc </ span ></ strong ></p></br></br>" +
            "<p style='text-align:center;'><strong> DANH MỤC SÁNG KIẾN THUỘC LĨNH VỰC QUẢN LÝ VÀ HOẠT ĐỘNG ĐOÀN ĐỘI</strong></p></br>";

            builder.InsertHtml(headerTemplate);

            Table table = builder.StartTable();

            builder.InsertCell();
            builder.Write("TT");
            // Insert a cell
            builder.InsertCell();
            builder.Write("Tên sáng kiến");
            // Insert a cell
            builder.InsertCell();
            builder.Write("Mô tả sáng kiến");
            // Insert a cell
            builder.InsertCell();
            builder.Write("Ý kiến tổ thẩm định");

            builder.InsertCell();
            builder.Write("Điểm trung bình Tổ thẩm định");

            builder.EndRow();

            var count = 1;
            font.Bold = false;

            foreach (var initiative in initiatives)
            {
                // Insert a cell
                builder.InsertCell();
                builder.Write(count.ToString());
                // Insert a cell
                builder.InsertCell();
                builder.Write(initiative.Title);
                // Insert a cell
                builder.InsertCell();
                builder.InsertHtml(GetInitiativeInfo(initiative));
                // Insert a cell
                builder.InsertCell();
                builder.Write("");

                builder.InsertCell();
                builder.Write("");

                builder.EndRow();

                count++;
            }

            builder.EndTable();

            var fileName = "danh_sach_de_tai.docx";

            doc.Save(System.Web.HttpContext.Current.Response, fileName, ContentDisposition.Inline, null);

            System.Web.HttpContext.Current.Response.End();
        }

        private string GetInitiativeInfo(Initiative initiative)
        {
            var info = "<p><strong>1. Thông tin chung:</strong></p>" +
               "<p><strong>- Ngày sáng kiến được áp dụng lần đầu: " + initiative.DeploymentTime + "</strong></p>" +
               "<p><strong>2. Bản chất của sáng kiến</strong></p>" +
               "<p>2.1. Tình trạng của giải pháp đã biết</p>" +
               "<p>" + initiative.KnowSolutionContent + "</p>" +
               "<p>2.2. Nội dung giải pháp đề nghị công nhận là sáng kiến</p>" +
               "<p>" + initiative.ImprovedContent + "</p>" +
               "<p>2.3. Khả năng áp dụng</p>" +
               "<p>" + initiative.InitiativeApplicability + "</p>" +
               "<p><strong>3. Hiệu quả đem lại</strong></p>" +
               "<p>" + initiative.AchievedByAnothers + "</p>";

            return info;
        }
    }
}