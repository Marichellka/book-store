using AutoMapper;
using BookStore.BLL.DTOs.Book;
using BookStore.BLL.DTOs.Publisher;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class PublisherService: BaseService
{
    public PublisherService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<PublisherDto>> GetAll()
    {
        var publishers = await UnitOfWork.PublisherRepository.GetAll();
        return Mapper.Map<ICollection<PublisherDto>>(publishers);
    }

    public async Task<ICollection<BookDto>> GetPublisherBooks(int publisherId)
    {
        var books = await UnitOfWork.BookRepository.GetAll(book => book.PublisherId == publisherId);

        return Mapper.Map<ICollection<BookDto>>(books);
    }

    public async Task<PublisherDto> GetById(int id)
    {
        var publisher = await UnitOfWork.PublisherRepository.GetById(id) ??
                     throw new NotFoundException(nameof(Publisher), id);

        return Mapper.Map<PublisherDto>(publisher);
    }
    
    public async Task<PublisherDto> Create(NewPublisherDto publisher)
    {
        var publisherEntity = Mapper.Map<Publisher>(publisher);
        await UnitOfWork.PublisherRepository.Add(publisherEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<PublisherDto>(publisherEntity);
    }

    public async Task<PublisherDto> Update(PublisherDto updatedPublisher)
    {
        if (await UnitOfWork.PublisherRepository.GetById(updatedPublisher.Id) is null)
            throw new NotFoundException(nameof(Publisher), updatedPublisher.Id);

        await UnitOfWork.PublisherRepository.Update(Mapper.Map<Publisher>(updatedPublisher));
        await UnitOfWork.SaveChangesAsync();

        return updatedPublisher;
    }

    public async Task Delete(int id)
    {
        var publisher = await UnitOfWork.PublisherRepository.GetById(id) ??
                     throw new NotFoundException(nameof(Publisher), id);

        await UnitOfWork.PublisherRepository.Delete(publisher);
        await UnitOfWork.SaveChangesAsync();
    }
}