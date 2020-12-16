namespace Valhalla.Web.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string _root = "api";
        private const string _version = "v1";
        private const string _base = _root + "/" + _version;

        public static class Addresses
        {
            public const string GetAll = _base + "/addresses";
            public const string Create = _base + "/addresses";
            public const string Get = _base + "/addresses/{addressId:Guid}";
            public const string Update = _base + "/addresses";
            public const string Delete = _base + "/addresses/{addressId:Guid}";
        }

        public static class People
        {
            public const string GetAll = _base + "/people";
            public const string Create = _base + "/people";
            public const string Get = _base + "/people/{personId:Guid}";
            public const string Update = _base + "/people";
            public const string Delete = _base + "/people/{personId:Guid}";
        }
    }
}