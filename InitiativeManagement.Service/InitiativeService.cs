using InitiativeManagement.Common;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitiativeManagement.Service
{
    public interface IInitiativeService
    {
        bool Add(Initiative initiative, string userId);

        void Update(Initiative initiative);

        Initiative Delete(int id);

        IEnumerable<Initiative> GetAll();

        IEnumerable<Initiative> GetMulti();

        IEnumerable<Initiative> GetAll(DynamicFilter fiter, out int totalCount, List<string> roles, string userId);

        IEnumerable<Initiative> GetAll(string keyword);

        IEnumerable<Initiative> DownloadWord(DynamicFilter filter, List<string> roles, string userId);

        void Save();

        Initiative GetById(int id);

        Initiative FindById(int id);
    }

    public class InitiativeService : IInitiativeService
    {
        private IInitiativeRepository _initiativeRepository;
        private IUnitOfWork _unitOfWork;

        public InitiativeService(IInitiativeRepository initiativeRepository,
            IUnitOfWork unitOfWork)
        {
            _initiativeRepository = initiativeRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// For Add initiative
        /// </summary>
        /// <param name="initiative"></param>
        /// <param name="userId">The account which added initiave</param>
        /// <param name="authors"></param>
        /// <returns></returns>
        public bool Add(Initiative initiative, string userId)
        {
            // the user which is the name of the initiative
            initiative.AccountId = userId;

            initiative.IsDeactive = false;

            _initiativeRepository.Add(initiative);

            Save();

            return true;
        }

        public Initiative Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Initiative FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Initiative> GetAll()
        {
            return _initiativeRepository.GetAll();
        }

        public IEnumerable<Initiative> GetAll(DynamicFilter filter, out int totalCount, List<string> roles, string userId)
        {
            var query = new List<Initiative>();

            // has permission to view all
            var keyword = filter.Keyword.ToLower();

            if (roles.Any(x => x.Equals(Role.ViewIntiniativeForAdmin)))
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive && x.DateCreated >= filter.StartTime && x.DateCreated <= filter.EndTime && x.AccountId.Contains(filter.AccountId)
                && (x.Title.ToLower().Contains(keyword)
                || x.KnowSolutionContent.ToLower().Contains(keyword)
                || x.ImprovedContent.ToLower().Contains(keyword)), new string[] { "Field" }).ToList();
            }
            else
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive && x.AccountId == userId
                    && x.DateCreated.Date >= filter.StartTime.Date && x.DateCreated.Date <= filter.EndTime.Date
                    && (x.Title.ToLower().Contains(keyword)
                    || x.KnowSolutionContent.ToLower().Contains(keyword)
                    || x.ImprovedContent.ToLower().Contains(keyword)), new string[] { "Field" }).ToList();
            }

            if (filter.FieldId > -1)
            {
                query.Where(_ => _.FieldId == filter.FieldId);
            }

            if (filter.FieldGroupId > -1)
            {
                query.Where(_ => _.FieldId == filter.FieldGroupId);
            }

            totalCount = query.Count();

            return query.OrderByDescending(x => x.DateCreated).Skip(filter.Skip).Take(filter.Take);
        }

        public IEnumerable<Initiative> DownloadWord(DynamicFilter filter, List<string> roles, string userId)
        {
            IEnumerable<Initiative> query = Enumerable.Empty<Initiative>();

            if (roles.Any(x => x.Equals(CommonConstants.ADMIN) || x.Equals(CommonConstants.ADVANCEDROLE)))
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive, new string[] { "Field" });
            }
            else
            {
                query = _initiativeRepository.GetMulti(x => !x.IsDeactive && x.AccountId == userId, new string[] { "Field" });
            }

            //if (!string.IsNullOrEmpty(filter.Keyword))
            //    query = query.Where(x => x.Title.ToLower().Contains(filter.Keyword.ToLower()) || x.KnowSolutionContent.ToLower().Contains(filter.Keyword.ToLower()) ||
            //    x.ImprovedContent.ToLower().Contains(filter.Keyword.ToLower()));

            //if (!string.IsNullOrEmpty(filter.Time))
            //    query = query.Where(x => x.DeploymentTime.Year.ToString() == filter.Time);

            //if (filter.Field != null)
            //    query = query.Where(x => x.FieldId == filter.Field);

            return query.OrderBy(x => x.Title);
        }

        public IEnumerable<Initiative> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public Initiative GetById(int id)
        {
            return _initiativeRepository.GetSingleById(id);
        }

        public IEnumerable<Initiative> GetMulti()
        {
            return _initiativeRepository.GetMulti(item => item.IsDeactive != true);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Initiative initiative)
        {
            _initiativeRepository.Update(initiative);
        }
    }
}