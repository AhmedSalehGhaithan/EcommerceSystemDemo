﻿using AutoMapper;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between DTOs and domain entities.
    /// </summary>
    public class MappingConfig : Profile
    {
        /// <summary>
        /// Configures the mappings between DTOs and domain entities.
        /// </summary>
        public MappingConfig()
        {
            // Map from CreateProduct DTO to Product entity
            CreateMap<CreateProduct, Product>();

            // Map from CreateCategory DTO to Category entity
            CreateMap<CreateCategory, Category>();

            // Map from Product entity to GetProduct DTO
            CreateMap<Product, GetProduct>();

            // Map from Category entity to GetCategory DTO
            CreateMap<Category, GetCategory>();

            // Map from UpdateCategory DTO to Category entity
            CreateMap<UpdateCategory, Category>();

            // Map from UpdateProduct DTO to Product entity
            CreateMap<UpdateProduct, Product>();
        }
    }
}
