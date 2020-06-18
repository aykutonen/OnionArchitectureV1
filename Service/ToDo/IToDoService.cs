using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IToDoService : ISave
    {
        void Create(ToDo entity);
        void Update(ToDo entity);
        IEnumerable<ToDo> Get(long userId);
        void Delete(long id);
    }
}
