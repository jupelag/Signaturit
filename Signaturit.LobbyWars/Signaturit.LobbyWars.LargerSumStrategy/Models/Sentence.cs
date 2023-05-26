using Signaturit.LobbyWars.Judge.Ports;

namespace Signaturit.LobbyWars.LargerSumStrategy.Models
{
    internal class Sentence:ISentence
    {
        public Sentence(IParticipant plaintiff, IParticipant defendant, IParticipant winner)
        {
            Plaintiff = plaintiff;
            Defendant = defendant;
            Winner = winner;
        }
        public IParticipant Plaintiff { get;}
        public IParticipant Defendant { get;}
        public IParticipant Winner { get;}
    }
}
