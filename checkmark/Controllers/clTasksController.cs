using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using checkmark.Models;

namespace checkmark.Controllers
{
    public class clTasksController : Controller
    {
        private readonly AppDbContext _context;

        public clTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: clTasks
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Tasks.Include(c => c.user);
            return View(await appDbContext.ToListAsync());
        }

        // GET: clTasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clTask = await _context.Tasks
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (clTask == null)
            {
                return NotFound();
            }

            return View(clTask);
        }

        // GET: clTasks/Create
        public IActionResult Create()
        {
            ViewData["userId"] = new SelectList(_context.Users, "UserId", "UserEmailAddress");
            return View();
        }

        // POST: clTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,TaskName,TaskDescription,priorityLvl,statusLvl,DateCreated,DateDue,userId")] clTask clTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["userId"] = new SelectList(_context.Users, "UserId", "UserEmailAddress", clTask.userId);
            return View(clTask);
        }

        // GET: clTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clTask = await _context.Tasks.FindAsync(id);
            if (clTask == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.Users, "UserId", "UserEmailAddress", clTask.userId);
            return View(clTask);
        }

        // POST: clTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,TaskName,TaskDescription,priorityLvl,statusLvl,DateCreated,DateDue,userId")] clTask clTask)
        {
            if (id != clTask.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!clTaskExists(clTask.TaskId))
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
            ViewData["userId"] = new SelectList(_context.Users, "UserId", "UserEmailAddress", clTask.userId);
            return View(clTask);
        }

        // GET: clTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clTask = await _context.Tasks
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (clTask == null)
            {
                return NotFound();
            }

            return View(clTask);
        }

        // POST: clTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clTask = await _context.Tasks.FindAsync(id);
            if (clTask != null)
            {
                _context.Tasks.Remove(clTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool clTaskExists(int id)
        {
            return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}
