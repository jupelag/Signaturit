namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface IJudgeService
    {
        ISentence GetSentence(IContract contract);
    }
}
