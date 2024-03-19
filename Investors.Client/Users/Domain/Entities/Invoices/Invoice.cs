using Investors.Client.Users.Domain.Entities.Profiles;
using Investors.Client.Users.Domain.ValueObjects;

namespace Investors.Client.Users.Domain.Entities.Invoices;

public class Invoice : BaseEntity<int>
{
    public string Number { get; private set; } = string.Empty;

    public DateTime Date { get; private set; }

    public string Identification { get; private set; } = string.Empty;

    public Guid UserId { get; private set; }
    public string IdentificationInvestor { get; private set; } = string.Empty;

    public InvoiceType InvoiceType { get; private set; }

    public virtual Receipt Receipt { get; private set; }

    public int ReceiptId { get; private set; }

    public int OperationId { get; private set; }

    /// <summary>
    /// Integridad de entidad y agregar valor a entidades
    /// </summary>
    private readonly List<InvoiceDetail> _invoiceDetails = new();

    /// <summary>
    /// Detalle de factura 
    /// </summary>
    public virtual IReadOnlyList<InvoiceDetail> InvoiceDetails => _invoiceDetails;

    public double TotalInvoice { get; private set; }

    public uint? TotalPoints { get; private set; }

    public uint? TotalNights { get; private set; }


    protected Invoice()
    {
    }
    private Invoice(string number,
        DateTime date,
        Guid userId,
        string identification,
        string identificationInvestor,
        InvoiceType invoiceType,
        List<InvoiceDetail> detailInvoices,
        double totalInvoice,
        int operationId,
        uint? totalPoints,
        uint? totalNights,
        Guid createBy,
        DateTime createOn,
        Status status) : base(createBy, createOn, status)
    {
        Number = number;
        Date = date;
        Identification = identification;
        IdentificationInvestor = identificationInvestor;
        InvoiceType = invoiceType;
        _invoiceDetails.AddRange(detailInvoices);
        TotalInvoice = totalInvoice;
        UserId = userId;
        OperationId = operationId;
        TotalPoints = totalPoints;
        TotalNights = totalNights;
    }

    public static Result<Invoice> Create(string number,
        DateTime date,
        Guid userId,
        int operationId,
        string identification,
        string identificationInvestor,
        InvoiceType invoiceType,
        List<InvoiceDetail> detailInvoices,
        Guid createBy)
    {
        double totalInvoice = detailInvoices.Sum(x => x.TotalValue);
        uint? totalPoints = null;
        uint? totalNights = null;

        if (invoiceType == InvoiceType.Cashback)
        {
            totalPoints = Profile.CalculateTotalInvoice(detailInvoices.Where(x => x.TransactionType == 1).Sum(x => x.TotalValue));
        }

        if (invoiceType == InvoiceType.Night)
        {
            totalNights = (uint?)detailInvoices.Where(x => x.TransactionType == 2).Sum(x => x.Nights ?? 0);
        }

        if (invoiceType == InvoiceType.Mixed)
        {
            totalNights = (uint?)detailInvoices.Where(x => x.TransactionType == 2).Sum(x => x.Nights ?? 0);
            totalPoints = Profile.CalculateTotalInvoice(detailInvoices.Where(x => x.TransactionType == 1).Sum(x => x.TotalValue));
        }

        return Result.Success(
            new Invoice(
                number,
                date,
                userId,
                identification,
                identificationInvestor,
                invoiceType,
                detailInvoices,
                totalInvoice,
                operationId,
                totalPoints,
                totalNights,
                createBy,
                DateTime.Now,
                Status.Active));
    }
}