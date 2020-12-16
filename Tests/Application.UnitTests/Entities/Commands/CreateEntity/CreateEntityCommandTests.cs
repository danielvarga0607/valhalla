using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.TestUtils;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Moq.EntityFrameworkCore;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Entities.Commands.CreateEntity
{
    public class CreateEntityCommandTests
    {
        private readonly Guid _johnPersonId = new Guid("6D61C03E-23FF-4BED-A8BB-0AC9742F4759");

        [Fact]
        public async Task CreateEntityCommandHandlerTest()
        {
            // Arrange
            var sut = new CreateEntityCommandHandler<Person>(
                TestAutoMapper.Instance,
                ValhallaDbContextMock().Object);

            // Act
            var person = await sut.Handle(new CreateEntityCommand<Person>
            {
                Dto = new PersonDto
                {
                    Age = 23,
                    Name = "James"
                }
            }, CancellationToken.None);

            // Assert
            person.Age.Should().Be(23);
            person.Name.Should().Be("James");
        }

        private Mock<IValhallaDbContext> ValhallaDbContextMock()
        {
            var valhallaDbContextMock = new Mock<IValhallaDbContext>();

            var persons = new List<Person>
            {
                new Person
                {
                    Id = _johnPersonId,
                    Name = "John",
                    Age = 34
                }
            };

            valhallaDbContextMock
                .SetupSequence(x => x.Set<Person>())
                .ReturnsDbSet(persons);

            valhallaDbContextMock
                .SetupSequence(x => x.Set<Person>().AddAsync(persons[0], CancellationToken.None))
                .Returns(new ValueTask<EntityEntry<Person>>());

            valhallaDbContextMock
                .Setup(x => x.SaveChangesAsync(CancellationToken.None))
                .ReturnsAsync(1);

            return valhallaDbContextMock;
        }
    }
}