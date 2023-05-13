using System.ComponentModel.DataAnnotations;

namespace WebAPIBlog.Requests.User
{
    public class DeleteUserRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}
