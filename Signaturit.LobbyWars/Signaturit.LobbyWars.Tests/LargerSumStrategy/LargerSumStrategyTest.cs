using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.LargerSumStrategy.Contracts;
using Signaturit.LobbyWars.LargerSumStrategy.Services;
using Xunit;

namespace Signaturit.LobbyWars.Tests.LargerSumStrategy
{
    public class LargerSumStrategyTest
    {
        [Fact]
        public void GetSentece_when_signatures_contains_king_but_not_validator()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.King,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
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

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(plaintiffContractMoq.Object,defendantContractMoq.Object);
            sentence.Result.Should().Be(SentenceResult.Plaintiff);
        }

        [Fact]
        public void GetSentece_when_signatures_contains_king_and_validator()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.King,
                SignatureTypes.Validator,
                SignatureTypes.Validator
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                SignatureTypes.Notary
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

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(plaintiffContractMoq.Object, defendantContractMoq.Object);
            sentence.Result.Should().Be(SentenceResult.Defendant);
        }

        [Fact]
        public void GetSentence_key_weight_not_configured_throws_exception()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.King,
                SignatureTypes.Validator,
                SignatureTypes.Validator
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                SignatureTypes.Notary
            };
            plaintiffContractMoq.Setup(p => p.Signatures).Returns(plaintiffSignatures);
            defendantContractMoq.Setup(p => p.Signatures).Returns(defendantSignatures);

            var signatureTypesWeights = new Dictionary<SignatureTypes, int>()
            {
                { SignatureTypes.King, 5 },
                { SignatureTypes.Validator, 1 }
            };
            var weightsMoq = new Mock<ISignatureWeights>();
            weightsMoq.Setup(w => w.WeightsPerSignature).Returns(signatureTypesWeights);
            var service = new LargerSumService(weightsMoq.Object);
            FluentActions.Invoking(()=>service.GetSentence(plaintiffContractMoq.Object, defendantContractMoq.Object)).Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void GetSentence_plaintiff_win_without_king()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                SignatureTypes.Validator,
                SignatureTypes.Validator
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
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

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(plaintiffContractMoq.Object, defendantContractMoq.Object);
            sentence.Result.Should().Be(SentenceResult.Plaintiff);
        }
        [Fact]
        public void GetSentence_defendant_win_without_king()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
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

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(plaintiffContractMoq.Object, defendantContractMoq.Object);
            sentence.Result.Should().Be(SentenceResult.Defendant);
        }

        [Fact]
        public void GetSentences_is_a_draw()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
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

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(plaintiffContractMoq.Object, defendantContractMoq.Object);
            sentence.Result.Should().Be(SentenceResult.Draw);
        }

        [Fact]
        public void GetSentences_null_type_sums_zero()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                null
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                null
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

            var service = new LargerSumService(weightsMoq.Object);
            var sentence = service.GetSentence(plaintiffContractMoq.Object, defendantContractMoq.Object);
            sentence.Result.Should().Be(SentenceResult.Draw);
        }
    }
}
