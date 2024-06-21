using AutoMapper;
using Final.Domain.Dto;
using Final.Dto;
using Final.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddPostDto, Post>();
        CreateMap<UpdateCommentDto, Comment>();
        CreateMap<AddCommentDto, Comment>();

        CreateMap<UpdateUserDto, User>();
        CreateMap<AddUserDto, User>();
        CreateMap<UpdatePostDto, Post>();
    }
}
