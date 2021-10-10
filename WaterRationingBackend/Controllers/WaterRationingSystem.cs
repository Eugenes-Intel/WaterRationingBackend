using Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Interfaces;

namespace WaterRationingBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterRationingSystem : ControllerBase
    {
        private readonly ISupervisor _supervisor;
        private readonly ICollator _collator;

        public WaterRationingSystem(ISupervisor supervisor, ICollator collator)
        {
            _supervisor = supervisor;
            _collator = collator;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IEnumerable<IData>> Get()
        {
            return await _supervisor.Get();
        }

        //// GET api/<ValuesController>/5
        //[HttpGet, ActionName(nameof(Collated))]
        //public async Task<IData> Collated(int id)
        //{
        //    return await _collator.Collate(id);
        //}

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<IData> Get(int id)
        {
            return await _supervisor.Get(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<string> Post([FromBody] object data)
        {
            return await _supervisor.Post(data);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] string data)
        {
            return await _supervisor.Edit(data);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return await _supervisor.Delete(id);
        }
    }
}
