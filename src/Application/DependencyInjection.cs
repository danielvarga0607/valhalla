using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Application.Common.Interfaces;
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
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRequestHandler<CreateEntityCommand<PersonDto,Person>, Person>), typeof(CreateEntityCommandHandler<PersonDto, Person>));
            services.AddTransient(typeof(IRequestHandler<GetEntityQuery<Person>, IDto>), typeof(GetEntityQueryHandler<PersonDto, Person>));
            services.AddTransient(typeof(IRequestHandler<UpdateEntityCommand<PersonDto>, Guid>), typeof(UpdateEntityCommandHandler<PersonDto, Person>));
            services.AddTransient(typeof(IRequestHandler<DeleteEntityCommand<Person>, int>), typeof(DeleteEntityCommandHandler<Person>));

            services.AddTransient(typeof(IRequestHandler<CreateEntityCommand<AddressDto, Address>, Address>), typeof(CreateEntityCommandHandler<AddressDto, Address>));
            services.AddTransient(typeof(IRequestHandler<GetEntityQuery<Address>, IDto>), typeof(GetEntityQueryHandler<AddressDto, Address>));
            services.AddTransient(typeof(IRequestHandler<UpdateEntityCommand<AddressDto>, Guid>), typeof(UpdateEntityCommandHandler<AddressDto, Address>));
            services.AddTransient(typeof(IRequestHandler<DeleteEntityCommand<Address>, int>), typeof(DeleteEntityCommandHandler<Address>));
        }
    }
}