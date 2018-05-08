using EventStoreTools.Core.Entities;
using EventStoreTools.Core.Entities.EventStore;
using EventStoreTools.Core.Services.Search;
using EventStoreTools.Core.Services.Search.Factories;
using EventStoreTools.Infrastructure.EventStore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventStoreTools.Tests.UnitTests.Search
{
    public class SearchTests
    {
        private List<Event> _events;

        public SearchTests()
        {
            _events = new List<Event>();
            _events.Add(new Event(Guid.NewGuid().ToString(), 0, DateTime.Now.AddDays(-1), "Jame age 24", "IEvent"));
            _events.Add(new Event(Guid.NewGuid().ToString(), 1, DateTime.Now.AddYears(-3), "Mike age 25", "IEvent"));
            _events.Add(new Event(Guid.NewGuid().ToString(), 2, DateTime.Now.AddDays(-6), "Kara age 34", "IEvent"));
            _events.Add(new Event(Guid.NewGuid().ToString(), 3, DateTime.Now.AddDays(-5), "Walt age 42", "IEvent"));
            _events.Add(new Event(Guid.NewGuid().ToString(), 4, DateTime.Now.AddYears(-2), "Bacha age 24", "IEvent"));
            _events.Add(new Event(Guid.NewGuid().ToString(), 5, DateTime.Now.AddYears(-2), "Mira age 25", "IEvent"));
        }

        [Fact]
        public void DataSearchStrategyTest()
        {
            // Arrange
            var dataSearchStrategy = new DataSearchStrategy("age 25");

            // Act
            var result = 0;
            foreach(var @event in _events)
            {
                result += dataSearchStrategy.Compare(@event) ? 1 : 0;
            }
            

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public void DataSearchStrategy_Wrong_Test()
        {
            // Arrange
            var dataSearchStrategy = new DataSearchStrategy("margin 1");

            // Act
            var result = 0;
            foreach (var @event in _events)
            {
                result += dataSearchStrategy.Compare(@event) ? 1 : 0;
            }

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void DateSearchStrategyTest()
        {
            var fromToParam = new FromToParam(DateTime.Now.AddYears(-1), DateTime.UtcNow);
            // Arrange
            var dataSearchStrategy = new DateSearchStrategy(fromToParam);

            // Act
            var result = 0;
            foreach (var @event in _events)
            {
                result += dataSearchStrategy.Compare(@event) ? 1 : 0;
            }


            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void DateSearchStrategy_OnlyFrom_Test()
        {
            var fromToParam = new FromToParam(DateTime.Now.AddYears(-1), DateTime.MaxValue);
            // Arrange
            var dataSearchStrategy = new DateSearchStrategy(fromToParam);

            // Act
            var result = 0;
            foreach (var @event in _events)
            {
                result += dataSearchStrategy.Compare(@event) ? 1 : 0;
            }


            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void DateSearchStrategy_OnlyTo_Test()
        {
            var fromToParam = new FromToParam(DateTime.MinValue, DateTime.Now);
            // Arrange
            var dataSearchStrategy = new DateSearchStrategy(fromToParam);

            // Act
            var result = 0;
            foreach (var @event in _events)
            {
                result += dataSearchStrategy.Compare(@event) ? 1 : 0;
            }


            // Assert
            Assert.Equal(6, result);
        }

        [Fact]
        public void DateSearchStrategy_Wrong_Test()
        {
            var fromToParam = new FromToParam(DateTime.MinValue, DateTime.MinValue);
            // Arrange
            var dataSearchStrategy = new DateSearchStrategy(fromToParam);

            // Act
            var result = 0;
            foreach (var @event in _events)
            {
                result += dataSearchStrategy.Compare(@event) ? 1 : 0;
            }


            // Assert
            Assert.Equal(0, result);
        }
    }
}
