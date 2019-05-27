namespace Blog.Api.Model
{
    public class Post
    {
        public string Slug { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; set; }
        public string ThumbnailImage { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
