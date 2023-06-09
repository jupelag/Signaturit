﻿using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface IAdvisorService
    {
        public SignatureTypes? GetMissingSignatureToWin(IContract ownContract, IContract oppositionContract);
    }
}
