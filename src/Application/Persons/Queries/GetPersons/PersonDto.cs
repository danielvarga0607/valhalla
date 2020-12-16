using Valhalla.Application.Common.Interfaces;

namespace Valhalla.Application.Persons.Queries.GetPersons
{
    public class PersonDto : IDto
    {
        public string Name { get; set; }

        public short Age { get; set; }
    }
}