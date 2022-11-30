using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Dto;
using Xunit;
using FluentAssertions;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class TestUsers : IClassFixture<TestWebApplicationFactory<Api.Startup>>
    {
        private readonly TestWebApplicationFactory<Api.Startup> _factory;

        public TestUsers(TestWebApplicationFactory<Api.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var userDto = new UserDto()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "124"
            };

            var client = _factory.CreateClient();
            var content = JsonContent.Create(userDto);
            var response = await client.PostAsync("/create-user", content);
            var stringResponse = await response.Content
                .ReadAsStringAsync();
            var responseDto = JsonSerializer
                .Deserialize<Result>(stringResponse, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            response.EnsureSuccessStatusCode();
            responseDto.IsSuccess.Should().Be(true);
            responseDto.Errors.Should().Be("User Created");
        }

        [Fact]
        public async Task Should_Error_Duplicate()
        {
            var userDto = new UserDto()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Premium",
                Money = "124"
            };

            var client = _factory.CreateClient();
            var content = JsonContent.Create(userDto);
            var response = await client.PostAsync("/create-user", content);
            var stringResponse = await response.Content
                .ReadAsStringAsync();
            var responseDto = JsonSerializer
                .Deserialize<Result>(stringResponse, new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            response.EnsureSuccessStatusCode(); 
            responseDto.IsSuccess.Should().Be(false);
            responseDto.Errors.Should().Be("The user is duplicated");
        }
    }
}
