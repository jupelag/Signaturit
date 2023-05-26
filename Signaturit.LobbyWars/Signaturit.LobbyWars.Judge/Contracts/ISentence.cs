using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.Shared.Contracts;

namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface ISentence
    {
        public IContract PlaintiffContract { get; }
        public IContract DefendantContract { get; }
        SentenceResult Result { get; }
    }
}
