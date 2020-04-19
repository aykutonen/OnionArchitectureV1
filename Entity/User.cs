using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Relations
        public virtual ICollection<ToDo> ToDos { get; set; }
    }
}
