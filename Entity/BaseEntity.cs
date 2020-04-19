using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            var dt = DateTime.UtcNow;
            this.CreatedAt = dt;
            this.UpdatedAt = dt;
        }

        public long id { get; set; }
        public int Status { get; set; } = 1;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
