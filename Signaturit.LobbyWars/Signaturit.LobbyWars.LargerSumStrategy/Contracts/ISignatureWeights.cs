using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.LargerSumStrategy.Ports
{
    public interface ISignatureWeights
    {
        IDictionary<SignatureTypes, int> WeightsPerSignature { get; set; }
    }
}
