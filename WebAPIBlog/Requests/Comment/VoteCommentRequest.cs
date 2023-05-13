using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Comment
{
    public class VoteCommentRequest
    {
        [Required]
        public int? CommentId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public bool? Vote { get; set; }
    }
}
