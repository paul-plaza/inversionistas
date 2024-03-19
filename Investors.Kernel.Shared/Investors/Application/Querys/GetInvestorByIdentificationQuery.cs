using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Investors.Domain.Aggregate;
using Investors.Kernel.Shared.Investors.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Investors.Application.Querys
{
    public record GetInvestorByIdentificationQuery(string Identification) : IRequest<Result<InvestorByIdentificationResponse>>;

    public class GetInvestorByIdentificationHandler : IRequestHandler<GetInvestorByIdentificationQuery, Result<InvestorByIdentificationResponse>>
    {
        private readonly IRepositorySharedForReadManager _repositoryShared;

        public GetInvestorByIdentificationHandler(IRepositorySharedForReadManager repositoryShared)
        {
            _repositoryShared = repositoryShared;
        }

        public async Task<Result<InvestorByIdentificationResponse>> Handle(GetInvestorByIdentificationQuery request, CancellationToken cancellationToken)
        {
            var investor = await _repositoryShared.Investors.SingleOrDefaultAsync(new InvestorByIdentificationSpecs(request.Identification), cancellationToken);

            if (investor is null)
            {
                return Result.Failure<InvestorByIdentificationResponse>("Investor not found");
            }
            return Result.Success(investor.ToInvestorByIdentificationResponse());
        }
    }
}