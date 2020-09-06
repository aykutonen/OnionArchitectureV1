using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Base
{
    public interface IService<T> : ISave
    {
        ReturnModel<T> Create(T entity);
        ReturnModel<T> Update(T entity);
        ReturnModel<IEnumerable<T>> Get(long id);
        ReturnModel<long> Delete(long id);
    }
}
