using System.Collections.ObjectModel;
using CSharpFunctionalExtensions;
using Investors.Administrator.Reports.Domain;
using Investors.Administrator.Shared.Infrastructure;
using MediatR;

namespace Investors.Administrator.Reports.Application.Query
{

    public record ViewMovementsOfCashbackQuery() : IRequest<Result<IReadOnlyCollection<ViewMovementsOfCashback>>>;

    public class ViewMovementsOfCashbackHandler : IRequestHandler<ViewMovementsOfCashbackQuery, Result<IReadOnlyCollection<ViewMovementsOfCashback>>>
    {
        private readonly IRepositoryAdminForReadManager _repository;
        private readonly ISender _sender;

        public ViewMovementsOfCashbackHandler(IRepositoryAdminForReadManager repository, ISender sender)
        {
            _repository = repository;
            _sender = sender;
        }

        public async Task<Result<IReadOnlyCollection<ViewMovementsOfCashback>>> Handle(ViewMovementsOfCashbackQuery request, CancellationToken cancellationToken)
        {

            return Result.Success<IReadOnlyCollection<ViewMovementsOfCashback>>(new Collection<ViewMovementsOfCashback>());
        }
    }
}