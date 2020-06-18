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

        public void Create(ToDo entity)
        {
            //entity.Status = 1;
            //entity.CreatedAt = DateTime.UtcNow;
            //entity.UpdatedAt = entity.CreatedAt;
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
        }

        public void Delete(long id)
        {
            repository.Delete(x => x.id == id);
        }

        public IEnumerable<ToDo> Get(long userId)
        {
            return repository.GetMany(x => x.UserId == userId, o => o.Order, true);
        }

        public void Update(ToDo entity)
        {
            repository.Update(entity);
        }

        public void Save()
        {
            unitOfWork.Commit();
        }
    }
}
