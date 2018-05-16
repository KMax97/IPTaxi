using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPTaxi.Models;

namespace IPTaxi.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Service_taxiContext _context;

        public OrdersController(Service_taxiContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var service_taxiContext = _context.Order.Include(o => o.Client).Include(o => o.Dispetcher).Include(o => o.FinalStreet).Include(o => o.NumberOfRecordNavigation).Include(o => o.StartStreet);
            return View(await service_taxiContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Client)
                .Include(o => o.Dispetcher)
                .Include(o => o.FinalStreet)
                .Include(o => o.NumberOfRecordNavigation)
                .Include(o => o.StartStreet)
                .SingleOrDefaultAsync(m => m.NumberOfOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId");
            ViewData["DispetcherId"] = new SelectList(_context.Dispetcher, "DispetcherId", "DispetcherId");
            ViewData["FinalStreetId"] = new SelectList(_context.Street, "StreetId", "Name");
            ViewData["NumberOfRecord"] = new SelectList(_context.Record, "NumberOfRecord", "NumberOfRecord");
            ViewData["StartStreetId"] = new SelectList(_context.Street, "StreetId", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumberOfOrder,StartStreetId,NumberOfStartHouse,Time,FinalStreetId,NumberOfFinalHouse,TimeOfEndingOrder,NumberOfRecord,DispetcherId,ClientId,RealValue,AmountOfWrittenPoints,Value,AmountOfAccruedPoints")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId", order.ClientId);
            ViewData["DispetcherId"] = new SelectList(_context.Dispetcher, "DispetcherId", "DispetcherId", order.DispetcherId);
            ViewData["FinalStreetId"] = new SelectList(_context.Street, "StreetId", "Name", order.FinalStreetId);
            ViewData["NumberOfRecord"] = new SelectList(_context.Record, "NumberOfRecord", "NumberOfRecord", order.NumberOfRecord);
            ViewData["StartStreetId"] = new SelectList(_context.Street, "StreetId", "Name", order.StartStreetId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.NumberOfOrder == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId", order.ClientId);
            ViewData["DispetcherId"] = new SelectList(_context.Dispetcher, "DispetcherId", "DispetcherId", order.DispetcherId);
            ViewData["FinalStreetId"] = new SelectList(_context.Street, "StreetId", "Name", order.FinalStreetId);
            ViewData["NumberOfRecord"] = new SelectList(_context.Record, "NumberOfRecord", "NumberOfRecord", order.NumberOfRecord);
            ViewData["StartStreetId"] = new SelectList(_context.Street, "StreetId", "Name", order.StartStreetId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NumberOfOrder,StartStreetId,NumberOfStartHouse,Time,FinalStreetId,NumberOfFinalHouse,TimeOfEndingOrder,NumberOfRecord,DispetcherId,ClientId,RealValue,AmountOfWrittenPoints,Value,AmountOfAccruedPoints")] Order order)
        {
            if (id != order.NumberOfOrder)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.NumberOfOrder))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId", order.ClientId);
            ViewData["DispetcherId"] = new SelectList(_context.Dispetcher, "DispetcherId", "DispetcherId", order.DispetcherId);
            ViewData["FinalStreetId"] = new SelectList(_context.Street, "StreetId", "Name", order.FinalStreetId);
            ViewData["NumberOfRecord"] = new SelectList(_context.Record, "NumberOfRecord", "NumberOfRecord", order.NumberOfRecord);
            ViewData["StartStreetId"] = new SelectList(_context.Street, "StreetId", "Name", order.StartStreetId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Client)
                .Include(o => o.Dispetcher)
                .Include(o => o.FinalStreet)
                .Include(o => o.NumberOfRecordNavigation)
                .Include(o => o.StartStreet)
                .SingleOrDefaultAsync(m => m.NumberOfOrder == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.NumberOfOrder == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.NumberOfOrder == id);
        }
    }
}
