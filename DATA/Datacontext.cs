using APIcv.Models;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace APIcv.DATA
{
    public class Datacontext : DbContext
    {
        public Datacontext(DbContextOptions<Datacontext> options) : base(options)
        {
            
        }
        public DbSet<CV> CVs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EMPLOYEE> EMPLOYEEs { get; set; }
        public DbSet<CVMODIF> CVMODIFs { get; set; }
        public DbSet<CVEXPORTE> CVEXPORTEs { get; set; }

         protected override void OnModelCreating(ModelBuilder builder)
        {
            {
                base.OnModelCreating(builder);
                List<IdentityRole> roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
            };
                builder.Entity<IdentityRole>().HasData(roles);







            }


                
            ;
            



        }




    }

    
}
    

