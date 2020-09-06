using Data.Infrastructure;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Base
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> repository;
        private readonly IUnitOfWork unitOfWork;
        public Service(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public ReturnModel<T> Create(T entity)
        {
            var result = new ReturnModel<T>();
            try
            {
                repository.Add(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<long> Delete(long id)
        {
            var result = new ReturnModel<long>();
            try
            {
                repository.Delete(x => x.id == id);
                result.Data = id;
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<IEnumerable<T>> Get(long id)
        {
            var result = new ReturnModel<IEnumerable<T>>();
            try
            {
                result.Data = repository.GetMany(x => x.id == id, o => o.id, true);
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public ReturnModel<T> Update(T entity)
        {
            var result = new ReturnModel<T>();
            try
            {
                repository.Update(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }
            return result;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
