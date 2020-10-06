using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IToDoService : ISave
    {
        ReturnModel<ToDoResponseModel.Create> Create(ToDoRequestModel.Create entity);
        ReturnModel<ToDoResponseModel.Update> Update(ToDoRequestModel.Update entity);
        ReturnModel<IEnumerable<ToDo>> Get(long userId);
        ReturnModel<long> Delete(long id);
    }
}
