using AutoFixture;
using Investors.Client.Users.Application.Commands;
using Investors.Client.Users.Application.Commands.RegisterUser;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Repository.EF.Shared;
using Microsoft.EntityFrameworkCore;

namespace Investors.Client.NU.Tests.Users.Application.Commands;

[TestFixture]
public class RegisterUserCommandNUTests
{
    private RegisterUserHandler _registerUserHandler;

    [SetUp]
    public void Setup()
    {
        var fixture = new Fixture();
        var user = fixture.Create<User>();

        var options = new DbContextOptionsBuilder<InvestorsDbContext>()
            .UseInMemoryDatabase(databaseName: $"Investors-{Guid.NewGuid()}")
            .Options;


        var dbContext = new InvestorsDbContext(options, null, null);
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        _registerUserHandler = new RegisterUserHandler(null, null, null, null);
    }

    [Test]
    public async Task RegisterUserCommand_WhenValid_ShouldReturnSuccess()
    {

    }
}