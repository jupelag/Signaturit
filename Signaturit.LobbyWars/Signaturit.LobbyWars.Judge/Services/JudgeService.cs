using Signaturit.LobbyWars.Judge.Ports;

namespace Signaturit.LobbyWars.Judge.Services
{
    public class JudgeService:IJudgeService
    {
        private readonly ISentencingStrategy _sentencingStrategy;

        public JudgeService(ISentencingStrategy sentencingStrategy)
        {
            _sentencingStrategy = sentencingStrategy;
        }
        public ISentence GetSentence(IContract contract)
        {
            throw new NotImplementedException();
        }
    }
}
