using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Events.ProfileEvents;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Categories;

namespace Investors.Client.Users.Domain.Entities.Profiles
{
    public class Profile : BaseEntity<int>
    {
        public const int NightsFree = 10;
        private const int InitialValueForAllProfile = 0;

        /// <summary>
        /// Cashback actual que puede reclamar
        /// </summary>
        public uint CashBackToRedeem { get; private set; }

        /// <summary>
        /// Numero de noches acumuladas para obtener una noche gratis
        /// </summary>
        public uint AccumulativeNights { get; private set; }

        /// <summary>
        /// Noches a reclamar gratis
        /// </summary>
        public uint NightsToRedeem { get; private set; }

        /// <summary>
        /// Historico de cashback acumulando el 5%
        /// </summary>
        public uint HistoryCashBack { get; private set; }

        /// <summary>
        /// Historico de noches gratis obtenido para categorizar en fourstar, fivestar, sixstar
        /// </summary>
        public int HistoryNights { get; private set; }

        /// <summary>
        /// Categoria fourstar, fivestar, sixstar
        /// </summary>
        public UserCategory Category { get; private set; }

        /// <summary>
        /// Total de facturas reclamadas por el usuario
        /// </summary>
        public int TotalAccumulatedInvoice { get; private set; }

        /// <summary>
        /// Total de cashback a reclamar en el mes
        /// </summary>
        public int TotalMonthlyCashBackToRedeem { get; private set; }

        /// <summary>
        /// Total de cashback reclamado en el mes
        /// </summary>
        public int TotalMonthlyCashBackClaimed { get; private set; }

        /// <summary>
        /// Total de cashback reclamado toda la vida
        /// </summary>
        public int TotalCashBackClaimed { get; private set; }

        /// <summary>
        /// Total de noches reclamado en el mes
        /// </summary>
        public int TotalMonthlyNightsClaimed { get; private set; }

        /// <summary>
        /// Total de noches a reclamar en el mes
        /// </summary>
        public int TotalMonthlyNightsToRedeem { get; private set; }

        /// <summary>
        /// Total de cashback reclamado toda la vida
        /// </summary>
        public int TotalNightsClaimed { get; private set; }

        /// <summary>
        /// Primera factura registrada por el usuario en el app
        /// </summary>
        public DateTime? FirstInvoiceRegistrationDate { get; private set; }

        /// <summary>
        /// Total de dinero acumulado
        /// </summary>
        public int TotalMoneyAccumulated { get; private set; }

        /// <summary>
        /// Total de noches acumuladas puras
        /// </summary>
        public int TotalNightsAccumulated { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; }

        protected Profile()
        {
        }

        private Profile(
            UserCategory userCategory,
            DateTime createOn,
            Guid userInSession,
            Status status) : base(userInSession, createOn, status)
        {
            CashBackToRedeem = InitialValueForAllProfile;
            AccumulativeNights = InitialValueForAllProfile;
            NightsToRedeem = InitialValueForAllProfile;
            HistoryCashBack = InitialValueForAllProfile;
            HistoryNights = InitialValueForAllProfile;
            Category = userCategory;
            TotalAccumulatedInvoice = InitialValueForAllProfile;
            TotalMonthlyCashBackToRedeem = InitialValueForAllProfile;
            TotalMonthlyCashBackClaimed = InitialValueForAllProfile;
            TotalCashBackClaimed = InitialValueForAllProfile;
            TotalMonthlyNightsClaimed = InitialValueForAllProfile;
            TotalMonthlyNightsToRedeem = InitialValueForAllProfile;
            TotalNightsClaimed = InitialValueForAllProfile;
            TotalMoneyAccumulated = InitialValueForAllProfile;
            TotalNightsAccumulated = InitialValueForAllProfile;
        }
        public static Result<Profile> Create(Guid userInSession)
        {
            return Result.Success(new Profile(UserCategory.Started, DateTime.Now, userInSession, Status.Active));
        }

        public static uint CalculateTotalInvoice(double totalInvoice)
        {
            return (uint)Math.Ceiling(totalInvoice * 0.05);
        }

        /// <summary>
        /// Acumula noches en base a noches hospedadas
        /// </summary>
        /// <param name="numberNights">numero de noches hospedadas</param>
        /// <param name="invoiceDocument"></param>
        /// <param name="operationId"></param>
        /// <param name="userInSession">Usuario en sesion</param>
        /// <returns></returns>
        public Result AccumulateNights(int numberNights, string invoiceDocument, int operationId, Guid userInSession)
        {
            if (numberNights < 1 || numberNights > 100)
                return Result.Failure<User>("Número de noches inválidas revise por favor su factura");

            FirstInvoiceRegistrationDate ??= DateTime.Now;

            TotalNightsAccumulated += numberNights;

            AccumulativeNights += (uint)numberNights;
            uint completeGroups = AccumulativeNights / NightsFree;
            NightsToRedeem += completeGroups;

            HistoryNights += (int)completeGroups;

            if (AccumulativeNights > 10)
            {
                AccumulativeNights %= completeGroups * NightsFree;
            }
            UpdateCategory();
            Update(userInSession);
            RaiseDomainEvent(new NightsAccumulatedEvent(UserId, numberNights, invoiceDocument, operationId, userInSession));
            return Result.Success();
        }

        /// <summary>
        /// suma en uno la facturas reclamadas por el usuario
        /// </summary>
        public void SumTotalAccumulatedInvoice()
        {
            TotalAccumulatedInvoice++;
        }

        /// <summary>
        /// Acumula puntos en base al total de una factura
        /// </summary>
        /// <param name="totalInvoice">Valor Total de la factura</param>
        /// <param name="invoiceDocument"></param>
        /// <param name="operationId"></param>
        /// <param name="userInSession">Usuario en sesion</param>
        /// <returns></returns>
        public Result AccumulatePoints(double totalInvoice, string invoiceDocument, int operationId, Guid userInSession)
        {
            if (totalInvoice < 1)
                return Result.Failure<User>("Total de factura inválido, revise por favor");

            uint points = CalculateTotalInvoice(totalInvoice);

            TotalMoneyAccumulated += (int)Math.Ceiling(totalInvoice);
            FirstInvoiceRegistrationDate ??= DateTime.Now;

            CashBackToRedeem += points;
            HistoryCashBack += points;
            UpdateCategory();
            Update(userInSession);
            RaiseDomainEvent(new CashbackAccumulatedEvent(UserId, (int)points, invoiceDocument, operationId, userInSession));
            return Result.Success();
        }

        /// <summary>
        /// Redime los puntos de un usuario en una operación
        /// </summary>
        /// <param name="points">costo en puntos</param>
        /// <param name="userInSession">usuario en sesion</param>
        /// <returns></returns>
        public Result RedeemCashback(uint points, Guid userInSession)
        {
            if (CashBackToRedeem < points)
            {
                return Result.Failure("No tiene suficiente puntos para realizar ésta operación");
            }
            CashBackToRedeem -= points;
            TotalCashBackClaimed += (int)points;
            Update(userInSession);
            return Result.Success();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberNights"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Result RedeemNights(uint numberNights, Guid userId)
        {
            if (NightsToRedeem < numberNights)
            {
                return Result.Failure("No tiene suficientes noches para realizar ésta operación");
            }
            NightsToRedeem -= numberNights;
            Update(userId);
            return Result.Success();
        }

        private void UpdateCategory()
        {
            Category = UserCategory.Started;
            if (TotalNightsAccumulated >= 15 || TotalMoneyAccumulated is >= 500 and <= 2000)
            {
                Category = UserCategory.FourStar;
            }

            if (TotalNightsAccumulated >= 30 || TotalMoneyAccumulated is >= 2000 and < 4000)
            {
                Category = UserCategory.FiveStar;
            }

            if (TotalNightsAccumulated >= 50 || TotalMoneyAccumulated >= 4000)
            {
                Category = UserCategory.SixStar;
            }
        }

        /// <summary>
        /// Actualiza categoria del usuario, cuando se modifica la tabla inversionista
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="isInvestor"></param>
        /// <returns></returns>
        public Result UpdateCategory(IEnumerable<CategoryResponse>? categories, bool isInvestor)
        {
            if (!isInvestor)
            {
                Category = UserCategory.Started;
                return Result.Success();
            }

            if (categories == null || !categories.Any())
            {
                return Result.Failure("Error al encontrar categorias para ubicar");
            }

            var categoryFour = categories.First(x => x.Id == 4);
            var categoryFive = categories.First(x => x.Id == 5);
            var categorySix = categories.First(x => x.Id == 6);

            if (HistoryNights >= categorySix.Nights || TotalMoneyAccumulated >= categorySix.Points)
            {
                Category = UserCategory.SixStar;
            }
            else if (HistoryNights >= categoryFive.Nights ||
                     TotalMoneyAccumulated >= categoryFive.Points &&
                     TotalMoneyAccumulated < categoryFive.NextLevelPoints)
            {
                Category = UserCategory.FiveStar;
            }
            else if (HistoryNights >= categoryFour.Nights ||
                     TotalMoneyAccumulated >= categoryFour.Points &&
                     TotalMoneyAccumulated <= categoryFour.NextLevelPoints)
            {
                Category = UserCategory.FourStar;
            }
            else
            {
                Category = UserCategory.Started;
            }

            return Result.Success();
        }

        public Result DeleteProfile(Guid userId)
        {
            Delete(userId);
            return Result.Success();
        }
    }

}