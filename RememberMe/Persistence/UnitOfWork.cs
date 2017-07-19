using System;
using System.Threading.Tasks;
using RememberMe.Core.Interfaces;

namespace RememberMe.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RememberMeDbContext context;
        public UnitOfWork(RememberMeDbContext context)
        {
            this.context = context;

        }

        public void Complete()
        {
            context.SaveChanges(); 
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync(); 
        }

    }
}