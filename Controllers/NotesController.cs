using Managingnotes.Dbcontext;
using Managingnotes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Managingnotes.Controllers
{
    public class NotesController : Controller
    {
        private readonly NotesDbContext _context;
        public NotesController(NotesDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var notes = await _context.Notes
                            .OrderByDescending(n => n.CreatedAt)
                            .ToListAsync();
            return View(notes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,Priority")] Notes note)
        {
            if (ModelState.IsValid)
            {
                note.CreatedAt = DateTime.UtcNow;
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Priority")] Notes note)
        {
            if (id != note.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingNote = await _context.Notes.FindAsync(id);
                    if (existingNote == null) return NotFound();

                    existingNote.Content = note.Content;
                    existingNote.Priority = note.Priority;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View("Index", await _context.Notes.OrderByDescending(n => n.CreatedAt).ToListAsync());
        }

        // POST: Notes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
