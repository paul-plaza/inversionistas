using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Categories
{
    public record CategoryByIdQuery(int CategoryId) : IRequest<Result<CategoryResponse>>;

    public class CategoriesHandler : IRequestHandler<CategoryByIdQuery, Result<CategoryResponse>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public CategoriesHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<CategoryResponse>> Handle(CategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repositoryClient.Catalogs.SingleOrDefaultAsync(new CategoryByIdSpecs(request.CategoryId), cancellationToken);

            if (categories is null)
            {
                return Result.Failure<CategoryResponse>("Categoria no encontrada");
            }
            var category = categories.Categories
                .Select(category => new CategoryResponse(
                    Id: category.Id,
                    Description: category.Description,
                    Points: category.Points,
                    Nights: category.Nights,
                    NextLevelPoints: category.NextLevelPoints,
                    NextLevelNights: category.NextLevelNights,
                    Observation: category.Observation,
                    Percent: category.Percent,
                    Image: category.UrlImage
                    ))
                .Single();

            return Result.Success(category);
        }
    }
}