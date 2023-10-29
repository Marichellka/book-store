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
        UserRepository = new GenericRepository<User>(_context);
        BookRepository = new GenericRepository<Book>(_context);
        AuthorRepository = new GenericRepository<Author>(_context);
        PublisherRepository = new GenericRepository<Publisher>(_context);
        CategoryRepository = new GenericRepository<Category>(_context);
        BookCategoryRepository = new GenericRepository<BookCategory>(_context);
        ReviewRepository = new GenericRepository<Review>(_context);
        OrderRepository = new GenericRepository<Order>(_context);
        OrderItemRepository = new GenericRepository<OrderItem>(_context);
        CartRepository = new GenericRepository<Cart>(_context);
        CartItemRepository = new GenericRepository<CartItem>(_context);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}