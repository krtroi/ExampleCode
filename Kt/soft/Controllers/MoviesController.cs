using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ktsoft.Data;
using Ktsoft.Models;

namespace Ktsoft.Controllers;

public class MoviesController(ApplicationDbContext c) : Controller {
    private readonly ApplicationDbContext context = c;
    
    public async Task<IActionResult> Index() => View(await context.Movie.ToListAsync());

    public async Task<IActionResult> Details(int? id) {
        if (id == null) return NotFound();
        var movie = await context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        return movie == null ? NotFound() : View(movie);
    }

    public IActionResult Create() => View();

    [HttpPost] [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie) {
        if (ModelState.IsValid) {
            context.Add(movie);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(movie);
    }

    public async Task<IActionResult> Edit(int? id) {
        if (id == null) return NotFound();
        var movie = await context.Movie.FindAsync(id);
        return movie == null ? NotFound() : View(movie);
    }
    
    [HttpPost] [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie) {
        if (id != movie.Id) return NotFound();
        if (ModelState.IsValid) {
            try {
                context.Update(movie);
                await context.SaveChangesAsync();
            }catch (DbUpdateConcurrencyException) {
                if (!MovieExists(movie.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        return View(movie);
    }

    public async Task<IActionResult> Delete(int? id) {
        if (id == null) return NotFound();
        var movie = await context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        return movie == null? NotFound() : View(movie);
    }

    [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var movie = await context.Movie.FindAsync(id);
        if (movie != null) {
            context.Movie.Remove(movie);
        }
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool MovieExists(int id) => context.Movie.Any(e => e.Id == id);
}

