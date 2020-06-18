using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IUserService : ISave
    {
        void Create(User entity);
        void Update(User entity);
        IEnumerable<User> Get();
        void Delete(long id);
    }
}
