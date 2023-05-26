using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface ISentence
    {
        public IContract PlaintiffContract { get; }
        public IContract DefendantContract { get; }
        SentenceResult Result { get; }
    }
}
