using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.LargerSumStrategy.Models;
using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;
using Signaturit.LobbyWars.Shared.Extenders;

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
            var cleanedPlaintiffSignatures = plaintiffContract.CleanSignatures(SignatureTypes.Validator);
            var cleanedDefendantSignatures = defendanContract.CleanSignatures(SignatureTypes.Validator);

            var plaintiffSignaturesWeight = cleanedPlaintiffSignatures.Sum(s=>s.GetSignatureWeight(_signatureWeights));
            var defendantSignaturesWeight = cleanedDefendantSignatures.Sum(s => s.GetSignatureWeight(_signatureWeights));
            
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
    }
}
