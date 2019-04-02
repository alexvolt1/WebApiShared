using Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Data
{
    public class WebApiSharedDbContext : DbContext
    {
        public WebApiSharedDbContext(DbContextOptions<WebApiSharedDbContext> options):base(options)
        {

        }

        public DbSet<Menu> Menus { get; set; }


    }
}
