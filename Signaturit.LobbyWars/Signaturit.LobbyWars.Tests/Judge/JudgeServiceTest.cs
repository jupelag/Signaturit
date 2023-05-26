using FluentAssertions;
using Moq;
using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.Judge.Ports;
using Signaturit.LobbyWars.LargerSumStrategy.Ports;
using Signaturit.LobbyWars.LargerSumStrategy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signaturit.LobbyWars.Judge.Services;
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
            strategyMock.Setup(s => s.GetSentence(It.IsAny<IContract>())).Returns(sentence);

            var contract = Mock.Of<IContract>();

            var service = new JudgeService(strategyMock.Object);
            service.GetSentence(contract).Should().NotBeNull();
            service.GetSentence(contract).Should().Be(sentence);
        }
    }
}
