using System;
using System.Collections.Generic;
using InitiativeManagement.Data.Infrastructure;
using InitiativeManagement.Data.Repositories;
using InitiativeManagement.Model.Models;

namespace InitiativeManagement.Service
{
    public interface IAuthorService
    {
        Author Add(Author Author);

        void Update(Author Author);

        Author Delete(int id);

        IEnumerable<Author> GetAll();

        IEnumerable<Author> GetAll(string keyword);

        void Save();

        Author GetById(int id);

        Author FindById(int id);
    }

    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        private IUnitOfWork _unitOfWork;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork)
        {
            this._authorRepository = authorRepository;
            this._unitOfWork = unitOfWork;
        }

        public Author Add(Author Author)
        {
            var author = _authorRepository.Add(Author);
            _unitOfWork.Commit();

            return author;
        }

        public Author Delete(int id)
        {
            return _authorRepository.Delete(id);
        }

        public void Update(Author Field)
        {
            _authorRepository.Update(Field);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Author> GetAll()
        {
            return _authorRepository.GetAll();
        }

        public IEnumerable<Author> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _authorRepository.GetMulti(x => x.FullName.Contains(keyword));
            else
                return _authorRepository.GetAll();
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetSingleById(id);
        }

        public Author FindById(int id)
        {
            return _authorRepository.GetSingleByCondition(x => x.Id == id);
        }
    }
}