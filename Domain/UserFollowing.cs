namespace Domain;

public class UserFollowing
{
    public required string FollowerId { get; set; }
    public User Follower { get; set; } = null!;
    public required string TargetId { get; set; }
    public User Target { get; set; } = null!;

}