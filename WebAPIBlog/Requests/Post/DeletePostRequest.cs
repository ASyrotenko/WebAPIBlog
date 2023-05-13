using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.Post
{
    public class DeletePostRequest
    {
        [Required]
        public int? PostId { get; set; }
    }
}
