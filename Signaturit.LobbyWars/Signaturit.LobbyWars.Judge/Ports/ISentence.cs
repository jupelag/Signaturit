using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface ISentence:IContract
    {
        SentenceResult Result { get; }
    }
}
