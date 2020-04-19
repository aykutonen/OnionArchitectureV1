using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public abstract class BaseEntity
    {
        public long id { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
