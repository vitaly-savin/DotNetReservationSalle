using backend.Models;
using Microsoft.AspNetCore.Identity;

namespace backend.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(EnregistrementPersonne enregistrementPersonne);
        Task<string> LoginAsync(ConnecterPersonne connecterPersonne);
        string getRole(string id);

    }
}
