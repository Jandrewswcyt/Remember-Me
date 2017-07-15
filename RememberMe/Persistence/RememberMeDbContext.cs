using System;
using Microsoft.EntityFrameworkCore;

namespace RememberMe.Persistence
{
    public class RememberMeDbContext : DbContext 
    {

        public RememberMeDbContext(DbContextOptions<RememberMeDbContext> options) : base(options)
        {
            
        }
        
    }
}