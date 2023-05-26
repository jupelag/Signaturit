using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.Shared.Contracts
{
    public interface IContract
    {
        IList<SignatureTypes?> Signatures { get; }
    }
}
