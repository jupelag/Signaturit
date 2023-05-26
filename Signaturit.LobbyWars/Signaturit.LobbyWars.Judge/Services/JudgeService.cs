using Signaturit.LobbyWars.Judge.Contracts;

namespace Signaturit.LobbyWars.Judge.Services
{
    public class JudgeService:IJudgeService
    {
        private readonly ISentencingStrategy _sentencingStrategy;

        public JudgeService(ISentencingStrategy sentencingStrategy)
        {
            _sentencingStrategy = sentencingStrategy;
        }

        public ISentence GetSentence(IContract plaintiffContract, IContract defendantContract)
        {
            return _sentencingStrategy.GetSentence(plaintiffContract,defendantContract);
        }
    }
}
