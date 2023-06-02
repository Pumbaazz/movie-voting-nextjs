﻿using AutoMapper;
using WebAPI.Domain.DTO;
using WebAPI.Domain.Model;

namespace WebAPI.Application.Mapper
{
    public static class MovieMapper
    {
        /// <summary>
        /// The mapper for movies dto.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        public static void CreateMap(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<Movies, MoviesDto>();
        }
    }
}
