using BookStore.DAL.Models;
using BookStore.DAL.Repositories;

namespace BookStore.DAL.UnitOfWork;

public interface IUnitOfWork
{
    IRepository<User> UserRepository { get; }
    IRepository<Book> BookRepository { get; }
    IRepository<Author> AuthorRepository { get; }
    IRepository<Publisher> PublisherRepository { get; }
    IRepository<Category> CategoryRepository { get; }
    IRepository<BookCategory> BookCategoryRepository { get; }
    IRepository<Review> ReviewRepository { get; }
    IRepository<Order> OrderRepository { get; }
    IRepository<OrderItem> OrderItemRepository { get; }
    IRepository<Cart> CartRepository { get; }
    IRepository<CartItem> CartItemRepository { get; }
    
    Task<int> SaveChangesAsync();
}