using Signaturit.Judge.Enumerations;

namespace Signaturit.Judge.Ports
{
    public interface ISignatureWeights
    {
        IDictionary<SignatureTypes, int> WeightsPerSignature { get; set; }
    }
}
