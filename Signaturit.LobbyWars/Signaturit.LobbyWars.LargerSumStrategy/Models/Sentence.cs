using Signaturit.LobbyWars.Judge.Enumerations;
using Signaturit.LobbyWars.Judge.Ports;

namespace Signaturit.LobbyWars.LargerSumStrategy.Models
{
    internal class Sentence:ISentence
    {
        public Sentence(IParticipant plaintiff, IParticipant defendant, SentenceResult result)
        {
            Plaintiff = plaintiff;
            Defendant = defendant;
            Result = result;
        }
        public IParticipant Plaintiff { get;}
        public IParticipant Defendant { get;}
        public SentenceResult Result { get;}
    }
}
