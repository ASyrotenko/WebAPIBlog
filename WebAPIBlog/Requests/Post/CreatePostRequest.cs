using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Post
{
    public class CreatePostRequest
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(400)]
        public string Text { get; set; }
        [Required]
        public int? UserId { get; set; }
    }
}
