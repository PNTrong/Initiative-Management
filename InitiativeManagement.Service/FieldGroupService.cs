using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IFieldGroupService
    {
        FieldGroup Add(FieldGroup Product);

        void Update(FieldGroup Product);

        FieldGroup Delete(int id);

        IEnumerable<FieldGroup> GetAll();

        IEnumerable<FieldGroup> GetAll(string keyword);

        void Save();

        FieldGroup GetById(int id);
    }

    public class FieldGroupService : IFieldGroupService
    {
        private IFieldGroupRepository _fieldGroupRepository;
        private IUnitOfWork _unitOfWork;

        public FieldGroupService(IFieldGroupRepository fieldGroupRepository, IUnitOfWork unitOfWork)
        {
            this._fieldGroupRepository = fieldGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public FieldGroup Add(FieldGroup FieldGroup)
        {
            var fieldGroup = _fieldGroupRepository.Add(FieldGroup);
            _unitOfWork.Commit();

            return fieldGroup;
        }

        public FieldGroup Delete(int id)
        {
            return _fieldGroupRepository.Delete(id);
        }

        public void Update(FieldGroup fieldGroup)
        {
            _fieldGroupRepository.Update(fieldGroup);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<FieldGroup> GetAll()
        {
            return _fieldGroupRepository.GetAll();
        }

        public IEnumerable<FieldGroup> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _fieldGroupRepository.GetMulti(x => x.Name.Contains(keyword));
            else
                return _fieldGroupRepository.GetAll();
        }

        public FieldGroup GetById(int id)
        {
            return _fieldGroupRepository.GetSingleById(id);
        }
    }
}