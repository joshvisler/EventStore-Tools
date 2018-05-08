using EventStoreTools.Core.Interfaces;
using System;
using EventStoreTools.Core.Entities;
using System.Security.Claims;
using EventStoreTools.Core.Encrypt;
using System.Collections.Generic;
using System.Linq;
using EventStoreTools.Core.Exceptions;
using System.Threading.Tasks;

namespace EventStoreTools.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IClientRepository _clientRepository;
        private const int BaseRole = 1;
        private const string KEY = "C2937E5FE29A448295823189042C0E37";

        public AuthService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<bool> ClientExist(string login)
        {
            var client = await _clientRepository.GetByLoginAsync(login);
            if (client != null)
                return true;

            return false;
        }

        public ClaimsIdentity Auth(AuthParameters user)
        {
            var person = _clientRepository.GetByLogin(user.Login);

            if (person == null)
                return null;

            var personPassord = Encrypter.DecryptString(person.PasswordHash, KEY);

            if (user.Password != personPassord)
            {
                throw new WrongPasswordException();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
            };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public Client Register(AuthParameters user)
        {
            if(ClientExist(user.Login).Result)
            {
                throw new UserExistException();
            }


            var clientId = Guid.NewGuid();
            var passwordHash = Encrypter.EncryptString(user.Password, KEY);

            return _clientRepository.Insert(new Client(Guid.NewGuid(), BaseRole, passwordHash, user.Login));
        }

        public Client GetCurrentClient(ClaimsPrincipal user)
        {
            var login = user.Claims.Where(c => c.Type == ClaimsIdentity.DefaultNameClaimType).FirstOrDefault().Value;

            if (login == null)
                throw new UserNotFoundException();

            return _clientRepository.GetByLogin(login);
        }
    }
}
