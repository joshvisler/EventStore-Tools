using EventStoreTools.Core.Encrypt;
using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Exceptions;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EventStoreTools.Tests.Auth
{
    public class AuthTests
    {
        private const string KEY = "C2937E5FE29A448295823189042C0E37";

        [Fact]
        public void RegistrationTest()
        {
            // Arrange
            var password = "DwadsadwWDASDwdwdasdawd";
            var passwordHash = Encrypter.EncryptString(password, KEY);
            var client = new Client(Guid.NewGuid(), 1, passwordHash, "Wall Disney");
            var authParameters = new AuthParameters(client.Login, password);
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.Insert(It.IsAny<Client>())).Returns(client);
            var authService = new AuthService(mockClientRepository.Object);

            // Act
            var result = authService.Register(authParameters);

            // Assert
            Assert.Equal(client.Login, result.Login);
            Assert.Equal(client.PasswordHash, result.PasswordHash);
            Assert.Equal(client.RoleId, result.RoleId);
        }

        [Fact]
        public void AutentificationTest()
        {
            // Arrange
            var password = "123456";
            var passwordHash = Encrypter.EncryptString(password, KEY);
            var client = new Client(Guid.NewGuid(), 1, new Role { Name = "Admin", RoleId = 1}, passwordHash, "Wall Disney");
            var authParameters = new AuthParameters(client.Login, password);
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetByLogin(It.IsAny<string>())).Returns(client);
            var authService = new AuthService(mockClientRepository.Object);

            // Act
            var result = authService.Auth(authParameters);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void AuthWithNotExistUser()
        {
            // Arrange
            var password = "123456";
            var passwordHash = Encrypter.EncryptString(password, KEY);
            var client = new Client(Guid.NewGuid(), 1, new Role { Name = "Admin", RoleId = 1 }, passwordHash, "Wall Disney");
            var authParameters = new AuthParameters(client.Login, password);
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetByLogin(It.IsAny<string>())).Returns((Client)null);
            var authService = new AuthService(mockClientRepository.Object);

            // Act
            var result = authService.Auth(authParameters);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void AuthWithWrongPassword()
        {
            // Arrange
            var password = "123456";
            var passwordHash = Encrypter.EncryptString(password, KEY);
            var client = new Client(Guid.NewGuid(), 1, new Role { Name = "Admin", RoleId = 1 }, passwordHash, "Wall Disney");
            var authParameters = new AuthParameters(client.Login, "253532");
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetByLogin(It.IsAny<string>())).Returns(client);
            var authService = new AuthService(mockClientRepository.Object);

            // Act
            Assert.Throws<WrongPasswordException>(() => authService.Auth(authParameters));
        }

        [Fact]
        public void RegisterSameUser()
        {
            // Arrange
            var password = "DwadsadwWDASDwdwdasdawd";
            var passwordHash = Encrypter.EncryptString(password, KEY);
            var client = new Client(Guid.NewGuid(), 1, passwordHash, "Wall Disney");
            var authParameters = new AuthParameters(client.Login, password);
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.Insert(It.IsAny<Client>())).Returns(client);
            mockClientRepository.Setup(repo => repo.GetByLoginAsync(It.IsAny<string>())).Returns(Task.FromResult(client));
            var authService = new AuthService(mockClientRepository.Object);
            
            // Assert
            Assert.Throws<UserExistException>(()=> authService.Register(authParameters));
        }
    }
}
