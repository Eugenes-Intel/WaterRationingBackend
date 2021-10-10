using System;
using System.Collections.Generic;
using System.Text;
using WaterRationingBackend.Entities;

namespace Helper
{
    public static class ClientResponse
    {
        public static string Add(string title, ResponseInfo info) => info switch
        {
            ResponseInfo.Success => $"{title} successfully added",
            ResponseInfo.Exist => $"{title} already exist",
            ResponseInfo.Error => $"error while adding {title}",
        };

        public static string Update(string title, ResponseInfo info) => info switch
        {
            ResponseInfo.Success => $"{title} successfully updated",
            ResponseInfo.Exist => $"{title} does not exist",
            ResponseInfo.Error => $"error while updating {title}",
        };

        public static string Delete(string title, ResponseInfo info) => info switch
        {
            ResponseInfo.Success => $"{title} successfully deleting",
            ResponseInfo.Exist => $"{title} does not exist",
            ResponseInfo.Error => $"error while deleting {title}",
        };
    }
}
