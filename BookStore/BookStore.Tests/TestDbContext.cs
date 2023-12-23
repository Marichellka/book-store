using BookStore.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Tests;

public class TestDbContext: AppDbContext
{
    public TestDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
