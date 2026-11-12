
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeFit.Models;
using BeFit.Data;
using Microsoft.AspNetCore.Authorization;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using BeFit.Dtos;

[Authorize]
public class ExerciseController : Controller
{
    private readonly ApplicationDbContext _context;

    public ExerciseController(ApplicationDbContext context)
    {
        _context = context;
      
    }

    private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    
    // GET: EXERCISES
    public async Task<IActionResult> Index()    
    {
        var exercise = _context.Exercise
             .Include(e => e.ExerciseType)
             .Include(e => e.Session)
             .Where(e => e.CreatedById == CurrentUserId);
        return View(await exercise.ToListAsync());
    }

    // GET: EXERCISES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercise
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    // GET: EXERCISES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: EXERCISES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExerciseFormDto dto)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        var exercise = new Exercise
        {
            Weight = dto.Weight,
            NumOfReps = dto.NumOfReps,
            NumOfSeries = dto.NumOfSeries,
            ExerciseTypeId = dto.ExerciseTypeId,
            SessionId = dto.SessionId,
            CreatedById = CurrentUserId
        };

        _context.Add(exercise);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: EXERCISES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercise.FindAsync(id);
        if (exercise == null)
        {
            return NotFound();
        }
        return View(exercise);
    }

    // POST: EXERCISES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Weight,NumOfReps,NumOfSeries,ExerciseTypeId,ExerciseType,SessionId,Session")] Exercise exercise)
    {
        if (id != exercise.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(exercise);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExerciseExists(exercise.Id))
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
        return View(exercise);
    }

    // GET: EXERCISES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var exercise = await _context.Exercise
            .FirstOrDefaultAsync(m => m.Id == id);
        if (exercise == null)
        {
            return NotFound();
        }

        return View(exercise);
    }

    // POST: EXERCISES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var exercise = await _context.Exercise.FindAsync(id);
        if (exercise != null)
        {
            _context.Exercise.Remove(exercise);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ExerciseExists(int? id)
    {
        return _context.Exercise.Any(e => e.Id == id);
    }
}
