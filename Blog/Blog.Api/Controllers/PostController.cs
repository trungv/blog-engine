using System;
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
    public class PostController : ApiBaseController
    {
        public PostController(ICommand<Post> postCommand, ICommand<Categories> categoriesCommand)
            : base(postCommand, categoriesCommand)
        {
        }

        [HttpGet("{categoriesName}")]
        public ActionResult<IEnumerable<Post>> GetPostsByCategory(string categoriesName)
        {
            string query = "MATCH (node1:Post)-[:BELONG_TO]->(node2:Categories) " +
                           "WHERE node2.name = $categoriesName " +
                           "RETURN node1";
            object param = new { categoriesName };

            return Ok(PostCommand.WriteTransaction(query, param));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAllPosts()
        {
            string query = "MATCH (node:Post) " +
                           "RETURN node";

            return Ok(PostCommand.WriteTransaction(query, null));
        }

        [HttpDelete("{slug}")]
        //[Authorize]
        public ActionResult<bool> DeletePost(string slug)
        {
            string query = "MATCH (n:Post { slug: '"+ slug + "' }) " +
                           "DETACH DELETE n ";

            return Ok(PostCommand.ExcuteTransaction(query, null));
        }

        [HttpGet("Newest/{num}")]
        public ActionResult<IEnumerable<Post>> GetNewestPosts(int num)
        {
            string query = "MATCH (node1:Post)  " +
                           "RETURN node1    " +
                           "ORDER BY node1.CreatedDate DESC  " +
                           "LIMIT $num  ";
            object param = new { num };

            return Ok(PostCommand.WriteTransaction(query, param));
        }

        [HttpPost]
        //[Authorize]
        public ActionResult<bool> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            string postQuery = "MATCH (p:Post{slug:'" + createPostDto.Post.Slug + "'}) RETURN p";
            var exsitPost = PostCommand.WriteTransaction(postQuery, null).ToList();
            if (exsitPost.Count > 0)
            {
                return Ok(false);
            }

            string query = "CREATE(p:Post{" +
                "slug : $slug, " +
                "ShortDescription : $shortDescription, " +
                "Content : $content, " +
                "ThumbnailImage : $thumbnailImage, " +
                "CreatedDate : $createdDate, " +
                "UpdatedDate : $updatedDate " +
                "}) ";
            string relationship = string.Format(
                "WITH p " +
                "MATCH (node:Categories) " +
                "WHERE node.name = '{0}' " +
                "CREATE (p)-[:BELONG_TO]->(node) ", createPostDto.CategoriesName);

            object param = new
            {
                slug = createPostDto.Post.Slug,
                shortDescription = createPostDto.Post.ShortDescription,
                content = createPostDto.Post.Content,
                thumbnailImage = createPostDto.Post.ThumbnailImage,
                createdDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                updatedDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            return Ok(PostCommand.ExcuteTransaction(query+relationship, param));
        }

        [HttpPut]
        //[Authorize]
        public ActionResult<bool> EditPost([FromBody] Post post)
        {
            string query = "MATCH (node:Post) " +
                           "WHERE node.slug = $slug " +
                           "SET node = {" +
                           "slug: $slug, " +
                           "ShortDescription: $shortDescription, " +
                           "ThumbnailImage: $thumbnailImage, " +
                           "Content: $content, " +
                           "CreatedDate: $createdDate, " +
                           "UpdatedDate: $updatedDate} ";
            object param = new
            {
                slug = post.Slug,
                shortDescription = post.ShortDescription,
                thumbnailImage = post.ThumbnailImage,
                content = post.Content,
                createdDate = post.CreatedDate,
                updatedDate = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            return Ok(PostCommand.ExcuteTransaction(query, param));
        }
    }
}