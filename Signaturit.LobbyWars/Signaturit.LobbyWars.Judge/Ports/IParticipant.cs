using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface IParticipant
    {
        List<SignatureTypes> Signatures { get; set; }
    }
}
