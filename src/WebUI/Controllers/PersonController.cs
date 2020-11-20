using Microsoft.AspNetCore.Mvc;

using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;

namespace Valhalla.Web.Controllers
{
    [ApiController]
    [Route("api/v1/people")]
    public class PersonController : CrudController<PersonDto, Person>
    {
    }
}