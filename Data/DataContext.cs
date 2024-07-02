using System.Data;
using dotnet6rpg.Migrations;
using Microsoft.EntityFrameworkCore;
using Weapon = dotnet6_rpg.Models.Weapon;

namespace dotnet6_rpg.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Skill>().HasData(
            new Skill{Id=1,Name="Fireball",Damage=30},
             new Skill{Id=2,Name="Frenzy",Damage=30},
              new Skill{Id=3,Name="Blizzard",Damage=50}
           );
        }
        public DbSet<Character>characters{get;set;}
         public DbSet<User>Users{get;set;}
         public DbSet<Weapon>weapons{get;set;}
        public DbSet<Skill>Skills{get;set;}
    }
}