using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IAppraisalBoardMemberCommnentService
    {
        AppraisalBoardMemberCommnent Add(AppraisalBoardMemberCommnent AppraisalBoardMemberCommnent);

        void Update(AppraisalBoardMemberCommnent AppraisalBoardMemberCommnent);

        AppraisalBoardMemberCommnent Delete(int id);

        IEnumerable<AppraisalBoardMemberCommnent> GetAll();

        IEnumerable<AppraisalBoardMemberCommnent> GetAll(string keyword);

        void Save();

        AppraisalBoardMemberCommnent GetById(int id);

        AppraisalBoardMemberCommnent FindById(int id);
    }

    public class AppraisalBoardMemberCommnentService : IAppraisalBoardMemberCommnentService
    {
        private IAppraisalBoardMemberCommnentRepository _appraisalBoardMemberCommnentRepository;
        private IUnitOfWork _unitOfWork;

        public AppraisalBoardMemberCommnentService(IAppraisalBoardMemberCommnentRepository appraisalBoardMemberCommnentRepository, IUnitOfWork unitOfWork)
        {
            this._appraisalBoardMemberCommnentRepository = appraisalBoardMemberCommnentRepository;
            this._unitOfWork = unitOfWork;
        }

        public AppraisalBoardMemberCommnent Add(AppraisalBoardMemberCommnent AppraisalBoardMemberCommnent)
        {
            var appraisalBoardMemberCommnent = _appraisalBoardMemberCommnentRepository.Add(AppraisalBoardMemberCommnent);
            _unitOfWork.Commit();

            return appraisalBoardMemberCommnent;
        }

        public AppraisalBoardMemberCommnent Delete(int id)
        {
            return _appraisalBoardMemberCommnentRepository.Delete(id);
        }

        public void Update(AppraisalBoardMemberCommnent AppraisalBoardMemberCommnent)
        {
            _appraisalBoardMemberCommnentRepository.Update(AppraisalBoardMemberCommnent);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<AppraisalBoardMemberCommnent> GetAll()
        {
            return _appraisalBoardMemberCommnentRepository.GetAll();
        }

        public IEnumerable<AppraisalBoardMemberCommnent> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _appraisalBoardMemberCommnentRepository.GetMulti(x => x.Comment.Contains(keyword));
            else
                return _appraisalBoardMemberCommnentRepository.GetAll();
        }

        public AppraisalBoardMemberCommnent GetById(int id)
        {
            return _appraisalBoardMemberCommnentRepository.GetSingleById(id);
        }

        public AppraisalBoardMemberCommnent FindById(int id)
        {
            return _appraisalBoardMemberCommnentRepository.GetSingleByCondition(x => x.AppraisalBoardCommnentId == id);
        }
    }
}