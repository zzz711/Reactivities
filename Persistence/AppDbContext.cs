using System.ComponentModel;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
{
    public required DbSet<Activity> Activities { get; set; }
    public required DbSet<ActivityAttendee> ActivityAttendees { get; set; }
    public required DbSet<Photo> Photos { get; set; }
    public required DbSet<Comment> Comments { get; set; }
    public DbSet<UserFollowing> UserFollowings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ActivityAttendee>(x => x.HasKey(a => new {a.ActivityId, a.UserId}));

        builder.Entity<ActivityAttendee>().HasOne(x => x.User).WithMany(x => x.Activities).HasForeignKey(x => x.UserId);
        builder.Entity<ActivityAttendee>().HasOne(x => x.Activity).WithMany(x => x.Attendees).HasForeignKey(x => x.ActivityId);

        builder.Entity<UserFollowing>(x =>
        {
            x.HasKey(k => new {k.FollowerId, k.TargetId});
            x.HasOne(f => f.Follower).WithMany(o => o.Followings).HasForeignKey(f => f.FollowerId).OnDelete(DeleteBehavior.Cascade);
            x.HasOne(f => f.Target).WithMany(o => o.Followers).HasForeignKey(f => f.TargetId).OnDelete(DeleteBehavior.Cascade);

        });

        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
        );

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }
}