using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Comment
{
    public class DeleteCommentRequest
    {
        [Required]
        public int CommentId { get; set; }
    }
}
