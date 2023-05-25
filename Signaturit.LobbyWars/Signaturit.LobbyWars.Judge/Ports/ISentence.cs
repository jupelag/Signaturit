namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface ISentence:IContract
    {
        IParticipant Winner { get; }
    }
}
