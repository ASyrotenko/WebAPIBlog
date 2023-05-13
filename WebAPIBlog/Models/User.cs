using System.Collections;
using System.Collections.Generic;

namespace WebAPIBlog.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<PostRating> PostsRating { get; set; }
        public ICollection<CommentRating> CommentsRating { get; set; }
    }
}
