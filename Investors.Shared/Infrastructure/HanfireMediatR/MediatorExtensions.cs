using Hangfire;
using MediatR;

namespace Investors.Shared.Infrastructure.HanfireMediatR
{
    public static class MediatorExtensions
    {
        public static void Enqueue(this IMediator mediator, string jobName, IRequest request)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatorHangfireBridge>(x => x.Send(jobName, request));
        }

        public static void Enqueue(this IMediator mediator, string jobName, INotification request)
        {
            var backgroundJobClient = new BackgroundJobClient();
            backgroundJobClient.Enqueue<MediatorHangfireBridge>(x => x.Send(jobName, request));
        }
    }
}

