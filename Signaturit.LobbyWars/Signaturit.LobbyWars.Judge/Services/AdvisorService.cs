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
            ValidateContracts(ownContract, oppositionContract);

            return GetMissingWinnerSignature(ownContract, oppositionContract);
        }

        private SignatureTypes? GetMissingWinnerSignature(IContract ownContract, IContract oppositionContract)
        {
            var ownWeight = GetWeight(ownContract);
            var oppositionWeigth = GetWeight(oppositionContract);

            var difference = oppositionWeigth - ownWeight;
            if (difference < 0) return null;

            var signatureTypesPerWeight =
                _signatureWeights.WeightsPerSignature.Keys.ToDictionary(k => _signatureWeights.WeightsPerSignature[k]);

            var immediatelyHeavierWeight = _signatureWeights.WeightsPerSignature.Values.OrderBy(v => v).FirstOrDefault(v => v > difference);

            if (immediatelyHeavierWeight == 0)
            {
                throw new InvalidOperationException("It is not possible to overcome the opponent");
            }

            return signatureTypesPerWeight[immediatelyHeavierWeight];
        }

        private static void ValidateContracts(IContract ownContract, IContract oppositionContract)
        {
            ValidateOwnContract(ownContract);
            ValidateOppositionContract(oppositionContract);
        }

        private int GetWeight(IContract contract)
        {
            return contract.CleanSignatures(SignatureTypes.King)
                .Sum(s => s.GetSignatureWeight(_signatureWeights));
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
