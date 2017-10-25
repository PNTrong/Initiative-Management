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
    [RoutePrefix("api/groupauthor")]
    public class GroupAuthorController : ApiControllerBase
    {
        #region Initialize

        private IAuthorGroupService _authorgroupService;
        private IAuthorService _authorService;

        public GroupAuthorController(IErrorService errorService, IAuthorGroupService authorGroupService)
            : base(errorService)
        {
            this._authorgroupService = authorGroupService;
        }

        #endregion Initialize

        [Route("getallauthorgroup")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _authorgroupService.GetAll();
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
                var model = _authorgroupService.GetById(id);
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
                var model = _authorgroupService.GetAll(keyword);

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
                    var newAuthorGroup = new AuthorGroup();
                    _authorgroupService.Add(newAuthorGroup);
                    _authorgroupService.Save();

                    var responseData = _authorgroupService.Add(newAuthorGroup);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, AuthorGroup authorGroup)
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
                    var dbAuthorGroup = _authorgroupService.GetById(authorGroup.Id);
                    _authorgroupService.Update(dbAuthorGroup);
                    _authorgroupService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbAuthorGroup);
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
                    var Author = _authorService.FindById(id);
                    if (Author != null)
                    {
                    }
                    else
                    {
                        var oldauthorGroupService = _authorgroupService.GetById(id);
                        oldauthorGroupService.IsDeactive = false;
                        _authorgroupService.Update(oldauthorGroupService);
                        _authorgroupService.Save();
                        // response = request.CreateResponse(HttpStatusCode.Created, oldFieldGroup);
                    }
                }

                return response;
            });
        }
    }
}