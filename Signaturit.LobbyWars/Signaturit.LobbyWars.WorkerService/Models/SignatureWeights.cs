using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.WorkerService.Models
{
    public class SignatureWeights : ISignatureWeights
    {
        public required IDictionary<SignatureTypes, int> WeightsPerSignature { get; set; }
    }
}
