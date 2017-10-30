using Aspose.Words;
using Aspose.Words.Tables;
using InitiativeManagement.Service;
using InitiativeManagement.Web.Infrastructure.Core;
using System.Web.Http;

namespace InitiativeManagement.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : ApiControllerBase
    {
        private IErrorService _errorService;
        private IInitiativeService _initiativeService;

        public HomeController(IErrorService errorService, IInitiativeService initiativeService) : base(errorService)

        {
            this._errorService = errorService;
            this._initiativeService = initiativeService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, TEDU Member. ";
        }

        [HttpGet]
        [Route("export")]
        public void Export()
        {
            var initiatives = _initiativeService.GetMulti();

            Document doc = new Document();
            DocumentBuilder builder = new DocumentBuilder(doc);

            var headerTemplate = "<h3 align='center'>DANH MỤC SÁNG KIẾN THUỘC LĨNH VỰC QUẢN LÝ VÀ HOẠT ĐỘNG ĐOÀN ĐỘI</h3>";

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

            var count = 1;
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
                builder.Write(initiative.ImprovedContent);
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
    }
}