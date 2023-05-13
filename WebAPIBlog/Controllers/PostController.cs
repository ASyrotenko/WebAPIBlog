using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIBlog.Models;
using WebAPIBlog.Requests;
using WebAPIBlog.Requests.Post;

namespace WebAPIBlog.Controllers
{
    [Route("api/post")]
    public class PostController : Controller
    {
        private readonly ApplicationContext dbContext;

        public PostController(ApplicationContext applicationContext)
        {
            dbContext = applicationContext;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var allPosts = dbContext
                .Posts
                .Select(x => new
                {
                    x.Title,
                    x.Id,
                    x.User.Name,
                    Likes = x.PostsRating.Where(x => x.Vote == true).Count(),
                    Dislike = x.PostsRating.Where(x => x.Vote == false).Count()
                });

            return Ok(new
            {
                AllPosts = allPosts
            });
        }

        [HttpGet("{postId}")]
        public IActionResult GetPostById([FromRoute] int postId)
        {
            var post = dbContext
                .Posts
                .Where(x => x.Id == postId)
                .Select(x => new
                {
                    x.User.Name,
                    x.Id,
                    x.Title,
                    x.Text,
                    PostLikes = x.PostsRating.Where(l => l.Vote == true).Count(),
                    PostDisikes = x.PostsRating.Where(l => l.Vote == false).Count(),
                    Comments = x.Comments.Select(x => new
                    {
                        x.Id,
                        x.Text,
                        Likes = x.CommentsRating.Where(l => l.Vote == true).Count(),
                        Dislikes = x.CommentsRating.Where(l => l.Vote == false).Count()
                    })
                });

            return Ok(new
            {
                Post = post
            });
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody] CreatePostRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newPost = new Post
            {
                Title = req.Title,
                Text = req.Text,
                UserId = req.UserId.Value
            };

            dbContext.Add(newPost);
            dbContext.SaveChanges();

            return Ok(new
            {
                PostTitle = req.Title,
                PostText = req.Text
            });
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var post = dbContext
                .Posts
                .Single(t => t.Id == req.PostId);

            if (!string.IsNullOrWhiteSpace(req.Title))
            {
                post.Title = req.Title;
            }

            if (!string.IsNullOrWhiteSpace(req.Text))
            {
                post.Text = req.Text;
            }

            dbContext.SaveChanges();

            return Ok(new
            {
                NewPostTitle = post.Title,
                NewPostText = post.Text
            });
        }

        [HttpDelete]
        public IActionResult DeletePost([FromBody] DeletePostRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var post = dbContext
                .Posts
                .Single(t => t.Id == req.PostId);

            dbContext.Remove(post);
            dbContext.SaveChanges();

            return Ok(new
            {
                RemovedPostId = req.PostId,
                RemovedPostTitle = post.Title
            });
        }

        [HttpPost("rate")]
        public IActionResult VotePost([FromBody] VotePostRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userAlreadyVoted = dbContext
                .PostsRating
                .Where(t => t.PostId == req.PostId)
                .Where(t => t.UserId == req.UserId)
                .Any();

            if (userAlreadyVoted)
            {
                return Conflict(new
                {
                    Reason = $"User with id {req.UserId} already voted for this post"
                });
            }

            var postRate = new PostRating
            {
                Vote = req.Vote.Value,
                UserId = req.UserId.Value,
                PostId = req.PostId.Value
            };

            dbContext.Add(postRate);
            dbContext.SaveChanges();

            return Ok(new
            {
                RatedPostId = req.PostId,
                PostVote = req.Vote
            });
        }
    }
}
