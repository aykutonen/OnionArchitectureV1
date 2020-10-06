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

        public ReturnModel<ToDoResponseModel.Update> Update(ToDoRequestModel.Update model)
        {
            var result = new ReturnModel<ToDoResponseModel.Update>();

            try
            {
                var entity = repository.Get(model.id);

                #region Check ToDo Order Change
                // Todo'nun sırası değişmişse
                if (model.Order.HasValue && entity.Order != model.Order)
                {
                    // Todo yukarı taşınmışsa; Yeni sıradan sonraki tüm sıraları 1 arttır.
                    if (model.Order.Value < entity.Order)
                    {
                        var nextRedords = repository
                            .GetMany(x =>
                            x.UserId == entity.UserId
                            && x.Order >= model.Order.Value
                            && x.Order < entity.Order
                            && x.id != model.id,
                            o => o.Order, true);

                        if (nextRedords != null && nextRedords.Count() > 0)
                        {
                            var lastOrder = model.Order.Value + 1;
                            foreach (var t in nextRedords)
                            {
                                t.Order = lastOrder;
                                lastOrder++;
                            }
                        }
                    }
                    // Todo aşağı taşınmışsa
                    else
                    {
                        var beforeRecords = repository
                            .GetMany(x =>
                            x.UserId == entity.UserId
                            && x.Order <= model.Order.Value
                            && x.Order > entity.Order
                            && x.id != model.id,
                            o => o.Order, true);

                        if (beforeRecords != null && beforeRecords.Count() > 0)
                        {
                            // Eski sıram 1 değilse eksiltme yap.
                            var lastOrder = model.Order.Value - 1;
                            foreach (var t in beforeRecords)
                            {
                                t.Order = lastOrder;
                                lastOrder--;
                            }
                        }

                    }
                }
                #endregion

                entity.Description = model.Description;
                entity.Order = model.Order ?? entity.Order;
                entity.Status = model.Status ?? entity.Status;
                entity.UpdatedAt = DateTime.UtcNow;

                repository.Update(entity);
                Save();

                result.Data = new ToDoResponseModel.Update()
                {
                    Description = entity.Description,
                    id = entity.id,
                    Order = entity.Order,
                    Status = entity.Status
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

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
