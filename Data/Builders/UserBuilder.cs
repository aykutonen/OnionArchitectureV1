using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Builders
{
    public class UserBuilder
    {
        public UserBuilder(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.id);
            builder.Property(p => p.id).ValueGeneratedOnAdd();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.Password).IsRequired();
        }
    }
}
