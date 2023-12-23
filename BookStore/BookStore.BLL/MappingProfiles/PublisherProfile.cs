using AutoMapper;
using BookStore.BLL.DTOs.Publisher;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class PublisherProfile: Profile
{
    public PublisherProfile()
    {
        CreateMap<Publisher, PublisherDto>();
        CreateMap<NewPublisherDto, Publisher>();
        CreateMap<NewPublisherDto, PublisherDto>();
    }
}