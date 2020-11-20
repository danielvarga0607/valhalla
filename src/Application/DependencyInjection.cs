using AutoMapper;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Reflection;

using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.GetEntity;
using Valhalla.Application.Persons.Queries.GetPersons;
using Valhalla.Domain.Entities;

namespace Valhalla.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRequestHandler<CreateEntityCommand<Person>, Guid>), typeof(CreateEntityCommandHandler<Person>));
            services.AddTransient(typeof(IRequestHandler<GetEntityQuery<PersonDto>, PersonDto>), typeof(GetEntityQueryHandler<PersonDto, Person>));
            services.AddTransient(typeof(IRequestHandler<UpdateEntityCommand<PersonDto>, Guid>), typeof(UpdateEntityCommandHandler<PersonDto, Person>));
            services.AddTransient(typeof(IRequestHandler<DeleteEntityCommand<Person>, bool>), typeof(DeleteEntityCommandHandler<Person>));

            services.AddTransient(typeof(IRequestHandler<CreateEntityCommand<Address>, Guid>), typeof(CreateEntityCommandHandler<Address>));
            services.AddTransient(typeof(IRequestHandler<GetEntityQuery<AddressDto>, AddressDto>), typeof(GetEntityQueryHandler<AddressDto, Address>));
            services.AddTransient(typeof(IRequestHandler<UpdateEntityCommand<AddressDto>, Guid>), typeof(UpdateEntityCommandHandler<AddressDto, Address>));
            services.AddTransient(typeof(IRequestHandler<DeleteEntityCommand<Address>, bool>), typeof(DeleteEntityCommandHandler<Address>));

            return services;
        }
    }
}