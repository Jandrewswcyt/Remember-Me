using System;
using Xunit;
using RememberMe.Core.Interfaces;
using RememberMe.Core.Models;
using RememberMe.Controllers;
using AutoMapper;
using RememberMe.Mapping;
using System.Threading.Tasks;
using RememberMe.Controllers.Resource;
using Microsoft.EntityFrameworkCore;
using RememberMe.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace RememberMe.Test
{


    public class FriendRepositoryTests

    {
        [Fact]
        public void CanAddToRepo()
        {
            
            //Arrange
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            
            //Act
            repo.Add(new Friend{Id = 4, Name = "Manjit"}); 
            work.Complete(); 
            //Assert
            var friendFour = repo.GetFriend(4).Result; 
            var friendTwo = repo.GetFriend(2).Result; 
            Assert.Equal( friendFour.Name, "Manjit"); 
            Assert.Equal( friendTwo.Name, "Jon"); 
        }


        private  FriendRepository GetRepoContextWithData(out UnitOfWork unitOfWork)
        {
            var options = new DbContextOptionsBuilder<RememberMeDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options; 
            var context = new RememberMeDbContext(options); 

            var repo = new FriendRepository(context); 
            ContactDetails cdOne = new ContactDetails {Phone = "07776668888", Email = "FooBlah@BlahFoo.com" }; 

            repo.Add(new Friend {Id = 1, Name = "Sally", ContactDetails = cdOne} );
            repo.Add(new Friend {Id = 2, Name = "Jon"} );
            repo.Add(new Friend {Id = 3, Name = "Theo"} );

            unitOfWork = new UnitOfWork(context);
            unitOfWork.Complete();  
            return repo;  
        }

    }

}
