using System;
using System.Collections.Generic;
using System.Text;
using Blog.Api.Command;
using Blog.Api.Model;


namespace Blog.UnitTest.MockData
{
    public class PostData
    {
        private IEnumerable<Post> _postList;
        public PostData()
        {
            List<Post> posts = new List<Post>();
            posts.Add(new Post()
            {
                Slug = "slug-1",
                ShortDescription = "short des 1",
                Content = "content 1",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            });
            posts.Add(new Post()
            {
                Slug = "slug-2",
                ShortDescription = "short des 2",
                Content = "content 2",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            });
            posts.Add(new Post()
            {
                Slug = "slug-3",
                ShortDescription = "short des 3",
                Content = "content 3",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            });
            posts.Add(new Post()
            {
                Slug = "slug-4",
                ShortDescription = "short des 4",
                Content = "content 4",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            });

            _postList = posts;
        }
        public IEnumerable<Post> Get()
        {
            return _postList;
        }
    }
}
