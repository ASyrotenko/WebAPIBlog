using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Comment
{
    public class UpdateCommentRequest
    {
        [Required]
        public int CommentId { get; set; }
        [Required]
        [StringLength(400, MinimumLength = 1)]
        public string Text { get; set; }
    }
}
