using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.User
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
