using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;
using System.Linq;

namespace InitiativeManagement.Service
{
    public interface IFieldService
    {
        Field Add(Field Field);

        void Update(Field Field);

        Field Delete(int id);

        IEnumerable<Field> GetAll();

        IEnumerable<Field> GetAll(int skip, int take, out int totalRow, string filter);

        IEnumerable<Field> GetAll(string keyword);

        void Save();

        Field GetById(int id);

        Field FindById(int id);

        IEnumerable<Field> GetByGroupId(int id);
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
            return _fieldRepository.GetMulti(x => !x.IsDeactive);
        }

        public IEnumerable<Field> GetByGroupId(int id)
        {
            return _fieldRepository.GetMulti(x => !x.IsDeactive && x.FieldGroupId == id);
        }

        public IEnumerable<Field> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _fieldRepository.GetMulti(x => x.FieldName.Contains(keyword) && !x.IsDeactive);
            else
                return _fieldRepository.GetMulti(x => !x.IsDeactive);
        }

        public Field GetById(int id)
        {
            return _fieldRepository.GetSingleById(id);
        }

        public Field FindById(int id)
        {
            return _fieldRepository.GetSingleByCondition(x => x.Id == id);
        }

        public IEnumerable<Field> GetAll(int skip, int take, out int totalRow, string filter)
        {
            var query = _fieldRepository.GetMulti(_ => !_.IsDeactive && (_.FieldName.Contains(filter)), new string[] { "FieldGroup" });

            totalRow = query.Count();

            return query.OrderBy(x => x.FieldName).Skip(skip).Take(take);
        }
    }
}