using Data.Infrastructure;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public class ToDoBaseService : Base.Service<ToDo>
    {
        public ToDoBaseService(IRepository<ToDo> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork) { }
    }
}
