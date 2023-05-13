using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Post
{
    public class VotePostRequest
    {
        [Required]
        public int? PostId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public bool? Vote { get; set; }
    }
}
