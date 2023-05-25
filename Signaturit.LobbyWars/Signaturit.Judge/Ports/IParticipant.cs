using Signaturit.Judge.Enumerations;

namespace Signaturit.Judge.Ports
{
    public interface IParticipant
    {
        List<SignatureTypes> Signatures { get; set; }
    }
}
