using tareasAPI.Models;
using Microsoft.EntityFrameworkCore;
using tareasAPI.Services.interfaces;
using tareasAPI.Data;

namespace tareasAPI.Services;

public class TareaRepository : IRepository<Tarea>
{
    private readonly ApplicationDbContext _context;

    public TareaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    async Task<Tarea> IRepository<Tarea>.AddAsync(Tarea entity)
    {
        _context.Tareas.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    async Task<bool> IRepository<Tarea>.DeleteAsync(int id)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea != null)
        {
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return true;

        }
        else
        {
            return false;
        }
    }

    async Task<IEnumerable<Tarea>> IRepository<Tarea>.GetAllAsync()
    {
        return await _context.Tareas.Include(t => t.Usuario).ToListAsync();
    }

    async Task<Tarea> IRepository<Tarea>.GetByIdAsync(int id)
    {
        var tarea = await _context.Tareas.FirstOrDefaultAsync(m => m.IdTarea == id);
        if (tarea != null)
        {
            return tarea;
        }
        else
        {
            return null;
        }
    }

    async Task<Tarea> IRepository<Tarea>.UpdateAsync(Tarea entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }
}
