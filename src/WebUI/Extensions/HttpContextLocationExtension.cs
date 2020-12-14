using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Valhalla.Web.Extensions
{
    public static class HttpContextLocationExtension
    {
        public static string GetLocation(this HttpContext httpContext, Guid id)
        {
            var request = httpContext.Request;
            var baseUrl = request.Scheme + "://" + Path.Join(request.Host.ToUriComponent(), request.Path);

            return baseUrl + '/'+ id;
        }
    }
}