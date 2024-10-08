﻿using BlogApplication.API.Models.Domain;

namespace BlogApplication.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);

        Task<IEnumerable<BlogImage>> GetAllAsync();
    }
}
