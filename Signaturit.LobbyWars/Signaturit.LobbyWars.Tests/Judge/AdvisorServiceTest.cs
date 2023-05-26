using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Judge.Services;
using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;
using Xunit;

namespace Signaturit.LobbyWars.Tests.Judge
{
    public class AdvisorServiceTest
    {
        [Fact]
        public void GetMissingSignatureToWin_works()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                null,
                SignatureTypes.Validator
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Validator,
                SignatureTypes.Validator
            };
            plaintiffContractMoq.Setup(p => p.Signatures).Returns(plaintiffSignatures);
            defendantContractMoq.Setup(p => p.Signatures).Returns(defendantSignatures);

            var signatureTypesWeights = new Dictionary<SignatureTypes, int>()
            {
                { SignatureTypes.King, 5 },
                { SignatureTypes.Notary, 2 },
                { SignatureTypes.Validator, 1 }
            };
            var weightsMoq = new Mock<ISignatureWeights>();
            weightsMoq.Setup(w => w.WeightsPerSignature).Returns(signatureTypesWeights);

            var advisor = new AdvisorService(weightsMoq.Object);
            advisor.GetMissingSignatureToWin(plaintiffContractMoq.Object, defendantContractMoq.Object).Should()
                .Be(SignatureTypes.Notary);
        }
    }
}
