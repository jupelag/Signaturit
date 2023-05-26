using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.Judge.Ports;
using Signaturit.LobbyWars.LargerSumStrategy.Ports;
using Signaturit.LobbyWars.LargerSumStrategy.Services;
using Xunit;

namespace Signaturit.LobbyWars.Tests.LargerSumStrategy
{
    public class LargerSumStrategyTest
    {
        [Fact]
        public void GetSentece_when_signatures_not_contains_king()
        {
            var plaintiffMoq = new Mock<IParticipant>();
            var defendantMoq = new Mock<IParticipant>();
            var plaintiffSignatures = new List<SignatureTypes>(2)
            {
                SignatureTypes.King,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                SignatureTypes.Validator
            };
            plaintiffMoq.Setup(p => p.Signatures).Returns(plaintiffSignatures);
            defendantMoq.Setup(p => p.Signatures).Returns(defendantSignatures);

            var contractMoq = new Mock<IContract>();
            contractMoq.Setup(c=>c.Plaintiff).Returns(plaintiffMoq.Object);
            contractMoq.Setup(c => c.Defendant).Returns(defendantMoq.Object);

            var signatureTypesWeights = new Dictionary<SignatureTypes, int>()
            {
                { SignatureTypes.King, 5 },
                { SignatureTypes.Notary, 2 },
                { SignatureTypes.Validator, 1 }
            };
            var weightsMoq = new Mock<ISignatureWeights>();
            weightsMoq.Setup(w => w.WeightsPerSignature).Returns(signatureTypesWeights);

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(contractMoq.Object);
            sentence.Winner.Should().Be(plaintiffMoq.Object);
        }

        [Fact]
        public void GetSentence_key_weight_not_configured_throws_exception()
        {
            var plaintiffMoq = new Mock<IParticipant>();
            var defendantMoq = new Mock<IParticipant>();
            var plaintiffSignatures = new List<SignatureTypes>(2)
            {
                SignatureTypes.King,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                SignatureTypes.Validator
            };
            plaintiffMoq.Setup(p => p.Signatures).Returns(plaintiffSignatures);
            defendantMoq.Setup(p => p.Signatures).Returns(defendantSignatures);

            var contractMoq = new Mock<IContract>();
            contractMoq.Setup(c => c.Plaintiff).Returns(plaintiffMoq.Object);
            contractMoq.Setup(c => c.Defendant).Returns(defendantMoq.Object);

            var signatureTypesWeights = new Dictionary<SignatureTypes, int>()
            {
                { SignatureTypes.King, 5 },
                { SignatureTypes.Validator, 1 }
            };
            var weightsMoq = new Mock<ISignatureWeights>();
            weightsMoq.Setup(w => w.WeightsPerSignature).Returns(signatureTypesWeights);
            var service = new LargerSumService(weightsMoq.Object);
            FluentActions.Invoking(()=>service.GetSentence(contractMoq.Object)).Should().Throw<KeyNotFoundException>();
        }
    }
}
