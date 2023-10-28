using BookStore.DAL.Contexts;
using BookStore.DAL.Models;
using BookStore.DAL.Repositories;

namespace BookStore.DAL.UnitOfWork;

public class UnitOfWork: IUnitOfWork
{
    public IRepository<User> UserRepository { get; }
    public IRepository<Book> BookRepository { get; }
    public IRepository<Author> AuthorRepository { get; }
    public IRepository<Publisher> PublisherRepository { get; }
    public IRepository<Category> CategoryRepository { get; }
    public IRepository<BookCategory> BookCategoryRepository { get; }
    public IRepository<Review> ReviewRepository { get; }
    public IRepository<Order> OrderRepository { get; }
    public IRepository<OrderItem> OrderItemRepository { get; }
    public IRepository<Cart> CartRepository { get; }
    public IRepository<CartItem> CartItemRepository { get; }
    
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        UserRepository = new Repository<User>(_context);
        BookRepository = new Repository<Book>(_context);
        AuthorRepository = new Repository<Author>(_context);
        PublisherRepository = new Repository<Publisher>(_context);
        CategoryRepository = new Repository<Category>(_context);
        BookCategoryRepository = new Repository<BookCategory>(_context);
        ReviewRepository = new Repository<Review>(_context);
        OrderRepository = new Repository<Order>(_context);
        OrderItemRepository = new Repository<OrderItem>(_context);
        CartRepository = new Repository<Cart>(_context);
        CartItemRepository = new Repository<CartItem>(_context);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}