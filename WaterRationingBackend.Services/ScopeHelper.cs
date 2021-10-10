using Microsoft.AspNetCore.Http;
using System;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Extensions;

namespace WaterRationingBackend.Services
{
    public class ScopeHelper : IScopeHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region constants
        private const string _scope = "scope";
        private const string _cities = "cities";
        private const string _suburbs = "suburbs";
        private const string _histories = "histories";
        #endregion

        public ScopeHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Entity GetEntity()
        {
            var entityScope = _httpContextAccessor.GetHeaderValue(_scope);

            return entityScope switch
            {
                _cities => Entity.City,
                _suburbs => Entity.Suburb,
                _histories => Entity.History,
                _ => throw new DataMisalignedException("data is of wrong format"),
            };
        }
    }
}
