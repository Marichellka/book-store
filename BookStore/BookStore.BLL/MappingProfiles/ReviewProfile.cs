using AutoMapper;
using BookStore.BLL.DTOs.Review;
using BookStore.DAL.Models;

namespace BookStore.BLL.MappingProfiles;

public class ReviewProfile: Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<NewReviewDto, Review>();
        CreateMap<NewReviewDto, ReviewDto>();
    }
    
}