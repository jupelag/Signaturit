using Signaturit.LobbyWars.Shared.Contracts;
using Signaturit.LobbyWars.Shared.Enumerations;

namespace Signaturit.LobbyWars.Shared.Extenders
{
    public static class ContractExtenders
    {
        public static IEnumerable<SignatureTypes> CleanSignatures(this IContract contract,SignatureTypes typeCleaner, SignatureTypes typeCleaned)
        {
            var signaturesCleaned = contract.Signatures.Where(s => s != null).Select(s => s.Value).ToArray();
            if (!contract.Signatures.Contains(typeCleaner)) return signaturesCleaned;

            signaturesCleaned = signaturesCleaned.Where(s => s != typeCleaned).ToArray();
            var newList = new List<SignatureTypes>();
            newList.AddRange(signaturesCleaned);
            return newList;
        }
    }
}
