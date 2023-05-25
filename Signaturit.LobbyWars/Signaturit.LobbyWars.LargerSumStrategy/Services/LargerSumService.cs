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
            return null;
        }
    }
}
