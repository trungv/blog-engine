namespace Blog.Api.Model
{
    public class CreatePostDto
    {
        public string CategoriesName { get; set; }
        public Post Post { get; set; }
    }
}
