using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs
{
    public record CatalogEmailAdministratorQuery : IRequest<Result<string>>;

    public class CatalogEmailAdministratorQueryHandler : IRequestHandler<CatalogEmailAdministratorQuery, Result<string>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;

        public CatalogEmailAdministratorQueryHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<string>> Handle(CatalogEmailAdministratorQuery request, CancellationToken cancellationToken)
        {
            var email = await _repositoryClient.Catalogs.SingleOrDefaultAsync(new EmailAdministratorSpecs(), cancellationToken);

            return email ?? "pplaza@bsdevelopers.com";
        }
    }
}