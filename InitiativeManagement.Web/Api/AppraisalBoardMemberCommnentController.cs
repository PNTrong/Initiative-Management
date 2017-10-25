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
    [RoutePrefix("api/AppraisalBoardMember")]
    public class AppraisalBoardMemberCommnentController : ApiControllerBase
    {
        #region Initialize

        private IAppraisalBoardMemberCommnentService _appraisalBoardMemberCommnentService;

        public AppraisalBoardMemberCommnentController(IErrorService errorService, IAppraisalBoardMemberCommnentService appraisalBoardCommnentService)
            : base(errorService)
        {
            this._appraisalBoardMemberCommnentService = appraisalBoardCommnentService;
        }

        #endregion Initialize

        [Route("getallAppraisalBoardMember")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = () =>
            {
                var model = _appraisalBoardMemberCommnentService.GetAll();
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
                var model = _appraisalBoardMemberCommnentService.GetById(id);
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
                var model = _appraisalBoardMemberCommnentService.GetAll(keyword);

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
                    var newAppraisalBoardMemberCommnent = new AppraisalBoardMemberCommnent();
                    _appraisalBoardMemberCommnentService.Add(newAppraisalBoardMemberCommnent);
                    _appraisalBoardMemberCommnentService.Save();

                    var responseData = _appraisalBoardMemberCommnentService.Add(newAppraisalBoardMemberCommnent);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, AppraisalBoardMemberCommnent appraisalBoardMemberCommnent)
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
                    var dbAppraisalBoardCommnent = _appraisalBoardMemberCommnentService.GetById(appraisalBoardMemberCommnent.Id);
                    _appraisalBoardMemberCommnentService.Update(dbAppraisalBoardCommnent);
                    _appraisalBoardMemberCommnentService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, dbAppraisalBoardCommnent);
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
                    var olddbAppraisalBoardCommnent = _appraisalBoardMemberCommnentService.Delete(id);
                    _appraisalBoardMemberCommnentService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, olddbAppraisalBoardCommnent);
                }

                return response;
            });
        }
    }
}