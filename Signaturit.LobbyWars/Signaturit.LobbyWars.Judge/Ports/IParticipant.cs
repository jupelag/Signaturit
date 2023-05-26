using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface IParticipant
    {
        string? Name { get; }
        List<SignatureTypes> Signatures { get; set; }
    }
}
