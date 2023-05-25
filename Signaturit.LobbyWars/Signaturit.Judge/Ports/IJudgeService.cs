namespace Signaturit.Judge.Ports
{
    public interface IJudgeService
    {
        ISentence GetSentence(IContract contract);
    }
}
