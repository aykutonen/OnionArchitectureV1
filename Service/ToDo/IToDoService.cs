using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IToDoService : ISave
    {
        ReturnModel<ToDo> Create(ToDo entity);
        ReturnModel<ToDo> Update(ToDo entity);
        ReturnModel<IEnumerable<ToDo>> Get(long userId);
        ReturnModel<long> Delete(long id);
    }
}
