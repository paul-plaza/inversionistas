using Ardalis.Specification;
using Investors.Administrator.Users.Application.Querys.GetUsersAdmin;
using Investors.Administrator.Users.Domain.Aggregate;
using Investors.Shared.Domain.ValueObjects;

namespace Investors.Administrator.Users.Specifications
{
    public sealed class UsersAdminProfileSpecs : Specification<UserAdministrator, UserAdministratorResponse>
    {

        public UsersAdminProfileSpecs()
        {
            Query.Where(x => x.Status == Status.Active);
            // Realizar la transformación con Select
            Query.Select(x => new UserAdministratorResponse(
                x.Id,
                x.Email,
                x.Operations
                    .Where(y => y.Status == Status.Active)
                    .Select(op => new OperationResponse(op.OperationId))
                    .ToList(),
                x.Options
                    .Where(opt => opt.Status == Status.Active)
                    .Select(opt => new OptionsByUserResponse(
                        opt.Option.Id,
                        "",
                        "",
                        "",
                        "",
                        0
                        )).ToList()
                ));

            // Incluir las opciones y luego la relación adicional de Option
            Query.Include(x => x.Options)
                .ThenInclude(opt => opt.Option);

            Query.AsSplitQuery();
        }

    }
}