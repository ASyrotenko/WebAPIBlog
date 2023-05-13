using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAPIBlog.Models;
using WebAPIBlog.Requests.User;

namespace WebAPIBlog.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly ApplicationContext dbContext;

        public UserController(ApplicationContext applicationContext)
        {
            dbContext = applicationContext;
        }

        [HttpGet]
        public string Index()
        {
            return "User Controller";
        }

        [HttpPost]
        public IActionResult CreateNewUser([FromBody] CreateUserRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newUser = new User
            {
                Name = req.Name
            };
            dbContext.Add(newUser);
            dbContext.SaveChanges();
            return Ok(new
            {
                NewUserAdded = req.Name
            });
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = dbContext
                .Users
                .Single(t => t.Id == req.UserId);

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                user.Name = req.Name;
            }
            
            dbContext.SaveChanges();

            return Ok(new
            {
                NewUserName = req.Name
            });
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] DeleteUserRequest req)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var posts = dbContext
                .Posts
                .Where(t => t.UserId == req.UserId)
                .Any();

            if (posts)
            {
                return BadRequest(new
                {
                    Reason = $"Can`t remove User with posts. This User has posts"
                });
            }

            var user = dbContext
                .Users
                .Single(t => t.Id == req.UserId);

            dbContext.Remove(user);
            dbContext.SaveChanges();

            return Ok(new
            {
                UserRemoved = user.Name,
            });
        }
    }
}
