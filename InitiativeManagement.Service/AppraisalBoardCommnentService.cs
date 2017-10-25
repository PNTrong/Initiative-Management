using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IAppraisalBoardCommnentService
    {
        AppraisalBoardCommnent Add(AppraisalBoardCommnent AppraisalBoardCommnent);

        void Update(AppraisalBoardCommnent AppraisalBoardCommnent);

        AppraisalBoardCommnent Delete(int id);

        IEnumerable<AppraisalBoardCommnent> GetAll();

        IEnumerable<AppraisalBoardCommnent> GetAll(string keyword);

        void Save();

        AppraisalBoardCommnent GetById(int id);
    }

    public class AppraisalBoardCommnentService : IAppraisalBoardCommnentService
    {
        private IAppraisalBoardCommnentRepository _appraisalBoardCommnentRepository;
        private IUnitOfWork _unitOfWork;

        public AppraisalBoardCommnentService(IAppraisalBoardCommnentRepository appraisalBoardCommnentRepository, IUnitOfWork unitOfWork)
        {
            this._appraisalBoardCommnentRepository = appraisalBoardCommnentRepository;
            this._unitOfWork = unitOfWork;
        }

        public AppraisalBoardCommnent Add(AppraisalBoardCommnent AppraisalBoardCommnent)
        {
            var appraisalBoardCommnent = _appraisalBoardCommnentRepository.Add(AppraisalBoardCommnent);
            _unitOfWork.Commit();

            return appraisalBoardCommnent;
        }

        public AppraisalBoardCommnent Delete(int id)
        {
            return _appraisalBoardCommnentRepository.Delete(id);
        }

        public void Update(AppraisalBoardCommnent AppraisalBoardCommnent)
        {
            _appraisalBoardCommnentRepository.Update(AppraisalBoardCommnent);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<AppraisalBoardCommnent> GetAll()
        {
            return _appraisalBoardCommnentRepository.GetAll();
        }

        public IEnumerable<AppraisalBoardCommnent> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _appraisalBoardCommnentRepository.GetMulti(x => x.GeneralComment.Contains(keyword));
            else
                return _appraisalBoardCommnentRepository.GetAll();
        }

        public AppraisalBoardCommnent GetById(int id)
        {
            return _appraisalBoardCommnentRepository.GetSingleById(id);
        }
    }
}