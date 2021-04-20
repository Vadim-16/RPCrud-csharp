using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RPCrud.Models;

namespace RPCrud.Data
{
    public class RPCrudContext : DbContext
    {
        public RPCrudContext (DbContextOptions<RPCrudContext> options)
            : base(options)
        {
        }

        public DbSet<RPCrud.Models.User> User { get; set; }
    }
}
