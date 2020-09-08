using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using InfoPlus.Data;
using InfoPlus.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InfoPlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoPlusController : ControllerBase
    {

        private readonly InfoPlusContext _infoPlusContext;

        
        public InfoPlusController(InfoPlusContext infoPlusContext)
        {
            _infoPlusContext = infoPlusContext;
        }

        // GET api/values
        [EnableCors("MyPolicy")]
        [HttpGet]
        public async Task <ActionResult<List<InfomationModel>>> GetRecords()
        {
            var records = await _infoPlusContext.Information.ToListAsync();
            return records;
        }

        // GET api/values/5
        [EnableCors("MyPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<InfomationModel>> GetRecord(int id)
        {
            var record = await _infoPlusContext.Information.FirstOrDefaultAsync(_ => _.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            return record;
        }

        // POST api/values
        [EnableCors("MyPolicy")]
        [HttpPost]
        public async Task<ActionResult> Post(InfomationModel infoModel)
        {
            var newId = await _infoPlusContext.Information.LastOrDefaultAsync();
            infoModel.Id = newId.Id + 1;
            _infoPlusContext.Information.Add(infoModel);
            await _infoPlusContext.SaveChangesAsync();

            return NoContent();
        }

        // PUT api/values/5
        [EnableCors("MyPolicy")]
        [HttpPut("{id}")]
        public async Task <ActionResult> PutRecord(int id, InfomationModel infoModel)
        {
            if (id != infoModel.Id)
            {
                return BadRequest();
            }

            _infoPlusContext.Entry(infoModel).State = EntityState.Modified;

            try
            {
                await _infoPlusContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecordExists(infoModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/values/5
        [EnableCors("MyPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _infoPlusContext.Information.FirstOrDefaultAsync(_ => _.Id == id);
            if (record == null)
            {
                return NotFound();
            }
            _infoPlusContext.Remove(record);

            await _infoPlusContext.SaveChangesAsync();

            return NoContent();
        }
        private bool RecordExists(int id)
        {
            return _infoPlusContext.Information.Any(e => e.Id == id);
        }
    }
}
