using AutoMapper;
using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Interfaces;
using EventStoreTools.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace EventStoreTools.Tests.UnitTests.Connections
{
    public class ConnectionsTests
    {
        private List<Connection> Connections;

        public ConnectionsTests()
        {
            Connections = new List<Connection>();
            Connections.Add(new Connection(Guid.NewGuid(), "test", "connection1", false, 0, "test1" ));
            Connections.Add(new Connection(Guid.NewGuid(), "test2", "connection2", false, 0, "test2" ));
            Connections.Add(new Connection(Guid.NewGuid(), "test3", "connection3", false, 0, "test3" ));
            Connections.Add(new Connection(Guid.NewGuid(), "test4", "connection4", false, 0, "test4" ));
        }

        [Fact]
        public void RegistrationTest()
        {
            // Arrange
            var connectionRepositoryMock = new Mock<IConnectionRepository>();
            var mapperMock = new Mock<IMapper>();
            connectionRepositoryMock.Setup(repo => repo.Get()).Returns(Connections);

            var connectionService = new ConnectionService(connectionRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = connectionService.Get();

            // Assert
            Assert.Equal(Connections, result);
        }

        [Fact]
        public void Registration_ConnectionsNot_Test()
        {
            // Arrange
            var connectionRepositoryMock = new Mock<IConnectionRepository>();
            var mapperMock = new Mock<IMapper>();
            connectionRepositoryMock.Setup(repo => repo.Get()).Returns((List<Connection>)null);

            var connectionService = new ConnectionService(connectionRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = connectionService.Get();

            // Assert
            Assert.Null(result);
        }
    }
}
