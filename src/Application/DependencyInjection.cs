using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Valhalla.Application.Addresses.Queries.GetAddresses;
using Valhalla.Application.Common.Interfaces;
using Valhalla.Application.Entities.Commands.CreateEntity;
using Valhalla.Application.Entities.Commands.DeleteEntity;
using Valhalla.Application.Entities.Commands.UpdateEntity;
using Valhalla.Application.Entities.Queries.ReadEntity;
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

            services
                .RegisterCrudDependencies<Person, PersonDto>()
                .RegisterCrudDependencies<Address, AddressDto>();
        }

        private static IServiceCollection RegisterCrudDependencies<TEntity, TDto>(this IServiceCollection services)
            where TEntity : EntityBase
            where TDto : IDto
        {
            services.AddTransient(typeof(IRequestHandler<CreateEntityCommand<TEntity>, TEntity>), typeof(CreateEntityCommandHandler<TEntity>));
            services.AddTransient(typeof(IRequestHandler<ReadEntityQuery<TEntity>, IDto>), typeof(ReadEntityQueryHandler<TDto, TEntity>));
            services.AddTransient(typeof(IRequestHandler<UpdateEntityCommand<TDto, TEntity>, TEntity>), typeof(UpdateEntityCommandHandler<TDto,TEntity>));
            services.AddTransient(typeof(IRequestHandler<DeleteEntityCommand<TEntity>, int>), typeof(DeleteEntityCommandHandler<TEntity>));

            return services;
        }
    }
}