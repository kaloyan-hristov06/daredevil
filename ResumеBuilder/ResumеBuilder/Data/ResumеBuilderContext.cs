using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ResumеBuilder.Models;

namespace ResumеBuilder.Data
{
    public class ResumеBuilderContext : DbContext
    {
        public ResumеBuilderContext (DbContextOptions<ResumеBuilderContext> options)
            : base(options)
        {
        }

        public DbSet<ResumеBuilder.Models.User> User { get; set; } = default!;
    }
}
