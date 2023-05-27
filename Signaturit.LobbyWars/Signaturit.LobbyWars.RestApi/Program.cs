using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Services;
using Signaturit.LobbyWars.LargerSumStrategy.Services;
using Signaturit.LobbyWars.RestApi;
using Signaturit.LobbyWars.Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var signatureWeights = GetSignatureTypesWeight(builder.Configuration);

builder.Services.AddSingleton<ISignatureWeights>(signatureWeights);
builder.Services.AddSingleton<ISentencingStrategy, LargerSumService>();
builder.Services.AddSingleton<IJudgeService, JudgeService>();
builder.Services.AddSingleton<IAdvisorService, AdvisorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/JudgeService/GetSentence/{plaintiffSignatures}/{defendantSignatures}", (string plaintiffSigantures,string defendantSignatures, IJudgeService judgeService, ILoggerFactory loggerFactory) =>
{
    //logic that transforms the entries and uses JudgeService.GetSentence and returns the winner.
})
.WithName("GetSentence")
.WithOpenApi();

app.MapGet("/AdvisorService/GetMissingWinnerSignature/{ownSignatures}/{oppositionSignatures}", (string ownSigantures, string oppositionSignatures, IAdvisorService judgeService, ILoggerFactory loggerFactory) =>
    {
        //Logic that transforms the inputs and uses AdvisorService to get the missing value to win and return it.
    })
    .WithName("GetMissingWinnerSignature")
    .WithOpenApi();

app.Run();
SignatureWeights GetSignatureTypesWeight(IConfiguration configuration)
{
    var dic = configuration.GetSection("SignatureTypesWeight")
                  .Get<SignatureWeightsSettings>()?
                  .SignatureWeightPairs
                  .ToDictionary(k => k.Key, v => v.Value)
              ?? throw new ArgumentNullException("SignatureTypesWeight not configurated.");

    return new SignatureWeights()
    {
        WeightsPerSignature = dic
    };
}

