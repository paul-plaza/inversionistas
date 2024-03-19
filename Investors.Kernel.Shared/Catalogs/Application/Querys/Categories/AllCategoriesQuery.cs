using CSharpFunctionalExtensions;
using Investors.Kernel.Shared.Catalogs.Specifications;
using Investors.Kernel.Shared.Shared.Infrastructure;
using MediatR;

namespace Investors.Kernel.Shared.Catalogs.Application.Querys.Categories
{

    public record AllCategoriesQuery : IRequest<Result<IReadOnlyCollection<CategoryResponse>>>;

    public class AllCategoriesHandler : IRequestHandler<AllCategoriesQuery, Result<IReadOnlyCollection<CategoryResponse>>>
    {
        private readonly IRepositorySharedForReadManager _repositoryClient;
        public AllCategoriesHandler(IRepositorySharedForReadManager repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
        public async Task<Result<IReadOnlyCollection<CategoryResponse>>> Handle(AllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repositoryClient.Catalogs.SingleOrDefaultAsync(new AllCategoryByIdSpecs(), cancellationToken);

            if (categories is null)
            {
                return Result.Failure<IReadOnlyCollection<CategoryResponse>>("Categoria no encontrada");
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
                .ToList();

            return Result.Success<IReadOnlyCollection<CategoryResponse>>(category);
        }
    }
}