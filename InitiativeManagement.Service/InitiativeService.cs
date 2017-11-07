using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using InitiativeManagement.Common;

namespace InitiativeManagement.Service
{
    public interface IInitiativeService
    {
        Initiative Add(Initiative initiative);

        void Update(Initiative initiative);

        Initiative Delete(int id);

        IEnumerable<Initiative> GetAll();

        IEnumerable<Initiative> GetMulti();

        IEnumerable<Initiative> GetAll(int page, int pageSize, out int totalRow, DynamicFilter filter, List<string> roles, string userId);

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

        public InitiativeService(IInitiativeRepository initiativeRepository, IUnitOfWork unitOfWork)
        {
            this._initiativeRepository = initiativeRepository;
            this._unitOfWork = unitOfWork;
        }

        public Initiative Add(Initiative initiative)
        {
            var initiativeSaved = _initiativeRepository.Add(initiative);

            _unitOfWork.Commit();

            return initiativeSaved;
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

        public IEnumerable<Initiative> GetAll(int page, int pageSize, out int totalRow, DynamicFilter filter, List<string> roles, string userId)
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

            if (!string.IsNullOrEmpty(filter.Keyword))
                query = query.Where(x => x.Title.ToUpper().Contains(filter.Keyword.ToUpper()) || x.KnowSolutionContent.ToUpper().Contains(filter.Keyword.ToUpper()) ||
                x.ImprovedContent.ToUpper().Contains(filter.Keyword.ToUpper()));

            if (!string.IsNullOrEmpty(filter.Time))
                query = query.Where(x => x.DeploymentTime.Year.ToString() == filter.Time);

            if (filter.Field != null)
                query = query.Where(x => x.FieldId == filter.Field);

            totalRow = query.Count();

            return query.OrderBy(x => x.Title).Skip(page * pageSize).Take(pageSize);
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

            if (!string.IsNullOrEmpty(filter.Keyword))
                query = query.Where(x => x.Title.ToUpper().Contains(filter.Keyword.ToUpper()) || x.KnowSolutionContent.ToUpper().Contains(filter.Keyword.ToUpper()) ||
                x.ImprovedContent.ToUpper().Contains(filter.Keyword.ToUpper()));

            if (!string.IsNullOrEmpty(filter.Time))
                query = query.Where(x => x.DeploymentTime.Year.ToString() == filter.Time);

            if (filter.Field != null)
                query = query.Where(x => x.FieldId == filter.Field);

            return query.OrderBy(x => x.Title);
        }

        public IEnumerable<Initiative> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public Initiative GetById(int id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}