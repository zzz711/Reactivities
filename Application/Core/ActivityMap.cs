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

    public static ActivityDto MapActivityDTO(Activity activity, ICollection<ActivityAttendee> attendees)
    {
        var host = attendees.FirstOrDefault(x => x.IsHost)!.User;

        ICollection<UserProfile> users = [];       

        foreach(var attendee in attendees)
        {
            users.Add(MapUserProfile(attendee));
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

    public static UserProfile MapUserProfile(ActivityAttendee attendee)
    {
        return new UserProfile
        {
            Id = attendee.User.Id,
            DisplayName = attendee.User.DisplayName!,
            Bio = attendee.User.Bio,
            ImageUrl = attendee.User.ImageUrl
        };
    }
}