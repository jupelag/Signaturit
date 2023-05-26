using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.Shared.Extenders
{
    public static class SignatureTypesExtender
    {
        public static int GetSignatureWeight(this SignatureTypes signatureType, ISignatureWeights signatureWeights)
        {
            if (signatureWeights.WeightsPerSignature.TryGetValue(signatureType, out var weight))
            {
                return weight;
            }
            throw new KeyNotFoundException($"Weight for {signatureType} not configured.");
        }
    }
}
