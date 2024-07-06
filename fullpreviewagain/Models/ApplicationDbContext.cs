using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using fullpreviewagain.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Manager> managers { get; set; }
    public DbSet<Employee> employees { get; set; }
    public DbSet<Department> department { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot config = builder.Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DBCS"));
        }
    }

   
}
