using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.Infrastructure.Core;
using InitiativeManagement.Web.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InitiativeManagement.Web.Api
{
    [Authorize]
    [RoutePrefix("api/field")]
    public class FieldController : ApiControllerBase
    {
        private IFieldService _fieldService;
        private IFieldGroupService _fieldGroupService;

        public FieldController(IErrorService errorService, IFieldService fieldService, IFieldGroupService fieldGroupService)
            : base(errorService)
        {
            this._fieldService = fieldService;
            this._fieldGroupService = fieldGroupService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _fieldService.GetAll();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getlistpaging")]
        [HttpGet]
        public HttpResponseMessage GetListPaging(HttpRequestMessage request, string filter, int skip, int take)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                int totalRow = 0;

                filter = filter ?? "";

                var model = _fieldService.GetAll(skip, take, out totalRow, filter);

                var data = new GridModel<Field>()
                {
                    items = model,
                    totalCount = totalRow
                };

                response = request.CreateResponse(HttpStatusCode.OK, data);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _fieldService.GetById(id);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, Field field)
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
                    _fieldService.Add(field);
                    _fieldService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, field);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, Field field)
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
                    _fieldService.Update(field);
                    _fieldService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, field);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var oldField = _fieldService.Delete(id);

                    _fieldService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, oldField);
                }

                return response;
            });
        }
    }
}