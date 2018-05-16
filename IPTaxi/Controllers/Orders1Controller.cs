using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPTaxi.Models;

namespace IPTaxi.Controllers
{
    [Produces("application/json")]
    [Route("api/Orders1")]
    public class Orders1Controller : Controller
    {
        private readonly Service_taxiContext _context;

        public Orders1Controller(Service_taxiContext context)
        {
            _context = context;
        }

        // GET: api/Orders1
        [HttpGet]
        public IEnumerable<dynamic> GetOrder()
        {
            return _context.Order/*.Include(o => o.Client).Include(o => o.StartStreet).Include(o => o.FinalStreet)*/
                .Select(o => new { o.NumberOfOrder, StartStreet = o.StartStreet.Name, o.NumberOfStartHouse
                , FinalStreet = o.FinalStreet.Name, o.NumberOfFinalHouse, o.Client.NumberOfTelephone });//.Include(d=>d.FinalStreet);
        }

        // GET: api/Orders1/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.NumberOfOrder == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                order.NumberOfOrder,
                StartStreet = order.StartStreet.Name,
                order.NumberOfStartHouse,
                FinalStreet = order.FinalStreet.Name,
                order.NumberOfFinalHouse,
                order.Client.NumberOfTelephone
            });
        }

        // PUT: api/Orders1/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] int id, [FromBody] dynamic order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != (int)order.numberOfOrder)
            {
                return BadRequest();
            }

            var found = await _context.Order.SingleOrDefaultAsync(o => o.NumberOfOrder == id);

            var startStreetName = (string)order.startStreet;
            var startStreet = await _context.Street.FirstOrDefaultAsync(s => s.Name == startStreetName);

            var endStreetName = (string)order.finalStreet;
            var endStreet = await _context.Street.FirstOrDefaultAsync(s => s.Name == endStreetName);

            if (found == null)
            {
                return NotFound();
            }

            found.StartStreet = startStreet;
            found.NumberOfStartHouse = (string)order.numberOfStartHouse;
            found.FinalStreet = endStreet;
            found.NumberOfFinalHouse = (string)order.numberOfFinalHouse;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Orders1
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] dynamic pseudoOrder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string telephone = (string)pseudoOrder.telephone;
            var client = await _context.Client.FirstOrDefaultAsync(cl => cl.NumberOfTelephone == telephone);

            var startStreetName = (string)pseudoOrder.startStreet;
            var startStreet = await _context.Street.FirstOrDefaultAsync(s => s.Name == startStreetName);

            var endStreetName = (string)pseudoOrder.endStreet;
            var endStreet = await _context.Street.FirstOrDefaultAsync(s => s.Name == endStreetName);

            if (client == null || startStreet == null || endStreet == null)
            {
                return NotFound();
            }

            var order = new Order { Client = client, Dispetcher = await _context.Dispetcher.FirstAsync(), FinalStreet = endStreet
                , NumberOfFinalHouse = (string)pseudoOrder.endHouse, NumberOfRecordNavigation = await _context.Record.FirstAsync()
                , NumberOfStartHouse = (string)pseudoOrder.startHouse, StartStreet = startStreet };

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.NumberOfOrder }, new
            {
                order.NumberOfOrder,
                StartStreet = order.StartStreet.Name,
                order.NumberOfStartHouse,
                FinalStreet = order.FinalStreet.Name,
                order.NumberOfFinalHouse,
                order.Client.NumberOfTelephone
            });
        }

        // DELETE: api/Orders1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.NumberOfOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.NumberOfOrder == id);
        }
    }
}