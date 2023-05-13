namespace WebAPIBlog.Models
{
    public class PostRating
    {
        public int Id { get; set; }
        public bool Vote { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
