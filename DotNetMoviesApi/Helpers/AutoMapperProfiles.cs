using AutoMapper;
using DotNetMoviesApi.Dtos;
using DotNetMoviesApi.Entities;

namespace DotNetMoviesApi.Helpers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<GenreDto, Genre>().ReverseMap();
    }   
}