using Data.Infrastructure;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service
{
    public class ToDoService : IToDoService
    {
        private readonly IRepository<ToDo> repository;
        private readonly IUnitOfWork unitOfWork;

        public ToDoService(IRepository<ToDo> repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public ReturnModel<ToDo> Create(ToDo entity)
        {
            var result = new ReturnModel<ToDo>();

            try
            {
                // Sıra belirtilmemişse en sona ekle.
                if (entity.Order == 0)
                {
                    var lastRecord = repository.Get(x => x.UserId == entity.UserId, o => o.Order);
                    entity.Order = lastRecord != null ? lastRecord.Order + 1 : 1;
                }
                // Sıra belirtilmişse belirtilen sıradan sonraki kayıtları tekrar sırala
                else
                {
                    var nextRedords = repository.GetMany(x => x.UserId == entity.UserId && x.Order >= entity.Order, o => o.Order, true);
                    if (nextRedords != null && nextRedords.Count() > 0)
                    {
                        var lastOrder = entity.Order + 1;
                        foreach (var t in nextRedords)
                        {
                            t.Order = lastOrder;
                            lastOrder++;
                        }
                    }
                }
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

        public ReturnModel<IEnumerable<ToDo>> Get(long userId)
        {
            var result = new ReturnModel<IEnumerable<ToDo>>();
            try
            {
                result.Data = repository.GetMany(x => x.UserId == userId, o => o.Order, true);
            }
            catch (Exception ex)
            {
                result.isSuccess = false;
                result.Exception = ex;
                result.Message = ex.Message;
            }

            return result;
        }

        public ReturnModel<ToDo> Update(ToDo entity)
        {
            var result = new ReturnModel<ToDo>();

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
