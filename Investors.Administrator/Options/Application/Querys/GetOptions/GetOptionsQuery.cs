using CSharpFunctionalExtensions;
using Investors.Administrator.Options.Specifications;
using Investors.Administrator.Users.Domain.Repositories;
using MediatR;

namespace Investors.Administrator.Options.Application.Querys.GetOptions
{
    public sealed record GetOptionsQuery : IRequest<Result<List<OptionResponse>>>;

    public sealed class GetOptionsQueryHandler : IRequestHandler<GetOptionsQuery, Result<List<OptionResponse>>>
    {
        private readonly IRepositoryOptionForRead _repository;
        public GetOptionsQueryHandler(IRepositoryOptionForRead repository)
        {
            _repository = repository;

        }

        public async Task<Result<List<OptionResponse>>> Handle(GetOptionsQuery request, CancellationToken cancellationToken)
        {
            var options = await _repository.ListAsync(new OptionsActivesSpecs(), cancellationToken);

            if (options.Any() is false)
            {
                return Result.Failure<List<OptionResponse>>("Opciones no encontradas");
            }

            return Result.Success(options);
        }
    }
}