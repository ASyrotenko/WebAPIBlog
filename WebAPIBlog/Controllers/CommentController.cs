using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIBlog.Models;
using WebAPIBlog.Requests;
using WebAPIBlog.Requests.Comment;
using WebAPIBlog.Requests.Post;

namespace WebAPIBlog.Controllers
{
    [Route("api/post/comments")]
    public class CommentController : Controller
    {
        private readonly ApplicationContext dbContext;
        public CommentController(ApplicationContext applicationContext)
        {
            dbContext = applicationContext;
        }

        [HttpGet]
        public string Index()
        {
            return "Comment Controller";
        }

        [HttpPost]
        public IActionResult CreateComment([FromBody] CreateCommentRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newComment = new Comment
            {
                Text = req.Text,
                UserId = req.UserId,
                PostId = req.PostId
            };

            dbContext.Add(newComment);
            dbContext.SaveChanges();

            return Ok(new
            {
                NewCommentText = req.Text
            });
        }

        [HttpPut]
        public IActionResult UpdateComment([FromBody] UpdateCommentRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = dbContext
                .Comments
                .Single(t => t.Id == req.CommentId);

            comment.Text = req.Text;
            dbContext.SaveChanges();

            return Ok(new
            {
                CommentId = comment.Id,
                NewText = req.Text
            });
        }

        [HttpDelete]
        public IActionResult DeleteComment([FromBody] DeleteCommentRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var comment = dbContext
                .Comments
                .Single(t => t.Id == req.CommentId);

            dbContext.Remove(comment);
            dbContext.SaveChanges();

            return Ok(new
            {
                RemovedCommentId = req.CommentId
            });
        }

        [HttpPost("rate")]
        public IActionResult VoteComment([FromBody] VoteCommentRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userAlreadyVoted = dbContext
                .CommentsRating
                .Where(t => t.CommentId == req.CommentId)
                .Where(t => t.UserId == req.UserId)
                .Any();

            if (userAlreadyVoted)
            {
                return Conflict(new
                {
                    Reason = $"User with id {req.UserId} already voted for this comment"
                });
            }

            var commentRate = new CommentRating
            {
                Vote = req.Vote.Value,
                UserId = req.UserId.Value,
                CommentId = req.CommentId.Value
            };

            dbContext.Add(commentRate);
            dbContext.SaveChanges();

            return Ok(new
            {
                RatedCommentId = req.CommentId,
                CommentVote = req.Vote
            });
        }

    }
}
