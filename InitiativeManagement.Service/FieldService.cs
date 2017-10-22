using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IFieldService
    {
        Field Add(Field Product);

        void Update(Field Product);

        Field Delete(int id);

        IEnumerable<Field> GetAll();

        IEnumerable<Field> GetAll(string keyword);

        void Save();

        Field GetById(int id);
    }

    public class FieldService : IFieldService
    {
        private IFieldRepository _fieldReIFieldRepositorypository;
        private IUnitOfWork _unitOfWork;

        public FieldService(IFieldRepository fieldRepository, IUnitOfWork unitOfWork)
        {
            this._fieldReIFieldRepositorypository = fieldRepository;
            this._unitOfWork = unitOfWork;
        }

        public Field Add(Field Field)
        {
            var field = _fieldReIFieldRepositorypository.Add(Field);
            _unitOfWork.Commit();

            return field;
        }

        public Field Delete(int id)
        {
            return _fieldReIFieldRepositorypository.Delete(id);
        }

        public void Update(Field Field)
        {
            _fieldReIFieldRepositorypository.Update(Field);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Field> GetAll()
        {
            return _fieldReIFieldRepositorypository.GetAll();
        }

        public IEnumerable<Field> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _fieldReIFieldRepositorypository.GetMulti(x => x.FieldName.Contains(keyword));
            else
                return _fieldReIFieldRepositorypository.GetAll();
        }

        public Field GetById(int id)
        {
            return _fieldReIFieldRepositorypository.GetSingleById(id);
        }
    }
}