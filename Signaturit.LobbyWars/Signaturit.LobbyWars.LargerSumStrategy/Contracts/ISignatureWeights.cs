using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.LargerSumStrategy.Contracts
{
    public interface ISignatureWeights
    {
        IDictionary<SignatureTypes, int> WeightsPerSignature { get; set; }
    }
}
