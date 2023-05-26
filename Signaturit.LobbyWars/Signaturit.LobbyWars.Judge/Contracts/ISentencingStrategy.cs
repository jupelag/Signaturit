namespace Signaturit.LobbyWars.Judge.Contracts
{
    public interface ISentencingStrategy
    {
        public ISentence GetSentence(IContract plaintiffContract, IContract defendanContract);
    }
}
