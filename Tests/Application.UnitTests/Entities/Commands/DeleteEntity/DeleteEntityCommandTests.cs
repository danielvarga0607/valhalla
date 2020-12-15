using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;
using Valhalla.Application.Common.Exceptions;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Entities.Commands.DeleteEntity
{
    public class DeleteEntityCommandTests
    {
        private readonly Guid _johnPersonId = new Guid("41777FD0-395F-42C8-BA09-140099038D8C");

        [Fact]
        public async Task DeleteEntityCommandHandlerTest()
        {
            // Arrange
            var sut = new DeleteEntityCommandHandler<Person>(ValhallaDbContextMock().Object);

            // Act
            var result = await sut.Handle(new DeleteEntityCommand<Person>
            {
                Id = _johnPersonId
            }, CancellationToken.None);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task DeleteEntityCommandHandlerTest_Throw_Not_Found_Exception()
        {
            // Arrange
            var sut = new DeleteEntityCommandHandler<Person>(ValhallaDbContextMock().Object);
            var entityId = new Guid("699F8C72-73DB-48FC-A657-A3E693CED5F2");

            // Act
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => sut.Handle(new DeleteEntityCommand<Person>
            {
                Id = entityId
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
                }
            };

            valhallaDbContextMock
                .SetupSequence(x => x.Set<Person>())
                .ReturnsDbSet(persons);

            valhallaDbContextMock
                .SetupSequence(x => x.Set<Person>().FindAsync(_johnPersonId))
                .ReturnsAsync(persons[0]);

            valhallaDbContextMock
                .Setup(x => x.SaveChangesAsync(CancellationToken.None))
                .ReturnsAsync(1);

            return valhallaDbContextMock;
        }
    }
}