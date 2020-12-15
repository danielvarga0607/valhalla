using System.Collections.Generic;
using System.Collections.ObjectModel;
using AutoMapper;
using Valhalla.Application.Addresses.Mappings;
using Valhalla.Application.Persons.Mappings;

namespace Application.UnitTests.TestUtils
{
    /// <summary>
    /// Automapper profiles for testing
    /// </summary>
    public static class AutoMapperProfiles
    {
        public static IEnumerable<Profile> Get =>
            new ReadOnlyCollection<Profile>(new List<Profile>
            {
                new AddressMap(), new PersonMap()
            });
    }
}