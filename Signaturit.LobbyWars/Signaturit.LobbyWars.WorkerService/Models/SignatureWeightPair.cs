using Signaturit.LobbyWars.Shared.Enumerations;
using System.Text.Json.Serialization;

namespace Signaturit.LobbyWars.WorkerService.Models
{
    public class SignatureWeightPair
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SignatureTypes Key { get; set; }
        public int Value { get; set; }
    }
}
