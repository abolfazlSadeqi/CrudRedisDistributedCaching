using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contexts;


public class RedisDistributedCachingContext : DbContext
{
    public RedisDistributedCachingContext(DbContextOptions<RedisDistributedCachingContext> options)
        : base(options)
    {
    }
    public DbSet<Person> Persons => Set<Person>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }
}


