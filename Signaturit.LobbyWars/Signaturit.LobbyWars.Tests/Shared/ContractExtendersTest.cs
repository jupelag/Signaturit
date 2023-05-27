using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;
using Signaturit.LobbyWars.Shared.Extenders;
using Xunit;

namespace Signaturit.LobbyWars.Tests.Shared
{
    public class ContractExtendersTest
    {
        [Fact]
        public void CleanSignatures()
        {
            var plaintiffContractMoq = new Mock<IContract>();
            var plaintiffSignatures = new List<SignatureTypes?>(3)
            {
                SignatureTypes.King,
                SignatureTypes.Notary,
                SignatureTypes.Validator
            };
            plaintiffContractMoq.Setup(p => p.Signatures).Returns(plaintiffSignatures);
            var cleanedSignatures = plaintiffContractMoq.Object.CleanSignatures(SignatureTypes.King, SignatureTypes.Validator);
            cleanedSignatures.Count().Should().Be(2);
            cleanedSignatures.Should().NotContain(SignatureTypes.Validator);
        }
    }
}
