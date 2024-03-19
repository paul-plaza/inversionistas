using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Domain.Services;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Categories;
using Investors.Shared.Infrastructure;
using MediatR;

namespace Investors.Client.Users.Application.Commands.RegisterUser
{
    public sealed record RegisterUserCommand(
        Guid UserId,
        string DisplayName,
        string Name,
        string SurName,
        string Identification,
        string Email,
        Guid UserInSession) : IRequest<Result<UserByIdResponse>>;

    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, Result<UserByIdResponse>>
    {
        private readonly IUserManager _userManager;
        private readonly IProviderConfigurationManager _provider;
        private readonly IRepositoryClientForReadManager _repositoryClient;
        private readonly ISender _sender;
        public RegisterUserHandler(IUserManager userManager,
            IRepositoryClientForReadManager repositoryClient,
            ISender sender,
            IProviderConfigurationManager provider)
        {
            _userManager = userManager;
            _repositoryClient = repositoryClient;
            _sender = sender;
            _provider = provider;
        }
        public async Task<Result<UserByIdResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //variable usado para buscar notificaciones solo si el usuario es existente
            bool searchNotification = true;
            int totalNotification = 0;
            var user = await _repositoryClient.Users.SingleOrDefaultAsync(new UserActiveByIdSpecs(request.UserId), cancellationToken);
            if (user is null)
            {
                var userToVerify = await _repositoryClient.Users.SingleOrDefaultAsync(new UserActiveByIdentificationSpecs(request.Identification), cancellationToken);
                if (userToVerify is not null)
                {
                    return Result.Failure<UserByIdResponse>("El usuario no puede activarse debido a que ya existe un usuario con la misma identificación");
                }

                searchNotification = false;
                var userNew = await _userManager.Users.Register(
                    userId: request.UserId,
                    displayName: request.DisplayName,
                    identification: request.Identification,
                    email: request.Email,
                    name: request.Name,
                    surname: request.SurName,
                    userInSession: request.UserInSession,
                    cancellationToken: cancellationToken);

                if (userNew.IsFailure)
                {
                    return Result.Failure<UserByIdResponse>(userNew.Error);
                }

                if (userNew.IsSuccess)
                    user = userNew.Value;
            }
            var category = await _sender.Send(new CategoryByIdQuery(user!.Profile.Category.Value), cancellationToken);
            if (category.IsFailure)
            {
                return Result.Failure<UserByIdResponse>("No se existen categorías configuradas");
            }

            var totalNightsToAccumulate = await _sender.Send(new CatalogTotalNightsToAccumulateQuery(), cancellationToken);
            if (totalNightsToAccumulate.IsFailure)
            {
                return Result.Failure<UserByIdResponse>("No existe total de noches a acumular configuradas");
            }

            var urlQr = _provider.GetUrlQr(user.Identification);

            if (searchNotification)
            {
                totalNotification = await _repositoryClient.Users.FirstOrDefaultAsync(new TotalNotificationUnReadSpecs(user.Id), cancellationToken);
            }

            return new UserByIdResponse
            {
                Id = user.Id,
                Name = user.DisplayName,
                Identification = user.Identification,
                Email = user.Email,
                UserRegistrationDate = user.CreatedOn,
                Category = category.Value.Id,
                CategoryDescription = category.Value.Description,
                CashbackHistoryValue = (uint)user.Profile.TotalMoneyAccumulated,
                CashbackToRedeem = user.Profile.CashBackToRedeem,
                CashbackForNextLevel = category.Value.NextLevelPoints,
                CashbackHistoryLabel = string.Format(category.Value.Observation.Split("|")[0], user.Profile.TotalMoneyAccumulated, category.Value.NextLevelPoints),
                AccumulativeNights = user.Profile.AccumulativeNights,
                NightsHistoryValue = user.Profile.TotalNightsAccumulated,
                NightsHistoryLabel = string.Format(category.Value.Observation.Split("|")[1], user.Profile.TotalNightsAccumulated, category.Value.NextLevelNights),
                TotalAccumulativeNights = totalNightsToAccumulate.Value,
                NightsForNextLevel = category.Value.NextLevelNights,
                NightsToRedeem = user.Profile.NightsToRedeem,
                UrlIdentificationQr = urlQr,
                UserTypeId = user.UserType.Value,
                UserTypeDescription = user.UserType.ToString(),
                UnReadNotifications = totalNotification,
                Percent = category.Value.Percent,
                FirstInvoiceRegistrationDate = user.Profile.FirstInvoiceRegistrationDate?.AddYears(1).ToString("dd/MM/yyyy") ?? string.Empty,
            };
        }
    }

}