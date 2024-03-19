namespace Investors.Repository.EF.Users.Configuration.InvoiceEntity
{
    public static class InvoiceMetaData
    {
        public const string InvoiceType = "Tipo de factura (Cashback,Alojamiento,Mixto)";

        public const string TotalInvoice = "Total de factua, es la suma de cada item del detalle";

        public const string IdentificationInvestor = "Identificacion del Inversionista";

        public const string Identification = "Identificacion registrada en la factura";

        public const string OperationId = "Id de la operación donde se realizo el consumo";

        public const string TotalPoints = "Total de puntos generados en la factura";

        public const string TotalNights = "Total de noches generados en la factura";

    }
}