using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.Judge.Ports;
using Signaturit.LobbyWars.LargerSumStrategy.Models;
using Signaturit.LobbyWars.LargerSumStrategy.Ports;

namespace Signaturit.LobbyWars.LargerSumStrategy.Services
{
    public class LargerSumService:ISentencingStrategy
    {
        private readonly ISignatureWeights _signatureWeights;

        public LargerSumService(ISignatureWeights signatureWeights)
        {
            _signatureWeights = signatureWeights;
        }

        public ISentence GetSentence(IContract contract)
        {
            var cleanedPlaintiffSignatures = CleanParticipantSignatures(contract.Plaintiff);
            var cleanedDefendantSignatures = CleanParticipantSignatures(contract.Defendant);

            var plaintiffSignaturesWeight = cleanedPlaintiffSignatures.Sum(GetSignatureWeight);
            var defendantSignaturesWeight = cleanedDefendantSignatures.Sum(GetSignatureWeight);
            
            var result = GetWinner(plaintiffSignaturesWeight, defendantSignaturesWeight);

            return new Sentence(contract.Plaintiff, contract.Defendant, result);
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

        private static IEnumerable<SignatureTypes> CleanParticipantSignatures(IParticipant participant)
        {
            if (!participant.Signatures.Contains(SignatureTypes.King)) return participant.Signatures;

            var signaturesWithoutValidator = participant.Signatures.Where(s => s != SignatureTypes.Validator).ToArray();
            var newList = new List<SignatureTypes>(signaturesWithoutValidator.Length);
            newList.AddRange(signaturesWithoutValidator);
            return newList;
        }
    }
}
