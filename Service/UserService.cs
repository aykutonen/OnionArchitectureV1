using Data.Infrastructure;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> repository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IRepository<User> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Create(User entity)
        {
            repository.Add(entity);
        }

        public void Delete(long id)
        {
            repository.Delete(x => x.id == id);
        }

        public IEnumerable<User> Get()
        {
            return repository.GetAll();
        }

        public void Update(User entity)
        {
            repository.Update(entity);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }

        
    }
}
