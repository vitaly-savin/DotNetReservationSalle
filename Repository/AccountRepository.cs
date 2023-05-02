using backend.Controllers;
using backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly BdreservationSalleContext _context;
        public AccountRepository(UserManager<ApplicationUser> userManager, 
                SignInManager<ApplicationUser> signInManager,
                IConfiguration configuration,
                BdreservationSalleContext context) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _context = context;
        }
        public async Task<IdentityResult> SignUpAsync(EnregistrementPersonne enregistrementPersonne)
        {
            var user = new ApplicationUser()
            {
                FirstName = enregistrementPersonne.FirstName,
                LastName = enregistrementPersonne.LastName,
                Email = enregistrementPersonne.Email,
                UserName = enregistrementPersonne.Email
            };

            return await _userManager.CreateAsync(user, enregistrementPersonne.Password);
        }
        public  async Task<string> LoginAsync(ConnecterPersonne connecterPersonne)
        {
            
            var result = await _signInManager.PasswordSignInAsync(connecterPersonne.Email, connecterPersonne.Password,false,false);
            if (!result.Succeeded) 
            {
                return null;
            }
            var user =  _context.Users
                    .Where(s => s.UserName == connecterPersonne.Email)
                    .FirstOrDefault();
           
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, connecterPersonne.Email),
                new Claim(ClaimTypes.Role, getRole(connecterPersonne.Email)),
                new Claim(ClaimTypes.Name, user.LastName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        
        public string getRole(string id)
        {
            string result = "Membre";
            bool estAdm = _context.Administrateurs.Any(e => e.Courriel == id);

            if (estAdm == true)
            {
                result = "Admin";
            }
            return result;
        }
        

    }
}
