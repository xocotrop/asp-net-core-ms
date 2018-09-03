using System.Threading.Tasks;
using Actio.Services.Identity.Domain.Models;
using Actio.Services.Identity.Domain.Repositories;
using Actio.Services.Identity.Domain.Services;
using Action.Common.Exceptions;

namespace Actio.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository userRepository, IEncrypter encrypter)
        {
            _encrypter = encrypter;
            _userRepository = userRepository;

        }
        public async Task LoginAsync(string email, string passwor)
        {
             var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new ActioException("invalid_credentials", $"Invalid credentials.");

            if(!user.ValidatePassword(passwor, _encrypter))
             throw new ActioException("invalid_credentials", $"Invalid credentials.");
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new ActioException("email_in_use", $"Email: '{email}' is already in use");

            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(user);

        }
    }
}