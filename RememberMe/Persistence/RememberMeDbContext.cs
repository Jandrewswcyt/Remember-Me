using System;
using Microsoft.EntityFrameworkCore;
using RememberMe.Core.Models;

namespace RememberMe.Persistence
{
    public class RememberMeDbContext : DbContext 
    {
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<ContactDetails> ContactDetails {get; set;}
        public RememberMeDbContext(DbContextOptions<RememberMeDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); 

            builder.Entity<Friend>().OwnsOne(
                p =>p.ContactDetails
            );
        }
        
    }
}