namespace BlogApplication.API.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String UrlHandle { get; set; }

        public ICollection<BlogPost> BlogPosts { get; set;}
    }
}
