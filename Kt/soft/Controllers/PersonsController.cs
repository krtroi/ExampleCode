using Ktsoft.Data;
using Ktsoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ktsoft.Controllers;
    public class PersonsController(ApplicationDbContext c) : Controller {
        private readonly ApplicationDbContext context = c;

        public async Task<IActionResult> Index() => View(await context.Persons.ToListAsync());

        public async Task<IActionResult> Details(int? id) {
            if (id == null) return NotFound();
            var  person= await context.Persons.FirstOrDefaultAsync(m => m.Id == id);
            return person == null ? NotFound() : View(person);
        }

        public IActionResult Create() => View();

        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FamilyName,FirstName,DoB,Gender")] Person person) {
            if (ModelState.IsValid) {
                context.Add(person);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) return NotFound();
            var person = await context.Persons.FindAsync(id);
            return person == null ? NotFound() : View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FamilyName,FirstName,DoB,Gender")] Person person) {
            if (id != person.Id) return NotFound();
            if (ModelState.IsValid) {
                try {
                    context.Update(person);
                    await context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!PersonExists(person.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return NotFound();
            var person = await context.Persons.FirstOrDefaultAsync(m => m.Id == id);
            return person == null ? NotFound() : View(person);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var person = await context.Persons.FindAsync(id);
            if (person != null) {
                context.Persons.Remove(person);
            }
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id) => context.Persons.Any(e => e.Id == id);
    }   
