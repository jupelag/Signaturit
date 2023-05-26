using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.Shared.Contracts
{
    public interface ISignatureWeights
    {
        IDictionary<SignatureTypes, int> WeightsPerSignature { get; set; }
    }
}
