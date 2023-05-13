using System.Collections;
using System.Collections.Generic;

namespace WebAPIBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostRating> PostsRating { get; set; }
    }
}
