using Ardalis.Specification;
using Investors.Administrator.Options.Application.Querys.GetOptions;
using Investors.Administrator.Users.Domain.Entities;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Options.Specifications
{
    public sealed class OptionsActivesSpecs : Specification<Option, OptionResponse>
    {
        public OptionsActivesSpecs()
        {
            Query
                .Select(option => new OptionResponse(
                    option.Id,
                    option.Name,
                    option.Description,
                    option.Route,
                    option.Icon.Value,
                    option.Order
                    ))
                .Where(option => option.Status == Status.Active);
        }
    }
}