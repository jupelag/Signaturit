using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Judge.Contracts;
using Signaturit.LobbyWars.Judge.Services;
using Signaturit.LobbyWars.Shared.Contracts;
using Xunit;

namespace Signaturit.LobbyWars.Tests.Judge
{
    public class JudgeServiceTest
    {
        [Fact]
        public void GetSentence_works_from_strategy()
        {
            var strategyMock = new Mock<ISentencingStrategy>();
            var sentence = Mock.Of<ISentence>();
            strategyMock.Setup(s => s.GetSentence(It.IsAny<IContract>(),It.IsAny<IContract>())).Returns(sentence);

            var contract = Mock.Of<IContract>();

            var service = new JudgeService(strategyMock.Object);
            service.GetSentence(contract,contract).Should().NotBeNull();
            service.GetSentence(contract,contract).Should().Be(sentence);
        }
    }
}
