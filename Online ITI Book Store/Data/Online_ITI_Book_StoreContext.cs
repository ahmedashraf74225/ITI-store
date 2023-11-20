using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Online_ITI_Book_Store.Models;

namespace Online_ITI_Book_Store.Data
{
    public class Online_ITI_Book_StoreContext : DbContext
    {
        public Online_ITI_Book_StoreContext (DbContextOptions<Online_ITI_Book_StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Online_ITI_Book_Store.Models.book> book { get; set; } = default!;

        public DbSet<Online_ITI_Book_Store.Models.useraccounts>? useraccounts { get; set; }

        public DbSet<Online_ITI_Book_Store.Models.orders>? orders { get; set; }
        public DbSet<Online_ITI_Book_Store.Models.report> report { get; set; }

    }
}
