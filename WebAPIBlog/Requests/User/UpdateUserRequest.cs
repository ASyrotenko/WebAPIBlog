using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.User
{
    public class UpdateUserRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
