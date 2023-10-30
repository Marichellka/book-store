using AutoMapper;
using BookStore.BLL.DTOs.Author;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class AuthorService: BaseService
{
    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<AuthorDto>> GetAll()
    {
        var authors = await UnitOfWork.AuthorRepository.GetAll();
        return Mapper.Map<ICollection<AuthorDto>>(authors);
    }

    public async Task<ICollection<BookDto>> GetAuthorBooks(int authorId)
    {
        var books = await UnitOfWork.BookRepository.GetAll(book => book.AuthorId == authorId);

        return Mapper.Map<ICollection<BookDto>>(books);
    }

    public async Task<AuthorDto> GetById(int id)
    {
        var author = await UnitOfWork.AuthorRepository.GetById(id) ??
                   throw new NotFoundException(nameof(Author), id);

        return Mapper.Map<AuthorDto>(author);
    }
    
    public async Task<AuthorDto> Create(NewAuthorDto author)
    {
        var authorEntity = Mapper.Map<Author>(author);
        await UnitOfWork.AuthorRepository.Add(authorEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<AuthorDto>(authorEntity);
    }

    public async Task<AuthorDto> Update(AuthorDto updatedAuthor)
    {
        if (await UnitOfWork.AuthorRepository.GetById(updatedAuthor.Id) is null)
            throw new NotFoundException(nameof(Author), updatedAuthor.Id);

        await UnitOfWork.AuthorRepository.Update(Mapper.Map<Author>(updatedAuthor));
        await UnitOfWork.SaveChangesAsync();

        return updatedAuthor;
    }

    public async Task Delete(int id)
    {
        var author = await UnitOfWork.AuthorRepository.GetById(id) ??
                   throw new NotFoundException(nameof(Author), id);

        await UnitOfWork.AuthorRepository.Delete(author);
        await UnitOfWork.SaveChangesAsync();
    }
}