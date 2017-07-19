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

        [Fact]
        public void CanAddContactDetails()
        {
            //Arrange 
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            ContactDetails cdOne = new ContactDetails {Phone = "07776668888", Email = "FooBlah@BlahFoo.com"}; 
            //Act
            repo.Add(new Friend{Id = 4, Name ="Dominic", ContactDetails = cdOne});
            work.Complete(); 
            //Assert
            var friend = repo.GetFriend(4).Result; 
            Assert.Equal(friend.ContactDetails.Email, cdOne.Email); 
            Assert.Equal(friend.ContactDetails.Phone, cdOne.Phone);  
        }

        [Fact]
        public async void CanUpdateDetails()
        {
            //Arrange 
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            ContactDetails cdOne = new ContactDetails {Phone = "07776668888", Email = "FooBlah@BlahFoo.com"}; 
            ContactDetails cdTwo = new ContactDetails {Phone = "2525252", Email = "NewFoo@Gmail.com"}; 
            //Act
            repo.Add(new Friend{Id = 4, Name = "James Brown",ContactDetails = cdOne}); 
            work.Complete(); 
            var friend = repo.GetFriend(4).Result; 
            friend.ContactDetails = cdTwo; 
            friend.Name = "Jim Brown"; 
            //Assert
            int totalFriends = await repo.TotalFriends(); 
            Assert.Equal(totalFriends, 4); 
            Assert.Equal(friend.Name, "Jim Brown") ; 
            Assert.Equal(cdTwo.Email, friend.ContactDetails.Email);
            Assert.Equal(cdTwo.Phone, friend.ContactDetails.Phone);
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
