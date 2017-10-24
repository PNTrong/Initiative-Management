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

        [Route("getallfieldGroup")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _fieldGroupService.GetAll();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            };
            return CreateHttpResponse(request, func);
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

        [Route("getallbykeyword")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _fieldGroupService.GetAll(keyword);

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
                    var newFieldGroup = new FieldGroup();
                    _fieldGroupService.Add(newFieldGroup);
                    _fieldGroupService.Save();

                    var responseData = _fieldGroupService.Add(newFieldGroup);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
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
                    var dbFieldGroup = _fieldGroupService.GetById(fieldGroup.Id);
                    _fieldGroupService.Update(dbFieldGroup);
                    _fieldGroupService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbFieldGroup);
                }

                return response;
            });
        }

        [Route("delete/{id:int}")]
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
                    Field field = _fieldService.GetById(id);
                    if (field != null)
                    {
                    }
                    else
                    {
                        var fieldGroup = _fieldGroupService.GetById(id);
                        fieldGroup.IsDeactive = false;
                        _fieldGroupService.Update(fieldGroup);
                        _fieldGroupService.Save();
                        // response = request.CreateResponse(HttpStatusCode.Created, oldFieldGroup);
                    }
                }

                return response;
            });
        }
    }
}