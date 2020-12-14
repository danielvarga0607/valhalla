namespace Valhalla.Web.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string _root = "api";
        private const string _version = "v1";
        private const string _base = _root + "/" + _version;

        public const string Addresses = _base + "/addresses";
        public const string People = _base + "/people";
    }
}