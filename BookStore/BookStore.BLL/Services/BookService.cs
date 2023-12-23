using AutoMapper;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Category;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications;
using BookStore.DAL.Specifications.Books;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class BookService: BaseService
{
    public BookService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<BookDto>> GetAll(ISpecification<Book>? specification = null)
    {
        var books = await UnitOfWork.BookRepository.GetAll(specification);
        return Mapper.Map<ICollection<BookDto>>(books);
    }

    public async Task<BookDto> GetById(int id)
    {
        var book = await UnitOfWork.BookRepository.GetById(id) ??
                   throw new NotFoundException(nameof(Book), id);

        return Mapper.Map<BookDto>(book);
    }
    
    public async Task<ICollection<CategoryDto>> GetCategories(int bookId)
    {
        var book = await UnitOfWork.BookRepository.GetById(bookId) ??
                   throw new NotFoundException(nameof(Book), bookId);
        BookCategoriesSpecification specification = new(bookId);

        var categories = await UnitOfWork.CategoryRepository.GetAll(specification);
        return Mapper.Map<ICollection<CategoryDto>>(categories);
    }
    
    public async Task<BookDto> Create(NewBookDto book)
    {
        if (await UnitOfWork.AuthorRepository.GetById(book.AuthorId) is null)
            throw new NotFoundException(nameof(Author), book.AuthorId);
        
        if (await UnitOfWork.PublisherRepository.GetById(book.PublisherId) is null)
            throw new NotFoundException(nameof(Publisher), book.PublisherId);

        var bookEntity = Mapper.Map<Book>(book);
        await UnitOfWork.BookRepository.Add(bookEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<BookDto>(bookEntity);
    }

    public async Task<BookDto> Update(BookDto updatedBook)
    {
        if (await UnitOfWork.BookRepository.GetById(updatedBook.Id) is null) 
            throw new NotFoundException(nameof(Book), updatedBook.Id);
        
        await UnitOfWork.BookRepository.Update(Mapper.Map<Book>(updatedBook));
        await UnitOfWork.SaveChangesAsync();

        return updatedBook;
    }

    public async Task<BookDto> AddCategory(int bookId, CategoryDto category)
    {
        var bookEntity = await UnitOfWork.BookRepository.GetById(bookId) ??
                   throw new NotFoundException(nameof(Book), bookId);

        var categoryEntity = await UnitOfWork.CategoryRepository.GetById(category.Id) ??
                                 throw new NotFoundException(nameof(Category), category.Id);
            
        BookCategory bookCategoryEntity = new (){BookId = bookEntity.Id, CategoryId = categoryEntity.Id}; 
        
        await UnitOfWork.BookCategoryRepository.Add(bookCategoryEntity);
        await UnitOfWork.SaveChangesAsync();
        
        return Mapper.Map<BookDto>(bookEntity);
    }

    public async Task Delete(int id)
    {
        var book = await UnitOfWork.BookRepository.GetById(id) ??
                   throw new NotFoundException(nameof(Book), id);

        await UnitOfWork.BookRepository.Delete(book);
        await UnitOfWork.SaveChangesAsync();
    }
    
    public async Task DeleteCategory(int bookId, int categoryId)
    {
        var book = await UnitOfWork.BookRepository.GetById(bookId) ??
                   throw new NotFoundException(nameof(Book), bookId);

        var category = await UnitOfWork.CategoryRepository.GetById(categoryId) ??
                       throw new NotFoundException(nameof(Book), categoryId);

        if (book.Categories != null)
        {
            var bookCategory = book.Categories.Single(c => c.CategoryId == category.Id);
            await UnitOfWork.BookCategoryRepository.Delete(bookCategory);
            await UnitOfWork.SaveChangesAsync();

        } 
    }
}