using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IFieldService
    {
        Field Add(Field Field);

        void Update(Field Field);

        Field Delete(int id);

        IEnumerable<Field> GetAll();

        IEnumerable<Field> GetAll(string keyword);

        void Save();

        Field GetById(int id);

        Field FindById(int id);
    }

    public class FieldService : IFieldService
    {
        private IFieldRepository _fieldRepository;
        private IUnitOfWork _unitOfWork;

        public FieldService(IFieldRepository fieldRepository, IUnitOfWork unitOfWork)
        {
            this._fieldRepository = fieldRepository;
            this._unitOfWork = unitOfWork;
        }

        public Field Add(Field Field)
        {
            var field = _fieldRepository.Add(Field);
            _unitOfWork.Commit();

            return field;
        }

        public Field Delete(int id)
        {
            return _fieldRepository.Delete(id);
        }

        public void Update(Field Field)
        {
            _fieldRepository.Update(Field);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Field> GetAll()
        {
            return _fieldRepository.GetAll();
        }

        public IEnumerable<Field> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _fieldRepository.GetMulti(x => x.FieldName.Contains(keyword));
            else
                return _fieldRepository.GetAll();
        }

        public Field GetById(int id)
        {
            return _fieldRepository.GetSingleById(id);
        }

        public Field FindById(int id)
        {
            return _fieldRepository.GetSingleByCondition(x => x.FieldGroupId == id);
        }
    }
}