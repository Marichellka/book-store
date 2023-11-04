using AutoMapper;
using BookStore.BLL.DTOs.Cart;
using BookStore.BLL.DTOs.User;
using BookStore.BLL.Exceptions;
using BookStore.DAL.Models;
using BookStore.DAL.Specifications.Users;
using BookStore.DAL.UnitOfWork;

namespace BookStore.BLL.Services;

public class UserService: BaseService
{
    public UserService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    
    public async Task<ICollection<UserDto>> GetAll()
    {
        var users = await UnitOfWork.UserRepository.GetAll();
        return Mapper.Map<ICollection<UserDto>>(users);
    }

    public async Task<UserDto> GetById(int id)
    {
        var user = await UnitOfWork.UserRepository.GetById(id) ??
                   throw new NotFoundException(nameof(User), id);

        return Mapper.Map<UserDto>(user);
    }
    
    public async Task<UserDto> Login(NewUserDto userDto)
    {
        var user = (await UnitOfWork.UserRepository.GetAll(new UserNameSpecification(userDto.Name))).FirstOrDefault() ??
                   throw new NotFoundException($"User with name {userDto.Name} is not found");

        if (!BCrypt.Net.BCrypt.Verify(userDto.Password, user.PasswordHash))
            throw new ArgumentException("Invalid password");

        return Mapper.Map<UserDto>(user);
    }
    
    public async Task<UserDto> Create(NewUserDto user)
    {
        var userEntity = Mapper.Map<User>(user);
        userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

        await UnitOfWork.UserRepository.Add(userEntity);
        await UnitOfWork.SaveChangesAsync();

        return Mapper.Map<UserDto>(userEntity);
    }

    public async Task<UserDto> Update(UserDto updatedUser)
    {
        if (await UnitOfWork.UserRepository.GetById(updatedUser.Id) is null) 
            throw new NotFoundException(nameof(User), updatedUser.Id);

        await UnitOfWork.UserRepository.Update(Mapper.Map<User>(updatedUser));
        await UnitOfWork.SaveChangesAsync();

        return updatedUser;
    }

    public async Task Delete(int id)
    {
        var user = await UnitOfWork.UserRepository.GetById(id) ??
                   throw new NotFoundException(nameof(User), id);

        await UnitOfWork.UserRepository.Delete(user);
        await UnitOfWork.SaveChangesAsync();
    }
}