using System.Reflection;
using BookStore.BLL.MappingProfiles;
using BookStore.BLL.Services;
using BookStore.DAL.Contexts;
using BookStore.DAL.UnitOfWork;

namespace BookStore.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthorService>();
        services.AddScoped<PublisherService>();
        services.AddScoped<BookService>();
        services.AddScoped<CartService>();
        services.AddScoped<OrderService>();
        services.AddScoped<ReviewService>();
        services.AddScoped<CategoryService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<AppDbContext>();
    }
    
    public static  void ConfigureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {
                cfg.AddProfile<UserProfile>(); 
                cfg.AddProfile<AuthorProfile>(); 
                cfg.AddProfile<PublisherProfile>(); 
                cfg.AddProfile<BookProfile>(); 
                cfg.AddProfile<CartProfile>(); 
                cfg.AddProfile<OrderProfile>(); 
                cfg.AddProfile<ReviewProfile>(); 
                cfg.AddProfile<CategoryProfile>();
            },
            Assembly.GetExecutingAssembly());
    }

}