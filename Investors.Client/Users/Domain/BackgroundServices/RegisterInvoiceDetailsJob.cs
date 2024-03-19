using Investors.Client.Shared.Infrastructure;
using Investors.Client.Users.Application.Querys.Invoice;
using Investors.Client.Users.Domain.Aggregate;
using Investors.Client.Users.Domain.Entities.Invoices;
using Investors.Client.Users.Domain.Repositories;
using Investors.Client.Users.Domain.ValueObjects;
using Investors.Client.Users.Infrastructure.Satcom;
using Investors.Client.Users.Infrastructure.Satcom.DataObject;
using Investors.Client.Users.Repositories;
using Investors.Client.Users.Specifications;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs;
using Investors.Kernel.Shared.Catalogs.Application.Querys.Catalogs.Response;
using Investors.Kernel.Shared.Operations.Application.Querys.Operation;
using Investors.Kernel.Shared.Operations.Domain.Aggregate;
using Investors.Shared.Infrastructure;
using MediatR;

namespace Investors.Client.Users.Domain.BackgroundServices
{
    public interface IRegisterInvoiceDetailsJob
    {
        Task<Result> RegisterInvoiceByReceipts(CancellationToken cancellationToken);
        Task<Result> RegisterInvoicesForInvestors(CancellationToken cancellationToken);

    }
    public class RegisterInvoiceDetailsJob : IRegisterInvoiceDetailsJob
    {
        private readonly IInvoiceManagement _invoiceManagement;
        private readonly IUserWriteRepository _repository;
        private readonly IReceiptWriteRepository _repositoryReceipt;
        private readonly ISender _sender;

        public RegisterInvoiceDetailsJob(IInvoiceManagement invoiceManagement,
            IUserWriteRepository repository,
            ISender sender,
            IReceiptWriteRepository receiptReadRepository)
        {
            _invoiceManagement = invoiceManagement;
            _repository = repository;
            _sender = sender;
            _repositoryReceipt = receiptReadRepository;
        }
        /// <summary>
        /// Registro Facturas de referidos
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> RegisterInvoiceByReceipts(CancellationToken cancellationToken)
        {
            //obtengo operaciones con usuario y contraseña
            var operations = await FilteredOperations(cancellationToken);

            //lista de usuarios activos y inversionistas include receipts
            var users = await _repository.ListAsync(new UserAndReceiptSpecs(), cancellationToken);

            //filtramos el receipt id 0 que es el recibo
            var receiptsFilter = users.SelectMany(x => x.Receipts)
                .Where(receipt => receipt.Id != 0)
                .Select(x => new RequestInvoiceFromDates
                {
                    From = x.Date.ToString("yyyy-MM-dd"),
                    To = x.Date.ToString("yyyy-MM-dd"),
                })
                .DistinctBy(x => x.From).ToList();


            //aqui tengo que obtener todos los comprobantes no sincronizados, hacer un distinct por fecha de registro y enviarlos a satcom
            var existingInvoices = users.SelectMany(x => x.Receipts).SelectMany(x => x.Invoices);

            //obtengo catalogos de habitaciones y consumos de restaurantes
            var rooms = await _sender.Send(new CatalogRoomsQuery(), cancellationToken);
            //obtengo catalogos con codigos que no deben sincronizarse
            var unauthorizedRecords = await _sender.Send(new CatalogUnauthorizedRecordsForPointsCatalogQuery(), cancellationToken);

            //obtengo todas las facturas de las fechas registradas en la tabla recibos
            var responses = await GetAllInvoicesFromSatcom(operations, receiptsFilter, cancellationToken);

            //Obtengo todas las facturas que esten ok y que no esten en nuestro sistema
            var invoicesOk =
                from invoicesSatcom in responses
                join receipt in users.SelectMany(x => x.Receipts)
                    on invoicesSatcom.Identification equals receipt.Identification
                where !existingInvoices.Select(x => x.Number).Contains(invoicesSatcom.Document)
                select new ResponseInvoice
                {
                    DateOfIssue = invoicesSatcom.DateOfIssue,
                    DepartureDate = invoicesSatcom.DepartureDate,
                    Document = invoicesSatcom.Document,
                    ArrivalDate = invoicesSatcom.ArrivalDate,
                    BusinessName = invoicesSatcom.BusinessName,
                    DescriptionState = invoicesSatcom.DescriptionState,
                    Environment = invoicesSatcom.Environment,
                    Group = invoicesSatcom.Group,
                    Identification = invoicesSatcom.Identification,
                    Institution = invoicesSatcom.Institution,
                    Spot = invoicesSatcom.Spot,
                    Subtotal = invoicesSatcom.Subtotal,
                    Total = invoicesSatcom.Total,
                    ReceiptId = receipt.Id,
                    OperationId = invoicesSatcom.OperationId

                };

            var invoiceToProcess = ConcatInvoiceToProcess(invoicesOk,
                rooms.IsSuccess
                    ? rooms.Value
                    : new List<CatalogResponse>(),
                unauthorizedRecords.IsSuccess
                    ? unauthorizedRecords.Value
                    : new List<CatalogResponse>());

            await ProcessInvoices(
                users,
                invoiceToProcess);

            await _repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        public async Task<Result> RegisterInvoicesForInvestors(CancellationToken cancellationToken)
        {
            //obtengo operaciones con usuario y contraseña
            var operations = await FilteredOperations(cancellationToken);

            //lista de usuarios activos con sus perfiles
            var users = await _repository.ListAsync(new UsersAndProfileSpecs(), cancellationToken);

            var currentDate = DateTime.Now;
            var receipt = new RequestInvoiceFromDates
            {
                From = currentDate.ToString("yyyy-MM-dd"),
                To = currentDate.ToString("yyyy-MM-dd"),
            };

            var receiptsFilter = new List<RequestInvoiceFromDates>()
            {
                receipt
            };

            //aqui obtengo todas las facturas registradas en el sistema para evitar duplicidad
            var invoices = await _sender.Send(new GetInvoicesQuery(), cancellationToken);

            //obtengo catalogos de habitaciones y consumos de restaurantes
            var rooms = await _sender.Send(new CatalogRoomsQuery(), cancellationToken);
            //obtengo catalogos con codigos que no deben sincronizarse
            var unauthorizedRecords = await _sender.Send(new CatalogUnauthorizedRecordsForPointsCatalogQuery(), cancellationToken);

            //obtengo todas las facturas de hoy en satcom para todas las operaciones
            var invoicesSuccess = await GetAllInvoicesFromSatcom(operations, receiptsFilter, cancellationToken);

            //obtengo solo las facturas que no esten en nuestro sistema
            var invoicesOk =
                from invoicesSatcom in invoicesSuccess
                where !invoices.Value.Select(x => x.Number).Contains(invoicesSatcom.Document)
                select new ResponseInvoice
                {
                    DateOfIssue = invoicesSatcom.DateOfIssue,
                    DepartureDate = invoicesSatcom.DepartureDate,
                    Document = invoicesSatcom.Document,
                    ArrivalDate = invoicesSatcom.ArrivalDate,
                    BusinessName = invoicesSatcom.BusinessName,
                    DescriptionState = invoicesSatcom.DescriptionState,
                    Environment = invoicesSatcom.Environment,
                    Group = invoicesSatcom.Group,
                    Identification = invoicesSatcom.Identification,
                    Institution = invoicesSatcom.Institution,
                    Spot = invoicesSatcom.Spot,
                    Subtotal = invoicesSatcom.Subtotal,
                    Total = invoicesSatcom.Total,
                    ReceiptId = 0, // Id 0 de recibo por defecto
                    OperationId = invoicesSatcom.OperationId
                };

            var invoiceToProcess = ConcatInvoiceToProcess(invoicesOk,
                rooms.IsSuccess
                    ? rooms.Value
                    : new List<CatalogResponse>(),
                unauthorizedRecords.IsSuccess
                    ? unauthorizedRecords.Value
                    : new List<CatalogResponse>());

            var receiptToProcess = await _repositoryReceipt.SingleOrDefaultAsync(new ReceiptsByIdSpecs(0), cancellationToken);

            if (receiptToProcess is null)
            {
                return Result.Failure("No existe configurado un recibo por defecto");
            }
            await ProcessInvoices(
                users,
                invoiceToProcess,
                receiptToProcess);

            await _repository.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        /// <summary>
        /// Obtengo todas las operaciones que tengan usuario y contraseña
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<List<OperationResponse>> FilteredOperations(CancellationToken cancellationToken)
        {
            var operations = await _sender.Send(new GetOperationsQuery(), cancellationToken);
            return operations.Value.Where(op => !string.IsNullOrEmpty(op.UserName)).ToList();
        }

        /// <summary>
        /// Obtengo todas las facturas de satcom de todas las operaciones
        /// </summary>
        /// <param name="operations"></param>
        /// <param name="filters"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<IEnumerable<ResponseInvoice>> GetAllInvoicesFromSatcom(List<OperationResponse> operations,
            List<RequestInvoiceFromDates> filters,
            CancellationToken cancellationToken)
        {
            var tasks = operations
                .SelectMany(operation =>
                    filters
                        .Select(x =>
                            _invoiceManagement.GetInvoices(
                                operation.Id,
                                operation.UserName,
                                operation.Password,
                                x, cancellationToken)));
            var result = await Task.WhenAll(tasks);
            return result.Where(x => x.IsSuccess)
                .SelectMany(x => x.Value)
                .ToList();
        }

        private IEnumerable<ResponseInvoice> ConcatInvoiceToProcess(
            IEnumerable<ResponseInvoice> invoicesOk,
            IEnumerable<CatalogResponse> rooms,
            IEnumerable<CatalogResponse> unauthorizedRecords)
        {
            //Obtengo todos los registros catalogados como cashback
            var invoicesForCashback =
                from invoicesSatcom in invoicesOk
                join cashback in unauthorizedRecords.SelectMany(x => x.CatalogDetails).ToList()
                    on invoicesSatcom.Group equals cashback.Value into joinedCashback
                from result in joinedCashback.DefaultIfEmpty()
                where result == null
                select invoicesSatcom with
                {
                    TransactionType = InvoiceType.Cashback.Value
                };


            //Obtengo todos los registros catalogados como noches a acumular
            var invoicesForNights = from invoicesSatcom in invoicesOk
                join nights in rooms.SelectMany(x => x.CatalogDetails).ToList()
                    on invoicesSatcom.Group equals nights.Value
                select invoicesSatcom with
                {
                    TransactionType = InvoiceType.Night.Value
                };

            //Agrupo registro de cashback y noches para armar mi entidad factura-detalle

            return invoicesForCashback.Concat(invoicesForNights);
        }

        /// <summary>
        /// Proceso las facturas de satcom y las guardo en el sistema
        /// </summary>
        /// <param name="users"></param>
        /// <param name="invoicesToSave"></param>
        /// <returns></returns>
        private Task ProcessInvoices(
            IEnumerable<User> users,
            IEnumerable<ResponseInvoice> invoicesToSave)
        {
            foreach (var user in users)
            {
                foreach (var receipt in user.Receipts)
                {
                    List<Invoice> listInvoice = new List<Invoice>();
                    var invoiceInReceipt = invoicesToSave
                        .Where(x => x.ReceiptId == receipt.Id && x.Identification == receipt.Identification)
                        .GroupBy(x => x.Document);


                    foreach (var item in invoiceInReceipt)
                    {
                        List<InvoiceDetail> listDetail = new List<InvoiceDetail>();
                        foreach (var itemDetail in item)
                        {
                            var isArrivalDateOk = DateTime.TryParse(itemDetail.ArrivalDate, out var arrivalDate);

                            var isDepartureDateOk = DateTime.TryParse(itemDetail.DepartureDate, out var departureDate);

                            Result<InvoiceDetail> ensureInvoiceDetailCreated = InvoiceDetail.Create(
                                itemDetail.Group,
                                groupDetail: itemDetail.Environment.ToString() ?? string.Empty,
                                itemDetail.Total,
                                isArrivalDateOk ? arrivalDate : null,
                                isDepartureDateOk ? departureDate : null,
                                itemDetail.TotalDays,
                                itemDetail.TransactionType,
                                new Guid(IUserSession.UserVirtualCode));

                            if (ensureInvoiceDetailCreated.IsSuccess)
                            {
                                listDetail.Add(ensureInvoiceDetailCreated.Value);
                            }
                        }

                        //verifico si la factura es solo cashback, noches o mixta
                        int invoiceTypeCount = item.Select(x => x.TransactionType).Distinct().Count();

                        var invoiceType = invoiceTypeCount == 1 ? InvoiceType.From(item.First().TransactionType) : InvoiceType.Mixed;

                        Result<Invoice> ensureInvoiceCreated = Invoice.Create(
                            item.First().Document,
                            item.First().DateOfIssue,
                            user.Id,
                            item.First().OperationId,
                            item.First().Identification,
                            user.Identification,
                            invoiceType,
                            listDetail,
                            new Guid(IUserSession.UserVirtualCode));

                        if (ensureInvoiceCreated.IsSuccess)
                        {
                            listInvoice.Add(ensureInvoiceCreated.Value);
                            //si existe noches a acumular
                            var transNights = item.Where(x => x.TransactionType == 2);
                            //si existe cashback a acumular
                            var transCashback = item.Where(x => x.TransactionType == 1);
                            bool hasInvoice = false;
                            if (transNights.Any())
                            {
                                int totalDays = transNights.Sum(x => x.TotalDays);
                                user.Profile.AccumulateNights(
                                    totalDays,
                                    item.First().Document,
                                    item.First().OperationId,
                                    new Guid(IUserSession.UserVirtualCode));
                                hasInvoice = true;
                            }
                            if (transCashback.Any())
                            {
                                double totalPoints = transCashback.Sum(x => x.Total);
                                user.Profile.AccumulatePoints(
                                    totalPoints,
                                    item.First().Document,
                                    item.First().OperationId,
                                    new Guid(IUserSession.UserVirtualCode));
                                hasInvoice = true;
                            }
                            if (hasInvoice)
                            {
                                user.Profile.SumTotalAccumulatedInvoice();
                            }

                        }
                        receipt.RegisterInvoice(listInvoice, new Guid(IUserSession.UserVirtualCode));
                    }
                    receipt.SyncReceipt(new Guid(IUserSession.UserVirtualCode));
                }
            }

            return Task.CompletedTask;
        }

        private Task ProcessInvoices(
            IEnumerable<User> users,
            IEnumerable<ResponseInvoice> invoicesToSave,
            Receipt receipt)
        {
            foreach (var user in users)
            {
                //agrupo items para formar una factura por identificacion
                var invoiceInReceipt = invoicesToSave
                    .Where(x => x.Identification == user.Identification)
                    .GroupBy(x => x.Document);

                foreach (var item in invoiceInReceipt)
                {
                    List<Invoice> listInvoice = new List<Invoice>();
                    List<InvoiceDetail> listDetail = new List<InvoiceDetail>();
                    foreach (var itemDetail in item)
                    {
                        var isArrivalDateOk = DateTime.TryParse(itemDetail.ArrivalDate, out var arrivalDate);

                        var isDepartureDateOk = DateTime.TryParse(itemDetail.DepartureDate, out var departureDate);

                        Result<InvoiceDetail> ensureInvoiceDetailCreated = InvoiceDetail.Create(
                            itemDetail.Group,
                            groupDetail: itemDetail.Environment.ToString() ?? string.Empty,
                            itemDetail.Total,
                            isArrivalDateOk ? arrivalDate : null,
                            isDepartureDateOk ? departureDate : null,
                            itemDetail.TotalDays,
                            itemDetail.TransactionType,
                            new Guid(IUserSession.UserVirtualCode));
                        if (ensureInvoiceDetailCreated.IsSuccess)
                        {
                            listDetail.Add(ensureInvoiceDetailCreated.Value);
                        }
                    }

                    //verifico si la factura es solo cashback, noches o mixta
                    int invoiceTypeCount = item.Select(x => x.TransactionType).Distinct().Count();

                    var invoiceType = invoiceTypeCount == 1 ? InvoiceType.From(item.First().TransactionType) : InvoiceType.Mixed;

                    Result<Invoice> ensureInvoiceCreated = Invoice.Create(
                        item.First().Document,
                        item.First().DateOfIssue,
                        user.Id,
                        item.First().OperationId,
                        item.First().Identification,
                        user.Identification,
                        invoiceType,
                        listDetail,
                        new Guid(IUserSession.UserVirtualCode));
                    if (ensureInvoiceCreated.IsSuccess)
                    {
                        listInvoice.Add(ensureInvoiceCreated.Value);
                        //si existe noches a acumular
                        var transNights = item.Where(x => x.TransactionType == 2).AsEnumerable();
                        //si existe cashback a acumular
                        var transCashback = item.Where(x => x.TransactionType == 1);
                        bool hasInvoice = false;
                        if (transNights.Any())
                        {
                            int totalDays = transNights.Sum(x => x.TotalDays);
                            user.Profile.AccumulateNights(
                                totalDays,
                                item.First().Document,
                                item.First().OperationId,
                                new Guid(IUserSession.UserVirtualCode));

                            hasInvoice = true;
                        }
                        if (transCashback.Any())
                        {
                            double totalPoints = transCashback.Sum(x => x.Total);
                            user.Profile.AccumulatePoints(
                                totalPoints,
                                item.First().Document,
                                item.First().OperationId,
                                new Guid(IUserSession.UserVirtualCode));
                            hasInvoice = true;
                        }

                        if (hasInvoice)
                        {
                            user.Profile.SumTotalAccumulatedInvoice();
                        }
                        receipt.RegisterInvoice(listInvoice, new Guid(IUserSession.UserVirtualCode));
                    }
                }
            }

            return Task.CompletedTask;
        }

    }

}