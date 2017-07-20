using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RememberMe.Controllers;
using RememberMe.Controllers.Resource;
using RememberMe.Core.Models;
using RememberMe.Persistence;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using RememberMe.Test.IntegrationTests;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace RememberMe.Test
{
    public class FriendControllerTest : IClassFixture<TestFixture<RememberMe.Test.Startup>>
    {
        public FriendControllerTest(TestFixture<RememberMe.Test.Startup> fixture)
        {
            _client = fixture.Client; 
        }

        private readonly HttpClient _client;
        [Fact]
        public void GetFriend_WhenCalled_ReturnsNotNull()
        {
            //Arrange
            FriendsController controller = CreateFriendController(); 

            //Act
            var response= controller.GetFriend(1); 

            //Assert
            Assert.NotNull(response); 
        }

        [Fact]
        public async Task GetFriend_WhenCalled_ReturnsCorrectId()
        {
            //Arrange
            //FriendsController controller = CreateFriendController(); 

            //Act
            var response = await _client.GetAsync("/api/friends/1");

            //Assert
            response.EnsureSuccessStatusCode(); 
            var fre = JsonConvert.DeserializeObject<FriendResource>( await response.Content.ReadAsStringAsync()); 
        
            Assert.Equal(fre.Name, "Sally"); 

        }

        //#TODO Probably a better way to create the mapper, without having a copy. Will do for now, change in the future. 
        private IMapper GetMapper()
        {
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Friend, FriendResource>(); 
                cfg.CreateMap<Friend, SaveFriendResource>(); 
                cfg.CreateMap<ContactDetails,ContactDetailsResource>();
                //API Resource to Domain
                cfg.CreateMap<SaveFriendResource,Friend>().ForMember(f => f.Id, opt => opt.Ignore()); 
                cfg.CreateMap<ContactDetailsResource,ContactDetails>(); 
            });

            return Mapper.Instance; 
        }

        private FriendsController CreateFriendController()
        {
            UnitOfWork work; 
            var repo = GetRepoContextWithData(out work);
            return new FriendsController(GetMapper(), repo,work);  
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