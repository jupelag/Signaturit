using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;
using Signaturit.LobbyWars.Shared.Extenders;

namespace Signaturit.LobbyWars.Judge.Services
{
    public class AdvisorService:IAdvisorService
    {
        private readonly ISignatureWeights _signatureWeights;

        public AdvisorService(ISignatureWeights signatureWeights)
        {
            _signatureWeights = signatureWeights;
        }
        public SignatureTypes? GetMissingSignatureToWin(IContract ownContract, IContract oppositionContract)
        {
            ValidateOwnContract(ownContract);
            ValidateOppositionContract(oppositionContract);

            var ownWeight = ownContract.CleanSignatures(SignatureTypes.King)
                .Sum(s => s.GetSignatureWeight(_signatureWeights));
            var oppositionWeigth = oppositionContract.CleanSignatures(SignatureTypes.King)
                .Sum(s => s.GetSignatureWeight(_signatureWeights));

            var difference = oppositionWeigth - ownWeight;
            if (difference < 0) return null;

            var signatureTypesPerWeight =
                _signatureWeights.WeightsPerSignature.Keys.ToDictionary(k => _signatureWeights.WeightsPerSignature[k]);

            var majorWeight = _signatureWeights.WeightsPerSignature.Values.OrderBy(v=>v).FirstOrDefault(v => v > difference);

            if (majorWeight == 0)
            {
                throw new InvalidOperationException("It is not possible to overcome the opponent");
            }
            return signatureTypesPerWeight[majorWeight];
        }

        private static void ValidateOppositionContract(IContract oppositionContract)
        {
            if (oppositionContract.Signatures.Any(s => s == null))
            {
                throw new ArgumentException("The opposition contract cannot have empty signatures");
            }
        }

        private static void ValidateOwnContract(IContract ownContract)
        {
            var numberOfNulls = ownContract.Signatures.Count(s => s == null);
            switch (numberOfNulls)
            {
                case > 1:
                    throw new ArgumentException("The own contract can only have one empty signature");
                case 0:
                    throw new ArgumentException("The own contract must have an empty signature");
            }
        }
    }
}
