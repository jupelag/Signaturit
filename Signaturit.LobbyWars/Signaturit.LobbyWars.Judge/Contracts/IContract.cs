using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface IContract
    {
        List<SignatureTypes?> Signatures { get; }
    }
}
