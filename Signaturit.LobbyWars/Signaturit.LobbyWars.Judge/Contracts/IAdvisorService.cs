using Signaturit.LobbyWars.Judge.Enumerations;

namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface IAdvisorService
    {
        public SignatureTypes GetMissingSignatureToWin(IContract ownContract, IContract oppositionContract);
    }
}
