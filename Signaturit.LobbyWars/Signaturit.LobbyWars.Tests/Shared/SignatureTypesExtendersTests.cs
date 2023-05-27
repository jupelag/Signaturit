using Signaturit.LobbyWars.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Extenders;
using Xunit;

namespace Signaturit.LobbyWars.Tests.Shared
{
    public class SignatureTypesExtendersTests
    {
        [Fact]
        public void GetSignatureWeight()
        {
            var signatureTypesWeights = new Dictionary<SignatureTypes, int>()
            {
                { SignatureTypes.King, 5 },
                { SignatureTypes.Notary, 2 },
                { SignatureTypes.Validator, 1 }
            };
            var signatureWeightsMock = new Mock<ISignatureWeights>();
            signatureWeightsMock.Setup(x => x.WeightsPerSignature).Returns(signatureTypesWeights);
            SignatureTypes.King.GetSignatureWeight(signatureWeightsMock.Object).Should().Be(5);
            SignatureTypes.Notary.GetSignatureWeight(signatureWeightsMock.Object).Should().Be(2);
            SignatureTypes.Validator.GetSignatureWeight(signatureWeightsMock.Object).Should().Be(1);
        }
        [Fact]
        public void GetSignatureWeight_key_not_found()
        {
            var signatureTypesWeights = new Dictionary<SignatureTypes, int>()
            {
                { SignatureTypes.King, 5 },
                { SignatureTypes.Notary, 2 },
            };
            var signatureWeightsMock = new Mock<ISignatureWeights>();
            signatureWeightsMock.Setup(x => x.WeightsPerSignature).Returns(signatureTypesWeights);
            FluentActions.Invoking(()=> SignatureTypes.Validator.GetSignatureWeight(signatureWeightsMock.Object)).Should().Throw<KeyNotFoundException>();
        }
    }
}
