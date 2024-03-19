namespace Investors.Client.Users.Application.Commands.RegisterUser
{
    public record UserByIdResponse
    {
        /// <summary>
        /// Id de usuario
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Identificacion de usuario
        /// </summary>
        public string Identification { get; init; }

        /// <summary>
        /// email de usuario
        /// </summary>
        public string Email { get; init; }

        /// <summary>
        /// Fecha de registro de usuario
        /// </summary>
        public DateTime UserRegistrationDate { get; init; }

        public string? FirstInvoiceRegistrationDate { get; init; }

        /// <summary>
        /// Categoria del usuario (3,4,5,6) Star
        /// </summary>
        public int Category { get; init; }

        /// <summary>
        /// Descripcion de la categoria (STAR, FOURSTAR, FIVESTAR, SIXSTAR)
        /// </summary>
        public string CategoryDescription { get; init; }

        /// <summary>
        /// porcentaje a mostrar de avance de categoria (3-25%, 4-50%, 5-75%, 6-100%)
        /// </summary>
        public int Percent { get; init; }

        /// <summary>
        /// Saldo historico acumulado en cashback
        /// </summary>
        public uint CashbackHistoryValue { get; init; }

        /// <summary>
        /// Descricion del label de cashback
        /// </summary>
        public string CashbackHistoryLabel { get; init; }

        /// <summary>
        /// cashback a completar para subir al siguiente nivel
        /// </summary>
        public int CashbackForNextLevel { get; init; }

        /// <summary>
        /// Saldo actual a redimir en cashback
        /// </summary>
        public uint CashbackToRedeem { get; init; }

        /// <summary>
        /// Noches actuales acumuladas
        /// </summary>
        public uint AccumulativeNights { get; init; }

        /// <summary>
        /// Total de noches a acumular para tener una noche gratis
        /// </summary>
        public int TotalAccumulativeNights { get; init; }

        /// <summary>
        /// Saldo historico acumulado en cashback
        /// </summary>
        public int NightsHistoryValue { get; init; }

        /// <summary>
        /// Descricion del label de cashback
        /// </summary>
        public string NightsHistoryLabel { get; init; }

        /// <summary>
        /// noches a completar para subir al siguiente nivel
        /// </summary>
        public int NightsForNextLevel { get; init; }

        /// <summary>
        /// Noches disponibles a redimir
        /// </summary>
        public uint NightsToRedeem { get; init; }

        /// <summary>
        /// Identificacion de usuario en QR
        /// </summary>
        public string UrlIdentificationQr { get; init; }

        /// <summary>
        /// Id de tipo de usuario
        /// </summary>
        public int UserTypeId { get; init; }

        /// <summary>
        /// Descripcion de tipo de usuario (Inversionista, Invitado)
        /// </summary>
        public string UserTypeDescription { get; init; }

        /// <summary>
        /// numero de notificaciones no leidas
        /// </summary>
        public int UnReadNotifications { get; init; }

    }
}