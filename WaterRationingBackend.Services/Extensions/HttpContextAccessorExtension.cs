using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WaterRationingBackend.Services.Extensions
{
    public static class HttpContextAccessorExtension
    {
        public static string GetHeaderValue(this IHttpContextAccessor accessor, string headerKey)
        {
            return accessor.HttpContext.Request.Headers.FirstOrDefault((header) => header.Key == headerKey).Value.FirstOrDefault();
        }
    }
}
