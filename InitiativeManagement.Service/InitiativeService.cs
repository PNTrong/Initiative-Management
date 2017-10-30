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
        Initiative Add(Initiative initiative);

        void Update(Initiative initiative);

        Initiative Delete(int id);

        IEnumerable<Initiative> GetAll();

        IEnumerable<Initiative> GetMulti();

        IEnumerable<Initiative> GetAll(int page, int pageSize, out int totalRow, string filter);

        IEnumerable<Initiative> GetAll(string keyword);

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

        public IEnumerable<Initiative> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _initiativeRepository.GetAll(new string[] { "Field" });
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Title.Contains(filter) || x.Field.ToString() == filter || x.IsDeactive != true);

            totalRow = query.Count();
            return query.OrderBy(x => x.Title).Skip(page * pageSize).Take(pageSize);
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