using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Indigo.Data;
using Indigo.Models;
using Indigo.Exstention;

namespace Indigo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly IWebHostEnvironment _env;

        public CardsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Admin/Cards1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cards.ToListAsync());
        }

        // GET: Admin/Cards1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards
                .FirstOrDefaultAsync(m => m.id == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Admin/Cards1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Cards1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Card card)
        {
            card.ImagePath = card.File.CreateFile(_env.WebRootPath, "assets/images");

            _context.Add(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            return View(card);
        }

        // GET: Admin/Cards1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cards == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            return View(card);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Card card)
        {
            var card2 = _context.Cards.Find(card.id);
            card2.Title = card.Title;
            card2.Description = card.Description;
            if (card.File != null)
            {
                card2.ImagePath = card.File.CreateFile(_env.WebRootPath, "assets/images");
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

    }

    // GET: Admin/Cards1/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Cards == null)
        {
            return NotFound();
        }

        var card = await _context.Cards
            .FirstOrDefaultAsync(m => m.id == id);
        if (card == null)
        {
            return NotFound();
        }

        return View(card);
    }

    // POST: Admin/Cards1/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Cards == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Cards'  is null.");
        }
        var card = await _context.Cards.FindAsync(id);
        if (card != null)
        {
            _context.Cards.Remove(card);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CardExists(int id)
    {
        return _context.Cards.Any(e => e.id == id);
    }
}
}
