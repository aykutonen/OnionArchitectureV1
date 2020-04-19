using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class ToDo : BaseEntity
    {
        public string Description { get; set; }
        public int Order { get; set; }

        // Relations
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
