using Aspose.Words;
using Aspose.Words.Tables;
using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.Infrastructure.Core;
using InitiativeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace InitiativeManagement.Web.Api
{
    [RoutePrefix("api/initiative")]
    public class InitiativeController : ApiControllerBase
    {
        private IInitiativeService _initiativeService;
        private IAuthorService _authorService;

        public InitiativeController(IErrorService errorService, IInitiativeService initiativeService, IAuthorService authorService) : base(errorService)
        {
            this._initiativeService = initiativeService;
            this._authorService = authorService;
        }

        [Route("export")]
        [HttpGet]
        public HttpResponseMessage Export(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
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
                    builder.InsertCell();

                    builder.EndRow();

                    count++;
                }

                builder.EndTable();

                var fileName = "danh_sach_de_tai.docx";
                var data = doc.Save(fileName);

                var response = request.CreateResponse(HttpStatusCode.OK, data);

                return response;
            });

            //var result = new HttpResponseMessage(HttpStatusCode.OK);
            //result.Content = new StringContent(content);
            ////a text file is actually an octet-stream (pdf, etc)
            //result.Content.Headers.ContentType = ExportToWord(model);
            ////we used attachment to force download
            //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //return result;
        }

        private void ExportToWord(IEnumerable<Initiative> initiatives)
        {
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
                builder.InsertCell();

                builder.EndRow();

                count++;
            }

            builder.EndTable();

            var fileName = "danh_sach_de_tai.docx";
            //return doc.Save(fileName);
            //doc.Save(System.Web.HttpContext.Current.Response, fileName, ContentDisposition.Inline, null);

            //string refURL = Request.UrlReferrer.AbsoluteUri;

            //string html = new WebClient().DownloadString(refURL);

            // To make the relative image paths work, base URL must be included in head section
            //html = html.Replace("</head>", string.Format("<base href='{0}'></base></head>", baseURL));

            //MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(html));
            //Document doc = new Document(stream);
            //string fileName = Guid.NewGuid().ToString() + ".doc";
            //doc.Save(System.Web.HttpContext.Current.Response, fileName, ContentDisposition.Inline, null);
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, int page, int pageSize, string filter = null)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                int totalRow = 0;
                var initiatives = _initiativeService.GetAll(page, pageSize, out totalRow, filter);
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

        [Route("add")]
        [HttpPost]
        [AllowAnonymous]
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
    }
}