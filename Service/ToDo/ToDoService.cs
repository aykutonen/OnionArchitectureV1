using Data.Infrastructure;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public ReturnModel<ToDoResponseModel.Create> Create(ToDoRequestModel.Create model)
        {
            var result = new ReturnModel<ToDoResponseModel.Create>();
            try
            {
                // Sıra belirtilmemişse en sona ekle.
                if (model.Order == 0)
                {
                    var lastRecord = repository.Get(x => x.UserId == model.UserId, o => o.Order);
                    model.Order = lastRecord != null ? lastRecord.Order + 1 : 1;
                }
                // Sıra belirtilmişse belirtilen sıradan sonraki kayıtları tekrar sırala
                else
                {
                    var nextRedords = repository.GetMany(x => x.UserId == model.UserId && x.Order >= model.Order, o => o.Order, true);
                    if (nextRedords != null && nextRedords.Count() > 0)
                    {
                        var lastOrder = model.Order + 1;
                        foreach (var t in nextRedords)
                        {
                            t.Order = lastOrder;
                            lastOrder++;
                        }
                    }
                }

                var toDo = new ToDo()
                {
                    Description = model.Description,
                    Order = model.Order,
                    UserId = model.UserId
                };

                repository.Add(toDo);
                Save();

                result.Data = new ToDoResponseModel.Create()
                {
                    id = toDo.id,
                    Description = toDo.Description,
                    Order = toDo.Order
                };
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
                Save();
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

        public ReturnModel<ToDoResponseModel.Update> Update(ToDoRequestModel.Update entity)
        {
            var result = new ReturnModel<ToDoResponseModel.Update>();

            // TODO: update işlemini yap yeni modellere göre.
            //try
            //{
            //    repository.Update(entity);
            //    result.Data = entity;
            //}
            //catch (Exception ex)
            //{
            //    result.isSuccess = false;
            //    result.Exception = ex;
            //    result.Message = ex.Message;
            //}

            return result;
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
