using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace WebAPIBlog.Requests.Post
{
    public class UpdatePostRequest
    {
        [Required]
        public int? PostId { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Text { get; set; }
    }
}
