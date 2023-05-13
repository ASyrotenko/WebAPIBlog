using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Comment
{
    public class CreateCommentRequest
    {
        [Required]
        [MaxLength(400)]
        public string Text { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PostId { get; set; }
    }
}
