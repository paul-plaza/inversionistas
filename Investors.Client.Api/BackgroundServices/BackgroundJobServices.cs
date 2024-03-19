using Hangfire;
using Investors.Client.Users.Domain.BackgroundServices;

namespace Investors.Client.Api.BackgroundServices
{
    public static class BackgroundJobServices
    {
        public static void ConfigureJobs()
        {
            RecurringJob.AddOrUpdate<RegisterInvoiceDetailsJob>(
                recurringJobId: "Sincroniza facturas de inversionistas desde Satcom",
                methodCall: x => x.RegisterInvoicesForInvestors(CancellationToken.None),
                cronExpression: "0 8,14,20,23 * * *",
                options:
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local
                });

            RecurringJob.AddOrUpdate<RegisterInvoiceDetailsJob>(
                recurringJobId: "Sincroniza facturas de referidos desde Satcom",
                methodCall: x => x.RegisterInvoiceByReceipts(CancellationToken.None),
                cronExpression: "0 20 */2 * *",
                options:
                new RecurringJobOptions
                {
                    TimeZone = TimeZoneInfo.Local
                });

        }
    }
}