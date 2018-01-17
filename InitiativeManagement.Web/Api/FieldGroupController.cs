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
    [RoutePrefix("api/fieldGroup")]
    public class FieldGroupController : ApiControllerBase
    {
        #region Initialize

        private IFieldGroupService _fieldGroupService;
        private IFieldService _fieldService;

        public FieldGroupController(IErrorService errorService, IFieldGroupService fieldGroupService, IFieldService fieldService)
            : base(errorService)
        {
            this._fieldGroupService = fieldGroupService;
            this._fieldService = fieldService;
        }

        #endregion Initialize

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            //Func<HttpResponseMessage> func = () =>
            //{
            //    var model = _fieldGroupService.GetAll();
            //    var response = request.CreateResponse(HttpStatusCode.OK, model);
            //    return response;
            //};
            //return CreateHttpResponse(request, func);
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = _fieldGroupService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
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

                var model = _fieldGroupService.GetAll(skip, take, out totalRow, filter);

                var data = new GridModel<FieldGroup>()
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
                var model = _fieldGroupService.GetById(id);
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, FieldGroup fieldGroup)
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
                    _fieldGroupService.Add(fieldGroup);
                    _fieldGroupService.Save();

                    response = request.CreateResponse(HttpStatusCode.Created, fieldGroup);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, FieldGroup fieldGroup)
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
                    _fieldGroupService.Update(fieldGroup);
                    _fieldGroupService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, fieldGroup);
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

                try
                {
                    if (!ModelState.IsValid)
                    {
                        response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                    else
                    {
                        var fieldGroup = _fieldGroupService.Delete(id);

                        _fieldGroupService.Save();

                        response = request.CreateResponse(HttpStatusCode.OK, fieldGroup);
                    }
                }
                catch
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return response;
            });
        }
    }
}