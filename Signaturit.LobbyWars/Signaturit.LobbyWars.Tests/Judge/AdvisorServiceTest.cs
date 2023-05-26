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
        public void GetMissingSignatureToWin_without_kings_works()
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
        [Fact]
        public void GetMissingSignatureToWin_with_kings_works()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                null,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.King,
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
        [Fact]
        public void GetMissingSignatureToWin_impossible_to_win()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                null,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.King,
                SignatureTypes.King,
                SignatureTypes.King
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
            FluentActions
                .Invoking(() =>
                    advisor.GetMissingSignatureToWin(plaintiffContractMoq.Object, defendantContractMoq.Object)).Should()
                .Throw<InvalidOperationException>();
        }
        [Fact]
        public void GetMissingSignatureToWin_when_alredy_wins()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                null,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Validator,
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
                .BeNull();
        }

        [Fact]
        public void GetMissingSignatureToWin_ownContract_has_more_than_one_empty()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                null,
                null
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Validator,
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
            FluentActions
                .Invoking(() => advisor.GetMissingSignatureToWin(plaintiffContractMoq.Object, defendantContractMoq.Object)).Should()
                .Throw<ArgumentException>().WithMessage("The own contract can only have one empty signature");
        }

        [Fact]
        public void GetMissingSignatureToWin_ownContract_has_not_empty_signature()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                SignatureTypes.Notary,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Validator,
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
            FluentActions
                .Invoking(() => advisor.GetMissingSignatureToWin(plaintiffContractMoq.Object, defendantContractMoq.Object)).Should()
                .Throw<ArgumentException>().WithMessage("The own contract must have an empty signature");
        }

        [Fact]
        public void GetMissingSignatureToWin_oppositionContract_has_empty_signature()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var defendantContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(2)
            {
                SignatureTypes.Notary,
                null,
                SignatureTypes.Notary
            };
            var defendantSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.Validator,
                null,
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
            FluentActions
                .Invoking(() => advisor.GetMissingSignatureToWin(plaintiffContractMoq.Object, defendantContractMoq.Object)).Should()
                .Throw<ArgumentException>().WithMessage("The opposition contract cannot have empty signatures");
        }
    }
}
