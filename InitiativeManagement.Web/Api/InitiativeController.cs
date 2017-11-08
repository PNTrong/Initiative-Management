using Aspose.Words;
using Aspose.Words.Tables;
using AutoMapper;
using InitiativeManagement.Common;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.App_Start;
using InitiativeManagement.Web.Infrastructure.Core;
using InitiativeManagement.Web.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InitiativeManagement.Web.Api
{
    [Authorize]
    [RoutePrefix("api/initiative")]
    public class InitiativeController : ApiControllerBase
    {
        private IInitiativeService _initiativeService;
        private IApplicationGroupService _applicationGroupService;
        private IApplicationRoleService _appRoleService;
        private ApplicationUserManager _userManager;

        private IAuthorService _authorService;

        public InitiativeController(IErrorService errorService, ApplicationUserManager userManager, IApplicationGroupService applicationGroupService,
            IInitiativeService initiativeService, IAuthorService authorService, IApplicationRoleService appRoleService) : base(errorService)
        {
            this._initiativeService = initiativeService;
            this._authorService = authorService;
            this._applicationGroupService = applicationGroupService;
            this._appRoleService = appRoleService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("export")]
        public void Export(string filter)
        {
            var filterObj = JsonConvert.DeserializeObject<DynamicFilter>(filter);

            var user = _userManager.FindById(User.Identity.GetUserId());

            var roles = _applicationGroupService.GetRolesByUserId(user.Id).Select(x => x.Name).ToList();

            var initiatives = _initiativeService.DownloadWord(filterObj, roles, user.Id);

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

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                int totalRow = 0;

                var filterObj = JsonConvert.DeserializeObject<DynamicFilter>(filter);

                var user = _userManager.FindById(User.Identity.GetUserId());

                var roles = _applicationGroupService.GetRolesByUserId(user.Id).Select(x => x.Name).ToList();

                var initiatives = _initiativeService.GetAll(page, pageSize, out totalRow, filterObj, roles, user.Id);

                var pagedSet = new PaginationSet<Initiative>()
                {
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize),
                    Items = initiatives
                };

                response = request.CreateResponse(HttpStatusCode.OK, pagedSet);

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(HttpRequestMessage request, InitiativeViewModel initiativeVM)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var initiative = Mapper.Map<InitiativeViewModel, Initiative>(initiativeVM);

                    if (initiative != null && string.IsNullOrEmpty(initiative.AccountId))
                    {
                        var userId = User.Identity.GetUserId();

                        initiative.AccountId = userId;
                    }

                    var initiativeDb = _initiativeService.Add(initiative);

                    if (initiativeDb == null)
                        response = request.CreateResponse(HttpStatusCode.BadGateway, ModelState);

                    var initiativeId = initiativeDb.Id;

                    var authors = initiativeVM.Authors;

                    foreach (var author in authors)
                    {
                        author.InitiativeId = initiativeId;
                        author.DateCreate = DateTime.Now;
                        _authorService.Add(author);
                    }

                    _initiativeService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, initiativeDb);
                }

                return response;
            });
        }

        [HttpDelete]
        [Route("deactive")]
        public HttpResponseMessage Deactive(HttpRequestMessage request, int id)
        {
            try
            {
                var initiative = _initiativeService.GetById(id);

                initiative.IsDeactive = true;

                _initiativeService.Update(initiative);

                _initiativeService.Save();

                return request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch
            {
                return request.CreateResponse(HttpStatusCode.OK, false);
            }
        }
    }
}