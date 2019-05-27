using Moq;
using Xunit;
using Blog.Api.Command;
using Blog.Api.Model;
using Blog.Api.Controllers;
using Blog.UnitTest.MockData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Blog.UnitTest
{
    public class PostControllerTest
    {
        [Fact]
        public void GetAllPosts()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();

            mockPost.Setup(p => p.WriteTransaction(It.IsAny<string>(), null)).Returns(mockData.Get);
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.GetAllPosts().Result as OkObjectResult;
            var expect = mockData.Get();

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void DeletePost()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();
            var slug = "slug-1";

            mockPost.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), null)).Returns(true);
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.DeletePost(slug).Result as OkObjectResult;
            var expect = true;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void GetNewestPosts()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();
            var num = 2;

            mockPost.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(mockData.Get().Take(num));
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.GetNewestPosts(num).Result as OkObjectResult;
            var expect = mockData.Get().Take(2);

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void GetPostsByCategory()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();
            var cateName = "DotNet";

            mockPost.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(mockData.Get());
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.GetPostsByCategory(cateName).Result as OkObjectResult;
            var expect = mockData.Get();

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void CreatePost_ExistPost()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();
            var itemPost = new Post()
            {
                Slug = "slug-5",
                ShortDescription = "short des 5",
                Content = "content 5",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            };
            var item = new CreatePostDto()
            {
                Post = itemPost,
                CategoriesName = "DotNet"
            };

            mockPost.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(mockData.Get());
            mockPost.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.CreatePost(item).Result as OkObjectResult;
            var expect = false;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void CreatePost_NotExistPost()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();
            var itemPost = new Post()
            {
                Slug = "slug-5",
                ShortDescription = "short des 5",
                Content = "content 5",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            };
            var item = new CreatePostDto()
            {
                Post = itemPost,
                CategoriesName = "DotNet"
            };

            mockPost.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<Post>());
            mockPost.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.CreatePost(item).Result as OkObjectResult;
            var expect = true;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void EditPost()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new PostData();
            var item = new Post()
            {
                Slug = "slug-1",
                ShortDescription = "short des 1 edit",
                Content = "content 5 edit",
                ThumbnailImage = "thum.png",
                CreatedDate = "2018-07-04T19:32:24",
                UpdatedDate = "2018-07-04T19:32:24"
            };

            mockPost.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            PostController post = new PostController(mockPost.Object, mockCate.Object);
            var result = post.EditPost(item).Result as OkObjectResult;
            var expect = true;

            Assert.Equal(expect, result.Value);
        }
    }
}
