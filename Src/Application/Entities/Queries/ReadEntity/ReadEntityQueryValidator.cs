using FluentValidation;
using Valhalla.Application.Common.Interfaces;

namespace Valhalla.Application.Entities.Queries.ReadEntity
{
    public class ReadEntityQueryValidator : AbstractValidator<IDto>
    {
        public ReadEntityQueryValidator()
        {
            RuleFor(dto => dto.Id).NotEmpty();
        }
    }
}