using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs
{
    public record class CatalogUrlAdministratorApiQuery : IRequest<Result<string>>;

    public class CatalogUrlAdministratorApiHandler : IRequestHandler<CatalogUrlAdministratorApiQuery, Result<string>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogUrlAdministratorApiHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<string>> Handle(CatalogUrlAdministratorApiQuery request, CancellationToken cancellationToken)
        {
            var url = await _repositoryClient.Catalogs.SingleOrDefaultAsync(new UrlAdministratorApiSpecs(), cancellationToken);

            return url ?? "https://investorsclient.azurewebsites.net/api/users/";
        }
    }
}