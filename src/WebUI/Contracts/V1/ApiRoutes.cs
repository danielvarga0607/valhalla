namespace Valhalla.Web.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public const string Addresses = Base + "/addresses";
        public const string People = Base + "/people";
    }
}