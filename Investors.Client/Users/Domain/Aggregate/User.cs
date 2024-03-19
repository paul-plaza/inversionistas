using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Domain.Entities.Notifications;
using Investors.Client.Users.Domain.Entities.Profiles;
using Investors.Client.Users.Domain.Entities.Transactions;
using Investors.Client.Users.Domain.Events.UserEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Categories;
using Investors.Kernel.Shared.Operations.Domain.Entities.Menus;

namespace Investors.Client.Users.Domain.Aggregate;

public class User : BaseEntity<Guid>, IAggregateRoot
{
    public const int IdentificationMaxLength = 20;
    public const int IdentificationMinLength = 5;
    public const int DisplayMaxLength = 20;
    public const int DisplayMinLength = 3;

    public const int NameMaxLength = 50;
    public const int SurNameMaxLength = 50;
    public string Identification { get; private set; } = string.Empty;
    public string DisplayName { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;
    public string SurName { get; private set; } = string.Empty;
    public UserType UserType { get; private set; } = UserType.Invited;

    public Email Email { get; private set; }
    public virtual Profile Profile { get; private set; }

    /// <summary>
    /// Integridad de entidad y agregar valor a entidades
    /// </summary>
    private readonly List<Receipt> _receipts = new();

    /// <summary>
    /// Lista de recibos
    /// </summary>
    public virtual IReadOnlyList<Receipt> Receipts => _receipts;

    /// <summary>
    /// Integridad de entidad y agregar valor a entidades
    /// </summary>
    private readonly List<Movement> _movements = new();

    /// <summary>
    /// Lista de movimientos
    /// </summary>
    public virtual IReadOnlyList<Movement> Movements => _movements;

    /// <summary>
    /// Integridad de entidad y agregar valor a entidades
    /// </summary>
    private readonly List<Notification> _notifications = new();

    /// <summary>
    /// Lista de notificaciones
    /// </summary>
    public virtual IReadOnlyList<Notification> Notifications => _notifications;

    protected User()
    {

    }

    private User(
        Guid id,
        string identification,
        string displayName,
        string name,
        string surname,
        Email email,
        UserType userType,
        Guid createBy,
        DateTime createOn,
        Status status,
        Profile profile
        ) : base(createBy, createOn, status)
    {
        Id = id;
        Identification = identification;
        DisplayName = displayName;
        UserType = userType;
        Profile = profile;
        Email = email;
        Name = name;
        SurName = surname;

        RaiseDomainEvent(new RegisteredUserEvent(Id, email, displayName, createBy, identification));
    }
    //TODO: UserType recibir parametro 
    public static Result<User> Create(
        Guid id,
        string identification,
        string displayName,
        string name,
        string surname,
        Guid userInSession,
        Email email,
        bool isInvestor = false)
    {
        Guard.Against.NullOrEmpty(id);
        Guard.Against.NullOrWhiteSpace(displayName);
        Guard.Against.NullOrWhiteSpace(identification);
        Guard.Against.NullOrWhiteSpace(name);
        Guard.Against.NullOrWhiteSpace(surname);

        if (displayName.Length > DisplayMaxLength || displayName.Length < DisplayMinLength)
            displayName = displayName.Substring(0, DisplayMaxLength);

        if (identification.Length > IdentificationMaxLength || identification.Length < IdentificationMinLength)
            identification = identification.Substring(0, IdentificationMaxLength);

        if (name.Length > NameMaxLength)
            name = name.Substring(0, NameMaxLength);

        if (surname.Length > SurNameMaxLength)
            surname = surname.Substring(0, SurNameMaxLength);

        Result<Profile> ensureProfileCreated = Profile.Create(userInSession);

        if (ensureProfileCreated.IsFailure)
        {
            return Result.Failure<User>(ensureProfileCreated.Error);
        }

        return Result.Success(new User(
            id: id,
            identification: identification,
            displayName: displayName,
            name: name,
            surname: surname,
            email: email,
            userType: isInvestor ? UserType.Investor : UserType.Invited,
            createBy: userInSession,
            createOn: DateTime.Now,
            status: Status.Active,
            profile: ensureProfileCreated.Value));
    }

    /// <summary>
    /// Actualiza informacion de perfil
    /// </summary>
    /// <param name="displayName"></param>
    /// <param name="surname"></param>
    /// <param name="identification"></param>
    /// <param name="isInvestor"></param>
    /// <param name="userInSession"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public Result UpdateInformation(
        string displayName,
        string name,
        string surname,
        string identification,
        bool isInvestor,
        Guid userInSession)
    {
        Guard.Against.NullOrWhiteSpace(displayName);
        Guard.Against.NullOrWhiteSpace(identification);
        Guard.Against.EnumOutOfRange<UserType.UserTypeEnum>(UserType.Value);

        if (displayName.Length > DisplayMaxLength || displayName.Length < DisplayMinLength)
            displayName = displayName.Substring(0, DisplayMaxLength);

        if (identification.Length > IdentificationMaxLength || identification.Length < IdentificationMinLength)
            identification = identification.Substring(0, IdentificationMaxLength);

        if (name.Length > NameMaxLength)
            name = name.Substring(0, NameMaxLength);

        if (surname.Length > SurNameMaxLength)
            surname = surname.Substring(0, SurNameMaxLength);

        if (Errors.Any())
            return Result.Failure(GetErrors());

        DisplayName = displayName;
        Identification = identification;
        UserType = isInvestor ? UserType.Investor : UserType.Invited;
        SurName = surname;
        Name = name;
        Update(userInSession);

        return Result.Success();
    }

    public Result DeleteUserAndProfile(Guid userId)
    {
        Profile.DeleteProfile(userId);
        Delete(userId);
        return Result.Success();
    }

    public Result RegisterReceipt(string identification, DateOnly invoiceDate, Guid userInSession)
    {
        Guard.Against.NullOrWhiteSpace(identification);

        if (UserType != UserType.Investor)
            Errors.Add(UserValidationConstants.UserNotAllowedToEnterInvoice);

        Result<Receipt> receipt = Receipt.Create(
            Id,
            DateOnly.FromDateTime(CreatedOn),
            identification,
            invoiceDate,
            $"{Name} {SurName}",
            Email.Value,
            userInSession);

        if (receipt.IsFailure)
            Errors.Add(receipt.Error);

        if (Errors.Any())
            return Result.Failure<Receipt>(GetErrors());

        _receipts.Add(receipt.Value);

        return Result.Success();
    }

    public Result RegisterRedemptionCashback(
        Guid userId,
        string email,
        string identification,
        int operationId,
        IEnumerable<MenuResponse> menu,
        int restaurantId,
        Guid userInSession)
    {
        Guard.Against.NullOrEmpty(userId);

        //valido si es inversionista
        if (UserType != UserType.Investor)
            Errors.Add(UserValidationConstants.UserNotAllowedToReedemCashback);

        Result<Movement> movement = Movement.CreateCashback(userId, operationId, menu, restaurantId, userInSession);

        if (movement.IsFailure)
            Errors.Add(movement.Error);
        if (Errors.Any())
            return Result.Failure<User>(GetErrors());

        RaiseDomainEvent(new RegisteredRedemptionCashbackEvent(Id, email, identification, operationId, menu, restaurantId, userInSession, DisplayName, movement.Value.Id));
        _movements.Add(movement.Value);

        return Result.Success();
    }

    /// <summary>
    /// Registra pedido para reserva de noches
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="operationId"></param>
    /// <param name="observation"></param>
    /// <param name="roomId"></param>
    /// <param name="dateStart"></param>
    /// <param name="dateEnd"></param>
    /// <param name="userInSession"></param>
    /// <returns></returns>
    public Result RegisterRedemptionNights(
        Guid userId,
        string email,
        string identification,
        int operationId,
        string? observation,
        int roomId,
        DateTime dateStart,
        DateTime dateEnd,
        Guid userInSession)
    {
        Guard.Against.NullOrEmpty(userId);

        //valido si es inversionista
        if (UserType != UserType.Investor)
            Errors.Add(UserValidationConstants.UserNotAllowedToReedemNight);

        //valido que tenga noches suficientes para reservar
        int nights = Math.Abs((dateStart - dateEnd).Days);
        if (Profile.NightsToRedeem < nights)
            return Result.Failure<ResponseDefault>("No cuenta con noches suficientes para reservar habitaciones");

        Result<Movement> ensureCreateNights = Movement.CreateNights(
            userId,
            operationId,
            observation,
            nights,
            roomId,
            dateStart,
            dateEnd,
            userInSession);

        if (ensureCreateNights.IsFailure)
            Errors.Add(ensureCreateNights.Error);

        if (Errors.Any())
            return Result.Failure<User>(GetErrors());

        RaiseDomainEvent(new RegisteredRedemptionNightsEvent(userId, email, identification, operationId, roomId, dateStart, dateEnd, userInSession, DisplayName,
            ensureCreateNights.Value.Id));
        _movements.Add(ensureCreateNights.Value);
        return Result.Success();
    }

    public Result RegisterNotification(
        Guid userId,
        string title,
        string? subTitle,
        string description,
        NotificationType notificationType,
        int? operationId,
        Guid userInSession)
    {
        Guard.Against.NullOrEmpty(userId);

        Result<Notification> notification = Notification.CreateNotification(
            userId,
            title,
            subTitle,
            description,
            notificationType,
            operationId,
            userInSession);
        if (notification.IsFailure)
            Errors.Add(notification.Error);

        if (Errors.Any())
            return Result.Failure<User>(GetErrors());

        _notifications.Add(notification.Value);
        return Result.Success();
    }

    public Result UpdateMovementCashback(TransactionState transactionState, Guid userInSession)
    {
        var movement = _movements.FirstOrDefault()!.UpdateMovement(transactionState, userInSession);
        if (movement.IsFailure)
            Errors.Add(movement.Error);

        if (Errors.Any())
            return Result.Failure<User>(GetErrors());

        if (transactionState == TransactionState.Accepted)
        {
            var redeem = Profile.RedeemCashback((uint)Movements.First().TotalToRedeem, userInSession);
            if (redeem.IsFailure)
                return Result.Failure<ResponseDefault>(redeem.Error);
        }

        RaiseDomainEvent(new CashbackRedeemedEvent(Id, DisplayName, transactionState, _movements.FirstOrDefault()!, userInSession));

        return Result.Success();
    }

    public Result UpdateMovementNights(TransactionState transactionState, Guid userInSession)
    {
        var movement = _movements.FirstOrDefault()!.UpdateMovement(transactionState, userInSession);
        if (movement.IsFailure)
            Errors.Add(movement.Error);

        if (Errors.Any())
            return Result.Failure<User>(GetErrors());

        if (transactionState == TransactionState.Accepted)
        {
            var redeem = Profile.RedeemNights((uint)Movements.First().TotalToRedeem, userInSession);
            if (redeem.IsFailure)
                return Result.Failure<ResponseDefault>(redeem.Error);
        }

        RaiseDomainEvent(new NightsRedeemedEvent(Id, DisplayName, transactionState, _movements.FirstOrDefault(), userInSession));

        return Result.Success();
    }

    /// <summary>
    /// Lee la notificación seleccionada
    /// </summary>
    /// <param name="status">stado de leída</param>
    /// <param name="notificationId">id de notificación a afectar</param>
    /// <param name="userInSession">usuario en sesion</param>
    /// <returns></returns>
    public Result ReadNotification(Status status, int notificationId, Guid userInSession)
    {
        var movement = _notifications.FirstOrDefault(noti => noti.Id == notificationId).ReadNotification(status, userInSession);
        if (movement.IsFailure)
            Errors.Add(movement.Error);

        if (Errors.Any())
            return Result.Failure<User>(GetErrors());

        return Result.Success();
    }

    /// <summary>
    /// Actualiza categoria del usuario, cuando se modifica la tabla inversionista
    /// </summary>
    /// <param name="user"></param>
    /// <param name="categories"></param>
    /// <param name="isInvestor"></param>
    /// <returns></returns>
    public Result UpdateCategory(User user, IEnumerable<CategoryResponse> categories, bool isInvestor)
    {
        if (!isInvestor)
        {
            UserType = UserType.Invited;
        }
        else
        {
            UserType = UserType.Investor;
        }
        return user.Profile.UpdateCategory(categories, isInvestor);
    }
}