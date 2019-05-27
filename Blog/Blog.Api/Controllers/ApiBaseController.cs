using Blog.Api.Command;
using Blog.Api.Model;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        public ICommand<Post> PostCommand;
        public ICommand<Categories> CategoriesCommand;

        public ApiBaseController(ICommand<Post> postCommand, ICommand<Categories> categoriesCommand)
        {
            PostCommand = postCommand;
            CategoriesCommand = categoriesCommand;
        }
    }
}