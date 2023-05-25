namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface IContract
    {
        IParticipant Plaintiff { get; set; }
        IParticipant Defendant { get; set; }
    }
}
