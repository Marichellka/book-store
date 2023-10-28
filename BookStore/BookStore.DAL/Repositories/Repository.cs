using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories;

public class Repository<TModel>: IRepository<TModel> where TModel : BaseModel
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<TModel>> GetAll()
    {
        return _context.Set<TModel>().ToListAsync();
    }

    public Task<TModel?> GetById(int id)
    {
        return _context.Set<TModel>().FindAsync(id).AsTask();
    }

    public async Task Add(TModel entity)
    {
        await _context.Set<TModel>().AddAsync(entity);
    }

    public async Task Delete(TModel entity)
    {
        _context.Set<TModel>().Remove(entity);
    }

    public async Task Update(TModel entity)
    {
        _context.Set<TModel>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}