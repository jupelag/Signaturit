namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface ISentencingStrategy
    {
        public ISentence GetSentence(IContract contract);
    }
}
