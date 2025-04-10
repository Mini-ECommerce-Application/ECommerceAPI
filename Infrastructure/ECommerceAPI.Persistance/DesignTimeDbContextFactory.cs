﻿using ECommerceAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ECommerceAPIDbContext>
    {
        public ECommerceAPIDbContext CreateDbContext(string[] args = null)
        {

            DbContextOptionsBuilder<ECommerceAPIDbContext> dbContextoptionsBuilder = new();
            dbContextoptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextoptionsBuilder.Options);
        }
    }
}
