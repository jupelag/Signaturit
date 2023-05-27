using System.Text.Json.Serialization;
using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.RestApi
{
    public class SignatureWeightPair
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SignatureTypes Key { get; set; }
        public int Value { get; set; }
    }
}
