using System;
using System.Runtime.Serialization;
using Application.UnitTests.TestUtils;
using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;
using Xunit;

namespace Application.UnitTests.Mappings
{
    public class MappingTests
    {
        [Theory]
        [InlineData(typeof(Address), typeof(AddressDto))]
        [InlineData(typeof(Person), typeof(PersonDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            TestAutoMapper.Instance.Map(instance, source, destination);
        }

        private static object GetInstanceOf(Type type)
        {
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (type.GetConstructor(Type.EmptyTypes) != null) return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}