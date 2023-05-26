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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contract"></param>
        /// <returns>Null if draw</returns>
        public ISentence? GetSentence(IContract contract)
        {
            var cleanedPlaintiffSignatures = CleanParticipantSignatures(contract.Plaintiff);
            var cleanedDefendantSignatures = CleanParticipantSignatures(contract.Defendant);

            var plaintiffSignaturesWeight = cleanedPlaintiffSignatures.Sum(s => _signatureWeights.WeightsPerSignature[s]);
            var defendantSignaturesWeight = cleanedDefendantSignatures.Sum(s => _signatureWeights.WeightsPerSignature[s]);
            if (plaintiffSignaturesWeight == defendantSignaturesWeight) return null;

            var winner = plaintiffSignaturesWeight > defendantSignaturesWeight ? contract.Plaintiff : contract.Defendant;
            return new Sentence(contract.Plaintiff, contract.Defendant, winner);
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
