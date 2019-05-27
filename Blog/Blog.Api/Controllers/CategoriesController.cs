using System.Collections.Generic;
using System.Linq;
using Blog.Api.Command;
using Blog.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ApiBaseController
    {
        public CategoriesController(ICommand<Post> postCommand, ICommand<Categories> categoriesCommand)
            : base(postCommand, categoriesCommand)
        {
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categories>> GetAllCategories()
        {
            return Ok(CategoriesCommand.WriteTransaction("MATCH (node1:Categories) RETURN node1", null));
        }

        [HttpPost]
        //[Authorize]
        public ActionResult<bool> CreateCategories([FromBody] Categories category)
        {
            string exsitCategoriese = "MATCH (c:Categories{name:'" + category.Name + "'}) RETURN c";
            var exsitPost = CategoriesCommand.WriteTransaction(exsitCategoriese, null).ToList();
            if (exsitPost.Count > 0)
            {
                return Ok(false);
            }
            string query = "CREATE(" + category.Name.Trim() + ":Categories{name : $name, Description : $description})";
            object param = new
            {
                name = category.Name,
                description = category.Description
            };

            return Ok(CategoriesCommand.ExcuteTransaction(query, param));
        }

        [HttpPut]
        //[Authorize]
        public ActionResult<bool> EditCategories([FromBody] Categories category)
        {
            string query = "MATCH (node:Categories) " +
                           "WHERE node.name = $name " +
                           "SET node.Description = $description ";
            object param = new
            {
                name = category.Name,
                description = category.Description
            };

            return Ok(CategoriesCommand.ExcuteTransaction(query, param));
        }
        [HttpDelete("{categoriesName}")]
        //[Authorize]
        public ActionResult<bool> DeleteCategories(string categoriesName)
        {
            string exsitPostInCate = "MATCH (p:Post)-[:BELONG_TO]->(c:Categories{name:'" + categoriesName+"'}) RETURN c";
            var exsitPost = CategoriesCommand.WriteTransaction(exsitPostInCate, null).ToList();
            if (exsitPost.Count > 0 )
            {
                return Ok(false);
            }
            string query = "MATCH (n:Categories { name: '" + categoriesName + "' }) " +
                           "DETACH DELETE n ";

            return Ok(CategoriesCommand.ExcuteTransaction(query, null));
        }
    }
}