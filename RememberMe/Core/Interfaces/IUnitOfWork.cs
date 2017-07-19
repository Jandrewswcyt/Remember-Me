using System.Threading.Tasks;

namespace RememberMe.Core.Interfaces
{
    public interface IUnitOfWork
    {
         Task CompleteAsync(); 
         void Complete(); 
    }
}