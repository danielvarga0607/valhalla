using System;
using AutoMapper;

namespace Application.UnitTests.TestUtils
{
    /// <summary>
    /// Automapper instance for testing. This implementation is Singleton.
    /// </summary>
    public static class TestAutoMapper
    {
        private static Lazy<IMapper> _instance;

        public static IMapper Instance
        {
            get
            {
                if (_instance != null) return _instance.Value;

                var configuration = new MapperConfiguration(cfg => cfg
                    .AddProfiles(AutoMapperProfiles.Get));
                _instance = new Lazy<IMapper>(() => new Mapper(configuration));

                return _instance.Value;
            }
        }
    }
}