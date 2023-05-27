using Signaturit.LobbyWars.Judge.Contracts;

namespace Signaturit.LobbyWars.WorkerService
{
    public class Worker : IHostedService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IJudgeService _judgeService;
        private readonly IAdvisorService _advisorService;

        public Worker(ILogger<Worker> logger, IJudgeService judgeService, IAdvisorService advisorService)
        {
            _logger = logger;
            _judgeService = judgeService;
            _advisorService = advisorService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Console program that receives data, transforms it and consumes _judgeService and _advisorService
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}