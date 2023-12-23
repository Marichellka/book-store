using AutoMapper;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public abstract class BaseService
{
    private protected readonly IUnitOfWork UnitOfWork;
    public readonly IMapper Mapper;


    protected BaseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        Mapper = mapper;
    }

}