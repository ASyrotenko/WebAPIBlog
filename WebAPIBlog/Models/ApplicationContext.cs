using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebAPIBlog.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostRating> PostsRating { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentRating> CommentsRating { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder
                .Entity<User>()
                .ToTable("Users");

            modelBuilder
                .Entity<User>()
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<User>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder
                .Entity<User>()
                .HasMany(t => t.Posts)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<User>()
                .HasMany(t => t.PostsRating)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<User>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<User>()
                .HasMany(t => t.CommentsRating)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Post
            modelBuilder
                .Entity<Post>()
                .ToTable("Posts");

            modelBuilder
                .Entity<Post>()
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<Post>()
                .Property(t => t.Title)
                .IsRequired();

            modelBuilder
                .Entity<Post>()
                .Property(t => t.Text)
                .IsRequired();

            modelBuilder
                .Entity<Post>()
                .HasMany(t => t.Comments)
                .WithOne(t => t.Post)
                .HasForeignKey(t => t.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Post>()
                .HasMany(t => t.PostsRating)
                .WithOne(t => t.Post)
                .HasForeignKey(t => t.PostId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Post Rating
            modelBuilder
                .Entity<PostRating>()
                .ToTable("PostsRating");

            modelBuilder
                .Entity<PostRating>()
                .HasKey(t => t.Id);

            // Захищає від можливості поставити одночасно оцінку від одного юзера під один пост
            // більше одного разу (в тому числі одночастно)
            modelBuilder
                .Entity<PostRating>()
                .HasIndex(x => new { x.UserId, x.PostId })
                .IsUnique(true);
            #endregion

            #region Comment
            modelBuilder
                .Entity<Comment>()
                .ToTable("Comments");

            modelBuilder
                .Entity<Comment>()
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<Comment>()
                .Property(t => t.Text)
                .IsRequired();

            //modelBuilder
            //    .Entity<Comment>()
            //    .HasOne(t => t.User)
            //    .WithMany(t => t.Comments)
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Comment>()
                .HasMany(t => t.CommentsRating)
                .WithOne(t => t.Comment)
                .HasForeignKey(t => t.CommentId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Comment Rating
            modelBuilder
                .Entity<CommentRating>()
                .ToTable("CommentsRating");

            modelBuilder
                .Entity<Comment>()
                .HasKey(t => t.Id);

            modelBuilder
                .Entity<CommentRating>()
                .HasIndex(x => new { x.UserId, x.CommentId })
                .IsUnique(true);
            #endregion
        }
    }
}
