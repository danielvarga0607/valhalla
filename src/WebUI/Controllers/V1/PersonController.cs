using Microsoft.AspNetCore.Mvc;

using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;
using Valhalla.Web.Contracts.V1;

namespace Valhalla.Web.Controllers.V1
{
    [ApiController]
    [Route(ApiRoutes.People)]
    public class PersonController : CrudController<PersonDto, Person>
    {
    }
}