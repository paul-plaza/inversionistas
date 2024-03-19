using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.Services;
using Investors.Repository.EF.Shared;
using MediatR;

namespace Investors.Repository.EF.Users
{
    public class UserManager : IUserManager
    {

        private readonly Lazy<UserService> _userService;
        public UserManager(InvestorsDbContext repositoryContext, ISender sender)
        {
            _userService = new Lazy<UserService>(() =>
                new UserService(
                    userWriteRepository: new UserRepository(repositoryContext),
                    sender: sender,
                    receiptWriteRepository: new ReceiptRepository(repositoryContext)));
        }

        public UserService Users => _userService.Value;
    }
}