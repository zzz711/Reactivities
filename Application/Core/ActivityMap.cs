using Application.Activities.DTO;
using Application.Profiles.DTOs;
using Domain;

namespace Application.Core;

public class ActivityMap
{
    public static void MapActivity(EditActivityDto activity, ref Activity outActivity)
    {

        outActivity.Id = activity.Id;
        outActivity.Title = activity.Title;
        outActivity.Date = activity.Date;
        outActivity.Description = activity.Description;
        outActivity.Category = activity.Category;
        outActivity.City = activity.City;
        outActivity.Venue = activity.Venue;
        outActivity.Latitude = activity.Latitude;
        outActivity.Longitude = activity.Longitude;
    }

    public static Activity MapCreateActivityDTO(CreateActivityDTO activity)
    {
        return new Activity
        {
            Title = activity.Title,
            Date = activity.Date,
            Description = activity.Description,
            Category = activity.Category,
            City = activity.City,
            Venue = activity.Venue,
            Latitude = activity.Latitude,
            Longitude = activity.Longitude
        };
    }

    public static ActivityDto MapActivityDTO(Activity activity, ICollection<ActivityAttendee> attendees, string currentUserId, List<UserFollowing> followers)
    {
        var host = attendees.FirstOrDefault(x => x.IsHost)!.User;

        ICollection<UserProfile> users = [];       

        foreach(var attendee in attendees)
        {
            bool isFollowing = false;

            if (followers.Any(x => x.FollowerId == currentUserId))
                isFollowing = true;

            users.Add(MapUserProfile(attendee, isFollowing));
        }

        return new ActivityDto
        {
            Id = activity.Id,
            Title = activity.Title,
            Date = activity.Date,
            Description = activity.Description,
            Category = activity.Category,
            City = activity.City,
            Venue = activity.Venue,
            Latitude = activity.Latitude,
            Longitude = activity.Longitude,
            IsCancelled = activity.IsCancelled,
            HostDisplayName = host.DisplayName!,
            HostId = host.Id,
            Attendees = users
        };
    }

    public static UserProfile MapUserProfile(ActivityAttendee attendee, bool isFollowing)
    {
        return new UserProfile
        {
            Id = attendee.User.Id,
            DisplayName = attendee.User.DisplayName!,
            Bio = attendee.User.Bio,
            ImageUrl = attendee.User.ImageUrl,
            IsFollowing = isFollowing
        };
    }

    public static UserProfile MapUserToUserProfile(User user)
    {           
        return new UserProfile
        {
            Id = user.Id,
            DisplayName = user.DisplayName!,
            Bio = user.Bio,
            ImageUrl = user.ImageUrl,
            FollowersCount = user.Followers.Count,
            FollowingCount = user.Followings.Count,
        };
    }

    public static CommentDto MapComment(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Body = comment.Body,
            CreatedAt = comment.CreatedAt,
            UserId = comment.User.Id,
            DisplayName = comment.User.DisplayName!,
            ImageUrl = comment.User.ImageUrl,
            ActivityId = comment.ActivityId         
        };
    }
}