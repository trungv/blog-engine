using System;
using System.Collections.Generic;
using System.Text;
using Blog.Api.Command;
using Blog.Api.Model;

namespace Blog.UnitTest.MockData
{
    public class CategoriesData
    {
        private IEnumerable<Categories> _categoriesList;

        public CategoriesData()
        {
            List<Categories> categories = new List<Categories>();
            categories.Add(new Categories() {Name = "DotNet", Description = "Description DotNet"});
            categories.Add(new Categories() { Name = "iOS", Description = "Description iOS" });
            categories.Add(new Categories() { Name = "Php", Description = "Description Php" });
            categories.Add(new Categories() { Name = "Java", Description = "Description Java" });
            _categoriesList = categories;
        }

        public IEnumerable<Categories> Get()
        {
            return _categoriesList;
        }

    }
}
