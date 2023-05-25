namespace Signaturit.Judge.Ports
{
    public interface ISentence:IContract
    {
        IParticipant Winner { get; set; }
    }
}
