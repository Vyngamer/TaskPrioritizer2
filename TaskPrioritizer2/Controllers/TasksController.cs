using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskPrioritizer2.Data;
using TaskPrioritizer2.Models;

namespace TaskPrioritizer2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/tasks (Returns tasks ordered by highest priority score)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentTask>>> GetPrioritizedTasks()
    {
        var tasks = await _context.Tasks
            .Where(t => !t.IsCompleted)
            .ToListAsync();

        return Ok(tasks.OrderByDescending(t => t.PriorityScore));
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<ActionResult<StudentTask>> CreateTask(StudentTask task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPrioritizedTasks), new { id = task.Id }, task);
    }

    // PUT: api/tasks/{id}/complete
    [HttpPut("{id}/complete")]
    public async Task<IActionResult> MarkCompleted(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        task.IsCompleted = true;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}