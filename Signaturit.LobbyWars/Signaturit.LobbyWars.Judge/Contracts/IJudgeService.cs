﻿using Signaturit.LobbyWars.Shared.Contracts;

namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface IJudgeService
    {
        ISentence GetSentence(IContract plaintiffContract, IContract defendantContract);
    }
}
