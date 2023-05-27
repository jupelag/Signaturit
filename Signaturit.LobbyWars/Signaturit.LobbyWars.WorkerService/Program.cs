using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Services;
using Signaturit.LobbyWars.LargerSumStrategy.Services;
using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.WorkerService;
using Signaturit.LobbyWars.WorkerService.Models;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((host,services) =>
    {
        var signatureWeights = GetSignatureTypesWeight(host.Configuration);

        services.AddSingleton<ISignatureWeights>(signatureWeights);
        services.AddSingleton<ISentencingStrategy, LargerSumService>();
        services.AddSingleton<IJudgeService, JudgeService>();

        services.AddSingleton<IAdvisorService, AdvisorService>();

        services.AddHostedService<Worker>();

    })
    .Build();

host.Run();

 SignatureWeights GetSignatureTypesWeight(IConfiguration configuration)
{
    var dic= configuration.GetSection("SignatureTypesWeight")
                 .Get<SignatureWeightsSettings>()?
                 .SignatureWeightPairs
                 .ToDictionary(k => k.Key, v => v.Value)
           ?? throw new ArgumentNullException("SignatureTypesWeight not configurated.");

    return new SignatureWeights()
    {
        WeightsPerSignature = dic
    };
}
