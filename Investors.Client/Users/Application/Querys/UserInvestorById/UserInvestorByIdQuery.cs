using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Application.Querys.HistoryNights;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using MediatR;

namespace Investors.Client.Users.Application.Querys.UserInvestorById
{

    public record UserInvestorByIdQuery(string Identification) : IRequest<Result<UserInvestorByIdResponse>>;

    public class UserInvestorByIdHandler : IRequestHandler<UserInvestorByIdQuery, Result<UserInvestorByIdResponse>>
    {
        private readonly ISender _sender;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        public UserInvestorByIdHandler(IRepositoryClientForReadManager repositoryClient, ISender sender)
        {
            _repositoryClient = repositoryClient;
            _sender = sender;
        }
        public async Task<Result<UserInvestorByIdResponse>> Handle(UserInvestorByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repositoryClient.Users.SingleOrDefaultAsync(new UserActiveByIdentificationSpecs(request.Identification), cancellationToken);

            if (user is null)
                return Result.Failure<UserInvestorByIdResponse>("Usuario no encontrado");

            bool isInvestor = user.UserType == UserType.Investor;

            return new UserInvestorByIdResponse(
                Id: user.Id,
                IsInvestor: isInvestor,
                Name: user.Name,
                SurName: user.SurName,
                Identification: user.Identification);
        }
    }
}