using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.RestApi
{
    public class SignatureWeights:ISignatureWeights
    {
        public required IDictionary<SignatureTypes, int> WeightsPerSignature { get; set; }
    }
}
