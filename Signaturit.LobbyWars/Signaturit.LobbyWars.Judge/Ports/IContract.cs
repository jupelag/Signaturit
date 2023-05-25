namespace Signaturit.LobbyWars.Judge.Ports
{
    public interface IContract
    {
        IParticipant Plaintiff { get;}
        IParticipant Defendant { get;}
    }
}
