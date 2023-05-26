using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.Shared.Contracts;

namespace Signaturit.LobbyWars.LargerSumStrategy.Models
{
    internal class Sentence:ISentence
    {
        public Sentence(IContract plaintiffContractContract, IContract defendantContractContract, SentenceResult result)
        {
            PlaintiffContract = plaintiffContractContract;
            DefendantContract = defendantContractContract;
            Result = result;
        }
        public IContract PlaintiffContract { get;}
        public IContract DefendantContract { get;}
        public SentenceResult Result { get;}
    }
}
