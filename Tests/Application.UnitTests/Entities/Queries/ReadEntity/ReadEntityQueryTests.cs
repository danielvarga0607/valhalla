using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.TestUtils;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Application.Entities.Queries.ReadEntity;
using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Entities.Queries.ReadEntity
{
    public class ReadEntityQueryTests
    {
        private readonly Guid _johnPersonId = new Guid("B420F57A-CCFA-4E78-A074-2E5D1D01DD74");
        private readonly Guid _jamesPersonId = new Guid("9913EA6F-59A0-4E85-891C-11F3A500A43F");

        [Fact]
        public async Task ReadEntityQueryHandlerTest_Entity_Is_Person()
        {
            // Arrange
            var sut = new ReadEntityQueryHandler<PersonDto, Person>(
                ValhallaDbContextMock().Object,
                TestAutoMapper.Instance);

            // Act
            var dto = await sut.Handle(new ReadEntityQuery<Person>
            {
                Dto = new PersonDto
                {
                    Id = _johnPersonId
                }
            }, CancellationToken.None);
            var personDto = dto as PersonDto;

            // Assert
            personDto?.Id.Should().Be(_johnPersonId);
            personDto?.Age.Should().Be(34);
            personDto?.Name.Should().Be("John");
        }

        [Fact]
        public async Task ReadEntityQueryHandlerTest_Throw_Not_Found_Exception()
        {
            // Arrange
            var sut = new ReadEntityQueryHandler<PersonDto, Person>(
                ValhallaDbContextMock().Object,
                TestAutoMapper.Instance);
            var entityId = Guid.NewGuid();

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(new ReadEntityQuery<Person>
            {
                Dto = new PersonDto
                {
                    Id = entityId
                }
            }, CancellationToken.None));

            exception.Should().BeAssignableTo<NotFoundException>();
            exception.Message.Should().Be($"Entity TEntity ({entityId}) was not found.");
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
                },
                new Person
                {
                    Id = _jamesPersonId,
                    Name = "James",
                    Age = 56
                }
            };

            valhallaDbContextMock
                .SetupSequence(x => x.Set<Person>())
                .ReturnsDbSet(persons);

            valhallaDbContextMock
                .SetupSequence(x => x.Set<Person>().FindAsync(_johnPersonId))
                .ReturnsAsync(persons[0]);

            return valhallaDbContextMock;
        }
    }
}