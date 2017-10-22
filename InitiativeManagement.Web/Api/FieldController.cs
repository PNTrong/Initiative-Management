using AutoMapper;
using InitiativeManagement.Model.Models;
using InitiativeManagement.Service;
using InitiativeManagement.Web.Infrastructure.Core;
using InitiativeManagement.Web.Infrastructure.Extensions;
using InitiativeManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace InitiativeManagement.Web.Api
{
    [RoutePrefix("api/field")]
    [Authorize]
    public class FieldController : ApiControllerBase
    {
        #region Initialize

        private IFieldService _fieldService;

        public FieldController(IErrorService errorService, IFieldService fieldService)
            : base(errorService)
        {
            this._fieldService = fieldService;
        }

        #endregion Initialize

        [Route("getallfield")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _fieldService.GetAll();

                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            };
            return CreateHttpResponse(request, func);
        }

        [Route("getallbykeyword")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _fieldService.GetAll(keyword);

                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request)
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
                    var newField = new Field();
                    _fieldService.Add(newField);
                    _fieldService.Save();

                    var responseData = _fieldService.Add(newField);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ProductViewModel productVm)
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
                    var dbField = _fieldService.GetById(productVm.ID);
                    _fieldService.Update(dbField);
                    _fieldService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbField);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
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