using BlogApplication.API.Data;
using BlogApplication.API.Models.Domain;
using BlogApplication.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BlogApplication.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BlogImage>> GetAllAsync()
        {
            return await dbContext.BlogImages.ToListAsync();
        }

        public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
        {
            // 1- Upload the Image to the server Images folder
            //Define the path to save the image
            var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{blogImage.FileName}{blogImage.FileExtension}");
            // Create a stream to transfer the file
            using var stream = new FileStream(localPath, FileMode.Create);
            //Copy the file to the stream, saving it to the server's Images folder.
            await file.CopyToAsync(stream);

            // 2- Update the database
            //URL Path will be something like: https:/blog-application.com/images/somefilename.jpg
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";

            blogImage.Url = urlPath;

            await dbContext.BlogImages.AddAsync(blogImage);
            await dbContext.SaveChangesAsync();

            return blogImage;


        }
    }

}
