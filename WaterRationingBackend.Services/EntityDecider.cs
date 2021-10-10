using System;
using System.Collections.Generic;
using System.Linq;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Interfaces;

namespace WaterRationingBackend.Services
{
    public class EntityDecider : IEntityDecider
    {
        private readonly IEnumerable<IEntities> _entities;
        private readonly IScopeHelper _scopeHelper;

        public EntityDecider(IEnumerable<IEntities> entities, IScopeHelper scopeHelper)
        {
            _entities = entities;
            _scopeHelper = scopeHelper;
        }

        public IEntities Decided()
        {
            var entityScope = _scopeHelper.GetEntity();

            return entityScope switch
            {
                Entity.City => _entities.FirstOrDefault((e) => e.GetType() == typeof(Cities)),
                Entity.Suburb => _entities.FirstOrDefault((e) => e.GetType() == typeof(Suburbs)),
                Entity.History => _entities.FirstOrDefault((e) => e.GetType() == typeof(Histories)),
                _ => throw new NotImplementedException("scope out of reach"),
            };
            #region initial idea
            //if (serializedData.Contains(nameof(City.Suburbs), StringComparison.OrdinalIgnoreCase))
            //{
            //    entities = _entities.FirstOrDefault((e) => e.GetType() == typeof(Cities));
            //}
            //else if (serializedData.Contains(nameof(Suburb.Density), StringComparison.OrdinalIgnoreCase))
            //{
            //    entities = _entities.FirstOrDefault((e) => e.GetType() == typeof(Suburbs));
            //}
            //else if (serializedData.Contains(nameof(UsageHistory.Day), StringComparison.OrdinalIgnoreCase))
            //{
            //    entities = _entities.FirstOrDefault((e) => e.GetType() == typeof(Histories));
            //}
            //else
            //{
            //    throw new DataMisalignedException("data is of wrong format");
            //} 
            #endregion
        }
    }
}
