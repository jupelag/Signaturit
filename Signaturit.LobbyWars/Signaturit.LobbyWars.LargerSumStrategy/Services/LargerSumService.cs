using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.LargerSumStrategy.Contracts;
using Signaturit.LobbyWars.LargerSumStrategy.Models;

namespace Signaturit.LobbyWars.LargerSumStrategy.Services
{
    public class LargerSumService:ISentencingStrategy
    {
        private readonly ISignatureWeights _signatureWeights;

        public LargerSumService(ISignatureWeights signatureWeights)
        {
            _signatureWeights = signatureWeights;
        }

        public ISentence GetSentence(IContract plaintiffContract, IContract defendanContract)
        {
            var cleanedPlaintiffSignatures = CleanContractSignatures(plaintiffContract, SignatureTypes.Validator);
            var cleanedDefendantSignatures = CleanContractSignatures(defendanContract,SignatureTypes.Validator);

            var plaintiffSignaturesWeight = cleanedPlaintiffSignatures.Sum(GetSignatureWeight);
            var defendantSignaturesWeight = cleanedDefendantSignatures.Sum(GetSignatureWeight);
            
            var result = GetWinner(plaintiffSignaturesWeight, defendantSignaturesWeight);

            return new Sentence(plaintiffContract, defendanContract, result);
        }

        private static SentenceResult GetWinner(int plaintiffSignaturesWeight, int defendantSignaturesWeight)
        {
            var winner = plaintiffSignaturesWeight > defendantSignaturesWeight
                ? SentenceResult.Plaintiff
                : SentenceResult.Defendant;
            if (plaintiffSignaturesWeight == defendantSignaturesWeight) winner = SentenceResult.Draw;
            return winner;
        }

        private int GetSignatureWeight(SignatureTypes signatureType)
        {
            if (_signatureWeights.WeightsPerSignature.TryGetValue(signatureType, out var weight))
            {
                return weight;
            }
            throw new KeyNotFoundException($"Weight for {signatureType} not configured.");
        }

        private static IEnumerable<SignatureTypes> CleanContractSignatures(IContract contract, SignatureTypes typeCleaned)
        {
            if (!contract.Signatures.Contains(SignatureTypes.King)) return contract.Signatures;

            var signaturesCleaned = contract.Signatures.Where(s => s != typeCleaned).ToArray();
            var newList = new List<SignatureTypes>(signaturesCleaned.Length);
            newList.AddRange(signaturesCleaned);
            return newList;
        }
    }
}
