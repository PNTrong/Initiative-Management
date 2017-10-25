using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IAuthorGroupService
    {
        AuthorGroup Add(AuthorGroup Product);

        void Update(AuthorGroup Product);

        AuthorGroup Delete(int id);

        IEnumerable<AuthorGroup> GetAll();

        IEnumerable<AuthorGroup> GetAll(string keyword);

        void Save();

        AuthorGroup GetById(int id);
    }

    public class AuthorGroupService : IAuthorGroupService
    {
        private IAuthorGroupRepository _authorGroupRepository;
        private IUnitOfWork _unitOfWork;

        public AuthorGroupService(IAuthorGroupRepository authorGroupRepository, IUnitOfWork unitOfWork)
        {
            this._authorGroupRepository = authorGroupRepository;
            this._unitOfWork = unitOfWork;
        }

        public AuthorGroup Add(AuthorGroup AuthorGroup)
        {
            var authorGroup = _authorGroupRepository.Add(AuthorGroup);
            _unitOfWork.Commit();

            return authorGroup;
        }

        public AuthorGroup Delete(int id)
        {
            return _authorGroupRepository.Delete(id);
        }

        public void Update(AuthorGroup AuthorGroup)
        {
            _authorGroupRepository.Update(AuthorGroup);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<AuthorGroup> GetAll()
        {
            return _authorGroupRepository.GetAll();
        }

        public IEnumerable<AuthorGroup> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                // return _authorGroupRepository.GetMulti(x => x.Id.Contains(keyword));
                return _authorGroupRepository.GetAll();
            else
                return _authorGroupRepository.GetAll();
        }

        public AuthorGroup GetById(int id)
        {
            return _authorGroupRepository.GetSingleById(id);
        }
    }
}