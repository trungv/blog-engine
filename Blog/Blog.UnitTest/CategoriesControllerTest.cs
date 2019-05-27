using Moq;
using Xunit;
using Blog.Api.Command;
using Blog.Api.Model;
using Blog.Api.Controllers;
using Blog.UnitTest.MockData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.UnitTest
{
    public class CategoriesControllerTest
    {
        [Fact]
        public void GetAllCategories()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new CategoriesData();

            mockCate.Setup(p => p.WriteTransaction(It.IsAny<string>(),null)).Returns(mockData.Get);
            CategoriesController categories = new CategoriesController(mockPost.Object, mockCate.Object);
            var result = categories.GetAllCategories().Result as OkObjectResult;
            var expect = mockData.Get();

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void EditCategories()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new CategoriesData();
            var item = new Categories()
            {
                Name = "DotNet",
                Description = "Description DotNet edit"
            };

            mockCate.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            CategoriesController categories = new CategoriesController(mockPost.Object, mockCate.Object);
            var result = categories.EditCategories(item).Result as OkObjectResult;
            var expect = true;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void CreateCategories_ExistCategories()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new CategoriesData();
            var item = new Categories()
            {
                Name = "BlockChain",
                Description = "Description BlockChain"
            };
            mockCate.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(mockData.Get);
            mockCate.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            CategoriesController categories = new CategoriesController(mockPost.Object, mockCate.Object);
            var result = categories.CreateCategories(item).Result as OkObjectResult;
            var expect = false;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void CreateCategories_NotExistCategories()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new CategoriesData();
            var item = new Categories()
            {
                Name = "BlockChain",
                Description = "Description BlockChain"
            };
            mockCate.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<Categories>());
            mockCate.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            CategoriesController categories = new CategoriesController(mockPost.Object, mockCate.Object);
            var result = categories.CreateCategories(item).Result as OkObjectResult;
            var expect = true;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void DeleteCategiries_ExistPostInCategories()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new CategoriesData();
            string itemName = "DotNet";

            mockCate.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(mockData.Get);
            mockCate.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            CategoriesController categories = new CategoriesController(mockPost.Object, mockCate.Object);
            var result = categories.DeleteCategories(itemName).Result as OkObjectResult;
            var expect = false;

            Assert.Equal(expect, result.Value);
        }

        [Fact]
        public void DeleteCategories_NotExistPostInCategories()
        {
            var mockCate = new Mock<ICommand<Categories>>();
            var mockPost = new Mock<ICommand<Post>>();
            var mockData = new CategoriesData();
            string itemName = "DotNet";

            mockCate.Setup(p => p.WriteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<Categories>());
            mockCate.Setup(p => p.ExcuteTransaction(It.IsAny<string>(), It.IsAny<object>())).Returns(true);
            CategoriesController categories = new CategoriesController(mockPost.Object, mockCate.Object);
            var result = categories.DeleteCategories(itemName).Result as OkObjectResult;
            var expect = true;

            Assert.Equal(expect, result.Value);
        }
    }
}
